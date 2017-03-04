/*
*
* Copyright (C) 2006 Bill Tracey, KD5TFD, bill@ewjt.com 
* Copyright (C) 2010-2015  Doug Wigley
* 
* This program is free software; you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation; either version 2 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

namespace PowerSDR
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Net.NetworkInformation;
    //
    // routines to access audio from kd5tfd/vk6aph fpga based audio 
    // 
    partial class JanusAudio
    {
        //public JanusAudio()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        public static bool isFirmwareLoaded = false;

        // get ozy firmware version string - 8 bytes.  returns 
        // null for error 
        private static string getOzyFirmwareString()
        {
            IntPtr oz_h = OzyOpen();

            if (oz_h == (IntPtr)0)
            {
                return null;
            }
            IntPtr usb_h = JanusAudio.OzyHandleToRealHandle(oz_h);
            if (usb_h == (IntPtr)0)
            {
                JanusAudio.OzyClose(oz_h);
                return null;
            }

            byte[] buf = new byte[8];
            int rc = GetOzyID(usb_h, buf, buf.Length);

            // System.Console.WriteLine("read version rc: " + rc); 

            string result = null;

            if (rc == 8)    // got length we expected 
            {
                char[] cbuf = new char[8];
                for (int i = 0; i < 8; i++)
                {
                    cbuf[i] = (char)(buf[i]);
                }
                result = new string(cbuf);
                System.Console.WriteLine("version: >" + result + "<");
            }
            JanusAudio.OzyClose(oz_h);
            return result;
        }

        public static string setC1Opts(string opt)
        {
            int bits;
            int off_mask = 0xff;
            int on_mask = 0;

            string result = null;

            switch (opt)
            {
                case "--Atlas10MHz":
                    off_mask = 0xf3;  // 11110011
                    on_mask = 0;       // 10 meg atlas == 00xx
                    result = "Atlas10";
                    break;

                case "--Penny10MHz":
                    off_mask = 0xf3;  // 11110011
                    on_mask = 0x4;      // 10 meg penny == 01xx 
                    result = "Penny10";
                    break;

                case "--Mercury10Mhz":
                    off_mask = 0xf3;  // 11110011
                    on_mask = 0x8;      // 10 meg merc == 10xx 
                    result = "Merc10";
                    break;

                case "--Mercury125MHz":
                    off_mask = 0xef;     // 11101111
                    on_mask = 0x10;
                    result = "Merc125";
                    break;

                case "--CfgPenny":
                    off_mask = 0x9f;     // 10011111
                    on_mask = 0x20;
                    result = "CfgPenny";
                    break;

                case "--CfgMercury":
                    off_mask = 0x9f;     // 10011111
                    on_mask = 0x40;
                    result = "CfgMerc";
                    break;

                case "--CfgBoth":
                    off_mask = 0x9f;     // 10011111
                    on_mask = 0x60;
                    result = "CfgBoth";
                    break;

                case "--PennyMic":
                    off_mask = 0x7f;     // 01111111
                    on_mask = 0x80;
                    result = "PennyMic";
                    break;
            }

            bits = JanusAudio.GetC1Bits();
            bits &= off_mask;
            bits |= on_mask;
            JanusAudio.SetC1Bits(bits);
            return result;
        }

        private static string fx2_fw_version = "n/a";

        public static string getFX2FirmwareVersionString()
        {
            return fx2_fw_version;
        }

        public static void SetOutputPower(float f)
        {
            if (f < 0.0)
            {
                f = 0.0F;
            }
            if (f >= 1.0)
            {
                f = 1.0F;
            }

            int i = (int)(255 * f);
            //System.Console.WriteLine("output power i: " + i); 
            SetOutputPowerFactor(i);
        }

        // [DllImport("JanusAudio.dll")]
        // public static extern int getNetworkAddrs(Int32[] addrs, Int32 count); 

        //		private static bool MetisInitialized = false;
        // returns 0 on success, !0 on failure 

        // get the name of this PC and, using it, the IP address of the first adapter
        //static String strHostName = Dns.GetHostName();
        //public static IPAddress[] addr = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        // get a socket to send and receive on
        static Socket socket; // = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // set an endpoint
        static IPEndPoint iep;
        // receive data buffer for EP6 (normal data)
        static byte[] data = new byte[1444];
        const int MetisPort = 1024;
        const int LocalPort = 0;
       // private static IPEndPoint MetisEP = null;
        public static bool enableStaticIP = false;
        public static uint static_host_network = 0;
        public static bool FastConnect = false;
        public static HPSDRHW MetisBoardID = HPSDRHW.Hermes;
        public static byte MetisCodeVersion = 0;
        public static string EthernetHostIPAddress = "";
        public static string Metis_IP_address = "";
        public static string MetisMAC = "";
        private const int IP_SUCCESS = 0;
        private const short VERSION = 2;

        public static int initMetis()
        {
            int rc;
            int adapterIndex = adapterSelected - 1;
            IPAddress[] addr = null;
            bool cleanup = false;
            System.Console.WriteLine("MetisNetIPAddr: " + Console.getConsole().MetisNetworkIPAddr);

            try
            {
                addr = Dns.GetHostAddresses(Dns.GetHostName());
            }
            catch (SocketException e)
            {
                Win32.WSAData data = new Win32.WSAData();
                int result = 0;

                result = Win32.WSAStartup(VERSION, out data);
                if (result != IP_SUCCESS)
                {
                    System.Console.WriteLine(data.description);
                    Win32.WSACleanup();
                }

                addr = Dns.GetHostAddresses(Dns.GetHostName());
                cleanup = true;
                // System.Console.WriteLine("SocketException caught!!!");
                // System.Console.WriteLine("Source : " + e.Source);
                // System.Console.WriteLine("Message : " + e.Message);           
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception caught!!!");
                System.Console.WriteLine("Source : " + e.Source);
                System.Console.WriteLine("Message : " + e.Message);
            }

            GetNetworkInterfaces();

            List<IPAddress> addrList = new List<IPAddress>();

            // make a list of all the adapters that we found in Dns.GetHostEntry(strHostName).AddressList
            foreach (IPAddress a in addr)
            {
                // make sure to get only IPV4 addresses!
                // test added because Erik Anderson noted an issue on Windows 7.  May have been in the socket
                // construction or binding below.
                if (a.AddressFamily == AddressFamily.InterNetwork)
                {
                    addrList.Add(a);
                }
            }

            bool foundMetis = false;
            List<HPSDRDevice> mhd = new List<HPSDRDevice>();

            if (enableStaticIP)
            {
                Metis_IP_address = Console.getConsole().MetisNetworkIPAddr;
 
                IPAddress remoteIp = IPAddress.Parse(Metis_IP_address);
                IPEndPoint remoteEndPoint = new IPEndPoint(remoteIp, 0);
                Socket socket = new Socket(
                                      AddressFamily.InterNetwork,
                                      SocketType.Dgram,
                                      ProtocolType.Udp);
                IPEndPoint localEndPoint = QueryRoutingInterface(socket, remoteEndPoint);
                EthernetHostIPAddress = IPAddress.Parse(localEndPoint.Address.ToString()).ToString();

                socket.Close();
                socket = null;

                // if success set foundMetis to true, and fill in ONE mhd entry.
                IPAddress targetIP;
                IPAddress hostIP;
                if (IPAddress.TryParse(EthernetHostIPAddress, out hostIP) && IPAddress.TryParse(Metis_IP_address, out targetIP))
                {
                    System.Console.WriteLine(String.Format("Attempting fast re-connect to host adapter {0}, metis IP {1}", EthernetHostIPAddress, Metis_IP_address));

                    if (DiscoverMetisOnPort(ref mhd, hostIP, targetIP))
                    {
                        foundMetis = true;

                        // make sure that there is only one entry in the list!
                        if (mhd.Count > 0)
                        {
                            // remove the extra ones that don't match!
                            HPSDRDevice m2 = null;
                            foreach (var m in mhd)
                            {
                                if (m.IPAddress.CompareTo(Metis_IP_address) == 0)
                                {
                                    m2 = m;
                                }
                            }

                            // clear the list and put our single element in it, if we found it.
                            mhd.Clear();
                            if (m2 != null)
                            {
                                mhd.Add(m2);
                            }
                            else
                            {
                                foundMetis = false;
                            }
                        }
                    }
                }
            }

            if (FastConnect  && (EthernetHostIPAddress.Length > 0) && (Metis_IP_address.Length > 0))
            {
                // if success set foundMetis to true, and fill in ONE mhd entry.
                IPAddress targetIP;
                IPAddress hostIP;
                if (IPAddress.TryParse(EthernetHostIPAddress, out hostIP) && IPAddress.TryParse(Metis_IP_address, out targetIP))
                {
                    System.Console.WriteLine(String.Format("Attempting fast re-connect to host adapter {0}, metis IP {1}", EthernetHostIPAddress, Metis_IP_address));

                    if (DiscoverMetisOnPort(ref mhd, hostIP, targetIP))
                    {
                        foundMetis = true;

                        // make sure that there is only one entry in the list!
                        if (mhd.Count > 0)
                        {
                            // remove the extra ones that don't match!
                            HPSDRDevice m2 = null;
                            foreach (var m in mhd)
                            {
                                if (m.IPAddress.CompareTo(Metis_IP_address) == 0)
                                {
                                    m2 = m;
                                }
                            }

                            // clear the list and put our single element in it, if we found it.
                            mhd.Clear();
                            if (m2 != null)
                            {
                                mhd.Add(m2);
                            }
                            else
                            {
                                foundMetis = false;
                            }
                        }
                    }
                }
            }

            if (!foundMetis)
            {
                foreach (IPAddress ipa in addrList)
                {
                    if (DiscoverMetisOnPort(ref mhd, ipa, null))
                    {
                        foundMetis = true;
                    }
                }
            }

            if (!foundMetis)
            {
                if (cleanup)
                    Win32.WSACleanup();
                return -1;
            }

            int chosenDevice = 0;
            MetisBoardID = mhd[chosenDevice].deviceType;
            MetisCodeVersion = mhd[chosenDevice].codeVersion;
            Metis_IP_address = mhd[chosenDevice].IPAddress;
            MetisMAC = mhd[chosenDevice].MACAddress;
            EthernetHostIPAddress = mhd[chosenDevice].hostPortIPAddress.ToString();

            rc = nativeInitMetis(Metis_IP_address);
            return -rc;
        }

        public static int initOzy()
        {
            Console c = Console.getConsole();

            if (c != null && c.HPSDRisMetis)
            {
                return initMetis();
            }

            if (!isOzyAttached())
            {
                System.Console.WriteLine("Ozy not attached!!");
                return 1;
            }

            string oz_fw_version = getOzyFirmwareString();

            if (oz_fw_version == null)  // firmware not loaded -- load it
            {
                ProcessStartInfo start_info = new ProcessStartInfo();
                start_info.FileName = "initozy11.bat";
                start_info.UseShellExecute = true;
                Process p = new Process();
                p.StartInfo = start_info;
                bool rc = p.Start();
                // System.Console.WriteLine("start returned: " + rc); 
                p.WaitForExit();
                // System.Console.WriteLine("OzyInit completes"); 						

                // load it again 
                oz_fw_version = getOzyFirmwareString();
            }

            if (oz_fw_version == null)
            {
                return 1;
            }

            fx2_fw_version = oz_fw_version;

            /* else */
            isFirmwareLoaded = true;
            return 0;
        }

        public static bool fwVersionsChecked = false;
        private static string fwVersionMsg = null;

        public static string getFWVersionErrorMsg()
        {
            return fwVersionMsg;
        }

        public static bool forceFWGood = false;

        private static bool legacyDotDashPTT = false;

        // checks if the firmware versions are consistent - returns false if they are not 
        // and set fwVersionmsg to point to an appropriate message
        private static bool fwVersionsGood()
        {
            // return true;
            bool result = true;
            Console c = Console.getConsole();
            int penny_ver = 0;
            int mercury_ver = 0;
            byte[] metis_ver = new byte[1];
            int mercury2_ver = 0;

            // if (c.CurrentHPSDRModel == HPSDRModel.ANAN100D) c.RX2PreampPresent = true;
            if (forceFWGood == true || c.CurrentModel == Model.HERMES)
            {
                System.Console.WriteLine("Firmware ver check forced good!");
                return true;
            }

            if (c != null && c.HPSDRisMetis)
            {
                // GetMetisCodeVersion(metis_ver);

                if (c.PowerOn)
                {
                    byte metis_vernum = JanusAudio.MetisCodeVersion;//metis_ver[0];
                    mercury_ver = getMercuryFWVersion();

                    if (c.PennyPresent || c.PennyLanePresent)
                    {
                        do
                        {
                            // Thread.Sleep(500);
                            penny_ver = getPenelopeFWVersion();
                            if (penny_ver < 16 || penny_ver > 80)
                            {
                                Thread.Sleep(500);
                                penny_ver = getPenelopeFWVersion();
                                if (penny_ver > 0) break;
                                penny_ver = getPenelopeFWVersion();
                                if (penny_ver == 0) break;
                            }
                        }
                        while (penny_ver <= 15);
                    }

                    switch (metis_vernum)
                    {
                        case 16:
                        case 17:
                            if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 16)) ||
                                (c != null && c.MercuryPresent && (mercury_ver != 31 && mercury_ver != 71)))
                            {
                                result = false;
                                c.SetupForm.alex_fw_good = false;
                            }
                            break;
                        case 18:
                            if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 17)) ||
                                (c != null && c.MercuryPresent && (mercury_ver != 32)))
                            {
                                result = false;
                                c.SetupForm.alex_fw_good = false;
                            }
                            break;
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                            if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 17)) ||
                                (c != null && c.MercuryPresent && (mercury_ver != 33)))
                            {
                                result = false;
                                c.SetupForm.alex_fw_good = false;
                            }
                            break;
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                            if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 18)) ||
                                (c != null && c.MercuryPresent && (mercury_ver != 34)))
                            {
                                result = false;
                                c.SetupForm.alex_fw_good = false;
                            }
                            break;
                        default:
                            // fwVersionMsg = "Invalid Firmware Level.\nPowerSDR requires Mercury v3.1\nYou have version: " + mercury_ver.ToString("0\\.0");
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                            break;
                    }

                    mercury2_ver = getMercury2FWVersion();
                    if (mercury2_ver == 0)
                    {
                        Thread.Sleep(300);
                        mercury2_ver = getMercury2FWVersion();
                    }
                    if (mercury2_ver < 32 || mercury2_ver == 127) //check if physical rx2 present
                        c.RX2PreampPresent = false;
                    else
                        c.RX2PreampPresent = true;

                    if (c.SetupForm.FirmwareBypass == true) result = true;

                    if (!result)
                        fwVersionMsg = "Invalid Firmware.\nYou have Metis: " + metis_ver[0].ToString("0\\.0") +
                                                       "\nMercury:" + mercury_ver.ToString("0\\.0") +
                                                       "\nPenny:" + penny_ver.ToString("0\\.0");
                }
                return result;
            }

            string fx2_version_string = getFX2FirmwareVersionString();
            //int merc_ver = 0;
            int ozy_ver = 0;
            // int merc2_ver = 0;
            //System.Console.WriteLine("fx2: " + fx2_version_string); 
            //System.Console.WriteLine("ozy: " + ozy_ver); 
            //System.Console.WriteLine("merc: " + merc_ver); 
            //System.Console.WriteLine("penny: " + penny_ver); 
            //  c.SetI2CSpeed();
            // Thread.Sleep(100);

            if (fx2_version_string.CompareTo("20090524") >= 0)
            {
                //   do
                for (int i = 0; i < 5; i++)
                {
                    ozy_ver = getOzyFWVersion();
                    if (ozy_ver > 17) break;
                    Thread.Sleep(100);
                }
                //  while (ozy_ver < 12);
                // Thread.Sleep(2000);
                if (c.MercuryPresent)
                {
                    //  do
                    //  for (int i = 0; i < 2; i++)
                    {
                        mercury_ver = getMercuryFWVersion();
                        // if (mercury_ver > 0) break;
                        Thread.Sleep(100);
                    }
                    mercury_ver = getMercuryFWVersion();
                    mercury2_ver = getMercury2FWVersion();
                }

                if (c.PennyPresent || c.PennyLanePresent)
                {
                    do
                    {
                        // Thread.Sleep(500);
                        penny_ver = getPenelopeFWVersion();
                        if (penny_ver < 11)
                        {
                            Thread.Sleep(500);
                            penny_ver = getPenelopeFWVersion();
                            if (penny_ver > 0) break;
                            penny_ver = getPenelopeFWVersion();
                            if (penny_ver == 0) break;
                        }
                    }
                    while (penny_ver <= 10);
                }
                switch (ozy_ver)
                {
                    case 18:
                        // if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 13)) ||
                        //      (c != null && c.MercuryPresent && (mercury_ver != 29)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    case 19:
                        // if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 14)) ||
                        //      (c != null && c.MercuryPresent && (mercury_ver != 29)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    case 20:
                        // if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 15)) ||
                        //    (c != null && c.MercuryPresent && (mercury_ver != 30)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    case 21:
                        if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 16)) ||
                            (c != null && c.MercuryPresent && (mercury_ver != 31)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    case 22:
                        if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 17)) ||
                            (c != null && c.MercuryPresent && (mercury_ver != 32 && mercury_ver != 33)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    case 23:
                    case 24:
                        if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 17)) ||
                            (c != null && c.MercuryPresent && (mercury_ver != 33)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                        if ((c != null && (c.PennyPresent || c.PennyLanePresent) && (penny_ver != 18)) ||
                           (c != null && c.MercuryPresent && (mercury_ver != 34)))
                        {
                            result = false;
                            c.SetupForm.alex_fw_good = false;
                        }
                        break;
                    default:
                        result = false;
                        c.SetupForm.alex_fw_good = false;
                        break;
                }

                mercury2_ver = getMercury2FWVersion();
                if (mercury2_ver == 0)
                {
                    Thread.Sleep(300);
                    mercury2_ver = getMercury2FWVersion();
                }
                if (mercury2_ver < 32 || mercury2_ver == 127) //check if physical rx2 present
                    c.RX2PreampPresent = false;
                else
                    c.RX2PreampPresent = true;

                if (c.SetupForm.FirmwareBypass == true) result = true;

                if (!result)
                    fwVersionMsg = "Invalid Firmware.\nYou have Ozy: " + ozy_ver.ToString("0\\.0") +
                                                           "\nMercury: " + mercury_ver.ToString("0\\.0") +
                                                             "\nPenny: " + penny_ver.ToString("0\\.0");
            }
            return result;
        }

        // returns -101 for firmware version error 
        unsafe public static int StartAudio(int sample_rate, int samples_per_block, PA19.PaStreamCallback cb, int sample_bits, int no_send)
        {
            if (initOzy() != 0)
            {
                return 1;
            }
            int result = StartAudioNative(sample_rate, samples_per_block, cb, sample_bits, no_send);

            if (result == 0 && !fwVersionsChecked)
            {
                Thread.Sleep(100); // wait for frames 
                if (!fwVersionsGood())
                {
                    result = -101;
                }
                else
                {
                    fwVersionsChecked = true;
                }
            }
            //  InitMic();
            return result;
        }


        unsafe public static int GetDotDashPTT()
        {
            int bits = nativeGetDotDashPTT();
            if (legacyDotDashPTT)  // old style dot and ptt overloaded on 0x1 bit, new style dot on 0x4, ptt on 0x1 
            {
                if ((bits & 0x1) != 0)
                {
                    bits |= 0x4;
                }
            }
            return bits;
        }

        private static double freq_correction_factor = 1.0;
        public static double FreqCorrectionFactor
        {
            get { return freq_correction_factor; }
            set
            {
                freq_correction_factor = value;
                freqCorrectionChanged();
            }
        }

        public static void freqCorrectionChanged()
        {
            if (!Console.FreqCalibrationRunning)    // we can't be applying freq correction when cal is running 
            {
                SetVFOfreqRX1(lastVFORX1freq, true);
                SetVFOfreqRX2(lastVFORX2freq, true);
                SetVFOfreqRX3(lastVFORX3freq, true);
                SetVFOfreqRX4(lastVFORX4freq, true);
                SetVFOfreqRX5(lastVFORX5freq, true);
                SetVFOfreqTX(lastVFOTXfreq);
            }
        }

        private static double lastVFORX1freq = 0.0;
        unsafe public static void SetVFOfreqRX1(double f, bool offset)
        {
            // wdsp.RXANBPSetTuneFrequency(wdsp.id(0, 0), f * 1e6);
            // wdsp.RXANBPSetTuneFrequency(wdsp.id(0, 1), f * 1e6);
            lastVFORX1freq = f;
            int f_freq;
            f_freq = (int)((f * 1e6) * freq_correction_factor);
            SetRX1VFOfreq(f_freq); // center freq
        }

        private static double lastVFORX2freq = 0.0;
        unsafe public static void SetVFOfreqRX2(double f, bool offset)
        {
            // wdsp.RXANBPSetTuneFrequency(wdsp.id(2, 0), f * 1e6);
            lastVFORX2freq = f;
            int f_freq;
            f_freq = (int)((f * 1e6) * freq_correction_factor);
            SetRX2VFOfreq(f_freq);
        }

        private static double lastVFORX3freq = 0.0;
        unsafe public static void SetVFOfreqRX3(double f, bool offset)
        {
            lastVFORX3freq = f;
            int f_freq;
            if (offset)
                f_freq = (int)((f * 1e6 * freq_correction_factor) - low_freq_offset);
            else
                f_freq = (int)((f * 1e6) * freq_correction_factor);
            SetRX3VFOfreq(f_freq); // low frq
        }

        private static double lastVFORX4freq = 0.0;
        unsafe public static void SetVFOfreqRX4(double f, bool offset)
        {
            lastVFORX4freq = f;
            int f_freq;
            if (offset)
                f_freq = (int)((f * 1e6 * freq_correction_factor) + high_freq_offset);
            else
                f_freq = (int)((f * 1e6) * freq_correction_factor);
            SetRX4VFOfreq(f_freq); // high freq
        }

        private static double lastVFORX5freq = 0.0;
        unsafe public static void SetVFOfreqRX5(double f, bool offset)
        {
            lastVFORX5freq = f;
            int f_freq;
            f_freq = (int)((f * 1e6 * freq_correction_factor)); // + high_freq_offset);
            SetRX5VFOfreq(f_freq); // highest freq
        }

        unsafe public static void SetVFOfreqRX6(double f)
        {
            int f_freq = (int)((f * 1e6) + (high_freq_offset * 2.0));
            SetRX6VFOfreq(f_freq);
        }

        unsafe public static void SetVFOfreqRX7(double f)
        {
            int f_freq = (int)((f * 1e6) + (high_freq_offset * 3.0));
            SetRX7VFOfreq(f_freq);
        }

        private static double low_freq_offset;
        public static double LowFreqOffset
        {
            get { return low_freq_offset; }
            set
            {
                low_freq_offset = value;
            }
        }

        private static double high_freq_offset;
        public static double HighFreqOffset
        {
            get { return high_freq_offset; }
            set
            {
                high_freq_offset = value;
            }
        }

        private static double lastVFOTXfreq = 0.0;
        unsafe public static void SetVFOfreqTX(double f)
        {
            lastVFOTXfreq = f;
            int f_freq = (int)((f * 1e6) * freq_correction_factor);
            SetTXVFOfreq(f_freq);
            // c.SetupForm.txtTXVFO.Text = f_freq.ToString();
        }
        // 
        // compute fwd power from Penny based on count returned 
        // this conversion is a linear interpolation of values measured on an 
        // actual penny board 		
        // 
        /*    public static float computeFwdPower()
            {
                int power_int = getFwdPower();
                double computed_result = computePower(power_int);
                return (float)computed_result;
            }

            public static float computeRefPower()
            {
                Console c = Console.getConsole();
                int adc = JanusAudio.getRefPower();
                if (adc < 300) adc = 0;
                float volts = (float)adc * (3.3f / 4095.0f);
                float watts = (float)(Math.Pow(volts, 2) / 0.095f);

                if (c != null && c.PAValues)
                {
                    c.SetupForm.txtRevADCValue.Text = adc.ToString();
                    c.SetupForm.txtRevVoltage.Text = volts.ToString();
                  //  c.SetupForm.txtPARevPower.Text = watts.ToString();              
                } 

                return watts;
            }

            public static float computeAlexFwdPower()
            {
                Console c = Console.getConsole();
                int adc = JanusAudio.getAlexFwdPower();
                if (adc < 300) adc = 0;
                float volts = (float)adc * (3.3f / 4095.0f);
                float watts = (float)(Math.Pow(volts, 2) / 0.095f);

                if (c != null && c.PAValues)
                {
                    c.SetupForm.txtFwdADCValue.Text = adc.ToString();
                    c.SetupForm.txtFwdVoltage.Text = volts.ToString();
                   // c.SetupForm.txtPAFwdPower.Text = watts.ToString();
                } 

                return watts;
            }

            public static float computePower(int power_int)
            {
                // Console c = Console.getConsole();
                double power_f = (double)power_int;
                double result = 0.0;

                if (power_int <= 2095)
                {
                    if (power_int <= 874)
                    {
                        if (power_int <= 113)
                        {
                            result = 0.0;
                        }
                        else  // > 113 
                        {
                            result = (power_f - 113.0) * 0.065703;
                        }
                    }
                    else  // > 874 
                    {
                        if (power_int <= 1380)
                        {
                            result = 50.0 + ((power_f - 874.0) * 0.098814);
                        }
                        else  // > 1380 
                        {
                            result = 100.0 + ((power_f - 1380.0) * 0.13986);
                        }
                    }
                }
                else  // > 2095 
                {
                    if (power_int <= 3038)
                    {
                        if (power_int <= 2615)
                        {
                            result = 200.0 + ((power_f - 2095.0) * 0.192308);
                        }
                        else  // > 2615, <3038 
                        {
                            result = 300.0 + ((power_f - 2615.0) * 0.236407);
                        }
                    }
                    else  // > 3038 
                    {
                        result = 400.0 + ((power_f - 3038.0) * 0.243902);
                    }
                }

                result = result / 1000;  //convert to watts 
                // c.SetupForm.txtFwdPower.Text = result.ToString();
                // c.SetupForm.txtFwdADC.Text = power_int.ToString();

                return (float)result;
            } */

         // return true if ozy vid/pid found on usb bus .. native code does all the real work 
        unsafe static bool isOzyAttached()
        {
            int rc;
            rc = IsOzyAttached();
            if (rc == 0)
            {
                return false;
            }
            /* else */
            return true;
        }

        // Taken from: KISS Konsole
        public static List<NetworkInterface> foundNics = new List<NetworkInterface>();
        public static List<NicProperties> nicProperties = new List<NicProperties>();
        public static string numberOfIPAdapters;
        public static string Network_interfaces = null;  // holds a list with the description of each Network Adapter
        public static int adapterSelected = 1;           // from Setup form, the number of the Network Adapter to use

        public static void GetNetworkInterfaces()
        {
            // creat a string that contains the name and speed of each Network adapter 
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foundNics.Clear();
            nicProperties.Clear();

            Network_interfaces = "";
            int adapterNumber = 1;

            foreach (var netInterface in nics)
            {
                if ((netInterface.OperationalStatus == OperationalStatus.Up ||
                     netInterface.OperationalStatus == OperationalStatus.Unknown) &&
                    (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                 netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
                {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            NicProperties np = new NicProperties();
                            np.ipv4Address = addrInfo.Address;
                            np.ipv4Mask = addrInfo.IPv4Mask;
                            nicProperties.Add(np);
                        }
                    }
                }

                // if the length of the network adapter name is > 31 characters then trim it, if shorter then pad to 31.
                // Need to use fixed width font - Courier New
                string speed = "  " + (netInterface.Speed / 1000000).ToString() + "T";
                if (netInterface.Description.Length > 31)
                {
                    Network_interfaces += adapterNumber++.ToString() + ". " + netInterface.Description.Remove(31) + speed + "\n";
                }
                else
                {
                    Network_interfaces += adapterNumber++.ToString() + ". " + netInterface.Description.PadRight(31, ' ') + speed + "\n";
                }

                foundNics.Add(netInterface);
            }

            /*
                        foreach (NetworkInterface adapter in nics)
                        {
                            IPInterfaceProperties properties = adapter.GetIPProperties();

                            // if it's not 'up' (operational), ignore it.  (Dan Quigley, 13 Aug 2011)
                            if (adapter.OperationalStatus != OperationalStatus.Up)
                                continue;

                            // if it's a loopback interface, ignore it!
                            if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                                continue;

                            // get rid of non-ethernet addresses
                            if ((adapter.NetworkInterfaceType != NetworkInterfaceType.Ethernet) && (adapter.NetworkInterfaceType != NetworkInterfaceType.GigabitEthernet))
                                continue;

                            System.Console.WriteLine("");      // a blank line
                            System.Console.WriteLine(adapter.Description);
                            System.Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                            System.Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                            System.Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
                            System.Console.WriteLine("  Is receive only.......................... : {0}", adapter.IsReceiveOnly);
                            System.Console.WriteLine("  Multicast................................ : {0}", adapter.SupportsMulticast);
                            System.Console.WriteLine("  Speed    ................................ : {0}", adapter.Speed);

                            // list unicast addresses
                            UnicastIPAddressInformationCollection c = properties.UnicastAddresses;
                            foreach (UnicastIPAddressInformation a in c)
                            {
                                IPAddress addr = a.Address;
                                System.Console.WriteLine("  Unicast Addr ............................ : {0}", addr.ToString());
                                IPAddress mask = a.IPv4Mask;
                                System.Console.WriteLine("  Unicast Mask ............................ : {0}", (mask == null ? "null" : mask.ToString()));

                                NicProperties np = new NicProperties();
                                np.ipv4Address = a.Address;
                                np.ipv4Mask = a.IPv4Mask;

                                nicProperties.Add(np);
                            }

                            // list multicast addresses
                            MulticastIPAddressInformationCollection m = properties.MulticastAddresses;
                            foreach (MulticastIPAddressInformation a in m)
                            {
                                IPAddress addr = a.Address;
                                System.Console.WriteLine("  Multicast Addr .......................... : {0}", addr.ToString());
                            }

                            // if the length of the network adapter name is > 31 characters then trim it, if shorter then pad to 31.
                            // Need to use fixed width font - Courier New
                            string speed = "  " + (adapter.Speed / 1000000).ToString() + "T";
                            if (adapter.Description.Length > 31)
                            {
                                Network_interfaces += adapterNumber++.ToString() + ". " + adapter.Description.Remove(31) + speed + "\n";
                            }
                            else
                            {
                                Network_interfaces += adapterNumber++.ToString() + ". " + adapter.Description.PadRight(31, ' ') + speed + "\n";
                            }

                            foundNics.Add(adapter);
                        }
            */

            System.Console.WriteLine(Network_interfaces);

            // display number of adapters on Setup form
            numberOfIPAdapters = (adapterNumber - 1).ToString();
        }

        private static bool DiscoverMetisOnPort(ref List<HPSDRDevice> mhdList, IPAddress HostIP, IPAddress targetIP)
        {
            bool result = false;

            // configure a new socket object for each Ethernet port we're scanning
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Listen to data on this PC's IP address. Allow the program to allocate a free port.
            iep = new IPEndPoint(HostIP, LocalPort);  // was iep = new IPEndPoint(ipa, 0);

            try
            {
                // bind to socket and Port
                socket.Bind(iep);
                //   socket.ReceiveBufferSize = 0xFFFFF;   // no lost frame counts at 192kHz with this setting
                socket.Blocking = true;

                IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
                System.Console.WriteLine("Looking for Metis boards using host adapter IP {0}, port {1}", localEndPoint.Address, localEndPoint.Port);

                if (Metis_Discovery(ref mhdList, iep, targetIP))
                {
                    result = true;
                }

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Caught an exception while binding a socket to endpoint {0}.  Exception was: {1} ", iep.ToString(), ex.ToString());
                result = false;
            }
            finally
            {
                socket.Close();
                socket = null;
            }

            return result;
        }


        private static bool Metis_Discovery(ref List<HPSDRDevice> mhdList, IPEndPoint iep, IPAddress targetIP)
        {
            string MetisMAC;

            // socket.SendBufferSize = 0xFFFFFFF;

            // set up HPSDR Metis discovery packet
            byte[] Metis_discovery = new byte[63];
            Array.Clear(Metis_discovery, 0, Metis_discovery.Length);

            byte[] Metis_discovery_preamble = new byte[] { 0xEF, 0xFE, 0x02 };
            Metis_discovery_preamble.CopyTo(Metis_discovery, 0);

            bool have_Metis = false;            // true when we find an Metis
            int time_out = 0;

            // set socket option so that broadcast is allowed.
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            // need this so we can Broadcast on the socket
            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, MetisPort);
            string receivedIP = "";   // the IP address Metis obtains; assigned, from DHCP or APIPA (169.254.x.y)

            IPAddress hostPortIPAddress = iep.Address;
            IPAddress hostPortMask = IPAddress.Broadcast;

            // find the subnet mask that goes with this host port
            foreach (NicProperties n in nicProperties)
            {
                if (hostPortIPAddress.Equals(n.ipv4Address))
                {
                    hostPortMask = n.ipv4Mask;
                    break;
                }
            }

            broadcast = new IPEndPoint(IPAddressExtensions.GetBroadcastAddress(hostPortIPAddress, hostPortMask), MetisPort);

            // send every second until we either find an Metis board or exceed the number of attempts
            while (!have_Metis)            // #### djm should loop for a while in case there are multiple Metis boards
            {
                // send a broadcast to the port 1024
                socket.SendTo(Metis_discovery, broadcast);

                // now listen on  send port for any Metis cards
                System.Console.WriteLine("Ready to receive.... ");
                int recv;
                byte[] data = new byte[100];

                bool data_available;

                // await possibly multiple replies, if there are multiple Metis/Hermes on this port,
                // which MIGHT be the 'any' port, 0.0.0.0
                do
                {
                    // Poll the port to see if data is available 
                    data_available = socket.Poll(100000, SelectMode.SelectRead);  // wait 100 msec  for time out    

                    if (data_available)
                    {
                        EndPoint remoteEP = new IPEndPoint(IPAddress.None, 0);
                        recv = socket.ReceiveFrom(data, ref remoteEP);                 // recv has number of bytes we received
                        //string stringData = Encoding.ASCII.GetString(data, 0, recv); // use this to print the received data

                        System.Console.WriteLine("raw Discovery data = " + BitConverter.ToString(data, 0, recv));
                        // see what port this came from at the remote end
                        // IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
                        //  Console.Write(" Remote Port # = ",remoteIpEndPoint.Port);

                        string junk = Convert.ToString(remoteEP);  // see code in DataLoop

                        string[] words = junk.Split(':');

                        System.Console.Write(words[1]);

                        // get Metis MAC address from the payload
                        byte[] MAC = { 0, 0, 0, 0, 0, 0 };
                        Array.Copy(data, 3, MAC, 0, 6);
                        MetisMAC = BitConverter.ToString(MAC);
                        byte codeVersion = data[9];
                        byte boardType = data[10];

                        // check for HPSDR frame ID and type 2 (not currently streaming data, which also means 'not yet in use')
                        // changed to find Metis boards, even if alreay in use!  This prevents the need to power-cycle metis.
                        // (G Byrkit, 8 Jan 2012)
                        if ((data[0] == 0xEF) && (data[1] == 0xFE) && ((data[2] & 0x02) != 0))
                        {
                            System.Console.WriteLine("\nFound a Metis/Hermes/Griffin.  Checking whether it qualifies");

                            // get Metis IP address from the IPEndPoint passed to ReceiveFrom.
                            IPEndPoint ripep = (IPEndPoint)remoteEP;
                            IPAddress receivedIPAddr = ripep.Address;
                            receivedIP = receivedIPAddr.ToString();

                            System.Console.WriteLine("Metis IP from IP Header = " + receivedIP);
                            System.Console.WriteLine("Metis MAC address from payload = " + MetisMAC);
                            if (!SameSubnet(receivedIPAddr, hostPortIPAddress, hostPortMask))
                            {
                                // device is NOT on the subnet that this port actually services.  Do NOT add to list!
                                System.Console.WriteLine("Not on subnet of host adapter! Adapter IP {0}, Adapter mask {1}",
                                    hostPortIPAddress.ToString(), hostPortMask.ToString());
                            }
                            else if (receivedIPAddr.Equals(hostPortIPAddress))
                            {
                                System.Console.WriteLine("Rejected: contains same IP address as the host adapter; not from a Metis/Hermes/Griffin");
                            }
                            else if (MetisMAC.Equals("00-00-00-00-00-00"))
                            {
                                System.Console.WriteLine("Rejected: contains bogus MAC address of all-zeroes");
                            }
                            else
                            {
                                HPSDRDevice mhd = new HPSDRDevice();
                                mhd.IPAddress = receivedIP;
                                mhd.MACAddress = MetisMAC;
                                mhd.deviceType = (HPSDRHW)boardType;
                                mhd.codeVersion = codeVersion;
                                mhd.InUse = false;
                                mhd.hostPortIPAddress = hostPortIPAddress;

                                if (targetIP != null)
                                {
                                    if (mhd.IPAddress.CompareTo(targetIP.ToString()) == 0)
                                    {
                                        have_Metis = true;
                                        mhdList.Add(mhd);
                                        return true;
                                    }
                                }
                                else
                                {
                                    have_Metis = true;
                                    mhdList.Add(mhd);
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data  from Port = ");
                        if ((++time_out) > 3)
                        {
                            System.Console.WriteLine("Time out!");
                            return false;
                        }
                    }
                } while (data_available);
            }

            return have_Metis;
        }

        /// <summary>
        /// Determines whether the board and hostAdapter IPAddresses are on the same subnet,
        /// using subnetMask to make the determination.  All addresses are IPV4 addresses
        /// </summary>
        /// <param name="board">IP address of the remote device</param>
        /// <param name="hostAdapter">IP address of the ethernet adapter</param>
        /// <param name="subnetMask">subnet mask to use to determine if the above 2 IPAddresses are on the same subnet</param>
        /// <returns>true if same subnet, false otherwise</returns>
        public static bool SameSubnet(IPAddress board, IPAddress hostAdapter, IPAddress subnetMask)
        {
            byte[] boardBytes = board.GetAddressBytes();
            byte[] hostAdapterBytes = hostAdapter.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (boardBytes.Length != hostAdapterBytes.Length)
            {
                return false;
            }
            if (subnetMaskBytes.Length != hostAdapterBytes.Length)
            {
                return false;
            }

            for (int i = 0; i < boardBytes.Length; ++i)
            {
                byte boardByte = (byte)(boardBytes[i] & subnetMaskBytes[i]);
                byte hostAdapterByte = (byte)(hostAdapterBytes[i] & subnetMaskBytes[i]);
                if (boardByte != hostAdapterByte)
                {
                    return false;
                }
            }
            return true;
        }

        // Taken From: https://searchcode.com/codesearch/view/7464800/
        private static IPEndPoint QueryRoutingInterface(
                  Socket socket,
                  IPEndPoint remoteEndPoint)
        {
            SocketAddress address = remoteEndPoint.Serialize();

            byte[] remoteAddrBytes = new byte[address.Size];
            for (int i = 0; i < address.Size; i++)
            {
                remoteAddrBytes[i] = address[i];
            }

            byte[] outBytes = new byte[remoteAddrBytes.Length];
            socket.IOControl(
                        IOControlCode.RoutingInterfaceQuery,
                        remoteAddrBytes,
                        outBytes);
            for (int i = 0; i < address.Size; i++)
            {
                address[i] = outBytes[i];
            }

            EndPoint ep = remoteEndPoint.Create(address);
            return (IPEndPoint)ep;
        }

    }

    // Taken from: http://blogs.msdn.com/b/knom/archive/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks.aspx
    public static class IPAddressExtensions
    {
        public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }
    }

    public class HPSDRDevice
    {
        public HPSDRHW deviceType;   // which type of device (currently Metis or Hermes)
        public byte codeVersion;        // reported code version type
        public bool InUse;              // whether already in use
        public string IPAddress;        // currently, an IPV4 address
        public string MACAddress;       // a physical (MAC) address
        public IPAddress hostPortIPAddress;
    }

    public class NicProperties
    {
        public IPAddress ipv4Address;
        public IPAddress ipv4Mask;
    }


}

using System;
using System.Runtime.InteropServices;

namespace PowerSDR
{
    unsafe partial class JanusAudio
    {
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeInitMetisSockets();
        
        [DllImport("JanusAudio.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int nativeInitMetis(String netaddr);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetXVTREnable(int enable);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPennyPresent(int present);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableHermesPower(int enable);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetOutputPowerFactor(int i);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLegacyDotDashPTT(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetXmitBit(int xmitbit);  // bit xmitbit ==0, recv mode, != 0, xmit mode

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDelayXmit(int bit, int loops);  

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDiagData(int* a, int count);  // get diag data, count is how many slots are in array 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX1VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX2VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX3VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX4VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX5VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX6VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX7VFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTXVFOfreq(int f);  // tell aux hardware current freq -- in MHz 

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getControlByteIn(int n);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFPGATestMode(int i);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartAudioNative(int sample_rate, int samples_per_block, PA19.PaStreamCallback cb, int sample_bits, int no_send);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopAudio();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetC1Bits(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexManEnable(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexEnabled(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexHPFBits(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexLPFBits(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexTRRelayBit(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlex2HPFBits(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlex2LPFBits(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableApolloFilter(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableApolloTuner(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableApolloAutoTune(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableEClassModulation(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEERPWMmin(int min);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEERPWMmax(int max);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetHermesFilter(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetUserOut0(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetUserOut1(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetUserOut2(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetUserOut3(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool getUserI01(); // TX Inhibit input sense

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool getUserI02(); // Tx Inhibit input on Orion MKII board

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool getUserI03();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool getUserI04(); // external straight key input

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)] // sets number of receivers
        public static extern void SetNRx(int nrx);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)] // controls PureSignal
        public static extern void SetPureSignal(int enable);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)] // sets full or half duplex
        public static extern void SetDuplex(int dupx);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetC1Bits();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int nativeGetDotDashPTT();  // bit 0 = ptt, bit1 = dash asserted, bit 2 = dot asserted 
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OzyOpen();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void OzyClose(IntPtr ozyh);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OzyHandleToRealHandle(IntPtr ozh);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsOzyAttached();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMicBoost(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLineIn(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLineBoost(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexAtten(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMercDither(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMercRandom(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTxAttenData(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMercTxAtten(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX1Preamp(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRX2Preamp(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableADC1StepAtten(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableADC2StepAtten(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableADC3StepAtten(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetADC1StepAttenData(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetADC2StepAttenData(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetADC3StepAttenData(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMicTipRing(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMicBias(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMicPTT(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGndRx2onTx(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAndResetADC_Overload();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getSeqError();
        
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getMercuryFWVersion();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getMercury2FWVersion();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getMercury3FWVersion();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getMercury4FWVersion();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPenelopeFWVersion();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getOzyFWVersion();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getHaveSync();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getFwdPower();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getRefPower();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getAlexFwdPower();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAIN3();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAIN4();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getHermesDCVoltage();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableCWKeyer(int enable);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWSidetoneVolume(int vol);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWPTTDelay(int delay);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWHangTime(int hang);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWSidetoneFreq(int freq);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWKeyerSpeed(int speed);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWKeyerMode(int mode);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWKeyerWeight(int weight);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableCWKeyerSpacing(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReversePaddles(int bits);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWDash(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWDot(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCWX(int bit);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDiscoveryMode(int b);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPennyOCBits(int b);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSWRProtect(float g);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAlexAntBits(int rx_ant, int tx_ant, int rx_out);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEP4Data(char* bufp);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetADC_cntrl1(int g);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetADC_cntrl1();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetADC_cntrl2(int g);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetADC_cntrl2();

        // Diversity
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableDiversity2(int g);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMercSource(int g);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetrefMerc(int g);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetIQ_Rotate(double a, double b);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetIQ_RotateA(double a, double b);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetIQ_RotateB(double a, double b);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTheta(double a);

        // Ozyutils
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetOzyID(IntPtr usb_h, byte[] bytes, int length);

        //[DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        // unsafe extern public static bool Write_I2C(IntPtr usb_h, int i2c_addr, byte[] bytes, int length);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool WriteI2C(IntPtr usb_h, int i2c_addr, byte[] bytes, int length);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ReadI2C(IntPtr usb_h, int i2c_addr, byte[] bytes, int length);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Set_I2C_Speed(IntPtr hdev, int speed);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteControlMsg(IntPtr hdev, int requesttype, int request, int value,
                                              int index, byte[] bytes, int length, int timeout);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetAndResetAmpProtect();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAmpProtectRun(int run);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAIN4Voltage(int v);
     
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void isOrionMKII(int v);

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetProLpacks(int lpacks);
        
        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetOoopCounter();

        [DllImport("JanusAudio.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetOoopCounter();
        
    }
}

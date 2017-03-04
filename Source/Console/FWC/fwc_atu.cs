//=================================================================
// fwc_atu.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2004-2009  FlexRadio Systems
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// You may contact us via email at: sales@flex-radio.com.
// Paper mail may be sent to: 
//    FlexRadio Systems
//    8900 Marybank Dr.
//    Austin, TX 78750
//    USA
//=================================================================

using System;
using System.Threading;
using System.Diagnostics;

namespace PowerSDR
{
	public class FWCATU
	{
		#region Enums

		// host to ATU commands - description
		private enum ATURequest
		{
			REQ_NOOP = 0,			// No Operation
			REQ_INDUP = 1,			// Inductor Up. Increase the current inductor value by one.
			REQ_INDDN = 2,			// Inductor Down. Decrease the current inductor value by one.
			REQ_CAPUP = 3,			// Capacitor Up. Increase the current capacitor value by one.
			REQ_CAPDN = 4,			// Capacitor Down. Decrease the current capacitor value by one.
			REQ_MEMTUNE = 5,		// Request Memory Tune. Attempt to tune via a built-in stored tune value for this frequency. Will fall through to a full tune if the memory tune fails.
			REQ_FULLTUNE = 6,		// Request Full Tune. Do a complete tuning sequence.
			REQ_HIZ = 	8,			// Set the HiLoZ relay to High Impedance.
			REQ_LOZ = 	9,			// Set the HiLoZ relay to Low Impedance.
			REQ_ANT1 = 10,			// Select Antenna 1
			REQ_ANT2 = 11,			// Select Antenna 2
			REQ_ALLUPDATE = 40,		// Request that the tuner send an update of current relay settings.
			REQ_VERSION = 41,		// Ask the tuner for its product ID and version number.
			REQ_ARM_CLEAR = 42,		// Arm the memory clear routine. This must be sent immediately before a REQ_CLEAR MEM command.
			REQ_CLEAR_MEM = 43,		// Clear EEPROM memory data. The memory clear routine must first be armed by sending a REQ_ARM CLEAR command. If any intervening commands are sent, the memory clear armed status is unset, and the memory clear will not take place.
			REQ_TUNER_STANDBY = 44, // Place the tuner in standby mode.
			REQ_TUNER_ACTIVE = 45,	// Take the tuner out of standby mode.
			REQ_MANUAL_STORE = 46,	// Store Tune. Stores the current inductor and capacitor relays settings at the memory location corresponding to the last frequency transmitted on.
			REQ_SWR11 = 50,			// Set SWR threshold for “good tune” to 1.1:1 or lower.
			REQ_SWR13 = 51,			// Set SWR threshold for “good tune” to 1.3:1 or lower.
			REQ_SWR15 = 52,			// Set SWR threshold for “good tune” to 1.5:1 or lower.
			REQ_SWR17 = 53,			// Set SWR threshold for “good tune” to 1.7:1 or lower.
			REQ_SWR20 = 54,			// Set SWR threshold for “good tune” to 2.0:1 or lower.
			REQ_SWR25 = 55,			// Set SWR threshold for “good tune” to 2.5:1 or lower.
			REQ_SWR30 = 56,			// Set SWR threshold for “good tune” to 3.0:1 or lower.
			REQ_RESET = 57,			// Reset L and C relays, HiLoZ relay
			REQ_AUTO_ON = 58,		// Request Automatic Tuning On
			REQ_AUTO_OFF = 59,		// Request Automatic Tuning Off
			REQ_FWD_PWR = 60,		// Request Current FWD Power
			REQ_REV_PWR = 61,		// Request Current Reflected power
			REQ_SWR = 62,			// Request Current SWR
			REQ_UPDATE_ON = 63,		// Turn live updates on
			REQ_UPDATE_OFF = 64,	// Turn live updates off
			REQ_SET_IND = 65,		// Directly set Inductor (and HiLoZ) relay value
			REQ_SET_CAP = 66,		// Directly set Capacitor relay value
			REQ_SET_FREQ = 67,		// Request that tuner recalls tuning memory relay setting for the specified frequency
			REQ_MEM_DUMP = 68,		// Request memory dump of all of EEPROM data.
		}

		//Command Name = Value (dec)	Parm1	Parm2	Description
		private enum ATUResponse
		{
			CMD_NOOP = 00,			//	none	none	No Operation - Also sent to indicate AT200PC has woken up
			CMD_INDVAL = 01,		//	0-127	none	Inductor Value
			CMD_CAPVAL = 02,		//	0-127	none	Capacitor Value
			CMD_HILOZ = 03,			//	0 or 1	none	HiLoZ relay value
			CMD_ANTENNA = 04,		//	0 or 1	none	Antenna Selection
			CMD_FWDPWR = 05,		//	MSB		LSB		Forward Power, in hundredths of watts.  I.e. a value of 1000 indicates 10.0 watts
			CMD_REVPWR = 18,		//	MSB		LSB		Reflected Power, in hundredths of watts.
			CMD_SWR = 06,			//	none	LSB		SWR
			CMD_TXFREQ = 07,		//	MSB		LSB		Transmit period
			CMD_TUNEPASS = 09,		//	none	none	Indicates that the requested tuning operation succeeded.
			CMD_TUNEFAIL = 10,		//	0-2		none	Indicates that the requested tuning operation failed. Byte 2 indicates reason for failure.
			CMD_VERSION = 11,		//	Major	Minor	Indicates product major and minor version number.
			CMD_CLEAR_DONE = 12,	//	none	none	Indicates that the EEPROM erase operation has completed.
			CMD_INSTANDBY = 13,		//	none	none	Indicates that the tuner has been placed in standby mode.
			CMD_ACTIVE = 14,		//	none	none	Indicates that the tuner has restored the settings that were saved prior to entering standby mode.
			CMD_STORE_OK = 15,		//	none	none	Indicated that the requested store to memory operation has completed. Note that this will be indicated whether the store operation failed or succeeded.
			CMD_SWRTHRESH = 16,		//	0-6		none	Indicates the currently active SWR threshold used for tuning.
			CMD_AUTO_STATUS = 17,	//	0 or 1	none	Report Auto Tune setting.
			CMD_UPDATE_STATUS = 19,	//	0 or 1	none	Indicate Live Update on/off.
		}

		private enum ATUTuneFail
		{
			TUNEFAIL_NO_RF = 00, // No RF was detected.
			TUNEFAIL_RF_CARRIER_LOST = 01, // RF Carrier was lost before the tune completed.
			TUNEFAIL_UNABLE_TO_BRING_SWR_DOWN = 02, // The tuner was unable to bring the SWR down below the SWR Threshold.
		}

		#endregion

		#region Variable/Property Declarations

		private static byte ind_val = 0;
		public static byte InductorValue
		{
			get { return ind_val; }
		}

		private static byte cap_val = 0;
		public static byte CapacitorValue
		{
			get { return cap_val; }
		}

		private static byte hi_lo_z = 0;
		public static byte HiLoZ
		{
			get { return hi_lo_z; }
		}

		private static byte antenna = 0; // note FLEX-5000 only uses Antenna 1
		public static byte Antenna
		{
			get { return antenna; }
		}

		private static double fwd_pwr = 0; // in watts
		public static double ForwardPower
		{
			get { return fwd_pwr; }
		}

		private static double rev_pwr = 0; // in watts
		public static double ReflectedPower
		{
			get { return rev_pwr; }
		}

		private static double swr = 1.0f;
		public static double SWR
		{
			get { return swr; }
		}

		private static double txfreq = 0.0f; // in MHz
		public static double TXFreq
		{
			get { return txfreq; }
		}

		private static bool tune_pass = false;
		public static bool TunePass
		{
			get { return tune_pass; }
		}

		private static byte tune_fail = 0;
		public static byte TuneFail
		{
			get { return tune_fail; }
		}

		private static double sw_version = 1.7;
		public static double SWVersion
		{
			get { return sw_version; }
		}

		private static bool active = true;
		public static bool Active
		{
			get { return active; }
		}

		private static double swr_thresh = 3.0;
		public static double SWRThreshold
		{
			get { return swr_thresh; }
		}

		private static byte auto_status = 1;
		public static byte AutoStatus
		{
			get { return auto_status; }
		}

		private static byte update_status = 0;
		public static byte UpdateStats
		{
			get { return update_status; }
		}

		#endregion

		#region Public Routines

		public static void NoOp()
		{
			FWC.ATUSendCmd((byte)ATURequest.REQ_NOOP, 0, 0);
		}

		public static void IncrementInductance()
		{
			byte cmd, b2, b3, b4;
			FWC.ATUSendCmd((byte)ATURequest.REQ_INDUP, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		public static void DecrementInductance()
		{
			byte cmd, b2, b3, b4;
			FWC.ATUSendCmd((byte)ATURequest.REQ_INDDN, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		public static void IncrementCapacitance()
		{
			byte cmd, b2, b3, b4;
			FWC.ATUSendCmd((byte)ATURequest.REQ_CAPUP, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		public static void DecrementCapacitance()
		{
			byte cmd, b2, b3, b4;
			FWC.ATUSendCmd((byte)ATURequest.REQ_CAPDN, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		public static void MemoryTune()
		{
			int count = 0;
			bool done_tuning = false;
			byte cmd, b2, b3, b4;
			FWC.ATUSendCmd((byte)ATURequest.REQ_MEMTUNE, 0, 0);
			while(!done_tuning)
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
				switch((ATUResponse)cmd)
				{
					case ATUResponse.CMD_TUNEPASS:
					case ATUResponse.CMD_TUNEFAIL:
						done_tuning = true;
						break;
				}
				Thread.Sleep(100);
				if(count++ > 100)
					break;
			}
			
			for(int i=0; i<13; i++)
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				if(cmd == 200 || b4 == 0) break;
				ParseResult(cmd, b2, b3);
			}

			if(tune_pass)
				Debug.WriteLine("fwd: "+fwd_pwr.ToString("f2")+" ref: "+rev_pwr.ToString("f2")+" swr: "+swr.ToString("f2")+":1");
			else
				Debug.WriteLine("Tune Failed: "+((ATUTuneFail)tune_fail).ToString());
		}

		public static void FullTune()
		{
			int count = 0;
			bool done_tuning = false;
			byte cmd, b2, b3, b4;
			FWC.ATUSendCmd((byte)ATURequest.REQ_FULLTUNE, 0, 0);
			while(!done_tuning)
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
				switch((ATUResponse)cmd)
				{
					case ATUResponse.CMD_TUNEPASS:
					case ATUResponse.CMD_TUNEFAIL:
						done_tuning = true;
						break;
				}
				Thread.Sleep(100);
				if(count++ > 200)
					break;
			}

			for(int i=0; i<13; i++)
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				if(cmd == 200 || b4 == 0) break;
				ParseResult(cmd, b2, b3);
			}

			if(tune_pass)
				Debug.WriteLine("fwd: "+fwd_pwr.ToString("f2")+" ref: "+rev_pwr.ToString("f2")+" swr: "+swr.ToString("f2")+":1");
			else
				Debug.WriteLine("Tune Failed: "+((ATUTuneFail)tune_fail).ToString());
		}

		public static void Activate(bool b)
		{
			byte cmd, b2, b3, b4;
			if(b) cmd = (byte)ATURequest.REQ_TUNER_ACTIVE;
			else cmd = (byte)ATURequest.REQ_TUNER_STANDBY;
			FWC.ATUSendCmd(cmd, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		public static void AutoTuning(bool b)
		{
			byte cmd, b2, b3, b4;
			if(b) cmd = (byte)ATURequest.REQ_AUTO_ON;
			else cmd = (byte)ATURequest.REQ_AUTO_OFF;
			FWC.ATUSendCmd(cmd, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		public static void SetSWRThreshold(double swr_thresh)
		{
			byte cmd=0, b2, b3, b4;
			if(swr_thresh == 1.1) cmd = (byte)ATURequest.REQ_SWR11;
			else if(swr_thresh == 1.3) cmd = (byte)ATURequest.REQ_SWR13;
			else if(swr_thresh == 1.5) cmd = (byte)ATURequest.REQ_SWR15;
			else if(swr_thresh == 1.7) cmd = (byte)ATURequest.REQ_SWR17;
			else if(swr_thresh == 2.0) cmd = (byte)ATURequest.REQ_SWR20;
			else if(swr_thresh == 2.5) cmd = (byte)ATURequest.REQ_SWR25;
			else if(swr_thresh == 3.0) cmd = (byte)ATURequest.REQ_SWR30;
			FWC.ATUSendCmd(cmd, 0, 0);
			do
			{
				FWC.ATUGetResult(out cmd, out b2, out b3, out b4, 200);
				ParseResult(cmd, b2, b3);
			} while(b4 > 0);
		}

		#endregion

		#region Private Routines

		private static void ParseResult(byte cmd, byte b2, byte b3)
		{
			//Debug.WriteLine("ParseResult: "+(count++)+" cmd:"+cmd+" b2:"+b2+" b3"+b3);
			
			if(cmd < 0 || cmd > 19) return; // only process valid commands
			switch((ATUResponse)cmd)
			{
				case ATUResponse.CMD_NOOP:
					break;
				case ATUResponse.CMD_INDVAL:
					ind_val = b2;
					break;
				case ATUResponse.CMD_CAPVAL:
					cap_val = b2;
					break;
				case ATUResponse.CMD_HILOZ:
					hi_lo_z = b2;
					break;
				case ATUResponse.CMD_ANTENNA:
					antenna = b2;
					if(antenna != 0) 
						Debug.WriteLine("ATU Warning: FLEX-5000 only uses Antenna 1");
					break;
				case ATUResponse.CMD_FWDPWR:
					fwd_pwr = ((b2<<8) + b3)*0.01;
					break;
				case ATUResponse.CMD_REVPWR:
					rev_pwr = ((b2<<8) + b3)*0.01;
					break;
				case ATUResponse.CMD_SWR:
					double ro = Math.Sqrt((double)b3/256);
					swr = (1+ro)/(1-ro);
					break;
				case ATUResponse.CMD_TXFREQ:
					txfreq = (double)32768 / (((b2<<8) + b3)*1.6);
					break;
				case ATUResponse.CMD_TUNEPASS:
					tune_pass = true;
					break;
				case ATUResponse.CMD_TUNEFAIL:
					tune_pass = false;
					tune_fail = b2;
					break;
				case ATUResponse.CMD_VERSION:
					sw_version = (b3>>4)+(b3&0xF)*0.1;
					break;
				case ATUResponse.CMD_CLEAR_DONE:
					break;
				case ATUResponse.CMD_INSTANDBY:
					active = false;
					break;
				case ATUResponse.CMD_ACTIVE:
					active = true;
					break;
				case ATUResponse.CMD_STORE_OK:
					break;
				case ATUResponse.CMD_SWRTHRESH:
				switch(b2)
				{
					case 0: swr_thresh = 1.1; break;
					case 1: swr_thresh = 1.3; break;
					case 2: swr_thresh = 1.5; break;
					case 3: swr_thresh = 1.7; break;
					case 4: swr_thresh = 2.0; break;
					case 5: swr_thresh = 2.5; break;
					case 6: swr_thresh = 3.0; break;
				}
					break;
				case ATUResponse.CMD_AUTO_STATUS:
					auto_status = b2;
					break;
				case ATUResponse.CMD_UPDATE_STATUS:
					update_status = b2;
					break;
			}
		}

		#endregion
	}
}
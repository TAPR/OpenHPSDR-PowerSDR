//=================================================================
// usb.cs
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

using System.Runtime.InteropServices;
using System.Windows.Forms;

// TODO: Get all form references out of this driver.
// Just error numbers and have a function to look up errors.

namespace PowerSDR
{
	unsafe class USB
	{
		#region USB Variable and DLL Declarations
		
		private static Console console;
		public static Console Console
		{
			set { console = value; }
		}

		private static int deviceID = -1;
		public static int DeviceID
		{
			get { return deviceID; }
		}

		[DllImport("Sdr1kUsb.dll")]
		private static extern int Sdr1kOpen(string appName, uint config);

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kClose")]
		private static extern int Sdr1kCloseDll(int deviceID);
		public static int Sdr1kClose()
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kCloseDll(deviceID);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kSetNotify")]
		private static extern int Sdr1kSetNotifyDll(int deviceID, void *hEvent);
		public static int Sdr1kSetNotify(void *hEvent)
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kSetNotifyDll(deviceID, hEvent);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kGetStatusPort")]
		private static extern int Sdr1kGetStatusPortDll(int deviceID);
		public static int Sdr1kGetStatusPort()
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kGetStatusPortDll(deviceID);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kLatch")]
		private static extern int Sdr1kLatchDll(int deviceID, byte latch, byte data);
		public static int Sdr1kLatch(byte latch, byte data)
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kLatchDll(deviceID, latch, data);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kDDSReset")]
		private static extern int Sdr1kDDSResetDll(int deviceID);
		public static int Sdr1kDDSReset()
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kDDSResetDll(deviceID);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kDDSWrite")]
		private static extern int Sdr1kDDSWriteDll(int deviceID, byte addr, byte data);
		public static int Sdr1kDDSWrite(byte addr, byte data)
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kDDSWriteDll(deviceID, addr, data);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kSRLoad")]
		private static extern int Sdr1kSRLoadDll(int deviceID, byte reg, byte data);
		public static int Sdr1kSRLoad(byte reg, byte data)
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kSRLoadDll(deviceID, reg, data);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		[DllImport("Sdr1kUsb.dll", EntryPoint="Sdr1kGetADC")]
		private static extern int Sdr1kGetADCDll(int deviceID);
		public static int Sdr1kGetADC()
		{
			if(deviceID < 0) return SDR1K_ERR_DEVICENOTFOUND;
			int status = Sdr1kGetADCDll(deviceID);
			if(status < 0 && status != SDR1K_ERR_ADCREADINGLOST)
			{
				console.PowerOn = false;
				console.USBPresent = false;
				DisplayError(status);
			}
			return status;
		}

		private const uint SDR1K_CFG_RFE	= 0x01;
		private const uint SDR1K_CFG_ADC	= 0x02;
		public const byte SDR1K_LATCH_EXT	= 0x01;
		public const byte SDR1K_LATCH_BPF	= 0x02;

		private const int SDR1K_ERR_DEVICENOTFOUND	= -1;
		private const int SDR1K_ERR_DEVICEINUSE		= -2;
		private const int SDR1K_ERR_DEVICENOTOPEN	= -3;
		private const int SDR1K_ERR_USBINITFAILED	= -4;
		private const int SDR1K_ERR_USBDISCONNECTED = -5;
		private const int SDR1K_ERR_RFENOTENABLED	= -10;
		private const int SDR1K_ERR_ADCNOTENABLED	= -11;
		private const int SDR1K_ERR_ADCREADINGLOST	= -12;

		#endregion

		#region Misc Routines

		public static bool Init(bool rfe, bool pa)
		{
			if(deviceID >= 0)		// already open
				return true;

			uint options = 0;
			if(rfe) options |= SDR1K_CFG_RFE;
			if(pa) options |= SDR1K_CFG_ADC;

			deviceID = Sdr1kOpen("PowerSDR", options);
			if (deviceID < 0)
			{
				MessageBox.Show("USB Device Not Found.  ("+deviceID.ToString()+")");
				return false;
			}
			else return true;
		}

		public static void Exit()
		{
			if(deviceID < 0)
				return;

			Sdr1kClose();
			deviceID = -1;
		}

		private static void DisplayError(int status)
		{
			string error_msg = "";
			switch(status)
			{
				case SDR1K_ERR_DEVICENOTFOUND:
					error_msg = "Device Not Found";
					break;
				case SDR1K_ERR_DEVICEINUSE:
					error_msg = "Device In Use";
					break;
				case SDR1K_ERR_DEVICENOTOPEN:
					error_msg = "Device Not Open";
					break;
				case SDR1K_ERR_USBINITFAILED:
					error_msg = "USB Init Failed";
					break;
				case SDR1K_ERR_USBDISCONNECTED:
					error_msg = "USB Disconnected";
					break;
				case SDR1K_ERR_RFENOTENABLED:
					error_msg = "RFE Option Not Enabled";
					break;
				case SDR1K_ERR_ADCNOTENABLED:
					error_msg = "ADC Not Enabled";
					break;
				case SDR1K_ERR_ADCREADINGLOST:
					error_msg = "ADC Reading Lost";
					break;
			}

			MessageBox.Show("USB Adapter Error: "+error_msg+" ("+status.ToString()+").\n"+
				"Please note that the USB Adapter has been disabled.  To enable it, please open the Setup Form\n"+
				"and enable it manually on the General Tab.",
				"USB Adapter Error",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}

		#endregion
	}
}
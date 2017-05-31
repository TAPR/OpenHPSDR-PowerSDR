//=================================================================
// ad9854.cs
//=================================================================
// Controls a AD9854 DDS for automated test stand qualification.
// Note: This code will not work with the DDS on the SDR-1000
// without modification.
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


namespace PowerSDR
{
    using System;
    using System.Threading;
    using PortTalk;

	public class AD9854
	{
		#region Enums

		private enum LPTRegisters
		{
			DATA = 0,
			STATUS = 1,
			CTRL = 2,
		}

		#endregion

		#region Variable Declaration

		public static ushort lpt_addr = 0x378;
		private static int delay_value = 0;	// Value that sets the delay in Deelay()		   

		#endregion

		#region Properties

		private static int i_amp = 4095;
		public static int IAmp
		{
			set
			{
				int temp = i_amp;
				if(value < 0) value = 0;
				if(value > 4095) value = 4095;
				i_amp = value;
				if(i_amp != temp)
				{
					PWrite((byte)(i_amp >> 8), 33);
					PWrite((byte)(i_amp), 34);
				}
			}
		}
		
		private static int q_amp = 4095;
		public static int QAmp
		{
			set
			{
				int temp = q_amp;
				if(value < 0) value = 0;
				if(value > 4095) value = 4095;
				q_amp = value;
				if(q_amp != temp)
				{
					PWrite((byte)(q_amp >> 8), 35);
					PWrite((byte)(q_amp), 36);
				}
			}
		}

		public static float IAmplitude
		{
			set
			{
				if(value > 1.0) value = 1.0f;
				if(value < 0) value = 0;
				IAmp = (int)(value*4095.0);
			}
		}

		public static float QAmplitude
		{
			set
			{
				if(value > 1.0) value = 1.0f;
				if(value < 0) value = 0;
				QAmp = (int)(value*4095.0);
			}
		}

		private static byte reg_29 = 16;
		public static bool PowerDownFullDigital
		{
			set
			{
				int temp = reg_29;
				if(value) reg_29 |= 0x01;
				else reg_29 &= 0xFE;
				if(temp != reg_29)
					PWrite(reg_29, 29);
			}
		}

		public static bool PowerDownAllDAC
		{
			set
			{
				int temp = reg_29;
				if(value) reg_29 |= 0x02;
				else reg_29 &= 0xFD;
				if(temp != reg_29)
					PWrite(reg_29, 29);
			}
		}

		public static bool PowerDownQDAC
		{
			set
			{
				int temp = reg_29;
				if(value) reg_29 |= 0x04;
				else reg_29 &= 0xFB;
				if(temp != reg_29)
					PWrite(reg_29, 29);
			}
		}

		public static bool PowerDownPLL
		{
			set
			{
				int temp = reg_29;
				if(value) reg_29 |= 0x08;
				else reg_29 &= 0xF7;
				if(temp != reg_29)
					PWrite(reg_29, 29);
			}
		}

		public static bool PowerDownComparator
		{
			set
			{
				int temp = reg_29;
				if(value) reg_29 |= 0x10;
				else reg_29 &= 0xEF;
				if(temp != reg_29)
					PWrite(reg_29, 29);
			}
		}

		private static float clock_correction = 0;
		public static int ClockCorrection
		{
			set	{ clock_correction = (float)(value*0.000001); }
		}
		
//		private static byte reg_30 = 16;
//		public static byte RefMult
//		{
//			get { return reg_30&0x1F; }
//			set
//			{
//				if(ref_mult < 4)
//					reg_30 = 0x

		#endregion

		#region Private Routines

		private static void LatchData()
		{
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 0xA);		// Drive bit high.
			Thread.Sleep(new TimeSpan(delay_value));
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 0xB);		// Drive bit low.
			Thread.Sleep(new TimeSpan(delay_value));
		}
        
		private static void LatchAddr()
		{
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 0x9);		// Drive bit high.
			Thread.Sleep(new TimeSpan(delay_value));
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 0xB);		// Drive bit low.
			Thread.Sleep(new TimeSpan(delay_value));
		}

		private static void LatchCtrl()
		{
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 3);		// Drive bit high.
			Thread.Sleep(new TimeSpan(delay_value));
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 11);		// Drive bit low.
			Thread.Sleep(new TimeSpan(delay_value));
		}

		private static void PWrite(byte data, byte addr)
		{
			// Set up data bits.
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), (byte)(~data));
			LatchData();

			// Set up addr bits.
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), (byte)(~addr));
			LatchAddr();

			// WRBAR,RESET,UDCLK = lo
			// RDBAR,PMODE = hi
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), 237);
			LatchCtrl();

			// RESET,UDCLK = lo
			// WRBAR,RDBAR,PMODE = hi
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), 236);
			LatchCtrl();
		}

		#endregion

		#region Public Routines

		public static void ResetDDS()
		{
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.CTRL), 11);
			
			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), 255);	
			LatchData();

			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), 255);
			LatchAddr();

			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), 232);
			LatchCtrl();

			Parallel.outport((ushort)(lpt_addr+LPTRegisters.DATA), 236);
			LatchCtrl();

			// Setup Options
			PWrite(reg_29, 29);
			

			// Setup Amplitude
			PWrite((byte)(i_amp >> 8), 33);	// I High
			PWrite((byte)(i_amp & 0x00FF), 34);	// I Low
			PWrite((byte)(q_amp >> 8), 35);	// Q High
			PWrite((byte)(q_amp & 0x00FF), 36);	// Q Low
			PWrite(128, 37);	// Ramp Rate Register
			PWrite(32, 32);

			// Setup Clock Frequency
			PWrite(0x04, 0x1E);
			//PWrite(100, 30);
		}

		public static void SetFreq(double freq)		// in MHz
		{
			//Debug.WriteLine(freq);
			long tuning_word = (long)(freq / (200.0 + clock_correction) * Math.Pow(2, 48));

			for(int i=0; i<6; i++)
			{
				byte b = (byte)(tuning_word >> (40 - i * 8));
				PWrite(b, (byte)(4 + i));
			}
		}

		#endregion
	}
}
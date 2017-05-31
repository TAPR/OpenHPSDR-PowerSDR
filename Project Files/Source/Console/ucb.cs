//=================================================================
// ucb.cs
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

//#define DEBUG_VERBOSE

namespace PowerSDR
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

	public class UCB
	{
		private const int TIME_OUT_COUNT = 2000;

		private static int send_bit_delay1 = 10000;
		public static int SendBitDelay1
		{
			get { return send_bit_delay1; }
			set { send_bit_delay1 = value; }
		}

		private static int send_bit_delay2 = 10000;
		public static int SendBitDelay2
		{
			get { return send_bit_delay2; }
			set { send_bit_delay2 = value; }
		}

		private static int receive_bit_delay1 = 10000;
		public static int ReceiveBitDelay1
		{
			get { return receive_bit_delay1; }
			set { receive_bit_delay1 = value; }
		}

		private static int receive_bit_delay2 = 1000;
		public static int ReceiveBitDelay2
		{
			get { return receive_bit_delay2; }
			set { receive_bit_delay2 = value; }
		}

		public enum CMD
		{
			DISABLE_OUTPUTS_AND_CLEAR_MATRIX = 0x8C,	// Disable all outputs, Clear the Matrix
			DISABLE_OUTPUTS = 0x8B,					// Disable all outputs, don’t clear the matrix
			RESUME = 0x8A,			// Resume Normal operation, address lines 1 – 4 control the UCB output lines
			DELAY_2	 = 0x80,		// Set a communications delay of  2ms
			DELAY_5	 = 0x81,		// Set a communications delay of  5ms 
			DELAY_10 = 0x82,		// Set a communications delay of 10ms
			DELAY_20 = 0x83,		// Set a communications delay of 20ms
			DELAY_40 = 0x84,		// Set a communications delay of 40ms
			DELAY_80 = 0x85,		// Set a communications delay of 80us
			READ_STATUS = 0xCA,		// Read the status byte status back from the UCB
		}

		public static void KeepAlive(HW hw)
		{
			SendBit(hw, 0);
		}

		public static byte SendCommand(HW hw, CMD cmd)
		{
			return SendCommand(hw, (byte)cmd);
		}

		public static byte SendCommand(HW hw, byte cmd)
		{
			for(int i=0; i<8; i++)
			{
#if(DEBUG_VERBOSE)
				Debug.WriteLine("Sending Bit "+(7-i));
#endif
				if(!SendBit(hw, (cmd >> (7-i)) & 1))
					return 0x55;
			}

			byte retval = 0xFF;

			if(cmd == (byte)CMD.READ_STATUS)
			{
				retval = 0;

				for(int i=0; i<8; i++)
				{
#if(DEBUG_VERBOSE)
					Debug.WriteLine("Receiving Bit "+(7-i));
#endif				
					byte b = ReceiveBit(hw);
					if(b > 1) return 0x55;
					retval = (byte)((retval << 1) + b);
				}
			}

			return retval;
		}

		public static void WriteReg(HW hw, byte reg, int val)
		{
			if(SendCommand(hw, (byte)(0xA0 + (0x0F&reg))) == 0x55) return;
			if(SendCommand(hw, (byte)(val >> 8)) == 0x55) return;
			if(SendCommand(hw, (byte)(val & 0x00FF)) == 0x55) return;;
		}

		/// <summary>
		/// Sends a single bit to the UCB
		/// </summary>
		/// <param name="hw">A hardware control object.</param>
		/// <param name="val">An integer representing a 0 or a 1.
		/// (Anything other than 0 is considered a 1)</param>
		private static bool SendBit(HW hw, int val)
		{
			int count = 0;

			// take X2-5 high
			hw.X2 |= 0x10;
	
			// wait for pin 12 to go high
			while((hw.StatusPort() & (byte)StatusPin.PIN_12) == 0)
			{
				if(count++ > TIME_OUT_COUNT)
				{
					MessageBox.Show("Error sending bit to UCB (1).",
						"UCB Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return false;
				}
				Thread.Sleep(1);				
			}

			// set X2-5 for data bit
			if(val == 0) hw.X2 &= 0xEF;
			count = 0;
			if((hw.StatusPort() & (byte)StatusPin.PIN_12) == 0)
			{
				MessageBox.Show("Error sending bit to UCB (2).",
					"UCB Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}

			// wait until pin 12 goes back low
			count = 0;
			while((hw.StatusPort() & (byte)StatusPin.PIN_12) != 0)
			{
				if(count++ > TIME_OUT_COUNT)
				{
					MessageBox.Show("Error sending bit to UCB (3).",
						"UCB Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return false;
				}
				Thread.Sleep(1);
			}
			//Thread.Sleep(new TimeSpan(send_bit_delay1));

			// set X2-5 back low
			hw.X2 &= 0xEF;

			Thread.Sleep(new TimeSpan(send_bit_delay2));
			return true;
		}

		private static byte ReceiveBit(HW hw)
		{
			int count = 0;

			// take X2-5 high
			hw.X2 |= 1<<4;

			// wait for pin 12 to go high
			while((hw.StatusPort() & (byte)StatusPin.PIN_12) == 0)
			{
				if(count++ > TIME_OUT_COUNT)
				{
					MessageBox.Show("Error receiving bit from UCB.",
						"UCB Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return 2;
				}
				Thread.Sleep(1);
			}

			// set X2-5 low
			hw.X2 &= 0xEF;
			Thread.Sleep(new TimeSpan(receive_bit_delay1));

			byte retval = 0;
			if((hw.StatusPort() & (byte)StatusPin.PIN_12) != 0)
				retval = 1;

			Thread.Sleep(receive_bit_delay2);
			return retval;
		}
	}
}
//=================================================================
// ad9854_v2.cs
//=================================================================
// Controls a AD9854 DDS using using the parallel interface 
// connected to either the FlexRadio SDR-1000 or the Eval board.
// Depends on PortTalk (parallel.cs) being available to talk to the
// parallel port.
//
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
    using System.Diagnostics;

	public class AD9854_v2
	{
		#region Enums

		public enum ControlMode
		{
			SDR = 0,
			EVAL,
		}

		private enum LPTPort
		{
			DATA = 0,
			STATUS = 1,
			CTRL = 2,
		}

		#endregion

		#region Variable Declarations

		private Register8[] registers;	// array of registers that represent the 29 8-bit registers on the AD9854
		private ushort lpt_addr = 0x378;	// parallel port address
		private ControlMode current_control_mode = ControlMode.SDR;
		private Register8WriteDel write_reg;

		#endregion

		#region Constructors

		public AD9854_v2()
		{
			byte[] default_vals = {   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x40, 0x00, 0x00, 0x00, 0x10, 0x64, 0x01, 
									  0x20, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00 };
			write_reg = new Register8WriteDel(WriteRegister);
			registers = new Register8[40];
			for(int i=0; i<40; i++)
				registers[i] = new Register8(i.ToString(), write_reg, (byte)i, default_vals[i]);
		}

		public AD9854_v2(ushort addr)
		{
			lpt_addr = addr;
			byte[] default_vals = {   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x40, 0x00, 0x00, 0x00, 0x10, 0x64, 0x01, 
									  0x20, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00 };
			write_reg = new Register8WriteDel(WriteRegister);
			registers = new Register8[40];
			for(int i=0; i<40; i++)
				registers[i] = new Register8(i.ToString(), write_reg, (byte)i, default_vals[i]);
		}

		public AD9854_v2(ControlMode mode)
		{
			byte[] default_vals = {   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x40, 0x00, 0x00, 0x00, 0x10, 0x64, 0x01, 
									  0x20, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00 };
			write_reg = new Register8WriteDel(WriteRegister);
			registers = new Register8[40];
			for(int i=0; i<40; i++)
				registers[i] = new Register8(i.ToString(), write_reg, (byte)i, default_vals[i]);
			current_control_mode = mode;
		}

		public AD9854_v2(ushort addr, ControlMode mode)
		{
			byte[] default_vals = {   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
									  0x00, 0x40, 0x00, 0x00, 0x00, 0x10, 0x64, 0x01, 
									  0x20, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00 };
			write_reg = new Register8WriteDel(WriteRegister);
			registers = new Register8[40];
			for(int i=0; i<40; i++)
				registers[i] = new Register8(i.ToString(), write_reg, (byte)i, default_vals[i]);
			lpt_addr = addr;
			current_control_mode = mode;
		}

		#endregion

		#region Properties

		private bool update = false;
		public bool Update
		{
			get { return update; }
			set
			{
				update = value;
				for(int i=0; i<40; i++)
					registers[i].UpdateHardware = value;
			}
		}

		private int delay = 0;	// Value that sets the delay in ms
		public int Delay
		{
			get { return delay; }
			set { delay = value; }
		}

		private long ftw1 = 0;
		public long FTW1
		{
			get { return ftw1; }
			set
			{
				ftw1 = value;		   //save new tuning word
				ComparatorPowerDown = false;
				for(int i=0; i<6; i++)
				{
					registers[4+i].SetData((byte)(ftw1 >> (40-i*8)));
				}
			}
		}

		private bool bypass_inverse_sinc = false;
		public bool BypassInverseSinc
		{
			get { return bypass_inverse_sinc; }
			set
			{
				bypass_inverse_sinc = value;
				if(value) registers[0x20].SetBit(6);
				else registers[0x20].ClearBit(6);
			}
		}

		// valid values are 1 - 20.  1 indicates disable pll.
		private byte pll_mult = 1;
		public byte PLLMult
		{
			get { return pll_mult; }
			set 
			{
				if(pll_mult < 1 || pll_mult > 20) return;
				pll_mult = value;
				if(pll_mult == 1)
					registers[0x1E].SetBit(6);
				else
					registers[0x1E].SetData((byte)((registers[0x1E].GetData() & 0x40) | value));
			}
		}

		private bool comparator_power_down = false;
		public bool ComparatorPowerDown
		{
			get { return comparator_power_down; }
			set 
			{
				comparator_power_down = value;
				if(value) registers[0x1D].SetBit(4);
				else registers[0x1D].ClearBit(4);
			}
		}

		private bool output_shape_key_enable = true;
		public bool OutputShapeKeyEnable
		{
			get { return output_shape_key_enable; }
			set
			{
				output_shape_key_enable = value;
				if(value) registers[0x20].SetBit(5);
				else registers[0x20].ClearBit(5);
			}
		}

		#endregion

		#region Private Routines

		private void LatchData()
		{
			byte b1 = 0xF, b2 = 0xB;
			switch(current_control_mode)
			{
				case ControlMode.SDR:
					b1 = 0xF; b2 = 0xB;
					break;
				case ControlMode.EVAL:
					b1 = 0xA; b2 = 0xB;
					break;
			}
			Parallel.outport((ushort)(lpt_addr+LPTPort.CTRL), b1);		// Drive bit high.
			Thread.Sleep(new TimeSpan(delay));
			Parallel.outport((ushort)(lpt_addr+LPTPort.CTRL), b2);		// Drive bit low.
			Thread.Sleep(new TimeSpan(delay));
		}
        
		private void LatchAddr()
		{
			byte b1 = 0x3, b2 = 0xB;
			switch(current_control_mode)
			{
				case ControlMode.SDR:
					b1 = 0x3; b2 = 0xB;
					break;
				case ControlMode.EVAL:
					b1 = 0x9; b2 = 0xB;
					break;
			}
			Parallel.outport((ushort)(lpt_addr+LPTPort.CTRL), b1);		// Drive bit high.
			Thread.Sleep(new TimeSpan(delay));
			Parallel.outport((ushort)(lpt_addr+LPTPort.CTRL), b2);		// Drive bit low.
			Thread.Sleep(new TimeSpan(delay));
		}

		private void LatchCtrl()
		{
			byte b1 = 0x3, b2 = 0xB;
			switch(current_control_mode)
			{
				case ControlMode.SDR:
					b1 = 0x3; b2 = 0xB;
					break;
				case ControlMode.EVAL:
					b1 = 0x3; b2 = 0xB;
					break;
			}
			Parallel.outport((ushort)(lpt_addr+LPTPort.CTRL), b1);		// Drive bit high.
			Thread.Sleep(new TimeSpan(delay));
			Parallel.outport((ushort)(lpt_addr+LPTPort.CTRL), b2);		// Drive bit low.
			Thread.Sleep(new TimeSpan(delay));
		}

		private void WriteRegister(byte data, object user)
		{
			byte register = (byte)user;

			byte b1 = data;
			byte b2 = (byte)(register | 0x40);
			byte b3 = register;
			byte b4 = (byte)0x40;
			switch(current_control_mode)
			{
				case ControlMode.SDR:
					b1 = data;
					b2 = (byte)(register | 0x40);	// DDSWRB high
					b3 = register;
					b4 = 0x40;						// DDSWRB low
					break;
				case ControlMode.EVAL:
					b1 = (byte)~data;
					b3 = (byte)~register;
					b3 = 0xED;
					b4 = 0xEC;
					break;
			}
			// Set up data bits.
			Parallel.outport((ushort)(lpt_addr+LPTPort.DATA), b1);
			LatchData();

			// Set up addr bits.
			Parallel.outport((ushort)(lpt_addr+LPTPort.DATA), b2);
			LatchAddr();

			// WRBAR,RESET,UDCLK = lo
			// RDBAR,PMODE = hi
			Parallel.outport((ushort)(lpt_addr+LPTPort.DATA), b3);
			LatchCtrl();

			// RESET,UDCLK = lo
			// WRBAR,RDBAR,PMODE = hi
			Parallel.outport((ushort)(lpt_addr+LPTPort.DATA), b4);
			LatchCtrl();
		}

		#endregion

		#region Public Routines

		public void Reset()
		{
			registers[0x1D].SetData(0x10);
		}

		public void ForceUpdate()
		{
			for(int i=0; i<40; i++)
				registers[i].ForceUpdate();
		}

		#endregion
	}
}
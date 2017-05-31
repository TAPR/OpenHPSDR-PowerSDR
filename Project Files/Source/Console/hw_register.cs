//=================================================================
// hw_register.cs
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

using System.Diagnostics;

namespace PowerSDR
{
	/// <summary>
	/// A delegate that ties the software register to the hardware.  This is
	/// the mechanism the software uses to talk to the hardware.
	/// </summary>
	public delegate void Register8WriteDel(byte data, object user_data);

	/// <summary>
	/// Represents an 8 bit hardware register
	/// </summary>
	public class Register8
	{
		#region Private Variables

		private string name;
		private byte hardware_data;				// always matches hardware data
		private byte software_data;				// possibly new data not yet sent to hardware
		private bool update_hardware = false;	// only calls the HardwareWrite if this is true
		private object user_data;				// used to pass specific data (config, etc)
		private Register8WriteDel HardwareWrite;// function pointer for hardware writing

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of an 8-bit register.
		/// </summary>
		/// <param name="n">Identifier for debugging</param>
		/// <param name="del">Delegate for talking to hardware</param>
		/// <param name="user">Pointer to specific data such as config</param>
		public Register8(string n, Register8WriteDel del, object user)
		{
			name = n;
			HardwareWrite = del;
			user_data = user;
		}

		/// <summary>
		/// Creates an instance of an 8-bit register.
		/// </summary>
		/// <param name="n">Identifier for debugging</param>
		/// <param name="del">Delegate for talking to hardware</param>
		/// <param name="user">Pointer to specific data such as config</param>
		/// <param name="default_val">Default value for the register</param>
		public Register8(string n, Register8WriteDel del, object user, byte default_val)
		{
			name = n;
			HardwareWrite = del;
			user_data = user;
			hardware_data = software_data = default_val;
		}

		#endregion

		#region Public Functions

		/// <summary>
		/// Gets the latest data from the register.
		/// </summary>
		/// <returns>latest 8 bit value</returns>
		public byte GetData()
		{
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" GetData()  software_data = "+software_data);
#endif
			return software_data;
		}

		/// <summary>
		/// Gets the value of the bit requested from the latest data.
		/// </summary>
		/// <param name="bit">The 0-indexed (least significant) bit to return.
		/// Valid from [0, 7]</param>
		/// <returns>True if the bit is high, False if the bit is low.</returns>
		public bool GetBit(int bit)
		{
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" GetBit("+bit+") = "+(((software_data >> bit) & 1) == 1));
#endif
			// shift the latest data right and then chop off all but
			// the last bit and compare with one.
			return ((software_data >> bit) & 1) == 1;
		}

		/// <summary>
		/// Sets the data to a new value.  Writes the data to hardware if needed.
		/// </summary>
		/// <param name="data">8-bit value to write.</param>
		public void SetData(byte data)
		{			
			software_data = data;	// set software copy of register to new data
			if(update_hardware)		
				Update();			// update the hardware
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" SetData("+data+")  software_data: "+software_data);
#endif
		}

		/// <summary>
		/// Sets the value of the selected bit.
		/// </summary>
		/// <param name="bit">The 0-indexed (least significant) bit to set</param>
		public void SetBit(int bit)
		{
			software_data |= (byte)(1 << bit);	// set the bit
			if(update_hardware)
				Update();						// update hardware
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" SetBit("+bit+")  software_data: "+software_data);			
#endif
		}

		/// <summary>
		/// Clears the value of the selected bit.
		/// </summary>
		/// <param name="bit">The 0-indexed (least significant) bit to clear</param>
		public void ClearBit(int bit)
		{
			software_data &= (byte)(~(1 << bit));	// clear the bit
			if(update_hardware)
				Update();							// update hardware
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" ClearBit("+bit+")  software_data: "+software_data);			
#endif
		}

		/// <summary>
		/// Property that determines whether to keep hardware current with
		/// software copy of the register.
		/// </summary>
		public bool UpdateHardware
		{
			get { return update_hardware; }
			set
			{
				update_hardware = value;
				if(update_hardware)
					Update();	// update hardware
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" UpdateHardware = "+update_hardware);
#endif
			}
		}

		/// <summary>
		/// Update the hardware even if the property above is false.
		/// </summary>
		public void ForceUpdate()
		{
			HardwareWrite(software_data, user_data);	// update hardware
			hardware_data = software_data;				// update soft copy of hardware register
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" ForceUpdate:  hardware_data: "+hardware_data);			
#endif
		}		

		#endregion

		#region Private Functions

		/// <summary>
		/// Brings the hardware register in line with the software register.
		/// </summary>
		private void Update()
		{
			if(hardware_data != software_data)	 // if data has changed
			{
				HardwareWrite(software_data, user_data); // update hardware
				hardware_data = software_data;			// update soft copy of hardware register
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" Update:  hardware_data: "+hardware_data);				
#endif
			}
		}

		#endregion
	}

	/// <summary>
	/// A delegate that ties the software register to the hardware.  This is
	/// the mechanism the software uses to talk to the hardware.
	/// </summary>
	public delegate void Register16WriteDel(ushort data, object user_data);

	/// <summary>
	/// Represents an 16 bit hardware register
	/// </summary>
	public class Register16
	{
		#region Private Variables

		private string name;
		private ushort hardware_data;			// always matches hardware data
		private ushort software_data;			// possibly new data not yet sent to hardware
		private bool update_hardware = false;	// only calls the HardwareWrite if this is true
		private object user_data;				// used to pass specific data (config, etc)
		private Register16WriteDel HardwareWrite;// function pointer for hardware writing

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of an 16-bit register.
		/// </summary>
		/// <param name="n">Identifier for debugging</param>
		/// <param name="del">Delegate for talking to hardware</param>
		/// <param name="user">Pointer to specific data such as config</param>
		public Register16(string n, Register16WriteDel del, object user)
		{
			name = n;
			HardwareWrite = del;
			user_data = user;
		}

		/// <summary>
		/// Creates an instance of an 16-bit register.
		/// </summary>
		/// <param name="n">Identifier for debugging</param>
		/// <param name="del">Delegate for talking to hardware</param>
		/// <param name="user">Pointer to specific data such as config</param>
		/// <param name="default_val">Default value for the register</param>
		public Register16(string n, Register16WriteDel del, object user, ushort default_val)
		{
			name = n;
			HardwareWrite = del;
			user_data = user;
			hardware_data = software_data = default_val;
		}

		#endregion

		#region Public Functions

		/// <summary>
		/// Gets the latest data from the register.
		/// </summary>
		/// <returns>latest 16 bit value</returns>
		public ushort GetData()
		{
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" GetData()  software_data = "+software_data);
#endif
			return software_data;
		}

		/// <summary>
		/// Gets the value of the bit requested from the latest data.
		/// </summary>
		/// <param name="bit">The 0-indexed (least significant) bit to return.
		/// Valid from [0, 15]</param>
		/// <returns>True if the bit is high, False if the bit is low.</returns>
		public bool GetBit(int bit)
		{
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" GetBit("+bit+") = "+(((software_data >> bit) & 1) == 1));
#endif
			// shift the latest data right and then chop off all but
			// the last bit and compare with one.
			return ((software_data >> bit) & 1) == 1;
		}

		/// <summary>
		/// Sets the data to a new value.  Writes the data to hardware if needed.
		/// </summary>
		/// <param name="data">16-bit value to write.</param>
		public void SetData(ushort data)
		{			
			software_data = data;	// set software copy of register to new data
			if(update_hardware)		
				Update();			// update the hardware
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" SetData("+data+")  software_data: "+software_data);
#endif
		}

		/// <summary>
		/// Sets the value of the selected bit.
		/// </summary>
		/// <param name="bit">The 0-indexed (least significant) bit to set.
		/// Valid from [0, 15]</param>
		public void SetBit(int bit)
		{
			software_data |= (ushort)(1 << bit);	// set the bit
			if(update_hardware)
				Update();						// update hardware
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" SetBit("+bit+")  software_data: "+software_data);			
#endif
		}

		/// <summary>
		/// Clears the value of the selected bit.
		/// </summary>
		/// <param name="bit">The 0-indexed (least significant) bit to clear.
		/// Valid from [0, 15]</param>
		public void ClearBit(int bit)
		{
			software_data &= (ushort)(~(1 << bit));	// clear the bit
			if(update_hardware)
				Update();							// update hardware
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" ClearBit("+bit+")  software_data: "+software_data);			
#endif
		}

		/// <summary>
		/// Property that determines whether to keep hardware current with
		/// software copy of the register.
		/// </summary>
		public bool UpdateHardware
		{
			get { return update_hardware; }
			set
			{
				update_hardware = value;
				if(update_hardware)
					Update();	// update hardware
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" UpdateHardware = "+update_hardware);
#endif
			}
		}

		/// <summary>
		/// Update the hardware even if the property above is false.
		/// </summary>
		public void ForceUpdate()
		{
			HardwareWrite(software_data, user_data);	// update hardware
			hardware_data = software_data;				// update soft copy of hardware register
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" ForceUpdate:  hardware_data: "+hardware_data);			
#endif
		}		

		#endregion

		#region Private Functions

		/// <summary>
		/// Brings the hardware register in line with the software register.
		/// </summary>
		private void Update()
		{
			if(hardware_data != software_data)	 // if data has changed
			{
				HardwareWrite(software_data, user_data); // update hardware
				hardware_data = software_data;			// update soft copy of hardware register
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" Update:  hardware_data: "+hardware_data);				
#endif
			}
		}

		#endregion
	}
	public delegate void Register24WriteDel(int data, object user_data);

	/// <summary>
	/// Represents an 24 bit hardware register
	/// </summary>
	public class Register24
	{
		#region Private Variables

		private string name;
		private int hardware_data;		// always matches hardware data
		private int software_data;		// possibly new data not yet sent to hardware
		private bool update_hardware = false;
		private object user_data;
		private Register24WriteDel HardwareWrite;	// function pointer for hardware writing
	
		#endregion

		#region Constructor

		public Register24(string n, Register24WriteDel del, object user)
		{
			name = n;
			HardwareWrite = del;
			user_data = user;			
		}

		public Register24(string n, Register24WriteDel del, object user, int default_val)
		{
			name = n;
			HardwareWrite = del;
			user_data = user;
			hardware_data = software_data = default_val;
		}

		#endregion

		#region Public Functions

		public int GetData()
		{
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" GetData()  software_data = "+software_data);
#endif
			return software_data;
		}

		public bool GetBit(int bit)
		{
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" GetBit("+bit+") = "+(((software_data >> bit) & 1) == 1));
#endif
			return ((software_data >> bit) & 1) == 1;
		}

		public void SetData(int data)
		{
			software_data = data;
			if(update_hardware)
				Update();
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" SetData("+data+")  software_data: "+software_data);
#endif
		}

		public void SetBit(int bit)
		{
			software_data |= (1 << bit);
			if(update_hardware)
				Update();
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" SetBit("+bit+")  software_data: "+software_data);
#endif
		}

		public void ClearBit(int bit)
		{
			software_data &= ~(1 << bit);
			if(update_hardware)
				Update();
#if(DEBUG_VERBOSE)
			Debug.WriteLine(name+" ClearBit("+bit+")  software_data: "+software_data);
#endif
		}

		public bool UpdateHardware
		{
			get { return update_hardware; }
			set
			{
				update_hardware = value;
				if(update_hardware)
					Update();
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" UpdateHardware = "+update_hardware);
#endif
			}
		}

		public void ForceUpdate()
		{
			HardwareWrite(software_data, user_data);
			hardware_data = software_data;
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" ForceUpdate:  hardware_data: "+hardware_data);
#endif
		}

		#endregion

		#region Private Functions

		private void Update()
		{
			if(hardware_data != software_data)	 // write register to hardware
			{
				HardwareWrite(software_data, user_data);
				hardware_data = software_data;
#if(DEBUG_VERBOSE)
				Debug.WriteLine(name+" Update:  hardware_data: "+hardware_data);
#endif
			}
		}

		#endregion
	}
}
//=================================================================
// fwc_midi.cs
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
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
//using System.Windows.Forms;

namespace PowerSDR
{
	public class FWCMidi
	{
		#region Enums

		private enum Command
		{
			NoteOn = 0x90,
			NoteOff = 0x80,
			Aftertouch0 = 0xA0,
			Aftertouch1 = 0xA1,
            Controller = 0xB0,			
		}

		private enum Controller
		{
			HoldPedal = 64,
			Hold2Pedal = 69,
		}

		private enum Note
		{
			Mox = 0,
			PAADC = 1,
			Dot = 61,
			Dash = 63,
			MicDown = 66,
			MicUp = 68,
			MicFast = 70,
		}

		#endregion

		#region Variable Declaration

		private static Midi.MidiInCallback callback;
		private static bool resetting = false;
		private static int midi_in_handle;
		private static int midi_out_handle;
		private static object in_lock_obj = new Object();
		private static object out_lock_obj = new Object();

		#endregion

		#region High Level Midi Control Functions

		public static bool Present()
		{
			int num_in = Midi.MidiInGetNumDevs();
			for(int i=0; i<num_in; i++)
			{
				
				string s = Midi.MidiInGetName(i);
				//if(s == "FLEX 5000" || s == "FLEX-5000 Midi" || s == "FlexRadio Flex-5000" || s == "FlexRadio Flex-5000 MIDI")
				if(s.IndexOf("FLEX") >= 0 || s.IndexOf("Flex") >= 0)
				{
					goto test_out;
				}
			}

			return false;

			test_out:
				int num_out = Midi.MidiOutGetNumDevs();
			for(int i=0; i<num_out; i++)
			{
				string s = Midi.MidiOutGetName(i);
				//if(s == "FLEX 5000" || s == "2- FLEX-5000 Midi" || s == "FlexRadio Flex-5000" || s == "2- FlexRadio Flex-5000 MIDI")
				if(s.IndexOf("FLEX") >= 0 || s.IndexOf("Flex") >= 0)
				{
					return true;
				}
			}
			return false;
		}

		public static bool Open()
		{
			FillTables();
			if(!OpenMidiIn()) return false;
			if(!OpenMidiOut()) return false;
			return true;
		}

		private static bool OpenMidiIn()
		{
			int num_in = Midi.MidiInGetNumDevs();
			int in_index = -1;
			for(int i=0; i<num_in; i++)
			{
				
				string s = Midi.MidiInGetName(i);
				//if(s == "FLEX 5000" || s == "FLEX-5000 Midi" || s == "FlexRadio Flex-5000" || s == "FlexRadio Flex-5000 MIDI")
				if(s.IndexOf("FLEX") >= 0 || s.IndexOf("Flex") >= 0)
				{
					Debug.WriteLine("Midi In Device Name: "+s);
					in_index = i;
					break;
				}
			}

			if(in_index < 0) 
			{
				//MessageBox.Show("Error opening Midi In device");
				return false;
			}

			callback = new Midi.MidiInCallback(InCallback);
			int result = Midi.MidiInOpen(ref midi_in_handle, in_index, callback, 0, Midi.CALLBACK_FUNCTION);
			if(result != 0)
			{
				StringBuilder error_text = new StringBuilder(64);
				Midi.MidiInGetErrorText(result, error_text, 64);
				Debug.WriteLine("MidiInOpen Error: "+error_text);
				//MessageBox.Show("Error opening Midi In device");
				return false;
			}

			for(int i=0; i<3; i++)
			{
				result = Midi.AddSysExBuffer(midi_in_handle);
				if(result != 0)
				{
					StringBuilder error_text = new StringBuilder(64);
					Midi.MidiInGetErrorText(result, error_text, 64);
					Debug.WriteLine("AddSysExBuffer Error: "+error_text);
					//MessageBox.Show("Error adding Midi In device SysEx Buffer");
					return false;
				}
			}

			result = Midi.MidiInStart(midi_in_handle);
			if(result != 0)
			{
				StringBuilder error_text = new StringBuilder(64);
				Midi.MidiInGetErrorText(result, error_text, 64);
				Debug.WriteLine("MidiInStart Error: "+error_text);
				//MessageBox.Show("Error starting Midi In device");
				return false;
			}

			return true;
		}

		private static bool OpenMidiOut()
		{
			int num_out = Midi.MidiOutGetNumDevs();
			int out_index = -1;
			for(int i=0; i<num_out; i++)
			{
				string s = Midi.MidiOutGetName(i);
				//if(s == "FLEX 5000" || s == "2- FLEX-5000 Midi" || s == "FlexRadio Flex-5000" || s == "2- FlexRadio Flex-5000 MIDI")
				if(s.IndexOf("FLEX") >= 0 || s.IndexOf("Flex") >= 0)
				{
					Debug.WriteLine("Midi Out Device Name: "+s);
					out_index = i;
					break;
				}
			}

			if(out_index < 0) 
			{
				//MessageBox.Show("Error finding Midi Out device");
				return false;
			}

			int result = Midi.MidiOutOpen(ref midi_out_handle, out_index, IntPtr.Zero, 0, 0);
			if(result != 0)
			{
				StringBuilder error_text = new StringBuilder(64);
				Midi.MidiInGetErrorText(result, error_text, 64);
				Debug.WriteLine("MidiOutOpen Error: "+error_text);
				//MessageBox.Show("Error Opening Midi Out device");
				return false;
			}
			return true;
		}

		public static void Close()
		{
			CloseMidiIn();
			CloseMidiOut();
		}

		private static void CloseMidiIn()
		{
			if(midi_in_handle != 0)
			{
				Midi.MidiInStop(midi_in_handle);
				resetting = true;
				Midi.MidiInReset(midi_in_handle);
				Midi.MidiInClose(midi_in_handle);
				midi_in_handle = 0;
				resetting = false;
			}
		}

		private static void CloseMidiOut()
		{
			if(midi_out_handle != 0)
			{
				Midi.MidiOutClose(midi_out_handle);
			}
		}

		public static void SetMOX(bool b)
		{
			int data = 0;
			data = (b ? (int)Command.NoteOn : (int)Command.NoteOff);
			data += ((byte)Note.Mox)<<8;
			data += (127<<16);
			lock(out_lock_obj)
				if(Midi.MidiOutShortMessage(midi_out_handle, data) != 0)
					Debug.WriteLine("Error in SendShortMessage");
		}

		public static int ReadADC(int chan)
		{
			int data = (int)Command.NoteOn;
			data += ((byte)Note.PAADC)<<8;
			data += ((byte)chan)<<16;
			lock(out_lock_obj)
				if(Midi.MidiOutShortMessage(midi_out_handle, data) != 0)
					Debug.WriteLine("Error in SendShortMessage");

			int counter = 0;
			int id = chan;
			while(counter++ < 20)
			{
				if(midi_in_table.ContainsKey(id))
				{
					int val = (int)midi_in_table[id];
					midi_in_table.Remove(id);
					return val;
				}
				Thread.Sleep(50);
			}

			Debug.WriteLine("Timeout waiting on return Midi message.");
			return 0;
		}

		// F0 <3 mfc> <2 msgID> <2 protocolID> <4 opcode> <4 data1> <4 data2> F7
		private static ushort msgID = 0;
		public static void SendSetMessage(FWC.Opcode opcode, uint data1, uint data2)
		{
			ushort id = msgID++;
			byte[] msg = new byte[37];
			msg[0] = 0xF0; // start byte
			msg[1] = 0x00; // mfc highest byte
			msg[2] = 0x00; // mfc high byte
			msg[3] = 0x41;	// mfc low byte
			
			byte[] guts = new byte[16];
			byte[] temp = new byte[32];

			byte[] temp2 = BitConverter.GetBytes(id);
			temp2.CopyTo(guts, 0);

			temp2 = BitConverter.GetBytes((uint)opcode);
			temp2.CopyTo(guts, 4);

			temp2 = BitConverter.GetBytes(data1);
			temp2.CopyTo(guts, 8);

			temp2 = BitConverter.GetBytes(data2);
			temp2.CopyTo(guts, 12);

			EncodeBytes(temp, guts);

			temp.CopyTo(msg, 4);
			//DebugByte(guts);
			
			/*DecodeBytes(guts, temp);
			DebugByte(guts);*/

			msg[36] = 0xF7;
			//DebugByte(msg);
			
			lock(out_lock_obj)
				if(Midi.SendLongMessage(midi_out_handle, msg) != 0)
					Debug.WriteLine("Error in SendLongMessage");
		}

		public static void SendSetMessage(FWC.Opcode opcode, float data1, uint data2)
		{
			ushort id = msgID++;
			byte[] msg = new byte[37];
			msg[0] = 0xF0; // start byte
			msg[1] = 0x00; // mfc highest byte
			msg[2] = 0x00; // mfc high byte
			msg[3] = 0x41;	// mfc low byte
			
			byte[] guts = new byte[16];
			byte[] temp = new byte[32];

			byte[] temp2 = BitConverter.GetBytes(id);
			temp2.CopyTo(guts, 0);

			temp2 = BitConverter.GetBytes((uint)opcode);
			temp2.CopyTo(guts, 4);

			temp2 = BitConverter.GetBytes(data1);
			temp2.CopyTo(guts, 8);

			temp2 = BitConverter.GetBytes(data2);
			temp2.CopyTo(guts, 12);

			EncodeBytes(temp, guts);

			temp.CopyTo(msg, 4);
			//DebugByte(guts);
			
			/*DecodeBytes(guts, temp);
			DebugByte(guts);*/

			msg[36] = 0xF7;
			//DebugByte(msg);
			
			lock(out_lock_obj)
				if(Midi.SendLongMessage(midi_out_handle, msg) != 0)
					Debug.WriteLine("Error in SendLongMessage");
		}

		public static void SendSetMessage(FWC.Opcode opcode, uint data1, float data2)
		{
			ushort id = msgID++;
			byte[] msg = new byte[37];
			msg[0] = 0xF0; // start byte
			msg[1] = 0x00; // mfc highest byte
			msg[2] = 0x00; // mfc high byte
			msg[3] = 0x41;	// mfc low byte
			
			byte[] guts = new byte[16];
			byte[] temp = new byte[32];

			byte[] temp2 = BitConverter.GetBytes(id);
			temp2.CopyTo(guts, 0);

			temp2 = BitConverter.GetBytes((uint)opcode);
			temp2.CopyTo(guts, 4);

			temp2 = BitConverter.GetBytes(data1);
			temp2.CopyTo(guts, 8);

			temp2 = BitConverter.GetBytes(data2);
			temp2.CopyTo(guts, 12);

			EncodeBytes(temp, guts);

			temp.CopyTo(msg, 4);
			//DebugByte(guts);
			
			/*DecodeBytes(guts, temp);
			DebugByte(guts);*/

			msg[36] = 0xF7;
			//DebugByte(msg);
			
			lock(out_lock_obj)
				if(Midi.SendLongMessage(midi_out_handle, msg) != 0)
					Debug.WriteLine("Error in SendLongMessage");
		}

		public static uint SendGetMessage(FWC.Opcode opcode, uint data1, uint data2)
		{
			ushort id = msgID++;
			byte[] msg = new byte[37];
			msg[0] = 0xF0; // start byte
			msg[1] = 0x00; // mfc highest byte
			msg[2] = 0x00; // mfc high byte
			msg[3] = 0x41;	// mfc low byte
			
			byte[] guts = new byte[16];
			byte[] temp = new byte[32];

			byte[] temp2 = BitConverter.GetBytes(id);
			temp2.CopyTo(guts, 0);

			temp2 = BitConverter.GetBytes((uint)opcode);
			temp2.CopyTo(guts, 4);

			temp2 = BitConverter.GetBytes(data1);
			temp2.CopyTo(guts, 8);

			temp2 = BitConverter.GetBytes(data2);
			temp2.CopyTo(guts, 12);

			EncodeBytes(temp, guts);

			temp.CopyTo(msg, 4);
			//DebugByte(guts);
			
			/*DecodeBytes(guts, temp);
			DebugByte(guts);*/

			msg[36] = 0xF7;
			//DebugByte(msg);
			
			lock(out_lock_obj)
				if(Midi.SendLongMessage(midi_out_handle, msg) != 0)
					Debug.WriteLine("Error in SendLongMessage");
			
			int counter = 0;
			while(counter++ < 100)
			{
				if(midi_in_table.ContainsKey(id))
				{
					byte[] buf = (byte[])midi_in_table[id];
					midi_in_table.Remove(id);
					return BitConverter.ToUInt32(buf, 2);
				}
				Thread.Sleep(10);
			}

			Debug.WriteLine("Timeout waiting on return Midi message.");
			/*Midi.MidiInStop(midi_in_handle);
			Midi.MidiInReset(midi_in_handle);
			Midi.AddSysExBuffer(midi_in_handle);
			Midi.MidiInStart(midi_in_handle);*/
			return 0;
		}

		public static float SendGetMessageFloat(FWC.Opcode opcode, uint data1, uint data2)
		{
			ushort id = msgID++;
			byte[] msg = new byte[37];
			msg[0] = 0xF0; // start byte
			msg[1] = 0x00; // mfc highest byte
			msg[2] = 0x00; // mfc high byte
			msg[3] = 0x41;	// mfc low byte
			
			byte[] guts = new byte[16];
			byte[] temp = new byte[32];

			byte[] temp2 = BitConverter.GetBytes(id);
			temp2.CopyTo(guts, 0);

			temp2 = BitConverter.GetBytes((uint)opcode);
			temp2.CopyTo(guts, 4);

			temp2 = BitConverter.GetBytes(data1);
			temp2.CopyTo(guts, 8);

			temp2 = BitConverter.GetBytes(data2);
			temp2.CopyTo(guts, 12);

			EncodeBytes(temp, guts);

			temp.CopyTo(msg, 4);
			//DebugByte(guts);
			
			/*DecodeBytes(guts, temp);
			DebugByte(guts);*/

			msg[36] = 0xF7;
			//DebugByte(msg);
			
			lock(out_lock_obj)
				if(Midi.SendLongMessage(midi_out_handle, msg) != 0)
					Debug.WriteLine("Error in SendLongMessage");
			
			int counter = 0;
			while(counter++ < 100)
			{
				if(midi_in_table.ContainsKey(id))
				{
					byte[] buf = (byte[])midi_in_table[id];
					midi_in_table.Remove(id);
					return BitConverter.ToSingle(buf, 2);
				}
				Thread.Sleep(10);
			}

			Debug.WriteLine("Timeout waiting on return Midi message.");
			/*Midi.MidiInStop(midi_in_handle);
			Midi.MidiInReset(midi_in_handle);
			Midi.AddSysExBuffer(midi_in_handle);
			Midi.MidiInStart(midi_in_handle);*/
			return 0;
		}

		private static byte[][] encode_table;
		private static byte[][] decode_table;

		private static void FillTables()
		{
			encode_table = new byte[256][];
			for(int i=0; i<256; i++)
				encode_table[i] = new byte[2];

			for(int i=0; i<256; i++)
			{
				byte high_nibble = (byte)(i>>4); // set 0 byte for high nibble
				if(high_nibble < 0xA) high_nibble += 48; // offset for ascii '0'
				else high_nibble += 55;	// offset for ascii 'A'
				encode_table[i][0] = high_nibble;

				byte low_nibble = (byte)(i&0xF);
				if(low_nibble < 0xA) low_nibble += 48; // offset for ascii '0'
				else low_nibble += 55; // offset for ascii 'A'
				encode_table[i][1] = low_nibble;			
			}


			decode_table = new byte[0x80][];
			for(int i=0; i<0x80; i++)
				decode_table[i] = new byte[0x80];

			for(int i=0; i<128; i++)
			{
				for(int j=0; j<128; j++)
				{
					if(((i >= 48 && i <= 57) || (i >= 65 && i <= 70)) &&
						((j >= 48 && j <= 57) || (j >= 65 && j <= 70)))
					{
						byte high_nibble;
						if(i < 58) high_nibble = (byte)((i-48)<<4);
						else high_nibble = (byte)((i-55)<<4);
						byte low_nibble;
						if(j < 58) low_nibble = (byte)(j-48);
						else low_nibble = (byte)(j-55);
						decode_table[i][j] = (byte)(high_nibble + low_nibble);
					}
				}
			}
		}

		private static int EncodeBytes(byte[] outb, byte[] inb)
		{
			int j=0;
			for(int i=0; i < inb.Length; i++)
			{
				outb[j++] = encode_table[inb[i]][0]; 
				outb[j++] = encode_table[inb[i]][1];
			}
			return j;
		}

		private static int DecodeBytes(byte[] outb, byte[] inb)
		{
			int j=0;
			for(int i=0; i < inb.Length; i+=2)
			{
				outb[j++] = decode_table[inb[i]][inb[i + 1]];
				//Debug.WriteLine("decode["+inb[i].ToString("X")+"]["+inb[i+1].ToString("X")+"] = "+decode_table[inb[i]][inb[i+1]].ToString("X"));
			}
			return j;
		}

		/*public static byte[] PackBytes(byte[] b1) // leave highest order bit low for MIDI msg
		{
			byte[] b2 = new byte[(int)Math.Ceiling(b1.Length*8/7.0)];
			for(int i=b2.Length-1; i>=0; i--)
			{
				int index1 = (b1.Length-1)-(int)Math.Floor(((b2.Length-1)-i)*7/8.0);
				int index2 = (b1.Length-1)-(int)Math.Ceiling(((b2.Length-1)-i)*7/8.0);
				int shift = (b2.Length-1-i)%8;

				if(shift == 0)
					b2[i] = (byte)(b1[index1]&0x7F);
				else
				{
					byte temp1 = (byte)(b1[index1]>>(8-shift));
					byte temp2;
					if(index2 < 0) temp2 = 0;
					else temp2 = (byte)(b1[index2]<<shift);
					b2[i] = (byte)((temp1 | temp2) & 0x7F);
				}
			}
			DebugByte(b1);
			DebugByte(b2);

			return b2;
		}

		public static byte[] UnpackBytes(byte[] b1)
		{
			byte[] b2 = new byte[(int)Math.Floor(b1.Length*7/8.0)];
			for(int i=b2.Length-1; i>=0; i--)
			{
				int index = (b1.Length-1)-(int)Math.Floor(((b2.Length-1)-i)*8/7.0);
				int shift = (b2.Length-1-i)%7;

				byte temp1 = (byte)(b1[index]>>shift);
				byte temp2 = (byte)(b1[index-1]<<(7-shift));
				b2[i] = (byte)(temp1 | temp2);
			}
			DebugByte(b1);
			DebugByte(b2);
			return b2;
		}*/

		public static void DebugByte(byte[] b)
		{
			for(int i=0; i<b.Length; i++)
				Debug.Write(b[i].ToString("X")+" ");
			Debug.WriteLine("");
		}

		#endregion

		#region Midi In Callback
		
		public static Console console = null;
		private static Hashtable midi_in_table = new Hashtable(10);
		private static int InCallback(int hMidiIn, int wMsg, int dwInstance, int dwParam1, int dwParam2)
		{
			lock(in_lock_obj)
			{
				switch(wMsg)
				{
					case Midi.MIM_DATA:
						Command cmd = (Command)((byte)dwParam1);
						byte byte1 = (byte)(dwParam1>>8);
						byte byte2 = (byte)(dwParam1>>16);

						switch(cmd)
						{
							case Command.NoteOn:
								switch((Note)byte1)
								{
									case Note.Dot:
										//console.Keyer.FWCDot = true;
										//FWC.SetMOX(true);
										break;
									case Note.Dash:
										//console.Keyer.FWCDash = true;
										//FWC.SetMOX(true);
										break;
									case Note.MicDown:
										console.MicDown = true;
										break;
									case Note.MicUp:
										console.MicUp = true;
										break;
									case Note.MicFast:
										console.MicFast = !console.MicFast;
										break;
								}
								break;
							case Command.NoteOff:
								switch((Note)byte1)
								{
									case Note.Dot:
										//console.Keyer.FWCDot = false;							
										//FWC.SetMOX(false);
										break;
									case Note.Dash:
										//console.Keyer.FWCDash = false;
										//FWC.SetMOX(false);
										break;
									case Note.MicDown:
										console.MicDown = false;
										break;
									case Note.MicUp:
										console.MicUp = false;
										break;
									case Note.MicFast:
										break;
								}
								break;
							case Command.Controller:
								switch((Controller)byte1)
								{
									case Controller.HoldPedal:
										console.FWCMicPTT = (byte2 > 63);
										break;
									case Controller.Hold2Pedal:
										console.FWCRCAPTT = (byte2 > 63);
										break;
								}
								break;
							case Command.Aftertouch0:
							case Command.Aftertouch1:
								int id = (ushort)((((byte)cmd-(byte)Command.Aftertouch0)<<2)+(byte1>>5));
								int data = (int)(((byte1&0x1F)<<7)+byte2);
								if(midi_in_table.ContainsKey(id))
									midi_in_table.Remove(id);
								midi_in_table.Add(id, data);
								break;
						}							
						break;
					case Midi.MIM_LONGDATA:
						if(!resetting && midi_in_handle != 0) // in case device closes, don't send anymore buffers
						{
							int result = Midi.AddSysExBuffer(midi_in_handle);
							if(result != 0) 
							{
								StringBuilder error_text = new StringBuilder(64);
								Midi.MidiInGetErrorText(result, error_text, 64);
								Debug.WriteLine("AddSysExBuffer Error: "+error_text);
							}
						}

						IntPtr headerPtr = new IntPtr(dwParam1);
						Midi.MidiHeader header = (Midi.MidiHeader)Marshal.PtrToStructure(headerPtr, typeof(Midi.MidiHeader));
						byte[] temp = new byte[header.bytesRecorded];
						for(int i=0; i<header.bytesRecorded; i++)
							temp[i] = Marshal.ReadByte(header.data, i);

						if(temp.Length > 5)
						{
							byte[] temp2 = new byte[temp.Length-5];
							for(int i=0; i<temp.Length-5; i++)
								temp2[i] = temp[i+4];
					
							byte[] buf = new byte[temp2.Length/2];
							DecodeBytes(buf, temp2);
							if(midi_in_table.ContainsKey(BitConverter.ToUInt16(buf, 0)))
								midi_in_table.Remove(BitConverter.ToUInt16(buf, 0));
							midi_in_table.Add(BitConverter.ToUInt16(buf, 0), buf);
						}

						/*for(int i=0; i<header.bytesRecorded; i++)
							Debug.Write(buf[i].ToString("X")+" ");
						Debug.WriteLine("");*/

						if(midi_in_handle != 0)
							Midi.ReleaseBuffer(midi_in_handle, headerPtr);
						break;
				}
			}
			
			return 0;
		}

		#endregion
	}
}
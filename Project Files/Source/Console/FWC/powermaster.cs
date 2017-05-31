//=================================================================
// powermaster.cs
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
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace PowerSDR
{
	public class PowerMaster
	{
		private SerialPort com_port;

		public PowerMaster(string s)
		{
			if(s.StartsWith("COM"))
				s = s.Substring(3);
			int port = int.Parse(s);
			InitComPort(port);
		}

		public PowerMaster(int port)
		{
			InitComPort(port);
		}

		public void Close()
		{
			try
			{
				if(com_port != null && com_port.IsOpen)
					com_port.Close();
			}
			catch(Exception) { }
			com_port = null;
		}

		private void InitComPort(int port)
		{
			com_port = new SerialPort();
			com_port.Encoding = System.Text.Encoding.ASCII;
			com_port.RtsEnable = true; // kd5tfd hack for soft rock ptt 
			com_port.DtrEnable = false; // set dtr off 
			com_port.ErrorReceived += new SerialErrorReceivedEventHandler(SerialPort_ErrorReceived);
			com_port.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
			com_port.PinChanged += new SerialPinChangedEventHandler(SerialPort_PinChanged);
			com_port.PortName = "COM" + port.ToString(); 
			com_port.Parity = Parity.None; 
			com_port.StopBits = StopBits.One;
			com_port.DataBits = 8; 
			com_port.BaudRate = 38400; 
			com_port.ReadTimeout = 5000;
			com_port.WriteTimeout = 500;
			com_port.ReceivedBytesThreshold = 1;
			com_port.Open();

			out_buffer = new byte[7];
			out_buffer[0] = 0x02;
			Encoding.ASCII.GetBytes("D3", 0, 2, out_buffer, 1);
			out_buffer[3] = 0x03;
			byte crc = CRC(out_buffer);
			out_buffer[4] = ByteToAscii((byte)(crc>>4));
			out_buffer[5] = ByteToAscii((byte)(crc&0x0F));
			out_buffer[6] = 0x0D;
		}

		private bool running = false;
		public bool Running
		{
			get { return running; }
		}

		public void Start()
		{
			Thread t = new Thread(new ThreadStart(Poll));
			t.Name = "PowerMaster Poll Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
			running = true;
		}

		public void Stop()
		{
			running = false;
		}

		private byte[] out_buffer;
		private void Poll()
		{
			while(running)
			{
				if(com_port == null || !com_port.IsOpen) break;

				com_port.Write(out_buffer, 0, 7);
				while(com_port.BytesToRead > 0)
				{
					string s = com_port.ReadLine();
					if(s.Length >= 10 && s[1] == 'D')
						last_rx = s;
					Thread.Sleep(5);
				}
				Thread.Sleep(100);
			}
		}

		private byte CRC(byte[] b)
		{
			byte crc = 0;
			Debug.Assert(b[0] == 0x02);
			for(int i=1; b[i] != 0x03 && i<b.Length; i++)
				crc = crc8revtab[crc ^ b[i]];
			return (byte)(~crc);
		}

		private byte ByteToAscii(byte b)
		{
			byte ret_val = (byte)(b+0x30);
			if(ret_val > 0x39) ret_val += 7;
			return ret_val;
		}

		private static byte[] crc8revtab =
		{
		//	0x00  0x01  0x02  0x03  0x04  0x05  0x06  0x07  0x08  0x09  0x0A  0x0B  0x0C  0x0D  0x0E  0x0F
/*0x0*/		0x00, 0xB1, 0xD3, 0x62, 0x17, 0xA6, 0xC4, 0x75, 0x2E, 0x9F, 0xFD, 0x4C, 0x39, 0x88, 0xEA, 0x5B,
/*0x1*/		0x5C, 0xED, 0x8F, 0x3E, 0x4B, 0xFA, 0x98, 0x29,	0x72, 0xC3, 0xA1, 0x10, 0x65, 0xD4, 0xB6, 0x07,
/*0x2*/		0xB8, 0x09, 0x6B, 0xDA, 0xAF, 0x1E, 0x7C, 0xCD,	0x96, 0x27, 0x45, 0xF4, 0x81, 0x30, 0x52, 0xE3,
/*0x3*/		0xE4, 0x55, 0x37, 0x86, 0xF3, 0x42, 0x20, 0x91,	0xCA, 0x7B, 0x19, 0xA8, 0xDD, 0x6C, 0x0E, 0xBF,
/*0x4*/		0xC1, 0x70, 0x12, 0xA3, 0xD6, 0x67, 0x05, 0xB4,	0xEF, 0x5E, 0x3C, 0x8D, 0xF8, 0x49, 0x2B, 0x9A,
/*0x5*/		0x9D, 0x2C, 0x4E, 0xFF, 0x8A, 0x3B, 0x59, 0xE8,	0xB3, 0x02, 0x60, 0xD1, 0xA4, 0x15, 0x77, 0xC6,
/*0x6*/		0x79, 0xC8, 0xAA, 0x1B, 0x6E, 0xDF, 0xBD, 0x0C, 0x57, 0xE6, 0x84, 0x35, 0x40, 0xF1, 0x93, 0x22,
/*0x7*/		0x25, 0x94, 0xF6, 0x47, 0x32, 0x83, 0xE1, 0x50,	0x0B, 0xBA, 0xD8, 0x69, 0x1C, 0xAD, 0xCF, 0x7E,
/*0x8*/		0x33, 0x82, 0xE0, 0x51, 0x24, 0x95, 0xF7, 0x46, 0x1D, 0xAC, 0xCE, 0x7F, 0x0A, 0xBB, 0xD9, 0x68,
/*0x9*/		0x6F, 0xDE, 0xBC, 0x0D, 0x78, 0xC9, 0xAB, 0x1A,	0x41, 0xF0, 0x92, 0x23, 0x56, 0xE7, 0x85, 0x34,
/*0xA*/		0x8B, 0x3A, 0x58, 0xE9, 0x9C, 0x2D, 0x4F, 0xFE,	0xA5, 0x14, 0x76, 0xC7, 0xB2, 0x03, 0x61, 0xD0,
/*0xB*/		0xD7, 0x66, 0x04, 0xB5, 0xC0, 0x71, 0x13, 0xA2,	0xF9, 0x48, 0x2A, 0x9B, 0xEE, 0x5F, 0x3D, 0x8C,
/*0xC*/		0xF2, 0x43, 0x21, 0x90, 0xE5, 0x54, 0x36, 0x87,	0xDC, 0x6D, 0x0F, 0xBE, 0xCB, 0x7A, 0x18, 0xA9,
/*0xD*/		0xAE, 0x1F, 0x7D, 0xCC, 0xB9, 0x08, 0x6A, 0xDB,	0x80, 0x31, 0x53, 0xE2, 0x97, 0x26, 0x44, 0xF5,
/*0xE*/		0x4A, 0xFB, 0x99, 0x28, 0x5D, 0xEC, 0x8E, 0x3F,	0x64, 0xD5, 0xB7, 0x06, 0x73, 0xC2, 0xA0, 0x11,
/*0xF*/		0x16, 0xA7, 0xC5, 0x74, 0x01, 0xB0, 0xD2, 0x63, 0x38, 0x89, 0xEB, 0x5A, 0x2F, 0x9E, 0xFC, 0x4D
		};


		private void SerialPort_ErrorReceived(object source, SerialErrorReceivedEventArgs e)
		{
			
		}
		
		private void SerialPort_PinChanged(object source, SerialPinChangedEventArgs e)
		{
			
		}	
		
		private float watts = 0.0f;
		public float Watts
		{
			get 
			{
				if(last_rx != "" && last_rx[1] == 'D')
					watts = float.Parse(last_rx.Substring(3, 7));
				return watts;
			}
		}

		private string last_rx = "";

        private void SerialPort_DataReceived(object source, SerialDataReceivedEventArgs e)
		{
			/*Debug.Write("Serial Debug: ");
			while(com_port.InBufferBytes > 0)
				Debug.Write(com_port.ReadByte().ToString("X")+" ");
			Debug.WriteLine("");*/
			/*string s = com_port.ReadLine();
			Debug.WriteLine("Serial Debug: "+s);
			if(s[1] == 'D')
				last_rx = s;*/
		}
	}
}
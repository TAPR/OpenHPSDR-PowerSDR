//=================================================================
// pal.cs
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

namespace PowerSDR
{
	public class Pal
	{
		[DllImport("pal.dll", EntryPoint="Init")]
		[return:MarshalAs(UnmanagedType.I1)]
		public static extern bool Init();				// initialize PAL system

		[DllImport("pal.dll", EntryPoint="GetNumDevices")]
		public static extern int GetNumDevices();

		[DllImport("pal.dll", EntryPoint="GetDeviceInfo")]
		[return:MarshalAs(UnmanagedType.I1)]
		public static extern bool GetDeviceInfo(uint index, out uint model, out uint sn);

		[DllImport("pal.dll", EntryPoint="SelectDevice")]
		[return:MarshalAs(UnmanagedType.I1)]
		public static extern bool SelectDevice(int index);

		[DllImport("pal.dll", EntryPoint="WriteOp")]
		public static extern int WriteOp(FWC.Opcode opcode, uint data1, uint data2);

        [DllImport("pal.dll", EntryPoint = "WriteOp")]
        public static extern int WriteOp(FWC.Opcode opcode, int data1, int data2);

		[DllImport("pal.dll", EntryPoint="WriteOp")]
		public static extern int WriteOp(FWC.Opcode opcode, uint data1, float data2);

		[DllImport("pal.dll", EntryPoint="WriteOp")]
		public static extern int WriteOp(FWC.Opcode opcode, float data1, uint data2);

		[DllImport("pal.dll", EntryPoint="ReadOp")]
		public static extern int ReadOp(FWC.Opcode opcode, uint data1, uint data2, out uint rtn);

		[DllImport("pal.dll", EntryPoint="ReadOp")]
		public static extern int ReadOp(FWC.Opcode opcode, uint data1, uint data2, out float rtn);

		[DllImport("pal.dll", EntryPoint="Exit")]
		public static extern void Exit();				// cleanup and leave system in a stable state

		[DllImport("pal.dll", EntryPoint="SetCallback")]
		public static extern void SetCallback(NotificationCallback callback);

        [DllImport("pal.dll", EntryPoint = "SetBufferSize")]
        public static extern void SetBufferSize(uint val);

		public delegate void NotificationCallback(uint bitmap); 
	}
}

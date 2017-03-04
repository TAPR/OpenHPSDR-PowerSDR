//=================================================================
// paddle.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2004, 2005, 2006  FlexRadio Systems
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
//    12100 Technology Blvd.
//    Austin, TX 78727
//    USA
//=================================================================
// Originally written by Bob Tracy K5KDN.
// Modified by Eric Wachsmann.
//=================================================================

using System;
using System.Threading;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace CWKeyer
{
	public class Paddle
	{
		public static JoystickState jsState = new JoystickState();
		public Device paddleDevice = null;
		int lastDit = 0;
		int lastDah = 0;
		public delegate void PaddleEventHandler(int[] args);
		public event PaddleEventHandler Changed;
		System.Timers.Timer pollTimer;

		public Paddle()
		{

			foreach(DeviceInstance di in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
			{
				paddleDevice = new Device(di.InstanceGuid);
				break;
			}

			if(paddleDevice != null)
			{
				try
				{
					paddleDevice.SetDataFormat(DeviceDataFormat.Joystick);
					paddleDevice.Acquire();
				}
				catch(InputException)
				{
					
				}
			}

			pollTimer = new System.Timers.Timer(1);
			pollTimer.Enabled = false;
			pollTimer.Elapsed += new System.Timers.ElapsedEventHandler(pollTimer_Elapsed);
			
		}

		// Changed 02/19/2005 K5KDN, added MS suggested code 
		// for event handling in multithreaded environment
		protected virtual void OnChanged(int[] args)
		{
			PaddleEventHandler handler = Changed;
			if(handler != null)
			{
				handler(args);
			}
		}


//		protected virtual void OnChanged(int[] args)
//		{
//			if(Changed != null)
//			{
//				Changed(args);
//			}
//		}

		public bool TimerEnabled
		{
			get { return pollTimer.Enabled; }
			set { pollTimer.Enabled = value; }
		}

		public bool JoystickPresent
		{
			get { return (paddleDevice != null); }
		}

		public void ReadPaddle()
		{
			paddleDevice.Poll();
			jsState = paddleDevice.CurrentJoystickState;
			byte[] contacts = jsState.GetButtons();
			int thisDit = contacts[0];
			int thisDah = contacts[1];
			if(thisDit != lastDit | (thisDah != lastDah))
			{
				lastDit = thisDit;
				lastDah = thisDah;
				int[] bauds = {thisDit, thisDah};
				OnChanged(bauds);
			}			
		}

		private void pollTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			ReadPaddle();
		}
	}
}


//=================================================================
// hardware_v2.cs
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
// Ozy Modifications Copyright (C) 2007 Bill Tracey (kd5tfd) 

//#define DEBUG_VERBOSE

namespace PowerSDR
{
    using System;
    using System.Threading;
    //using PortTalk;

	public class HW
	{
		#region Structs

		public struct Config
		{
			public int config;
			public byte address;

			public Config(int c, byte add)
			{
				config = c;
				address = add;
			}
		}

		#endregion

		#region Constants

		// Configurations
		private const int PIO = 0;
		private const int RFE = 1;
		private const int RFX = 2;

		// PIO IC1 pins
		private const int BPF0		= 0;
		private const int BPF1		= 1;
		private const int BPF3		= 2;
		private const int BPF2		= 3;
		private const int BPF4		= 4;
		private const int BPF5		= 5;
		private const int TR		= 6;
		private const int MUTE		= 7;

		// PIO IC3 pins
		private const int X2_1		= 0;
		private const int X2_2		= 1;
		private const int X2_3		= 2;
		private const int X2_4		= 3;
		private const int X2_5		= 4;
		private const int X2_6		= 5;
		private const int X2_7		= 6;
		private const int GAIN		= 7;

		// RFE IC7 pins
		private const int AMP2		= 0;	// 1W -- switch together with AMP1
		private const int AMP1		= 1;	// 1W -- switch together with AMP2
		private const int XVTX		= 2;
		private const int XVEN		= 3;
		private const int ATTN		= 4;
		private const int IMPR_EN	= 5;
		private const int PA_BIAS	= 6;
		private const int IMPR		= 7;

		// RFE IC9 pins
		private const int LPF0		= 0;
		private const int LPF1		= 1;
		private const int LPF2		= 2;
		private const int LPF3		= 3;
		private const int LPF4		= 4;
		private const int LPF5		= 5;
		private const int LPF6		= 6;
		private const int LPF7		= 7;

		// RFE IC10 pins
		private const int LPF8		= 0;
		private const int LPF9		= 1;
		private const int BPF5_PT	= 2;
		private const int BPF4_PT	= 3;
		private const int BPF2_PT	= 4;
		private const int BPF3_PT	= 5;
		private const int BPF1_PT	= 6;
		private const int BPF0_PT	= 7;

		// RFE IC11 pins
		private const int PAF0		= 0;
		private const int PAF1		= 1;
		private const int PAF2		= 2;
		private const int ADC_CLK	= 3;
		private const int ADC_DI	= 4;
		private const int ADC_CS_NOT= 5;
		private const int PA_TR		= 6;
		private const int ATU_CTL	= 7;

		// Control and data line pins for RFE serial decoders
		private const int SER		= 0x01;
		private const int SCK		= 0x02;
		private const int SCLR_NOT	= 0x04;
		private const int DCDR_NE	= 0x20;
		
		// PIO Register Address
		private const int PIO_IC1	= 0x9;
		private const int PIO_IC3	= 0xA;
		private const int PIO_IC8	= 0x3;
		private const int PIO_IC11	= 0xF;
		private const int PIO_NONE	= 0xB;

		// RFE 1:4 Decoder (74HC139) values to drive shift register RCK lines
		private const int RFE_IC7	= 0x08;
		private const int RFE_IC9	= 0x18;
		private const int RFE_IC10	= 0x10;
		private const int RFE_IC11	= 0x00;

		// PA filter selection
		private const int PA_LPF_OFF	= 0;					// 0
		private const int PA_LPF_12_10	= 1<<PAF0;				// 1
		private const int PA_LPF_17_15	= 1<<PAF1;				// 2
		private const int PA_LPF_30_20	= 1<<PAF0 | 1<<PAF1;	// 3
		private const int PA_LPF_60_40	= 1<<PAF2;				// 4
		private const int PA_LPF_80		= 1<<PAF2 | 1<<PAF0;	// 5
		private const int PA_LPF_160	= 1<<PAF2 | 1<<PAF1;	// 6

		// DDS Variables
		private const byte DDSWRB		= 0x40;
		private const byte DDSRESET		= 0x80;
		private const byte COMP_PD		= 0x10;		// DDS Comparator power down
		private const byte BYPASS_PLL	= 0x20;		// Bypass DDS PLL
		private const byte BYPASS_SINC	= 0x40;		// Bypass Inverse Sinc Filter

		#endregion

		#region Variable declaration

		private Register8WriteDel write_reg;

		private Register8 pio_ic1;
		private Register8 pio_ic3;
		public Register8 rfe_ic9;
		public Register8 rfe_ic10;
		public Register8 rfe_ic7;
		public Register8 rfe_ic11;
        public static Console console = null;

		#endregion

		#region Constructor

		public HW()
		{
			//Parallel.InitPortTalk();
			//lpt_addr = (ushort)addr;
			write_reg = new Register8WriteDel(UpdateRegister8);

			pio_ic1 = new Register8("pio_ic1", write_reg, new Config(PIO, PIO_IC1));
			MuteRelay = false;
			pio_ic1.SetBit(5);
			pio_ic3 = new Register8("pio_ic3", write_reg, new Config(PIO, PIO_IC3));
			GainRelay = true;
			rfe_ic7 = new Register8("rfe_ic7", write_reg, new Config(RFE, RFE_IC7));
			PABias = false;
			rfe_ic9 = new Register8("rfe_ic9", write_reg, new Config(RFE, RFE_IC9));
			rfe_ic10 = new Register8("rfe_ic10", write_reg, new Config(RFE, RFE_IC10));
			rfe_ic11 = new Register8("rfe_ic11", write_reg, new Config(RFE, RFE_IC11));
		}

		~HW()
		{
			//Parallel.ExitPortTalk();
		}


		#endregion

		#region Properties

		#region Configurations

        //private ushort lpt_addr;
        //public ushort LPTAddr
        //{
        //    get { return lpt_addr; }
        //    set { lpt_addr = value; }
        //}
 
        // returns true if current model is hpsdr, false otherwise 
        private bool isHPSDRorHermes()
        {
            Console c = Console.getConsole();
            if (c != null)
            {
                if (c.ModelIsHPSDRorHermes())
                {
                    return true;
                }
            }
            return false;
        } 

		private bool xvtr_present = false;
		public bool XVTRPresent
		{
			//get { return xvtr_present; }
			set
			{
				xvtr_present = value;
				XVTR_TR = value;
			}
		}

		private bool pa_present = false;
		public bool PAPresent
		{
			//get { return pa_present; }
			set { pa_present = value; }
		}

		private bool atu_present = false;
		public bool ATUPresent
		{
			//get { return atu_present; }
			set	{ atu_present = value; }
		}

		private bool usb_present = false;
		public bool USBPresent 
		{
			//get { return usb_present; }
			set { usb_present = value; }
		}

        private bool ozy_control = false;
        public bool OzyControl
        {
            set { ozy_control = value; }
        }

        private bool ptto_delay_control = false;
        public bool PTTODelayControl
        {
            get { return ptto_delay_control; }
            set { ptto_delay_control = value; }
        }

		private int pll_mult = 1;
		public int PLLMult
		{
			//get { return pll_mult; }
			set { pll_mult = value; }
		}

        private static int ptt_out_delay = 20;
        public static int PTTOutDelay
        {
            get { return ptt_out_delay; }
            set { ptt_out_delay = value; }
        }

     /*   private bool tun_control = false;
        public bool TUNControl
        {
            get { return tun_control; }
            set { tun_control = value; }
        } */

		#endregion

		#region Control

		public BPFBand BPFRelay	// gets or sets the BPF Relay using an integer index
		{
//			get
//			{
//				BPFBand retval = BPFBand.NONE;
//				if(!rfe_present)
//				{
//					switch(pio_ic1.GetData() & 0x3f)
//					{
//						case 1 << BPF0:
//							retval = BPFBand.B160;
//							break;
//						case 1 << BPF1:
//							retval = BPFBand.B60;
//							break;
//						case 1 << BPF2:
//							retval = BPFBand.B20;
//							break;
//						case 1 << BPF3:
//							retval = BPFBand.B40;
//							break;
//						case 1 << BPF4:
//							retval = BPFBand.B10;
//							break;
//						case 1 << BPF5:
//							retval = BPFBand.B6;
//							break;
//						default:
//							retval = BPFBand.NONE;
//							break;
//					}
//				}
//				else // RFE is present
//				{
//					switch(rfe_ic10.GetData() & 0xfc)
//					{
//						case 1 << BPF0_PT:
//							retval = BPFBand.B160;
//							break;
//						case 1 << BPF1_PT:
//							retval = BPFBand.B60;
//							break;
//						case 1 << BPF2_PT:
//							retval = BPFBand.B20;
//							break;
//						case 1 << BPF3_PT:
//							retval = BPFBand.B40;
//							break;
//						case 1 << BPF4_PT:
//							retval = BPFBand.B10;
//							break;
//						case 1 << BPF5_PT:
//							retval = BPFBand.B6;
//							break;
//						default:
//							retval = BPFBand.NONE;
//							break;
//					}
//				}
//
//#if(DEBUG_VERBOSE)
//				if(retval < 0)
//					Debug.WriteLine("Error in BPFRelay->Get -- invalid register value ("+(rfe_ic10.GetData()&0xfc)+").");
//#endif
//					
//				return retval;
//			}
			set
			{
				switch(value)
				{
					case BPFBand.NONE:
						rfe_ic10.SetData((byte)(rfe_ic10.GetData() & 0x03));
						break;
					case BPFBand.B160:
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0x03) | (1 << BPF0_PT)));
						break;
					case BPFBand.B60:
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0x03) | (1 << BPF1_PT)));
						break;
					case BPFBand.B20:
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0x03) | (1 << BPF3_PT)));
						break;
					case BPFBand.B40:
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0x03) | (1 << BPF2_PT)));
						break;
					case BPFBand.B10:
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0x03) | (1 << BPF4_PT)));
						break;
					case BPFBand.B6:
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0x03) | (1 << BPF5_PT)));
						break;
					default:
#if(DEBUG_VERBOSE)
							Debug.WriteLine("Error in BPFRelay->Set -- invalid relay passed ("+value+").");
#endif
						break;
				}
			}
		}
		
		public bool TransmitRelay	// true means TX mode
		{
//			get { return pio_ic1.GetBit(TR); }
			set 
			{
				Console c = Console.getConsole();
                if (value)
                {
                    if (c.CWFWKeyer &&
                        c.RX1DSPMode == DSPMode.CWL ||
                        c.RX1DSPMode == DSPMode.CWU)
                    JanusAudio.SetXmitBit(0);
                    else JanusAudio.SetXmitBit(1);

                    if (c.serialPTT != null)
                    {
                        c.serialPTT.setDTR(true);
                    }
                   // if (c.Keyer.PrimaryConnPort != "SDR" &&
                   //     c.Keyer.PrimaryConnPort != "Ozy")
                  //  {
                    //    c.Keyer.sp.RtsEnable = true;
                   // }

                   // pio_ic1.SetBit(TR);
                }
                else
                {
                    if (ptto_delay_control &&
                        c.RX1DSPMode != DSPMode.CWL &&
                        c.RX1DSPMode != DSPMode.CWU)
                        PTTODelayStart();
                    else
                        JanusAudio.SetXmitBit(0);
  
                    if (c.serialPTT != null)
                    {
                        c.serialPTT.setDTR(false);
                    }
                   // if (c.Keyer.PrimaryConnPort != "SDR" &&
                    //    c.Keyer.PrimaryConnPort != "Ozy")
                   // {
                   //     c.Keyer.sp.RtsEnable = false;
                   // }
                }
			}
		}

		public bool MuteRelay			// true means the Mute Relay is engaged (muted)
		{
//			get { return !pio_ic1.GetBit(MUTE); }
			set
			{
				if(!value)
					pio_ic1.SetBit(MUTE);
				else
					pio_ic1.ClearBit(MUTE);					
			}
		}

		public byte X2	// gets or sets the X2 pins 1-7
		{
			get { return (byte)(pio_ic3.GetData() & (byte)0x7f); }
			set	
			{
				//System.Console.WriteLine("X2: "+value.ToString("X"));
				bool update = pio_ic3.UpdateHardware;
				if(update) pio_ic3.UpdateHardware = false;
				for(int i=0; i<7; i++)
				{
					if((value>>i & 0x1) == 1) pio_ic3.SetBit(i);
					else pio_ic3.ClearBit(i);
				}
				if(update) pio_ic3.UpdateHardware = true;
				//pio_ic3.SetData((byte)((pio_ic3.GetData() & 0x80) | (value & 0x7f)));
			}
		}


		public bool GainRelay			// true means 0dB (40dB for old configs)
		{
//			get { return !pio_ic3.GetBit(GAIN); }
			set
			{
				if(!value)
					pio_ic3.SetBit(GAIN);
				else
					pio_ic3.ClearBit(GAIN);
			}
		}

		// RFE only properties
		public RFELPFBand RFE_LPF	// returns an integer index into the LPF switches
		{
//			get
//			{
//				RFELPFBand retval = RFELPFBand.NONE;
//
//				if(rfe_ic9.GetData() != 0)
//				{
//					switch(rfe_ic9.GetData())
//					{
//						case 1 << LPF0:
//							retval = RFELPFBand.AUX;
//							break;
//						case 1 << LPF1:
//							retval = RFELPFBand.B6;
//							break;
//						case 1 << LPF2:
//							retval = RFELPFBand.B60;
//							break;
//						case 1 << LPF3:
//							retval = RFELPFBand.B20;
//							break;
//						case 1 << LPF4:
//							retval = RFELPFBand.B30;
//							break;
//						case 1 << LPF5:
//							retval = RFELPFBand.B40;
//							break;
//						case 1 << LPF6:
//							retval = RFELPFBand.B1210;
//							break;
//						case 1 << LPF7:
//							retval = RFELPFBand.B80;
//							break;
//						default:
//							retval = RFELPFBand.NONE;
//							break;
//					}
//				}
//				else // LPF is 8 or 9
//				{
//					switch(rfe_ic10.GetData() & 0x03)
//					{
//						case 1 << LPF8:
//							retval = RFELPFBand.B1715;
//							break;
//						case 1 << LPF9:
//							retval = RFELPFBand.B160;
//							break;
//						default:
//							retval = RFELPFBand.NONE;
//							break;
//					}
//				}
//
//#if(DEBUG_VERBOSE)
//				if(retval < 0)
//					Debug.WriteLine("Error in RFE_LPF->Get -- invalid register value ("+(rfe_ic9.GetData()&0xfc)+").");
//#endif
//				return retval;
//			}
			set
			{
				switch(value)
				{
					case RFELPFBand.NONE:
						rfe_ic9.SetData(0);
						rfe_ic10.SetData((byte)(rfe_ic10.GetData() & 0xfc));
						break;
					case RFELPFBand.AUX:
					case RFELPFBand.B6:
					case RFELPFBand.B60:
					case RFELPFBand.B20:
					case RFELPFBand.B30:
					case RFELPFBand.B40:
					case RFELPFBand.B1210:
					case RFELPFBand.B80:
						rfe_ic10.SetData((byte)(rfe_ic10.GetData() & 0xfc));
						rfe_ic9.SetData((byte)(1 << (byte)value));
						break;
					case RFELPFBand.B1715:
					case RFELPFBand.B160:
						rfe_ic9.SetData(0);
						rfe_ic10.SetData((byte)((rfe_ic10.GetData() & 0xfc) | (1 << ((byte)value-8))));
						break;
					default:
#if(DEBUG_VERBOSE)
						Debug.WriteLine("Error in RFE_LPF->Set -- invalid relay passed ("+value+").");
#endif
						break;
				}
			}
		}

		public bool RFE_TR
		{
//			get
//			{
//				bool amp1 = rfe_ic7.GetBit(AMP1);
//				bool amp2 = rfe_ic7.GetBit(AMP2);
//				
//				if(amp1 && amp2) return true;
//				if(!amp1 && !amp2) return false;
//#if(DEBUG_VERBOSE)
//				Debug.WriteLine("Error in RFE_TR->Get -- invalid amp1/amp2 ("+amp1+"/"+amp2+") combo.");
//#endif
//				return false;
//			}
			set
			{
				if(value)
					rfe_ic7.SetData((byte)(rfe_ic7.GetData() | 0x03));
				else
					rfe_ic7.SetData((byte)(rfe_ic7.GetData() & 0xfc));
			}
		}

		public bool XVTR_RF		// true means the RF path is active to the XVTR
		{
			get
			{
				return rfe_ic7.GetBit(XVEN);
			}
			set
			{
				if(value)
					rfe_ic7.SetBit(XVEN);
				else
					rfe_ic7.ClearBit(XVEN);
			}
		}

		public bool XVTR_TR	// true means the TR relay on the xvtr is active
		{
//			get
//			{
//				return rfe_ic7.GetBit(XVTX);
//			}
			set
			{
				if(value)
					rfe_ic7.SetBit(XVTX);
				else
					rfe_ic7.ClearBit(XVTX);
			}
		}

		public bool Attn		// true means the 10dB attenuator is switched inline
		{
//			get
//			{
//				return rfe_ic7.GetBit(ATTN);
//			}
			set
			{
				if(value)
					rfe_ic7.SetBit(ATTN);
				else
					rfe_ic7.ClearBit(ATTN);
			}
		}

		public bool ImpulseEnable
		{
//			get
//			{
//				return rfe_ic7.GetBit(IMPR_EN);
//			}
			set
			{
				if(value)
					rfe_ic7.SetBit(IMPR_EN);
				else
					rfe_ic7.ClearBit(IMPR_EN);
			}
		}

		public bool PABias
		{
//			get
//			{
//				return !rfe_ic7.GetBit(PA_BIAS);
//			}
			set
			{
				if(!value)
					rfe_ic7.SetBit(PA_BIAS);
				else
					rfe_ic7.ClearBit(PA_BIAS);
			}
		}

		public PAFBand PA_LPF
		{
//			get
//			{
//				return (PAFBand)(rfe_ic11.GetData() & 0x07);
//			}
			set
			{
				if(value < 0)
					rfe_ic11.SetData((byte)(rfe_ic11.GetData() & 0xf8));
				else
					rfe_ic11.SetData((byte)((rfe_ic11.GetData() & 0xf8) | ((byte)value & 0x07)));
			}
		}

		public bool PA_ADC_CLK
		{
//			get
//			{
//				return rfe_ic11.GetBit(ADC_CLK);
//			}
			set
			{
				if(value)
					rfe_ic11.SetBit(ADC_CLK);
				else
					rfe_ic11.ClearBit(ADC_CLK);
			}
		}

		public bool PA_ADC_DI
		{
//			get
//			{
//				return rfe_ic11.GetBit(ADC_DI);
//			}
			set
			{
				if(value)
					rfe_ic11.SetBit(ADC_DI);
				else
					rfe_ic11.ClearBit(ADC_DI);
			}
		}

		public bool PA_ADC_CS_NOT
		{
//			get
//			{
//				return rfe_ic11.GetBit(ADC_CS_NOT);
//			}
			set
			{
				if(value)
					rfe_ic11.SetBit(ADC_CS_NOT);
				else
					rfe_ic11.ClearBit(ADC_CS_NOT);
			}
		}

		public bool PA_TR_Relay
		{
//			get
//			{
//				return rfe_ic11.GetBit(PA_TR);
//			}
			set
			{
				if(value)
					rfe_ic11.SetBit(PA_TR);
				else
					rfe_ic11.ClearBit(PA_TR);
			}
		}

		public bool ATU_DI
		{
//			get
//			{
//				return rfe_ic11.GetBit(ATU_CTL);
//			}
			set
			{
				if(value)
					rfe_ic11.SetBit(ATU_CTL);
				else
					rfe_ic11.ClearBit(ATU_CTL);
			}
		}

//        private long dds_tuning_word = 0;
//        public long DDSTuningWord
//        {
//            get { return dds_tuning_word; }
//            set
//            {
//                if(value != dds_tuning_word)
//                {
//                    dds_tuning_word = value;		   //save new tuning word    

//                    Console c = Console.getConsole();

//#if false 
//                    if (c != null && c.label_DDStune != null ) 
//                    { 
//                        double dds_tune = c.TuningWordToFreq(dds_tuning_word);	// = dds_tuning_word * c.DDSClockCorrection / Math.Pow(2,48);
//                        c.label_DDStune.Text = "DDS: " + dds_tune.ToString("f6");
//                    }
//#endif
//                    if (c != null)
//                    {   
//                       // c.SetupForm.txtTuningWord.Text = dds_tuning_word.ToString();
//                        //double dds_tune = c.TuningWordToFreq(dds_tuning_word);
//                       // double dds_tune = c.TW2Freq(dds_tuning_word);
//                       // c.SetupForm.txt_DDStune.Text = dds_tune.ToString();
//                       // c.SetupForm.txtDDSRounded.Text = Math.Round(dds_tune, 6).ToString();
//                        //JanusAudio.SetVFOfreq(Math.Round(dds_tune, 6));

//                    }

//                    if (!isHPSDRorHermes())
//                    {
//                        for (int i = 0; i < 6; i++)
//                        {
//                            byte b = (byte)(dds_tuning_word >> (40 - i * 8));
//                            DDSWrite(b, (byte)(4 + i));
//                        }
//                    }
//                }
//            }
//        }

		private bool update_hardware = false;
		public bool UpdateHardware
		{
			get { return update_hardware; }
			set
			{
				update_hardware = value;
				
				pio_ic1.UpdateHardware = value;
				pio_ic3.UpdateHardware = value;

				rfe_ic7.UpdateHardware = value;
				rfe_ic9.UpdateHardware = value;
				rfe_ic10.UpdateHardware = value;
				rfe_ic11.UpdateHardware = value;
			}
		}

		#endregion

		#endregion

		#region Private Functions

		private void LatchRegister(ushort lpt, byte addr, byte data)
		{
			//Parallel.outport(lpt, data);
			//Parallel.outport((ushort)(lpt+2), addr);
			//Parallel.outport((ushort)(lpt+2), PIO_NONE);
		}

		private void UpdateRegister8(byte data, object user_data)
		{
			Config config = (Config)user_data;
			//switch(config.config)
			//{
				//case PIO:
                    if (ozy_control)
                    {
                        byte address = 0;
                        switch (config.address)
                        {
                            case PIO_IC1:
                                address = OzySDR1kControl.LATCH_BPF;
                                break;
                            case PIO_IC3:
                                address = OzySDR1kControl.LATCH_EXT;
                                break;
                            default:
                                address = 0;
                                break;
                        }
                        //OzySDR1kControl.Latch(address, data);
                        OzySDR1kControl.SRLoad(config.address, data);
                    }
                    else if (usb_present)
					{
						byte address = 0;
						switch(config.address)
						{
							case PIO_IC1:
								address = USB.SDR1K_LATCH_BPF;
								break;
							case PIO_IC3:
								address = USB.SDR1K_LATCH_EXT;
								break;
							default:
								address = 0;
								break;
						}
						//USB.Sdr1kLatch(address, data);
                        USB.Sdr1kSRLoad(config.address, data);
					}
                    else if (isHPSDRorHermes())
                    {
                        ;	 // do nothing 
                    }
                    else
					{
						//LatchRegister(lpt_addr, config.address, data);
						byte tmp_data;
						byte pio_data = (byte)(pio_ic1.GetData() & 0xC0);

						// Shift 8 bits into the 4 RFE shift registers
						for(int i=0x80; i>0; i >>= 1)
						{
							if((i & data) == 0)	// Current bit is low
							{
								tmp_data = (byte)(pio_data | SCLR_NOT); tmp_data |= DCDR_NE;
								//LatchRegister(lpt_addr, PIO_IC1, tmp_data);			// Output 0 bit
								tmp_data |= SCK;
								//LatchRegister(lpt_addr, PIO_IC1, tmp_data);		// Clock 0 into shift register
							}
							else				// Current bit is high
							{
								tmp_data = (byte)(pio_data | SCLR_NOT); tmp_data |= DCDR_NE; tmp_data |= SER;
								//LatchRegister(lpt_addr, PIO_IC1, tmp_data);		// Output 1 bit
								tmp_data |= SCK;
								//LatchRegister(lpt_addr, PIO_IC1, tmp_data);// Clock 1 into shift register
							}

							tmp_data = (byte)(pio_data | SCLR_NOT); tmp_data |= DCDR_NE;
							//LatchRegister(lpt_addr, PIO_IC1, tmp_data);				// Return SCK low
						}
						// Strobe the RFE 1:4 decoder output to transfer contents
						// of shift register to output latches
						tmp_data = (byte)(pio_data | SCLR_NOT); tmp_data |= config.address; tmp_data |= DCDR_NE; 
						//LatchRegister(lpt_addr, PIO_IC1, tmp_data);		// Latch 2:4 decoder outputs
						tmp_data = (byte)(pio_data | SCLR_NOT); tmp_data |= config.address;
						//LatchRegister(lpt_addr, PIO_IC1, tmp_data);		// Take 2:4 decoder enable low
						tmp_data = (byte)(pio_data | SCLR_NOT); tmp_data |= config.address; tmp_data |= DCDR_NE; 
						//LatchRegister(lpt_addr, PIO_IC1, tmp_data);		// Take 2:4 decoder enable high
					}
					
			}
						

		private void ResetDDS()
		{
            if (ozy_control)
            {
                OzySDR1kControl.DDSReset();
            }
            else if (usb_present)
			{
				USB.Sdr1kDDSReset();
			}
            else if (isHPSDRorHermes())
            {
                ;	 // do nothing 
            }
            else
			{
				//LatchRegister(lpt_addr, PIO_IC8, (byte)(DDSRESET | DDSWRB));	// Reset the DDS chip
				//LatchRegister(lpt_addr, PIO_IC8, DDSWRB);					// Leave WRB high
			}

			DDSWrite(COMP_PD, 0x1D);		//Power down comparator
			if(pll_mult == 1)
				DDSWrite(BYPASS_PLL, 0x1E);
			else
				DDSWrite((byte)pll_mult, 0x1E);
			DDSWrite(BYPASS_SINC, 0x20);
		}

		private void DDSWrite(byte data, byte addr)
		{
            if (ozy_control)
            {
                OzySDR1kControl.DDSWrite(addr, data);
            }
            else if (usb_present)
			{
				USB.Sdr1kDDSWrite(addr, data);
			}
            else if (isHPSDRorHermes())
            {
                ;	 // do nothing 
            }
            else
			{
				//Set up data bits
				//LatchRegister(lpt_addr, PIO_IC11, data);
			
				//Set up address bits with WRB high
				//LatchRegister(lpt_addr, PIO_IC8, (byte)(addr | DDSWRB));
    
				//Send write command with WRB low
				//LatchRegister(lpt_addr, PIO_IC8, addr);
    
				//Return WRB high
				//LatchRegister(lpt_addr, PIO_IC8, (byte)(addr | DDSWRB));
			}
		}

        private void PTTODelayStart()
        {
            Thread t = new Thread(new ThreadStart(PTTODelay));
            t.Name = "Run PTT Out Delay Thread";
            t.IsBackground = true;
            t.Priority = ThreadPriority.Highest;
            t.Start();
        }

        private void PTTODelay()
        {
           // HiPerfTimer t1 = new HiPerfTimer();
             //   t1.Start();
              //  t1.Stop();
              //  while (t1.DurationMsec < ptt_out_delay) t1.Stop();
                Thread.Sleep(ptt_out_delay);
                JanusAudio.SetXmitBit(0);
        }

		#endregion

		#region Public Functions

		public void Init()
		{
			UpdateHardware = false;
			//DDSTuningWord = 0;
			ResetDDS();

			UpdateRegister8(1<<MUTE, new Config(PIO, PIO_IC1));
			UpdateRegister8(0, new Config(PIO, PIO_IC3));

			UpdateRegister8(1<<PA_BIAS, new Config(RFE, RFE_IC7));
			UpdateRegister8(0, new Config(RFE, RFE_IC9));
			UpdateRegister8(0, new Config(RFE, RFE_IC10));
			UpdateRegister8(1<<ADC_CS_NOT, new Config(RFE, RFE_IC11));
		}
        private bool ignore_ptt = false;

        public void ignorePTT(bool v)
        {
            ignore_ptt = v;
            return;
        }

		public void StandBy()
		{
			UpdateHardware = false;
			//DDSTuningWord = 0;
			ResetDDS();			

			byte pio_ic1_temp = pio_ic1.GetData();
			pio_ic1.SetData(0);
			pio_ic1.SetBit(MUTE);

			byte pio_ic3_temp = pio_ic3.GetData();
			pio_ic3.SetData(0);

			byte rfe_ic7_temp = 0;
			byte rfe_ic9_temp = 0;
			byte rfe_ic10_temp = 0;
			byte rfe_ic11_temp = 0;

			rfe_ic7_temp = rfe_ic7.GetData();
			rfe_ic7.SetData(0);
			rfe_ic7.SetBit(PA_BIAS);

			rfe_ic9_temp = rfe_ic9.GetData();
			rfe_ic9.SetData(0);
				
			rfe_ic10_temp = rfe_ic10.GetData();
			rfe_ic10.SetData(0);

			rfe_ic11_temp = rfe_ic11.GetData();
			rfe_ic11.SetData(0);
			rfe_ic11.SetBit(ADC_CS_NOT);

			UpdateHardware = true;
			UpdateHardware = false;
			
			pio_ic1.SetData(pio_ic1_temp);
			pio_ic3.SetData(pio_ic3_temp);

			rfe_ic7.SetData(rfe_ic7_temp);
			rfe_ic9.SetData(rfe_ic9_temp);
			rfe_ic10.SetData(rfe_ic10_temp);
			rfe_ic11.SetData(rfe_ic11_temp);
		}

		public void PowerOn()
		{
			ResetDDS();
			//DDSTuningWord = dds_tuning_word;

			pio_ic1.ForceUpdate();
			pio_ic3.ForceUpdate();

			rfe_ic7.ForceUpdate();
			rfe_ic9.ForceUpdate();
			rfe_ic10.ForceUpdate();
			rfe_ic11.ForceUpdate();

			UpdateHardware = true;
		}

		public byte StatusPort()
		{
            if (ozy_control)
            {
                return (byte)OzySDR1kControl.GetStatusPort();
            }

			else if(usb_present) 
            {
				return (byte)USB.Sdr1kGetStatusPort();
            }
			else
			 if ( ignore_ptt || isHPSDRorHermes() ) 
				{
					return 0; /* kd5tfd hack */ 
				}
			else
                {
                    return 0;// Parallel.inport((ushort)(lpt_addr + 1));
                }
		}

		public void Impulse()
		{
			rfe_ic7.SetBit(IMPR);
			rfe_ic7.ClearBit(IMPR);
		}
	
		public byte PA_GetADC(int chan)
		{
			// get ADC on amplifier
			// 0 for forward power, 1 for reverse

            if (ozy_control)
            {
                System.Console.WriteLine("warning - Ozy ADC read not implemented!!");
                OzySDR1kControl.GetADC();
            }
            else if (usb_present)
			{
				int data = USB.Sdr1kGetADC();
				if(chan == 0)
					return (byte)(data & 255);
				else // chan == 1
					return (byte)(data >> 8);
			}

			PA_ADC_CS_NOT = false;			// CS not goes low
			PA_ADC_DI = true;			// set DI bit high for start bit
			PA_ADC_CLK = true;			// clock it into shift register
			PA_ADC_CLK = false;

			// set DI bit high for single ended -- done since DI is already high
			PA_ADC_CLK = true;			// clock it into shift register
			PA_ADC_CLK = false;

			if(chan == 0)	// Forward Power
			{
				PA_ADC_DI = false;		// set DI bit low for Channel 0
			}	
			else	// Reverse Power
			{
				// set DI bit high for Channel 1 -- done since DI is already high
			}
			
			PA_ADC_CLK = true;			// clock it into shift register
			PA_ADC_CLK = false;

			short num = 0;

			for(int i=0; i<8; i++)			// read 15 bits out of DO
			{
				PA_ADC_CLK = true;			// clock high
				PA_ADC_CLK = false;			// clock low

				if((StatusPort() & (byte)StatusPin.PA_DATA) != 0)	// read DO 
					num++;	// add bit		
				
				if(i != 7)
					num <<= 1;
			}

			PA_ADC_CS_NOT = true;		// CS not goes high

			//			if((num & 1<<14)>>14 != (num & 1<<0)>>0 ||
			//				(num & 1<<13)>>13 != (num & 1<<1)>>1 ||
			//				(num & 1<<12)>>12 != (num & 1<<2)>>2 ||
			//				(num & 1<<11)>>11 != (num & 1<<3)>>3 ||
			//				(num & 1<<10)>>10 != (num & 1<<4)>>4 ||
			//				(num & 1<<9)>>9 != (num & 1<<5)>>5 ||
			//				(num & 1<<8)>>8 != (num & 1<<6)>>6)
			//			{
			//				Debug.WriteLine("ADC Error");
			//				for(int i=0; i<8; i++)
			//					Debug.Write((num & 1<<(14-i))>>(14-i));
			//				Debug.WriteLine("");
			//				for(int i=0; i<8; i++)
			//					Debug.Write((num & 1<<i)>>i);
			//				Debug.WriteLine("");
			//			}
			//			//Debug.WriteLine(Convert.ToString(num, 2));
			//			num >>= 7;

			return (byte)(num);
		}

		public bool PA_ATUTune(ATUTuneMode mode)
		{
			rfe_ic11.ClearBit(ATU_CTL);

			int delay = 0;
			switch(mode)
			{
				case ATUTuneMode.BYPASS:
					delay = 250;
					break;
				case ATUTuneMode.MEMORY:
					delay = 2000;
					break;
				case ATUTuneMode.FULL:
					delay = 3250;
					break;
			}

			Thread.Sleep(delay);
			rfe_ic11.SetBit(ATU_CTL);

			int count = 0;
			if(mode == ATUTuneMode.MEMORY ||
				mode == ATUTuneMode.FULL)
			{
				while((StatusPort() & (byte)StatusPin.PA_DATA) != 0)	// wait for low output from ATU
				{
					Thread.Sleep(50);
					if(count++ > 240)	// 12 seconds
						return false;
				}
				count = 0;
				while((StatusPort() & (byte)StatusPin.PA_DATA) == 0)	// wait for high output from ATU
				{
					Thread.Sleep(50);
					if(count++ > 240)	// 12 seconds
						return false;
				}
				Thread.Sleep(250);
			}
			return true;
		}

		public void SetDDSDAC(int level)
		{
			DDSWrite(96, 32);
			DDSWrite((byte)(level >> 8), 33);
			DDSWrite((byte)level, 34);
			DDSWrite((byte)(level >> 8), 35);
			DDSWrite((byte)level, 36);
		}

		#endregion

		#region Test Functions

		public void TestPIO1()
		{
			for(int i=0; i<8; i++)
			{
                //LatchRegister(lpt_addr, PIO_IC1, (byte)(1<<i));
                //LatchRegister(lpt_addr, PIO_IC3, (byte)(1<<i));
                //LatchRegister(lpt_addr, PIO_IC8, (byte)(1<<i));
                //LatchRegister(lpt_addr, PIO_IC11, (byte)(1<<i));
				Thread.Sleep(2);
			}
		}

		public void TestPIO2(bool evens)
		{
			byte data = 0;

			for(int i=0; i<8; i++)
			{
				if(evens)
				{
					data <<= 1;
					if(i%2 == 0) data += 1;
				}
				else
				{
					data <<= 1;
					if(i%2 == 1) data += 1;
				}
			}

            //LatchRegister(lpt_addr, PIO_IC1, data);
            //LatchRegister(lpt_addr, PIO_IC3, data);
            //LatchRegister(lpt_addr, PIO_IC8, data);
            //LatchRegister(lpt_addr, PIO_IC11, data);
		}

		public void TestPIO3()
		{
			byte data = 0xFF;
            //LatchRegister(lpt_addr, PIO_IC1, data);
            //LatchRegister(lpt_addr, PIO_IC3, data);
            //LatchRegister(lpt_addr, PIO_IC8, data);
            //LatchRegister(lpt_addr, PIO_IC11, data);
			Thread.Sleep(2);

			data = 0;
            //LatchRegister(lpt_addr, PIO_IC1, data);
            //LatchRegister(lpt_addr, PIO_IC3, data);
            //LatchRegister(lpt_addr, PIO_IC8, data);
            //LatchRegister(lpt_addr, PIO_IC11, data);
			Thread.Sleep(2);
		}

		public void TestRFEIC11()
		{
			for(int i=0; i<8; i++)
			{
				UpdateRegister8((byte)(1<<i), new Config(RFE, RFE_IC11));
				Thread.Sleep(10);
			}
		}

		#endregion
	}
}


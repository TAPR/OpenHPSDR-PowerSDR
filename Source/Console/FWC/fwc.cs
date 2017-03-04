//=================================================================
// fwc.cs
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

//#define DAL

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PowerSDR
{
	unsafe public class FWC
	{
		#region Opcode Definition

		public enum Opcode
		{
			RDAL_OP_CREATE_INTERFACE = 1000,	
			RDAL_OP_START_INTERFACE,
			RDAL_OP_SET_CLOCK_SOURCE,
			RDAL_OP_SET_ROUTE,
			RDAL_OP_GET_CURRENT_STATUS,
			RDAL_OP_IS_LOCKED,
			RDAL_OP_GET_CURRENT_CONFIG,
			RDAL_OP_INSTALL_CALLBACK,
			RDAL_OP_UNINSTALL_CALLBACK,
			RDAL_OP_GLOBAL_MUTE,
			RDAL_OP_TOGGLE_LED,
			RDAL_OP_GET_EVM_UI,
			RDAL_OP_SET_OUT_CHANNEL_VOL,		// AXM20
			RDAL_OP_SET_IN_CHANNEL_VOL,			// AXM20
			RDAL_OP_SET_OUT_CHANNEL_MUTE,		// AXM20
			RDAL_OP_SET_IN_CHANNEL_MUTE,		// AXM20
			RDAL_OP_GET_IN_CHANNEL_VOL,			// AXM20
			RDAL_OP_GET_OUT_CHANNEL_VOL,		// AXM20
			RDAL_OP_NOOP,
			RDAL_OP_I2C_WRITE_VALUE,
			RDAL_OP_I2C_WRITE_2_VALUE,
			RDAL_OP_I2C_READ_VALUE,
			RDAL_OP_REG_GET,
			RDAL_OP_REG_SET,
			RDAL_OP_SET_SAMPLE_RATE,
			RDAL_OP_GET_RX_NUM,
			RDAL_OP_GET_RX_PRESENT_MASK,
			RDAL_OP_GET_RX_ENABLE_MASK,
			RDAL_OP_SET_RX_ENABLE_MASK,
			RDAL_OP_GET_TX_NUM,
			RDAL_OP_GET_TX_PRESENT_MASK,
			RDAL_OP_GET_TX_ENABLE_MASK,
			RDAL_OP_SET_TX_ENABLE_MASK,
			RDAL_OP_SET_DDS_CHAN,
			RDAL_OP_SET_DDS_FREQ,
			RDAL_OP_SET_DDS_AMPLITUDE,
			RDAL_OP_SET_PREAMP_ON,
			RDAL_OP_SET_ATT_ON,
			RDAL_OP_SET_ATT_VAL,
			RDAL_OP_SET_LED,
			RDAL_OP_SET_FILTER,
			RDAL_OP_READ_CLOCK_GEN,
			RDAL_OP_WRITE_CLOCK_GEN,
			RDAL_OP_READ_DDS,
			RDAL_OP_WRITE_DDS,
			RDAL_OP_UPDATE_DDS,
			RDAL_OP_READ_GPIO,
			RDAL_OP_WRITE_GPIO,
			RDAL_OP_READ_GPIO_DDR,
			RDAL_OP_WRITE_GPIO_DDR,
			RDAL_OP_SET_TEST_RELAY,
			RDAL_OP_SET_SIG_GEN_RELAY,
			RDAL_OP_SET_ADC_MASTER,
			RDAL_OP_GET_ADC_CAL_STATE,
			RDAL_OP_SET_ADC_HIGH_PASS_FILTER,
			RDAL_OP_SET_ADC_ZCAL,
			RDAL_OP_SET_ADC_RESET,
			RDAL_OP_SET_ENABLE_QSD,
			RDAL_OP_SET_POWER_DOWN1,
			RDAL_OP_SET_POWER_DOWN2,
			RDAL_OP_GET_PLL_STATUS,
			RDAL_OP_SET_DDS_MASTER,
			RDAL_OP_SET_PLL_ON,
			RDAL_OP_SET_IMPULSE_ON,
			RDAL_OP_SET_PORT,

			RDAL_OP_GET_FIRMWARE_REV = 1200,
			RDAL_OP_GET_TRX_OK = 1201,
			RDAL_OP_GET_TRX_REV = 1202,
			RDAL_OP_GET_TRX_SN = 1203,
			RDAL_OP_GET_PA_OK = 1204,
			RDAL_OP_GET_PA_REV = 1205,
			RDAL_OP_GET_PA_SN = 1206,
			RDAL_OP_GET_RFIO_OK = 1207,
			RDAL_OP_GET_RFIO_REV = 1208,
			RDAL_OP_GET_RFIO_SN = 1209,
			RDAL_OP_GET_ATU_OK = 1210,
			RDAL_OP_GET_ATU_REV = 1211,
			RDAL_OP_GET_ATU_SN = 1212,
			RDAL_OP_GET_RX2_OK = 1213,
			RDAL_OP_GET_RX2_REV = 1214,
			RDAL_OP_GET_RX2_SN = 1215,
			RDAL_OP_GET_VU_OK = 1216,
			RDAL_OP_GET_VU_REV = 1217,
			RDAL_OP_GET_VU_SN = 1218,
			RDAL_OP_INITIALIZE = 1219,		
			RDAL_OP_GET_SERIAL_NUM = 1220,
			RDAL_OP_GET_MODEL = 1221,	
			RDAL_OP_READ_TRX_EEPROM_UINT8 = 1222,
			RDAL_OP_WRITE_TRX_EEPROM_UINT8 = 1223,
			RDAL_OP_READ_TRX_EEPROM_UINT16 = 1224,
			RDAL_OP_WRITE_TRX_EEPROM_UINT16 = 1225,
			RDAL_OP_READ_TRX_EEPROM_UINT32 = 1226,	
			RDAL_OP_WRITE_TRX_EEPROM_UINT32 = 1227,
			RDAL_OP_READ_CLOCK_REG = 1228,
			RDAL_OP_WRITE_CLOCK_REG = 1229,
			RDAL_OP_READ_TRX_DDS_REG = 1230,
			RDAL_OP_WRITE_TRX_DDS_REG = 1231,
			RDAL_OP_READ_PIO_REG = 1232,
			RDAL_OP_WRITE_PIO_REG = 1233,
			RDAL_OP_TRX_POT_GET_RDAC = 1234,
			RDAL_OP_TRX_POT_SET_RDAC = 1235,				
			RDAL_OP_PA_POT_GET_RDAC = 1236,
			RDAL_OP_PA_POT_SET_RDAC = 1237,	
			RDAL_OP_SET_MUX = 1241,
			RDAL_OP_READ_CODEC_REG = 1242,
			RDAL_OP_WRITE_CODEC_REG = 1243,
			RDAL_OP_SET_RX1_FREQ = 1244,
			RDAL_OP_SET_RX2_FREQ = 1245,
			RDAL_OP_SET_TX_FREQ = 1246,
			RDAL_OP_SET_TRX_PREAMP = 1247,
			RDAL_OP_SET_TEST = 1248,
			RDAL_OP_SET_GEN = 1249,
			RDAL_OP_SET_SIG = 1250,
			RDAL_OP_SET_IMPULSE = 1251,
			RDAL_OP_SET_XVEN = 1252,
			RDAL_OP_SET_XVTXEN = 1253,
			RDAL_OP_SET_QSD = 1254,
			RDAL_OP_SET_QSE = 1255,
			RDAL_OP_SET_XREF = 1256,
			RDAL_OP_SET_RX1_FILTER = 1257,
			RDAL_OP_SET_RX2_FILTER = 1258,
			RDAL_OP_SET_TX_FILTER = 1259,
			RDAL_OP_SET_PA_FILTER = 1260,
			RDAL_OP_SET_INT_SPKR = 1261,
			RDAL_OP_SET_RX1_TAP = 1262,
			RDAL_OP_BYPASS_RX1_FILTER = 1263,
			RDAL_OP_BYPASS_TX_FILTER = 1264,
			RDAL_OP_BYPASS_PA_FILTER = 1265,
			RDAL_OP_READ_PTT = 1266,
			RDAL_OP_SET_HEADPHONE = 1267,
			RDAL_OP_SET_PLL = 1268,
			RDAL_OP_SET_RCA_TX1 = 1269,
			RDAL_OP_SET_RCA_TX2 = 1270,
			RDAL_OP_SET_RCA_TX3 = 1271,
			RDAL_OP_SET_FAN = 1272,
			RDAL_OP_GET_PLL_STATUS2 = 1273,
			RDAL_OP_READ_PA_ADC = 1274,
			RDAL_OP_SET_RX1_LOOP = 1275,
			RDAL_OP_SET_TR = 1276,
			RDAL_OP_SET_ANT = 1277,
			RDAL_OP_SET_RX1_ANT = 1278,
			RDAL_OP_SET_TX_ANT = 1279,
			RDAL_OP_SET_TXMON = 1280,
			RDAL_OP_SET_XVCOM = 1281,
			RDAL_OP_SET_EN_2M = 1282,
			RDAL_OP_SET_KEY_2M = 1283,
			RDAL_OP_SET_XVTR = 1284,
			RDAL_OP_SET_PA_BIAS = 1285,
			RDAL_OP_SET_POWER_OFF = 1286,
			RDAL_OP_SET_FPLED = 1287,
			RDAL_OP_SET_CTS = 1288,
			RDAL_OP_SET_RTS = 1289,
			RDAL_OP_SET_PC_RESET = 1290,
			RDAL_OP_SET_PC_PWRBT = 1291,
			RDAL_OP_SET_MOX = 1292,
			RDAL_OP_SET_INT_LED = 1293,
			RDAL_OP_ATU_SEND_CMD = 1294,
			RDAL_OP_ATU_GET_RESULT = 1295,
			RDAL_OP_SET_FULL_DUPLEX = 1296,
			RDAL_OP_SET_TX_DAC = 1297,
			RDAL_OP_SET_AMP_TX1 = 1298,
			RDAL_OP_SET_AMP_TX2 = 1299,
			RDAL_OP_SET_AMP_TX3 = 1300,
			RDAL_OP_SET_XVTR_ACTIVE = 1301,
			RDAL_OP_SET_XVTR_SPLIT = 1302,
			RDAL_OP_SET_RX1OUT = 1303,
			RDAL_OP_FLEXWIRE_WRITE_VALUE = 1304,
			RDAL_OP_FLEXWIRE_WRITE_2_VALUE = 1305,
			RDAL_OP_SET_RX1_DSP_MODE = 1306,
			RDAL_OP_SET_RX2_ANT = 1307,
			RDAL_OP_READ_RX2_EEPROM_UINT8 = 1308,
			RDAL_OP_WRITE_RX2_EEPROM_UINT8 = 1309,
			RDAL_OP_READ_RX2_EEPROM_UINT16 = 1310,
			RDAL_OP_WRITE_RX2_EEPROM_UINT16 = 1311,
			RDAL_OP_READ_RX2_EEPROM_UINT32 = 1312,	
			RDAL_OP_WRITE_RX2_EEPROM_UINT32 = 1313,
			RDAL_OP_READ_RX2_DDS_REG = 1314,
			RDAL_OP_WRITE_RX2_DDS_REG = 1315,
			RDAL_OP_SET_RX2_ON = 1319,
			RDAL_OP_SET_STANDBY = 1320,
			RDAL_OP_BYPASS_RX2_FILTER = 1321,
			RDAL_OP_SET_AMP_TX1_DELAY_ENABLE = 1322,
			RDAL_OP_SET_AMP_TX2_DELAY_ENABLE = 1323,
			RDAL_OP_SET_AMP_TX3_DELAY_ENABLE = 1324,
			RDAL_OP_SET_AMP_TX1_DELAY = 1325,
			RDAL_OP_SET_AMP_TX2_DELAY = 1326,
			RDAL_OP_SET_AMP_TX3_DELAY = 1327,
			RDAL_OP_RESET_RX2_DDS = 1328,
			RDAL_OP_SET_RX2_PREAMP = 1329,
			RDAL_OP_SET_RX2_DSP_MODE = 1330,
			RDAL_OP_SET_TRX_POT = 1331,	
			RDAL_OP_SET_IAMBIC = 1332,
			RDAL_OP_SET_BREAK_IN = 1333,
			RDAL_OP_SET_MANUAL_RX1_FILTER = 1334,
			RDAL_OP_SET_MANUAL_RX2_FILTER = 1335,
			RDAL_OP_SET_EEPROM_WC = 1336,
			RDAL_OP_SET_HIZ = 1337,
			RDAL_OP_ENABLE_ATU = 1338,	
			RDAL_OP_SET_ATU_ATTN = 1339,
			RDAL_OP_SET_PDRVMON = 1340,
			RDAL_OP_SET_RX_ATTN = 1341,
			RDAL_OP_FLEXWIRE_READ_VALUE = 1342,
			RDAL_OP_FLEXWIRE_READ_2_VALUE = 1343,
			RDAL_OP_SET_FAN_PWM = 1344,
			RDAL_OP_SET_FAN_SPEED = 1345,
			RDAL_OP_SYNC_PHASE = 1346,
			RDAL_OP_SET_RX1_FREQ_TW = 1347,
			RDAL_OP_SET_RX2_FREQ_TW = 1348,
			RDAL_OP_SET_TX_FREQ_TW = 1349,
			RDAL_OP_GET_REGION = 1350,
            RDAL_OP_SET_TX_DSP_FILTER = 1351,
            RDAL_OP_SET_TX_OFFSET = 1352,
            RDAL_OP_SET_TX_DSP_MODE = 1353,
            RDAL_OP_SET_CW_PITCH = 1354,
            RDAL_OP_GET_STATUS = 1355,
            FWC_OP_SET_VU_FAN_HIGH = 1356,
	        FWC_OP_SET_VU_KEY_V = 1357,
	        FWC_OP_SET_VU_TXIFU = 1358,
	        FWC_OP_SET_VU_KEY_U = 1359,
	        FWC_OP_SET_VU_RXURX2 = 1360,
	        FWC_OP_SET_VU_RX2U = 1361,
	        FWC_OP_SET_VU_RXIFU = 1362,
	        FWC_OP_SET_VU_K6 = 1363,
	        FWC_OP_SET_VU_K7 = 1364,
	        FWC_OP_SET_VU_K8 = 1365,
	        FWC_OP_SET_VU_K9 = 1366,
	        FWC_OP_SET_VU_K10 = 1367,
	        FWC_OP_SET_VU_K11 = 1368,
	        FWC_OP_SET_VU_K12 = 1369,
	        FWC_OP_SET_VU_K13 = 1370,
	        FWC_OP_SET_VU_K14 = 1371,
	        FWC_OP_SET_VU_RX2V = 1372,
	        FWC_OP_SET_VU_TX_U = 1373,
	        FWC_OP_SET_VU_TX_V = 1374,
		}

		#endregion

		#region Public Functions

		public static int SetRegister(uint addr, uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_REG_SET, addr, val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_REG_SET, addr, val);
		}

		public static int GetRegister(uint addr, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_REG_GET, addr, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_REG_GET, addr, 0, out val);
		}

		public static int GPIORead(out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_GPIO, 0, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_GPIO, 0, 0, out val);
		}
		
		public static int GPIOWrite(uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_GPIO, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_GPIO, val, 0);
		}

		public static int GPIODDRRead(out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_GPIO_DDR, 0, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_GPIO_DDR, 0, 0, out val);
		}

		public static int GPIODDRWrite(uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_GPIO_DDR, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_GPIO_DDR, val, 0);
		}

		public static int I2C_WriteValue(ushort addr, byte val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_I2C_WRITE_VALUE, (uint)addr, (uint)val); 
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_I2C_WRITE_VALUE, (uint)addr, (uint)val);
		}

		public static int I2C_Write2Value(ushort addr, byte v1, byte v2)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_I2C_WRITE_2_VALUE, (uint)addr, (uint)((v1<<8)+v2));
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_I2C_WRITE_2_VALUE, (uint)addr, (uint)((v1<<8)+v2));
		}

		public static int I2C_ReadValue(ushort addr, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_I2C_READ_VALUE, (uint)addr, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_I2C_READ_VALUE, (uint)addr, 0, out val);
		}

		#region SDR-X Routines

		/*public static int SetSampleRate(int slot, int rate)
		{
#if(DAL)
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			param[1] = (uint)rate;
			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetSampleRate: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_SAMPLE_RATE, 1000))
			{
				Debug.WriteLine("SetSampleRate: IssueOpcode Error: "+error);
				return -2;
			}
#else
			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_SAMPLE_RATE, (uint)slot, (uint)rate);
#endif
			return 0;
		}*/
/*
		public static int GetNumRX()
		{
			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_RX_NUM, 1000))
			{
				Debug.WriteLine("GetNumRX: IssueOpcode Error: "+error);
				return -1;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetNumRX: ReceiveResult Error");
				return -2;
			}

			if(data < 0 || data > 8) 
			{
				Debug.WriteLine("GetNumRX: Invalid result ("+data+")");
				return -3;
			}
            return (int)data;            
		}

		public static int GetRXPresentMask(out byte bit_array)
		{
			uint error;
			bit_array = 0;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_RX_PRESENT_MASK, 1000))
			{
				Debug.WriteLine("GetRXPresentMask: IssueOpcode Error");
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetRXPresentMask: ReceiveResult Error");
				return -3;
			}

			bit_array = (byte)data;

			return 0;
		}

		public static int GetRXEnableMask(out byte bit_array)
		{
			Debug.WriteLine((uint)Opcode.RDAL_OP_GET_RX_ENABLE_MASK);
			uint error;
			bit_array = 0;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_RX_ENABLE_MASK, 1000))
			{
				Debug.WriteLine("GetRXEnableMask: IssueOpcode Error");
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetRXEnableMask: ReceiveResult Error");
				return -3;
			}

			bit_array = (byte)data;

			return 0;
		}

		public static int SetRXEnableMask(byte bit_array)
		{
			uint param = (uint)bit_array;
			if(SendParam(&param, 4))
			{
				Debug.WriteLine("SetRXEnable: SendParam Error");
				return -1;
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_RX_ENABLE_MASK, 1000))
			{
				Debug.WriteLine("SetRXEnable: IssueOpcode Error");
				return -2;
			}

			return 0;
		}

		public static int GetNumTX()
		{
			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_TX_NUM, 1000))
			{
				Debug.WriteLine("GetNumTX: IssueOpcode Error: "+error);
				return -1;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetNumTX: ReceiveResult Error");
				return -2;
			}

			if(data < 0 || data > 8) 
			{
				Debug.WriteLine("GetNumTX: Invalid result ("+data+")");
				return -3;
			}
			return (int)data;            
		}

		public static int GetTXPresentMask(out byte bit_array)
		{
			uint error;
			bit_array = 0;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_TX_PRESENT_MASK, 1000))
			{
				Debug.WriteLine("GetTXPresentMask: IssueOpcode Error");
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetTXPresentMask: ReceiveResult Error");
				return -3;
			}

			bit_array = (byte)data;

			return 0;
		}

		public static int GetTXEnableMask(out byte bit_array)
		{
			uint error;
			bit_array = 0;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_TX_ENABLE_MASK, 1000))
			{
				Debug.WriteLine("GetTXEnableMask: IssueOpcode Error");
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetTXEnableMask: ReceiveResult Error");
				return -3;
			}

			bit_array = (byte)data;

			return 0;
		}

		public static int SetTXEnableMask(byte bit_array)
		{
			uint param = (uint)bit_array;
			if(SendParam(&param, 4))
			{
				Debug.WriteLine("SetTXEnableMask: SendParam Error");
				return -1;
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_TX_ENABLE_MASK, 1000))
			{
				Debug.WriteLine("SetTXEnableMask: IssueOpcode Error");
				return -2;
			}

			return 0;
		}

		public static int SetDDSChan(int slot, int chan_bit_array) // lower 4 bits indicate chan 3-0 on/off
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			param[1] = (uint)chan_bit_array;
			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetDDSChan: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_DDS_CHAN, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetDDSChan: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetDDSChan: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		private struct I1F1
		{ 
			public int i1;
			public float f1;
		} 

		public static int SetDDSFreq(int slot, float freq)
		{
			I1F1 i1f1 = new I1F1();
			i1f1.i1 = slot;
			i1f1.f1 = freq;

			if(SendParam(&i1f1, 8))
			{
				Debug.WriteLine("SetDDSFreq: SendParam Error");
				return -1;
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_DDS_FREQ, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetDDSFreq: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetDDSFreq: IssueOpcode Error: "+error);
				return -2;
			}
            
			return 0;
		}

		public static int SetDDSAmplitude(int slot, int val) // values valid from 0 to 1023 (hex 0x3ff)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			param[1] = (uint)val;
			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetDDSAmplitude: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_DDS_AMPLITUDE, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetDDSAmplitude: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetDDSAmplitude: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetPreamp(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetPreamp: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_PREAMP_ON, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetPreamp: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetPreamp: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetAttOn(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(uint* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetAttOn: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ATT_ON, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetAttOn: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetAttOn: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetAttVal(int slot, int val) // val is valid 0 to 31
		{
			if(val < 0) val = 0;
			if(val > 31) val = 31;

			uint[] param = new uint[2];
			param[0] = (uint)slot;
			param[1] = (uint)val;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetAttVal: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ATT_VAL, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetAttVal: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetAttVal: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetLED(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetLED: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_LED, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetLED: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetLED: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetFilter(int slot, float freq) // freq in MHz
		{
			I1F1 i1f1 = new I1F1();
			i1f1.i1 = slot;
			i1f1.f1 = freq;

			if(SendParam(&i1f1, 8))
			{
				Debug.WriteLine("SetFilter: SendParam Error");
				return -1;
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_FILTER, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetFilter: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetFilter: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int ReadClockGen(int slot, int reg, out uint val)
		{
			val = 0;
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			param[1] = (uint)reg;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("ReadClockGen: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_READ_CLOCK_GEN, 1000))
			{
				if(error == 100)
					Debug.WriteLine("ReadClockGen: Invalid index ("+slot+")");
				else
					Debug.WriteLine("ReadClockGen: IssueOpcode Error: "+error);
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("ReadClockGen: ReceiveResult Error");
				return -3;
			}

			val = data;
			return 0;
		}

		public static int WriteClockGen(int slot, int reg, uint val)
		{
			uint[] param = new uint[3];
			param[0] = (uint)slot;
			param[1] = (uint)reg;
			param[2] = (uint)val;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 12))
				{
					Debug.WriteLine("WriteClockGen: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_WRITE_CLOCK_GEN, 1000))
			{
				if(error == 100)
					Debug.WriteLine("WriteClockGen: Invalid index ("+slot+")");
				else
					Debug.WriteLine("WriteClockGen: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int ReadDDS(int slot, int chan, int reg, out uint val)
		{
			val = 0;
			uint[] param = new uint[3];
			param[0] = (uint)slot;
			param[1] = (uint)chan;
			param[2] = (uint)reg;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 12))
				{
					Debug.WriteLine("ReadDDS: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_READ_DDS, 1000))
			{
				if(error == 100)
					Debug.WriteLine("ReadDDS: Invalid index ("+slot+")");
				else
					Debug.WriteLine("ReadDDS: IssueOpcode Error: "+error);
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("ReadDDS: ReceiveResult Error");
				return -3;
			}

			val = data;
			return 0;
		}

		public static int WriteDDS(int slot, int chan, int reg, uint val)
		{
			uint[] param = new uint[4];
			param[0] = (uint)slot;
			param[1] = (uint)chan;
			param[2] = (uint)reg;
			param[3] = (uint)val;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 16))
				{
					Debug.WriteLine("WriteDDS: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_WRITE_DDS, 1000))
			{
				if(error == 100)
					Debug.WriteLine("WriteDDS: Invalid index ("+slot+")");
				else
					Debug.WriteLine("WriteDDS: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int UpdateDDS()
		{
			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_UPDATE_DDS, 1000))
			{
				Debug.WriteLine("UpdateDDS: IssueOpcode Error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetTestRelay(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetTestRelay: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_TEST_RELAY, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetTestRealy: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetTestRelay: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetSigGenRelay(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetSigGenRelay: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_SIG_GEN_RELAY, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetSigGenRelay: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetSigGenRelay: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetADCMaster(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetADCMaster: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ADC_MASTER, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetADCMaster: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetADCMaster: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int GetADCCalState(int slot, out bool val)
		{
			val = false;
			uint param = (uint)slot;

			if(SendParam(&param, 4))
			{
				Debug.WriteLine("GetADCCalState: SendParam Error");
				return -1;
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_ADC_CAL_STATE, 1000))
			{
				if(error == 100)
					Debug.WriteLine("GetADCCalState: Invalid index ("+slot+")");
				else
					Debug.WriteLine("GetADCCalState: IssueOpcode Error: "+error);
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetADCCalState: ReceiveResult Error");
				return -3;
			}

			if(data == 1) val = true;
			else val = false;

			return 0;
		}

		public static int SetADCHPF(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetADCHPF: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ADC_HIGH_PASS_FILTER, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetADCHPF: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetADCHPF: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetADCZCAL(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetADCZCAL: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ADC_ZCAL, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetADCZCAL: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetADCZCAL: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetADCReset(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetADCReset: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ADC_RESET, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetADCReset: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetADCReset: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetEnableQSD(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetEnableQSD: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_ENABLE_QSD, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetEnableQSD: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetEnableQSD: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetPowerDown1(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetPowerDown1: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_POWER_DOWN1, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetPowerDown1: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetPowerDown1: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetPowerDown2(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetPowerDown2: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_POWER_DOWN2, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetPowerDown2: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetPowerDown2: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int GetPLLStatus(int slot, out bool val)
		{
			val = false;
			uint param = (uint)slot;

			if(SendParam(&param, 4))
			{
				Debug.WriteLine("GetPLLStatus: SendParam Error");
				return -1;
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_GET_PLL_STATUS, 1000))
			{
				if(error == 100)
					Debug.WriteLine("GetPLLStatus: Invalid index ("+slot+")");
				else
					Debug.WriteLine("GetPLLStatus: IssueOpcode Error: "+error);
				return -2;
			}

			uint data;
			if(ReceiveResult(&data, 4))
			{
				Debug.WriteLine("GetPLLStatus: ReceiveResult Error");
				return -3;
			}

			if(data == 1) val = true;
			else val = false;

			return 0;
		}

		public static int SetDDSMaster(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetDDSMaster: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_DDS_MASTER, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetDDSMaster: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetDDSMaster: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetPLL(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetPLL: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_PLL, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetPLL: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetPLL: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetImpulse(int slot, bool on)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			if(on) param[1] = 1;
			else param[1] = 0;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetImpulse: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_IMPULSE_ON, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetImpulse: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetImpulse: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}

		public static int SetPort(int slot, int val)
		{
			uint[] param = new uint[2];
			param[0] = (uint)slot;
			param[1] = (uint)val;

			fixed(void* ptr = &param[0])
			{
				if(SendParam(ptr, 8))
				{
					Debug.WriteLine("SetPort: SendParam Error");
					return -1;
				}
			}

			uint error;
			if(IssueOpcode(out error, (uint)Opcode.RDAL_OP_SET_PORT, 1000))
			{
				if(error == 100)
					Debug.WriteLine("SetPort: Invalid index ("+slot+")");
				else
					Debug.WriteLine("SetPort: IssueOpcode error: "+error);
				return -2;
			}

			return 0;
		}
*/
		#endregion

		public static int GetSerialNum(out uint num)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_SERIAL_NUM, 0, 0);
			//num = data;
			//return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_SERIAL_NUM, 0, 0, out num);			
		}

		public static int GetModel(out uint model)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_MODEL, 0, 0);
			//model = data;
			//return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_MODEL, 0, 0, out model);
		}

//*****************************************************************************************
		public static int GetFirmwareRev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_FIRMWARE_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_FIRMWARE_REV, 0, 0, out rev);
		}

		public static int Initialize()
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_INITIALIZE, 0, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_INITIALIZE, 0, 0);
		}

		public static int GetTRXOK(out bool b)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_TRX_OK, 0, 0);
//			b = (data != 0 ? true : false);
//			return 0;
			uint val;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_TRX_OK, 0, 0, out val);
			b = Convert.ToBoolean(val);
			return rtn;
		}

		public static int GetTRXRev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_TRX_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_TRX_REV, 0, 0, out rev);
		}

		public static int GetTRXSN(out uint sn)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_TRX_SN, 0, 0);
//			sn = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_TRX_SN, 0, 0, out sn);
		}

		public static int GetPAOK(out bool b)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_PA_OK, 0, 0);
//			b = (data != 0 ? true : false);
//			return 0;
			uint val;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_PA_OK, 0, 0, out val);
			b = Convert.ToBoolean(val);
			return rtn;
		}

		public static int GetPARev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_PA_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_PA_REV, 0, 0, out rev);
		}

		public static int GetPASN(out uint sn)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_PA_SN, 0, 0);
//			sn = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_PA_SN, 0, 0, out sn);
		}

		public static int GetRFIOOK(out bool b)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_RFIO_OK, 0, 0);
//			b = (data != 0 ? true : false);
//			return 0;
			uint val;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_RFIO_OK, 0, 0, out val);
			b = Convert.ToBoolean(val);
			return rtn;
		}

		public static int GetRFIORev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_RFIO_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_RFIO_REV, 0, 0, out rev);
		}

		public static int GetRFIOSN(out uint sn)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_RFIO_SN, 0, 0);
//			sn = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_RFIO_SN, 0, 0, out sn);
		}

		public static int GetATUOK(out bool b)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_ATU_OK, 0, 0);
//			b = (data != 0 ? true: false);
//			return 0;
			uint val;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_ATU_OK, 0, 0, out val);
			b = Convert.ToBoolean(val);
			return rtn;
		}

		public static int GetATURev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_ATU_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_ATU_REV, 0, 0, out rev);
		}

		public static int GetATUSN(out uint sn)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_ATU_SN, 0, 0);
//			sn = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_ATU_SN, 0, 0, out sn);
		}

		public static int GetRX2OK(out bool b)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_RX2_OK, 0, 0);
//			b = (data != 0 ? true : false);
//			return 0;
			uint val;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_RX2_OK, 0, 0, out val);
			b = Convert.ToBoolean(val);
			return rtn;
		}

		public static int GetRX2Rev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_RX2_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_RX2_REV, 0, 0, out rev);
		}

		public static int GetRX2SN(out uint sn)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_RX2_SN, 0, 0);
//			sn = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_RX2_SN, 0, 0, out sn);
		}

		public static int GetVUOK(out bool b)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_XVTR_OK, 0, 0);
//			b = (data != 0 ? true : false);
//			return 0;
			uint val;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_VU_OK, 0, 0, out val);
			b = Convert.ToBoolean(val);
			return rtn;
		}

		public static int GetVURev(out uint rev)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_XVTR_REV, 0, 0);
//			rev = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_VU_REV, 0, 0, out rev);
		}

		public static int GetVUSN(out uint sn)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_XVTR_SN, 0, 0);
//			sn = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_GET_VU_SN, 0, 0, out sn);
		}

		public static int ReadClockReg(int index, out int val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_CLOCK_REG, (uint)index, 0);
//			val = (int)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_CLOCK_REG, (uint)index, 0, out data);
			val = (int)data;
			return rtn;
		}

		public static int WriteClockReg(int index, int val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_CLOCK_REG, (uint)index, (uint)val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_CLOCK_REG, (uint)index, (uint)val);
		}

		public static int ReadTRXDDSReg(int chan, int index, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_TRX_DDS_REG, (uint)chan, (uint)index);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_TRX_DDS_REG, (uint)index, 0, out val);
		}

		public static int WriteTRXDDSReg(int chan, int index, uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_TRX_DDS_REG, (uint)((chan<<8)+index), val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_TRX_DDS_REG, (uint)((chan<<8)+index), val);
		}

		public static int ReadRX2DDSReg(int chan, int index, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_RX2_DDS_REG, (uint)chan, (uint)index);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_RX2_DDS_REG, (uint)chan, (uint)index, out val);
		}

		public static int WriteRX2DDSReg(int chan, int index, uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_RX2_DDS_REG, (uint)((chan<<8)+index), val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_RX2_DDS_REG, (uint)((chan<<8)+index), val);
		}

		public static int WritePIOReg(int index, int reg, int val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_PIO_REG, (uint)((index<<8)+reg), (uint)val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_PIO_REG, (uint)((index<<8)+reg), (uint)val);
		}

		public static int ReadPIOReg(int index, int reg, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PIO_REG, (uint)index, (uint)reg);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_PIO_REG, (uint)index, (uint)reg, out val);
		}

		public static int ReadTRXEEPROMByte(uint offset, out byte buf)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT8, offset, 0);
//			buf = (byte)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT8, offset, 0, out data);
			buf = (byte)data;
			return rtn;
		}

		public static int ReadTRXEEPROMUshort(uint offset, out ushort buf)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT16, offset, 0);
//			buf = (ushort)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT16, offset, 0, out data);
			buf = (ushort)data;
			return rtn;
		}

		public static int ReadTRXEEPROMUint(uint offset, out uint buf)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT32, offset, 0);
//			buf = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT32, offset, 0, out buf);
		}

		public static int ReadTRXEEPROMFloat(uint offset, out float buf)
		{
//			float data = FWCMidi.SendGetMessageFloat(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT32, offset, 0);
//			buf = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_TRX_EEPROM_UINT32, offset, 0, out buf);
		}

		public static int WriteTRXEEPROMByte(uint offset, byte buf)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT8, offset, (uint)buf);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT8, offset, (uint)buf);
		}

		public static int WriteTRXEEPROMUshort(uint offset, ushort buf)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT16, offset, (uint)buf);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT16, offset, (uint)buf);
		}

		public static int WriteTRXEEPROMUint(uint offset, uint buf)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT32, offset, buf);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT32, offset, buf);
		}

		public static int WriteTRXEEPROMFloat(uint offset, float val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT32, offset, val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_TRX_EEPROM_UINT32, offset, val);
		}

		public static int ReadRX2EEPROMByte(uint offset, out byte buf)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT8, offset, 0);
//			buf = (byte)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT8, offset, 0, out data);
			buf = (byte)data;
			return rtn;
		}

		public static int ReadRX2EEPROMUshort(uint offset, out ushort buf)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT16, offset, 0);
//			buf = (ushort)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT16, offset, 0, out data);
			buf = (ushort)data;
			return rtn;
		}

		public static int ReadRX2EEPROMUint(uint offset, out uint buf)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT32, offset, 0);
//			buf = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT32, offset, 0, out buf);
		}

		public static int ReadRX2EEPROMFloat(uint offset, out float buf)
		{
//			float data = FWCMidi.SendGetMessageFloat(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT32, offset, 0);
//			buf = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_READ_RX2_EEPROM_UINT32, offset, 0, out buf);
		}

		public static int WriteRX2EEPROMByte(uint offset, byte buf)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT8, offset, (uint)buf);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT8, offset, (uint)buf);
		}

		public static int WriteRX2EEPROMUshort(uint offset, ushort buf)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT16, offset, (uint)buf);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT16, offset, (uint)buf);
		}

		public static int WriteRX2EEPROMUint(uint offset, uint buf)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT32, offset, buf);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT32, offset, buf);
		}

		public static int WriteRX2EEPROMFloat(uint offset, float val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT32, offset, val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_RX2_EEPROM_UINT32, offset, val);
		}

		public static int SetTRXPot(uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TRX_POT, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TRX_POT, val, 0);
		}

		public static int TRXPotSetRDAC(int index, int val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_TRX_POT_SET_RDAC, (uint)index, (uint)val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_TRX_POT_SET_RDAC, (uint)index, (uint)val);
		}

		public static int TRXPotGetRDAC(out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_TRX_POT_GET_RDAC, 0, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_TRX_POT_GET_RDAC, 0, 0, out val);
		}

		public static int PAPotSetRDAC(int index, int val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_PA_POT_SET_RDAC, (uint)index, (uint)val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_PA_POT_SET_RDAC, (uint)index, (uint)val);
		}

		public static int PAPotGetRDAC(out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_PA_POT_GET_RDAC, 0, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_PA_POT_GET_RDAC, 0, 0, out val);
		}

		public static int SetMux(int chan)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_MUX, (uint)chan, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_MUX, (uint)chan, 0);
		}

		public static int WriteCodecReg(int reg, int val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_WRITE_CODEC_REG, (uint)reg, (uint)val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_WRITE_CODEC_REG, (uint)reg, (uint)val);
		}

		public static int ReadCodecReg(int reg, out int val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_CODEC_REG, (uint)reg, 0);
//			val = (int)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_CODEC_REG, (uint)reg, 0, out data);
			val = (int)data;
			return rtn;
		}

		public static int SetRX1Freq(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_FREQ, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_FREQ, freq, 0);
		}

		public static int SetRX1FreqTW(uint tw, float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_FREQ_TW, tw, freq);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_FREQ_TW, tw, freq);
		}

		public static int SetRX2Freq(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_FREQ, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_FREQ, freq, 0);
		}

		public static int SetRX2FreqTW(uint tw, float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_FREQ_TW, tw, freq);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_FREQ_TW, tw, freq);
		}

		public static int SetTXFreq(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TX_FREQ, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_FREQ, freq, 0);
		}

		public static int SetTXFreqTW(uint tw, float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TX_FREQ_TW, tw, freq);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_FREQ_TW, tw, freq);
		}

		public static int SetTRXPreamp(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TRX_PREAMP, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TRX_PREAMP, Convert.ToUInt32(b), 0);
		}

		public static int SetTest(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TEST, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TEST, Convert.ToUInt32(b), 0);
		}

		public static int SetGen(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_GEN, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_GEN, Convert.ToUInt32(b), 0);
		}

		public static int SetSig(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_SIG, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_SIG, Convert.ToUInt32(b), 0);
		}

		public static int SetImpulse(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_IMPULSE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_IMPULSE, Convert.ToUInt32(b), 0);
		}

		public static int SetXVEN(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XVEN, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XVEN, Convert.ToUInt32(b), 0);
		}

		public static int SetXVTXEN(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XVTXEN, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XVTXEN, Convert.ToUInt32(b), 0);
		}		

		public static int SetQSD(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_QSD, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_QSD, Convert.ToUInt32(b), 0);
		}

		public static int SetQSE(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_QSE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_QSE, Convert.ToUInt32(b), 0);
		}

		public static int SetXREF(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XREF, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XREF, Convert.ToUInt32(b), 0);
		}

		public static int SetRX1Filter(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_FILTER, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_FILTER, freq, 0);
		}

		public static int SetRX2Filter(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_FILTER, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_FILTER, freq, 0);
		}

		public static int SetTXFilter(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TX_FILTER, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_FILTER, freq, 0);
		}

		public static int SetPAFilter(float freq)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_PA_FILTER, freq, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_PA_FILTER, freq, 0);
		}

		public static int SetIntSpkr(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_INT_SPKR, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_INT_SPKR, Convert.ToUInt32(b), 0);
		}

		public static int SetRX1Tap(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_TAP, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_TAP, Convert.ToUInt32(b), 0);
		}

		public static int BypassRX1Filter(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_BYPASS_RX1_FILTER, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_BYPASS_RX1_FILTER, Convert.ToUInt32(b), 0);
		}

		public static int BypassRX2Filter(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_BYPASS_RX2_FILTER, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_BYPASS_RX2_FILTER, Convert.ToUInt32(b), 0);
		}

		public static int BypassTXFilter(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_BYPASS_TX_FILTER, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_BYPASS_TX_FILTER, Convert.ToUInt32(b), 0);
		}

		public static int BypassPAFilter(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_BYPASS_PA_FILTER, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_BYPASS_PA_FILTER, Convert.ToUInt32(b), 0);
		}

		public static int ReadPTT(out bool dot, out bool dash, out bool rca_ptt, out bool mic_ptt)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			dot = ((data & 0x01) == 1);
			dash = ((data & 0x02) == 2);
			rca_ptt = ((data & 0x04) == 4);
			mic_ptt = ((data & 0x08) == 8);
			//Debug.WriteLine("dot: "+dot+" dash: "+dash+" rca: "+rca_ptt+" mic: "+mic_ptt);
			return rtn;
		}

		public static int ReadDot(out bool dot)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			dot = ((data & 0x01) == 1);
			return rtn;
		}

		public static int ReadDash(out bool dash)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			dash = ((data & 0x02) == 2);
			return rtn;
		}

		public static int ReadRCAPTT(out bool rca_ptt)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			rca_ptt = ((data & 0x04) == 4);
			return rtn;
		}

		public static int ReadMicPTT(out bool mic_ptt)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			mic_ptt = ((data & 0x08) == 8);
			return rtn;
		}

		public static int ReadMicDown(out bool mic_down)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			mic_down = ((data & 0x10) == 0x10);
			return rtn;
		}

		public static int ReadMicUp(out bool mic_up)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			mic_up = ((data & 0x20) == 0x20);
			return rtn;
		}

		public static int ReadMicFast(out bool mic_fast)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			mic_fast = ((data & 0x40) == 0x40);
			return rtn;
		}

		public static int ReadPTT(out byte dot, out byte dash, out bool rca_ptt, out bool mic_ptt)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PTT, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PTT, 0, 0, out data);
			if((data & 0x01) == 1) dot = 1; else dot = 0;
			if((data & 0x02) == 2) dash = 1; else dash = 0;
			rca_ptt = ((data & 0x04) == 4);
			mic_ptt = ((data & 0x08) == 8);
			//Debug.WriteLine("dot: "+dot+" dash: "+dash+" rca: "+rca_ptt+" mic: "+mic_ptt);
			return rtn;
		}

		public static int SetHeadphone(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_HEADPHONE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_HEADPHONE, Convert.ToUInt32(b), 0);
		}

		public static int SetPLL(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_PLL, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_PLL, Convert.ToUInt32(b), 0);
		}

		public static int SetRCATX1(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RCA_TX1, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RCA_TX1, Convert.ToUInt32(b), 0);
		}

		public static int SetRCATX2(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RCA_TX2, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RCA_TX2, Convert.ToUInt32(b), 0);
		}

		public static int SetRCATX3(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RCA_TX3, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RCA_TX3, Convert.ToUInt32(b), 0);
		}

		public static int SetFan(bool b)
		{
			//FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_FAN, Convert.ToUInt32(b), 0);
			return 0;
		}

		public static int GetPLLStatus2(out bool b)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_PLL_STATUS2, 0, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_PLL_STATUS2, 0, 0, out data);
			b = (data != 0);

			return rtn;
		}

		public static int ReadPAADC(int chan, out int val)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_READ_PA_ADC, (uint)chan, 0);
//			int data = FWCMidi.ReadADC(chan);
//			val = (int)data;
//			return 0;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_READ_PA_ADC, (uint)chan, 0, out data);
			val = (int)data;
			return rtn;
		}

		public static int SetRX1Loop(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_LOOP, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_LOOP, Convert.ToUInt32(b), 0);
		}

		public static int SetTR(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TR, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TR, Convert.ToUInt32(b), 0);
		}

		public static int SetAnt(int ant)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_ANT, (uint)ant, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_ANT, (uint)ant, 0);
		}

		public static int SetRX1Ant(int ant)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_ANT, Convert.ToUInt32(ant), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_ANT, Convert.ToUInt32(ant), 0);
		}

		public static int SetRX2Ant(int ant)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_ANT, Convert.ToUInt32(ant), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_ANT, Convert.ToUInt32(ant), 0);
		}

		public static int SetTXAnt(int ant)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TX_ANT, Convert.ToUInt32(ant), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_ANT, Convert.ToUInt32(ant), 0);
		}

		public static int SetTXMon(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TXMON, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TXMON, Convert.ToUInt32(b), 0);
		}

		public static int SetXVCOM(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XVCOM, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XVCOM, Convert.ToUInt32(b), 0);
		}

		public static int SetEN2M(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_EN_2M, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_EN_2M, Convert.ToUInt32(b), 0);
		}

		public static int SetKey2M(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_KEY_2M, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_KEY_2M, Convert.ToUInt32(b), 0);
		}

		public static int SetXVTR(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XVTR, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XVTR, Convert.ToUInt32(b), 0);
		}

		public static int SetPABias(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_PA_BIAS, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_PA_BIAS, Convert.ToUInt32(b), 0);
		}

		public static int SetPowerOff()
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_POWER_OFF, 0, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_POWER_OFF, 0, 0);
		}

		public static int SetFPLED(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_FPLED, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_FPLED, Convert.ToUInt32(b), 0);
		}

		public static int SetCTS(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_CTS, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_CTS, Convert.ToUInt32(b), 0);
		}

		public static int SetRTS(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RTS, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RTS, Convert.ToUInt32(b), 0);
		}

		public static int SetPCReset(bool b)
		{
//            FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_PC_RESET, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_PC_RESET, Convert.ToUInt32(b), 0);
		}

		public static int SetPCPWRBT(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_PC_PWRBT, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_PC_PWRBT, Convert.ToUInt32(b), 0);
		}

		public static int SetMOX(bool b)
		{
			//FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_MOX, Convert.ToUInt32(b), 0);
//			FWCMidi.SetMOX(b);
//			return 0;
			//Debug.WriteLine("FWC.SetMOX("+b+")");
			return Pal.WriteOp(Opcode.RDAL_OP_SET_MOX, Convert.ToUInt32(b), 0);
		}

		public static int SetIntLED(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_INT_LED, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_INT_LED, Convert.ToUInt32(b), 0);
		}

		public static int ATUSendCmd(byte b1, byte b2, byte b3)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_ATU_SEND_CMD, (uint)((b1<<8)+b2), b3);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_ATU_SEND_CMD, (uint)((b1<<8)+b2), b3);
		}

		public static int ATUGetResult(out byte cmd, out byte b2, out byte b3, out byte b4, uint timeout_ms)
		{
			//uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_ATU_GET_RESULT, timeout_ms, 0);
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_ATU_GET_RESULT, timeout_ms, 0, out data);
			cmd = (byte)data;
			b2 = (byte)(data>>8);
			b3 = (byte)(data>>16);
			b4 = (byte)(data>>24);
			return rtn;
		}

		public static int SetFullDuplex(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_FULL_DUPLEX, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_FULL_DUPLEX, Convert.ToUInt32(b), 0);
		}

		public static int SetTXDAC(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_TX_DAC, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_DAC, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX1(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX1, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX1, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX2(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX2, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX2, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX3(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX3, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX3, Convert.ToUInt32(b), 0);
		}

		public static int SetXVTRActive(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XVTR_ACTIVE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XVTR_ACTIVE, Convert.ToUInt32(b), 0);
		}

		public static int SetXVTRSplit(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_XVTR_SPLIT, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_XVTR_SPLIT, Convert.ToUInt32(b), 0);
		}

		public static int SetRX1Out(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1OUT, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1OUT, Convert.ToUInt32(b), 0);
		}

		public static int FlexWire_WriteValue(byte addr, byte val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_FLEXWIRE_WRITE_VALUE, addr, val);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_FLEXWIRE_WRITE_VALUE, addr, val);
		}

		public static int FlexWire_Write2Value(byte addr, byte v1, byte v2)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_FLEXWIRE_WRITE_2_VALUE, addr, (uint)((v1<<8)+v2));
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_FLEXWIRE_WRITE_2_VALUE, addr, (uint)((v1<<8)+v2));
		}

		public static int FlexWire_ReadValue(ushort addr, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_FLEXWIRE_READ_VALUE, (uint)addr, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_FLEXWIRE_READ_VALUE, (uint)addr, 0, out val);
		}

		public static int FlexWire_Read2Value(ushort addr, out uint val)
		{
//			uint data = FWCMidi.SendGetMessage(Opcode.RDAL_OP_FLEXWIRE_READ_2_VALUE, (uint)addr, 0);
//			val = data;
//			return 0;
			return Pal.ReadOp(Opcode.RDAL_OP_FLEXWIRE_READ_2_VALUE, (uint)addr, 0, out val);
		}

		public static int SetRX1DSPMode(DSPMode mode)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX1_DSP_MODE, (uint)mode, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX1_DSP_MODE, (uint)mode, 0);
		}

		public static int SetRX2DSPMode(DSPMode mode)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_DSP_MODE, (uint)mode, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_DSP_MODE, (uint)mode, 0);
		}

		public static int SetRX2On(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_ON, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_ON, Convert.ToUInt32(b), 0);
		}

		public static int SetStandby(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_STANDBY, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_STANDBY, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX1DelayEnable(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX1_DELAY_ENABLE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX1_DELAY_ENABLE, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX2DelayEnable(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX2_DELAY_ENABLE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX2_DELAY_ENABLE, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX3DelayEnable(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX3_DELAY_ENABLE, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX3_DELAY_ENABLE, Convert.ToUInt32(b), 0);
		}

		public static int SetAmpTX1Delay(uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX1_DELAY, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX1_DELAY, val, 0);
		}

		public static int SetAmpTX2Delay(uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX2_DELAY, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX2_DELAY, val, 0);
		}

		public static int SetAmpTX3Delay(uint val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_AMP_TX3_DELAY, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_AMP_TX3_DELAY, val, 0);
		}

		public static int ResetRX2DDS()
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_RESET_RX2_DDS, 0, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_RESET_RX2_DDS, 0, 0);
		}

		public static int SetRX2Preamp(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX2_PREAMP, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX2_PREAMP, Convert.ToUInt32(b), 0);
		}

		public static int SetIambic(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_IAMBIC, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_IAMBIC, Convert.ToUInt32(b), 0);
		}

		public static int SetBreakIn(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_BREAK_IN, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_BREAK_IN, Convert.ToUInt32(b), 0);
		}

		public static int SetManualRX1Filter(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_MANUAL_RX1_FILTER, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_MANUAL_RX1_FILTER, Convert.ToUInt32(b), 0);
		}

		public static int SetManualRX2Filter(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_MANUAL_RX2_FILTER, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_MANUAL_RX2_FILTER, Convert.ToUInt32(b), 0);
		}

		public static int SetHiZ(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_HIZ, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_HIZ, Convert.ToUInt32(b), 0);
		}		

		public static int SetPDrvMon(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_PDRVMON, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_PDRVMON, Convert.ToUInt32(b), 0);
		}

		public static int SetATUEnable(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_ENABLE_ATU, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_ENABLE_ATU, Convert.ToUInt32(b), 0);
		}

		public static int SetATUATTN(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_ATU_ATTN, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_ATU_ATTN, Convert.ToUInt32(b), 0);
		}

		public static int SetRXAttn(bool b)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_RX_ATTN, Convert.ToUInt32(b), 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_RX_ATTN, Convert.ToUInt32(b), 0);
		}

		public static int SetFanPWM(int on, int off)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_FAN_PWM, (uint)on, (uint)off);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_FAN_PWM, (uint)on, (uint)off);
		}

		public static int SetFanSpeed(float val)
		{
//			FWCMidi.SendSetMessage(Opcode.RDAL_OP_SET_FAN_SPEED, val, 0);
//			return 0;
			return Pal.WriteOp(Opcode.RDAL_OP_SET_FAN_SPEED, val, 0);
		}

		public static int GetRegion(out FRSRegion region)
		{
//			uint val = FWCMidi.SendGetMessage(Opcode.RDAL_OP_GET_REGION, 0, 0);
//			return (FRSRegion)val;
			uint data;
			int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_REGION, 0, 0, out data);
			region = (FRSRegion)data;
			return rtn;
		}

        public static int SetTXDSPFilter(int low, int high)
        {
            return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_DSP_FILTER, low, high);
        }

        public static int SetTXOffset(int offset)
        {
            return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_OFFSET, offset, 0);
        }

        public static int SetTXDSPMode(DSPMode mode)
        {
            return Pal.WriteOp(Opcode.RDAL_OP_SET_TX_DSP_MODE, (uint)mode, 0);
        }

        public static int SetCWPitch(uint pitch)
        {
            return Pal.WriteOp(Opcode.RDAL_OP_SET_CW_PITCH, pitch, 0);
        }

		public static void SetPalCallback()
		{
			//Pal.SetCallback(callback);
		}

        public static bool GetStatus()
        {
            uint status;
            int rtn = Pal.ReadOp(Opcode.RDAL_OP_GET_STATUS, 0, 0, out status);
            return (status != 0);
        }

        public static int SetVUFanHigh(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_FAN_HIGH, Convert.ToUInt32(b), 0);
        }

        public static int SetVUKeyV(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_KEY_V, Convert.ToUInt32(b), 0);
        }

        public static int SetVUTXIFU(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_TXIFU, Convert.ToUInt32(b), 0);
        }

        public static int SetVUKeyU(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_KEY_U, Convert.ToUInt32(b), 0);
        }

        public static int SetVURXURX2(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_RXURX2, Convert.ToUInt32(b), 0);
        }

        public static int SetVURX2U(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_RX2U, Convert.ToUInt32(b), 0);
        }

        public static int SetVURXIFU(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_RXIFU, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK6(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K6, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK7(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K7, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK8(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K8, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK9(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K9, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK10(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K10, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK11(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K11, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK12(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K12, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK13(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K13, Convert.ToUInt32(b), 0);
        }

        public static int SetVUK14(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_K14, Convert.ToUInt32(b), 0);
        }

        public static int SetVURX2V(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_RX2V, Convert.ToUInt32(b), 0);
        }

        public static int SetVUTXU(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_TX_U, Convert.ToUInt32(b), 0);
        }

        public static int SetVUTXV(bool b)
        {
            return Pal.WriteOp(Opcode.FWC_OP_SET_VU_TX_V, Convert.ToUInt32(b), 0);
        }

		//private static Pal.NotificationCallback callback = new Pal.NotificationCallback(Notify);

        public static bool ignore_dash = false;
		public static Console console;
		public static void Notify(uint bmp)
		{
           // if (console == null || console.Keyer == null) return;
			//Debug.WriteLine("FWCNotify: "+bmp.ToString("X"));
			bool temp = (bmp & 0x01) != 0; // dot
			//if(temp != console.Keyer.FWCDot) console.Keyer.FWCDot = temp;
 
			temp = (bmp & 0x02) != 0; // dash
		//	if(!ignore_dash && temp != console.Keyer.FWCDash) console.Keyer.FWCDash = temp;

			temp = (bmp & 0x04) != 0; // RCA PTT
			if(temp != console.FWCRCAPTT) console.FWCRCAPTT = temp;
 
			temp = (bmp & 0x08) != 0;
			if(temp != console.FWCMicPTT) console.FWCMicPTT = temp;
 
			temp = (bmp & 0x10) != 0;
			if(temp != console.MicDown) console.MicDown = temp;

			temp = (bmp & 0x20) != 0;
			if(temp != console.MicUp) console.MicUp = temp;

			if((bmp & 0x40) != 0) console.MicFast = !console.MicFast;
		}

		#endregion
	}
}

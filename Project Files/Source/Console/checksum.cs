//=================================================================
// checksum.cs
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

namespace PowerSDR
{
    using System;

    /// <summary>
	/// Summary description for checksum.
	/// </summary>
	public class Checksum
	{
		public static bool Match(int[][] table, byte sum, bool only_band_indexes)
		{
			return (Calc(table, only_band_indexes) == sum);
		}

		public static bool Match(float[] table, byte sum)
		{
			return (Calc(table) == sum);
		}

		public static bool Match(float[][] table, byte sum)
		{
			return (Calc(table) == sum);
		}

		public static byte Calc(int[][] table, bool only_band_indexes) // = false for pa bias, true for tx carrier
		{
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M,
							   Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };

			if(!only_band_indexes)
			{
				byte[] data = new byte[table.Length*table[0].Length];
				for(int i=0; i<table.Length; i++)
					for(int j=0; j<table[0].Length; j++)
						data[i*table[0].Length+j] = (byte)table[i][j];
				return Calc(data);
			}
			else
			{
				byte[] data = new byte[bands.Length*table[0].Length];
				for(int i=0; i<bands.Length; i++)
					for(int j=0; j<table[0].Length; j++)
						data[i*table[0].Length+j] = (byte)table[(int)bands[i]][j];
				return Calc(data);
			}
		}
		
		public static byte Calc(float[] table)
		{
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M,
							   Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };

			byte[] data = new byte[bands.Length*4];
			for(int i=0; i<bands.Length; i++)
			{
				byte[] temp = BitConverter.GetBytes(table[(int)bands[i]]);
				for(int j=0; j<4; j++)
					data[i*4+j] = temp[j];
			}
			return Calc(data);
		}

		public static byte Calc(float[][] table)
		{
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M,
							   Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };

			byte[] data = new byte[bands.Length*table[0].Length*4];
			for(int i=0; i<bands.Length; i++)
			{
				for(int j=0; j<table[0].Length; j++)
				{
					byte[] temp = BitConverter.GetBytes(table[(int)bands[i]][j]);
					for(int k=0; k<4; k++)
					{
						data[i*table[0].Length*4+j*4+k] = temp[k];
					}
				}
			}
			return Calc(data);
		}

		public static byte Calc(byte[] data)
		{				
			byte sum = 0;
			for(int i=0; i<data.Length; i++)
			{
				sum ^= data[i];
			}
			return sum;
		}
	}
}

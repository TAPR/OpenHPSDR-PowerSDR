//=================================================================
// PreSelForm.cs
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
using System.Drawing;
using System.Windows.Forms;

namespace PowerSDR
{
    public partial class PreSelForm : Form
    {
        Console console; 

        public PreSelForm(Console c)
        {
            InitializeComponent();
            console = c;
            mox = c.MOX;
            UpdatePreSel();
        }

        private bool mox = false;
        public bool MOX
        {
            get { return mox; }
            set
            {
                mox = value;
                UpdatePreSel();
            }
        }

        private void radBand_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreSel();
        }

        private void tbTune_Scroll(object sender, EventArgs e)
        {
            UpdatePreSel();
        }

        private void radAnt1_CheckedChanged(object sender, EventArgs e)
        {
            if(radAnt1.Checked)
                UpdatePreSel();
        }

        private void radAnt2_CheckedChanged(object sender, EventArgs e)
        {
            if (radAnt2.Checked)
                UpdatePreSel();
        }

        private void radAntRXOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(radAntRXOnly.Checked)
                UpdatePreSel();
        }

        private void chkBypassTR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBypassTR.Checked)
                chkBypassTR.BackColor = console.ButtonSelectedColor;
            else chkBypassTR.BackColor = SystemColors.Control;
            UpdatePreSel();
        }

        private byte ReverseBits(byte b)
        {
            byte temp = 0;
            for (int i = 0; i < 8; i++)
                temp += (byte)(((b >> i) & 0x01) << (7-i));
            return temp;
        }

        private void UpdatePreSel()
        {
            byte reg0=0, reg1=0;

            if (radBandA.Checked)
                reg0 += (1 << 4); // Port 04
            else if (radBandB.Checked)
                reg0 += (1 << 1); // Port 01
            else if (radBandC.Checked)
                reg0 += (1 << 0); // Port 00
            else if (radBandD.Checked)
                reg0 += (1 << 2); // Port 02
            else if (radBandE.Checked)
                reg0 += (1 << 3); // Port 03

            if (radAntRXOnly.Checked)
                reg0 += (1 << 5); // Port 05

            if (radAnt1.Checked)
                reg0 += (1 << 6); // Port 06

            if (mox && !chkBypassTR.Checked)
                reg0 += (1 << 7); // Port 07

            reg1 = ReverseBits((byte)tbTune.Value);
            //Debug.WriteLine("Presel slider: " + ((byte)tbTune.Value).ToString("X") + "  reg1: " + reg1.ToString("X"));

            FWC.FlexWire_Write2Value(0x40, reg0, reg1);
        }

        private void PreSelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}

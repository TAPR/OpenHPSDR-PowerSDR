//=================================================================
// VUForm.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2004-2010  FlexRadio Systems
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
using System.Windows.Forms;

namespace PowerSDR
{
    public partial class VUForm : Form
    {
        public VUForm()
        {
            InitializeComponent();
        }

        private void chkFanHigh_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUFanHigh(chkVUFanHigh.Checked);
        }

        private void chkVUKeyV_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUKeyV(chkVUKeyV.Checked);
        }

        private void chkVUTXIFU_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUTXIFU(chkVUTXIFU.Checked);
        }

        private void chkVUKeyU_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUKeyU(chkVUKeyU.Checked);
        }

        private void chkVURXURX2_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVURXURX2(chkVURXURX2.Checked);
        }

        private void chkVURX2U_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVURX2U(chkVURX2U.Checked);
        }

        private void chkVURXIFU_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVURXIFU(chkVURXIFU.Checked);
        }

        private void chkVURX2V_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVURX2V(chkVURX2V.Checked);
        }

        private void chkVUTXU_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUTXU(chkVUTXU.Checked);
        }

        private void chkVUTXV_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUTXV(chkVUTXV.Checked);
        }

        private void chkVUK6_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK6(chkVUK6.Checked);
        }

        private void chkVUK7_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK7(chkVUK7.Checked);
        }

        private void chkVUK8_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK8(chkVUK8.Checked);
        }

        private void chkVUK9_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK9(chkVUK9.Checked);
        }

        private void chkVUK10_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK10(chkVUK10.Checked);
        }

        private void chkVUK11_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK11(chkVUK11.Checked);
        }

        private void chkVUK12_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK12(chkVUK12.Checked);
        }

        private void chkVUK13_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK13(chkVUK13.Checked);
        }

        private void chkVUK14_CheckedChanged(object sender, EventArgs e)
        {
            FWC.SetVUK14(chkVUK14.Checked);
        }

        private void VUForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}

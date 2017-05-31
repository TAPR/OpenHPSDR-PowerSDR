//=================================================================
// PreSelForm.Designer.cs
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
    partial class PreSelForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreSelForm));
            this.chkBypassTR = new System.Windows.Forms.CheckBoxTS();
            this.grpAnt = new System.Windows.Forms.GroupBoxTS();
            this.radAntRXOnly = new System.Windows.Forms.RadioButtonTS();
            this.radAnt2 = new System.Windows.Forms.RadioButtonTS();
            this.radAnt1 = new System.Windows.Forms.RadioButtonTS();
            this.tbTune = new System.Windows.Forms.TrackBarTS();
            this.grpBand = new System.Windows.Forms.GroupBoxTS();
            this.radBandE = new System.Windows.Forms.RadioButtonTS();
            this.radBandD = new System.Windows.Forms.RadioButtonTS();
            this.radBandC = new System.Windows.Forms.RadioButtonTS();
            this.radBandB = new System.Windows.Forms.RadioButtonTS();
            this.radBandA = new System.Windows.Forms.RadioButtonTS();
            this.grpAnt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTune)).BeginInit();
            this.grpBand.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBypassTR
            // 
            this.chkBypassTR.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBypassTR.Image = null;
            this.chkBypassTR.Location = new System.Drawing.Point(165, 143);
            this.chkBypassTR.Name = "chkBypassTR";
            this.chkBypassTR.Size = new System.Drawing.Size(69, 23);
            this.chkBypassTR.TabIndex = 4;
            this.chkBypassTR.Text = "Bypass TR";
            this.chkBypassTR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBypassTR.UseVisualStyleBackColor = true;
            this.chkBypassTR.CheckedChanged += new System.EventHandler(this.chkBypassTR_CheckedChanged);
            // 
            // grpAnt
            // 
            this.grpAnt.Controls.Add(this.radAntRXOnly);
            this.grpAnt.Controls.Add(this.radAnt2);
            this.grpAnt.Controls.Add(this.radAnt1);
            this.grpAnt.Location = new System.Drawing.Point(150, 14);
            this.grpAnt.Name = "grpAnt";
            this.grpAnt.Size = new System.Drawing.Size(99, 97);
            this.grpAnt.TabIndex = 3;
            this.grpAnt.TabStop = false;
            this.grpAnt.Text = "Antenna";
            // 
            // radAntRXOnly
            // 
            this.radAntRXOnly.AutoSize = true;
            this.radAntRXOnly.Image = null;
            this.radAntRXOnly.Location = new System.Drawing.Point(15, 74);
            this.radAntRXOnly.Name = "radAntRXOnly";
            this.radAntRXOnly.Size = new System.Drawing.Size(64, 17);
            this.radAntRXOnly.TabIndex = 4;
            this.radAntRXOnly.Text = "RX Only";
            this.radAntRXOnly.UseVisualStyleBackColor = true;
            this.radAntRXOnly.CheckedChanged += new System.EventHandler(this.radAntRXOnly_CheckedChanged);
            // 
            // radAnt2
            // 
            this.radAnt2.AutoSize = true;
            this.radAnt2.Image = null;
            this.radAnt2.Location = new System.Drawing.Point(15, 51);
            this.radAnt2.Name = "radAnt2";
            this.radAnt2.Size = new System.Drawing.Size(53, 17);
            this.radAnt2.TabIndex = 3;
            this.radAnt2.Text = "ANT2";
            this.radAnt2.UseVisualStyleBackColor = true;
            this.radAnt2.CheckedChanged += new System.EventHandler(this.radAnt2_CheckedChanged);
            // 
            // radAnt1
            // 
            this.radAnt1.AutoSize = true;
            this.radAnt1.Checked = true;
            this.radAnt1.Image = null;
            this.radAnt1.Location = new System.Drawing.Point(15, 28);
            this.radAnt1.Name = "radAnt1";
            this.radAnt1.Size = new System.Drawing.Size(53, 17);
            this.radAnt1.TabIndex = 2;
            this.radAnt1.TabStop = true;
            this.radAnt1.Text = "ANT1";
            this.radAnt1.UseVisualStyleBackColor = true;
            this.radAnt1.CheckedChanged += new System.EventHandler(this.radAnt1_CheckedChanged);
            // 
            // tbTune
            // 
            this.tbTune.LargeChange = 16;
            this.tbTune.Location = new System.Drawing.Point(12, 172);
            this.tbTune.Maximum = 255;
            this.tbTune.Name = "tbTune";
            this.tbTune.Size = new System.Drawing.Size(132, 45);
            this.tbTune.TabIndex = 1;
            this.tbTune.TickFrequency = 32;
            this.tbTune.Value = 128;
            this.tbTune.Scroll += new System.EventHandler(this.tbTune_Scroll);
            // 
            // grpBand
            // 
            this.grpBand.Controls.Add(this.radBandE);
            this.grpBand.Controls.Add(this.radBandD);
            this.grpBand.Controls.Add(this.radBandC);
            this.grpBand.Controls.Add(this.radBandB);
            this.grpBand.Controls.Add(this.radBandA);
            this.grpBand.Location = new System.Drawing.Point(12, 12);
            this.grpBand.Name = "grpBand";
            this.grpBand.Size = new System.Drawing.Size(132, 154);
            this.grpBand.TabIndex = 0;
            this.grpBand.TabStop = false;
            this.grpBand.Text = "Bands";
            // 
            // radBandE
            // 
            this.radBandE.AutoSize = true;
            this.radBandE.Image = null;
            this.radBandE.Location = new System.Drawing.Point(11, 122);
            this.radBandE.Name = "radBandE";
            this.radBandE.Size = new System.Drawing.Size(99, 17);
            this.radBandE.TabIndex = 4;
            this.radBandE.Text = "E (16 - 30 MHz)";
            this.radBandE.UseVisualStyleBackColor = true;
            this.radBandE.CheckedChanged += new System.EventHandler(this.radBand_CheckedChanged);
            // 
            // radBandD
            // 
            this.radBandD.AutoSize = true;
            this.radBandD.Image = null;
            this.radBandD.Location = new System.Drawing.Point(11, 99);
            this.radBandD.Name = "radBandD";
            this.radBandD.Size = new System.Drawing.Size(118, 17);
            this.radBandD.TabIndex = 3;
            this.radBandD.Text = "D (10.5 - 21.5 MHz)";
            this.radBandD.UseVisualStyleBackColor = true;
            this.radBandD.CheckedChanged += new System.EventHandler(this.radBand_CheckedChanged);
            // 
            // radBandC
            // 
            this.radBandC.AutoSize = true;
            this.radBandC.Image = null;
            this.radBandC.Location = new System.Drawing.Point(11, 76);
            this.radBandC.Name = "radBandC";
            this.radBandC.Size = new System.Drawing.Size(108, 17);
            this.radBandC.TabIndex = 2;
            this.radBandC.Text = "C (5 - 10.15 MHz)";
            this.radBandC.UseVisualStyleBackColor = true;
            this.radBandC.CheckedChanged += new System.EventHandler(this.radBand_CheckedChanged);
            // 
            // radBandB
            // 
            this.radBandB.AutoSize = true;
            this.radBandB.Image = null;
            this.radBandB.Location = new System.Drawing.Point(11, 53);
            this.radBandB.Name = "radBandB";
            this.radBandB.Size = new System.Drawing.Size(99, 17);
            this.radBandB.TabIndex = 1;
            this.radBandB.Text = "B ( 3 - 5.5 MHz)";
            this.radBandB.UseVisualStyleBackColor = true;
            this.radBandB.CheckedChanged += new System.EventHandler(this.radBand_CheckedChanged);
            // 
            // radBandA
            // 
            this.radBandA.AutoSize = true;
            this.radBandA.Checked = true;
            this.radBandA.Image = null;
            this.radBandA.Location = new System.Drawing.Point(11, 30);
            this.radBandA.Name = "radBandA";
            this.radBandA.Size = new System.Drawing.Size(96, 17);
            this.radBandA.TabIndex = 0;
            this.radBandA.TabStop = true;
            this.radBandA.Text = "A (1.8 - 3 MHz)";
            this.radBandA.UseVisualStyleBackColor = true;
            this.radBandA.CheckedChanged += new System.EventHandler(this.radBand_CheckedChanged);
            // 
            // PreSelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 224);
            this.Controls.Add(this.chkBypassTR);
            this.Controls.Add(this.grpAnt);
            this.Controls.Add(this.tbTune);
            this.Controls.Add(this.grpBand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreSelForm";
            this.Text = "Preselector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreSelForm_FormClosing);
            this.grpAnt.ResumeLayout(false);
            this.grpAnt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTune)).EndInit();
            this.grpBand.ResumeLayout(false);
            this.grpBand.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBoxTS grpBand;
        private System.Windows.Forms.RadioButtonTS radBandA;
        private System.Windows.Forms.RadioButtonTS radBandE;
        private System.Windows.Forms.RadioButtonTS radBandD;
        private System.Windows.Forms.RadioButtonTS radBandC;
        private System.Windows.Forms.RadioButtonTS radBandB;
        private System.Windows.Forms.TrackBarTS tbTune;
        private System.Windows.Forms.RadioButtonTS radAnt1;
        private System.Windows.Forms.GroupBoxTS grpAnt;
        private System.Windows.Forms.RadioButtonTS radAnt2;
        private System.Windows.Forms.CheckBoxTS chkBypassTR;
        private System.Windows.Forms.RadioButtonTS radAntRXOnly;
    }
}
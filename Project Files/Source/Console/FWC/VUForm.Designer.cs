//=================================================================
// VUForm.Designer.cs
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

namespace PowerSDR
{
    partial class VUForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkVUFanHigh = new System.Windows.Forms.CheckBox();
            this.chkVUKeyV = new System.Windows.Forms.CheckBox();
            this.chkVUTXIFU = new System.Windows.Forms.CheckBox();
            this.chkVUKeyU = new System.Windows.Forms.CheckBox();
            this.chkVURXURX2 = new System.Windows.Forms.CheckBox();
            this.chkVURX2U = new System.Windows.Forms.CheckBox();
            this.chkVURXIFU = new System.Windows.Forms.CheckBox();
            this.chkVUK6 = new System.Windows.Forms.CheckBox();
            this.chkVUK7 = new System.Windows.Forms.CheckBox();
            this.chkVUK8 = new System.Windows.Forms.CheckBox();
            this.chkVUK9 = new System.Windows.Forms.CheckBox();
            this.chkVUK10 = new System.Windows.Forms.CheckBox();
            this.chkVUK11 = new System.Windows.Forms.CheckBox();
            this.chkVUK12 = new System.Windows.Forms.CheckBox();
            this.chkVUK13 = new System.Windows.Forms.CheckBox();
            this.chkVUK14 = new System.Windows.Forms.CheckBox();
            this.chkVURX2V = new System.Windows.Forms.CheckBox();
            this.chkVUTXU = new System.Windows.Forms.CheckBox();
            this.chkVUTXV = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkVUFanHigh
            // 
            this.chkVUFanHigh.AutoSize = true;
            this.chkVUFanHigh.Location = new System.Drawing.Point(33, 32);
            this.chkVUFanHigh.Name = "chkVUFanHigh";
            this.chkVUFanHigh.Size = new System.Drawing.Size(69, 17);
            this.chkVUFanHigh.TabIndex = 0;
            this.chkVUFanHigh.Text = "Fan High";
            this.chkVUFanHigh.UseVisualStyleBackColor = true;
            this.chkVUFanHigh.CheckedChanged += new System.EventHandler(this.chkFanHigh_CheckedChanged);
            // 
            // chkVUKeyV
            // 
            this.chkVUKeyV.AutoSize = true;
            this.chkVUKeyV.Location = new System.Drawing.Point(33, 55);
            this.chkVUKeyV.Name = "chkVUKeyV";
            this.chkVUKeyV.Size = new System.Drawing.Size(54, 17);
            this.chkVUKeyV.TabIndex = 1;
            this.chkVUKeyV.Text = "Key-V";
            this.chkVUKeyV.UseVisualStyleBackColor = true;
            this.chkVUKeyV.CheckedChanged += new System.EventHandler(this.chkVUKeyV_CheckedChanged);
            // 
            // chkVUTXIFU
            // 
            this.chkVUTXIFU.AutoSize = true;
            this.chkVUTXIFU.Location = new System.Drawing.Point(33, 78);
            this.chkVUTXIFU.Name = "chkVUTXIFU";
            this.chkVUTXIFU.Size = new System.Drawing.Size(57, 17);
            this.chkVUTXIFU.TabIndex = 2;
            this.chkVUTXIFU.Text = "TXIFU";
            this.chkVUTXIFU.UseVisualStyleBackColor = true;
            // 
            // chkVUKeyU
            // 
            this.chkVUKeyU.AutoSize = true;
            this.chkVUKeyU.Location = new System.Drawing.Point(33, 101);
            this.chkVUKeyU.Name = "chkVUKeyU";
            this.chkVUKeyU.Size = new System.Drawing.Size(55, 17);
            this.chkVUKeyU.TabIndex = 3;
            this.chkVUKeyU.Text = "Key-U";
            this.chkVUKeyU.UseVisualStyleBackColor = true;
            this.chkVUKeyU.CheckedChanged += new System.EventHandler(this.chkVUKeyU_CheckedChanged);
            // 
            // chkVURXURX2
            // 
            this.chkVURXURX2.AutoSize = true;
            this.chkVURXURX2.Location = new System.Drawing.Point(33, 124);
            this.chkVURXURX2.Name = "chkVURXURX2";
            this.chkVURXURX2.Size = new System.Drawing.Size(70, 17);
            this.chkVURXURX2.TabIndex = 4;
            this.chkVURXURX2.Text = "RXURX2";
            this.chkVURXURX2.UseVisualStyleBackColor = true;
            this.chkVURXURX2.CheckedChanged += new System.EventHandler(this.chkVURXURX2_CheckedChanged);
            // 
            // chkVURX2U
            // 
            this.chkVURX2U.AutoSize = true;
            this.chkVURX2U.Location = new System.Drawing.Point(33, 147);
            this.chkVURX2U.Name = "chkVURX2U";
            this.chkVURX2U.Size = new System.Drawing.Size(55, 17);
            this.chkVURX2U.TabIndex = 5;
            this.chkVURX2U.Text = "RX2U";
            this.chkVURX2U.UseVisualStyleBackColor = true;
            this.chkVURX2U.CheckedChanged += new System.EventHandler(this.chkVURX2U_CheckedChanged);
            // 
            // chkVURXIFU
            // 
            this.chkVURXIFU.AutoSize = true;
            this.chkVURXIFU.Location = new System.Drawing.Point(33, 170);
            this.chkVURXIFU.Name = "chkVURXIFU";
            this.chkVURXIFU.Size = new System.Drawing.Size(58, 17);
            this.chkVURXIFU.TabIndex = 6;
            this.chkVURXIFU.Text = "RXIFU";
            this.chkVURXIFU.UseVisualStyleBackColor = true;
            this.chkVURXIFU.CheckedChanged += new System.EventHandler(this.chkVURXIFU_CheckedChanged);
            // 
            // chkVUK6
            // 
            this.chkVUK6.AutoSize = true;
            this.chkVUK6.Location = new System.Drawing.Point(119, 32);
            this.chkVUK6.Name = "chkVUK6";
            this.chkVUK6.Size = new System.Drawing.Size(39, 17);
            this.chkVUK6.TabIndex = 7;
            this.chkVUK6.Text = "K6";
            this.chkVUK6.UseVisualStyleBackColor = true;
            this.chkVUK6.CheckedChanged += new System.EventHandler(this.chkVUK6_CheckedChanged);
            // 
            // chkVUK7
            // 
            this.chkVUK7.AutoSize = true;
            this.chkVUK7.Location = new System.Drawing.Point(119, 55);
            this.chkVUK7.Name = "chkVUK7";
            this.chkVUK7.Size = new System.Drawing.Size(39, 17);
            this.chkVUK7.TabIndex = 8;
            this.chkVUK7.Text = "K7";
            this.chkVUK7.UseVisualStyleBackColor = true;
            this.chkVUK7.CheckedChanged += new System.EventHandler(this.chkVUK7_CheckedChanged);
            // 
            // chkVUK8
            // 
            this.chkVUK8.AutoSize = true;
            this.chkVUK8.Location = new System.Drawing.Point(119, 78);
            this.chkVUK8.Name = "chkVUK8";
            this.chkVUK8.Size = new System.Drawing.Size(39, 17);
            this.chkVUK8.TabIndex = 9;
            this.chkVUK8.Text = "K8";
            this.chkVUK8.UseVisualStyleBackColor = true;
            this.chkVUK8.CheckedChanged += new System.EventHandler(this.chkVUK8_CheckedChanged);
            // 
            // chkVUK9
            // 
            this.chkVUK9.AutoSize = true;
            this.chkVUK9.Location = new System.Drawing.Point(119, 101);
            this.chkVUK9.Name = "chkVUK9";
            this.chkVUK9.Size = new System.Drawing.Size(63, 17);
            this.chkVUK9.TabIndex = 10;
            this.chkVUK9.Text = "K9 (ruff)";
            this.chkVUK9.UseVisualStyleBackColor = true;
            this.chkVUK9.CheckedChanged += new System.EventHandler(this.chkVUK9_CheckedChanged);
            // 
            // chkVUK10
            // 
            this.chkVUK10.AutoSize = true;
            this.chkVUK10.Location = new System.Drawing.Point(119, 124);
            this.chkVUK10.Name = "chkVUK10";
            this.chkVUK10.Size = new System.Drawing.Size(45, 17);
            this.chkVUK10.TabIndex = 11;
            this.chkVUK10.Text = "K10";
            this.chkVUK10.UseVisualStyleBackColor = true;
            this.chkVUK10.CheckedChanged += new System.EventHandler(this.chkVUK10_CheckedChanged);
            // 
            // chkVUK11
            // 
            this.chkVUK11.AutoSize = true;
            this.chkVUK11.Location = new System.Drawing.Point(119, 147);
            this.chkVUK11.Name = "chkVUK11";
            this.chkVUK11.Size = new System.Drawing.Size(45, 17);
            this.chkVUK11.TabIndex = 12;
            this.chkVUK11.Text = "K11";
            this.chkVUK11.UseVisualStyleBackColor = true;
            this.chkVUK11.CheckedChanged += new System.EventHandler(this.chkVUK11_CheckedChanged);
            // 
            // chkVUK12
            // 
            this.chkVUK12.AutoSize = true;
            this.chkVUK12.Location = new System.Drawing.Point(119, 170);
            this.chkVUK12.Name = "chkVUK12";
            this.chkVUK12.Size = new System.Drawing.Size(45, 17);
            this.chkVUK12.TabIndex = 13;
            this.chkVUK12.Text = "K12";
            this.chkVUK12.UseVisualStyleBackColor = true;
            this.chkVUK12.CheckedChanged += new System.EventHandler(this.chkVUK12_CheckedChanged);
            // 
            // chkVUK13
            // 
            this.chkVUK13.AutoSize = true;
            this.chkVUK13.Location = new System.Drawing.Point(119, 193);
            this.chkVUK13.Name = "chkVUK13";
            this.chkVUK13.Size = new System.Drawing.Size(45, 17);
            this.chkVUK13.TabIndex = 14;
            this.chkVUK13.Text = "K13";
            this.chkVUK13.UseVisualStyleBackColor = true;
            this.chkVUK13.CheckedChanged += new System.EventHandler(this.chkVUK13_CheckedChanged);
            // 
            // chkVUK14
            // 
            this.chkVUK14.AutoSize = true;
            this.chkVUK14.Location = new System.Drawing.Point(119, 216);
            this.chkVUK14.Name = "chkVUK14";
            this.chkVUK14.Size = new System.Drawing.Size(45, 17);
            this.chkVUK14.TabIndex = 15;
            this.chkVUK14.Text = "K14";
            this.chkVUK14.UseVisualStyleBackColor = true;
            this.chkVUK14.CheckedChanged += new System.EventHandler(this.chkVUK14_CheckedChanged);
            // 
            // chkVURX2V
            // 
            this.chkVURX2V.AutoSize = true;
            this.chkVURX2V.Location = new System.Drawing.Point(33, 193);
            this.chkVURX2V.Name = "chkVURX2V";
            this.chkVURX2V.Size = new System.Drawing.Size(54, 17);
            this.chkVURX2V.TabIndex = 16;
            this.chkVURX2V.Text = "RX2V";
            this.chkVURX2V.UseVisualStyleBackColor = true;
            this.chkVURX2V.CheckedChanged += new System.EventHandler(this.chkVURX2V_CheckedChanged);
            // 
            // chkVUTXU
            // 
            this.chkVUTXU.AutoSize = true;
            this.chkVUTXU.Location = new System.Drawing.Point(33, 216);
            this.chkVUTXU.Name = "chkVUTXU";
            this.chkVUTXU.Size = new System.Drawing.Size(51, 17);
            this.chkVUTXU.TabIndex = 17;
            this.chkVUTXU.Text = "TX-U";
            this.chkVUTXU.UseVisualStyleBackColor = true;
            this.chkVUTXU.CheckedChanged += new System.EventHandler(this.chkVUTXU_CheckedChanged);
            // 
            // chkVUTXV
            // 
            this.chkVUTXV.AutoSize = true;
            this.chkVUTXV.Location = new System.Drawing.Point(33, 239);
            this.chkVUTXV.Name = "chkVUTXV";
            this.chkVUTXV.Size = new System.Drawing.Size(50, 17);
            this.chkVUTXV.TabIndex = 18;
            this.chkVUTXV.Text = "TX-V";
            this.chkVUTXV.UseVisualStyleBackColor = true;
            this.chkVUTXV.CheckedChanged += new System.EventHandler(this.chkVUTXV_CheckedChanged);
            // 
            // VUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 325);
            this.Controls.Add(this.chkVUTXV);
            this.Controls.Add(this.chkVUTXU);
            this.Controls.Add(this.chkVURX2V);
            this.Controls.Add(this.chkVUK14);
            this.Controls.Add(this.chkVUK13);
            this.Controls.Add(this.chkVUK12);
            this.Controls.Add(this.chkVUK11);
            this.Controls.Add(this.chkVUK10);
            this.Controls.Add(this.chkVUK9);
            this.Controls.Add(this.chkVUK8);
            this.Controls.Add(this.chkVUK7);
            this.Controls.Add(this.chkVUK6);
            this.Controls.Add(this.chkVURXIFU);
            this.Controls.Add(this.chkVURX2U);
            this.Controls.Add(this.chkVURXURX2);
            this.Controls.Add(this.chkVUKeyU);
            this.Controls.Add(this.chkVUTXIFU);
            this.Controls.Add(this.chkVUKeyV);
            this.Controls.Add(this.chkVUFanHigh);
            this.Name = "VUForm";
            this.Text = "VUForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VUForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkVUFanHigh;
        private System.Windows.Forms.CheckBox chkVUKeyV;
        private System.Windows.Forms.CheckBox chkVUTXIFU;
        private System.Windows.Forms.CheckBox chkVUKeyU;
        private System.Windows.Forms.CheckBox chkVURXURX2;
        private System.Windows.Forms.CheckBox chkVURX2U;
        private System.Windows.Forms.CheckBox chkVURXIFU;
        private System.Windows.Forms.CheckBox chkVUK6;
        private System.Windows.Forms.CheckBox chkVUK7;
        private System.Windows.Forms.CheckBox chkVUK8;
        private System.Windows.Forms.CheckBox chkVUK9;
        private System.Windows.Forms.CheckBox chkVUK10;
        private System.Windows.Forms.CheckBox chkVUK11;
        private System.Windows.Forms.CheckBox chkVUK12;
        private System.Windows.Forms.CheckBox chkVUK13;
        private System.Windows.Forms.CheckBox chkVUK14;
        private System.Windows.Forms.CheckBox chkVURX2V;
        private System.Windows.Forms.CheckBox chkVUTXU;
        private System.Windows.Forms.CheckBox chkVUTXV;
    }
}
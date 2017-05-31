//=================================================================
// FLEX5000ProdTestForm.cs
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
	unsafe public class FLEX5000ProdTestForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Progress p;
		private Console console;
		private System.Windows.Forms.ButtonTS btnPLL;
		private System.Windows.Forms.ButtonTS btnRXFilter;
		private System.Windows.Forms.GroupBoxTS grpReceiver;
		private System.Windows.Forms.ButtonTS btnRXLevel;
		private System.Windows.Forms.GroupBoxTS grpGeneral;
		private System.Windows.Forms.GroupBoxTS grpTransmitter;
		private System.Windows.Forms.ButtonTS btnTXImage;
		private System.Windows.Forms.ButtonTS btnTXCarrier;
		private System.Windows.Forms.ButtonTS btnNoise;
		private System.Windows.Forms.ButtonTS btnTXFilter;
		private System.Windows.Forms.ButtonTS btnImpulse;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.GroupBoxTS grpBands;
		private System.Windows.Forms.ButtonTS btnClearAll;
		private System.Windows.Forms.ButtonTS btnCheckAll;
		private System.Windows.Forms.ButtonTS btnRXImage;
		private System.Windows.Forms.ListBox lstDebug;
		private System.Windows.Forms.LabelTS lblTech;
		private System.Windows.Forms.TextBoxTS txtTech;
		private System.Windows.Forms.ButtonTS btnPrintReport;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.ButtonTS btnRunSelectedTests;
		private System.Windows.Forms.GroupBoxTS grpTestGeneral;
		private System.Windows.Forms.GroupBoxTS grpTestReceiver;
		private System.Windows.Forms.GroupBoxTS grpTestTransmitter;
		private System.Windows.Forms.ButtonTS btnTestGenAll;
		private System.Windows.Forms.ButtonTS btnTestGenNone;
		private System.Windows.Forms.ButtonTS btnTestRXNone;
		private System.Windows.Forms.ButtonTS btnTestRXAll;
		private System.Windows.Forms.ButtonTS btnTestTXNone;
		private System.Windows.Forms.ButtonTS btnTestTXAll;
		private System.Windows.Forms.ButtonTS btnTestNone;
		private System.Windows.Forms.ButtonTS btnTestAll;
		private System.Windows.Forms.GroupBoxTS grpIO;
		private System.Windows.Forms.ButtonTS btnIORunAll;
		private System.Windows.Forms.ButtonTS btnIOExtRef;
		private System.Windows.Forms.ButtonTS btnIOHeadphone;
		private System.Windows.Forms.ButtonTS btnIOMicPTT;
		private System.Windows.Forms.ButtonTS btnIORCAPTT;
		private System.Windows.Forms.ButtonTS btnIODash;
		private System.Windows.Forms.ButtonTS btnIODot;
		private System.Windows.Forms.ButtonTS btnIOFWInOut;
		private System.Windows.Forms.ButtonTS btnIORCAInOut;
		private System.Windows.Forms.ButtonTS btnIOPwrSpkr;
		private System.Windows.Forms.ButtonTS btnGenPreamp;
		private System.Windows.Forms.ButtonTS btnGenBal;
		private System.Windows.Forms.CheckBoxTS ck6;
		private System.Windows.Forms.CheckBoxTS ck10;
		private System.Windows.Forms.CheckBoxTS ck12;
		private System.Windows.Forms.CheckBoxTS ck15;
		private System.Windows.Forms.CheckBoxTS ck17;
		private System.Windows.Forms.CheckBoxTS ck20;
		private System.Windows.Forms.CheckBoxTS ck30;
		private System.Windows.Forms.CheckBoxTS ck40;
		private System.Windows.Forms.CheckBoxTS ck60;
		private System.Windows.Forms.CheckBoxTS ck80;
		private System.Windows.Forms.CheckBoxTS ck160;
		private System.Windows.Forms.CheckBoxTS ckTestGenPreamp;
		private System.Windows.Forms.CheckBoxTS ckTestGenImpulse;
		private System.Windows.Forms.CheckBoxTS ckTestGenNoise;
		private System.Windows.Forms.CheckBoxTS ckTestGenBal;
		private System.Windows.Forms.CheckBoxTS ckTestGenPLL;
		private System.Windows.Forms.CheckBoxTS ckTestRXMDS;
		private System.Windows.Forms.CheckBoxTS ckTestRXImage;
		private System.Windows.Forms.CheckBoxTS ckTestRXLevel;
		private System.Windows.Forms.CheckBoxTS ckTestRXFilter;
		private System.Windows.Forms.CheckBoxTS ckTestTXImage;
		private System.Windows.Forms.CheckBoxTS ckTestTXCarrier;
		private System.Windows.Forms.CheckBoxTS ckTestTXFilter;
		private System.Windows.Forms.ButtonTS btnPostFence;
		private System.Windows.Forms.ButtonTS btnTXGain;
		private System.Windows.Forms.CheckBoxTS ckTestTXGain;
		private System.Windows.Forms.NumericUpDown udLevel;
		private System.Windows.Forms.ButtonTS btnGenATTN;
		private System.Windows.Forms.CheckBoxTS ckTestGenATTN;
		private System.Windows.Forms.ButtonTS btnIOMicUp;
		private System.Windows.Forms.ButtonTS btnIOMicDown;
		private System.Windows.Forms.ButtonTS btnIOMicFast;
        private ButtonTS btnTXSpur;
		private System.ComponentModel.IContainer components;

		#endregion
		
		#region Constructor and Destructor

		public FLEX5000ProdTestForm(Console c)
		{
			InitializeComponent();
			console = c;
			this.Text += "  (TRX: "+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+")";
			Common.RestoreForm(this, "FLEX5000ProdTestForm", false);

			if(FWCEEPROM.TRXSerial == 0)
			{
				MessageBox.Show("No TRX Serial Found.  Please enter and try again.",
					"No TRX S/N Found",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				this.Enabled = false;
			}

			if(console.SampleRate1 < 96000)
				MessageBox.Show("Warning: Sample Rate should be at least 96kHz before calibrating.",
					"Warning: Sample Rate Low",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

			if(console.SetupForm.DSPPhoneRXBuffer != 4096)
				/*MessageBox.Show("Warning: DSP RX Buffer size should be at least 4096 before calibrating.",
					"Warning: DSP RX Buffer Size Low",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);*/
				console.SetupForm.DSPPhoneRXBuffer = 4096;

			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					udLevel.Value = -24.0m;
					break;
				case Model.FLEX3000:
					btnPLL.Visible = false;
					ckTestGenPLL.Visible = false;
					ckTestGenPLL.Checked = false;
					btnGenATTN.Visible = true;
					ckTestGenATTN.Visible = true;
					ckTestGenATTN.Checked = true;
					btnImpulse.Visible = false;
					ckTestGenImpulse.Visible = false;
					ckTestGenImpulse.Checked = false;
					btnTXFilter.Visible = false;
					ckTestTXFilter.Visible = false;
					ckTestTXFilter.Checked = false;
					this.Text = this.Text.Replace("FLEX-5000", "FLEX-3000");
					btnIOExtRef.Visible = false;
                    btnIORCAInOut.Visible = false;
					btnIOMicUp.Visible = true;
					btnIOMicDown.Visible = true;
					btnIOMicFast.Visible = true;
					btnPostFence.Visible = false;
					udLevel.Value = -37.4m;
					break;
			}
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLEX5000ProdTestForm));
            this.btnPLL = new System.Windows.Forms.ButtonTS();
            this.btnRXFilter = new System.Windows.Forms.ButtonTS();
            this.grpReceiver = new System.Windows.Forms.GroupBoxTS();
            this.udLevel = new System.Windows.Forms.NumericUpDown();
            this.btnRXImage = new System.Windows.Forms.ButtonTS();
            this.btnRXLevel = new System.Windows.Forms.ButtonTS();
            this.grpGeneral = new System.Windows.Forms.GroupBoxTS();
            this.btnGenATTN = new System.Windows.Forms.ButtonTS();
            this.btnGenPreamp = new System.Windows.Forms.ButtonTS();
            this.btnNoise = new System.Windows.Forms.ButtonTS();
            this.btnGenBal = new System.Windows.Forms.ButtonTS();
            this.btnImpulse = new System.Windows.Forms.ButtonTS();
            this.grpTransmitter = new System.Windows.Forms.GroupBoxTS();
            this.btnTXGain = new System.Windows.Forms.ButtonTS();
            this.btnTXFilter = new System.Windows.Forms.ButtonTS();
            this.btnTXCarrier = new System.Windows.Forms.ButtonTS();
            this.btnTXImage = new System.Windows.Forms.ButtonTS();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpBands = new System.Windows.Forms.GroupBoxTS();
            this.ck6 = new System.Windows.Forms.CheckBoxTS();
            this.ck10 = new System.Windows.Forms.CheckBoxTS();
            this.ck12 = new System.Windows.Forms.CheckBoxTS();
            this.ck15 = new System.Windows.Forms.CheckBoxTS();
            this.ck17 = new System.Windows.Forms.CheckBoxTS();
            this.ck20 = new System.Windows.Forms.CheckBoxTS();
            this.ck30 = new System.Windows.Forms.CheckBoxTS();
            this.ck40 = new System.Windows.Forms.CheckBoxTS();
            this.ck60 = new System.Windows.Forms.CheckBoxTS();
            this.ck80 = new System.Windows.Forms.CheckBoxTS();
            this.ck160 = new System.Windows.Forms.CheckBoxTS();
            this.btnClearAll = new System.Windows.Forms.ButtonTS();
            this.btnCheckAll = new System.Windows.Forms.ButtonTS();
            this.lstDebug = new System.Windows.Forms.ListBox();
            this.btnRunSelectedTests = new System.Windows.Forms.ButtonTS();
            this.lblTech = new System.Windows.Forms.LabelTS();
            this.txtTech = new System.Windows.Forms.TextBoxTS();
            this.btnPrintReport = new System.Windows.Forms.ButtonTS();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.grpTestGeneral = new System.Windows.Forms.GroupBoxTS();
            this.btnTestGenAll = new System.Windows.Forms.ButtonTS();
            this.ckTestGenPreamp = new System.Windows.Forms.CheckBoxTS();
            this.btnTestGenNone = new System.Windows.Forms.ButtonTS();
            this.ckTestGenImpulse = new System.Windows.Forms.CheckBoxTS();
            this.ckTestGenNoise = new System.Windows.Forms.CheckBoxTS();
            this.ckTestGenBal = new System.Windows.Forms.CheckBoxTS();
            this.ckTestGenPLL = new System.Windows.Forms.CheckBoxTS();
            this.ckTestGenATTN = new System.Windows.Forms.CheckBoxTS();
            this.grpTestReceiver = new System.Windows.Forms.GroupBoxTS();
            this.btnTestRXNone = new System.Windows.Forms.ButtonTS();
            this.btnTestRXAll = new System.Windows.Forms.ButtonTS();
            this.ckTestRXMDS = new System.Windows.Forms.CheckBoxTS();
            this.ckTestRXImage = new System.Windows.Forms.CheckBoxTS();
            this.ckTestRXLevel = new System.Windows.Forms.CheckBoxTS();
            this.ckTestRXFilter = new System.Windows.Forms.CheckBoxTS();
            this.grpTestTransmitter = new System.Windows.Forms.GroupBoxTS();
            this.ckTestTXGain = new System.Windows.Forms.CheckBoxTS();
            this.btnTestTXNone = new System.Windows.Forms.ButtonTS();
            this.btnTestTXAll = new System.Windows.Forms.ButtonTS();
            this.ckTestTXImage = new System.Windows.Forms.CheckBoxTS();
            this.ckTestTXCarrier = new System.Windows.Forms.CheckBoxTS();
            this.ckTestTXFilter = new System.Windows.Forms.CheckBoxTS();
            this.btnTestNone = new System.Windows.Forms.ButtonTS();
            this.btnTestAll = new System.Windows.Forms.ButtonTS();
            this.grpIO = new System.Windows.Forms.GroupBoxTS();
            this.btnIOMicFast = new System.Windows.Forms.ButtonTS();
            this.btnIOMicDown = new System.Windows.Forms.ButtonTS();
            this.btnIOMicUp = new System.Windows.Forms.ButtonTS();
            this.btnIORunAll = new System.Windows.Forms.ButtonTS();
            this.btnIOExtRef = new System.Windows.Forms.ButtonTS();
            this.btnIOHeadphone = new System.Windows.Forms.ButtonTS();
            this.btnIOMicPTT = new System.Windows.Forms.ButtonTS();
            this.btnIORCAPTT = new System.Windows.Forms.ButtonTS();
            this.btnIODash = new System.Windows.Forms.ButtonTS();
            this.btnIODot = new System.Windows.Forms.ButtonTS();
            this.btnIOFWInOut = new System.Windows.Forms.ButtonTS();
            this.btnIORCAInOut = new System.Windows.Forms.ButtonTS();
            this.btnIOPwrSpkr = new System.Windows.Forms.ButtonTS();
            this.btnPostFence = new System.Windows.Forms.ButtonTS();
            this.btnTXSpur = new System.Windows.Forms.ButtonTS();
            this.grpReceiver.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLevel)).BeginInit();
            this.grpGeneral.SuspendLayout();
            this.grpTransmitter.SuspendLayout();
            this.grpBands.SuspendLayout();
            this.grpTestGeneral.SuspendLayout();
            this.grpTestReceiver.SuspendLayout();
            this.grpTestTransmitter.SuspendLayout();
            this.grpIO.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPLL
            // 
            this.btnPLL.Image = null;
            this.btnPLL.Location = new System.Drawing.Point(16, 16);
            this.btnPLL.Name = "btnPLL";
            this.btnPLL.Size = new System.Drawing.Size(75, 23);
            this.btnPLL.TabIndex = 0;
            this.btnPLL.Text = "PLL";
            this.toolTip1.SetToolTip(this.btnPLL, "PLL Test: Not Run");
            this.btnPLL.Click += new System.EventHandler(this.btnPLL_Click);
            // 
            // btnRXFilter
            // 
            this.btnRXFilter.Image = null;
            this.btnRXFilter.Location = new System.Drawing.Point(16, 24);
            this.btnRXFilter.Name = "btnRXFilter";
            this.btnRXFilter.Size = new System.Drawing.Size(75, 23);
            this.btnRXFilter.TabIndex = 1;
            this.btnRXFilter.Text = "Filter";
            this.toolTip1.SetToolTip(this.btnRXFilter, "RX Filter Test: Not Run");
            this.btnRXFilter.Click += new System.EventHandler(this.btnRXFilter_Click);
            // 
            // grpReceiver
            // 
            this.grpReceiver.Controls.Add(this.udLevel);
            this.grpReceiver.Controls.Add(this.btnRXImage);
            this.grpReceiver.Controls.Add(this.btnRXFilter);
            this.grpReceiver.Controls.Add(this.btnRXLevel);
            this.grpReceiver.Location = new System.Drawing.Point(8, 120);
            this.grpReceiver.Name = "grpReceiver";
            this.grpReceiver.Size = new System.Drawing.Size(200, 104);
            this.grpReceiver.TabIndex = 2;
            this.grpReceiver.TabStop = false;
            this.grpReceiver.Text = "Receiver Tests";
            // 
            // udLevel
            // 
            this.udLevel.DecimalPlaces = 1;
            this.udLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udLevel.Location = new System.Drawing.Point(120, 64);
            this.udLevel.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udLevel.Name = "udLevel";
            this.udLevel.Size = new System.Drawing.Size(64, 20);
            this.udLevel.TabIndex = 5;
            this.udLevel.Value = new decimal(new int[] {
            240,
            0,
            0,
            -2147418112});
            this.udLevel.Visible = false;
            // 
            // btnRXImage
            // 
            this.btnRXImage.Image = null;
            this.btnRXImage.Location = new System.Drawing.Point(16, 64);
            this.btnRXImage.Name = "btnRXImage";
            this.btnRXImage.Size = new System.Drawing.Size(75, 23);
            this.btnRXImage.TabIndex = 4;
            this.btnRXImage.Text = "Image";
            this.toolTip1.SetToolTip(this.btnRXImage, "RX Image Test: Not Run");
            this.btnRXImage.Click += new System.EventHandler(this.btnRXImage_Click);
            // 
            // btnRXLevel
            // 
            this.btnRXLevel.Image = null;
            this.btnRXLevel.Location = new System.Drawing.Point(104, 24);
            this.btnRXLevel.Name = "btnRXLevel";
            this.btnRXLevel.Size = new System.Drawing.Size(75, 23);
            this.btnRXLevel.TabIndex = 3;
            this.btnRXLevel.Text = "Level";
            this.toolTip1.SetToolTip(this.btnRXLevel, "RX Level Test: Not Run");
            this.btnRXLevel.Click += new System.EventHandler(this.btnRXLevel_Click);
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.btnGenATTN);
            this.grpGeneral.Controls.Add(this.btnGenPreamp);
            this.grpGeneral.Controls.Add(this.btnNoise);
            this.grpGeneral.Controls.Add(this.btnGenBal);
            this.grpGeneral.Controls.Add(this.btnPLL);
            this.grpGeneral.Controls.Add(this.btnImpulse);
            this.grpGeneral.Location = new System.Drawing.Point(8, 8);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(200, 112);
            this.grpGeneral.TabIndex = 3;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General Tests";
            // 
            // btnGenATTN
            // 
            this.btnGenATTN.Image = null;
            this.btnGenATTN.Location = new System.Drawing.Point(104, 80);
            this.btnGenATTN.Name = "btnGenATTN";
            this.btnGenATTN.Size = new System.Drawing.Size(75, 23);
            this.btnGenATTN.TabIndex = 8;
            this.btnGenATTN.Text = "Attenuator";
            this.toolTip1.SetToolTip(this.btnGenATTN, "Attenuator Test: Not Run");
            this.btnGenATTN.Visible = false;
            this.btnGenATTN.Click += new System.EventHandler(this.btnGenATTN_Click);
            // 
            // btnGenPreamp
            // 
            this.btnGenPreamp.Image = null;
            this.btnGenPreamp.Location = new System.Drawing.Point(16, 80);
            this.btnGenPreamp.Name = "btnGenPreamp";
            this.btnGenPreamp.Size = new System.Drawing.Size(75, 23);
            this.btnGenPreamp.TabIndex = 7;
            this.btnGenPreamp.Text = "Preamp";
            this.toolTip1.SetToolTip(this.btnGenPreamp, "Preamp Test: Not Run");
            this.btnGenPreamp.Click += new System.EventHandler(this.btnGenPreamp_Click);
            // 
            // btnNoise
            // 
            this.btnNoise.Image = null;
            this.btnNoise.Location = new System.Drawing.Point(16, 48);
            this.btnNoise.Name = "btnNoise";
            this.btnNoise.Size = new System.Drawing.Size(75, 23);
            this.btnNoise.TabIndex = 2;
            this.btnNoise.Text = "Noise";
            this.toolTip1.SetToolTip(this.btnNoise, "Noise Test: Not Run");
            this.btnNoise.Visible = false;
            this.btnNoise.Click += new System.EventHandler(this.btnNoise_Click);
            // 
            // btnGenBal
            // 
            this.btnGenBal.Image = null;
            this.btnGenBal.Location = new System.Drawing.Point(104, 16);
            this.btnGenBal.Name = "btnGenBal";
            this.btnGenBal.Size = new System.Drawing.Size(75, 23);
            this.btnGenBal.TabIndex = 1;
            this.btnGenBal.Text = "Gen/Bal";
            this.toolTip1.SetToolTip(this.btnGenBal, "Gen/Bal Test: Not Run");
            this.btnGenBal.Click += new System.EventHandler(this.btnGenBal_Click);
            // 
            // btnImpulse
            // 
            this.btnImpulse.Image = null;
            this.btnImpulse.Location = new System.Drawing.Point(104, 48);
            this.btnImpulse.Name = "btnImpulse";
            this.btnImpulse.Size = new System.Drawing.Size(75, 23);
            this.btnImpulse.TabIndex = 6;
            this.btnImpulse.Text = "Impulse";
            this.toolTip1.SetToolTip(this.btnImpulse, "Impulse Test: Not Run");
            this.btnImpulse.Click += new System.EventHandler(this.btnImpulse_Click);
            // 
            // grpTransmitter
            // 
            this.grpTransmitter.Controls.Add(this.btnTXSpur);
            this.grpTransmitter.Controls.Add(this.btnTXGain);
            this.grpTransmitter.Controls.Add(this.btnTXFilter);
            this.grpTransmitter.Controls.Add(this.btnTXCarrier);
            this.grpTransmitter.Controls.Add(this.btnTXImage);
            this.grpTransmitter.Location = new System.Drawing.Point(8, 232);
            this.grpTransmitter.Name = "grpTransmitter";
            this.grpTransmitter.Size = new System.Drawing.Size(200, 100);
            this.grpTransmitter.TabIndex = 4;
            this.grpTransmitter.TabStop = false;
            this.grpTransmitter.Text = "Transmitter";
            // 
            // btnTXGain
            // 
            this.btnTXGain.Image = null;
            this.btnTXGain.Location = new System.Drawing.Point(104, 64);
            this.btnTXGain.Name = "btnTXGain";
            this.btnTXGain.Size = new System.Drawing.Size(75, 23);
            this.btnTXGain.TabIndex = 8;
            this.btnTXGain.Text = "Gain";
            this.btnTXGain.Visible = false;
            this.btnTXGain.Click += new System.EventHandler(this.btnTXGain_Click);
            // 
            // btnTXFilter
            // 
            this.btnTXFilter.Image = null;
            this.btnTXFilter.Location = new System.Drawing.Point(16, 24);
            this.btnTXFilter.Name = "btnTXFilter";
            this.btnTXFilter.Size = new System.Drawing.Size(75, 23);
            this.btnTXFilter.TabIndex = 7;
            this.btnTXFilter.Text = "Filter";
            this.btnTXFilter.Click += new System.EventHandler(this.btnTXFilter_Click);
            // 
            // btnTXCarrier
            // 
            this.btnTXCarrier.Image = null;
            this.btnTXCarrier.Location = new System.Drawing.Point(16, 64);
            this.btnTXCarrier.Name = "btnTXCarrier";
            this.btnTXCarrier.Size = new System.Drawing.Size(75, 23);
            this.btnTXCarrier.TabIndex = 6;
            this.btnTXCarrier.Text = "Carrier";
            this.btnTXCarrier.Click += new System.EventHandler(this.btnTXCarrier_Click);
            // 
            // btnTXImage
            // 
            this.btnTXImage.Image = null;
            this.btnTXImage.Location = new System.Drawing.Point(104, 24);
            this.btnTXImage.Name = "btnTXImage";
            this.btnTXImage.Size = new System.Drawing.Size(75, 23);
            this.btnTXImage.TabIndex = 5;
            this.btnTXImage.Text = "Image";
            this.btnTXImage.Click += new System.EventHandler(this.btnTXImage_Click);
            // 
            // grpBands
            // 
            this.grpBands.Controls.Add(this.ck6);
            this.grpBands.Controls.Add(this.ck10);
            this.grpBands.Controls.Add(this.ck12);
            this.grpBands.Controls.Add(this.ck15);
            this.grpBands.Controls.Add(this.ck17);
            this.grpBands.Controls.Add(this.ck20);
            this.grpBands.Controls.Add(this.ck30);
            this.grpBands.Controls.Add(this.ck40);
            this.grpBands.Controls.Add(this.ck60);
            this.grpBands.Controls.Add(this.ck80);
            this.grpBands.Controls.Add(this.ck160);
            this.grpBands.Controls.Add(this.btnClearAll);
            this.grpBands.Controls.Add(this.btnCheckAll);
            this.grpBands.Location = new System.Drawing.Point(216, 8);
            this.grpBands.Name = "grpBands";
            this.grpBands.Size = new System.Drawing.Size(256, 104);
            this.grpBands.TabIndex = 15;
            this.grpBands.TabStop = false;
            this.grpBands.Text = "Bands";
            // 
            // ck6
            // 
            this.ck6.Checked = true;
            this.ck6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck6.Image = null;
            this.ck6.Location = new System.Drawing.Point(216, 40);
            this.ck6.Name = "ck6";
            this.ck6.Size = new System.Drawing.Size(32, 24);
            this.ck6.TabIndex = 28;
            this.ck6.Text = "6";
            // 
            // ck10
            // 
            this.ck10.Checked = true;
            this.ck10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck10.Image = null;
            this.ck10.Location = new System.Drawing.Point(176, 40);
            this.ck10.Name = "ck10";
            this.ck10.Size = new System.Drawing.Size(40, 24);
            this.ck10.TabIndex = 27;
            this.ck10.Text = "10";
            // 
            // ck12
            // 
            this.ck12.Checked = true;
            this.ck12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck12.Image = null;
            this.ck12.Location = new System.Drawing.Point(136, 40);
            this.ck12.Name = "ck12";
            this.ck12.Size = new System.Drawing.Size(40, 24);
            this.ck12.TabIndex = 26;
            this.ck12.Text = "12";
            // 
            // ck15
            // 
            this.ck15.Checked = true;
            this.ck15.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck15.Image = null;
            this.ck15.Location = new System.Drawing.Point(96, 40);
            this.ck15.Name = "ck15";
            this.ck15.Size = new System.Drawing.Size(40, 24);
            this.ck15.TabIndex = 25;
            this.ck15.Text = "15";
            // 
            // ck17
            // 
            this.ck17.Checked = true;
            this.ck17.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck17.Image = null;
            this.ck17.Location = new System.Drawing.Point(56, 40);
            this.ck17.Name = "ck17";
            this.ck17.Size = new System.Drawing.Size(40, 24);
            this.ck17.TabIndex = 24;
            this.ck17.Text = "17";
            // 
            // ck20
            // 
            this.ck20.Checked = true;
            this.ck20.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck20.Image = null;
            this.ck20.Location = new System.Drawing.Point(16, 40);
            this.ck20.Name = "ck20";
            this.ck20.Size = new System.Drawing.Size(40, 24);
            this.ck20.TabIndex = 23;
            this.ck20.Text = "20";
            // 
            // ck30
            // 
            this.ck30.Checked = true;
            this.ck30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck30.Image = null;
            this.ck30.Location = new System.Drawing.Point(184, 16);
            this.ck30.Name = "ck30";
            this.ck30.Size = new System.Drawing.Size(40, 24);
            this.ck30.TabIndex = 22;
            this.ck30.Text = "30";
            // 
            // ck40
            // 
            this.ck40.Checked = true;
            this.ck40.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck40.Image = null;
            this.ck40.Location = new System.Drawing.Point(144, 16);
            this.ck40.Name = "ck40";
            this.ck40.Size = new System.Drawing.Size(40, 24);
            this.ck40.TabIndex = 21;
            this.ck40.Text = "40";
            // 
            // ck60
            // 
            this.ck60.Checked = true;
            this.ck60.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck60.Image = null;
            this.ck60.Location = new System.Drawing.Point(104, 16);
            this.ck60.Name = "ck60";
            this.ck60.Size = new System.Drawing.Size(40, 24);
            this.ck60.TabIndex = 20;
            this.ck60.Text = "60";
            // 
            // ck80
            // 
            this.ck80.Checked = true;
            this.ck80.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck80.Image = null;
            this.ck80.Location = new System.Drawing.Point(64, 16);
            this.ck80.Name = "ck80";
            this.ck80.Size = new System.Drawing.Size(40, 24);
            this.ck80.TabIndex = 19;
            this.ck80.Text = "80";
            // 
            // ck160
            // 
            this.ck160.Checked = true;
            this.ck160.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck160.Image = null;
            this.ck160.Location = new System.Drawing.Point(16, 16);
            this.ck160.Name = "ck160";
            this.ck160.Size = new System.Drawing.Size(48, 24);
            this.ck160.TabIndex = 18;
            this.ck160.Text = "160";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Image = null;
            this.btnClearAll.Location = new System.Drawing.Point(112, 64);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(64, 20);
            this.btnClearAll.TabIndex = 30;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Image = null;
            this.btnCheckAll.Location = new System.Drawing.Point(24, 64);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(64, 20);
            this.btnCheckAll.TabIndex = 29;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // lstDebug
            // 
            this.lstDebug.HorizontalScrollbar = true;
            this.lstDebug.Location = new System.Drawing.Point(216, 128);
            this.lstDebug.Name = "lstDebug";
            this.lstDebug.Size = new System.Drawing.Size(256, 199);
            this.lstDebug.TabIndex = 16;
            // 
            // btnRunSelectedTests
            // 
            this.btnRunSelectedTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunSelectedTests.Image = null;
            this.btnRunSelectedTests.Location = new System.Drawing.Point(8, 344);
            this.btnRunSelectedTests.Name = "btnRunSelectedTests";
            this.btnRunSelectedTests.Size = new System.Drawing.Size(88, 32);
            this.btnRunSelectedTests.TabIndex = 17;
            this.btnRunSelectedTests.Text = "Run Selected Tests";
            this.btnRunSelectedTests.Click += new System.EventHandler(this.btnRunSelectedTests_Click);
            // 
            // lblTech
            // 
            this.lblTech.Image = null;
            this.lblTech.Location = new System.Drawing.Point(216, 352);
            this.lblTech.Name = "lblTech";
            this.lblTech.Size = new System.Drawing.Size(64, 23);
            this.lblTech.TabIndex = 18;
            this.lblTech.Text = "Technician:";
            // 
            // txtTech
            // 
            this.txtTech.Location = new System.Drawing.Point(280, 352);
            this.txtTech.Name = "txtTech";
            this.txtTech.Size = new System.Drawing.Size(100, 20);
            this.txtTech.TabIndex = 19;
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.Image = null;
            this.btnPrintReport.Location = new System.Drawing.Point(400, 352);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(75, 24);
            this.btnPrintReport.TabIndex = 20;
            this.btnPrintReport.Text = "Print Report";
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // grpTestGeneral
            // 
            this.grpTestGeneral.Controls.Add(this.btnTestGenAll);
            this.grpTestGeneral.Controls.Add(this.ckTestGenPreamp);
            this.grpTestGeneral.Controls.Add(this.btnTestGenNone);
            this.grpTestGeneral.Controls.Add(this.ckTestGenImpulse);
            this.grpTestGeneral.Controls.Add(this.ckTestGenNoise);
            this.grpTestGeneral.Controls.Add(this.ckTestGenBal);
            this.grpTestGeneral.Controls.Add(this.ckTestGenPLL);
            this.grpTestGeneral.Controls.Add(this.ckTestGenATTN);
            this.grpTestGeneral.Location = new System.Drawing.Point(8, 384);
            this.grpTestGeneral.Name = "grpTestGeneral";
            this.grpTestGeneral.Size = new System.Drawing.Size(152, 96);
            this.grpTestGeneral.TabIndex = 21;
            this.grpTestGeneral.TabStop = false;
            this.grpTestGeneral.Text = "General";
            // 
            // btnTestGenAll
            // 
            this.btnTestGenAll.Image = null;
            this.btnTestGenAll.Location = new System.Drawing.Point(64, 72);
            this.btnTestGenAll.Name = "btnTestGenAll";
            this.btnTestGenAll.Size = new System.Drawing.Size(40, 16);
            this.btnTestGenAll.TabIndex = 24;
            this.btnTestGenAll.Text = "All";
            this.btnTestGenAll.Click += new System.EventHandler(this.btnTestGenAll_Click);
            // 
            // ckTestGenPreamp
            // 
            this.ckTestGenPreamp.Checked = true;
            this.ckTestGenPreamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestGenPreamp.Image = null;
            this.ckTestGenPreamp.Location = new System.Drawing.Point(8, 72);
            this.ckTestGenPreamp.Name = "ckTestGenPreamp";
            this.ckTestGenPreamp.Size = new System.Drawing.Size(64, 16);
            this.ckTestGenPreamp.TabIndex = 26;
            this.ckTestGenPreamp.Text = "Preamp";
            // 
            // btnTestGenNone
            // 
            this.btnTestGenNone.Image = null;
            this.btnTestGenNone.Location = new System.Drawing.Point(104, 72);
            this.btnTestGenNone.Name = "btnTestGenNone";
            this.btnTestGenNone.Size = new System.Drawing.Size(40, 16);
            this.btnTestGenNone.TabIndex = 25;
            this.btnTestGenNone.Text = "None";
            this.btnTestGenNone.Click += new System.EventHandler(this.btnTestGenNone_Click);
            // 
            // ckTestGenImpulse
            // 
            this.ckTestGenImpulse.Checked = true;
            this.ckTestGenImpulse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestGenImpulse.Image = null;
            this.ckTestGenImpulse.Location = new System.Drawing.Point(80, 48);
            this.ckTestGenImpulse.Name = "ckTestGenImpulse";
            this.ckTestGenImpulse.Size = new System.Drawing.Size(64, 24);
            this.ckTestGenImpulse.TabIndex = 22;
            this.ckTestGenImpulse.Text = "Impulse";
            // 
            // ckTestGenNoise
            // 
            this.ckTestGenNoise.Image = null;
            this.ckTestGenNoise.Location = new System.Drawing.Point(16, 48);
            this.ckTestGenNoise.Name = "ckTestGenNoise";
            this.ckTestGenNoise.Size = new System.Drawing.Size(56, 24);
            this.ckTestGenNoise.TabIndex = 21;
            this.ckTestGenNoise.Text = "Noise";
            this.ckTestGenNoise.Visible = false;
            // 
            // ckTestGenBal
            // 
            this.ckTestGenBal.Checked = true;
            this.ckTestGenBal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestGenBal.Image = null;
            this.ckTestGenBal.Location = new System.Drawing.Point(80, 24);
            this.ckTestGenBal.Name = "ckTestGenBal";
            this.ckTestGenBal.Size = new System.Drawing.Size(64, 24);
            this.ckTestGenBal.TabIndex = 20;
            this.ckTestGenBal.Text = "Gen/Bal";
            // 
            // ckTestGenPLL
            // 
            this.ckTestGenPLL.Checked = true;
            this.ckTestGenPLL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestGenPLL.Image = null;
            this.ckTestGenPLL.Location = new System.Drawing.Point(16, 24);
            this.ckTestGenPLL.Name = "ckTestGenPLL";
            this.ckTestGenPLL.Size = new System.Drawing.Size(48, 24);
            this.ckTestGenPLL.TabIndex = 19;
            this.ckTestGenPLL.Text = "PLL";
            // 
            // ckTestGenATTN
            // 
            this.ckTestGenATTN.Image = null;
            this.ckTestGenATTN.Location = new System.Drawing.Point(16, 24);
            this.ckTestGenATTN.Name = "ckTestGenATTN";
            this.ckTestGenATTN.Size = new System.Drawing.Size(56, 24);
            this.ckTestGenATTN.TabIndex = 27;
            this.ckTestGenATTN.Text = "ATTN";
            // 
            // grpTestReceiver
            // 
            this.grpTestReceiver.Controls.Add(this.btnTestRXNone);
            this.grpTestReceiver.Controls.Add(this.btnTestRXAll);
            this.grpTestReceiver.Controls.Add(this.ckTestRXMDS);
            this.grpTestReceiver.Controls.Add(this.ckTestRXImage);
            this.grpTestReceiver.Controls.Add(this.ckTestRXLevel);
            this.grpTestReceiver.Controls.Add(this.ckTestRXFilter);
            this.grpTestReceiver.Location = new System.Drawing.Point(168, 384);
            this.grpTestReceiver.Name = "grpTestReceiver";
            this.grpTestReceiver.Size = new System.Drawing.Size(144, 96);
            this.grpTestReceiver.TabIndex = 22;
            this.grpTestReceiver.TabStop = false;
            this.grpTestReceiver.Text = "Receiver";
            // 
            // btnTestRXNone
            // 
            this.btnTestRXNone.Image = null;
            this.btnTestRXNone.Location = new System.Drawing.Point(76, 72);
            this.btnTestRXNone.Name = "btnTestRXNone";
            this.btnTestRXNone.Size = new System.Drawing.Size(40, 16);
            this.btnTestRXNone.TabIndex = 27;
            this.btnTestRXNone.Text = "None";
            this.btnTestRXNone.Click += new System.EventHandler(this.btnTestRXNone_Click);
            // 
            // btnTestRXAll
            // 
            this.btnTestRXAll.Image = null;
            this.btnTestRXAll.Location = new System.Drawing.Point(28, 72);
            this.btnTestRXAll.Name = "btnTestRXAll";
            this.btnTestRXAll.Size = new System.Drawing.Size(40, 16);
            this.btnTestRXAll.TabIndex = 26;
            this.btnTestRXAll.Text = "All";
            this.btnTestRXAll.Click += new System.EventHandler(this.btnTestRXAll_Click);
            // 
            // ckTestRXMDS
            // 
            this.ckTestRXMDS.Checked = true;
            this.ckTestRXMDS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestRXMDS.Image = null;
            this.ckTestRXMDS.Location = new System.Drawing.Point(80, 48);
            this.ckTestRXMDS.Name = "ckTestRXMDS";
            this.ckTestRXMDS.Size = new System.Drawing.Size(56, 24);
            this.ckTestRXMDS.TabIndex = 22;
            this.ckTestRXMDS.Text = "MDS";
            this.ckTestRXMDS.Visible = false;
            // 
            // ckTestRXImage
            // 
            this.ckTestRXImage.Checked = true;
            this.ckTestRXImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestRXImage.Image = null;
            this.ckTestRXImage.Location = new System.Drawing.Point(16, 48);
            this.ckTestRXImage.Name = "ckTestRXImage";
            this.ckTestRXImage.Size = new System.Drawing.Size(56, 24);
            this.ckTestRXImage.TabIndex = 21;
            this.ckTestRXImage.Text = "Image";
            // 
            // ckTestRXLevel
            // 
            this.ckTestRXLevel.Checked = true;
            this.ckTestRXLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestRXLevel.Image = null;
            this.ckTestRXLevel.Location = new System.Drawing.Point(80, 24);
            this.ckTestRXLevel.Name = "ckTestRXLevel";
            this.ckTestRXLevel.Size = new System.Drawing.Size(56, 24);
            this.ckTestRXLevel.TabIndex = 20;
            this.ckTestRXLevel.Text = "Level";
            // 
            // ckTestRXFilter
            // 
            this.ckTestRXFilter.Checked = true;
            this.ckTestRXFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestRXFilter.Image = null;
            this.ckTestRXFilter.Location = new System.Drawing.Point(16, 24);
            this.ckTestRXFilter.Name = "ckTestRXFilter";
            this.ckTestRXFilter.Size = new System.Drawing.Size(48, 24);
            this.ckTestRXFilter.TabIndex = 19;
            this.ckTestRXFilter.Text = "Filter";
            // 
            // grpTestTransmitter
            // 
            this.grpTestTransmitter.Controls.Add(this.ckTestTXGain);
            this.grpTestTransmitter.Controls.Add(this.btnTestTXNone);
            this.grpTestTransmitter.Controls.Add(this.btnTestTXAll);
            this.grpTestTransmitter.Controls.Add(this.ckTestTXImage);
            this.grpTestTransmitter.Controls.Add(this.ckTestTXCarrier);
            this.grpTestTransmitter.Controls.Add(this.ckTestTXFilter);
            this.grpTestTransmitter.Location = new System.Drawing.Point(320, 384);
            this.grpTestTransmitter.Name = "grpTestTransmitter";
            this.grpTestTransmitter.Size = new System.Drawing.Size(144, 96);
            this.grpTestTransmitter.TabIndex = 23;
            this.grpTestTransmitter.TabStop = false;
            this.grpTestTransmitter.Text = "Transmitter";
            // 
            // ckTestTXGain
            // 
            this.ckTestTXGain.Image = null;
            this.ckTestTXGain.Location = new System.Drawing.Point(72, 48);
            this.ckTestTXGain.Name = "ckTestTXGain";
            this.ckTestTXGain.Size = new System.Drawing.Size(56, 24);
            this.ckTestTXGain.TabIndex = 28;
            this.ckTestTXGain.Text = "Gain";
            this.ckTestTXGain.Visible = false;
            // 
            // btnTestTXNone
            // 
            this.btnTestTXNone.Image = null;
            this.btnTestTXNone.Location = new System.Drawing.Point(76, 72);
            this.btnTestTXNone.Name = "btnTestTXNone";
            this.btnTestTXNone.Size = new System.Drawing.Size(40, 16);
            this.btnTestTXNone.TabIndex = 27;
            this.btnTestTXNone.Text = "None";
            this.btnTestTXNone.Click += new System.EventHandler(this.btnTestTXNone_Click);
            // 
            // btnTestTXAll
            // 
            this.btnTestTXAll.Image = null;
            this.btnTestTXAll.Location = new System.Drawing.Point(28, 72);
            this.btnTestTXAll.Name = "btnTestTXAll";
            this.btnTestTXAll.Size = new System.Drawing.Size(40, 16);
            this.btnTestTXAll.TabIndex = 26;
            this.btnTestTXAll.Text = "All";
            this.btnTestTXAll.Click += new System.EventHandler(this.btnTestTXAll_Click);
            // 
            // ckTestTXImage
            // 
            this.ckTestTXImage.Checked = true;
            this.ckTestTXImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestTXImage.Image = null;
            this.ckTestTXImage.Location = new System.Drawing.Point(72, 24);
            this.ckTestTXImage.Name = "ckTestTXImage";
            this.ckTestTXImage.Size = new System.Drawing.Size(56, 24);
            this.ckTestTXImage.TabIndex = 21;
            this.ckTestTXImage.Text = "Image";
            // 
            // ckTestTXCarrier
            // 
            this.ckTestTXCarrier.Checked = true;
            this.ckTestTXCarrier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestTXCarrier.Image = null;
            this.ckTestTXCarrier.Location = new System.Drawing.Point(16, 48);
            this.ckTestTXCarrier.Name = "ckTestTXCarrier";
            this.ckTestTXCarrier.Size = new System.Drawing.Size(64, 24);
            this.ckTestTXCarrier.TabIndex = 20;
            this.ckTestTXCarrier.Text = "Carrier";
            // 
            // ckTestTXFilter
            // 
            this.ckTestTXFilter.Checked = true;
            this.ckTestTXFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTestTXFilter.Image = null;
            this.ckTestTXFilter.Location = new System.Drawing.Point(16, 24);
            this.ckTestTXFilter.Name = "ckTestTXFilter";
            this.ckTestTXFilter.Size = new System.Drawing.Size(48, 24);
            this.ckTestTXFilter.TabIndex = 19;
            this.ckTestTXFilter.Text = "Filter";
            // 
            // btnTestNone
            // 
            this.btnTestNone.Image = null;
            this.btnTestNone.Location = new System.Drawing.Point(104, 360);
            this.btnTestNone.Name = "btnTestNone";
            this.btnTestNone.Size = new System.Drawing.Size(40, 16);
            this.btnTestNone.TabIndex = 27;
            this.btnTestNone.Text = "None";
            this.btnTestNone.Click += new System.EventHandler(this.btnTestNone_Click);
            // 
            // btnTestAll
            // 
            this.btnTestAll.Image = null;
            this.btnTestAll.Location = new System.Drawing.Point(104, 344);
            this.btnTestAll.Name = "btnTestAll";
            this.btnTestAll.Size = new System.Drawing.Size(40, 16);
            this.btnTestAll.TabIndex = 26;
            this.btnTestAll.Text = "All";
            this.btnTestAll.Click += new System.EventHandler(this.btnTestAll_Click);
            // 
            // grpIO
            // 
            this.grpIO.Controls.Add(this.btnIOMicFast);
            this.grpIO.Controls.Add(this.btnIOMicDown);
            this.grpIO.Controls.Add(this.btnIOMicUp);
            this.grpIO.Controls.Add(this.btnIORunAll);
            this.grpIO.Controls.Add(this.btnIOExtRef);
            this.grpIO.Controls.Add(this.btnIOHeadphone);
            this.grpIO.Controls.Add(this.btnIOMicPTT);
            this.grpIO.Controls.Add(this.btnIORCAPTT);
            this.grpIO.Controls.Add(this.btnIODash);
            this.grpIO.Controls.Add(this.btnIODot);
            this.grpIO.Controls.Add(this.btnIOFWInOut);
            this.grpIO.Controls.Add(this.btnIORCAInOut);
            this.grpIO.Controls.Add(this.btnIOPwrSpkr);
            this.grpIO.Location = new System.Drawing.Point(480, 8);
            this.grpIO.Name = "grpIO";
            this.grpIO.Size = new System.Drawing.Size(104, 472);
            this.grpIO.TabIndex = 28;
            this.grpIO.TabStop = false;
            this.grpIO.Text = "Input/Output";
            // 
            // btnIOMicFast
            // 
            this.btnIOMicFast.Image = null;
            this.btnIOMicFast.Location = new System.Drawing.Point(16, 384);
            this.btnIOMicFast.Name = "btnIOMicFast";
            this.btnIOMicFast.Size = new System.Drawing.Size(75, 23);
            this.btnIOMicFast.TabIndex = 34;
            this.btnIOMicFast.Text = "Mic Fast";
            this.btnIOMicFast.Visible = false;
            this.btnIOMicFast.Click += new System.EventHandler(this.btnIOMicFast_Click);
            // 
            // btnIOMicDown
            // 
            this.btnIOMicDown.Image = null;
            this.btnIOMicDown.Location = new System.Drawing.Point(16, 104);
            this.btnIOMicDown.Name = "btnIOMicDown";
            this.btnIOMicDown.Size = new System.Drawing.Size(75, 23);
            this.btnIOMicDown.TabIndex = 33;
            this.btnIOMicDown.Text = "Mic Down";
            this.btnIOMicDown.Visible = false;
            this.btnIOMicDown.Click += new System.EventHandler(this.btnIOMicDown_Click);
            // 
            // btnIOMicUp
            // 
            this.btnIOMicUp.Image = null;
            this.btnIOMicUp.Location = new System.Drawing.Point(16, 24);
            this.btnIOMicUp.Name = "btnIOMicUp";
            this.btnIOMicUp.Size = new System.Drawing.Size(75, 23);
            this.btnIOMicUp.TabIndex = 32;
            this.btnIOMicUp.Text = "Mic Up";
            this.btnIOMicUp.Visible = false;
            this.btnIOMicUp.Click += new System.EventHandler(this.btnIOMicUp_Click);
            // 
            // btnIORunAll
            // 
            this.btnIORunAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIORunAll.Image = null;
            this.btnIORunAll.Location = new System.Drawing.Point(16, 416);
            this.btnIORunAll.Name = "btnIORunAll";
            this.btnIORunAll.Size = new System.Drawing.Size(75, 40);
            this.btnIORunAll.TabIndex = 31;
            this.btnIORunAll.Text = "Run All IO Tests";
            this.btnIORunAll.Click += new System.EventHandler(this.btnIORunAll_Click);
            // 
            // btnIOExtRef
            // 
            this.btnIOExtRef.Image = null;
            this.btnIOExtRef.Location = new System.Drawing.Point(16, 24);
            this.btnIOExtRef.Name = "btnIOExtRef";
            this.btnIOExtRef.Size = new System.Drawing.Size(75, 23);
            this.btnIOExtRef.TabIndex = 30;
            this.btnIOExtRef.Text = "Ext Ref";
            this.btnIOExtRef.Click += new System.EventHandler(this.btnIOExtRef_Click);
            // 
            // btnIOHeadphone
            // 
            this.btnIOHeadphone.Image = null;
            this.btnIOHeadphone.Location = new System.Drawing.Point(16, 184);
            this.btnIOHeadphone.Name = "btnIOHeadphone";
            this.btnIOHeadphone.Size = new System.Drawing.Size(75, 23);
            this.btnIOHeadphone.TabIndex = 29;
            this.btnIOHeadphone.Text = "Headphone";
            this.btnIOHeadphone.Click += new System.EventHandler(this.btnIOHeadphone_Click);
            // 
            // btnIOMicPTT
            // 
            this.btnIOMicPTT.Image = null;
            this.btnIOMicPTT.Location = new System.Drawing.Point(16, 344);
            this.btnIOMicPTT.Name = "btnIOMicPTT";
            this.btnIOMicPTT.Size = new System.Drawing.Size(75, 23);
            this.btnIOMicPTT.TabIndex = 28;
            this.btnIOMicPTT.Text = "Mic PTT";
            this.btnIOMicPTT.Click += new System.EventHandler(this.btnIOMicPTT_Click);
            // 
            // btnIORCAPTT
            // 
            this.btnIORCAPTT.Image = null;
            this.btnIORCAPTT.Location = new System.Drawing.Point(16, 304);
            this.btnIORCAPTT.Name = "btnIORCAPTT";
            this.btnIORCAPTT.Size = new System.Drawing.Size(75, 23);
            this.btnIORCAPTT.TabIndex = 27;
            this.btnIORCAPTT.Text = "RCA PTT";
            this.btnIORCAPTT.Click += new System.EventHandler(this.btnIORCAPTT_Click);
            // 
            // btnIODash
            // 
            this.btnIODash.Image = null;
            this.btnIODash.Location = new System.Drawing.Point(16, 264);
            this.btnIODash.Name = "btnIODash";
            this.btnIODash.Size = new System.Drawing.Size(75, 23);
            this.btnIODash.TabIndex = 26;
            this.btnIODash.Text = "Dash";
            this.btnIODash.Click += new System.EventHandler(this.btnIODash_Click);
            // 
            // btnIODot
            // 
            this.btnIODot.Image = null;
            this.btnIODot.Location = new System.Drawing.Point(16, 224);
            this.btnIODot.Name = "btnIODot";
            this.btnIODot.Size = new System.Drawing.Size(75, 23);
            this.btnIODot.TabIndex = 25;
            this.btnIODot.Text = "Dot";
            this.btnIODot.Click += new System.EventHandler(this.btnIODot_Click);
            // 
            // btnIOFWInOut
            // 
            this.btnIOFWInOut.Image = null;
            this.btnIOFWInOut.Location = new System.Drawing.Point(16, 144);
            this.btnIOFWInOut.Name = "btnIOFWInOut";
            this.btnIOFWInOut.Size = new System.Drawing.Size(75, 23);
            this.btnIOFWInOut.TabIndex = 24;
            this.btnIOFWInOut.Text = "FW In/Out";
            this.btnIOFWInOut.Click += new System.EventHandler(this.btnIOFWInOut_Click);
            // 
            // btnIORCAInOut
            // 
            this.btnIORCAInOut.Image = null;
            this.btnIORCAInOut.Location = new System.Drawing.Point(16, 104);
            this.btnIORCAInOut.Name = "btnIORCAInOut";
            this.btnIORCAInOut.Size = new System.Drawing.Size(75, 23);
            this.btnIORCAInOut.TabIndex = 23;
            this.btnIORCAInOut.Text = "RCA In/Out";
            this.btnIORCAInOut.Click += new System.EventHandler(this.btnIORCAInOut_Click);
            // 
            // btnIOPwrSpkr
            // 
            this.btnIOPwrSpkr.Image = null;
            this.btnIOPwrSpkr.Location = new System.Drawing.Point(16, 64);
            this.btnIOPwrSpkr.Name = "btnIOPwrSpkr";
            this.btnIOPwrSpkr.Size = new System.Drawing.Size(75, 23);
            this.btnIOPwrSpkr.TabIndex = 22;
            this.btnIOPwrSpkr.Text = "PWR.SPKR";
            this.btnIOPwrSpkr.Click += new System.EventHandler(this.btnIOPwrSpkr_Click);
            // 
            // btnPostFence
            // 
            this.btnPostFence.Image = null;
            this.btnPostFence.Location = new System.Drawing.Point(152, 344);
            this.btnPostFence.Name = "btnPostFence";
            this.btnPostFence.Size = new System.Drawing.Size(56, 32);
            this.btnPostFence.TabIndex = 29;
            this.btnPostFence.Text = "Post Fence";
            this.btnPostFence.Click += new System.EventHandler(this.btnPostFence_Click);
            // 
            // btnTXSpur
            // 
            this.btnTXSpur.Image = null;
            this.btnTXSpur.Location = new System.Drawing.Point(104, 64);
            this.btnTXSpur.Name = "btnTXSpur";
            this.btnTXSpur.Size = new System.Drawing.Size(75, 23);
            this.btnTXSpur.TabIndex = 9;
            this.btnTXSpur.Text = "Spur";
            this.btnTXSpur.Visible = false;
            // 
            // FLEX5000ProdTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 486);
            this.Controls.Add(this.btnPostFence);
            this.Controls.Add(this.grpIO);
            this.Controls.Add(this.btnTestNone);
            this.Controls.Add(this.btnTestAll);
            this.Controls.Add(this.grpTestTransmitter);
            this.Controls.Add(this.grpTestReceiver);
            this.Controls.Add(this.grpTestGeneral);
            this.Controls.Add(this.btnPrintReport);
            this.Controls.Add(this.txtTech);
            this.Controls.Add(this.lblTech);
            this.Controls.Add(this.btnRunSelectedTests);
            this.Controls.Add(this.lstDebug);
            this.Controls.Add(this.grpBands);
            this.Controls.Add(this.grpTransmitter);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.grpReceiver);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FLEX5000ProdTestForm";
            this.Text = "FLEX-5000 Production TRX / IO Test";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FLEX5000ProdTestForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FLEX5000ProdTestForm_KeyDown);
            this.grpReceiver.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udLevel)).EndInit();
            this.grpGeneral.ResumeLayout(false);
            this.grpTransmitter.ResumeLayout(false);
            this.grpBands.ResumeLayout(false);
            this.grpTestGeneral.ResumeLayout(false);
            this.grpTestReceiver.ResumeLayout(false);
            this.grpTestTransmitter.ResumeLayout(false);
            this.grpIO.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Misc Routines

		private string BandToString(Band b)
		{
			string ret_val = "";
			switch(b)
			{
				case Band.B160M: ret_val = " 160m "; break;
				case Band.B80M: ret_val = " 80m "; break;
				case Band.B60M: ret_val = " 60m "; break;
				case Band.B40M: ret_val = " 40m "; break;
				case Band.B30M: ret_val = " 30m "; break;
				case Band.B20M: ret_val = " 20m "; break;
				case Band.B17M: ret_val = " 17m "; break;
				case Band.B15M: ret_val = " 15m "; break;
				case Band.B12M: ret_val = " 12m "; break;
				case Band.B10M: ret_val = " 10m "; break;
				case Band.B6M: ret_val = " 6m "; break;
			}
			return ret_val;
		}

		private void SetBandFromString(string s)
		{
			ck160.Checked = (s.IndexOf(" 160m ") >= 0);
			ck80.Checked = (s.IndexOf(" 80m ") >= 0);
			ck60.Checked = (s.IndexOf(" 60m ") >= 0);
			ck40.Checked = (s.IndexOf(" 40m ") >= 0);
			ck30.Checked = (s.IndexOf(" 30m ") >= 0);
			ck20.Checked = (s.IndexOf(" 20m ") >= 0);
			ck17.Checked = (s.IndexOf(" 17m ") >= 0);
			ck15.Checked = (s.IndexOf(" 15m ") >= 0);
			ck12.Checked = (s.IndexOf(" 12m ") >= 0);
			ck10.Checked = (s.IndexOf(" 10m ") >= 0);
			ck6.Checked = (s.IndexOf(" 6m ") >= 0);
		}

		private string GetStringFromBands()
		{
			string s = " ";
			if(ck160.Checked) s += "160m ";
			if(ck80.Checked) s += "80m ";
			if(ck60.Checked) s += "60m ";
			if(ck40.Checked) s += "40m ";
			if(ck30.Checked) s += "30m ";
			if(ck20.Checked) s += "20m ";
			if(ck17.Checked) s += "17m ";
			if(ck15.Checked) s += "15m ";
			if(ck12.Checked) s += "12m ";
			if(ck10.Checked) s += "10m ";
			if(ck6.Checked) s += "6m ";
			return s;
		}

		#endregion

		#region General Tests

		#region PLL

		private string test_pll = "PLL Test: Not Run";
		private void btnPLL_Click(object sender, System.EventArgs e)
		{
			console.VFOSplit = false;
			bool b;
			FWC.WriteClockReg(0x08, 0x47);
			//Thread.Sleep(50);
			FWC.GetPLLStatus2(out b);
			//Thread.Sleep(50);
			if(b) btnPLL.BackColor = Color.Green;
			else btnPLL.BackColor = Color.Red;
			if(b) test_pll = "PLL Test: Passed";
			else test_pll = " PLL Test: Failed (not locked)";
			toolTip1.SetToolTip(btnPLL, test_pll);
			lstDebug.Items.Insert(0, test_pll);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			/*path += "\\PLL";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);*/
			path += "\\pll.csv";
			bool file_exists = File.Exists(path);
			StreamWriter writer = new StreamWriter(path, true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, Locked");
			writer.WriteLine(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region Gen/Bal

		private void btnGenBal_Click(object sender, System.EventArgs e)
		{
			btnGenBal.Enabled = false;
			btnGenBal.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(TestGenBal));
			t.Name = "Test Gen/Bal Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_genbal = "Gen/Bal Test: Not Run";
		private void TestGenBal()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.VFOSplit = false;
			double vfob_freq = console.VFOBFreq;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			console.VFOAFreq = 14.2;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;

			PreampMode preamp = console.RX1PreampMode;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000: console.RX1PreampMode = PreampMode.OFF; break;
				case Model.FLEX3000: console.RX1PreampMode = PreampMode.LOW; break;
			}

			Filter filter = console.RX1Filter;
			int var_low = console.RX1FilterLow;
			int var_high = console.RX1FilterHigh;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = -1000;
			console.RX1FilterHigh = 1000;

			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(true);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(true);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			//Thread.Sleep(50);
			FWC.SetRX1Filter(-1.0f);
			Thread.Sleep(1000);

			float adc_l = 0.0f;
			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				adc_l += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			adc_l /= 5;

			float adc_r = 0.0f;
			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_IMAG);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				adc_r += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_IMAG);
				if(j != 4) Thread.Sleep(50);
			}
			adc_r /= 5;

			
			FWC.SetTest(false);
			//Thread.Sleep(50);
			FWC.SetSig(false);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			Thread.Sleep(500);

			float off_l = 0.0f;
			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				off_l += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			off_l /= 5;

			float target = 0.0f;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000: target = 55.0f; break;
				case Model.FLEX3000: target = 47.0f; break;
			}

			test_genbal = "Gen/Bal Test: ";
			bool b = true;
			if(adc_l - off_l < target)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_L S/N <"+target.ToString("f0")+"dB ("+(adc_l-off_l).ToString("f1")+")");
				test_genbal += "ADC_L S/N <"+target.ToString("f0")+"dB ("+(adc_l-off_l).ToString("f1")+")\n";
				b = false;
			}
			if(adc_r - off_l < target)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_R S/N <"+target.ToString("f0")+"dB ("+(adc_r-off_l).ToString("f1")+")");
				test_genbal += "ADC_R S/N <"+target.ToString("f0")+"dB ("+(adc_r-off_l).ToString("f1")+")\n";
				b = false;
			}
			if(Math.Abs(adc_r - adc_l) > 1.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed Chan Bal >1dB ("+Math.Abs(adc_r-adc_l).ToString("f1")+")");
				test_genbal += "Chan Bal >1dB ("+Math.Abs(adc_r-adc_l).ToString("f1")+")\n";
				b = false;
			}

			switch(console.CurrentModel)
			{
				case Model.FLEX5000: target = -26.6f; break;
				case Model.FLEX3000: target = -27.1f; break;
			}
			if(Math.Abs(target - adc_l) > 1.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_L "+target.ToString("f1")+"+/-1.0 ("+adc_l.ToString("f1")+")");
				test_genbal += "ADC_L "+target.ToString("f1")+"+/-1.0 ("+adc_l.ToString("f1")+")\n";
				b = false;
			}

			switch(console.CurrentModel)
			{
				case Model.FLEX5000: target = -26.1f; break;
				case Model.FLEX3000: target = -26.8f; break;
			}
			if(Math.Abs(target - adc_r) > 1.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_R "+target.ToString("f1")+"+/-1.0 ("+adc_r.ToString("f1")+")");
				test_genbal += "ADC_R "+target.ToString("f1")+"+/-1.0 ("+adc_r.ToString("f1")+")\n";
				b = false;
			}

			if(b)
			{
				btnGenBal.BackColor = Color.Green;
				test_genbal = "Gen/Bal Test: Passed ("+adc_l.ToString("f1")+", "+adc_r.ToString("f1")+")";
				lstDebug.Items.Insert(0, test_genbal);
			}
			else
			{
				btnGenBal.BackColor = Color.Red;
			}
			toolTip1.SetToolTip(btnGenBal, test_genbal);

			console.VFOAFreq = vfoa_freq;
			console.FullDuplex = full_duplex;
			console.VFOBFreq = vfob_freq;
			console.RX1DSPMode = dsp;
			console.RX1PreampMode = preamp;
			console.RX1Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX1FilterLow = var_low;
				console.RX1FilterHigh = var_high;
			}
			btnGenBal.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			/*path += "\\DDS";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);*/
			bool file_exists = File.Exists(path+"\\genbal.csv");
			StreamWriter writer = new StreamWriter(path+"\\genbal.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, ADC_L, ADC_R, Off_L, Passed");
			writer.WriteLine(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+adc_l.ToString("f1")+","+adc_r.ToString("f1")+","+off_l.ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region Noise

		private void btnNoise_Click(object sender, System.EventArgs e)
		{
			btnNoise.Enabled = false;
			btnNoise.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(TestNoise));
			t.Name = "Test Noise Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_noise = "Noise Test: Not Run";
		private void TestNoise()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfob_freq = console.VFOBFreq;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			console.VFOAFreq = 14.2;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.HIGH;

			MeterTXMode tx_meter = console.CurrentMeterTXMode;
			console.CurrentMeterTXMode = MeterTXMode.OFF;

			Filter filter = console.RX1Filter;
			int var_low = console.RX1FilterLow;
			int var_high = console.RX1FilterHigh;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = -1000;
			console.RX1FilterHigh = 1000;

			Thread.Sleep(500);

			float[] a = new float[Display.BUFFER_SIZE];
			float sum = 0.0f;

			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);

			for(int j=0; j<5; j++)
			{
				sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				Thread.Sleep(50);
			}

			float avg = sum / 5;

			Debug.WriteLine("Noise Test: "+avg.ToString("f1")+" dB");
			bool b = (avg > -75.0);
			if(b)
			{
				btnNoise.BackColor = Color.Red;
				test_noise = " Noise Test: Failed dbFS > -75dB ("+avg.ToString("f1")+")";
			}
			else 
			{
				btnNoise.BackColor = Color.Green;
				test_noise = "Noise Test: Passed ("+avg.ToString("f1")+"dBFS)";
			}
			toolTip1.SetToolTip(btnNoise, test_noise);
			lstDebug.Items.Insert(0, test_noise);

			console.CurrentMeterTXMode = tx_meter;
			console.VFOAFreq = vfoa_freq;
			console.FullDuplex = full_duplex;
			console.VFOBFreq = vfob_freq;
			console.RX1DSPMode = dsp;
			console.RX1PreampMode = preamp;
			console.RX1Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX1FilterLow = var_low;
				console.RX1FilterHigh = var_high;
			}
			btnNoise.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			/*path += "\\Noise";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);*/
			path += "\\noise.csv";
			bool file_exists = File.Exists(path);
			StreamWriter writer = new StreamWriter(path, true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, dBFS, Passed");
			writer.WriteLine(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+avg.ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region Impulse

		private void btnImpulse_Click(object sender, System.EventArgs e)
		{
			btnImpulse.Enabled = false;
			btnImpulse.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(TestImpulse));
			t.Name = "Test Impulse Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_impulse = "Impulse Test: Not Run";
		private void TestImpulse()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfob_freq = console.VFOBFreq;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			console.VFOAFreq = 14.2;

			MeterTXMode tx_meter = console.CurrentMeterTXMode;
			console.CurrentMeterTXMode = MeterTXMode.OFF;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.HIGH;

			bool nb = console.NB;
			console.NB = false;

			bool nb2 = console.NB2;
			console.NB2 = false;

			bool polyphase = console.SetupForm.Polyphase;				// save current polyphase setting
			console.SetupForm.Polyphase = false;						// disable polyphase

			Filter filter = console.RX1Filter;
			int var_low = console.RX1FilterLow;
			int var_high = console.RX1FilterHigh;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = -1000;
			console.RX1FilterHigh = 1000;

			FWC.SetTest(true);
			Thread.Sleep(500);

			float[] a = new float[Display.BUFFER_SIZE];
			float sum = 0.0f;

			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);

			for(int j=0; j<5; j++)
			{
				sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				Thread.Sleep(50);
			}

			float noise = sum / 5;
			
			Thread t = new Thread(new ThreadStart(Impulse));
			t.Name = "Impulse Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.AboveNormal;
			t.Start();

			Thread.Sleep(200);

			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);

			sum = 0.0f;
			for(int j=0; j<5; j++)
			{
				sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				Thread.Sleep(50);
			}
			t.Abort();
			FWC.SetTest(false);
			Thread.Sleep(50);

			sum /= 5;

			Debug.WriteLine("Impulse Test: "+(sum-noise).ToString("f1")+" dB");
			bool b = (sum - noise < 6.0);
			if(b)
			{
				btnImpulse.BackColor = Color.Red;
				test_impulse = " Impulse Test: Failed impulse not 6dB+ ("+(sum - noise).ToString("f1")+")";
			}
			else
			{
				btnImpulse.BackColor = Color.Green;
				test_impulse = "Impulse Test: Passed";
			}
			toolTip1.SetToolTip(btnImpulse, test_impulse);
			lstDebug.Items.Insert(0, test_impulse);
			btnImpulse.Enabled = true;

			console.CurrentMeterTXMode = tx_meter;
			console.VFOBFreq = vfob_freq;
			console.FullDuplex = full_duplex;
			console.RX1DSPMode = dsp;
			console.RX1PreampMode = preamp;
			console.RX1FilterLow = var_low;
			console.RX1FilterHigh = var_high;
			console.RX1Filter = filter;
			console.NB = nb;
			console.NB2 = nb2;
			console.SetupForm.Polyphase = polyphase;
            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			/*path += "\\Impulse";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);*/
			path += "\\impulse.csv";
			bool file_exists = File.Exists(path);
			StreamWriter writer = new StreamWriter(path, true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, Impulse, Noise, Diff, Passed");
			writer.WriteLine(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+sum.ToString("f1")+","+noise.ToString("f1")+","+(sum-noise).ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		private void Impulse()
		{
			try
			{
				while(true)
				{
					FWC.SetImpulse(true);
					FWC.SetImpulse(false);
				}
			}
			catch(Exception) {	}
		}

		#endregion

		#region Preamp

		private void btnGenPreamp_Click(object sender, System.EventArgs e)
		{
			btnGenPreamp.Enabled = false;
			btnGenPreamp.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(TestPreamp));
			t.Name = "Test Preamp Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_preamp = "Preamp Test: Not Run";
		private void TestPreamp()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfob_freq = console.VFOBFreq;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			console.VFOAFreq = 14.2;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;

			PreampMode preamp = console.RX1PreampMode;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000: console.RX1PreampMode = PreampMode.OFF; break;
				case Model.FLEX3000: console.RX1PreampMode = PreampMode.LOW; break;
			}

			Filter filter = console.RX1Filter;
			int var_low = console.RX1FilterLow;
			int var_high = console.RX1FilterHigh;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = -1000;
			console.RX1FilterHigh = 1000;

			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(true);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(true);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			Thread.Sleep(1000);

			float[] a = new float[Display.BUFFER_SIZE];
			console.calibration_mutex.WaitOne();
			fixed(float* ptr = &a[0])
				DttSP.GetSpectrum(0, ptr);// get the spectrum values
			console.calibration_mutex.ReleaseMutex();

			float peak = float.MinValue;
			int peak_bin = -1;
			for(int i=0; i<Display.BUFFER_SIZE; i++)
			{
				if(a[i] > peak)
				{
					peak = a[i];
					peak_bin = i;
				}
			}

			console.RX1PreampMode = PreampMode.HIGH;
			Thread.Sleep(500);

			console.calibration_mutex.WaitOne();
			fixed(float* ptr = &a[0])
				DttSP.GetSpectrum(0, ptr);// get the spectrum values
			console.calibration_mutex.ReleaseMutex();

			double target = 0.0;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					target = 14.0;
					break;
				case Model.FLEX3000:
					if(FWCEEPROM.TRXRev>>8 < 6) // before rev G
						target = 24.5;
					else target = 14.4;
					break;
			}

			Debug.WriteLine("Preamp Test: "+(a[peak_bin] - peak).ToString("f1")+" dB");
			bool b = (Math.Abs(target - (a[peak_bin] - peak)) <= 3.0); // > +/- 3.0dB
			if(!b)
			{
				btnGenPreamp.BackColor = Color.Red;
				test_preamp = " Preamp Test: Failed > "+target.ToString("f1")+" +/- 3dB ("+(a[peak_bin]-peak).ToString("f1")+")";
			}
			else
			{
				btnGenPreamp.BackColor = Color.Green;
				test_preamp = "Preamp Test: Passed ("+(a[peak_bin]-peak).ToString("f1")+")";
			}
			toolTip1.SetToolTip(btnGenPreamp, test_preamp);
			lstDebug.Items.Insert(0, test_preamp);

			// end
			FWC.SetTest(false);
			//Thread.Sleep(50);
			FWC.SetSig(false);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(false);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			//Thread.Sleep(50);

			console.VFOAFreq = vfoa_freq;
			console.FullDuplex = full_duplex;
			console.VFOBFreq = vfob_freq;
			console.RX1DSPMode = dsp;
			console.RX1PreampMode = preamp;
			console.RX1Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX1FilterLow = var_low;
				console.RX1FilterHigh = var_high;
			}
			btnGenPreamp.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\preamp.csv");
			StreamWriter writer = new StreamWriter(path+"\\preamp.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, High, Off, Diff, Passed");
			writer.WriteLine(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+peak.ToString("f1")+","+a[peak_bin].ToString("f1")+","+(peak-a[peak_bin]).ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region ATTN

		private void btnGenATTN_Click(object sender, System.EventArgs e)
		{
			btnGenATTN.Enabled = false;
			btnGenATTN.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(TestATTN));
			t.Name = "Test ATTN Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_attn = "Attenuator Test: Not Run";
		private void TestATTN()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfob_freq = console.VFOBFreq;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			console.VFOAFreq = 14.2;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.LOW;

			Filter filter = console.RX1Filter;
			int var_low = console.RX1FilterLow;
			int var_high = console.RX1FilterHigh;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = -1000;
			console.RX1FilterHigh = 1000;

			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(true);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(true);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			Thread.Sleep(1000);

			float[] a = new float[Display.BUFFER_SIZE];
			console.calibration_mutex.WaitOne();
			fixed(float* ptr = &a[0])
				DttSP.GetSpectrum(0, ptr);// get the spectrum values
			console.calibration_mutex.ReleaseMutex();

			float peak = float.MinValue;
			int peak_bin = -1;
			for(int i=0; i<Display.BUFFER_SIZE; i++)
			{
				if(a[i] > peak)
				{
					peak = a[i];
					peak_bin = i;
				}
			}

			console.RX1PreampMode = PreampMode.OFF;
			Thread.Sleep(500);

			console.calibration_mutex.WaitOne();
			fixed(float* ptr = &a[0])
				DttSP.GetSpectrum(0, ptr);// get the spectrum values
			console.calibration_mutex.ReleaseMutex();

			double target = 0.0;
			switch(console.CurrentModel)
			{
				case Model.FLEX3000:
					if(FWCEEPROM.TRXRev>>8 < 6) // before rev G
						target = -20.0;
					else target = -9.0;
					break;
			}

			Debug.WriteLine("ATTN Test: "+(a[peak_bin] - peak).ToString("f1")+" dB");
			bool b = (Math.Abs(target - (a[peak_bin] - peak)) <= 3.0); // > +/- 3.0dB
			if(!b)
			{
				btnGenATTN.BackColor = Color.Red;
				test_attn = " ATTN Test: Failed "+target.ToString("f1")+" +/- 3dB ("+(a[peak_bin]-peak).ToString("f1")+")";
			}
			else
			{
				btnGenATTN.BackColor = Color.Green;
				test_attn = "ATTN Test: Passed ("+(a[peak_bin]-peak).ToString("f1")+")";
			}
			toolTip1.SetToolTip(btnGenATTN, test_attn);
			lstDebug.Items.Insert(0, test_attn);

			// end
			FWC.SetTest(false);
			//Thread.Sleep(50);
			FWC.SetSig(false);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(false);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			//Thread.Sleep(50);

			console.VFOAFreq = vfoa_freq;
			console.FullDuplex = full_duplex;
			console.VFOBFreq = vfob_freq;
			console.RX1DSPMode = dsp;
			console.RX1PreampMode = preamp;
			console.RX1Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX1FilterLow = var_low;
				console.RX1FilterHigh = var_high;
			}
			btnGenATTN.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\attn.csv");
			StreamWriter writer = new StreamWriter(path+"\\attn.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, Off, On, Diff, Passed");
			writer.WriteLine(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+peak.ToString("f1")+","+a[peak_bin].ToString("f1")+","+(peak-a[peak_bin]).ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#endregion

		#region RX Tests

		#region Filter

		private void btnRXFilter_Click(object sender, System.EventArgs e)
		{
			p = new Progress("Test RX Filter");
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnRXFilter.BackColor = Color.Green;
			Thread t = new Thread(new ThreadStart(TestRXFilter));
			t.Name = "Test RX Filter Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
			Invoke(new MethodInvoker(p.Show));
		}

		private string test_rx_filter = "RX Filter Test: Not Run";
		private void TestRXFilter()
		{
			float[] avg = {1.4f, 1.3f, 0.9f, 1.2f, 0.6f, 0.7f, 0.7f, 0.6f, 0.5f, 0.7f, 0.5f};	// avg filter loss in dB
			float[] avg2 = {5.0f, 4.2f, 3.9f, 3.1f, 2.5f, 1.7f, 2.6f, 1.8f, 2.4f, 2.1f, 1.8f}; // avg filter loss for FLEX-3000
			//float tol = 0.5f; // tolerance
			/*if(!console.PowerOn)
			{
				p.Hide();
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);					
				grpGeneral.Enabled = true;
				grpReceiver.Enabled = true;
				grpTransmitter.Enabled = true;
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.VFOSplit = false;
			test_rx_filter = "RX Filter Test: Passed";

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

            int tx_low = console.TXFilterLow;
            console.TXFilterLow = 100;

            int tx_high = console.TXFilterHigh;
            console.TXFilterHigh = 200;

			bool spur_red = console.SpurReduction;
			console.SpurReduction = false;

			string display = console.DisplayModeText;
			console.DisplayModeText = "Spectrum";

			PreampMode preamp = console.RX1PreampMode;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;

			Filter filter = console.RX1Filter;
			console.RX1Filter = Filter.VAR1;

			int filt_high = console.RX1FilterHigh;
			console.RX1FilterHigh = 500;

			int filt_low = console.RX1FilterLow;			
			console.RX1FilterLow = -500;
			
			bool rit_on = console.RITOn;
			console.RITOn = false;

			int dsp_buf_size = console.SetupForm.DSPPhoneRXBuffer;			// save current DSP buffer size
			console.SetupForm.DSPPhoneRXBuffer = 4096;						// set DSP Buffer Size to 2048

			bool polyphase = console.SetupForm.Polyphase;				// save current polyphase setting
			console.SetupForm.Polyphase = false;						// disable polyphase

			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(true);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(true);
			Thread.Sleep(500);

			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			float[] on_table = new float[bands.Length];
			float[] off_table = new float[bands.Length];
			float[] a = new float[Display.BUFFER_SIZE];
			int counter = 0;
			int num_bands = 0;
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}
				if(do_band) num_bands++;
			}

			int total_counts = (10+10)*num_bands;

			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];

					switch(console.CurrentModel)
					{
						case Model.FLEX5000:
							console.RX1PreampMode = PreampMode.OFF;
							break;
						case Model.FLEX3000:
							console.RX1PreampMode = PreampMode.LOW;
							break;
					}
					Thread.Sleep(500);

					float sum = 0.0f;
					for(int j=0; j<10; j++)
					{
						console.calibration_mutex.WaitOne();
						fixed(float* ptr = &a[0])
							DttSP.GetSpectrum(0, ptr);		// read again to clear out changed DSP
						console.calibration_mutex.ReleaseMutex();
						if(j != 9) Thread.Sleep(100);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);

						sum += a[2048];
					}

					float with_filter = sum / 10;

					FWC.SetRX1Filter(-1.0f);
					Thread.Sleep(500);
					sum = 0.0f;
					for(int j=0; j<10; j++)
					{
						console.calibration_mutex.WaitOne();
						fixed(float* ptr = &a[0])
							DttSP.GetSpectrum(0, ptr);		// read again to clear out changed DSP
						console.calibration_mutex.ReleaseMutex();
						if(j != 4) Thread.Sleep(100);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);

						sum += a[2048];
					}
				
					float no_filter = sum / 10;

					on_table[i] = with_filter;
					off_table[i] = no_filter;

					float target = 0.0f;
					switch(console.CurrentModel)
					{
						case Model.FLEX5000:
							target = avg[i];
							break;
						case Model.FLEX3000:
							target = avg2[i];
							break;
					}

					if((no_filter < -10.0f || with_filter < -10.0f) ||
						((no_filter-with_filter) > target+1.0f) ||
						((no_filter-with_filter) < -3.0f))
					{
						btnRXFilter.BackColor = Color.Red;
						if(!test_rx_filter.StartsWith("RX Filter Test: Failed ("))
							test_rx_filter = "RX Filter Test: Failed (";
						test_rx_filter += BandToString(bands[i])+",";
						lstDebug.Items.Insert(0, "RX Filter Test - "+BandToString(bands[i])+": Failed ("
							+no_filter.ToString("f1")+", "+with_filter.ToString("f1")+", "+(no_filter-with_filter).ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "RX Filter Test - "+BandToString(bands[i])+": Passed ("
							+no_filter.ToString("f1")+", "+with_filter.ToString("f1")+", "+(no_filter-with_filter).ToString("f1")+")");
					}

					Debug.Write((no_filter-with_filter).ToString("f1")+" ");
					//Debug.WriteLine(band_freqs[i].ToString("f6")+" diff: "+(no_filter-with_filter).ToString("f1")+"dB");
					FWC.SetRX1Filter(band_freqs[i]);
					//Thread.Sleep(50);
				}
			}
			Debug.WriteLine("");

		end:
			if(test_rx_filter.StartsWith("RX Filter Test: Failed ("))
				test_rx_filter = test_rx_filter.Substring(0, test_rx_filter.Length-1)+")";
			toolTip1.SetToolTip(btnRXFilter, test_rx_filter);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx_filter.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx_filter.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, "
								 +"160m off, 160m on, 160m diff,"
								 +"80m off, 80m on, 80m diff,"
								 +"60m off, 60m on, 60m diff,"
								 +"40m off, 40m on, 40m diff,"
								 +"30m off, 30m on, 30m diff,"
								 +"20m off, 20m on, 20m diff,"
								 +"17m off, 17m on, 17m diff,"
								 +"15m off, 15m on, 15m diff,"
								 +"12m off, 12m on, 12m diff,"
								 +"10m off, 10m on, 10m diff,"
								 +"6m off, 6m on, 6m diff");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(off_table[i].ToString("f1")+",");
				writer.Write(on_table[i].ToString("f1")+",");
				writer.Write((off_table[i]-on_table[i]).ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\RX Filter";
			if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\rx_filter_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, Off, On, Diff");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(off_table[i].ToString("f1")+",");
				writer.Write(on_table[i].ToString("f1")+",");
				writer.WriteLine((off_table[i]-on_table[i]).ToString("f1"));
			}
			writer.Close();

            console.TXFilterLow = tx_low;
            console.TXFilterHigh = tx_high;

			FWC.SetTest(false);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetSig(false);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(false);
			//Thread.Sleep(50);
			console.DisplayModeText = display;
			console.RX1DSPMode = dsp_mode;
			console.RX1FilterHigh = filt_high;
			console.RX1FilterLow = filt_low;
			console.RX1Filter = filter;
			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;
			console.RX1PreampMode = preamp;
			console.RITOn = rit_on;
			console.SetupForm.DSPPhoneRXBuffer = dsp_buf_size;
			console.SetupForm.Polyphase = polyphase;
			console.SpurReduction = spur_red;
			
			Invoke(new MethodInvoker(p.Hide));
			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		#endregion

		#region Level

		private void btnRXLevel_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnRXLevel.BackColor = Color.Green;
			CallCalFWCRXLevel();
		}

		public void CallCalFWCRXLevel()
		{
			p = new Progress("Calibrate RX Level");
			Thread t = new Thread(new ThreadStart(RunCalFWCRXLevel));
			t.Name = "Calibrate RX Level Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_rx_level = "RX Level Test: Not Run";
		public void RunCalFWCRXLevel()
		{
			//float[] offset_avg = {-59.3f, -60.5f, -61.1f, -61.4f, -60.8f, -60.5f, -60.0f, -59.5f, -59.5f, -59.5f, -59.6f};
			//float[] preamp_avg = {-13.4f, -7.0f, -13.3f, -13.6f, -14.0f, -14.0f, -14.0f, -13.8f, -13.8f, -13.8f, -13.7f};
			//float offset_tol = 2.5f;	// maximum distance from the average value
			//float preamp_tol = 1.5f;

			test_rx_level = "RX Level Test: Passed";
			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

            int tx_low = console.TXFilterLow;
            console.TXFilterLow = 100;

            int tx_high = console.TXFilterHigh;
            console.TXFilterHigh = 200;

			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();
			Band[] bands = { Band.B6M, Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M };
			float[] band_freqs = { 50.11f, 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f };

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			float level = (float)udLevel.Value;

			if(console.VFOSync)
				console.VFOSync = false;

			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					p.SetPercent(0.0f);
					Invoke(new MethodInvoker(p.Show));
					Application.DoEvents();
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.CalibrateLevel(level, band_freqs[i], p, true);
					if(p.Text == "") break;

					/*if(Math.Abs(console.GetRX1Level(bands[i], 0)-offset_avg[i]) > offset_tol ||
						Math.Abs(console.GetRX1Level(bands[i], 1)-preamp_avg[i]) > preamp_tol)
					{
						btnRXLevel.BackColor = Color.Red;
						if(!test_rx_level.StartsWith("RX Level Test: Failed ("))
							test_rx_level = "RX Level Test: Failed (";
						test_rx_level += BandToString(bands[i])+", ";
						lstDebug.Items.Insert(0, "RX Level Test - "+BandToString(bands[i])+": Failed ("
							+console.GetRX1Level(bands[i], 0).ToString("f1")+", "
							+console.GetRX1Level(bands[i], 1).ToString("f1")+", "
							+console.GetRX1Level(bands[i], 2).ToString("f1")+")");
					}
					else
					{*/
						lstDebug.Items.Insert(0, "RX Level Test - "+BandToString(bands[i])+": Passed ("
							+console.GetRX1Level(bands[i], 0).ToString("f1")+", "
							+console.GetRX1Level(bands[i], 1).ToString("f1")+", "
							+console.GetRX1Level(bands[i], 2).ToString("f1")+")");
					//}
						
					Thread.Sleep(500);				
				}
			}

            console.TXFilterLow = tx_low;
            console.TXFilterHigh = tx_high;

			if(test_rx_level.StartsWith("RX Level Test: Failed ("))
				test_rx_level = test_rx_level.Substring(0, test_rx_level.Length-2)+")";
			toolTip1.SetToolTip(btnRXLevel, test_rx_level);

			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;

			t1.Stop();
			Debug.WriteLine("RX Level Timer: "+t1.Duration);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx_level.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx_level.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, "
								 +"6m display offset, 6m preamp offset, 6m multimeter offset, "
								 +"160m display offset, 160m preamp offset, 160m multimeter offset, "
								 +"80m display offset, 80m preamp offset, 80m multimeter offset, "
								 +"60m display offset, 60m preamp offset, 60m multimeter offset, "
								 +"40m display offset, 40m preamp offset, 40m multimeter offset, "
								 +"30m display offset, 30m preamp offset, 30m multimeter offset, "
								 +"20m display offset, 20m preamp offset, 20m multimeter offset, "
								 +"17m display offset, 17m preamp offset, 17m multimeter offset, "
								 +"15m display offset, 15m preamp offset, 15m multimeter offset, "
								 +"12m display offset, 12m preamp offset, 12m multimeter offset, "
								 +"10m display offset, 10m preamp offset, 10m multimeter offset");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				for(int j=0; j<3; j++)
				{
					writer.Write(console.GetRX1Level(bands[i], j).ToString("f1"));
					if(i!=bands.Length-1 || j!=2) writer.Write(",");
				}
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\RX Level";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\rx_level_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, Display Offset, Preamp Offset, Multimeter Offset");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(console.GetRX1Level(bands[i], 0).ToString("f1")+",");
				writer.Write(console.GetRX1Level(bands[i], 1).ToString("f1")+",");
				writer.WriteLine(console.GetRX1Level(bands[i], 2).ToString("f1"));
			}
			writer.Close();

			lstDebug.Items.Insert(0, "Saving Level data to EEPROM...");
			byte checksum;
			FWCEEPROM.WriteRXLevel(console.rx1_level_table, out checksum);
			console.rx1_level_checksum = checksum;
			console.SyncCalDateTime();
			lstDebug.Items[0] = "Saving Level data to EEPROM...done";

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		#endregion

		#region RX Image

		private void btnRXImage_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnRXImage.BackColor = Color.Green;
			CallCalFWCRXImage();
		}

		public void CallCalFWCRXImage()
		{
			p = new Progress("Calibrate RX Image");
			Thread t = new Thread(new ThreadStart(RunCalFWCRXImage));
			t.Name = "Calibrate RX Image Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_rx_image = "RX Image Test: Not Run";
		public void RunCalFWCRXImage()
		{
			test_rx_image = "RX Image Test: Passed";

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

            int tx_low = console.TXFilterLow;
            console.TXFilterLow = 100;

            int tx_high = console.TXFilterHigh;
            console.TXFilterHigh = 200;

			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			for(int i=0; i<band_freqs.Length; i++)
			{
                float rejection_tol = 80.0f;	// rejection from worst to null
                float floor_tol = 10.0f;		// from null to noise floor

                if (console.CurrentModel == Model.FLEX3000 &&
                    FWCEEPROM.TRXRev >> 8 < 6) // before rev G
                {
                    switch (bands[i])
                    {
                        case Band.B160M:
                        case Band.B80M:
                        case Band.B60M:
                            rejection_tol = 77.0f;
                            break;
                    }
                }

				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					p.SetPercent(0.0f);
					Invoke(new MethodInvoker(p.Show));
					Application.DoEvents();
					console.VFOAFreq = band_freqs[i]+2*console.IFFreq;
					console.VFOBFreq = band_freqs[i];
					console.CalibrateRXImage(band_freqs[i], p, true);
					if(p.Text == "") break;

					if(console.rx_image_rejection[(int)bands[i]] < rejection_tol ||
						console.rx_image_from_floor[(int)bands[i]] > floor_tol)
					{
						if(!test_rx_image.StartsWith("RX Image Test: Failed ("))
							test_rx_image = "RX Image Test: Failed (";
						test_rx_image += BandToString(bands[i])+",";
						btnRXImage.BackColor = Color.Red;
						lstDebug.Items.Insert(0, "RX Image - "+BandToString(bands[i])+": Failed ("
							+console.rx_image_rejection[(int)bands[i]].ToString("f1")+", "
							+console.rx_image_from_floor[(int)bands[i]].ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "RX Image - "+BandToString(bands[i])+": Passed ("
							+console.rx_image_rejection[(int)bands[i]].ToString("f1")+", "
							+console.rx_image_from_floor[(int)bands[i]].ToString("f1")+")");
					}

					Thread.Sleep(500);
				}
			}

            console.TXFilterLow = tx_low;
            console.TXFilterHigh = tx_high;

			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;
			if(test_rx_image.StartsWith("RX Image Test: Failed ("))
				test_rx_image = test_rx_image.Substring(0, test_rx_image.Length-1)+")";
			toolTip1.SetToolTip(btnRXImage, test_rx_image);

			t1.Stop();
			Debug.WriteLine("RX Image Timer: "+t1.Duration);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx_image.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx_image.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, "
								 +"160m gain, 160m phase, 160m rejection, 160m noise distance, "
								 +"80m gain, 80m phase, 80m rejection, 80m noise distance, "
								 +"60m gain, 60m phase, 60m rejection, 60m noise distance, "
								 +"40m gain, 40m phase, 40m rejection, 40m noise distance, "
								 +"30m gain, 30m phase, 30m rejection, 30m noise distance, "
								 +"20m gain, 20m phase, 20m rejection, 20m noise distance, "
								 +"17m gain, 17m phase, 17m rejection, 17m noise distance, "
								 +"15m gain, 15m phase, 15m rejection, 15m noise distance, "
								 +"12m gain, 12m phase, 12m rejection, 12m noise distance, "
								 +"10m gain, 10m phase, 10m rejection, 10m noise distance, "
								 +"6m gain, 6m phase, 6m rejection, 6m noise distance");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(console.rx1_image_gain_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx1_image_phase_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx_image_rejection[(int)bands[i]].ToString("f1")+",");
				writer.Write(console.rx_image_from_floor[(int)bands[i]].ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\RX Image";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\rx_image_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, Gain, Phase, Rejection, Noise Distance");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(console.rx1_image_gain_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx1_image_phase_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx_image_rejection[(int)bands[i]].ToString("f1")+",");
				writer.WriteLine(console.rx_image_from_floor[(int)bands[i]].ToString("f1"));
			}
			writer.Close();

			lstDebug.Items.Insert(0, "Saving RX Image data to EEPROM...");
			byte gain_sum, phase_sum;
			FWCEEPROM.WriteRXImage(console.rx1_image_gain_table, console.rx1_image_phase_table, out gain_sum, out phase_sum);
			console.rx1_image_gain_checksum = gain_sum;
			console.rx1_image_phase_checksum = phase_sum;
			console.SyncCalDateTime();
			lstDebug.Items[0] = "Saving RX Image data to EEPROM...done";

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		#endregion

		#region MDS

		/*private void btnRXMDS_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnRXMDS.BackColor = Color.Green;
			CallCalFWCRXMDS();
		}

		public void CallCalFWCRXMDS()
		{
			p = new Progress("Calibrate RX MDS");
			Thread t = new Thread(new ThreadStart(RunCalFWCRXMDS));
			t.Name = "Calibrate RX Image Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_rx_mds = "RX MDS Test: Not Run";
		public void RunCalFWCRXMDS()
		{
			if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}
			
			float[] avg = {-128.8f, -133.7f, -134.4f, -135.7f, -134.3f, -131.6f, -132.4f, -132.0f, -131.4f, -131.7f, -129.2f};
			float tol = 20.0f;	// maximum distance from average noise floor
			test_rx_mds = "RX MDS Test: Passed";

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			float[] mds_table = new float[bands.Length];
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = chk160.Checked; break;
					case Band.B80M: do_band = chk80.Checked; break;
					case Band.B60M: do_band = chk60.Checked; break;
					case Band.B40M: do_band = chk40.Checked; break;
					case Band.B30M: do_band = chk30.Checked; break;
					case Band.B20M: do_band = chk20.Checked; break;
					case Band.B17M: do_band = chk17.Checked; break;
					case Band.B15M: do_band = chk15.Checked; break;
					case Band.B12M: do_band = chk12.Checked; break;
					case Band.B10M:	do_band = chk10.Checked; break;
					case Band.B6M: do_band = chk6.Checked; break;
				}

				if(do_band)
				{
					p.SetPercent(0.0f);
					Invoke(new MethodInvoker(p.Show));
					Application.DoEvents();
					console.VFOAFreq = band_freqs[i];
					float val = CheckRXMDS(band_freqs[i], p);
					mds_table[i] = val;
					if(p.Text == "" || !p.Visible) break;

					if(val - avg[i] > tol)
					{
						if(!test_rx_mds.StartsWith("RX MDS Test: Failed ("))
							test_rx_mds = "RX MDS Test: Failed (";
						test_rx_mds += BandToString(bands[i])+", ";
						btnRXMDS.BackColor = Color.Red;
						lstDebug.Items.Insert(0, "RX MDS - "+BandToString(bands[i])+": Failed ("
							+val.ToString("f1")+" > target "+(avg[i]+tol).ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "RX MDS - "+BandToString(bands[i])+": Passed ("
							+val.ToString("f1")+")");
					}

					Thread.Sleep(500);
				}
			}
			
			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;
			if(test_rx_mds.StartsWith("RX MDS Test: Failed ("))
				test_rx_mds = test_rx_mds.Substring(0, test_rx_mds.Length-2)+")";
			toolTip1.SetToolTip(btnRXMDS, test_rx_mds);

			t1.Stop();
			Debug.WriteLine("RX Image Timer: "+t1.Duration);

			string path = app_data_path+"\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx_mds.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx_mds.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, "
								 +"160m, 80m, 60m, 40m, 30m, 20m, 17m, 15m, 12m, 10m, 6m");

			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
				writer.Write(mds_table[i].ToString("f1")+",");
			writer.WriteLine("");
			writer.Close();

			p.Hide();
			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		private float CheckRXMDS(float freq, Progress progress)
		{
			double vfob_freq = console.VFOBFreq;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			console.VFOAFreq = freq;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.HIGH;

			Filter filter = console.RX1Filter;
			int var_low = console.RX1FilterLow;
			int var_high = console.RX1FilterHigh;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = 50;
			console.RX1FilterHigh = 550;

			Thread.Sleep(500);

			float[] a = new float[Display.BUFFER_SIZE];
			float sum = 0.0f;
			int counter = 0;

			for(int i=0; i<50; i++)
			{
				sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
				Thread.Sleep(50);
				if(!progress.Visible)
					return 1.0f;
				else progress.SetPercent((float)((float)++counter/50));
			}
			float avg = sum / 50.0f;

			avg = avg + console.rx1_meter_cal_offset
				+ console.GetRXLevel(console.CurrentBand, 1)
				+ console.RXPathOffset; // adjust for offset;

			console.VFOAFreq = vfoa_freq;
			console.FullDuplex = full_duplex;
			console.VFOBFreq = vfob_freq;
			console.RX1DSPMode = dsp;
			console.RX1PreampMode = preamp;
			console.RX1Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX1FilterLow = var_low;
				console.RX1FilterHigh = var_high;
			}
			
			return avg;
		}*/

		#endregion

		#endregion

		#region TX Tests

		#region Filter

		private void btnTXFilter_Click(object sender, System.EventArgs e)
		{
			p = new Progress("Test TX Filter");
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnTXFilter.BackColor = Color.Green;
			Thread t = new Thread(new ThreadStart(TestTXFilter));
			t.Name = "Test TX Filter Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
			Invoke(new MethodInvoker(p.Show));
		}

		private string test_tx_filter = "TX Filter Test: Not Run";
		private void TestTXFilter()
		{
			float[] avg = {};
			float tol = 1.0f;	// maximum acceptable filter loss in dB
			/*if(!console.PowerOn)
			{
				if(!console.PowerOn)
				{
					p.Hide();
					MessageBox.Show("Power must be on to run this test.",
						"Power not on",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);					
					grpGeneral.Enabled = true;
					grpReceiver.Enabled = true;
					grpTransmitter.Enabled = true;
					return;
				}
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.VFOSplit = false;
			test_tx_filter = "TX Filter Test: Passed";

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

			bool spur_red = console.SpurReduction;
			console.SpurReduction = false;

			string display = console.DisplayModeText;
			console.DisplayModeText = "Spectrum";

			PreampMode preamp = console.RX1PreampMode;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			Filter filter = console.RX1Filter;
			console.RX1Filter = Filter.VAR1;

			int filt_high = console.RX1FilterHigh;
			console.RX1FilterHigh = 700;	

			int filt_low = console.RX1FilterLow;			
			console.RX1FilterLow = 500;
			
			console.UpdateRX1Filters(500, 700);
			
			bool rit_on = console.RITOn;
			console.RITOn = false;

			int dsp_buf_size = console.SetupForm.DSPPhoneRXBuffer;			// save current DSP buffer size
			console.SetupForm.DSPPhoneRXBuffer = 4096;						// set DSP Buffer Size to 2048

			bool polyphase = console.SetupForm.Polyphase;				// save current polyphase setting
			console.SetupForm.Polyphase = false;						// disable polyphase

			double tone_scale = Audio.SourceScale;
			Audio.SourceScale = 1.0;

			double tone_freq = Audio.SineFreq1;				// save tone freq
			Audio.SineFreq1 = 600.0;						// set freq

			Audio.TXInputSignal = Audio.SignalSource.SINE;

			console.FullDuplex = true;
			//Thread.Sleep(50);
			FWC.SetQSD(true);
			//Thread.Sleep(50);
			FWC.SetQSE(true);
			//Thread.Sleep(50);
			FWC.SetTR(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			Thread.Sleep(500);
			
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			float[] on_table = new float[bands.Length];
			float[] off_table = new float[bands.Length];
			float[] a = new float[Display.BUFFER_SIZE];
			int counter = 0;
			int num_bands = 0;
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}
				if(do_band) num_bands++;
			}

			int total_counts = (5+5)*num_bands;

			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.RX1PreampMode = PreampMode.OFF;
					Audio.RadioVolume = 0.075;
					Thread.Sleep(500);

					int peak_bin = -1;
					float max = float.MinValue;
					float sum = 0.0f;
					for(int j=0; j<5; j++)
					{
						console.calibration_mutex.WaitOne();
						fixed(float* ptr = &a[0])
							DttSP.GetSpectrum(0, ptr);		// read again to clear out changed DSP
						console.calibration_mutex.ReleaseMutex();
						if(j != 4) Thread.Sleep(100);
						if(!p.Visible) goto end;
						p.SetPercent(++counter/(float)total_counts);

						if(j==0)
						{
							for(int k=0; k<Display.BUFFER_SIZE; k++)
							{
								if(a[k] > max)
								{
									max = a[k];
									peak_bin = k;
								}
							}
						}

						sum += a[peak_bin];
					}

					float with_filter = sum / 5;

					FWC.SetTXFilter(-1.0f);
					Thread.Sleep(500);
					sum = 0.0f;
					for(int j=0; j<5; j++)
					{
						console.calibration_mutex.WaitOne();
						fixed(float* ptr = &a[0])
							DttSP.GetSpectrum(0, ptr);		// read again to clear out changed DSP
						console.calibration_mutex.ReleaseMutex();
						if(j != 4) Thread.Sleep(100);
						if(!p.Visible) goto end;
						p.SetPercent(++counter/(float)total_counts);

						sum += a[peak_bin];
					}
				
					float no_filter = sum / 5;

					on_table[i] = with_filter;
					off_table[i] = no_filter;

					if((with_filter < -10.0f || no_filter < -10.0f) ||
						((no_filter-with_filter) > tol) ||
						((no_filter-with_filter) < -0.5f))
					{
						btnTXFilter.BackColor = Color.Red;
						if(!test_tx_filter.StartsWith("TX Filter Test: Failed ("))
							test_tx_filter = "TX Filter Test: Failed (";
						test_tx_filter += BandToString(bands[i])+",";
						lstDebug.Items.Insert(0, "TX Filter Test - "+BandToString(bands[i])+": Failed ("
							+no_filter.ToString("f1")+", "+with_filter.ToString("f1")+", "+(no_filter-with_filter).ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "TX Filter Test - "+BandToString(bands[i])+": Passed ("
							+no_filter.ToString("f1")+", "+with_filter.ToString("f1")+", "+(no_filter-with_filter).ToString("f1")+")");
					}

					Debug.Write((no_filter-with_filter).ToString("f1")+" ");
					//Debug.WriteLine(band_freqs[i].ToString("f6")+" diff: "+(no_filter-with_filter).ToString("f1")+"dB");
					FWC.SetTXFilter(band_freqs[i]);
					//Thread.Sleep(50);
				}
			}
			Debug.WriteLine("");

			end:
				if(test_tx_filter.StartsWith("TX Filter Test: Failed ("))
					test_tx_filter = test_tx_filter.Substring(0, test_tx_filter.Length-1)+")";
			toolTip1.SetToolTip(btnTXFilter, test_tx_filter);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\tx_filter.csv");
			StreamWriter writer = new StreamWriter(path+"\\tx_filter.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version"
								 +"160m off, 160m on, 160m diff,"
								 +"80m off, 80m on, 80m diff,"
								 +"60m off, 60m on, 60m diff,"
								 +"40m off, 40m on, 40m diff,"
								 +"30m off, 30m on, 30m diff,"
								 +"20m off, 20m on, 20m diff,"
								 +"17m off, 17m on, 17m diff,"
								 +"15m off, 15m on, 15m diff,"
								 +"12m off, 12m on, 12m diff,"
								 +"10m off, 10m on, 10m diff,"
								 +"6m off, 6m on, 6m diff");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(off_table[i].ToString("f1")+",");
				writer.Write(on_table[i].ToString("f1")+",");
				writer.Write((off_table[i]-on_table[i]).ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\TX Filter";
			if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\tx_filter_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, Off, On, Diff");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(off_table[i].ToString("f1")+",");
				writer.Write(on_table[i].ToString("f1")+",");
				writer.WriteLine((off_table[i]-on_table[i]).ToString("f1"));
			}
			writer.Close();

			FWC.SetTest(false);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetSig(false);
			//Thread.Sleep(50);
			console.FullDuplex = false;
			Audio.TXInputSignal = Audio.SignalSource.RADIO;
			Audio.SourceScale = tone_scale;
			Audio.SineFreq1 = tone_freq;
			console.DisplayModeText = display;
			console.RX1DSPMode = dsp_mode;
			console.RX1FilterHigh = filt_high;
			console.RX1FilterLow = filt_low;
			console.RX1Filter = filter;
			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;
			console.RX1PreampMode = preamp;
			console.RITOn = rit_on;
			console.SetupForm.DSPPhoneRXBuffer = dsp_buf_size;
			console.SetupForm.Polyphase = polyphase;
			console.SpurReduction = spur_red;
			
			Invoke(new MethodInvoker(p.Hide));
			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		#endregion

		#region Image

		private string test_tx_image = "TX Image Test: Not Run";
		private void btnTXImage_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnTXImage.BackColor = Color.Green;
			CallCalFWCTXImage();
		}

		public void CallCalFWCTXImage()
		{
			p = new Progress("Calibrate TX Image");
			Thread t = new Thread(new ThreadStart(RunCalFWCTXImage));
			t.Name = "Calibrate TX Image Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		public void RunCalFWCTXImage()
		{
			float tol = -55.0f;
			test_tx_image = "TX Image Test: Passed";

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					p.SetPercent(0.0f);
					Invoke(new MethodInvoker(p.Show));
					Application.DoEvents();
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.CalibrateTXImage(band_freqs[i], p, true);
					if(p.Text == "") break;

					if(console.tx_image_rejection[(int)bands[i]] > tol)
					{
						if(!test_tx_image.StartsWith("TX Image Test: Failed ("))
							test_tx_image = "TX Image Test: Failed (";
						test_tx_image += BandToString(bands[i])+",";
						btnTXImage.BackColor = Color.Red;
						lstDebug.Items.Insert(0, "TX Image - "+BandToString(bands[i])+": Failed ("
							+console.tx_image_rejection[(int)bands[i]].ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "TX Image - "+BandToString(bands[i])+": Passed ("
							+console.tx_image_rejection[(int)bands[i]].ToString("f1")+")");
					}

					Thread.Sleep(500);
				}
			}
			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;

			t1.Stop();
			Debug.WriteLine("TX Image Timer: "+t1.Duration);

			if(test_tx_image.StartsWith("TX Image Test: Failed ("))
				test_tx_image = test_tx_image.Substring(0, test_tx_image.Length-1)+")";
			toolTip1.SetToolTip(btnTXImage, test_tx_image);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\tx_image.csv");
			StreamWriter writer = new StreamWriter(path+"\\tx_image.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, "
								 +"160m gain, 160m phase, 160m rejection, "
								 +"80m gain, 80m phase, 80m rejection, "
								 +"60m gain, 60m phase, 60m rejection, "
								 +"40m gain, 40m phase, 40m rejection, "
								 +"30m gain, 30m phase, 30m rejection, "
								 +"20m gain, 20m phase, 20m rejection, "
								 +"17m gain, 17m phase, 17m rejection, "
								 +"15m gain, 15m phase, 15m rejection, "
								 +"12m gain, 12m phase, 12m rejection, "
								 +"10m gain, 10m phase, 10m rejection, "
								 +"6m gain, 6m phase, 6m rejection, ");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(console.tx_image_gain_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.tx_image_phase_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.tx_image_rejection[(int)bands[i]].ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\TX Image";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\tx_image_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, Gain, Phase, Rejection dBc");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(console.tx_image_gain_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.tx_image_phase_table[(int)bands[i]].ToString("f3")+",");
				writer.WriteLine(console.tx_image_rejection[(int)bands[i]].ToString("f1"));
			}
			writer.Close();

			lstDebug.Items.Insert(0, "Saving TX Image data to EEPROM...");
			byte gain_sum, phase_sum;
			FWCEEPROM.WriteTXImage(console.tx_image_gain_table, console.tx_image_phase_table, out gain_sum, out phase_sum);
			console.tx_image_gain_checksum = gain_sum;
			console.tx_image_phase_checksum = phase_sum;
			console.SyncCalDateTime();
			lstDebug.Items[0] = "Saving TX Image data to EEPROM...done";

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		#endregion

		#region Carrier

		private string test_tx_carrier = "TX Carrier Test: Not Run";
		private void btnTXCarrier_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnTXCarrier.BackColor = Color.Green;
			CallCalFWCTXCarrier();
		}

		public void CallCalFWCTXCarrier()
		{
			p = new Progress("Calibrate TX Carrier");
			Thread t = new Thread(new ThreadStart(RunCalFWCTXCarrier));
			t.Name = "Run Calibrate TX Carrier Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		public void RunCalFWCTXCarrier()
		{
			float tol = -105.0f;
			test_tx_carrier = "TX Carrier Test: Passed";

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

            int tx_low = console.TXFilterLow;
            console.TXFilterLow = 100;

            int tx_high = console.TXFilterHigh;
            console.TXFilterHigh = 200;

			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					p.SetPercent(0.0f);
					Invoke(new MethodInvoker(p.Show));
					Application.DoEvents();
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.CalibrateTXCarrier(band_freqs[i], p, true);
					//if(console.min_tx_carrier[(int)bands[i]] > tol) // try again
					//	console.CalibrateTXCarrier(band_freqs[i], p, true);

					if(p.Text == "") break;

					if(console.min_tx_carrier[(int)bands[i]] > tol)
					{
						if(!test_tx_carrier.StartsWith("TX Carrier Test: Failed ("))
							test_tx_carrier = "TX Carrier Test: Failed (";
						test_tx_carrier += BandToString(bands[i])+",";
						btnTXCarrier.BackColor = Color.Red;
						lstDebug.Items.Insert(0, "TX Carrier - "+BandToString(bands[i])+": Failed ("
							+console.min_tx_carrier[(int)bands[i]].ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "TX Carrier - "+BandToString(bands[i])+": Passed ("
							+console.min_tx_carrier[(int)bands[i]].ToString("f1")+")");
					}
					Thread.Sleep(500);				
				}
			}

            console.TXFilterLow = tx_low;
            console.TXFilterHigh = tx_high;

			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;

			if(test_tx_carrier.StartsWith("TX Carrier Test: Failed ("))
				test_tx_carrier = test_tx_carrier.Substring(0, test_tx_carrier.Length-1)+")";
			toolTip1.SetToolTip(btnTXCarrier, test_tx_carrier);

			t1.Stop();
			Debug.WriteLine("TX Carrier Timer: "+t1.Duration);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\tx_carrier.csv");
			StreamWriter writer = new StreamWriter(path+"\\tx_carrier.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version, "
								 +"160m C0, 160m C1, 160m C2, 160m C3, 160m From Noise, "
								 +"80m C0, 80m C1, 80m C2, 80m C3, 80m From Noise, "
								 +"60m C0, 60m C1, 60m C2, 60m C3, 60m From Noise, "
								 +"40m C0, 40m C1, 40m C2, 40m C3, 40m From Noise, "
								 +"30m C0, 30m C1, 30m C2, 30m C3, 30m From Noise, "
								 +"20m C0, 20m C1, 20m C2, 20m C3, 20m From Noise, "
								 +"17m C0, 17m C1, 17m C2, 17m C3, 17m From Noise, "
								 +"15m C0, 15m C1, 15m C2, 15m C3, 15m From Noise, "
								 +"12m C0, 12m C1, 12m C2, 12m C3, 12m From Noise, "
								 +"10m C0, 10m C1, 10m C2, 10m C3, 10m From Noise, "
								 +"6m C0, 6m C1, 6m C2, 6m C3, 6m From Noise");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				for(int j=0; j<4; j++)
					writer.Write(console.tx_carrier_table[(int)bands[i]][j].ToString()+",");
				writer.Write(console.min_tx_carrier[(int)bands[i]].ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\TX Carrier";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\tx_carrier_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, C0, C1, C2, C3, From Noise");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				for(int j=0; j<4; j++)
					writer.Write(console.tx_carrier_table[(int)bands[i]][j].ToString()+",");
				writer.WriteLine(console.min_tx_carrier[(int)bands[i]].ToString("f1"));
			}
			writer.Close();

			lstDebug.Items.Insert(0, "Saving Carrier data to EEPROM...");
			byte checksum;
			FWCEEPROM.WriteTXCarrier(console.tx_carrier_table, out checksum);
			console.tx_carrier_checksum = checksum;
			console.SyncCalDateTime();
			lstDebug.Items[0] = "Saving Carrier data to EEPROM...done";

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}	

		#endregion

		#region Gain

		private void btnTXGain_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnTXGain.BackColor = Color.Green;

			p = new Progress("Test TX Gain");
			Thread t = new Thread(new ThreadStart(TestTXGain));
			t.Name = "Test TX Gain Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
			Invoke(new MethodInvoker(p.Show));
		}

		private string test_tx_gain = "TX Gain Test: Not Run";
		private void TestTXGain()
		{
			float tol = 1.0f;	// maximum acceptable filter loss in dB
			float[] target = { -26.3f, -25.6f, -25.9f, -24.7f, -25.3f, -26.1f, -27.2f, -27.4f, -27.9f, -28.5f, -36.2f };

			/*if(!console.PowerOn)
			{
				if(!console.PowerOn)
				{
					p.Hide();
					MessageBox.Show("Power must be on to run this test.",
						"Power not on",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);					
					grpGeneral.Enabled = true;
					grpReceiver.Enabled = true;
					grpTransmitter.Enabled = true;
					return;
				}
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			float[] gain_table = new float[bands.Length];

			int counter = 0;
			int num_bands = 0;
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}
				if(do_band) num_bands++;
			}

			int total_counts = (5)*num_bands;

			console.VFOSplit = false;
			test_tx_gain = "TX Gain Test: Passed";

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

			bool spur_red = console.SpurReduction;
			console.SpurReduction = true;

			bool rx2 = console.RX2Enabled;
			console.RX2Enabled = false;

			string display = console.DisplayModeText;
			console.DisplayModeText = "Panadapter";

			PreampMode preamp = console.RX1PreampMode;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			Filter filter = console.RX1Filter;
			console.RX1Filter = Filter.VAR1;

			int filt_high = console.RX1FilterHigh;
			console.RX1FilterHigh = 800;	

			int filt_low = console.RX1FilterLow;			
			console.RX1FilterLow = 400;
			
			console.UpdateRX1Filters(400, 800);

			bool rit_on = console.RITOn;
			console.RITOn = false;

			int dsp_buf_size = console.SetupForm.DSPPhoneRXBuffer;			// save current DSP buffer size
			console.SetupForm.DSPPhoneRXBuffer = 4096;						// set DSP Buffer Size to 2048

			bool polyphase = console.SetupForm.Polyphase;				// save current polyphase setting
			console.SetupForm.Polyphase = false;						// disable polyphase

			MeterRXMode rx_meter = console.CurrentMeterRXMode;			// save current RX Meter mode
			console.CurrentMeterRXMode = MeterRXMode.OFF;				// turn RX Meter off

			double tone_scale = Audio.SourceScale;
			Audio.SourceScale = 0.050;

			double tone_freq = Audio.SineFreq1;				// save tone freq
			Audio.SineFreq1 = 600.0;						// set freq

			Audio.TXOutputSignal = Audio.SignalSource.SINE;

			console.FullDuplex = true;
			//Thread.Sleep(50);
			FWC.SetQSD(true);
			//Thread.Sleep(50);
			FWC.SetQSE(true);
			//Thread.Sleep(50);
			FWC.SetTR(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetTXMon(false);
			//Thread.Sleep(50);
			FWC.SetRX1Filter(-1.0f);
			//Thread.Sleep(50);
			FWC.SetTXFilter(-1.0f);
			Thread.Sleep(500);

			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.RX1PreampMode = PreampMode.OFF;
					Thread.Sleep(500);

					float[] gain = new float[5];
					for(int ii=0; ii<5; ii++) gain[ii] = 0.0f;

					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						gain[j] = DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) goto end;
						p.SetPercent(++counter/(float)total_counts);
					}
					
					Array.Sort(gain);

					//gain += console.rx1_meter_cal_offset;
					gain_table[i] = gain[2];

					if(Math.Abs(gain[2]-target[i]) > tol)
					{
						btnTXGain.BackColor = Color.Red;
						if(!test_tx_gain.StartsWith("TX Gain Test: Failed ("))
							test_tx_gain = "TX Gain Test: Failed (";
						test_tx_gain += BandToString(bands[i])+",";
						lstDebug.Items.Insert(0, "TX Gain Test - "+BandToString(bands[i])+": Failed ("
							+gain[2].ToString("f1")+", "+(gain[2]-target[i]).ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "TX Gain Test - "+BandToString(bands[i])+": Passed ("
							+gain[2].ToString("f1")+", "+(gain[2]-target[i]).ToString("f1")+")");
					}

					//Debug.WriteLine(gain.ToString("f1"));
				}
			}

			end:
				if(test_tx_gain.StartsWith("TX Gain Test: Failed ("))
					test_tx_gain = test_tx_gain.Substring(0, test_tx_gain.Length-1)+")";
			toolTip1.SetToolTip(btnTXGain, test_tx_gain);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\tx_gain.csv");
			StreamWriter writer = new StreamWriter(path+"\\tx_gain.csv", true);
			if(!file_exists) writer.WriteLine("Model, Serial Num, Date/Time, Version"
								 +"160m,"
								 +"80m,"
								 +"60m,"
								 +"40m,"
								 +"30m,"
								 +"20m,"
								 +"17m,"
								 +"15m,"
								 +"12m,"
								 +"10m,"
								 +"6m");
			writer.Write(console.CurrentModel.ToString()+","
				+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(gain_table[i].ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\TX Gain";
			if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			string model = "";
			switch(console.CurrentModel)
			{
				case Model.FLEX3000: model = "F3K"; break;
				case Model.FLEX5000: model = "F5K"; break;
			}
			writer = new StreamWriter(path+"\\tx_gain_"+model+"_"+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+".csv");
			writer.WriteLine("Band, Gain");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(gain_table[i].ToString("f1")+",");
			}
			writer.Close();

			FWC.SetRX1Filter((float)vfoa);
			//Thread.Sleep(50);
			FWC.SetTXFilter((float)vfoa);
			//Thread.Sleep(50);

			FWC.SetTest(false);
			//Thread.Sleep(50);
			FWC.SetGen(false);
			//Thread.Sleep(50);
			FWC.SetSig(false);
			//Thread.Sleep(50);
			console.FullDuplex = false;
			Audio.TXOutputSignal = Audio.SignalSource.RADIO;
			Audio.SourceScale = tone_scale;
			Audio.SineFreq1 = tone_freq;
			console.DisplayModeText = display;
			console.RX1DSPMode = dsp_mode;
			console.RX1FilterHigh = filt_high;
			console.RX1FilterLow = filt_low;
			console.RX1Filter = filter;
			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;
			console.RX1PreampMode = preamp;
			console.RITOn = rit_on;
			console.SetupForm.DSPPhoneRXBuffer = dsp_buf_size;
			console.SetupForm.Polyphase = polyphase;
			console.SpurReduction = spur_red;
			console.CurrentMeterRXMode = rx_meter;
			
			Invoke(new MethodInvoker(p.Hide));
			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
		}

		#endregion

        #endregion

        #region Run Selected Tests

        private void btnRunSelectedTests_Click(object sender, System.EventArgs e)
		{
			console.PowerOn = true;
			btnRunSelectedTests.BackColor = console.ButtonSelectedColor;
			btnRunSelectedTests.Enabled = false;
			Thread t = new Thread(new ThreadStart(RunSelectedTests));
			t.Name = "Run All Tests Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void RunSelectedTests()
		{
			bool rx2 = console.RX2Enabled;
			console.RX2Enabled = false;

			console.VFOSplit = false;
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();

			string start_bands = GetStringFromBands();

			#region Gen

			// run all general tests
			if(ckTestGenPLL.Checked)
			{
				Invoke(new MethodInvoker(btnPLL.PerformClick));
				Thread.Sleep(1000);
			}

			if(ckTestGenBal.Checked)
			{
				Invoke(new MethodInvoker(btnGenBal.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenNoise.Checked)
			{
				Invoke(new MethodInvoker(btnNoise.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenImpulse.Checked)
			{
				Invoke(new MethodInvoker(btnImpulse.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenPreamp.Checked)
			{
				Invoke(new MethodInvoker(btnGenPreamp.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenATTN.Checked)
			{
				Invoke(new MethodInvoker(btnGenATTN.PerformClick));
				Thread.Sleep(3000);
			}

			// re-run any Gen tests that failed
			if(ckTestGenPLL.Checked && btnPLL.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnPLL.PerformClick));
				Thread.Sleep(1000);
			}

			if(ckTestGenBal.Checked && btnGenBal.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnGenBal.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenNoise.Checked && btnNoise.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnNoise.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenImpulse.Checked && btnImpulse.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnImpulse.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenPreamp.Checked && btnGenPreamp.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnGenPreamp.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckTestGenATTN.Checked && btnGenATTN.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnGenATTN.PerformClick));
				Thread.Sleep(3000);
			}

			if((ckTestGenPLL.Checked && btnPLL.BackColor != Color.Green) ||
				(ckTestGenBal.Checked && btnGenBal.BackColor != Color.Green) ||
				(ckTestGenPreamp.Checked && btnGenPreamp.BackColor != Color.Green) ||
				(ckTestGenATTN.Checked && btnGenATTN.BackColor != Color.Green))
				goto end;

			#endregion

			#region RX

			// run all RX tests
			if(ckTestRXFilter.Checked)
			{
				Invoke(new MethodInvoker(btnRXFilter.PerformClick));
				while(true)
				{
					while(p.Visible) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(!p.Visible) break;
				}
				if(p.Text == "") goto end;
			}

			// re-run any RX tests that failed
			if(ckTestRXFilter.Checked && btnRXFilter.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnRXFilter));
				Invoke(new MethodInvoker(btnRXFilter.PerformClick));
				while(true)
				{
					while(p.Visible) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(!p.Visible) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestRXImage.Checked)
			{
				Invoke(new MethodInvoker(btnRXImage.PerformClick));
				while(true)
				{
					while(!btnRXImage.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnRXImage.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestRXImage.Checked && btnRXImage.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnRXImage));
				Invoke(new MethodInvoker(btnRXImage.PerformClick));
				while(true)
				{
					while(!btnRXImage.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnRXImage.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestRXLevel.Checked)
			{
				Invoke(new MethodInvoker(btnRXLevel.PerformClick));
				while(true)
				{
					while(!btnRXLevel.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnRXLevel.Enabled) break;
				}
				if(p.Text == "") goto end;
			}			

			if(ckTestRXLevel.Checked && btnRXLevel.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnRXLevel));
				Invoke(new MethodInvoker(btnRXLevel.PerformClick));
				while(true)
				{
					while(!btnRXLevel.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnRXLevel.Enabled) break;
				}
				if(p.Text == "") goto end;
			}			

			// reset bands back to start
			SetBandFromString(start_bands);

			/*if((ckTestRXFilter.Checked && btnRXFilter.BackColor != Color.Green) ||
				(ckTestRXLevel.Checked && btnRXLevel.BackColor != Color.Green) ||
				(ckTestRXImage.Checked && btnRXImage.BackColor != Color.Green) ||
				(ckTestRXMDS.Checked && btnRXMDS.BackColor != Color.Green))
				goto end;*/

			#endregion

			#region TX

			// run all TX tests
			if(ckTestTXFilter.Checked)
			{
				Invoke(new MethodInvoker(btnTXFilter.PerformClick));
				while(true)
				{
					while(p.Visible) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(!p.Visible) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestTXCarrier.Checked)
			{
				Invoke(new MethodInvoker(btnTXCarrier.PerformClick));
				while(true)
				{
					while(!btnTXCarrier.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnTXCarrier.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestTXImage.Checked)
			{
				Invoke(new MethodInvoker(btnTXImage.PerformClick));
				while(true)
				{
					while(!btnTXImage.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnTXImage.Enabled) break;
				}
				if(p.Text == "") goto end;
			}


			if(ckTestTXGain.Checked)
			{
				Invoke(new MethodInvoker(btnTXGain.PerformClick));
				while(true)
				{
					while(!btnTXGain.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnTXGain.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			// re-run any TX tests that failed
			if(ckTestTXFilter.Checked && btnTXFilter.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnTXFilter));
				Invoke(new MethodInvoker(btnTXFilter.PerformClick));
				while(true)
				{
					while(p.Visible) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(!p.Visible) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestTXImage.Checked && btnTXImage.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnTXImage));
				Invoke(new MethodInvoker(btnTXImage.PerformClick));
				while(true)
				{
					while(!btnTXImage.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnTXImage.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestTXCarrier.Checked && btnTXCarrier.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnTXCarrier));
				Invoke(new MethodInvoker(btnTXCarrier.PerformClick));
				while(true)
				{
					while(!btnTXCarrier.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnTXCarrier.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckTestTXGain.Checked && btnTXGain.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnTXGain));
				Invoke(new MethodInvoker(btnTXGain.PerformClick));
				while(true)
				{
					while(!btnTXGain.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnTXGain.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			SetBandFromString(start_bands);

			#endregion

			end:
				btnRunSelectedTests.BackColor = SystemColors.Control;
			btnRunSelectedTests.Enabled = true;
			console.RX2Enabled = rx2;

			t1.Stop();
			MessageBox.Show("Run All Tests Time: "+((int)(t1.Duration/60)).ToString()+":"+(((int)t1.Duration)%60).ToString("00"));
		}

		#endregion

		#region IO Tests

		#region Pwr Spkr

		private void btnIOPwrSpkr_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOPwrSpkr.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOPwrSpkr));
			t.Name = "IO Pwr Spkr Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}		

		private string test_io_pwrspkr = "IO Pwr Spkr Test: Not Run";
		private void CheckIOPwrSpkr()
		{			
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on in order to run this test.", "Power Is Off",
					MessageBoxButtons.OK, MessageBoxIcon.Stop);
				grpIO.Enabled = true;
				grpGeneral.Enabled = true;
				grpReceiver.Enabled = true;
				grpTransmitter.Enabled = true;
				btnIOPwrSpkr.BackColor = Color.Red;
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.MOX = false;

			console.FullDuplex = true;

			double scale = Audio.SourceScale;
			Audio.SourceScale = 0.1;

			double monitor = Audio.MonitorVolume;
			Audio.MonitorVolume = 1.0;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			int in_tx_l = Audio.IN_TX_L;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					Audio.IN_TX_L = 6;
					break;
				case Model.FLEX3000:
					Audio.IN_TX_L = 3;
					break;
			}

			int reg_11 = 0;
			FWC.ReadCodecReg(0x11, out reg_11);
			FWC.WriteCodecReg(0x11, 0x80);

			int reg_12 = 0;
			FWC.ReadCodecReg(0x12, out reg_12);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.WriteCodecReg(0x12, 0x80);
					break;
				case Model.FLEX3000:
					FWC.WriteCodecReg(0x12, 0x30); // set input side of mixer
					break;
			}

			int reg_13 = 0;
			FWC.ReadCodecReg(0x13, out reg_13);
			FWC.WriteCodecReg(0x13, 0x80);

			int reg_14 = 0; 
			FWC.ReadCodecReg(0x14, out reg_14); 
			FWC.WriteCodecReg(0x14, 0x80);

			int reg_15 = 0; 
			FWC.ReadCodecReg(0x15, out reg_15);
			
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.WriteCodecReg(0x15, 0x30); // set input side of mixer
					break;
				case Model.FLEX3000:
					FWC.WriteCodecReg(0x15, 0x80);
					break;
			}

			int reg_16 = 0;
			FWC.ReadCodecReg(0x16, out reg_16);
			FWC.WriteCodecReg(0x16, 0x80);

			int reg_7 = 0;
			FWC.ReadCodecReg(7, out reg_7);
			FWC.WriteCodecReg(7, 0xCF);

			int reg_CD = 0;
			FWC.ReadCodecReg(0x0C, out reg_CD);
			FWC.WriteCodecReg(0x0C, 0);
			FWC.WriteCodecReg(0x0D, 0);
			Audio.RX1OutputSignal = Audio.SignalSource.SILENCE;
			Thread.Sleep(500);

			float sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float off = sum / 5;

			float on=0.0f, unbal=0.0f, left=0.0f, right=0.0f;
			bool b = true;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					Audio.RX1OutputSignal = Audio.SignalSource.SINE;
					Thread.Sleep(500);
					sum = 0.0f;
					DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
						Thread.Sleep(50);
					}
					on = sum / 5;

					Audio.RX1OutputSignal = Audio.SignalSource.SAWTOOTH;
					Thread.Sleep(500);

					sum = 0.0f;
					DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
						Thread.Sleep(50);
					}
					unbal = sum / 5;

					if(on-off < 50.0f) b = false;
					if(on-unbal < 20.0f) b = false;
					break;
				case Model.FLEX3000:
					Audio.RX1OutputSignal = Audio.SignalSource.SINE_LEFT_ONLY;
					Thread.Sleep(500);
					sum = 0.0f;
					DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
						Thread.Sleep(50);
					}
					left = sum / 5;

					Audio.RX1OutputSignal = Audio.SignalSource.SINE_RIGHT_ONLY;
					Thread.Sleep(500);
					sum = 0.0f;
					DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
						Thread.Sleep(50);
					}
					right = sum / 5;
				
					if(right-off < 50.0f) b = false;
					if(left-off < 50.0f) b = false;

					on = left; unbal = right;
					break;
			}

			if(b)
			{
				btnIOPwrSpkr.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Pwr Spkr - Passed: ("+off.ToString("f1")+", "+on.ToString("f1")+", "+unbal.ToString("f1")+")");
				test_io_pwrspkr = "IO Pwr Spkr Test: Passed";
			}
			else
			{
				btnIOPwrSpkr.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Pwr Spkr - Failed: ("+off.ToString("f1")+", "+on.ToString("f1")+", "+unbal.ToString("f1")+")");
				test_io_pwrspkr = "IO Pwr Spkr Test: Failed";
			}
			toolTip1.SetToolTip(btnIOPwrSpkr, test_io_pwrspkr);
            
			// end
			Audio.RX1OutputSignal = Audio.SignalSource.RADIO;
			Audio.IN_TX_L = in_tx_l;
			FWC.WriteCodecReg(0x11, reg_11);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x12, reg_12);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x13, reg_13);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x14, reg_14);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x15, reg_15);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x16, reg_16);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(7, reg_7);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0C, reg_CD);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0D, reg_CD);
			//Thread.Sleep(50);
			Audio.SourceScale = scale;
			console.FullDuplex = false;
			console.RX1DSPMode = dsp_mode;
			Audio.MonitorVolume = monitor;

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_pwrspkr.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_pwrspkr.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Off, Out Of Phase, In Phase, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			
			writer.Write(off.ToString("f1")+",");
			writer.Write(on.ToString("f1")+",");
			writer.Write(unbal.ToString("f1")+",");
			writer.WriteLine(b.ToString());
			writer.Close();
		}

		#endregion

		#region RCA In/Out

		private void btnIORCAInOut_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIORCAInOut.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIORCAInOut));
			t.Name = "IO RCA In/Out Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_rcainout = "IO RCA In/Out Test: Not Run";
		private void CheckIORCAInOut()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on in order to run this test.", "Power Is Off",
					MessageBoxButtons.OK, MessageBoxIcon.Stop);
				grpIO.Enabled = true;
				grpGeneral.Enabled = true;
				grpReceiver.Enabled = true;
				grpTransmitter.Enabled = true;
				btnIORCAInOut.BackColor = Color.Red;
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.MOX = false;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = true;

			double scale = Audio.SourceScale;
			Audio.SourceScale = 0.1;

			double monitor = Audio.MonitorVolume;
			Audio.MonitorVolume = 1.0;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			int in_tx_l = Audio.IN_TX_L;
			Audio.IN_TX_L = 5;

			int reg_11 = 0;
			FWC.ReadCodecReg(0x11, out reg_11);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x11, 0x80);
			//Thread.Sleep(50);

			int reg_12 = 0;
			FWC.ReadCodecReg(0x12, out reg_12);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x12, 0x80);
			//Thread.Sleep(50);

			int reg_13 = 0;
			FWC.ReadCodecReg(0x13, out reg_13);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x13, 0x80);
			//Thread.Sleep(50);

			int reg_14 = 0; // set input side of mixer
			FWC.ReadCodecReg(0x14, out reg_14); 
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x14, 0x30);
			//Thread.Sleep(50);

			int reg_15 = 0;
			FWC.ReadCodecReg(0x15, out reg_15);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x15, 0x80);
			//Thread.Sleep(50);

			int reg_16 = 0;
			FWC.ReadCodecReg(0x16, out reg_16);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x16, 0x80);
			//Thread.Sleep(50);

			int old_codec_reg_7 = 0;
			FWC.ReadCodecReg(7, out old_codec_reg_7);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(7, 0xBF);
			//Thread.Sleep(50);

			int old_codec_reg_0E = 0;
			FWC.ReadCodecReg(0x0E, out old_codec_reg_0E);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0E, 0);
			//Thread.Sleep(50);
			Audio.RX1OutputSignal = Audio.SignalSource.SILENCE;
			Thread.Sleep(500);

			float sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float off = sum / 5;

			Audio.RX1OutputSignal = Audio.SignalSource.SINE;
			Thread.Sleep(500);

			sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float on = sum / 5;
			

			bool b = true;
			if(on-off < 50.0f) b = false;

			if(b)
			{
				btnIORCAInOut.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO RCA In/Out: Passed ("+on.ToString("f1")+", "+off.ToString("f1")+")");
				test_io_rcainout = "IO RCA In/Out Test: Passed";
			}
			else
			{
				btnIORCAInOut.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO RCA In/Out: Failed ("+on.ToString("f1")+", "+off.ToString("f1")+")");
				test_io_rcainout = "IO RCA In/Out Test: Failed";
			}
			toolTip1.SetToolTip(btnIORCAInOut, test_io_rcainout);
            
			// end
			Audio.RX1OutputSignal = Audio.SignalSource.RADIO;
			Audio.IN_TX_L = in_tx_l;
			FWC.WriteCodecReg(0x11, reg_11);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x12, reg_12);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x13, reg_13);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x14, reg_14);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x15, reg_15);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x16, reg_16);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(7, old_codec_reg_7);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0E, old_codec_reg_0E);
			//Thread.Sleep(50);
			Audio.SourceScale = scale;
			console.FullDuplex = false;
			console.RX1DSPMode = dsp_mode;
			Audio.MonitorVolume = monitor;

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_rcainout.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_rcainout.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Off, On, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			
			writer.Write(off.ToString("f1")+",");
			writer.Write(on.ToString("f1")+",");
			writer.WriteLine(b.ToString());
			writer.Close();
		}

		#endregion

		#region FlexWire In/Out

		private void btnIOFWInOut_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOFWInOut.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOFWInOut));
			t.Name = "IO FW In/Out Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_fwinout = "FlexWire In/Out Test: Not Run";
		private void CheckIOFWInOut()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on in order to run this test.", "Power Is Off",
					MessageBoxButtons.OK, MessageBoxIcon.Stop);
				grpIO.Enabled = true;
				grpGeneral.Enabled = true;
				grpReceiver.Enabled = true;
				grpTransmitter.Enabled = true;
				btnIOFWInOut.BackColor = Color.Red;
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.MOX = false;

			console.FullDuplex = true;

			double scale = Audio.SourceScale;
			Audio.SourceScale = 0.1;

			double monitor = Audio.MonitorVolume;
			Audio.MonitorVolume = 1.0;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			int in_tx_l = Audio.IN_TX_L;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					Audio.IN_TX_L = 4;
					break;
				case Model.FLEX3000:
					Audio.IN_TX_L = 2;
					break;
			}

			int reg_11 = 0; 
			FWC.ReadCodecReg(0x11, out reg_11);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.WriteCodecReg(0x11, 0x80);
					break;
				case Model.FLEX3000:
					FWC.WriteCodecReg(0x11, 0x30); // set input side of mixer
					break;
			}

			int reg_12 = 0;
			FWC.ReadCodecReg(0x12, out reg_12);
			FWC.WriteCodecReg(0x12, 0x80);

			int reg_13 = 0; 
			FWC.ReadCodecReg(0x13, out reg_13);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.WriteCodecReg(0x13, 0x30); // set input side of mixer
					break;
				case Model.FLEX3000:
					FWC.WriteCodecReg(0x13, 0x80);
					break;
			}


			int reg_14 = 0; 
			FWC.ReadCodecReg(0x14, out reg_14); 
			FWC.WriteCodecReg(0x14, 0x80);

			int reg_15 = 0;
			FWC.ReadCodecReg(0x15, out reg_15);
			FWC.WriteCodecReg(0x15, 0x80);

			int reg_16 = 0;
			FWC.ReadCodecReg(0x16, out reg_16);
			FWC.WriteCodecReg(0x16, 0x80);

			int reg_7 = 0;
			FWC.ReadCodecReg(7, out reg_7);
			FWC.WriteCodecReg(7, 0xBF);

			int reg_0E = 0;
			FWC.ReadCodecReg(0x0E, out reg_0E);
			FWC.WriteCodecReg(0x0E, 0);

			Audio.RX1OutputSignal = Audio.SignalSource.SILENCE;
			Thread.Sleep(500);

			float sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float off = sum / 5;

			Audio.RX1OutputSignal = Audio.SignalSource.SINE;
			Thread.Sleep(500);

			sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float on = sum / 5;

			bool b = true;
			if(on-off < 50.0f) b = false;

			if(b) 
			{
				btnIOFWInOut.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO FW In/Out: Passed ("+on.ToString("f1")+", "+off.ToString("f1")+")");
				test_io_fwinout = "FlexWire In/Out Test: Passed";
			}
			else
			{
				btnIOFWInOut.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO FW In/Out: Failed ("+on.ToString("f1")+", "+off.ToString("f1")+")");
				test_io_fwinout = "FlexWire In/Out Test: Failed";
			}
			toolTip1.SetToolTip(btnIOFWInOut, test_io_fwinout);
            
			// end
			Audio.RX1OutputSignal = Audio.SignalSource.RADIO;
			Audio.IN_TX_L = in_tx_l;
			FWC.WriteCodecReg(0x11, reg_11);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x12, reg_12);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x13, reg_13);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x14, reg_14);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x15, reg_15);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x16, reg_16);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(7, reg_7);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0E, reg_0E);
			//Thread.Sleep(50);
			Audio.SourceScale = scale;
			console.FullDuplex = false;
			console.RX1DSPMode = dsp_mode;
			Audio.MonitorVolume = monitor;

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_fwinout.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_fwinout.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Off, On, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			
			writer.Write(off.ToString("f1")+",");
			writer.Write(on.ToString("f1")+",");
			writer.WriteLine(b.ToString());
			writer.Close();
		}

		#endregion

		#region Headphones

		private void btnIOHeadphone_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOHeadphone.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOHeadphone));
			t.Name = "IO Headphone Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_headphone = "IO Headphone Test: Not Run";
		private void CheckIOHeadphone()
		{
			/*if(!console.PowerOn)
			{
				MessageBox.Show("Power must be on in order to run this test.", "Power Is Off",
					MessageBoxButtons.OK, MessageBoxIcon.Stop);
				grpIO.Enabled = true;
				grpGeneral.Enabled = true;
				grpReceiver.Enabled = true;
				grpTransmitter.Enabled = true;
				btnIOFWInOut.BackColor = Color.Red;
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			console.MOX = false;

			console.FullDuplex = true;

			double scale = Audio.SourceScale;
			Audio.SourceScale = 0.1;

			double monitor = Audio.MonitorVolume;
			Audio.MonitorVolume = 1.0;

			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;

			int in_tx_l = Audio.IN_TX_L;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					Audio.IN_TX_L = 7;
					break;
				case Model.FLEX3000:
					Audio.IN_TX_L = 3;
					break;
			}

			int reg_11 = 0;
			FWC.ReadCodecReg(0x11, out reg_11);
			FWC.WriteCodecReg(0x11, 0x80);

			int reg_12 = 0;
			FWC.ReadCodecReg(0x12, out reg_12);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.WriteCodecReg(0x12, 0x80);
					break;
				case Model.FLEX3000:
					FWC.WriteCodecReg(0x12, 0x30); // set input side of mixer
					break;
			}

			int reg_13 = 0; 
			FWC.ReadCodecReg(0x13, out reg_13);
			FWC.WriteCodecReg(0x13, 0x80);

			int reg_14 = 0; 
			FWC.ReadCodecReg(0x14, out reg_14); 
			FWC.WriteCodecReg(0x14, 0x80);

			int reg_15 = 0;
			FWC.ReadCodecReg(0x15, out reg_15);
			FWC.WriteCodecReg(0x15, 0x80);

			int reg_16 = 0; 
			FWC.ReadCodecReg(0x16, out reg_16);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.WriteCodecReg(0x16, 0x30); // set input side of mixer
					break;
				case Model.FLEX3000:
					FWC.WriteCodecReg(0x16, 0x80);
					break;
			}

			int reg_7 = 0;
			FWC.ReadCodecReg(7, out reg_7);
			FWC.WriteCodecReg(7, 0xF3);

			int reg_AB = 0;
			FWC.ReadCodecReg(0x0A, out reg_AB);
			FWC.WriteCodecReg(0x0A, 0);
			FWC.WriteCodecReg(0x0B, 0);

			int reg_C = 0;
			FWC.ReadCodecReg(0x0C, out reg_C);
			FWC.WriteCodecReg(0x0C, 0);
			
			int reg_D = 0;
			FWC.ReadCodecReg(0x0D, out reg_D);
			FWC.WriteCodecReg(0x0D, 0);

			Audio.RX1OutputSignal = Audio.SignalSource.SILENCE;
			Thread.Sleep(1000);

			float sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float off = sum / 5;

			Audio.RX1OutputSignal = Audio.SignalSource.SINE_LEFT_ONLY; // check left
			Thread.Sleep(1000);

			sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float left = sum / 5;

			Audio.RX1OutputSignal = Audio.SignalSource.SINE_RIGHT_ONLY; // check right
			Thread.Sleep(1000);

			sum = 0.0f;
			DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				sum += -DttSP.CalculateTXMeter(1, DttSP.MeterType.MIC);
				Thread.Sleep(50);
			}
			float right = sum / 5;

			bool b = true;
			if(left-off < 40.0f) b = false;
			if(right-off < 40.0f) b = false;

			if(b) 
			{
				btnIOHeadphone.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Headphone: Passed ("+off.ToString("f1")+", "+left.ToString("f1")+", "+right.ToString("f1")+")");
				test_io_headphone = "IO Headphone Test: Passed";
			}
			else
			{
				btnIOHeadphone.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Headphone: Failed ("+off.ToString("f1")+", "+left.ToString("f1")+", "+right.ToString("f1")+")");
				test_io_headphone = "IO Headphone Test: Failed";
			}
			toolTip1.SetToolTip(btnIOHeadphone, test_io_headphone);
            
			// end
			Audio.RX1OutputSignal = Audio.SignalSource.RADIO;
			Audio.IN_TX_L = in_tx_l;
			FWC.WriteCodecReg(0x11, reg_11);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x12, reg_12);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x13, reg_13);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x14, reg_14);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x15, reg_15);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x16, reg_16);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(7, reg_7);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0A, reg_AB);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0B, reg_AB);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0C, reg_C);
			//Thread.Sleep(50);
			FWC.WriteCodecReg(0x0D, reg_D);
			//Thread.Sleep(50);
			Audio.SourceScale = scale;
			console.FullDuplex = false;
			console.RX1DSPMode = dsp_mode;
			Audio.MonitorVolume = monitor;

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_headphone.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_headphone.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Off, Left, Right, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			
			writer.Write(off.ToString("f1")+",");
			writer.Write(left.ToString("f1")+",");
			writer.Write(right.ToString("f1")+",");
			writer.WriteLine(b.ToString());
			writer.Close();
		}

		#endregion

		#region Dot

		private void btnIODot_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIODot.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIODot));
			t.Name = "IO Dot Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_dot = "IO Dot Test: Not Run";
		private void CheckIODot()
		{
			bool retval = true;
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			FWC.SetAmpTX1(false);
			Thread.Sleep(100);
			
			bool dot;
			FWC.ReadDot(out dot);
			Thread.Sleep(50);
			if(dot) 
			{
				retval = false;
				goto end;
			}

			FWC.SetRCATX1(true);
			Thread.Sleep(100);
			FWC.ReadDot(out dot);
			Thread.Sleep(50);
			if(!dot) 
			{
				retval = false;
				goto end;
			}

			end:
				//FWC.SetMOX(false);
			FWC.SetRCATX1(false);
			FWC.SetAmpTX1(true);
			if(power)
				console.PowerOn = true;			
			Thread.Sleep(50);
			if(retval)
			{
				btnIODot.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Dot: Passed");
				test_io_dot = "IO Dot Test: Passed";
			}
			else
			{
				btnIODot.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Dot: Failed");
				test_io_dot = "IO Dot Test: Failed";
			}
			toolTip1.SetToolTip(btnIODot, test_io_dot);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_dot.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_dot.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region Dash

		private void btnIODash_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIODash.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIODash));
			t.Name = "IO Dash Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_dash = "IO Dash Test: Not Run";
		private void CheckIODash()
		{
			bool retval = true;
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetAmpTX2(false);
					break;
				case Model.FLEX3000:
					FWC.SetAmpTX1(false);
					break;
			}			
			Thread.Sleep(100);
			
			bool dash;
			FWC.ReadDash(out dash);
			Thread.Sleep(50);
			if(dash) 
			{
				retval = false;
				goto end;
			}

			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetRCATX2(true);
					break;
				case Model.FLEX3000:
					FWC.SetRCATX1(true);
					break;
			}
			Thread.Sleep(100);
			FWC.ReadDash(out dash);
			Thread.Sleep(50);
			if(!dash) 
			{
				retval = false;
				goto end;
			}

		end:
			//FWC.SetMOX(false);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetRCATX2(false);
					FWC.SetAmpTX2(true);
					break;
				case Model.FLEX3000:
					FWC.SetRCATX1(false);
					FWC.SetAmpTX1(true);
					break;
			}
			if(power) console.PowerOn = true;
			Thread.Sleep(50);
			if(retval)
			{
				btnIODash.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Dash: Passed");
				test_io_dash = "IO Dash Test: Passed";
			}
			else
			{
				btnIODash.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Dash: Failed");
				test_io_dash = "IO Dash Test: Failed";
			}
			toolTip1.SetToolTip(btnIODash, test_io_dash);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_dash.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_dash.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region RCA PTT

		private void btnIORCAPTT_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIORCAPTT.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIORCAPTT));
			t.Name = "IO RCA PTT Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_rcaptt = "IO RCA PTT Test: Not Run";
		private void CheckIORCAPTT()
		{
			bool retval = true;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetAmpTX3(false);
					break;
				case Model.FLEX3000:
					FWC.SetAmpTX1(false);
					break;
			}
			Thread.Sleep(100);
			
			bool ptt;
			FWC.ReadRCAPTT(out ptt);
			Thread.Sleep(50);
			if(ptt) 
			{
				retval = false;
				goto end;
			}

			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetRCATX3(true);
					break;
				case Model.FLEX3000:
					FWC.SetRCATX1(true);
					break;
			}
			Thread.Sleep(100);
			FWC.ReadRCAPTT(out ptt);
			Thread.Sleep(50);
			if(!ptt) 
			{
				retval = false;
				goto end;
			}

		end:
			//FWC.SetMOX(false);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetRCATX3(false);
					FWC.SetAmpTX3(true);
					break;
				case Model.FLEX3000:
					FWC.SetRCATX1(false);
					FWC.SetAmpTX1(true);
					break;
			}
			if(power) console.PowerOn = true;
			Thread.Sleep(50);

			if(retval)
			{
				btnIORCAPTT.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO RCA PTT: Passed");
				test_io_rcaptt = "IO RCA PTT Test: Passed";
			}
			else
			{
				btnIORCAPTT.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO RCA PTT: Failed");
				test_io_rcaptt = "IO RCA PTT Test: Failed";
			}
			toolTip1.SetToolTip(btnIORCAPTT, test_io_rcaptt);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_rcaptt.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_rcaptt.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region Mic PTT

		private void btnIOMicPTT_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOMicPTT.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOMicPTT));
			t.Name = "IO Mic PTT Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_micptt = "IO Mic PTT Test: Not Run";
		private void CheckIOMicPTT()
		{
			bool retval = true;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetAmpTX3(false);
					break;
				case Model.FLEX3000:
					FWC.SetAmpTX1(false);
					break;
			}
			Thread.Sleep(100);
			
			bool ptt;
			FWC.ReadMicPTT(out ptt);
			Thread.Sleep(50);
			if(ptt) 
			{
				retval = false;
				goto end;
			}

			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetRCATX3(true);
					break;
				case Model.FLEX3000:
					FWC.SetRCATX1(true);
					break;
			}
			Thread.Sleep(100);
			FWC.ReadMicPTT(out ptt);
			Thread.Sleep(50);
			if(!ptt) 
			{
				retval = false;
				goto end;
			}

		end:
			//FWC.SetMOX(false);
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					FWC.SetRCATX3(false);
					FWC.SetAmpTX3(true);
					break;
				case Model.FLEX3000:
					FWC.SetRCATX1(false);
					FWC.SetAmpTX1(true);
					break;
			}
			if(power) console.PowerOn = true;
			Thread.Sleep(50);

			if(retval)
			{
				btnIOMicPTT.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Mic PTT: Passed");
				test_io_micptt = "IO Mic PTT Test: Passed";
			}
			else
			{
				btnIOMicPTT.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Mic PTT: Failed");
				test_io_micptt = "IO Mic PTT Test: Failed";
			}
			toolTip1.SetToolTip(btnIOMicPTT, test_io_micptt);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_micptt.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_micptt.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region Ext Ref

		private void btnIOExtRef_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOExtRef.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOExtRef));
			t.Name = "IO Ext Ref Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_extref = "IO Ext Ref Test: Not Run";
		private void CheckIOExtRef()
		{
			FWC.SetXREF(true);
			//Thread.Sleep(50);
			int old_reg = 0;
			if(FWC.ReadClockReg(0x08, out old_reg) == 0)
				MessageBox.Show("Error in ReadClockReg");
			//Thread.Sleep(50);
			int val = (int)((old_reg&0xC3) | (int)(1 << 2));
			if(old_reg != val)
			{
				if(FWC.WriteClockReg(0x08, val) == 0)
					MessageBox.Show("Error in WriteClockReg");
			}
			Thread.Sleep(1000);

			bool b = true;
			FWC.GetPLLStatus2(out b);
			//Thread.Sleep(50);

			FWC.WriteClockReg(0x08, old_reg);
			//Thread.Sleep(50);
			FWC.SetXREF(false);
			//Thread.Sleep(50);

			if(b)
			{
				btnIOExtRef.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Ext Ref: Passed");
				test_io_extref = "IO Ext Ref Test: Passed";
			}
			else
			{
				btnIOExtRef.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Ext Ref: Failed");
				test_io_extref = "IO Ext Ref Test: Failed";
			}
			toolTip1.SetToolTip(btnIOExtRef, test_io_extref);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_extref.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_extref.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(b.ToString());
			writer.Close();
		}

		#endregion

		#region Mic Up

		private void btnIOMicUp_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOMicUp.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOMicUp));
			t.Name = "IO Mic Up Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_micup = "IO Mic Up Test: Not Run";
		private void CheckIOMicUp()
		{
			bool retval = true;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			FWC.SetAmpTX1(false);
			Thread.Sleep(100);
			
			bool b;
			FWC.ReadMicUp(out b);
			Thread.Sleep(50);
			if(b) 
			{
				retval = false;
				goto end;
			}

			FWC.SetRCATX1(true);
			Thread.Sleep(100);
			FWC.ReadMicUp(out b);
			Thread.Sleep(50);
			if(!b) 
			{
				retval = false;
				goto end;
			}

			end:
				//FWC.SetMOX(false);
				FWC.SetRCATX1(false);
			FWC.SetAmpTX1(true);
			if(power) console.PowerOn = true;
			Thread.Sleep(50);

			if(retval)
			{
				btnIOMicUp.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Mic Up: Passed");
				test_io_micup = "IO Mic Up Test: Passed";
			}
			else
			{
				btnIOMicUp.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Mic Up: Failed");
				test_io_micup = "IO Mic Up Test: Failed";
			}
			toolTip1.SetToolTip(btnIOMicUp, test_io_micup);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_micup.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_micup.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region Mic Down

		private void btnIOMicDown_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOMicDown.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOMicDown));
			t.Name = "IO Mic Down Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_micdown = "IO Mic Down Test: Not Run";
		private void CheckIOMicDown()
		{
			bool retval = true;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			FWC.SetAmpTX1(false);
			Thread.Sleep(100);
			
			bool b;
			FWC.ReadMicDown(out b);
			Thread.Sleep(50);
			if(b) 
			{
				retval = false;
				goto end;
			}

			FWC.SetRCATX1(true);
			Thread.Sleep(100);
			FWC.ReadMicDown(out b);
			Thread.Sleep(50);
			if(!b) 
			{
				retval = false;
				goto end;
			}

			end:
				//FWC.SetMOX(false);
				FWC.SetRCATX1(false);
			FWC.SetAmpTX1(true);
			if(power) console.PowerOn = true;
			Thread.Sleep(50);

			if(retval)
			{
				btnIOMicDown.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Mic Down: Passed");
				test_io_micdown = "IO Mic Down Test: Passed";
			}
			else
			{
				btnIOMicDown.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Mic Down: Failed");
				test_io_micdown = "IO Mic Down Test: Failed";
			}
			toolTip1.SetToolTip(btnIOMicDown, test_io_micdown);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_micdown.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_micdown.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region Mic Fast

		private void btnIOMicFast_Click(object sender, System.EventArgs e)
		{
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			grpIO.Enabled = false;
			btnIOMicFast.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(CheckIOMicFast));
			t.Name = "IO Mic Fast Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_io_micfast = "IO Mic Fast Test: Not Run";
		private void CheckIOMicFast()
		{
			bool retval = true;
			//FWC.SetMOX(true);
			//Thread.Sleep(50);
			//FWC.SetPABias(false);
			//Thread.Sleep(50);
			bool power = console.PowerOn;
			if(power) console.PowerOn = false;
			FWC.SetAmpTX1(false);
			Thread.Sleep(100);
			
			bool b;
			FWC.ReadMicFast(out b);
			Thread.Sleep(50);
			if(b) 
			{
				retval = false;
				goto end;
			}

			FWC.SetRCATX1(true);
			Thread.Sleep(100);
			FWC.ReadMicFast(out b);
			Thread.Sleep(50);
			if(!b) 
			{
				retval = false;
				goto end;
			}

			end:
				//FWC.SetMOX(false);
				FWC.SetRCATX1(false);
			FWC.SetAmpTX1(true);
			if(power) console.PowerOn = true;
			Thread.Sleep(50);

			if(retval)
			{
				btnIOMicFast.BackColor = Color.Green;
				lstDebug.Items.Insert(0, "IO Mic Fast: Passed");
				test_io_micfast = "IO Mic Fast Test: Passed";
			}
			else
			{
				btnIOMicFast.BackColor = Color.Red;
				lstDebug.Items.Insert(0, "IO Mic Fast: Failed");
				test_io_micfast = "IO Mic Fast Test: Failed";
			}
			toolTip1.SetToolTip(btnIOMicFast, test_io_micfast);

			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			grpIO.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\io_micfast.csv");
			StreamWriter writer = new StreamWriter(path+"\\io_micfast.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, Passed");
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");

			writer.WriteLine(retval.ToString());
			writer.Close();
		}

		#endregion

		#region Run All

		private void btnIORunAll_Click(object sender, System.EventArgs e)
		{
			console.PowerOn = true;
			btnIORunAll.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(IORunAll));
			t.Name = "Run All IO Tests Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void IORunAll()
		{
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();

			if(btnIOExtRef.Visible)
			{
				Invoke(new MethodInvoker(btnIOExtRef.PerformClick));
				Thread.Sleep(2000);
			}

			if(btnIOPwrSpkr.Visible) 
			{
				Invoke(new MethodInvoker(btnIOPwrSpkr.PerformClick));
				Thread.Sleep(3500);
			}

			if(btnIORCAInOut.Visible)
			{
				Invoke(new MethodInvoker(btnIORCAInOut.PerformClick));
				Thread.Sleep(3000);
			}

			if(btnIOFWInOut.Visible)
			{
				Invoke(new MethodInvoker(btnIOFWInOut.PerformClick));
				Thread.Sleep(3000);
			}

			if(btnIOHeadphone.Visible)
			{
				Invoke(new MethodInvoker(btnIOHeadphone.PerformClick));
				Thread.Sleep(5000);
			}

			if(btnIODot.Visible)
			{
				Invoke(new MethodInvoker(btnIODot.PerformClick));
				Thread.Sleep(1000);
			}

			if(btnIODash.Visible)
			{
				Invoke(new MethodInvoker(btnIODash.PerformClick));
				Thread.Sleep(1000);
			}

			if(btnIORCAPTT.Visible)
			{
				Invoke(new MethodInvoker(btnIORCAPTT.PerformClick));
				Thread.Sleep(1000);
			}

			if(btnIOMicPTT.Visible)
			{
				Invoke(new MethodInvoker(btnIOMicPTT.PerformClick));
				Thread.Sleep(1000);	
			}
	
			if(btnIOMicUp.Visible)
			{
				Invoke(new MethodInvoker(btnIOMicUp.PerformClick));
				Thread.Sleep(1000);
			}

			if(btnIOMicDown.Visible)
			{
				Invoke(new MethodInvoker(btnIOMicDown.PerformClick));
				Thread.Sleep(1000);
			}

			if(btnIOMicFast.Visible)
			{
				Invoke(new MethodInvoker(btnIOMicFast.PerformClick));
				Thread.Sleep(1000);
			}
	
			btnIORunAll.BackColor = SystemColors.Control;

			t1.Stop();
			MessageBox.Show("Run All Tests Time: "+((int)(t1.Duration/60)).ToString()+":"+(((int)t1.Duration)%60).ToString("00"));
		}

		#endregion

		#endregion

		#region Event Handlers

		private void FLEX5000ProdTestForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FLEX5000ProdTestForm");
		}

		private void btnCheckAll_Click(object sender, System.EventArgs e)
		{
			ck160.Checked = true;
			ck80.Checked = true;
			ck60.Checked = true;
			ck40.Checked = true;
			ck30.Checked = true;
			ck20.Checked = true;
			ck17.Checked = true;
			ck15.Checked = true;
			ck12.Checked = true;
			ck10.Checked = true;
			ck6.Checked = true;
		}

		private void btnClearAll_Click(object sender, System.EventArgs e)
		{
			ck160.Checked = false;
			ck80.Checked = false;
			ck60.Checked = false;
			ck40.Checked = false;
			ck30.Checked = false;
			ck20.Checked = false;
			ck17.Checked = false;
			ck15.Checked = false;
			ck12.Checked = false;
			ck10.Checked = false;
			ck6.Checked = false;
		}

		private void btnPrintReport_Click(object sender, System.EventArgs e)
		{
			printPreviewDialog1.Document = printDocument1;
			printPreviewDialog1.ShowDialog();
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			int V = 80;
			string text = "TRX Serial Number: "+FWCEEPROM.SerialToString(FWCEEPROM.TRXSerial)
				+"  Date: "+DateTime.Today.ToShortDateString()
				+"  Time: "+DateTime.Now.ToString("HH:mm:ss")
				+"  Tech: "+txtTech.Text+"\n\n";

			text += "\n"+test_pll+"\n";
			text += test_genbal+"\n";
			text += test_noise+"\n";
			text += test_impulse+"\n";
			text += test_preamp+"\n";

			text += "\n"+test_rx_filter+"\n";
			text += test_rx_level+"\n";
			text += test_rx_image+"\n";
			//text += test_rx_mds+"\n";

			text += "\n"+test_tx_filter+"\n";
			text += test_tx_image+"\n";
			text += test_tx_carrier+"\n";

			e.Graphics.DrawString(text, 
				new Font("Lucida Console", 11), Brushes.Black, 80, V);
		}

		private void btnTestGenAll_Click(object sender, System.EventArgs e)
		{
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					ckTestGenPLL.Checked = true;
					ckTestGenImpulse.Checked = true;
					break;
				case Model.FLEX3000:
					ckTestGenATTN.Checked = true;
					break;
			}
			ckTestGenBal.Checked = true;
			//ckTestGenNoise.Checked = true;			
			ckTestGenPreamp.Checked = true;
		}

		private void btnTestGenNone_Click(object sender, System.EventArgs e)
		{
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					ckTestGenPLL.Checked = false;
					ckTestGenImpulse.Checked = false;
					break;
				case Model.FLEX3000:
					ckTestGenATTN.Checked = false;
					break;
			}
			ckTestGenPLL.Checked = false;
			ckTestGenBal.Checked = false;
			//ckTestGenNoise.Checked = false;				
			ckTestGenPreamp.Checked = false;
		}

		private void btnTestRXAll_Click(object sender, System.EventArgs e)
		{
			ckTestRXFilter.Checked = true;
			ckTestRXLevel.Checked = true;
			ckTestRXImage.Checked = true;
			//ckTestRXMDS.Checked = true;
		}

		private void btnTestRXNone_Click(object sender, System.EventArgs e)
		{
			ckTestRXFilter.Checked = false;
			ckTestRXLevel.Checked = false;
			ckTestRXImage.Checked = false;
			//ckTestRXMDS.Checked = false;
		}

		private void btnTestTXAll_Click(object sender, System.EventArgs e)
		{
			switch(console.CurrentModel)
			{
				case Model.FLEX5000:
					ckTestTXFilter.Checked = true;
					break;
			}
			ckTestTXImage.Checked = true;
			ckTestTXCarrier.Checked = true;
			ckTestTXGain.Checked = true;
		}

		private void btnTestTXNone_Click(object sender, System.EventArgs e)
		{
			ckTestTXFilter.Checked = false;
			ckTestTXImage.Checked = false;
			ckTestTXCarrier.Checked = false;
			ckTestTXGain.Checked = false;
		}

		private void btnTestAll_Click(object sender, System.EventArgs e)
		{
			btnTestGenAll.PerformClick();
			btnTestRXAll.PerformClick();
			btnTestTXAll.PerformClick();
		}

		private void btnTestNone_Click(object sender, System.EventArgs e)
		{
			btnTestGenNone.PerformClick();
			btnTestRXNone.PerformClick();
			btnTestTXNone.PerformClick();
		}

		#endregion

		#region Post Fence

		private void btnPostFence_Click(object sender, System.EventArgs e)
		{
			//p = new Progress("Post Fence Test");
			grpGeneral.Enabled = false;
			grpReceiver.Enabled = false;
			grpTransmitter.Enabled = false;
			btnPostFence.BackColor = Color.Green;
			btnPostFence.Enabled = false;
			Thread t = new Thread(new ThreadStart(PostFenceTest));
			t.Name = "Post Fence Test Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
			//Invoke(new MethodInvoker(p.Show));
		}

		private string test_post_fence = "Post Fence: Not Run";
		private void PostFenceTest()
		{
			/*if(!console.PowerOn)
			{
				p.Hide();
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);					
				grpGeneral.Enabled = true;
				grpReceiver.Enabled = true;
				grpTransmitter.Enabled = true;
				btnPostFence.Enabled = true;
				return;
			}*/

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();

			test_post_fence = "Post Fence: ";

			console.VFOSplit = false;
			
			btnPLL_Click(this, EventArgs.Empty);
			Thread.Sleep(1000);

			btnGenBal_Click(this, EventArgs.Empty);
			Thread.Sleep(3000);

			btnGenPreamp_Click(this, EventArgs.Empty);
			Thread.Sleep(3000);

			btnCheckAll_Click(this, EventArgs.Empty);
			btnRXFilter_Click(this, EventArgs.Empty);
			while(true)
			{
				while(p.Visible) Thread.Sleep(2000);
				Thread.Sleep(2000);
				if(!p.Visible) break;
			}
			if(p.Text == "") goto end;

			btnClearAll_Click(this, EventArgs.Empty);
			ck20.Checked = true;
			btnTXImage_Click(this, EventArgs.Empty);
			while(true)
			{
				while(!btnTXImage.Enabled) Thread.Sleep(2000);
				Thread.Sleep(2000);
				if(btnTXImage.Enabled) break;
			}
			if(p.Text == "") goto end;

			if(btnPLL.BackColor == Color.Green &&
				btnGenBal.BackColor == Color.Green &&
				btnRXFilter.BackColor == Color.Green &&
				btnTXImage.BackColor == Color.Green)
			{
				btnPostFence.BackColor = Color.Green;
				test_post_fence += "Passed";
			}
			else
			{
				btnPostFence.BackColor = Color.Red;
				test_post_fence += "Failed";
			}

		end:
			p.Hide();
			FWC.SetFullDuplex(false);
			//Thread.Sleep(50);
			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			btnPostFence.Enabled = true;

			t1.Stop();
			MessageBox.Show("Post Fence Time: "+((int)(t1.Duration/60)).ToString()+":"+(((int)t1.Duration)%60).ToString("00"));

			/*string test_verify_rx_level = "";
			string test_verify_rx_image = "";
			string test_verify_tx_image = "";
			string test_verify_tx_carrier = "";
			bool pass_rx_level = false;
			bool pass_rx_image = false;
			bool pass_tx_image = false;
			bool pass_tx_carrier = false;
			Band[] bands = { Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M, Band.B6M };
			float[] band_freqs = { 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f, 50.11f};
			float[] a = new float[Display.BUFFER_SIZE];
			int counter = 0;

			bool spur_red = console.SpurReduction;
			console.SpurReduction = false;

			string display = console.DisplayModeText;
			console.DisplayModeText = "Spectrum";
			PreampMode preamp = console.RX1PreampMode;
			DSPMode dsp_mode = console.RX1DSPMode;
			Filter filter = console.RX1Filter;
			int rx_filt_high = console.RX1FilterHigh;
			int rx_filt_low = console.RX1FilterLow;	
			int tx_filt_high = console.SetupForm.TXFilterHigh;
			int tx_filt_low = console.SetupForm.TXFilterLow;
		
			bool rit_on = console.RITOn;
			console.RITOn = false;
			bool xit_on = console.XITOn;
			console.XITOn = false;
			bool split = console.VFOSplit;

			int dsp_buf_size = console.SetupForm.DSPRXBufferSize;			// save current DSP buffer size
			console.SetupForm.DSPRXBufferSize = 4096;						// set DSP Buffer Size to 2048

			bool polyphase = console.SetupForm.Polyphase;				// save current polyphase setting
			console.SetupForm.Polyphase = false;						// disable polyphase

			int num_bands = 0;
			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}
				if(do_band) num_bands++;
			}
			int total_counts = num_bands*(10+10+10+5);

			for(int i=0; i<bands.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M: do_band = ck80.Checked; break;
					case Band.B60M: do_band = ck60.Checked; break;
					case Band.B40M: do_band = ck40.Checked; break;
					case Band.B30M: do_band = ck30.Checked; break;
					case Band.B20M: do_band = ck20.Checked; break;
					case Band.B17M: do_band = ck17.Checked; break;
					case Band.B15M: do_band = ck15.Checked; break;
					case Band.B12M: do_band = ck12.Checked; break;
					case Band.B10M:	do_band = ck10.Checked; break;
					case Band.B6M: do_band = ck6.Checked; break;
				}

				if(do_band)
				{
					// verify RX Level
					FWC.SetTest(true);
					FWC.SetSig(true);
					FWC.SetGen(true);
					FWC.SetTXMon(false);
					FWC.SetFullDuplex(true);

			
					console.VFOAFreq = band_freqs[i];
					Thread.Sleep(50);
					console.VFOBFreq = band_freqs[i];

					console.RX1PreampMode = PreampMode.HIGH;
					console.RX1DSPMode = DSPMode.DSB;
					console.RX1Filter = Filter.VAR1;
					console.RX1FilterLow = -500;
					console.RX1FilterHigh = 500;
					Thread.Sleep(750);

					int peak_bin = -1;
					console.calibration_mutex.WaitOne();
					fixed(float* ptr = &a[0])
						DttSP.GetSpectrum(0, ptr);
					console.calibration_mutex.ReleaseMutex();
					float max = float.MinValue;
					for(int j=0; j<Display.BUFFER_SIZE; j++)
					{
						if(a[j] > max)
						{
							max = a[j];
							peak_bin = j;
						}
					}

					float sum_d = 0.0f;
					for(int j=0; j<5; j++)
					{
						console.calibration_mutex.WaitOne();
						fixed(float* ptr = &a[0])
							DttSP.GetSpectrum(0, ptr);
						console.calibration_mutex.ReleaseMutex();
						sum_d += a[peak_bin];
						if(j != 4) Thread.Sleep(100);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					sum_d /= 5;
					sum_d = sum_d + Display.DisplayCalOffset + Display.PreampOffset;

					float sum_m = 0.0f;
					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						sum_m += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					sum_m /= 5;
					sum_m = sum_m + console.MultiMeterCalOffset + console.PreampOffset + console.RXPathOffset;

					pass_rx_level = (Math.Abs(-25.0 - sum_d) < 1.0f) && (Math.Abs(-25.0 - sum_m) < 1.0f);

					if(pass_rx_level)
					{
						lstDebug.Items.Insert(0, "Verify RX Level "+BandToString(bands[i])+": Passed ("+sum_d.ToString("f1")+", "+sum_m.ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "Verify RX Level "+BandToString(bands[i])+": Failed ("+sum_d.ToString("f1")+", "+sum_m.ToString("f1")+")");
						if(!test_verify_rx_level.StartsWith("RX Level Failed ("))
							test_verify_rx_level = "RX Level Failed (";
						test_verify_rx_level += BandToString(bands[i])+", ";
						if(btnPostFence.BackColor != Color.Red)
							btnPostFence.BackColor = Color.Red;
					}

					// verify RX Image
					console.VFOSplit = true;
					FWC.SetQSE(false);
			
					console.VFOAFreq = band_freqs[i];
					Thread.Sleep(50);
					console.VFOBFreq = band_freqs[i];
					Thread.Sleep(750);

					float fundamental = 0.0f;
					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						fundamental += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					fundamental /= 5;
					fundamental = fundamental + console.MultiMeterCalOffset + console.PreampOffset + console.RXPathOffset;

					console.VFOAFreq = band_freqs[i]+2*console.IFFreq;
					Thread.Sleep(500);

					float image = 0.0f;
					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						image += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					image /= 5;
					image = image + console.MultiMeterCalOffset + console.PreampOffset + console.RXPathOffset;

					float rejection = fundamental - image;
					pass_rx_image = (rejection >= 60.0f);
 
					if(pass_rx_image)
					{
						lstDebug.Items.Insert(0, "Verify RX Image "+BandToString(bands[i])+": Passed ("+fundamental.ToString("f1")+", "+image.ToString("f1")+", "+rejection.ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "Verify RX Image "+BandToString(bands[i])+": Failed ("+fundamental.ToString("f1")+", "+image.ToString("f1")+", "+rejection.ToString("f1")+")");
						if(!test_verify_rx_image.StartsWith("RX Image Failed ("))
							test_verify_rx_image = "RX Image Failed (";
						test_verify_rx_image += BandToString(bands[i])+", ";
						if(btnPostFence.BackColor != Color.Red)
							btnPostFence.BackColor = Color.Red;
					}

					console.VFOSplit = false;

					// verify TX Image
					console.FullDuplex = true;
					Audio.TXInputSignal = Audio.SignalSource.SINE;
					double last_scale = Audio.SourceScale;			// saved audio scale
					Audio.SourceScale = 1.0;						
					double tone_freq = Audio.SineFreq1;				// save tone freq
					Audio.SineFreq1 = 1500.0;						// set freq

					int pwr = console.PWR;
					console.PWR = 100;
					Audio.RadioVolume = 0.200;

					FWC.SetQSD(true);
					FWC.SetQSE(true);
					FWC.SetTR(true);
					FWC.SetSig(true);
					FWC.SetGen(false);
					FWC.SetTest(true);
					FWC.SetTXMon(false);

					console.SetupForm.TXFilterLow = 300;
					console.SetupForm.TXFilterHigh = 3000;

					console.VFOAFreq = band_freqs[i];
					Thread.Sleep(50);
					console.VFOBFreq = band_freqs[i];
					console.RX1DSPMode = DSPMode.USB;
					console.UpdateFilters(300, 3000);
					Thread.Sleep(750);

					fundamental = 0.0f;
					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						fundamental += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					fundamental /= 5;
					fundamental = fundamental + console.MultiMeterCalOffset + console.PreampOffset + console.RXPathOffset;

					console.UpdateFilters(-1550, -1450);
					Thread.Sleep(500);

					image = 0.0f;
					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						image += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					image /= 5;
					image = image + console.MultiMeterCalOffset + console.PreampOffset + console.RXPathOffset;

					rejection = fundamental - image;

					pass_tx_image = (rejection >= 60.0);

					if(pass_tx_image)
					{
						lstDebug.Items.Insert(0, "Verify TX Image "+BandToString(bands[i])+": Passed ("+fundamental.ToString("f1")+", "+image.ToString("f1")+", "+rejection.ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "Verify TX Image "+BandToString(bands[i])+": Failed ("+fundamental.ToString("f1")+", "+image.ToString("f1")+", "+rejection.ToString("f1")+")");
						if(!test_verify_tx_image.StartsWith("TX Image Failed ("))
							test_verify_tx_image = "TX Image Failed (";
						test_verify_tx_image += BandToString(bands[i])+", ";
						if(btnPostFence.BackColor != Color.Red)
							btnPostFence.BackColor = Color.Red;
					}
					Audio.TXInputSignal = Audio.SignalSource.RADIO;
					Audio.SourceScale = last_scale;						// recall tone scale
					Audio.SineFreq1 = tone_freq;						// recall tone freq
					console.PWR = pwr;
					FWC.SetTR(false);

					// verify TX Carrier
					FWC.SetQSD(true);
					FWC.SetQSE(true);
					FWC.SetSig(true);
					FWC.SetGen(false);
					FWC.SetTest(true);
					FWC.SetTXMon(false);
					Audio.TXInputSignal = Audio.SignalSource.SILENCE;

					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.RX1DSPMode = DSPMode.DSB;
					console.UpdateFilters(-500, 500);
					Thread.Sleep(500);

					float carrier = 0.0f;
					DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						carrier += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(j != 4) Thread.Sleep(50);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
					}
					carrier /= 5;
					carrier = carrier + console.MultiMeterCalOffset + console.PreampOffset + console.RXPathOffset;

					pass_tx_carrier = (carrier <= -105.0);

					if(pass_tx_carrier)
					{
						lstDebug.Items.Insert(0, "Verify TX Carrier "+BandToString(bands[i])+": Passed ("+carrier.ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "Verify TX Carrier "+BandToString(bands[i])+": Failed ("+carrier.ToString("f1")+")");
						if(!test_verify_tx_carrier.StartsWith("TX Carrier Failed ("))
							test_verify_tx_carrier = "TX Carrier Failed (";
						test_verify_tx_carrier += BandToString(bands[i])+", ";
						if(btnPostFence.BackColor != Color.Red)
							btnPostFence.BackColor = Color.Red;
					}

					Audio.TXInputSignal = Audio.SignalSource.RADIO;
				}
			}
	
			if(!test_verify_rx_level.StartsWith("RX Level Failed ("))
				test_verify_rx_level = "RX Level Passed\n";
			else test_verify_rx_level = test_verify_rx_level.Substring(0, test_verify_rx_level.Length-2)+")\n";

			if(!test_verify_rx_image.StartsWith("RX Image Failed ("))
				test_verify_rx_image = "RX Image Passed\n";
			else test_verify_rx_image = test_verify_rx_image.Substring(0, test_verify_rx_image.Length-2)+")\n";	

			if(!test_verify_tx_image.StartsWith("TX Image Failed ("))
				test_verify_tx_image = "TX Image Passed\n";
			else test_verify_tx_image = test_verify_tx_image.Substring(0, test_verify_tx_image.Length-2)+")\n";

			if(!test_verify_tx_carrier.StartsWith("TX Carrier Failed ("))
				test_verify_tx_carrier = "TX Carrier Passed\n";
			else test_verify_tx_carrier = test_verify_tx_carrier.Substring(0, test_verify_tx_carrier.Length-2)+")";

			test_post_fence = test_verify_rx_level + test_verify_rx_image + test_verify_tx_image + test_verify_tx_carrier;

			end:
				FWC.SetTest(false);
			FWC.SetTR(false);
			FWC.SetGen(false);
			FWC.SetSig(false);
			FWC.SetTXMon(false);
			FWC.SetFullDuplex(false);
			console.FullDuplex = false;
			console.SpurReduction = spur_red;
			console.DisplayModeText = display;
			console.RX1PreampMode = preamp;
			console.RX1DSPMode = dsp_mode;
			console.RX1Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX1FilterHigh = rx_filt_high;
				console.RX1FilterLow = rx_filt_low;			
			}
			console.RITOn = rit_on;
			console.XITOn = xit_on;
			console.VFOSplit = split;
			console.SetupForm.DSPRXBufferSize = dsp_buf_size;				// set DSP Buffer Size to 2048
			console.SetupForm.Polyphase = polyphase;					// disable polyphase

			toolTip1.SetToolTip(btnPostFence, test_post_fence);

			p.Hide();
			FWC.SetFullDuplex(false);
			grpGeneral.Enabled = true;
			grpReceiver.Enabled = true;
			grpTransmitter.Enabled = true;
			btnPostFence.Enabled = true;

			t1.Stop();
			MessageBox.Show("Verify Time: "+((int)(t1.Duration/60)).ToString()+":"+(((int)t1.Duration)%60).ToString("00"));
			*/
		}

		#endregion

		private void FLEX5000ProdTestForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Control && e.Alt && e.KeyCode == Keys.L)
			{
				udLevel.Visible = true;
			}
		}
	}
}

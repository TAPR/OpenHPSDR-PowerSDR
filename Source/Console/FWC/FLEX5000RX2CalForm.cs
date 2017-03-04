//=================================================================
// FLEX5000RX2CalForm.cs
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
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
	public class FLEX5000RX2CalForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Progress p;
		private Console console;
		private System.Windows.Forms.GroupBoxTS grpTestGeneral;
		private System.Windows.Forms.ButtonTS btnTestAll;
		private System.Windows.Forms.CheckBoxTS ckRunPreamp;
		private System.Windows.Forms.ButtonTS btnTestNone;
		private System.Windows.Forms.CheckBoxTS ckRunNoise;
		private System.Windows.Forms.CheckBoxTS ckRunGenBal;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.TextBoxTS txtTech;
		private System.Windows.Forms.LabelTS lblTech;
		private System.Windows.Forms.ButtonTS btnSelNone;
		private System.Windows.Forms.ButtonTS btnPostFence;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.GroupBoxTS grpTestReceiver;
		private System.Windows.Forms.ButtonTS btnCalNone;
		private System.Windows.Forms.ButtonTS btnCalAll;
		private System.Windows.Forms.CheckBoxTS ckCalImage;
		private System.Windows.Forms.CheckBoxTS ckCalLevel;
		private System.Windows.Forms.ButtonTS btnSelAll;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.GroupBoxTS grpBands;
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
		private System.Windows.Forms.ButtonTS btnClearAll;
		private System.Windows.Forms.ButtonTS btnCheckAll;
		private System.Windows.Forms.ButtonTS btnRunSel;
		private System.Windows.Forms.ListBox lstDebug;
		private System.Windows.Forms.GroupBoxTS grpTests;
		private System.Windows.Forms.ButtonTS btnTestPreamp;
		private System.Windows.Forms.ButtonTS btnTestNoise;
		private System.Windows.Forms.ButtonTS btnTestGenBal;
		private System.Windows.Forms.GroupBoxTS grpCal;
		private System.Windows.Forms.ButtonTS btnCalImage;
		private System.Windows.Forms.ButtonTS btnTestFilters;
		private System.Windows.Forms.ButtonTS btnCalLevel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBoxTS ckRunFilters;
		private System.Windows.Forms.CheckBoxTS checkBox1;
        private NumericUpDown udLevel;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor and Destructor

		public FLEX5000RX2CalForm(Console c)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			console = c;
			this.Text += "  (RX2: "+FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+")";
			Common.RestoreForm(this, "FLEX5000RX2CalForm", false);

			if(FWCEEPROM.RX2Serial == 0)
			{
				MessageBox.Show("No RX2 Serial Found.  Please enter and try again.",
					"No RX2 S/N Found",
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
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLEX5000RX2CalForm));
            this.grpTestGeneral = new System.Windows.Forms.GroupBoxTS();
            this.btnTestAll = new System.Windows.Forms.ButtonTS();
            this.ckRunPreamp = new System.Windows.Forms.CheckBoxTS();
            this.btnTestNone = new System.Windows.Forms.ButtonTS();
            this.ckRunNoise = new System.Windows.Forms.CheckBoxTS();
            this.ckRunGenBal = new System.Windows.Forms.CheckBoxTS();
            this.ckRunFilters = new System.Windows.Forms.CheckBoxTS();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.txtTech = new System.Windows.Forms.TextBoxTS();
            this.lblTech = new System.Windows.Forms.LabelTS();
            this.btnSelNone = new System.Windows.Forms.ButtonTS();
            this.btnPostFence = new System.Windows.Forms.ButtonTS();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.grpTestReceiver = new System.Windows.Forms.GroupBoxTS();
            this.btnCalNone = new System.Windows.Forms.ButtonTS();
            this.btnCalAll = new System.Windows.Forms.ButtonTS();
            this.ckCalImage = new System.Windows.Forms.CheckBoxTS();
            this.ckCalLevel = new System.Windows.Forms.CheckBoxTS();
            this.btnSelAll = new System.Windows.Forms.ButtonTS();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnTestPreamp = new System.Windows.Forms.ButtonTS();
            this.btnTestNoise = new System.Windows.Forms.ButtonTS();
            this.btnTestGenBal = new System.Windows.Forms.ButtonTS();
            this.btnCalImage = new System.Windows.Forms.ButtonTS();
            this.btnTestFilters = new System.Windows.Forms.ButtonTS();
            this.btnCalLevel = new System.Windows.Forms.ButtonTS();
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
            this.btnRunSel = new System.Windows.Forms.ButtonTS();
            this.lstDebug = new System.Windows.Forms.ListBox();
            this.grpTests = new System.Windows.Forms.GroupBoxTS();
            this.udLevel = new System.Windows.Forms.NumericUpDown();
            this.grpCal = new System.Windows.Forms.GroupBoxTS();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBoxTS();
            this.grpTestGeneral.SuspendLayout();
            this.grpTestReceiver.SuspendLayout();
            this.grpBands.SuspendLayout();
            this.grpTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLevel)).BeginInit();
            this.grpCal.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTestGeneral
            // 
            this.grpTestGeneral.Controls.Add(this.btnTestAll);
            this.grpTestGeneral.Controls.Add(this.ckRunPreamp);
            this.grpTestGeneral.Controls.Add(this.btnTestNone);
            this.grpTestGeneral.Controls.Add(this.ckRunNoise);
            this.grpTestGeneral.Controls.Add(this.ckRunGenBal);
            this.grpTestGeneral.Controls.Add(this.ckRunFilters);
            this.grpTestGeneral.Location = new System.Drawing.Point(8, 379);
            this.grpTestGeneral.Name = "grpTestGeneral";
            this.grpTestGeneral.Size = new System.Drawing.Size(376, 48);
            this.grpTestGeneral.TabIndex = 39;
            this.grpTestGeneral.TabStop = false;
            this.grpTestGeneral.Text = "Tests";
            // 
            // btnTestAll
            // 
            this.btnTestAll.Image = null;
            this.btnTestAll.Location = new System.Drawing.Point(280, 16);
            this.btnTestAll.Name = "btnTestAll";
            this.btnTestAll.Size = new System.Drawing.Size(40, 24);
            this.btnTestAll.TabIndex = 24;
            this.btnTestAll.Text = "All";
            this.btnTestAll.Click += new System.EventHandler(this.btnTestAll_Click);
            // 
            // ckRunPreamp
            // 
            this.ckRunPreamp.Checked = true;
            this.ckRunPreamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckRunPreamp.Image = null;
            this.ckRunPreamp.Location = new System.Drawing.Point(152, 16);
            this.ckRunPreamp.Name = "ckRunPreamp";
            this.ckRunPreamp.Size = new System.Drawing.Size(64, 24);
            this.ckRunPreamp.TabIndex = 26;
            this.ckRunPreamp.Text = "Preamp";
            // 
            // btnTestNone
            // 
            this.btnTestNone.Image = null;
            this.btnTestNone.Location = new System.Drawing.Point(328, 16);
            this.btnTestNone.Name = "btnTestNone";
            this.btnTestNone.Size = new System.Drawing.Size(40, 24);
            this.btnTestNone.TabIndex = 25;
            this.btnTestNone.Text = "None";
            this.btnTestNone.Click += new System.EventHandler(this.btnTestNone_Click);
            // 
            // ckRunNoise
            // 
            this.ckRunNoise.Checked = true;
            this.ckRunNoise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckRunNoise.Image = null;
            this.ckRunNoise.Location = new System.Drawing.Point(88, 16);
            this.ckRunNoise.Name = "ckRunNoise";
            this.ckRunNoise.Size = new System.Drawing.Size(56, 24);
            this.ckRunNoise.TabIndex = 21;
            this.ckRunNoise.Text = "Noise";
            // 
            // ckRunGenBal
            // 
            this.ckRunGenBal.Checked = true;
            this.ckRunGenBal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckRunGenBal.Image = null;
            this.ckRunGenBal.Location = new System.Drawing.Point(16, 16);
            this.ckRunGenBal.Name = "ckRunGenBal";
            this.ckRunGenBal.Size = new System.Drawing.Size(64, 24);
            this.ckRunGenBal.TabIndex = 20;
            this.ckRunGenBal.Text = "Gen/Bal";
            // 
            // ckRunFilters
            // 
            this.ckRunFilters.Checked = true;
            this.ckRunFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckRunFilters.Image = null;
            this.ckRunFilters.Location = new System.Drawing.Point(224, 16);
            this.ckRunFilters.Name = "ckRunFilters";
            this.ckRunFilters.Size = new System.Drawing.Size(56, 24);
            this.ckRunFilters.TabIndex = 19;
            this.ckRunFilters.Text = "Filters";
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
            // txtTech
            // 
            this.txtTech.Location = new System.Drawing.Point(280, 448);
            this.txtTech.Name = "txtTech";
            this.txtTech.Size = new System.Drawing.Size(100, 20);
            this.txtTech.TabIndex = 37;
            // 
            // lblTech
            // 
            this.lblTech.Image = null;
            this.lblTech.Location = new System.Drawing.Point(280, 432);
            this.lblTech.Name = "lblTech";
            this.lblTech.Size = new System.Drawing.Size(64, 16);
            this.lblTech.TabIndex = 36;
            this.lblTech.Text = "Technician:";
            // 
            // btnSelNone
            // 
            this.btnSelNone.Image = null;
            this.btnSelNone.Location = new System.Drawing.Point(104, 360);
            this.btnSelNone.Name = "btnSelNone";
            this.btnSelNone.Size = new System.Drawing.Size(48, 20);
            this.btnSelNone.TabIndex = 43;
            this.btnSelNone.Text = "None";
            this.btnSelNone.Click += new System.EventHandler(this.btnSelNone_Click);
            // 
            // btnPostFence
            // 
            this.btnPostFence.Image = null;
            this.btnPostFence.Location = new System.Drawing.Point(168, 339);
            this.btnPostFence.Name = "btnPostFence";
            this.btnPostFence.Size = new System.Drawing.Size(56, 32);
            this.btnPostFence.TabIndex = 45;
            this.btnPostFence.Text = "Post Fence";
            // 
            // grpTestReceiver
            // 
            this.grpTestReceiver.Controls.Add(this.btnCalNone);
            this.grpTestReceiver.Controls.Add(this.btnCalAll);
            this.grpTestReceiver.Controls.Add(this.ckCalImage);
            this.grpTestReceiver.Controls.Add(this.ckCalLevel);
            this.grpTestReceiver.Location = new System.Drawing.Point(8, 432);
            this.grpTestReceiver.Name = "grpTestReceiver";
            this.grpTestReceiver.Size = new System.Drawing.Size(256, 48);
            this.grpTestReceiver.TabIndex = 40;
            this.grpTestReceiver.TabStop = false;
            this.grpTestReceiver.Text = "Calibrations";
            // 
            // btnCalNone
            // 
            this.btnCalNone.Image = null;
            this.btnCalNone.Location = new System.Drawing.Point(208, 16);
            this.btnCalNone.Name = "btnCalNone";
            this.btnCalNone.Size = new System.Drawing.Size(42, 24);
            this.btnCalNone.TabIndex = 27;
            this.btnCalNone.Text = "None";
            this.btnCalNone.Click += new System.EventHandler(this.btnCalNone_Click);
            // 
            // btnCalAll
            // 
            this.btnCalAll.Image = null;
            this.btnCalAll.Location = new System.Drawing.Point(160, 16);
            this.btnCalAll.Name = "btnCalAll";
            this.btnCalAll.Size = new System.Drawing.Size(40, 24);
            this.btnCalAll.TabIndex = 26;
            this.btnCalAll.Text = "All";
            this.btnCalAll.Click += new System.EventHandler(this.btnCalAll_Click);
            // 
            // ckCalImage
            // 
            this.ckCalImage.Checked = true;
            this.ckCalImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckCalImage.Image = null;
            this.ckCalImage.Location = new System.Drawing.Point(80, 16);
            this.ckCalImage.Name = "ckCalImage";
            this.ckCalImage.Size = new System.Drawing.Size(56, 24);
            this.ckCalImage.TabIndex = 21;
            this.ckCalImage.Text = "Image";
            // 
            // ckCalLevel
            // 
            this.ckCalLevel.Checked = true;
            this.ckCalLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckCalLevel.Image = null;
            this.ckCalLevel.Location = new System.Drawing.Point(16, 16);
            this.ckCalLevel.Name = "ckCalLevel";
            this.ckCalLevel.Size = new System.Drawing.Size(56, 24);
            this.ckCalLevel.TabIndex = 20;
            this.ckCalLevel.Text = "Level";
            // 
            // btnSelAll
            // 
            this.btnSelAll.Image = null;
            this.btnSelAll.Location = new System.Drawing.Point(104, 339);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(48, 20);
            this.btnSelAll.TabIndex = 42;
            this.btnSelAll.Text = "All";
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnTestPreamp
            // 
            this.btnTestPreamp.Image = null;
            this.btnTestPreamp.Location = new System.Drawing.Point(16, 104);
            this.btnTestPreamp.Name = "btnTestPreamp";
            this.btnTestPreamp.Size = new System.Drawing.Size(75, 23);
            this.btnTestPreamp.TabIndex = 7;
            this.btnTestPreamp.Text = "Preamp";
            this.toolTip1.SetToolTip(this.btnTestPreamp, "Preamp Test: Not Run");
            this.btnTestPreamp.Click += new System.EventHandler(this.btnTestPreamp_Click);
            // 
            // btnTestNoise
            // 
            this.btnTestNoise.Image = null;
            this.btnTestNoise.Location = new System.Drawing.Point(16, 64);
            this.btnTestNoise.Name = "btnTestNoise";
            this.btnTestNoise.Size = new System.Drawing.Size(75, 23);
            this.btnTestNoise.TabIndex = 2;
            this.btnTestNoise.Text = "Noise";
            this.toolTip1.SetToolTip(this.btnTestNoise, "Noise Test: Not Run");
            this.btnTestNoise.Click += new System.EventHandler(this.btnTestNoise_Click);
            // 
            // btnTestGenBal
            // 
            this.btnTestGenBal.Image = null;
            this.btnTestGenBal.Location = new System.Drawing.Point(16, 24);
            this.btnTestGenBal.Name = "btnTestGenBal";
            this.btnTestGenBal.Size = new System.Drawing.Size(75, 23);
            this.btnTestGenBal.TabIndex = 1;
            this.btnTestGenBal.Text = "Gen/Bal";
            this.toolTip1.SetToolTip(this.btnTestGenBal, "Gen/Bal Test: Not Run");
            this.btnTestGenBal.Click += new System.EventHandler(this.btnTestGenBal_Click);
            // 
            // btnCalImage
            // 
            this.btnCalImage.Image = null;
            this.btnCalImage.Location = new System.Drawing.Point(16, 64);
            this.btnCalImage.Name = "btnCalImage";
            this.btnCalImage.Size = new System.Drawing.Size(75, 23);
            this.btnCalImage.TabIndex = 4;
            this.btnCalImage.Text = "Image";
            this.toolTip1.SetToolTip(this.btnCalImage, "RX Image Test: Not Run");
            this.btnCalImage.Click += new System.EventHandler(this.btnCalImage_Click);
            // 
            // btnTestFilters
            // 
            this.btnTestFilters.Image = null;
            this.btnTestFilters.Location = new System.Drawing.Point(16, 144);
            this.btnTestFilters.Name = "btnTestFilters";
            this.btnTestFilters.Size = new System.Drawing.Size(75, 23);
            this.btnTestFilters.TabIndex = 1;
            this.btnTestFilters.Text = "Filters";
            this.toolTip1.SetToolTip(this.btnTestFilters, "RX Filter Test: Not Run");
            this.btnTestFilters.Click += new System.EventHandler(this.btnTestFilters_Click);
            // 
            // btnCalLevel
            // 
            this.btnCalLevel.Image = null;
            this.btnCalLevel.Location = new System.Drawing.Point(16, 24);
            this.btnCalLevel.Name = "btnCalLevel";
            this.btnCalLevel.Size = new System.Drawing.Size(75, 23);
            this.btnCalLevel.TabIndex = 3;
            this.btnCalLevel.Text = "Level";
            this.toolTip1.SetToolTip(this.btnCalLevel, "RX Level Test: Not Run");
            this.btnCalLevel.Click += new System.EventHandler(this.btnCalLevel_Click);
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
            this.grpBands.Location = new System.Drawing.Point(128, 232);
            this.grpBands.Name = "grpBands";
            this.grpBands.Size = new System.Drawing.Size(256, 96);
            this.grpBands.TabIndex = 33;
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
            // btnRunSel
            // 
            this.btnRunSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunSel.Image = null;
            this.btnRunSel.Location = new System.Drawing.Point(8, 339);
            this.btnRunSel.Name = "btnRunSel";
            this.btnRunSel.Size = new System.Drawing.Size(88, 32);
            this.btnRunSel.TabIndex = 35;
            this.btnRunSel.Text = "Run Selected";
            this.btnRunSel.Click += new System.EventHandler(this.btnRunSel_Click);
            // 
            // lstDebug
            // 
            this.lstDebug.HorizontalScrollbar = true;
            this.lstDebug.Location = new System.Drawing.Point(128, 8);
            this.lstDebug.Name = "lstDebug";
            this.lstDebug.Size = new System.Drawing.Size(256, 212);
            this.lstDebug.TabIndex = 34;
            // 
            // grpTests
            // 
            this.grpTests.Controls.Add(this.udLevel);
            this.grpTests.Controls.Add(this.btnTestPreamp);
            this.grpTests.Controls.Add(this.btnTestNoise);
            this.grpTests.Controls.Add(this.btnTestGenBal);
            this.grpTests.Controls.Add(this.btnTestFilters);
            this.grpTests.Location = new System.Drawing.Point(8, 3);
            this.grpTests.Name = "grpTests";
            this.grpTests.Size = new System.Drawing.Size(112, 221);
            this.grpTests.TabIndex = 31;
            this.grpTests.TabStop = false;
            this.grpTests.Text = "Tests";
            // 
            // udLevel
            // 
            this.udLevel.DecimalPlaces = 1;
            this.udLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udLevel.Location = new System.Drawing.Point(17, 182);
            this.udLevel.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udLevel.Name = "udLevel";
            this.udLevel.Size = new System.Drawing.Size(74, 20);
            this.udLevel.TabIndex = 8;
            this.udLevel.Value = new decimal(new int[] {
            240,
            0,
            0,
            -2147418112});
            this.udLevel.Visible = false;
            // 
            // grpCal
            // 
            this.grpCal.Controls.Add(this.btnCalImage);
            this.grpCal.Controls.Add(this.btnCalLevel);
            this.grpCal.Location = new System.Drawing.Point(8, 232);
            this.grpCal.Name = "grpCal";
            this.grpCal.Size = new System.Drawing.Size(112, 96);
            this.grpCal.TabIndex = 30;
            this.grpCal.TabStop = false;
            this.grpCal.Text = "Calibration";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(272, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 40);
            this.label1.TabIndex = 46;
            this.label1.Text = "RX2";
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.Image = null;
            this.checkBox1.Location = new System.Drawing.Point(368, 344);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(24, 24);
            this.checkBox1.TabIndex = 47;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FLEX5000RX2CalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(392, 486);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpTestReceiver);
            this.Controls.Add(this.txtTech);
            this.Controls.Add(this.lblTech);
            this.Controls.Add(this.btnSelNone);
            this.Controls.Add(this.btnPostFence);
            this.Controls.Add(this.btnSelAll);
            this.Controls.Add(this.grpBands);
            this.Controls.Add(this.btnRunSel);
            this.Controls.Add(this.lstDebug);
            this.Controls.Add(this.grpTests);
            this.Controls.Add(this.grpCal);
            this.Controls.Add(this.grpTestGeneral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FLEX5000RX2CalForm";
            this.Text = "FLEX-5000 RX2 Calibration";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FLEX5000RX2CalForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FLEX5000RX2CalForm_KeyDown);
            this.grpTestGeneral.ResumeLayout(false);
            this.grpTestReceiver.ResumeLayout(false);
            this.grpBands.ResumeLayout(false);
            this.grpTests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udLevel)).EndInit();
            this.grpCal.ResumeLayout(false);
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

		#region Event Handlers

		private void FLEX5000RX2CalForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FLEX5000RX2CalForm");
		}

		private void btnTestAll_Click(object sender, System.EventArgs e)
		{
			ckRunGenBal.Checked = true;
			ckRunNoise.Checked = true;
			ckRunPreamp.Checked = true;
			ckRunFilters.Checked = true;
		}

		private void btnTestNone_Click(object sender, System.EventArgs e)
		{
			ckRunGenBal.Checked = false;
			ckRunNoise.Checked = false;
			ckRunPreamp.Checked = false;
			ckRunFilters.Checked = false;
		}

		private void btnCalAll_Click(object sender, System.EventArgs e)
		{
			ckCalLevel.Checked = true;
			ckCalImage.Checked = true;
		}

		private void btnCalNone_Click(object sender, System.EventArgs e)
		{
			ckCalLevel.Checked = false;
			ckCalImage.Checked = false;
		}

		private void btnSelAll_Click(object sender, System.EventArgs e)
		{
			btnTestAll_Click(this, EventArgs.Empty);
			btnCalAll_Click(this, EventArgs.Empty);
		}

		private void btnSelNone_Click(object sender, System.EventArgs e)
		{
			btnTestNone_Click(this, EventArgs.Empty);
			btnCalNone_Click(this, EventArgs.Empty);
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

		private void btnRunSel_Click(object sender, System.EventArgs e)
		{
			console.PowerOn = true;
			btnRunSel.BackColor = console.ButtonSelectedColor;
			btnRunSel.Enabled = false;
			Thread t = new Thread(new ThreadStart(RunSel));
			t.Name = "Run Selected Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void RunSel()
		{
			console.VFOSplit = false;
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();

			string start_bands = GetStringFromBands();

			#region Tests

			// run all general tests
			if(ckRunGenBal.Checked)
			{
				Invoke(new MethodInvoker(btnTestGenBal.PerformClick));
				Thread.Sleep(3000);
			}

			/*if(ckRunNoise.Checked)
			{
				Invoke(new MethodInvoker(btnTestNoise.PerformClick));
				Thread.Sleep(3000);
			}*/

			if(ckRunPreamp.Checked)
			{
				Invoke(new MethodInvoker(btnTestPreamp.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckRunFilters.Checked)
			{
				Invoke(new MethodInvoker(btnTestFilters.PerformClick));
				while(true)
				{
					while(p.Visible) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(!p.Visible) break;
				}
				if(p.Text == "") goto end;
			}

			// re-run any tests that failed
			if(ckRunGenBal.Checked && btnTestGenBal.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnTestGenBal.PerformClick));
				Thread.Sleep(3000);
			}

			/*if(ckRunNoise.Checked && btnTestNoise.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnTestNoise.PerformClick));
				Thread.Sleep(3000);
			}*/

			if(ckRunPreamp.Checked && btnTestPreamp.BackColor != Color.Green)
			{
				Invoke(new MethodInvoker(btnTestPreamp.PerformClick));
				Thread.Sleep(3000);
			}

			if(ckRunFilters.Checked && btnTestFilters.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnTestFilters));
				Invoke(new MethodInvoker(btnTestFilters.PerformClick));
				while(true)
				{
					while(p.Visible) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(!p.Visible) break;
				}
				if(p.Text == "") goto end;
			}

			// reset bands back to start
			SetBandFromString(start_bands);

			if((ckRunGenBal.Checked && btnTestGenBal.BackColor != Color.Green) ||
				(ckRunPreamp.Checked && btnTestPreamp.BackColor != Color.Green) ||
				(ckRunFilters.Checked && btnTestFilters.BackColor != Color.Green))
				goto end;			

			#endregion

			#region Cal

			// run all calibrations
			if(ckCalImage.Checked)
			{
				Invoke(new MethodInvoker(btnCalImage.PerformClick));
				while(true)
				{
					while(!btnCalImage.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnCalImage.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			// re-run any Image tests that failed
			if(ckCalImage.Checked && btnCalImage.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnCalImage));
				Invoke(new MethodInvoker(btnCalImage.PerformClick));
				while(true)
				{
					while(!btnCalImage.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnCalImage.Enabled) break;
				}
				if(p.Text == "") goto end;
			}

			if(ckCalLevel.Checked)
			{
				Invoke(new MethodInvoker(btnCalLevel.PerformClick));
				while(true)
				{
					while(!btnCalLevel.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnCalLevel.Enabled) break;
				}
				if(p.Text == "") goto end;
			}			

			// re-run any Level tests that failed
			if(ckCalLevel.Checked && btnCalLevel.BackColor != Color.Green)
			{
				SetBandFromString(toolTip1.GetToolTip(btnCalLevel));
				Invoke(new MethodInvoker(btnCalLevel.PerformClick));
				while(true)
				{
					while(!btnCalLevel.Enabled) Thread.Sleep(2000);
					Thread.Sleep(2000);
					if(btnCalLevel.Enabled) break;
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


			end:
				btnRunSel.BackColor = SystemColors.Control;
			btnRunSel.Enabled = true;

			t1.Stop();
			Debug.WriteLine("Run Selected Time: "+((int)(t1.Duration/60)).ToString()+":"+(((int)t1.Duration)%60).ToString("00"));
		}

		#endregion		

		#region Tests

		#region Gen/Bal

		private void btnTestGenBal_Click(object sender, System.EventArgs e)
		{
			btnTestGenBal.Enabled = false;
			btnTestGenBal.BackColor = console.ButtonSelectedColor;
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

			bool rx1_sr = console.SpurReduction;
			console.SpurReduction = true;

			bool rx2_sr = console.RX2SpurReduction;
			console.RX2SpurReduction = true;

			bool rx2_enabled = console.RX2Enabled;
			console.RX2Enabled = true;

			console.VFOSplit = false;

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			double vfob_freq = console.VFOBFreq;
			
			FWCAnt rx2_ant = console.RX2Ant;
			console.RX2Ant = FWCAnt.RX1TAP;

			DSPMode dsp1 = console.RX1DSPMode;			
			console.RX1DSPMode = DSPMode.DSB;

			DSPMode dsp2 = console.RX2DSPMode;
			console.RX2DSPMode = DSPMode.DSB;
			//Thread.Sleep(50);

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			PreampMode rx2_preamp = console.RX2PreampMode;
			console.RX2PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			Filter filter = console.RX2Filter;
			int var_low = console.RX2FilterLow;
			int var_high = console.RX2FilterHigh;
			console.RX2Filter = Filter.VAR1;
			console.UpdateRX2Filters(-1000, 1000);

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
			FWC.SetRX2Filter(-1.0f);
			Thread.Sleep(1000);

			float adc_l = 0.0f;
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				adc_l += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			adc_l /= 5;

			float adc_r = 0.0f;
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_IMAG);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				adc_r += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_IMAG);
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
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				off_l += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			off_l /= 5;

			test_genbal = "Gen/Bal Test: ";
			bool b = true;
			if(adc_l - off_l < 50.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_L S/N <50dB ("+(adc_l-off_l).ToString("f1")+")");
				test_genbal += "ADC_L S/N <50dB ("+(adc_l-off_l).ToString("f1")+")\n";
				b = false;
			}
			if(adc_r - off_l < 50.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_R S/N <50dB ("+(adc_r-off_l).ToString("f1")+")");
				test_genbal += "ADC_R S/N <50dB ("+(adc_r-off_l).ToString("f1")+")\n";
				b = false;
			}
			if(Math.Abs(adc_r - adc_l) > 1.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed Chan Bal >1dB ("+Math.Abs(adc_r-adc_l).ToString("f1")+")");
				test_genbal += "Chan Bal >1dB ("+Math.Abs(adc_r-adc_l).ToString("f1")+")\n";
				b = false;
			}
			if(Math.Abs(-26.5 - adc_l) > 1.0f && Math.Abs(-32.0 - adc_l) > 1.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_L -26.5+/-1.0 ("+adc_l.ToString("f1")+")");
				test_genbal += "ADC_L -26.5+/-1.0 ("+adc_l.ToString("f1")+")\n";
				b = false;
			}
			if(Math.Abs(-27.0 - adc_r) > 1.0f && Math.Abs(-32.1 - adc_r) > 1.0f)
			{
				lstDebug.Items.Insert(0, " Gen/Bal Test: Failed ADC_R -27.0+/-1.0 ("+adc_r.ToString("f1")+")");
				test_genbal += "ADC_R -27.0+/-1.0 ("+adc_r.ToString("f1")+")\n";
				b = false;
			}

			if(b)
			{
				btnTestGenBal.BackColor = Color.Green;
				test_genbal = "Gen/Bal Test: Passed";
				lstDebug.Items.Insert(0, test_genbal);
			}
			else
			{
				btnTestGenBal.BackColor = Color.Red;
			}
			toolTip1.SetToolTip(btnTestGenBal, test_genbal);

			console.SpurReduction = rx1_sr;
			//Thread.Sleep(50);
			console.RX2SpurReduction = rx2_sr;
			//Thread.Sleep(50);
			console.FullDuplex = full_duplex;
			console.RX2Enabled = rx2_enabled;
			console.RX2Ant = rx2_ant;
			//Thread.Sleep(50);
			console.RX1DSPMode = dsp1;
			console.RX2DSPMode = dsp2;
			//Thread.Sleep(50);
			console.VFOAFreq = vfoa_freq;
			//Thread.Sleep(100);
			console.VFOBFreq = vfob_freq;
			//Thread.Sleep(100);
			console.RX1PreampMode = preamp;
			//Thread.Sleep(50);
			console.RX2PreampMode = rx2_preamp;
			//Thread.Sleep(50);
			console.RX2Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX2FilterLow = var_low;
				console.RX2FilterHigh = var_high;
			}
			btnTestGenBal.Enabled = true;

			string path = console.AppDataPath+"\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			/*path += "\\RX2 GenBal";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);*/
			bool file_exists = File.Exists(path+"\\rx2_genbal.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx2_genbal.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, ADC_L, ADC_R, Off_L, Passed");
			writer.WriteLine(FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+adc_l.ToString("f1")+","+adc_r.ToString("f1")+","+off_l.ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region Noise

		private void btnTestNoise_Click(object sender, System.EventArgs e)
		{
			btnTestNoise.Enabled = false;
			btnTestNoise.BackColor = console.ButtonSelectedColor;
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

			bool rx1_sr = console.SpurReduction;
			console.SpurReduction = true;
			//Thread.Sleep(50);

			bool rx2_sr = console.RX2SpurReduction;
			console.RX2SpurReduction = true;
			//Thread.Sleep(50);

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			double vfob_freq = console.VFOBFreq;

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;
			//Thread.Sleep(50);

			DSPMode dsp2 = console.RX2DSPMode;
			console.RX2DSPMode = DSPMode.DSB;

			bool rx2_enabled = console.RX2Enabled;
			console.RX2Enabled = true;

			FWCAnt rx2_ant = console.RX2Ant;
			console.RX2Ant = FWCAnt.RX2IN;
			//Thread.Sleep(50);

			console.VFOAFreq = 1.0;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.1;
			//Thread.Sleep(100);

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.HIGH;
			//Thread.Sleep(50);

			MeterTXMode tx_meter = console.CurrentMeterTXMode;
			console.CurrentMeterTXMode = MeterTXMode.OFF;

			Filter filter = console.RX2Filter;
			int var_low = console.RX2FilterLow;
			int var_high = console.RX2FilterHigh;
			console.UpdateRX2Filters(-1000, 1000);

			Thread.Sleep(500);

			float[] a = new float[Display.BUFFER_SIZE];
			float sum = 0.0f;

			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);

			for(int j=0; j<5; j++)
			{
				sum += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				Thread.Sleep(50);
			}

			float avg = sum / 5;

			Debug.WriteLine("Noise Test: "+avg.ToString("f1")+" dB");
			bool b = (avg > -70.0);
			if(b)
			{
				btnTestNoise.BackColor = Color.Red;
				test_noise = " Noise Test: Failed dbFS > -70dB ("+avg.ToString("f1")+")";
			}
			else 
			{
				btnTestNoise.BackColor = Color.Green;
				test_noise = "Noise Test: Passed ("+avg.ToString("f1")+"dBFS)";
			}
			toolTip1.SetToolTip(btnTestNoise, test_noise);
			lstDebug.Items.Insert(0, test_noise);

			console.SpurReduction = rx1_sr;
			//Thread.Sleep(50);
			console.RX2SpurReduction = rx2_sr;
			//Thread.Sleep(50);
			console.CurrentMeterTXMode = tx_meter;
			console.FullDuplex = full_duplex;
			console.RX1DSPMode = dsp;
			//Thread.Sleep(50);
			console.RX2DSPMode = dsp2;
			console.VFOBFreq = vfob_freq;
			//Thread.Sleep(100);
			console.VFOAFreq = vfoa_freq;
			//Thread.Sleep(100);
			console.RX2Enabled = rx2_enabled;
			console.RX2Ant = rx2_ant;
			//Thread.Sleep(50);
			console.RX1PreampMode = preamp;
			//Thread.Sleep(50);
			console.RX2Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX2FilterLow = var_low;
				console.RX2FilterHigh = var_high;
			}
			btnTestNoise.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			/*path += "\\RX2 Noise";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);*/
			path += "\\rx2_noise.csv";
			bool file_exists = File.Exists(path);
			StreamWriter writer = new StreamWriter(path, true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, dBFS, Passed");
			writer.WriteLine(FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+avg.ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region Preamp

		private void btnTestPreamp_Click(object sender, System.EventArgs e)
		{
			btnTestPreamp.Enabled = false;
			btnTestPreamp.BackColor = console.ButtonSelectedColor;
			Thread t = new Thread(new ThreadStart(TestPreamp));
			t.Name = "Test Preamp Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_preamp = "Preamp Test: Not Run";
		unsafe private void TestPreamp()
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

			bool rx1_sr = console.SpurReduction;
			console.SpurReduction = true;
			//Thread.Sleep(50);

			bool rx2_sr = console.RX2SpurReduction;
			console.RX2SpurReduction = true;
			//Thread.Sleep(50);

			bool full_duplex = console.FullDuplex;
			console.FullDuplex = false;

			double vfoa_freq = console.VFOAFreq;
			double vfob_freq = console.VFOBFreq;

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			DSPMode dsp = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.DSB;
			//Thread.Sleep(50);

			DSPMode dsp2 = console.RX2DSPMode;
			console.RX2DSPMode = DSPMode.DSB;
			//Thread.Sleep(50);

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			Filter filter = console.RX2Filter;
			int var_low = console.RX2FilterLow;
			int var_high = console.RX2FilterHigh;
			console.UpdateRX2Filters(-1000, 1000);

			bool rx2_enabled = console.RX2Enabled;
			console.RX2Enabled = true;

			FWCAnt rx2_ant = console.RX2Ant;
			console.RX2Ant = FWCAnt.RX1TAP;
			//Thread.Sleep(50);

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
			FWC.SetRX2Preamp(false);
			Thread.Sleep(1000);

			float off = 0.0f;
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				off += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			off /= 5;

			FWC.SetRX2Preamp(true);
			Thread.Sleep(500);

			float on = 0.0f;
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				on += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			on /= 5;

			Debug.WriteLine("Preamp Test: "+(on-off).ToString("f1")+" dB");
			bool b = (Math.Abs(14.0 - (on-off)) <= 1.0f); // pass from 13 - 15dB
			if(!b)
			{
				btnTestPreamp.BackColor = Color.Red;
				test_preamp = " Preamp Test: Failed delta > 14 +/-1dB ("+(on-off).ToString("f1")+")";
			}
			else
			{
				btnTestPreamp.BackColor = Color.Green;
				test_preamp = "Preamp Test: Passed ("+(on-off).ToString("f1")+")";
			}
			toolTip1.SetToolTip(btnTestPreamp, test_preamp);
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
			
			console.SpurReduction = rx1_sr;
			//Thread.Sleep(50);
			console.RX2SpurReduction = rx2_sr;
			//Thread.Sleep(50);
			console.FullDuplex = full_duplex;
			console.RX1DSPMode = dsp;
			//Thread.Sleep(50);
			console.RX2DSPMode = dsp2;
			console.VFOAFreq = vfoa_freq;
			//Thread.Sleep(100);
			console.VFOBFreq = vfob_freq;
			//Thread.Sleep(100);
			console.RX2Enabled = rx2_enabled;
			console.RX2Ant = rx2_ant;
			//Thread.Sleep(50);
			console.RX2Filter = filter;
			if(filter == Filter.VAR1 || filter == Filter.VAR2)
			{
				console.RX2FilterLow = var_low;
				console.RX2FilterHigh = var_high;
			}

			btnTestPreamp.Enabled = true;

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);

			bool file_exists = File.Exists(path+"\\rx2_preamp.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx2_preamp.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, On, Off, Diff, Passed");
			writer.WriteLine(FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+","
				+on.ToString("f1")+","+off.ToString("f1")+","+(on-off).ToString("f1")+","
				+b.ToString());
			writer.Close();
		}

		#endregion

		#region Filters

		private void btnTestFilters_Click(object sender, System.EventArgs e)
		{
			p = new Progress("Test RX2 Filter");
			grpTests.Enabled = false;
			grpCal.Enabled = false;
			btnTestFilters.BackColor = Color.Green;
			Thread t = new Thread(new ThreadStart(TestRX2Filters));
			t.Name = "Test RX2 Filters Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
			Invoke(new MethodInvoker(p.Show));
		}

		private string test_rx2_filters = "RX2 Filters Test: Not Run";
		unsafe private void TestRX2Filters()
		{
			float[] avg = {1.4f, 1.3f, 0.9f, 1.2f, 0.6f, 0.7f, 0.7f, 0.6f, 0.5f, 0.7f, 0.5f};	// avg filter loss in dB
			float[] avg2 = {0.7f, 0.7f, 0.9f, -0.3f, 0.0f, 0.9f, 2.2f, 1.6f, 1.4f, 1.7f, 0.0f}; // new data due to no 50 Ohm source imp.
			//float tol = 0.5f; // tolerance
			/*if(!console.PowerOn)
			{
				p.Hide();
				MessageBox.Show("Power must be on to run this test.",
					"Power not on",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);					
				grpTests.Enabled = true;
				grpCal.Enabled = true;
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
			test_rx2_filters = "RX2 Filters Test: Passed";

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

			bool rx1_sr = console.SpurReduction;
			console.SpurReduction = false;
			//Thread.Sleep(50);

			bool rx2_sr = console.RX2SpurReduction;
			console.RX2SpurReduction = false;
			//Thread.Sleep(50);

			DSPMode dsp_mode = console.RX2DSPMode;
			console.RX2DSPMode = DSPMode.DSB;
			
			bool rit_on = console.RITOn;
			console.RITOn = false;

			bool rx2 = console.RX2Enabled;
			console.RX2Enabled = true;

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			PreampMode preamp2 = console.RX2PreampMode;
			console.RX2PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(true);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(true);
			Thread.Sleep(200);

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			DSPMode dsp = console.RX1DSPMode;
			DSPMode dsp2 = console.RX2DSPMode;

			console.RX1DSPMode = DSPMode.DSB;
			//Thread.Sleep(50);
			console.RX2DSPMode = DSPMode.DSB;

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			Filter filter = console.RX2Filter;
			int var_low = console.RX2FilterLow;
			int var_high = console.RX2FilterHigh;
			console.UpdateRX2Filters(-1000, 1000);

			FWC.SetRX2Preamp(false);
			//Thread.Sleep(50);
			FWC.SetRX2Ant(6); // RX1 Tap
			Thread.Sleep(500);

			float rx1_adc = 0.0f;
			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				rx1_adc += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			rx1_adc /= 5;

			float rx2_adc = 0.0f;
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				rx2_adc += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			rx2_adc /= 5;

			console.RX1DSPMode = dsp;
			//Thread.Sleep(50);
			console.RX2DSPMode = dsp2;

			bool resistor = ((rx1_adc-rx2_adc)>4.0);
			Debug.WriteLine("resistor: "+resistor);

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

			int total_counts = 10*num_bands;

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
					//Thread.Sleep(100);
					console.VFOBFreq = band_freqs[i];
					//Thread.Sleep(100);

					dsp = console.RX1DSPMode;
					dsp2 = console.RX2DSPMode;

					console.RX1DSPMode = DSPMode.DSB;
					//Thread.Sleep(50);
					console.RX2DSPMode = DSPMode.DSB;

					console.VFOAFreq = band_freqs[i];
					//Thread.Sleep(100);
					console.VFOBFreq = band_freqs[i];
					//Thread.Sleep(100);

					filter = console.RX2Filter;
					var_low = console.RX2FilterLow;
					var_high = console.RX2FilterHigh;
					console.UpdateRX2Filters(-1000, 1000);

					FWC.SetRX2Preamp(false);
					//Thread.Sleep(50);
					FWC.SetRX2Ant(6); // RX1 Tap
					Thread.Sleep(500);

					float on = 0.0f;
					DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						on += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
						if(j != 4) Thread.Sleep(50);
					}
					on /= 5;

					FWC.SetRX2Filter(-1.0f);
					//Thread.Sleep(500);

					float off = 0.0f;
					DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.SIGNAL_STRENGTH);
					Thread.Sleep(50);
					for(int j=0; j<5; j++)
					{
						off += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						if(!p.Visible) 
						{
							p.Text = "";
							goto end;
						}
						p.SetPercent(++counter/(float)total_counts);
						if(j != 4) Thread.Sleep(50);
					}
					off /= 5;

					FWC.SetRX2Filter(band_freqs[i]);
					//Thread.Sleep(50);

					on_table[i] = on;
					off_table[i] = off;

					if((on < -10.0 || off < -10.0) ||
						(resistor && ((off-on) > avg[i]+1.0f || (off-on) < -3.0f)) ||
						(!resistor && ((off-on) > avg2[i]+1.0f || (off-on) < -3.0f)))
					{
						btnTestFilters.BackColor = Color.Red;
						if(!test_rx2_filters.StartsWith("RX2 Filter Test: Failed ("))
							test_rx2_filters = "RX2 Filter Test: Failed (";
						test_rx2_filters += BandToString(bands[i])+",";
						lstDebug.Items.Insert(0, "RX2 Filter Test - "+BandToString(bands[i])+": Failed ("
							+off.ToString("f1")+", "+on.ToString("f1")+", "+(off-on).ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "RX2 Filter Test - "+BandToString(bands[i])+": Passed ("
							+off.ToString("f1")+", "+on.ToString("f1")+", "+(off-on).ToString("f1")+")");
					}

					Debug.Write((off-on).ToString("f1")+" ");
					//Debug.WriteLine(band_freqs[i].ToString("f6")+" diff: "+(no_filter-with_filter).ToString("f1")+"dB");
					console.RX2Filter = filter;
					if(filter == Filter.VAR1 || filter == Filter.VAR2)
					{
						console.RX2FilterLow = var_low;
						console.RX2FilterHigh = var_high;
					}

					console.RX1DSPMode = dsp;
					//Thread.Sleep(50);
					console.RX2DSPMode = dsp2;
				}
			}
			Debug.WriteLine("");

			end:
				if(test_rx2_filters.StartsWith("RX2 Filter Test: Failed ("))
					test_rx2_filters = test_rx2_filters.Substring(0, test_rx2_filters.Length-1)+")";
			toolTip1.SetToolTip(btnTestFilters, test_rx2_filters);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx2_filters.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx2_filters.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, "
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
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+","
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
			writer = new StreamWriter(path+"\\rx_filter_"+FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+".csv");
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
			FWC.SetFullDuplex(false);
			console.SpurReduction = rx1_sr;
			console.RX2SpurReduction = rx2_sr;
			console.RX2DSPMode = dsp_mode;
			console.VFOAFreq = vfoa;
			console.VFOBFreq = vfob;
			console.RX1PreampMode = preamp;
			console.RITOn = rit_on;
			console.RX2Enabled = rx2;
			
			Invoke(new MethodInvoker(p.Hide));
			grpTests.Enabled = true;
			grpCal.Enabled = true;
		}

		#endregion

		#endregion

		#region Cal

		#region Level

		private void btnCalLevel_Click(object sender, System.EventArgs e)
		{
			grpTests.Enabled = false;
			grpCal.Enabled = false;
			btnCalLevel.BackColor = Color.Green;

			p = new Progress("Calibrate RX Level");
			Thread t = new Thread(new ThreadStart(CalRX2Level));
			t.Name = "Calibrate RX2 Level Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_rx2_level = "RX2 Level Test: Not Run";
		public void CalRX2Level()
		{
			float[] offset_avg = {-55.7f, -55.8f, -56.1f, -57.6f, -55.1f, -56.0f, -55.9f, -56.3f, -55.4f, -55.8f, -55.0f};
			float[] offset_avg2 = {-62.6f, -61.5f, -61.8f, -62.8f, -61.9f, -61.9f, -61.3f, -60.8f, -60.7f, -61.4f, -60.7f};
			float[] preamp_avg = {-13.7f, -7.1f, -13.5f, -13.8f, -14.2f, -14.2f, -14.1f, -14.0f, -14.0f, -14.0f, -13.9f};
			float[] preamp_avg2 = {-12.4f, -3.8f, -14.0f, -15.1f, -12.9f, -13.4f, -14.6f, -15.6f, -14.7f, -14.0f, -14.1f};
			//float offset_tol = 2.5f;	// maximum distance from the average value
			//float preamp_tol = 1.5f;

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;

			bool rx1_sr = console.SpurReduction;
			console.SpurReduction = false;
			//Thread.Sleep(50);

			bool rx2_sr = console.RX2SpurReduction;
			console.RX2SpurReduction = false;
			//Thread.Sleep(50);

			DSPMode dsp_mode = console.RX2DSPMode;
			console.RX2DSPMode = DSPMode.DSB;
			
			bool rit_on = console.RITOn;
			console.RITOn = false;

			bool rx2 = console.RX2Enabled;
			console.RX2Enabled = true;

			PreampMode preamp = console.RX1PreampMode;
			console.RX1PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			PreampMode preamp2 = console.RX2PreampMode;
			console.RX2PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			FWC.SetTest(true);
			//Thread.Sleep(50);
			FWC.SetSig(true);
			//Thread.Sleep(50);
			FWC.SetGen(true);
			//Thread.Sleep(50);
			FWC.SetFullDuplex(true);
			Thread.Sleep(200);

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			console.RX1PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);
			console.RX2PreampMode = PreampMode.OFF;
			//Thread.Sleep(50);

			DSPMode dsp = console.RX1DSPMode;
			DSPMode dsp2 = console.RX2DSPMode;

			console.RX1DSPMode = DSPMode.DSB;
			//Thread.Sleep(50);
			console.RX2DSPMode = DSPMode.DSB;

			console.VFOAFreq = 14.2;
			//Thread.Sleep(100);
			console.VFOBFreq = 14.2;
			//Thread.Sleep(100);

			Filter filter = console.RX2Filter;
			int var_low = console.RX2FilterLow;
			int var_high = console.RX2FilterHigh;
			console.UpdateRX2Filters(-1000, 1000);

			//Thread.Sleep(50);
			FWC.SetRX2Ant(6); // RX1 Tap
			Thread.Sleep(500);

			float rx1_adc = 0.0f;
			DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				rx1_adc += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			rx1_adc /= 5;

			float rx2_adc = 0.0f;
			DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
			Thread.Sleep(50);
			for(int j=0; j<5; j++)
			{
				rx2_adc += DttSP.CalculateRXMeter(2, 0, DttSP.MeterType.ADC_REAL);
				if(j != 4) Thread.Sleep(50);
			}
			rx2_adc /= 5;

			console.RX1DSPMode = dsp;
			//Thread.Sleep(50);
			console.RX2DSPMode = dsp2;

			float res_offset = rx1_adc-rx2_adc;
			console.rx2_res_offset = res_offset;
			bool resistor = ((rx1_adc-rx2_adc)>4.0);
			Debug.WriteLine("resistor: "+resistor+" "+res_offset.ToString("f2"));

			test_rx2_level = "RX2 Level Test: Passed";
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();
			Band[] bands = { Band.B6M, Band.B160M, Band.B80M, Band.B60M, Band.B40M, Band.B30M, Band.B20M, Band.B17M, Band.B15M, Band.B12M, Band.B10M };
			float[] band_freqs = { 50.11f, 1.85f, 3.75f, 5.3665f, 7.15f, 10.125f, 14.175f, 18.1f, 21.300f, 24.9f, 28.4f };

			for(int i=0; i<band_freqs.Length; i++)
			{
				bool do_band = false;
				switch(bands[i])
				{
					case Band.B160M: do_band = ck160.Checked; break;
					case Band.B80M:  do_band = ck80.Checked;  break;
					case Band.B60M:  do_band = ck60.Checked;  break;
					case Band.B40M:  do_band = ck40.Checked;  break;
					case Band.B30M:  do_band = ck30.Checked;  break;
					case Band.B20M:  do_band = ck20.Checked;  break;
					case Band.B17M:  do_band = ck17.Checked;  break;
					case Band.B15M:  do_band = ck15.Checked;  break;
					case Band.B12M:  do_band = ck12.Checked;  break;
					case Band.B10M:	 do_band = ck10.Checked;  break;
					case Band.B6M:   do_band = ck6.Checked;   break;
				}

				if(do_band)
				{
					p.SetPercent(0.0f);
					Invoke(new MethodInvoker(p.Show));
					Application.DoEvents();
					console.VFOAFreq = band_freqs[i];
					console.VFOBFreq = band_freqs[i];
					console.CalibrateRX2Level((float)udLevel.Value, band_freqs[i], p, true);
					if(p.Text == "") break;

					float display_offset = console.GetRX2Level(bands[i], 0);
					float preamp_offset = console.GetRX2Level(bands[i], 1);
					float meter_offset = console.GetRX2Level(bands[i], 2);

					/*if((resistor && 
						((Math.Abs(display_offset-offset_avg[i]) > offset_tol) ||
						(Math.Abs(preamp_offset-preamp_avg[i]) > preamp_tol))) ||
						(!resistor &&
						((Math.Abs(display_offset-offset_avg2[i]) > offset_tol) ||
						(Math.Abs(preamp_offset-preamp_avg2[i]) > preamp_tol))))
					{
						btnCalLevel.BackColor = Color.Red;
						if(!test_rx2_level.StartsWith("RX2 Level Test: Failed ("))
							test_rx2_level = "RX2 Level Test: Failed (";
						test_rx2_level += BandToString(bands[i])+", ";
						lstDebug.Items.Insert(0, "RX2 Level Cal - "+BandToString(bands[i])+": Failed ("
							+display_offset.ToString("f1")+", "
							+preamp_offset.ToString("f1")+", "
							+meter_offset.ToString("f1")+")");
					}
					else
					{*/
						lstDebug.Items.Insert(0, "RX2 Level Cal - "+BandToString(bands[i])+": Passed ("
							+display_offset.ToString("f1")+", "
							+preamp_offset.ToString("f1")+", "
							+meter_offset.ToString("f1")+")");
					//}
						
					Thread.Sleep(500);				
				}
			}

			if(test_rx2_level.StartsWith("RX2 Level Test: Failed ("))
				test_rx2_level = test_rx2_level.Substring(0, test_rx2_level.Length-2)+")";
			toolTip1.SetToolTip(btnCalLevel, test_rx2_level);

			console.VFOAFreq = vfoa;
			//Thread.Sleep(100);
			console.VFOBFreq = vfob;
			//Thread.Sleep(100);

			t1.Stop();
			Debug.WriteLine("RX Level Timer: "+t1.Duration);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx2_level.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx2_level.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, "
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
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				for(int j=0; j<3; j++)
				{
					writer.Write(console.GetRX2Level(bands[i], j).ToString("f1"));
					if(i!=bands.Length-1 || j!=2) writer.Write(",");
				}
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\RX2 Level";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			writer = new StreamWriter(path+"\\rx2_level_"+FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+".csv");
			writer.WriteLine("Band, Display Offset, Preamp Offset, Multimeter Offset");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(console.GetRX2Level(bands[i], 0).ToString("f1")+",");
				writer.Write(console.GetRX2Level(bands[i], 1).ToString("f1")+",");
				writer.WriteLine(console.GetRX2Level(bands[i], 2).ToString("f1"));
			}
			writer.Close();

			lstDebug.Items.Insert(0, "Saving Level data to EEPROM...");
			byte checksum;
			FWCEEPROM.WriteRX2Level(console.rx2_level_table, out checksum);
			console.rx2_level_checksum = checksum;
			console.RX2SyncCalDateTime();
			lstDebug.Items[0] = "Saving Level data to EEPROM...done";

			grpTests.Enabled = true;
			grpCal.Enabled = true;
		}

		#endregion

		#region Image

		private void btnCalImage_Click(object sender, System.EventArgs e)
		{
			grpTests.Enabled = false;
			grpCal.Enabled = false;
			btnCalImage.BackColor = Color.Green;
			
			p = new Progress("Calibrate RX2 Image");
			Thread t = new Thread(new ThreadStart(CalRX2Image));
			t.Name = "Calibrate RX2 Image Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private string test_rx2_image = "RX2 Image Cal: Not Run";
		public void CalRX2Image()
		{
			float rejection_tol = 75.0f;	// rejection from worst to null
			float floor_tol = 10.0f;		// from null to noise floor
			test_rx2_image = "RX2 Image Cal: Passed";

			if(!console.PowerOn)
			{
				console.PowerOn = true;
				Thread.Sleep(500);
			}

			if(console.VFOSync)
				console.VFOSync = false;

			double vfoa = console.VFOAFreq;
			double vfob = console.VFOBFreq;
			DSPMode dsp1 = console.RX1DSPMode;
			DSPMode dsp2 = console.RX2DSPMode;
			console.RX1DSPMode = DSPMode.DSB;
			console.RX2DSPMode = DSPMode.DSB;
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
					console.VFOBFreq = band_freqs[i]+2*console.IFFreq;
					//Thread.Sleep(100);
					console.VFOAFreq = band_freqs[i];
					//Thread.Sleep(100);
					console.CalibrateRX2Image(band_freqs[i], p, true);
					if(p.Text == "") break;

					if(console.rx_image_rejection[(int)bands[i]] < rejection_tol ||
						console.rx_image_from_floor[(int)bands[i]] > floor_tol)
					{
						if(!test_rx2_image.StartsWith("RX2 Image Cal: Failed ("))
							test_rx2_image = "RX2 Image Cal: Failed (";
						test_rx2_image += BandToString(bands[i])+",";
						btnCalImage.BackColor = Color.Red;
						lstDebug.Items.Insert(0, "RX2 Image - "+BandToString(bands[i])+": Failed ("
							+console.rx_image_rejection[(int)bands[i]].ToString("f1")+", "
							+console.rx_image_from_floor[(int)bands[i]].ToString("f1")+")");
					}
					else
					{
						lstDebug.Items.Insert(0, "RX2 Image - "+BandToString(bands[i])+": Passed ("
							+console.rx_image_rejection[(int)bands[i]].ToString("f1")+", "
							+console.rx_image_from_floor[(int)bands[i]].ToString("f1")+")");
					}

					Thread.Sleep(500);
				}
			}
			console.RX1DSPMode = dsp1;
			console.RX2DSPMode = dsp2;
			console.VFOAFreq = vfoa;
			//Thread.Sleep(100);
			console.VFOBFreq = vfob;
			//Thread.Sleep(100);
			if(test_rx2_image.StartsWith("RX2 Image Cal: Failed ("))
				test_rx2_image = test_rx2_image.Substring(0, test_rx2_image.Length-1)+")";
			toolTip1.SetToolTip(btnCalImage, test_rx2_image);

			t1.Stop();
			Debug.WriteLine("RX2 Image Timer: "+t1.Duration);

            string path = console.AppDataPath + "\\Tests";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			bool file_exists = File.Exists(path+"\\rx2_image.csv");
			StreamWriter writer = new StreamWriter(path+"\\rx2_image.csv", true);
			if(!file_exists) writer.WriteLine("Serial Num, Date/Time, Version, "
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
			writer.Write(FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+","
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+","
				+console.Text+",");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(console.rx2_image_gain_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx2_image_phase_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx_image_rejection[(int)bands[i]].ToString("f1")+",");
				writer.Write(console.rx_image_from_floor[(int)bands[i]].ToString("f1")+",");
			}
			writer.WriteLine("");
			writer.Close();

			path += "\\RX2 Image";
			if(!Directory.Exists(path))	Directory.CreateDirectory(path);
			writer = new StreamWriter(path+"\\rx2_image_"+FWCEEPROM.SerialToString(FWCEEPROM.RX2Serial)+".csv");
			writer.WriteLine("Band, Gain, Phase, Rejection, Noise Distance");
			for(int i=0; i<bands.Length; i++)
			{
				writer.Write(BandToString(bands[i])+",");
				writer.Write(console.rx2_image_gain_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx2_image_phase_table[(int)bands[i]].ToString("f3")+",");
				writer.Write(console.rx_image_rejection[(int)bands[i]].ToString("f1")+",");
				writer.WriteLine(console.rx_image_from_floor[(int)bands[i]].ToString("f1"));
			}
			writer.Close();

			lstDebug.Items.Insert(0, "Saving Image data to EEPROM...");
			byte gain_sum, phase_sum;
			FWCEEPROM.WriteRX2Image(console.rx2_image_gain_table, console.rx2_image_phase_table, out gain_sum, out phase_sum);
			console.rx2_image_gain_checksum = gain_sum;
			console.rx2_image_phase_checksum = phase_sum;
			console.RX2SyncCalDateTime();
			lstDebug.Items[0] = "Saving Image data to EEPROM...done";

			grpTests.Enabled = true;
			grpCal.Enabled = true;
		}

		#endregion

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBox1.Checked)
			{
				checkBox1.BackColor = console.ButtonSelectedColor;
				Thread t = new Thread(new ThreadStart(TestCal));
				t.Name = "Test Cal Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();
			}
			else
			{
				checkBox1.BackColor = SystemColors.Control;
			}
		}

		private void TestCal()
		{
			int count = 0;
			while(checkBox1.Checked)
			{
				count++;
				while(!btnRunSel.Enabled && checkBox1.Checked)
					Thread.Sleep(1000);
				if(!checkBox1.Checked) break;
				Invoke(new MethodInvoker(btnRunSel.PerformClick));
				Thread.Sleep(1000);
			}

			MessageBox.Show("Total Runs: "+count);
		}

		#endregion

        private void FLEX5000RX2CalForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.L)
            {
                udLevel.Visible = true;
            }
        }
	}
}

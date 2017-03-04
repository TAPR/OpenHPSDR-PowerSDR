//=================================================================
// paqualify.cs
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
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
	public class PAQualify : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Progress progress;
		private Console console;
		private System.Windows.Forms.LabelTS label2;
		private System.Windows.Forms.LabelTS label4;
		private System.Windows.Forms.LabelTS label1;
		private System.Windows.Forms.LabelTS label5;
		private System.Windows.Forms.LabelTS label6;
		private System.Windows.Forms.CheckBoxTS chkSWRCal;
		private System.Windows.Forms.LabelTS label7;
		private System.Windows.Forms.LabelTS label9;
		private System.Windows.Forms.ButtonTS btnBandSweep;
		private System.Windows.Forms.CheckBoxTS chk160m;
		private System.Windows.Forms.ButtonTS btnGainCal;
		private System.Windows.Forms.CheckBoxTS chk80m;
		private System.Windows.Forms.CheckBoxTS chk60m;
		private System.Windows.Forms.CheckBoxTS chk40m;
		private System.Windows.Forms.CheckBoxTS chk30m;
		private System.Windows.Forms.CheckBoxTS chk20m;
		private System.Windows.Forms.CheckBoxTS chk10m;
		private System.Windows.Forms.CheckBoxTS chk12m;
		private System.Windows.Forms.CheckBoxTS chk15m;
		private System.Windows.Forms.CheckBoxTS chk17m;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.TextBoxTS txtSerialNum;
		private System.Windows.Forms.CheckBoxTS chkIMDTest;
		private System.Windows.Forms.LabelTS label12;
		private System.Windows.Forms.LabelTS label13;
		private System.Windows.Forms.CheckBoxTS chkFWDLow;
		private System.Windows.Forms.LabelTS label14;
		private System.Windows.Forms.LabelTS label15;
		private System.Windows.Forms.LabelTS label16;
		private System.Windows.Forms.CheckBoxTS chkFWDHigh;
		private System.Windows.Forms.ButtonTS btnPrint;
		private System.Windows.Forms.LabelTS label17;
		private System.Windows.Forms.LabelTS label18;
		private System.Windows.Forms.LabelTS label19;
		private System.Windows.Forms.LabelTS label10;
		private System.Windows.Forms.CheckBoxTS chkStep12;
		private System.Windows.Forms.CheckBoxTS chkStep11;
		private System.Windows.Forms.CheckBoxTS chkStep10;
		private System.Windows.Forms.CheckBoxTS chkStep7;
		private System.Windows.Forms.CheckBoxTS chkStep5;
		private System.Windows.Forms.CheckBoxTS chkStep3;
		private System.Windows.Forms.CheckBoxTS chkStep2;
		private System.Windows.Forms.CheckBoxTS chkStep4;
		private System.Windows.Forms.CheckBoxTS chkStep6;
		private System.Windows.Forms.CheckBoxTS chkStep8;
		private System.Windows.Forms.CheckBoxTS chkStep9;
		private System.Windows.Forms.CheckBoxTS chkStep18;
		private System.Windows.Forms.CheckBoxTS chkStep17;
		private System.Windows.Forms.CheckBoxTS chkStep15;
		private System.Windows.Forms.CheckBoxTS chkStep16;
		private System.Windows.Forms.CheckBoxTS chkStep13;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.GroupBoxTS grpStepByStep;
		private System.Windows.Forms.GroupBoxTS grpTests;
		private System.Windows.Forms.LabelTS label8;
		private System.Windows.Forms.CheckBoxTS checkBox2;
		private System.Windows.Forms.LabelTS label20;
		private System.Windows.Forms.ButtonTS btnClearCal;
		private System.Windows.Forms.ButtonTS btnClearAll;
		private System.Windows.Forms.CheckBoxTS chkBiasSet;
		private System.Windows.Forms.CheckBoxTS chkHarm30;
		private System.Windows.Forms.CheckBoxTS chkHarm60;
		private System.Windows.Forms.LabelTS label21;
		private System.Windows.Forms.CheckBoxTS checkBox3;
		private System.Windows.Forms.CheckBoxTS chkLFTest;
		private System.Windows.Forms.Timer timer_LF_test;
		private System.Windows.Forms.CheckBoxTS checkBox4;
		private System.Windows.Forms.LabelTS label11;
		private System.Windows.Forms.CheckBoxTS checkBox5;
		private System.Windows.Forms.LabelTS label22;
		private System.Windows.Forms.CheckBoxTS checkBox6;
		private System.Windows.Forms.ButtonTS btnCheckAll;
		private System.Windows.Forms.LabelTS label3;
		private System.Windows.Forms.CheckBoxTS checkBox1;
		private System.Windows.Forms.NumericUpDownTS udCalTarget;
		private System.Windows.Forms.LabelTS label23;
		private System.Windows.Forms.Button btnHarmFil30;
		private System.Windows.Forms.Button btnHarmFil60;
		private System.ComponentModel.IContainer components;
		#endregion

		#region Constructor and Destructor

		public PAQualify(Console c)
		{
			InitializeComponent();
			console = c;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PAQualify));
			this.grpStepByStep = new System.Windows.Forms.GroupBoxTS();
			this.label3 = new System.Windows.Forms.LabelTS();
			this.checkBox1 = new System.Windows.Forms.CheckBoxTS();
			this.btnCheckAll = new System.Windows.Forms.ButtonTS();
			this.label22 = new System.Windows.Forms.LabelTS();
			this.checkBox6 = new System.Windows.Forms.CheckBoxTS();
			this.label11 = new System.Windows.Forms.LabelTS();
			this.checkBox5 = new System.Windows.Forms.CheckBoxTS();
			this.checkBox4 = new System.Windows.Forms.CheckBoxTS();
			this.label21 = new System.Windows.Forms.LabelTS();
			this.btnClearCal = new System.Windows.Forms.ButtonTS();
			this.label8 = new System.Windows.Forms.LabelTS();
			this.checkBox2 = new System.Windows.Forms.CheckBoxTS();
			this.label19 = new System.Windows.Forms.LabelTS();
			this.chkStep18 = new System.Windows.Forms.CheckBoxTS();
			this.label17 = new System.Windows.Forms.LabelTS();
			this.chkStep8 = new System.Windows.Forms.CheckBoxTS();
			this.label16 = new System.Windows.Forms.LabelTS();
			this.chkStep6 = new System.Windows.Forms.CheckBoxTS();
			this.label15 = new System.Windows.Forms.LabelTS();
			this.chkStep4 = new System.Windows.Forms.CheckBoxTS();
			this.label14 = new System.Windows.Forms.LabelTS();
			this.label13 = new System.Windows.Forms.LabelTS();
			this.chkStep17 = new System.Windows.Forms.CheckBoxTS();
			this.label12 = new System.Windows.Forms.LabelTS();
			this.chkStep15 = new System.Windows.Forms.CheckBoxTS();
			this.label10 = new System.Windows.Forms.LabelTS();
			this.chkStep16 = new System.Windows.Forms.CheckBoxTS();
			this.chk10m = new System.Windows.Forms.CheckBoxTS();
			this.chk12m = new System.Windows.Forms.CheckBoxTS();
			this.chk15m = new System.Windows.Forms.CheckBoxTS();
			this.chk17m = new System.Windows.Forms.CheckBoxTS();
			this.chk20m = new System.Windows.Forms.CheckBoxTS();
			this.chk30m = new System.Windows.Forms.CheckBoxTS();
			this.chk40m = new System.Windows.Forms.CheckBoxTS();
			this.chk60m = new System.Windows.Forms.CheckBoxTS();
			this.chk80m = new System.Windows.Forms.CheckBoxTS();
			this.chk160m = new System.Windows.Forms.CheckBoxTS();
			this.label9 = new System.Windows.Forms.LabelTS();
			this.chkStep13 = new System.Windows.Forms.CheckBoxTS();
			this.label7 = new System.Windows.Forms.LabelTS();
			this.chkStep12 = new System.Windows.Forms.CheckBoxTS();
			this.label6 = new System.Windows.Forms.LabelTS();
			this.chkStep11 = new System.Windows.Forms.CheckBoxTS();
			this.label5 = new System.Windows.Forms.LabelTS();
			this.chkStep10 = new System.Windows.Forms.CheckBoxTS();
			this.label1 = new System.Windows.Forms.LabelTS();
			this.chkStep7 = new System.Windows.Forms.CheckBoxTS();
			this.label4 = new System.Windows.Forms.LabelTS();
			this.chkStep5 = new System.Windows.Forms.CheckBoxTS();
			this.chkStep3 = new System.Windows.Forms.CheckBoxTS();
			this.label2 = new System.Windows.Forms.LabelTS();
			this.chkStep2 = new System.Windows.Forms.CheckBoxTS();
			this.label18 = new System.Windows.Forms.LabelTS();
			this.chkStep9 = new System.Windows.Forms.CheckBoxTS();
			this.checkBox3 = new System.Windows.Forms.CheckBoxTS();
			this.chkBiasSet = new System.Windows.Forms.CheckBoxTS();
			this.chkFWDLow = new System.Windows.Forms.CheckBoxTS();
			this.grpTests = new System.Windows.Forms.GroupBoxTS();
			this.chkHarm60 = new System.Windows.Forms.CheckBoxTS();
			this.chkFWDHigh = new System.Windows.Forms.CheckBoxTS();
			this.chkIMDTest = new System.Windows.Forms.CheckBoxTS();
			this.chkLFTest = new System.Windows.Forms.CheckBoxTS();
			this.btnGainCal = new System.Windows.Forms.ButtonTS();
			this.btnBandSweep = new System.Windows.Forms.ButtonTS();
			this.chkSWRCal = new System.Windows.Forms.CheckBoxTS();
			this.chkHarm30 = new System.Windows.Forms.CheckBoxTS();
			this.btnHarmFil30 = new System.Windows.Forms.Button();
			this.btnHarmFil60 = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.ButtonTS();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.txtSerialNum = new System.Windows.Forms.TextBoxTS();
			this.btnClearAll = new System.Windows.Forms.ButtonTS();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.label20 = new System.Windows.Forms.LabelTS();
			this.timer_LF_test = new System.Windows.Forms.Timer(this.components);
			this.udCalTarget = new System.Windows.Forms.NumericUpDownTS();
			this.label23 = new System.Windows.Forms.LabelTS();
			this.grpStepByStep.SuspendLayout();
			this.grpTests.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udCalTarget)).BeginInit();
			this.SuspendLayout();
			// 
			// grpStepByStep
			// 
			this.grpStepByStep.Controls.Add(this.label3);
			this.grpStepByStep.Controls.Add(this.checkBox1);
			this.grpStepByStep.Controls.Add(this.btnCheckAll);
			this.grpStepByStep.Controls.Add(this.label22);
			this.grpStepByStep.Controls.Add(this.checkBox6);
			this.grpStepByStep.Controls.Add(this.label11);
			this.grpStepByStep.Controls.Add(this.checkBox5);
			this.grpStepByStep.Controls.Add(this.checkBox4);
			this.grpStepByStep.Controls.Add(this.label21);
			this.grpStepByStep.Controls.Add(this.btnClearCal);
			this.grpStepByStep.Controls.Add(this.label8);
			this.grpStepByStep.Controls.Add(this.checkBox2);
			this.grpStepByStep.Controls.Add(this.label19);
			this.grpStepByStep.Controls.Add(this.chkStep18);
			this.grpStepByStep.Controls.Add(this.label17);
			this.grpStepByStep.Controls.Add(this.chkStep8);
			this.grpStepByStep.Controls.Add(this.label16);
			this.grpStepByStep.Controls.Add(this.chkStep6);
			this.grpStepByStep.Controls.Add(this.label15);
			this.grpStepByStep.Controls.Add(this.chkStep4);
			this.grpStepByStep.Controls.Add(this.label14);
			this.grpStepByStep.Controls.Add(this.label13);
			this.grpStepByStep.Controls.Add(this.chkStep17);
			this.grpStepByStep.Controls.Add(this.label12);
			this.grpStepByStep.Controls.Add(this.chkStep15);
			this.grpStepByStep.Controls.Add(this.label10);
			this.grpStepByStep.Controls.Add(this.chkStep16);
			this.grpStepByStep.Controls.Add(this.chk10m);
			this.grpStepByStep.Controls.Add(this.chk12m);
			this.grpStepByStep.Controls.Add(this.chk15m);
			this.grpStepByStep.Controls.Add(this.chk17m);
			this.grpStepByStep.Controls.Add(this.chk20m);
			this.grpStepByStep.Controls.Add(this.chk30m);
			this.grpStepByStep.Controls.Add(this.chk40m);
			this.grpStepByStep.Controls.Add(this.chk60m);
			this.grpStepByStep.Controls.Add(this.chk80m);
			this.grpStepByStep.Controls.Add(this.chk160m);
			this.grpStepByStep.Controls.Add(this.label9);
			this.grpStepByStep.Controls.Add(this.chkStep13);
			this.grpStepByStep.Controls.Add(this.label7);
			this.grpStepByStep.Controls.Add(this.chkStep12);
			this.grpStepByStep.Controls.Add(this.label6);
			this.grpStepByStep.Controls.Add(this.chkStep11);
			this.grpStepByStep.Controls.Add(this.label5);
			this.grpStepByStep.Controls.Add(this.chkStep10);
			this.grpStepByStep.Controls.Add(this.label1);
			this.grpStepByStep.Controls.Add(this.chkStep7);
			this.grpStepByStep.Controls.Add(this.label4);
			this.grpStepByStep.Controls.Add(this.chkStep5);
			this.grpStepByStep.Controls.Add(this.chkStep3);
			this.grpStepByStep.Controls.Add(this.label2);
			this.grpStepByStep.Controls.Add(this.chkStep2);
			this.grpStepByStep.Controls.Add(this.label18);
			this.grpStepByStep.Controls.Add(this.chkStep9);
			this.grpStepByStep.Location = new System.Drawing.Point(16, 8);
			this.grpStepByStep.Name = "grpStepByStep";
			this.grpStepByStep.Size = new System.Drawing.Size(392, 584);
			this.grpStepByStep.TabIndex = 0;
			this.grpStepByStep.TabStop = false;
			this.grpStepByStep.Text = "Step by Step";
			// 
			// label3
			// 
			this.label3.Image = null;
			this.label3.Location = new System.Drawing.Point(40, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(344, 23);
			this.label3.TabIndex = 66;
			this.label3.Text = "7. Adjust Gain for roughly 50W.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBox1
			// 
			this.checkBox1.Image = null;
			this.checkBox1.Location = new System.Drawing.Point(16, 168);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(16, 24);
			this.checkBox1.TabIndex = 65;
			// 
			// btnCheckAll
			// 
			this.btnCheckAll.Image = null;
			this.btnCheckAll.Location = new System.Drawing.Point(304, 432);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new System.Drawing.Size(64, 23);
			this.btnCheckAll.TabIndex = 64;
			this.btnCheckAll.Text = "Check All";
			this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
			// 
			// label22
			// 
			this.label22.Image = null;
			this.label22.Location = new System.Drawing.Point(40, 360);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(344, 23);
			this.label22.TabIndex = 63;
			this.label22.Text = "15. Press HamFil30.  and check for <-45dBm 2nd.";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBox6
			// 
			this.checkBox6.Image = null;
			this.checkBox6.Location = new System.Drawing.Point(16, 360);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(16, 24);
			this.checkBox6.TabIndex = 62;
			// 
			// label11
			// 
			this.label11.Image = null;
			this.label11.Location = new System.Drawing.Point(40, 336);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(344, 23);
			this.label11.TabIndex = 61;
			this.label11.Text = "14. Press HamFil60.  Adjust VR6 counter clockwise <-45dBm 2nd.";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBox5
			// 
			this.checkBox5.Image = null;
			this.checkBox5.Location = new System.Drawing.Point(16, 336);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(16, 24);
			this.checkBox5.TabIndex = 60;
			// 
			// checkBox4
			// 
			this.checkBox4.Image = null;
			this.checkBox4.Location = new System.Drawing.Point(16, 24);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(16, 24);
			this.checkBox4.TabIndex = 59;
			// 
			// label21
			// 
			this.label21.Image = null;
			this.label21.Location = new System.Drawing.Point(40, 24);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(344, 23);
			this.label21.TabIndex = 58;
			this.label21.Text = "1. Compress 20m Inductors";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClearCal
			// 
			this.btnClearCal.Image = null;
			this.btnClearCal.Location = new System.Drawing.Point(304, 408);
			this.btnClearCal.Name = "btnClearCal";
			this.btnClearCal.Size = new System.Drawing.Size(64, 23);
			this.btnClearCal.TabIndex = 56;
			this.btnClearCal.Text = "Clear Cal";
			this.btnClearCal.Click += new System.EventHandler(this.btnClearCal_Click);
			// 
			// label8
			// 
			this.label8.Image = null;
			this.label8.Location = new System.Drawing.Point(40, 456);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(336, 23);
			this.label8.TabIndex = 55;
			this.label8.Text = "17. Press LF Test to check for LF instability on 160, 20 and 10m.";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBox2
			// 
			this.checkBox2.Image = null;
			this.checkBox2.Location = new System.Drawing.Point(16, 456);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(16, 24);
			this.checkBox2.TabIndex = 54;
			// 
			// label19
			// 
			this.label19.Image = null;
			this.label19.Location = new System.Drawing.Point(40, 552);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(336, 23);
			this.label19.TabIndex = 51;
			this.label19.Text = "21. Tighten screws.";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep18
			// 
			this.chkStep18.Image = null;
			this.chkStep18.Location = new System.Drawing.Point(16, 552);
			this.chkStep18.Name = "chkStep18";
			this.chkStep18.Size = new System.Drawing.Size(16, 24);
			this.chkStep18.TabIndex = 50;
			// 
			// label17
			// 
			this.label17.Image = null;
			this.label17.Location = new System.Drawing.Point(40, 240);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(344, 23);
			this.label17.TabIndex = 47;
			this.label17.Text = "10. Push FWD High and match the FWD pot to Wattmeter at 100W.";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep8
			// 
			this.chkStep8.Image = null;
			this.chkStep8.Location = new System.Drawing.Point(16, 240);
			this.chkStep8.Name = "chkStep8";
			this.chkStep8.Size = new System.Drawing.Size(16, 24);
			this.chkStep8.TabIndex = 46;
			// 
			// label16
			// 
			this.label16.Image = null;
			this.label16.Location = new System.Drawing.Point(40, 144);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(344, 23);
			this.label16.TabIndex = 45;
			this.label16.Text = "6. Switch DC to Run position.";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep6
			// 
			this.chkStep6.Image = null;
			this.chkStep6.Location = new System.Drawing.Point(16, 144);
			this.chkStep6.Name = "chkStep6";
			this.chkStep6.Size = new System.Drawing.Size(16, 24);
			this.chkStep6.TabIndex = 44;
			// 
			// label15
			// 
			this.label15.Image = null;
			this.label15.Location = new System.Drawing.Point(40, 96);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(344, 23);
			this.label15.TabIndex = 43;
			this.label15.Text = "4. Change DC switch to Bias position.";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep4
			// 
			this.chkStep4.Image = null;
			this.chkStep4.Location = new System.Drawing.Point(16, 96);
			this.chkStep4.Name = "chkStep4";
			this.chkStep4.Size = new System.Drawing.Size(16, 24);
			this.chkStep4.TabIndex = 42;
			// 
			// label14
			// 
			this.label14.Image = null;
			this.label14.Location = new System.Drawing.Point(40, 72);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(344, 23);
			this.label14.TabIndex = 41;
			this.label14.Text = "3. Preset All potentiometers fully counter clockwise except for FWD.";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.Image = null;
			this.label13.Location = new System.Drawing.Point(40, 528);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(336, 23);
			this.label13.TabIndex = 39;
			this.label13.Text = "20. Mark box as Quality Checked.";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep17
			// 
			this.chkStep17.Image = null;
			this.chkStep17.Location = new System.Drawing.Point(16, 528);
			this.chkStep17.Name = "chkStep17";
			this.chkStep17.Size = new System.Drawing.Size(16, 24);
			this.chkStep17.TabIndex = 38;
			// 
			// label12
			// 
			this.label12.Image = null;
			this.label12.Location = new System.Drawing.Point(40, 480);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(336, 23);
			this.label12.TabIndex = 37;
			this.label12.Text = "18. Press IMD Test button and measure IMD < -25dB.";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep15
			// 
			this.chkStep15.Image = null;
			this.chkStep15.Location = new System.Drawing.Point(16, 480);
			this.chkStep15.Name = "chkStep15";
			this.chkStep15.Size = new System.Drawing.Size(16, 24);
			this.chkStep15.TabIndex = 36;
			// 
			// label10
			// 
			this.label10.Image = null;
			this.label10.Location = new System.Drawing.Point(40, 504);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(336, 23);
			this.label10.TabIndex = 33;
			this.label10.Text = "19. Enter S/N, press Print button and place sheet in box.";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep16
			// 
			this.chkStep16.Image = null;
			this.chkStep16.Location = new System.Drawing.Point(16, 504);
			this.chkStep16.Name = "chkStep16";
			this.chkStep16.Size = new System.Drawing.Size(16, 24);
			this.chkStep16.TabIndex = 32;
			// 
			// chk10m
			// 
			this.chk10m.Image = null;
			this.chk10m.Location = new System.Drawing.Point(248, 432);
			this.chk10m.Name = "chk10m";
			this.chk10m.Size = new System.Drawing.Size(48, 24);
			this.chk10m.TabIndex = 31;
			this.chk10m.Text = "10m";
			// 
			// chk12m
			// 
			this.chk12m.Image = null;
			this.chk12m.Location = new System.Drawing.Point(200, 432);
			this.chk12m.Name = "chk12m";
			this.chk12m.Size = new System.Drawing.Size(48, 24);
			this.chk12m.TabIndex = 30;
			this.chk12m.Text = "12m";
			// 
			// chk15m
			// 
			this.chk15m.Image = null;
			this.chk15m.Location = new System.Drawing.Point(152, 432);
			this.chk15m.Name = "chk15m";
			this.chk15m.Size = new System.Drawing.Size(48, 24);
			this.chk15m.TabIndex = 29;
			this.chk15m.Text = "15m";
			// 
			// chk17m
			// 
			this.chk17m.Image = null;
			this.chk17m.Location = new System.Drawing.Point(104, 432);
			this.chk17m.Name = "chk17m";
			this.chk17m.Size = new System.Drawing.Size(48, 24);
			this.chk17m.TabIndex = 28;
			this.chk17m.Text = "17m";
			// 
			// chk20m
			// 
			this.chk20m.Image = null;
			this.chk20m.Location = new System.Drawing.Point(48, 432);
			this.chk20m.Name = "chk20m";
			this.chk20m.Size = new System.Drawing.Size(48, 24);
			this.chk20m.TabIndex = 27;
			this.chk20m.Text = "20m";
			// 
			// chk30m
			// 
			this.chk30m.Image = null;
			this.chk30m.Location = new System.Drawing.Point(248, 408);
			this.chk30m.Name = "chk30m";
			this.chk30m.Size = new System.Drawing.Size(48, 24);
			this.chk30m.TabIndex = 26;
			this.chk30m.Text = "30m";
			// 
			// chk40m
			// 
			this.chk40m.Image = null;
			this.chk40m.Location = new System.Drawing.Point(200, 408);
			this.chk40m.Name = "chk40m";
			this.chk40m.Size = new System.Drawing.Size(48, 24);
			this.chk40m.TabIndex = 25;
			this.chk40m.Text = "40m";
			// 
			// chk60m
			// 
			this.chk60m.Image = null;
			this.chk60m.Location = new System.Drawing.Point(152, 408);
			this.chk60m.Name = "chk60m";
			this.chk60m.Size = new System.Drawing.Size(48, 24);
			this.chk60m.TabIndex = 24;
			this.chk60m.Text = "60m";
			// 
			// chk80m
			// 
			this.chk80m.Image = null;
			this.chk80m.Location = new System.Drawing.Point(104, 408);
			this.chk80m.Name = "chk80m";
			this.chk80m.Size = new System.Drawing.Size(48, 24);
			this.chk80m.TabIndex = 23;
			this.chk80m.Text = "80m";
			// 
			// chk160m
			// 
			this.chk160m.Image = null;
			this.chk160m.Location = new System.Drawing.Point(48, 408);
			this.chk160m.Name = "chk160m";
			this.chk160m.Size = new System.Drawing.Size(56, 24);
			this.chk160m.TabIndex = 22;
			this.chk160m.Text = "160m";
			// 
			// label9
			// 
			this.label9.Image = null;
			this.label9.Location = new System.Drawing.Point(40, 384);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(344, 23);
			this.label9.TabIndex = 21;
			this.label9.Text = "16. Press Gain Cal button to calibrate unselected bands below.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep13
			// 
			this.chkStep13.Image = null;
			this.chkStep13.Location = new System.Drawing.Point(16, 384);
			this.chkStep13.Name = "chkStep13";
			this.chkStep13.Size = new System.Drawing.Size(16, 24);
			this.chkStep13.TabIndex = 20;
			// 
			// label7
			// 
			this.label7.Image = null;
			this.label7.Location = new System.Drawing.Point(40, 312);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(344, 23);
			this.label7.TabIndex = 17;
			this.label7.Text = "13. Switch to single dummy load.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep12
			// 
			this.chkStep12.Image = null;
			this.chkStep12.Location = new System.Drawing.Point(16, 312);
			this.chkStep12.Name = "chkStep12";
			this.chkStep12.Size = new System.Drawing.Size(16, 24);
			this.chkStep12.TabIndex = 16;
			// 
			// label6
			// 
			this.label6.Image = null;
			this.label6.Location = new System.Drawing.Point(40, 288);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(344, 23);
			this.label6.TabIndex = 15;
			this.label6.Text = "12. Press SWR Cal button and adjust REV pot to 2:1 SWR.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep11
			// 
			this.chkStep11.Image = null;
			this.chkStep11.Location = new System.Drawing.Point(16, 288);
			this.chkStep11.Name = "chkStep11";
			this.chkStep11.Size = new System.Drawing.Size(16, 24);
			this.chkStep11.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.Image = null;
			this.label5.Location = new System.Drawing.Point(40, 264);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(344, 23);
			this.label5.TabIndex = 13;
			this.label5.Text = "11. Switch to parallel dummy load.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep10
			// 
			this.chkStep10.Image = null;
			this.chkStep10.Location = new System.Drawing.Point(16, 264);
			this.chkStep10.Name = "chkStep10";
			this.chkStep10.Size = new System.Drawing.Size(16, 24);
			this.chkStep10.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.Image = null;
			this.label1.Location = new System.Drawing.Point(40, 216);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(344, 23);
			this.label1.TabIndex = 11;
			this.label1.Text = "9. Push FWD Low button and match the FWD pot to Wattmeter.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep7
			// 
			this.chkStep7.Image = null;
			this.chkStep7.Location = new System.Drawing.Point(16, 216);
			this.chkStep7.Name = "chkStep7";
			this.chkStep7.Size = new System.Drawing.Size(16, 24);
			this.chkStep7.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.Image = null;
			this.label4.Location = new System.Drawing.Point(40, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(344, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "5. Push Bias Set button and set the Bias.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep5
			// 
			this.chkStep5.Image = null;
			this.chkStep5.Location = new System.Drawing.Point(16, 120);
			this.chkStep5.Name = "chkStep5";
			this.chkStep5.Size = new System.Drawing.Size(16, 24);
			this.chkStep5.TabIndex = 8;
			// 
			// chkStep3
			// 
			this.chkStep3.Image = null;
			this.chkStep3.Location = new System.Drawing.Point(16, 72);
			this.chkStep3.Name = "chkStep3";
			this.chkStep3.Size = new System.Drawing.Size(16, 24);
			this.chkStep3.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Image = null;
			this.label2.Location = new System.Drawing.Point(40, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(344, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "2. Verify that R1 and R2 are spaced properly.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep2
			// 
			this.chkStep2.Image = null;
			this.chkStep2.Location = new System.Drawing.Point(16, 48);
			this.chkStep2.Name = "chkStep2";
			this.chkStep2.Size = new System.Drawing.Size(16, 24);
			this.chkStep2.TabIndex = 4;
			// 
			// label18
			// 
			this.label18.Image = null;
			this.label18.Location = new System.Drawing.Point(40, 192);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(344, 23);
			this.label18.TabIndex = 49;
			this.label18.Text = "8. Press FWD Low and Null REV diode on scope.";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkStep9
			// 
			this.chkStep9.Image = null;
			this.chkStep9.Location = new System.Drawing.Point(16, 192);
			this.chkStep9.Name = "chkStep9";
			this.chkStep9.Size = new System.Drawing.Size(16, 24);
			this.chkStep9.TabIndex = 48;
			// 
			// checkBox3
			// 
			this.checkBox3.Image = null;
			this.checkBox3.Location = new System.Drawing.Point(0, 0);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.TabIndex = 0;
			// 
			// chkBiasSet
			// 
			this.chkBiasSet.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkBiasSet.Image = null;
			this.chkBiasSet.Location = new System.Drawing.Point(16, 24);
			this.chkBiasSet.Name = "chkBiasSet";
			this.chkBiasSet.Size = new System.Drawing.Size(80, 24);
			this.chkBiasSet.TabIndex = 1;
			this.chkBiasSet.Text = "Bias Set";
			this.chkBiasSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkBiasSet.CheckedChanged += new System.EventHandler(this.chkBiasSet_CheckedChanged);
			// 
			// chkFWDLow
			// 
			this.chkFWDLow.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkFWDLow.Image = null;
			this.chkFWDLow.Location = new System.Drawing.Point(16, 184);
			this.chkFWDLow.Name = "chkFWDLow";
			this.chkFWDLow.Size = new System.Drawing.Size(80, 24);
			this.chkFWDLow.TabIndex = 12;
			this.chkFWDLow.Text = "FWD Low";
			this.chkFWDLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkFWDLow.CheckedChanged += new System.EventHandler(this.chkFWDLow_CheckedChanged);
			// 
			// grpTests
			// 
			this.grpTests.Controls.Add(this.chkHarm60);
			this.grpTests.Controls.Add(this.chkFWDHigh);
			this.grpTests.Controls.Add(this.chkIMDTest);
			this.grpTests.Controls.Add(this.chkLFTest);
			this.grpTests.Controls.Add(this.btnGainCal);
			this.grpTests.Controls.Add(this.btnBandSweep);
			this.grpTests.Controls.Add(this.chkSWRCal);
			this.grpTests.Controls.Add(this.chkBiasSet);
			this.grpTests.Controls.Add(this.chkFWDLow);
			this.grpTests.Controls.Add(this.chkHarm30);
			this.grpTests.Controls.Add(this.btnHarmFil30);
			this.grpTests.Controls.Add(this.btnHarmFil60);
			this.grpTests.Location = new System.Drawing.Point(416, 8);
			this.grpTests.Name = "grpTests";
			this.grpTests.Size = new System.Drawing.Size(112, 416);
			this.grpTests.TabIndex = 1;
			this.grpTests.TabStop = false;
			this.grpTests.Text = "Tests";
			// 
			// chkHarm60
			// 
			this.chkHarm60.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkHarm60.Image = null;
			this.chkHarm60.Location = new System.Drawing.Point(16, 88);
			this.chkHarm60.Name = "chkHarm60";
			this.chkHarm60.Size = new System.Drawing.Size(80, 24);
			this.chkHarm60.TabIndex = 21;
			this.chkHarm60.Text = "Harm 60";
			this.chkHarm60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkHarm60.CheckedChanged += new System.EventHandler(this.chkHarm60_CheckedChanged);
			// 
			// chkFWDHigh
			// 
			this.chkFWDHigh.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkFWDHigh.Image = null;
			this.chkFWDHigh.Location = new System.Drawing.Point(16, 216);
			this.chkFWDHigh.Name = "chkFWDHigh";
			this.chkFWDHigh.Size = new System.Drawing.Size(80, 24);
			this.chkFWDHigh.TabIndex = 19;
			this.chkFWDHigh.Text = "FWD High";
			this.chkFWDHigh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkFWDHigh.CheckedChanged += new System.EventHandler(this.chkFWDHigh_CheckedChanged);
			// 
			// chkIMDTest
			// 
			this.chkIMDTest.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkIMDTest.Image = null;
			this.chkIMDTest.Location = new System.Drawing.Point(16, 376);
			this.chkIMDTest.Name = "chkIMDTest";
			this.chkIMDTest.Size = new System.Drawing.Size(80, 24);
			this.chkIMDTest.TabIndex = 18;
			this.chkIMDTest.Text = "IMD Test";
			this.chkIMDTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkIMDTest.CheckedChanged += new System.EventHandler(this.chkIMDTest_CheckedChanged);
			// 
			// chkLFTest
			// 
			this.chkLFTest.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLFTest.Image = null;
			this.chkLFTest.Location = new System.Drawing.Point(16, 344);
			this.chkLFTest.Name = "chkLFTest";
			this.chkLFTest.Size = new System.Drawing.Size(80, 24);
			this.chkLFTest.TabIndex = 17;
			this.chkLFTest.Text = "LF Test";
			this.chkLFTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkLFTest.CheckedChanged += new System.EventHandler(this.chkLFTest_CheckedChanged);
			// 
			// btnGainCal
			// 
			this.btnGainCal.Image = null;
			this.btnGainCal.Location = new System.Drawing.Point(16, 312);
			this.btnGainCal.Name = "btnGainCal";
			this.btnGainCal.Size = new System.Drawing.Size(80, 23);
			this.btnGainCal.TabIndex = 16;
			this.btnGainCal.Text = "Gain Cal";
			this.btnGainCal.Click += new System.EventHandler(this.btnGainCal_Click);
			// 
			// btnBandSweep
			// 
			this.btnBandSweep.Image = null;
			this.btnBandSweep.Location = new System.Drawing.Point(16, 280);
			this.btnBandSweep.Name = "btnBandSweep";
			this.btnBandSweep.Size = new System.Drawing.Size(80, 23);
			this.btnBandSweep.TabIndex = 15;
			this.btnBandSweep.Text = "Band Sweep";
			this.btnBandSweep.Click += new System.EventHandler(this.btnBandSweep_Click);
			// 
			// chkSWRCal
			// 
			this.chkSWRCal.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkSWRCal.Image = null;
			this.chkSWRCal.Location = new System.Drawing.Point(16, 248);
			this.chkSWRCal.Name = "chkSWRCal";
			this.chkSWRCal.Size = new System.Drawing.Size(80, 24);
			this.chkSWRCal.TabIndex = 13;
			this.chkSWRCal.Text = "SWR Cal";
			this.chkSWRCal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkSWRCal.CheckedChanged += new System.EventHandler(this.chkSWRCal_CheckedChanged);
			// 
			// chkHarm30
			// 
			this.chkHarm30.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkHarm30.Image = null;
			this.chkHarm30.Location = new System.Drawing.Point(16, 56);
			this.chkHarm30.Name = "chkHarm30";
			this.chkHarm30.Size = new System.Drawing.Size(80, 24);
			this.chkHarm30.TabIndex = 20;
			this.chkHarm30.Text = "Harm 30";
			this.chkHarm30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkHarm30.CheckedChanged += new System.EventHandler(this.chkHarm30_CheckedChanged);
			// 
			// btnHarmFil30
			// 
			this.btnHarmFil30.Location = new System.Drawing.Point(16, 120);
			this.btnHarmFil30.Name = "btnHarmFil30";
			this.btnHarmFil30.Size = new System.Drawing.Size(80, 23);
			this.btnHarmFil30.TabIndex = 67;
			this.btnHarmFil30.Text = "Harm Fil 30";
			this.btnHarmFil30.Click += new System.EventHandler(this.btnHarmFil30_Click);
			// 
			// btnHarmFil60
			// 
			this.btnHarmFil60.Location = new System.Drawing.Point(16, 152);
			this.btnHarmFil60.Name = "btnHarmFil60";
			this.btnHarmFil60.Size = new System.Drawing.Size(80, 23);
			this.btnHarmFil60.TabIndex = 68;
			this.btnHarmFil60.Text = "Harm Fil 60";
			this.btnHarmFil60.Click += new System.EventHandler(this.btnHarmFil60_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Image = null;
			this.btnPrint.Location = new System.Drawing.Point(432, 528);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.TabIndex = 2;
			this.btnPrint.Text = "Print Cal";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// txtSerialNum
			// 
			this.txtSerialNum.Location = new System.Drawing.Point(424, 496);
			this.txtSerialNum.Name = "txtSerialNum";
			this.txtSerialNum.Size = new System.Drawing.Size(88, 20);
			this.txtSerialNum.TabIndex = 3;
			this.txtSerialNum.Text = "";
			// 
			// btnClearAll
			// 
			this.btnClearAll.Image = null;
			this.btnClearAll.Location = new System.Drawing.Point(432, 560);
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.TabIndex = 4;
			this.btnClearAll.Text = "Clear All";
			this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
			// 
			// printPreviewDialog1
			// 
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
			this.printPreviewDialog1.Location = new System.Drawing.Point(256, 21);
			this.printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty;
			this.printPreviewDialog1.Visible = false;
			// 
			// label20
			// 
			this.label20.Image = null;
			this.label20.Location = new System.Drawing.Point(424, 480);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(88, 16);
			this.label20.TabIndex = 5;
			this.label20.Text = "Serial Number";
			// 
			// timer_LF_test
			// 
			this.timer_LF_test.Tick += new System.EventHandler(this.timer_LF_test_Tick);
			// 
			// udCalTarget
			// 
			this.udCalTarget.Increment = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  0});
			this.udCalTarget.Location = new System.Drawing.Point(440, 448);
			this.udCalTarget.Maximum = new System.Decimal(new int[] {
																		100,
																		0,
																		0,
																		0});
			this.udCalTarget.Minimum = new System.Decimal(new int[] {
																		10,
																		0,
																		0,
																		0});
			this.udCalTarget.Name = "udCalTarget";
			this.udCalTarget.Size = new System.Drawing.Size(56, 20);
			this.udCalTarget.TabIndex = 6;
			this.udCalTarget.Value = new System.Decimal(new int[] {
																	  75,
																	  0,
																	  0,
																	  0});
			// 
			// label23
			// 
			this.label23.Image = null;
			this.label23.Location = new System.Drawing.Point(440, 432);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(64, 16);
			this.label23.TabIndex = 7;
			this.label23.Text = "Cal Target:";
			// 
			// PAQualify
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(544, 598);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.udCalTarget);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.btnClearAll);
			this.Controls.Add(this.txtSerialNum);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.grpTests);
			this.Controls.Add(this.grpStepByStep);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PAQualify";
			this.Text = "SDR-100WPA Qualification";
			this.grpStepByStep.ResumeLayout(false);
			this.grpTests.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udCalTarget)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Misc Routines

		private void DisableTests(CheckBoxTS chk)
		{
			foreach(Control c in grpTests.Controls)
			{
				if(c != chk)
					c.Enabled = false;
			}
		}

		private void EnableTests()
		{
			foreach(Control c in grpTests.Controls)
				c.Enabled = true;
		}

		#endregion

		#region Test Event Handlers

		private void chkBiasSet_CheckedChanged(object sender, System.EventArgs e)
		{
			console.Hdw.PABias = chkBiasSet.Checked;
			if(chkBiasSet.Checked)
			{
				chkBiasSet.BackColor = console.ButtonSelectedColor;
				DisableTests(chkBiasSet);
				console.Hdw.X2 |= 0x20;
			}
			else
			{
				EnableTests();
				chkBiasSet.BackColor = SystemColors.Control;
				console.Hdw.X2 &= 0xDF;
			}
		}

		private void chkHarm30_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkHarm30.Checked)
			{
				chkHarm30.BackColor = console.ButtonSelectedColor;
				DisableTests(chkHarm30);
				console.PowerOn = true;
				console.RX1DSPMode = DSPMode.USB;
				console.VFOAFreq = 10.1f;
				console.TUN = true;
				console.PreviousPWR = console.PWR;
				console.PWR = 50;
				//console.Hdw.PA_LPF = PAFBand.B1210;	// set LPF to 12_10
				console.CurrentMeterTXMode = MeterTXMode.SWR;		// set multimeter to SWR
			}
			else
			{
				console.PWR = console.PreviousPWR;
				console.TUN = false;
				EnableTests();
				chkHarm30.BackColor = SystemColors.Control;
			}
		}

		private void chkHarm60_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkHarm60.Checked)
			{
				chkHarm60.BackColor = console.ButtonSelectedColor;
				DisableTests(chkHarm60);
				console.PowerOn = true;
				console.RX1DSPMode = DSPMode.USB;
				console.VFOAFreq = 5.3305f;
				console.TUN = true;
				console.PreviousPWR = console.PWR;
				console.PWR = 50;
				//console.Hdw.PA_LPF = PAFBand.B1210;	// set LPF to 12_10
				console.CurrentMeterTXMode = MeterTXMode.SWR;		// set multimeter to SWR
			}
			else
			{
				console.PWR = console.PreviousPWR;
				console.TUN = false;
				EnableTests();
				chkHarm60.BackColor = SystemColors.Control;
			}
		}

		private void btnHarmFil30_Click(object sender, System.EventArgs e)
		{
			btnCheckAll.PerformClick();
			chk30m.Checked = false;

			console.PowerOn = true;
			console.CurrentMeterTXMode = MeterTXMode.FORWARD_POWER;
			progress = new Progress("Calibrate PA Gain");

			Thread t = new Thread(new ThreadStart(CalibratePAGain));
			t.Name = "PA Gain Calibration Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.AboveNormal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnHarmFil60_Click(object sender, System.EventArgs e)
		{
			btnCheckAll.PerformClick();
			chk60m.Checked = false;

			console.PowerOn = true;
			console.CurrentMeterTXMode = MeterTXMode.FORWARD_POWER;
			progress = new Progress("Calibrate PA Gain");

			Thread t = new Thread(new ThreadStart(CalibratePAGain));
			t.Name = "PA Gain Calibration Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.AboveNormal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void chkFWDLow_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkFWDLow.Checked)
			{
				chkFWDLow.BackColor = console.ButtonSelectedColor;
				DisableTests(chkFWDLow);
				console.PowerOn = true;
				console.PreviousPWR = console.PWR;
				console.RX1DSPMode = DSPMode.USB;
				console.PWR = 50;
				console.VFOAFreq = 14.2f;
				Audio.TXInputSignal = Audio.SignalSource.SINE;
				Audio.SourceScale = 1.0;
				console.MOX = true;
				Audio.MOX = true;
				console.CurrentMeterTXMode = MeterTXMode.FORWARD_POWER;				
			}
			else
			{
				Audio.MOX = false;
				console.MOX = false;
				Audio.TXInputSignal = Audio.SignalSource.RADIO;
				console.PWR = console.PreviousPWR;
				EnableTests();
				chkFWDLow.BackColor = SystemColors.Control;
			}
		}

		private void chkFWDHigh_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkFWDHigh.Checked)
			{
				chkFWDHigh.BackColor = console.ButtonSelectedColor;
				DisableTests(chkFWDHigh);
				console.PowerOn = true;
				console.PreviousPWR = console.PWR;
				console.PWR = 100;
				console.VFOAFreq = 14.2f;
				Audio.TXInputSignal = Audio.SignalSource.SINE;
				Audio.SourceScale = 1.0;
				console.MOX = true;
				Audio.MOX = true;
				console.CurrentMeterTXMode = MeterTXMode.FORWARD_POWER;
			}
			else
			{
				Audio.MOX = false;
				console.MOX = false;
				Audio.TXInputSignal = Audio.SignalSource.RADIO;
				console.PWR = console.PreviousPWR;
				EnableTests();
				chkFWDHigh.BackColor = SystemColors.Control;
			}
		}

		private void chkSWRCal_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkSWRCal.Checked)
			{
				chkSWRCal.BackColor = console.ButtonSelectedColor;
				DisableTests(chkSWRCal);
				console.PowerOn = true;
				console.PreviousPWR = console.PWR;
				console.VFOAFreq = 14.2;
				console.DisableSWRProtection = true;
				console.TUN = true;
				console.PWR = 50;
				console.CurrentMeterTXMode = MeterTXMode.SWR;
				console.Hdw.X2 |= 0x08;
			}
			else
			{
				console.TUN = false;
				console.DisableSWRProtection = false;
				EnableTests();
				chkSWRCal.BackColor = SystemColors.Control;
				console.Hdw.X2 &= 0xF7;
			}
		}

		private void btnBandSweep_Click(object sender, System.EventArgs e)
		{
			console.PowerOn = true;
			progress = new Progress("Low Power PA Sweep");

			Thread t = new Thread(new ThreadStart(LowPowerPASweep));
			t.Name = "PA Low Power PA Sweep Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.AboveNormal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void LowPowerPASweep()
		{
			bool done = console.LowPowerPASweep(progress, 10);
			if(done) MessageBox.Show("Low Power PA Sweep complete.");
		}

		private void btnGainCal_Click(object sender, System.EventArgs e)
		{
			console.PowerOn = true;
			console.CurrentMeterTXMode = MeterTXMode.FORWARD_POWER;
			progress = new Progress("Calibrate PA Gain");

			Thread t = new Thread(new ThreadStart(CalibratePAGain));
			t.Name = "PA Gain Calibration Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.AboveNormal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void CalibratePAGain()
		{
			bool[] run = new bool[10];
			run[0] = !chk160m.Checked;
			run[1] = !chk80m.Checked;
			run[2] = !chk60m.Checked;
			run[3] = !chk40m.Checked;
			run[4] = !chk30m.Checked;
			run[5] = !chk20m.Checked;
			run[6] = !chk17m.Checked;
			run[7] = !chk15m.Checked;
			run[8] = !chk12m.Checked;
			run[9] = !chk10m.Checked;

			bool done = console.CalibratePAGain(progress, run, (int)udCalTarget.Value);
			if(done) MessageBox.Show("PA Gain Calibration complete.");
		}

		private void chkLFTest_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkLFTest.Checked)
			{
				chkLFTest.BackColor = console.ButtonSelectedColor;
				DisableTests(chkLFTest);
				console.PowerOn = true;
				console.PreviousPWR = console.PWR;
				console.PWR = 1;
				console.VFOAFreq = 1.9f;
				console.MOX = true;
				Audio.MOX = true;
				Audio.TXInputSignal = Audio.SignalSource.SINE;
				Audio.SourceScale = 1.0;
				timer_LF_test.Enabled = true;
			}
			else
			{
				Audio.TXInputSignal = Audio.SignalSource.RADIO;
				Audio.MOX = false;
				console.MOX = false;
				timer_LF_test.Enabled = false;
				console.PWR = console.PreviousPWR;
				EnableTests();
				chkLFTest.BackColor = SystemColors.Control;
			}
		}

		private void chkIMDTest_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkIMDTest.Checked)
			{
				chkIMDTest.BackColor = console.ButtonSelectedColor;
				DisableTests(chkIMDTest);
				console.PowerOn = true;
				console.PreviousPWR = console.PWR;
				console.PWR = 50;
				console.VFOAFreq = 14.2f;
				Audio.SineFreq1 = 700.0;
				Audio.SineFreq2 = 1900.0;
				Audio.two_tone = true;
				console.MOX = true;
				Audio.MOX = true;
				Audio.TXInputSignal = Audio.SignalSource.SINE;
				Audio.SourceScale = 1.0;
				console.CurrentMeterTXMode = MeterTXMode.FORWARD_POWER;
			}
			else
			{
				Audio.TXInputSignal = Audio.SignalSource.RADIO;
				Audio.MOX = false;
				console.MOX = false;
				console.PWR = console.PreviousPWR;
				Audio.SineFreq1 = (double)console.CWPitch;
				Audio.two_tone = false;
				EnableTests();
				chkIMDTest.BackColor = SystemColors.Control;
			}
		}		

		#endregion		

		#region Other Event Handlers

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			printPreviewDialog1.Document = printDocument1;
			printPreviewDialog1.ShowDialog();
//			printDialog1.Document = printDocument1;
//			if (printDialog1.ShowDialog() == DialogResult.OK)
//			{
//				printDocument1.Print();
//			}
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			int V = 80;
			string text = "Date: "+DateTime.Today.ToShortDateString()+"  Time: "+
				DateTime.Now.ToString("HH:mm:ss")+"\n\n";
			text += "FlexRadio SDR-100WPA Gain Test Results\n";
			text += "Serial Number: "+txtSerialNum.Text+"\n";

			text += "\nBand  |  Value\n"+
				"160m  |  "+console.SetupForm.PAGain160.ToString("f1")+"\n"+
				"80m   |  "+console.SetupForm.PAGain80.ToString("f1")+"\n"+
				"60m   |  "+console.SetupForm.PAGain60.ToString("f1")+"\n"+
				"40m   |  "+console.SetupForm.PAGain40.ToString("f1")+"\n"+
				"30m   |  "+console.SetupForm.PAGain30.ToString("f1")+"\n"+
				"20m   |  "+console.SetupForm.PAGain20.ToString("f1")+"\n"+
				"17m   |  "+console.SetupForm.PAGain17.ToString("f1")+"\n"+
				"15m   |  "+console.SetupForm.PAGain15.ToString("f1")+"\n"+
				"12m   |  "+console.SetupForm.PAGain12.ToString("f1")+"\n"+
				"10m   |  "+console.SetupForm.PAGain10.ToString("f1")+"\n";
			text += "\nEnter these values when prompted by the PowerSDR Setup wizard or\n"+
				"(preferably) if a 100W dummy load is present, run the PA Cal routine\n"+
				"to obtain accurate values for your environment at your location.";
			e.Graphics.DrawString(text, 
				new Font("Lucida Console", 11), Brushes.Black, 80, V);
			V += 350;

			text = "Documentation Information";
			e.Graphics.DrawString(text,
				new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, 80, V);
			V += 30;

			text = "All manuals and software can be downloaded from our website at http://www.flex-radio.com.\n"+
				"Sign up for an account by clicking on \"Log In\" to access the Users Only folder which\n"+
				"contains the schematics and other Hardware docmentation.";

			e.Graphics.DrawString(text,
				new Font("Times New Roman", 12), Brushes.Black, 80, V);
			V += 90;

			text = "There are several important documents on the Knowledge Base (KB):\n" +
				"\t1. SDR-1000 Operating Manual\n" +
				"\t2. Soundcard Quick Start Guide\n" +
				"\t3. PowerSDR Quick Start Guide\n" +
				"\nIf you have any problems downloading, please send an email to support@flex-radio.com.\n" +
				"You may also call (512) 250-8595 if you need telephone assistance.";
			e.Graphics.DrawString(text,
				new Font("Times New Roman", 12), Brushes.Black, 80, V);
			V += 200;

			text = "Warning:";
			e.Graphics.DrawString(text,
				new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, 80, V);
			V += 30;

			text = "Proper operation of the SDR-1000 depends on the use of a sound card that is officially\n"+
				"supported by FlexRadio Systems.  Refer to the Specifications page on www.flex-radio.com\n"+
				"to determine which sound cards are currently supported.  Use only the specific model numbers\n"+
				"stated on the website because other models within the same family may not work properly with\n"+
				"the radio.  Officially supported sound cards may be updated on the website without notice.\n"+

				"\nNO WARRANTY IS IMPLIED WHEN THE SDR-1000 IS USED WITH ANY SOUND CARD\n"+
				"OTHER THAN THOSE CURRENTLY SUPPORTED AS STATED ON THE FLEXRADIO SYSTEMS\n"+
				"WEBSITE.  UNSUPPORTED SOUND CARDS MAY OR MAY NOT WORK WITH THE SDR-1000.\n"+
				"USE OF UNSUPPORTED SOUND CARDS IS AT THE CUSTOMERS OWN RISK.";
			e.Graphics.DrawString(text,
				new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, 80, V);			
		}

		private void btnClearCal_Click(object sender, System.EventArgs e)
		{
			chk160m.Checked = false;
			chk80m.Checked = false;
			chk60m.Checked = false;
			chk40m.Checked = false;
			chk30m.Checked = false;
			chk20m.Checked = false;
			chk17m.Checked = false;
			chk15m.Checked = false;
			chk12m.Checked = false;
			chk10m.Checked = false;
		}

		private void btnCheckAll_Click(object sender, System.EventArgs e)
		{
			chk160m.Checked = true;
			chk80m.Checked = true;
			chk60m.Checked = true;
			chk40m.Checked = true;
			chk30m.Checked = true;
			chk20m.Checked = true;
			chk17m.Checked = true;
			chk15m.Checked = true;
			chk12m.Checked = true;
			chk10m.Checked = true;
		}

		private void btnClearAll_Click(object sender, System.EventArgs e)
		{
			foreach(Control c in this.Controls)
			{
				if(c.GetType() == typeof(GroupBoxTS))
				{
					foreach(Control c2 in c.Controls)
					{
						if(c2.GetType() == typeof(CheckBoxTS))
						{
							CheckBoxTS chk = (CheckBoxTS)c2;
							chk.Checked = false;
						}
					}
				}
				else if(c.GetType() == typeof(CheckBoxTS))
				{
					CheckBoxTS chk = (CheckBoxTS)c;
					chk.Checked = false;
				}
			}
			txtSerialNum.Text = "Serial Num";
		}

		private void timer_LF_test_Tick(object sender, System.EventArgs e)
		{
			console.PWR++;

			if(console.PWR == 100)
			{
				console.PWR = 1;
				if(console.VFOAFreq == 1.9)
					console.VFOAFreq = 14.2;
				else if(console.VFOAFreq == 14.2)
					console.VFOAFreq = 28.8;
				else if(console.VFOAFreq == 28.8)
					chkLFTest.Checked = false;
			}
		}

		#endregion				
	}
}

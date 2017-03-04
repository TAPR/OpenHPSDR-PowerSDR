//=================================================================
// production_test.cs
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
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
	/// <summary>
	/// Summary description for production_test.
	/// </summary>
	public class ProductionTest : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private ProductionDebug debug_form;
		private Console console;
		private Progress progress;
		private System.Windows.Forms.TextBoxTS txtRefSig160;
		private System.Windows.Forms.TextBoxTS txtSignal160H;
		private System.Windows.Forms.LabelTS lblBand160;
		private System.Windows.Forms.LabelTS lblBand80;
		private System.Windows.Forms.TextBoxTS txtSignal80H;
		private System.Windows.Forms.TextBoxTS txtRefSig80;
		private System.Windows.Forms.LabelTS lblBand60;
		private System.Windows.Forms.TextBoxTS txtSignal60H;
		private System.Windows.Forms.TextBoxTS txtRefSig60;
		private System.Windows.Forms.LabelTS lblBand40;
		private System.Windows.Forms.TextBoxTS txtSignal40H;
		private System.Windows.Forms.TextBoxTS txtRefSig40;
		private System.Windows.Forms.LabelTS lblBand30;
		private System.Windows.Forms.TextBoxTS txtSignal30H;
		private System.Windows.Forms.TextBoxTS txtRefSig30;
		private System.Windows.Forms.LabelTS lblBand20;
		private System.Windows.Forms.TextBoxTS txtNoise20;
		private System.Windows.Forms.TextBoxTS txtSignal20H;
		private System.Windows.Forms.TextBoxTS txtRefNoise20;
		private System.Windows.Forms.TextBoxTS txtRefSig20;
		private System.Windows.Forms.LabelTS lblBand17;
		private System.Windows.Forms.TextBoxTS txtSignal17H;
		private System.Windows.Forms.TextBoxTS txtRefSig17;
		private System.Windows.Forms.LabelTS lblBand15;
		private System.Windows.Forms.TextBoxTS txtSignal15H;
		private System.Windows.Forms.TextBoxTS txtRefSig15;
		private System.Windows.Forms.LabelTS lblBand12;
		private System.Windows.Forms.TextBoxTS txtSignal12H;
		private System.Windows.Forms.TextBoxTS txtRefSig12;
		private System.Windows.Forms.LabelTS lblBand10;
		private System.Windows.Forms.TextBoxTS txtSignal10H;
		private System.Windows.Forms.TextBoxTS txtRefSig10;
		private System.Windows.Forms.LabelTS lblBand6;
		private System.Windows.Forms.TextBoxTS txtSignal6H;
		private System.Windows.Forms.TextBoxTS txtRefSig6;
		private System.Windows.Forms.GroupBoxTS grpSignal;
		private System.Windows.Forms.GroupBoxTS grpNoise;
		private System.Windows.Forms.LabelTS lblBand;
		private System.Windows.Forms.GroupBoxTS grpPreamp;
		private System.Windows.Forms.ButtonTS btnRunPreamp;
		private System.Windows.Forms.ButtonTS btnRunNoise;
		private System.Windows.Forms.ButtonTS btnRunSignal;
		private System.Windows.Forms.LabelTS lblSkipCheckedBands;
		private System.Windows.Forms.CheckBoxTS chk6;
		private System.Windows.Forms.CheckBoxTS chk10;
		private System.Windows.Forms.CheckBoxTS chk12;
		private System.Windows.Forms.CheckBoxTS chk15;
		private System.Windows.Forms.CheckBoxTS chk17;
		private System.Windows.Forms.CheckBoxTS chk20;
		private System.Windows.Forms.CheckBoxTS chk30;
		private System.Windows.Forms.CheckBoxTS chk40;
		private System.Windows.Forms.CheckBoxTS chk60;
		private System.Windows.Forms.CheckBoxTS chk80;
		private System.Windows.Forms.CheckBoxTS chk160;
		private System.Windows.Forms.ButtonTS btnRunAllTests;
		private System.Windows.Forms.LabelTS lblPreNoise;
		private System.Windows.Forms.LabelTS lblPreSignal;
		private System.Windows.Forms.ButtonTS btnCheckAll;
		private System.Windows.Forms.GroupBoxTS grpGenerator;
		private System.Windows.Forms.LabelTS lblGenFreq;
		private System.Windows.Forms.NumericUpDownTS udGenFreq;
		private System.Windows.Forms.LabelTS lblGenLevel;
		private System.Windows.Forms.NumericUpDownTS udGenLevel;
		private System.Windows.Forms.ButtonTS btnGenReset;
		private System.Windows.Forms.LabelTS label1;
		private System.Windows.Forms.NumericUpDownTS udGenClockCorr;
		private System.Windows.Forms.ButtonTS btnClearAll;
		private System.Windows.Forms.TextBoxTS txtPreAtt;
		private System.Windows.Forms.TextBoxTS txtPreGain;
		private System.Windows.Forms.LabelTS lblNoise;
		private System.Windows.Forms.LabelTS lblSigRef;
		private System.Windows.Forms.LabelTS lblNoiseRef;
		private System.Windows.Forms.GroupBoxTS grpTolerance;
		private System.Windows.Forms.TextBoxTS txtTolNoise;
		private System.Windows.Forms.LabelTS lblTolNoise;
		private System.Windows.Forms.LabelTS lblTolPreamp;
		private System.Windows.Forms.TextBoxTS txtTolPreamp;
		private System.Windows.Forms.ButtonTS btnRunImpulse;
		private System.Windows.Forms.GroupBoxTS grpTests;
		private System.Windows.Forms.CheckBox chkRunDot;
		private System.Windows.Forms.CheckBox chkRunDash;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mnuDebug;
		private System.Windows.Forms.ButtonTS btnRunRFE;
		private System.Windows.Forms.ButtonTS btnRunBalance;
		private System.Windows.Forms.LabelTS lblTolImpulse;
		private System.Windows.Forms.TextBoxTS txtTolImpulse;
		private System.Windows.Forms.LabelTS lblTolBalance;
		private System.Windows.Forms.TextBoxTS txtTolBalance;
		private System.Windows.Forms.ButtonTS btnRunTX;
		private System.Windows.Forms.LabelTS lblSignalActual;
		private System.Windows.Forms.GroupBoxTS groupBoxTS1;
		private System.Windows.Forms.LabelTS labelTS2;
		private System.Windows.Forms.LabelTS lblTXTestOnTime;
		private System.Windows.Forms.NumericUpDownTS udTXTestOffTime;
		private System.Windows.Forms.NumericUpDownTS udTXTestOnTime;
		private System.Windows.Forms.Button btnPrintResults;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBoxTS txtSerialNum;
		private System.Windows.Forms.TextBoxTS txtComments;
		private System.Windows.Forms.Label label3;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.LabelTS labelTS1;
		private System.Windows.Forms.TextBoxTS txtSigDelta60;
		private System.Windows.Forms.TextBoxTS txtSigDelta17;
		private System.Windows.Forms.TextBoxTS txtSigDelta40;
		private System.Windows.Forms.TextBoxTS txtSigDelta160;
		private System.Windows.Forms.TextBoxTS txtSigDelta80;
		private System.Windows.Forms.TextBoxTS txtSigDelta12;
		private System.Windows.Forms.TextBoxTS txtSigDelta20;
		private System.Windows.Forms.TextBoxTS txtSigDelta15;
		private System.Windows.Forms.TextBoxTS txtSigDelta30;
		private System.Windows.Forms.TextBoxTS txtSigDelta6;
		private System.Windows.Forms.TextBoxTS txtSigDelta10;
		private System.Windows.Forms.LabelTS lblTolSigHigh;
		private System.Windows.Forms.TextBoxTS txtTolSigHigh;
		private System.Windows.Forms.LabelTS labelTS3;
		private System.Windows.Forms.TextBoxTS txtTolSigLow;
		private System.Windows.Forms.ButtonTS btnRunPTT;
		private System.Windows.Forms.ButtonTS btnRunMic;
		private System.Windows.Forms.LabelTS labelTS4;
		private System.Windows.Forms.TextBoxTS txtTolMic;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor and Destructor

		public ProductionTest(Console c)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			console = c;
			Common.RestoreForm(this, "ProdTest", false);
			udGenClockCorr_ValueChanged(this, EventArgs.Empty);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProductionTest));
			this.txtRefSig160 = new System.Windows.Forms.TextBoxTS();
			this.txtSignal160H = new System.Windows.Forms.TextBoxTS();
			this.lblBand160 = new System.Windows.Forms.LabelTS();
			this.lblBand80 = new System.Windows.Forms.LabelTS();
			this.txtSignal80H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig80 = new System.Windows.Forms.TextBoxTS();
			this.lblBand60 = new System.Windows.Forms.LabelTS();
			this.txtSignal60H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig60 = new System.Windows.Forms.TextBoxTS();
			this.lblBand40 = new System.Windows.Forms.LabelTS();
			this.txtSignal40H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig40 = new System.Windows.Forms.TextBoxTS();
			this.lblBand30 = new System.Windows.Forms.LabelTS();
			this.txtSignal30H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig30 = new System.Windows.Forms.TextBoxTS();
			this.lblBand20 = new System.Windows.Forms.LabelTS();
			this.txtNoise20 = new System.Windows.Forms.TextBoxTS();
			this.txtSignal20H = new System.Windows.Forms.TextBoxTS();
			this.txtRefNoise20 = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig20 = new System.Windows.Forms.TextBoxTS();
			this.lblBand17 = new System.Windows.Forms.LabelTS();
			this.txtSignal17H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig17 = new System.Windows.Forms.TextBoxTS();
			this.lblBand15 = new System.Windows.Forms.LabelTS();
			this.txtSignal15H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig15 = new System.Windows.Forms.TextBoxTS();
			this.lblBand12 = new System.Windows.Forms.LabelTS();
			this.txtSignal12H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig12 = new System.Windows.Forms.TextBoxTS();
			this.lblBand10 = new System.Windows.Forms.LabelTS();
			this.txtSignal10H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig10 = new System.Windows.Forms.TextBoxTS();
			this.lblBand6 = new System.Windows.Forms.LabelTS();
			this.txtSignal6H = new System.Windows.Forms.TextBoxTS();
			this.txtRefSig6 = new System.Windows.Forms.TextBoxTS();
			this.lblNoise = new System.Windows.Forms.LabelTS();
			this.lblSigRef = new System.Windows.Forms.LabelTS();
			this.grpSignal = new System.Windows.Forms.GroupBoxTS();
			this.labelTS1 = new System.Windows.Forms.LabelTS();
			this.txtSigDelta60 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta17 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta6 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta40 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta160 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta80 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta12 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta20 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta10 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta15 = new System.Windows.Forms.TextBoxTS();
			this.txtSigDelta30 = new System.Windows.Forms.TextBoxTS();
			this.lblSignalActual = new System.Windows.Forms.LabelTS();
			this.grpNoise = new System.Windows.Forms.GroupBoxTS();
			this.lblNoiseRef = new System.Windows.Forms.LabelTS();
			this.lblBand = new System.Windows.Forms.LabelTS();
			this.grpPreamp = new System.Windows.Forms.GroupBoxTS();
			this.txtPreAtt = new System.Windows.Forms.TextBoxTS();
			this.lblPreNoise = new System.Windows.Forms.LabelTS();
			this.lblPreSignal = new System.Windows.Forms.LabelTS();
			this.txtPreGain = new System.Windows.Forms.TextBoxTS();
			this.grpTests = new System.Windows.Forms.GroupBoxTS();
			this.btnRunPTT = new System.Windows.Forms.ButtonTS();
			this.btnRunMic = new System.Windows.Forms.ButtonTS();
			this.btnRunBalance = new System.Windows.Forms.ButtonTS();
			this.btnRunRFE = new System.Windows.Forms.ButtonTS();
			this.btnRunTX = new System.Windows.Forms.ButtonTS();
			this.chkRunDash = new System.Windows.Forms.CheckBox();
			this.chkRunDot = new System.Windows.Forms.CheckBox();
			this.btnRunImpulse = new System.Windows.Forms.ButtonTS();
			this.btnClearAll = new System.Windows.Forms.ButtonTS();
			this.btnCheckAll = new System.Windows.Forms.ButtonTS();
			this.btnRunPreamp = new System.Windows.Forms.ButtonTS();
			this.btnRunNoise = new System.Windows.Forms.ButtonTS();
			this.btnRunSignal = new System.Windows.Forms.ButtonTS();
			this.lblSkipCheckedBands = new System.Windows.Forms.LabelTS();
			this.chk6 = new System.Windows.Forms.CheckBoxTS();
			this.chk10 = new System.Windows.Forms.CheckBoxTS();
			this.chk12 = new System.Windows.Forms.CheckBoxTS();
			this.chk15 = new System.Windows.Forms.CheckBoxTS();
			this.chk17 = new System.Windows.Forms.CheckBoxTS();
			this.chk20 = new System.Windows.Forms.CheckBoxTS();
			this.chk30 = new System.Windows.Forms.CheckBoxTS();
			this.chk40 = new System.Windows.Forms.CheckBoxTS();
			this.chk60 = new System.Windows.Forms.CheckBoxTS();
			this.chk80 = new System.Windows.Forms.CheckBoxTS();
			this.chk160 = new System.Windows.Forms.CheckBoxTS();
			this.btnRunAllTests = new System.Windows.Forms.ButtonTS();
			this.grpGenerator = new System.Windows.Forms.GroupBoxTS();
			this.udGenClockCorr = new System.Windows.Forms.NumericUpDownTS();
			this.label1 = new System.Windows.Forms.LabelTS();
			this.btnGenReset = new System.Windows.Forms.ButtonTS();
			this.udGenLevel = new System.Windows.Forms.NumericUpDownTS();
			this.lblGenLevel = new System.Windows.Forms.LabelTS();
			this.udGenFreq = new System.Windows.Forms.NumericUpDownTS();
			this.lblGenFreq = new System.Windows.Forms.LabelTS();
			this.grpTolerance = new System.Windows.Forms.GroupBoxTS();
			this.txtTolSigHigh = new System.Windows.Forms.TextBoxTS();
			this.labelTS3 = new System.Windows.Forms.LabelTS();
			this.txtTolSigLow = new System.Windows.Forms.TextBoxTS();
			this.lblTolBalance = new System.Windows.Forms.LabelTS();
			this.txtTolBalance = new System.Windows.Forms.TextBoxTS();
			this.lblTolImpulse = new System.Windows.Forms.LabelTS();
			this.txtTolImpulse = new System.Windows.Forms.TextBoxTS();
			this.lblTolPreamp = new System.Windows.Forms.LabelTS();
			this.txtTolPreamp = new System.Windows.Forms.TextBoxTS();
			this.txtTolNoise = new System.Windows.Forms.TextBoxTS();
			this.lblTolNoise = new System.Windows.Forms.LabelTS();
			this.lblTolSigHigh = new System.Windows.Forms.LabelTS();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mnuDebug = new System.Windows.Forms.MenuItem();
			this.groupBoxTS1 = new System.Windows.Forms.GroupBoxTS();
			this.udTXTestOffTime = new System.Windows.Forms.NumericUpDownTS();
			this.udTXTestOnTime = new System.Windows.Forms.NumericUpDownTS();
			this.lblTXTestOnTime = new System.Windows.Forms.LabelTS();
			this.labelTS2 = new System.Windows.Forms.LabelTS();
			this.btnPrintResults = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSerialNum = new System.Windows.Forms.TextBoxTS();
			this.txtComments = new System.Windows.Forms.TextBoxTS();
			this.label3 = new System.Windows.Forms.Label();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.labelTS4 = new System.Windows.Forms.LabelTS();
			this.txtTolMic = new System.Windows.Forms.TextBoxTS();
			this.grpSignal.SuspendLayout();
			this.grpNoise.SuspendLayout();
			this.grpPreamp.SuspendLayout();
			this.grpTests.SuspendLayout();
			this.grpGenerator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udGenClockCorr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udGenLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udGenFreq)).BeginInit();
			this.grpTolerance.SuspendLayout();
			this.groupBoxTS1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udTXTestOffTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udTXTestOnTime)).BeginInit();
			this.SuspendLayout();
			// 
			// txtRefSig160
			// 
			this.txtRefSig160.Location = new System.Drawing.Point(64, 24);
			this.txtRefSig160.Name = "txtRefSig160";
			this.txtRefSig160.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig160.TabIndex = 0;
			this.txtRefSig160.Text = "-65.8";
			// 
			// txtSignal160H
			// 
			this.txtSignal160H.Location = new System.Drawing.Point(64, 48);
			this.txtSignal160H.Name = "txtSignal160H";
			this.txtSignal160H.ReadOnly = true;
			this.txtSignal160H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal160H.TabIndex = 2;
			this.txtSignal160H.Text = "";
			// 
			// lblBand160
			// 
			this.lblBand160.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand160.Image = null;
			this.lblBand160.Location = new System.Drawing.Point(80, 112);
			this.lblBand160.Name = "lblBand160";
			this.lblBand160.Size = new System.Drawing.Size(48, 16);
			this.lblBand160.TabIndex = 6;
			this.lblBand160.Text = "160";
			this.lblBand160.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblBand80
			// 
			this.lblBand80.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand80.Image = null;
			this.lblBand80.Location = new System.Drawing.Point(136, 112);
			this.lblBand80.Name = "lblBand80";
			this.lblBand80.Size = new System.Drawing.Size(48, 16);
			this.lblBand80.TabIndex = 13;
			this.lblBand80.Text = "80";
			this.lblBand80.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal80H
			// 
			this.txtSignal80H.Location = new System.Drawing.Point(120, 48);
			this.txtSignal80H.Name = "txtSignal80H";
			this.txtSignal80H.ReadOnly = true;
			this.txtSignal80H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal80H.TabIndex = 9;
			this.txtSignal80H.Text = "";
			// 
			// txtRefSig80
			// 
			this.txtRefSig80.Location = new System.Drawing.Point(120, 24);
			this.txtRefSig80.Name = "txtRefSig80";
			this.txtRefSig80.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig80.TabIndex = 7;
			this.txtRefSig80.Text = "-64.3";
			// 
			// lblBand60
			// 
			this.lblBand60.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand60.Image = null;
			this.lblBand60.Location = new System.Drawing.Point(192, 112);
			this.lblBand60.Name = "lblBand60";
			this.lblBand60.Size = new System.Drawing.Size(48, 16);
			this.lblBand60.TabIndex = 20;
			this.lblBand60.Text = "60";
			this.lblBand60.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal60H
			// 
			this.txtSignal60H.Location = new System.Drawing.Point(176, 48);
			this.txtSignal60H.Name = "txtSignal60H";
			this.txtSignal60H.ReadOnly = true;
			this.txtSignal60H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal60H.TabIndex = 16;
			this.txtSignal60H.Text = "";
			// 
			// txtRefSig60
			// 
			this.txtRefSig60.Location = new System.Drawing.Point(176, 24);
			this.txtRefSig60.Name = "txtRefSig60";
			this.txtRefSig60.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig60.TabIndex = 14;
			this.txtRefSig60.Text = "-65.1";
			// 
			// lblBand40
			// 
			this.lblBand40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand40.Image = null;
			this.lblBand40.Location = new System.Drawing.Point(248, 112);
			this.lblBand40.Name = "lblBand40";
			this.lblBand40.Size = new System.Drawing.Size(48, 16);
			this.lblBand40.TabIndex = 27;
			this.lblBand40.Text = "40";
			this.lblBand40.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal40H
			// 
			this.txtSignal40H.Location = new System.Drawing.Point(232, 48);
			this.txtSignal40H.Name = "txtSignal40H";
			this.txtSignal40H.ReadOnly = true;
			this.txtSignal40H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal40H.TabIndex = 23;
			this.txtSignal40H.Text = "";
			// 
			// txtRefSig40
			// 
			this.txtRefSig40.Location = new System.Drawing.Point(232, 24);
			this.txtRefSig40.Name = "txtRefSig40";
			this.txtRefSig40.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig40.TabIndex = 21;
			this.txtRefSig40.Text = "-64.2";
			// 
			// lblBand30
			// 
			this.lblBand30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand30.Image = null;
			this.lblBand30.Location = new System.Drawing.Point(304, 112);
			this.lblBand30.Name = "lblBand30";
			this.lblBand30.Size = new System.Drawing.Size(48, 16);
			this.lblBand30.TabIndex = 34;
			this.lblBand30.Text = "30";
			this.lblBand30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal30H
			// 
			this.txtSignal30H.Location = new System.Drawing.Point(288, 48);
			this.txtSignal30H.Name = "txtSignal30H";
			this.txtSignal30H.ReadOnly = true;
			this.txtSignal30H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal30H.TabIndex = 30;
			this.txtSignal30H.Text = "";
			// 
			// txtRefSig30
			// 
			this.txtRefSig30.Location = new System.Drawing.Point(288, 24);
			this.txtRefSig30.Name = "txtRefSig30";
			this.txtRefSig30.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig30.TabIndex = 28;
			this.txtRefSig30.Text = "-65.0";
			// 
			// lblBand20
			// 
			this.lblBand20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand20.Image = null;
			this.lblBand20.Location = new System.Drawing.Point(360, 112);
			this.lblBand20.Name = "lblBand20";
			this.lblBand20.Size = new System.Drawing.Size(48, 16);
			this.lblBand20.TabIndex = 41;
			this.lblBand20.Text = "20";
			this.lblBand20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtNoise20
			// 
			this.txtNoise20.Location = new System.Drawing.Point(56, 48);
			this.txtNoise20.Name = "txtNoise20";
			this.txtNoise20.ReadOnly = true;
			this.txtNoise20.Size = new System.Drawing.Size(48, 20);
			this.txtNoise20.TabIndex = 40;
			this.txtNoise20.Text = "";
			// 
			// txtSignal20H
			// 
			this.txtSignal20H.Location = new System.Drawing.Point(344, 48);
			this.txtSignal20H.Name = "txtSignal20H";
			this.txtSignal20H.ReadOnly = true;
			this.txtSignal20H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal20H.TabIndex = 37;
			this.txtSignal20H.Text = "";
			// 
			// txtRefNoise20
			// 
			this.txtRefNoise20.Location = new System.Drawing.Point(56, 24);
			this.txtRefNoise20.Name = "txtRefNoise20";
			this.txtRefNoise20.Size = new System.Drawing.Size(48, 20);
			this.txtRefNoise20.TabIndex = 36;
			this.txtRefNoise20.Text = "-125.0";
			// 
			// txtRefSig20
			// 
			this.txtRefSig20.Location = new System.Drawing.Point(344, 24);
			this.txtRefSig20.Name = "txtRefSig20";
			this.txtRefSig20.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig20.TabIndex = 35;
			this.txtRefSig20.Text = "-64.5";
			// 
			// lblBand17
			// 
			this.lblBand17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand17.Image = null;
			this.lblBand17.Location = new System.Drawing.Point(416, 112);
			this.lblBand17.Name = "lblBand17";
			this.lblBand17.Size = new System.Drawing.Size(48, 16);
			this.lblBand17.TabIndex = 48;
			this.lblBand17.Text = "17";
			this.lblBand17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal17H
			// 
			this.txtSignal17H.Location = new System.Drawing.Point(400, 48);
			this.txtSignal17H.Name = "txtSignal17H";
			this.txtSignal17H.ReadOnly = true;
			this.txtSignal17H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal17H.TabIndex = 44;
			this.txtSignal17H.Text = "";
			// 
			// txtRefSig17
			// 
			this.txtRefSig17.Location = new System.Drawing.Point(400, 24);
			this.txtRefSig17.Name = "txtRefSig17";
			this.txtRefSig17.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig17.TabIndex = 42;
			this.txtRefSig17.Text = "-64.8";
			// 
			// lblBand15
			// 
			this.lblBand15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand15.Image = null;
			this.lblBand15.Location = new System.Drawing.Point(472, 112);
			this.lblBand15.Name = "lblBand15";
			this.lblBand15.Size = new System.Drawing.Size(48, 16);
			this.lblBand15.TabIndex = 55;
			this.lblBand15.Text = "15";
			this.lblBand15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal15H
			// 
			this.txtSignal15H.Location = new System.Drawing.Point(456, 48);
			this.txtSignal15H.Name = "txtSignal15H";
			this.txtSignal15H.ReadOnly = true;
			this.txtSignal15H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal15H.TabIndex = 51;
			this.txtSignal15H.Text = "";
			// 
			// txtRefSig15
			// 
			this.txtRefSig15.Location = new System.Drawing.Point(456, 24);
			this.txtRefSig15.Name = "txtRefSig15";
			this.txtRefSig15.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig15.TabIndex = 49;
			this.txtRefSig15.Text = "-66.2";
			// 
			// lblBand12
			// 
			this.lblBand12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand12.Image = null;
			this.lblBand12.Location = new System.Drawing.Point(528, 112);
			this.lblBand12.Name = "lblBand12";
			this.lblBand12.Size = new System.Drawing.Size(48, 16);
			this.lblBand12.TabIndex = 62;
			this.lblBand12.Text = "12";
			this.lblBand12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal12H
			// 
			this.txtSignal12H.Location = new System.Drawing.Point(512, 48);
			this.txtSignal12H.Name = "txtSignal12H";
			this.txtSignal12H.ReadOnly = true;
			this.txtSignal12H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal12H.TabIndex = 58;
			this.txtSignal12H.Text = "";
			// 
			// txtRefSig12
			// 
			this.txtRefSig12.Location = new System.Drawing.Point(512, 24);
			this.txtRefSig12.Name = "txtRefSig12";
			this.txtRefSig12.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig12.TabIndex = 56;
			this.txtRefSig12.Text = "-65.9";
			// 
			// lblBand10
			// 
			this.lblBand10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand10.Image = null;
			this.lblBand10.Location = new System.Drawing.Point(584, 112);
			this.lblBand10.Name = "lblBand10";
			this.lblBand10.Size = new System.Drawing.Size(48, 16);
			this.lblBand10.TabIndex = 69;
			this.lblBand10.Text = "10";
			this.lblBand10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal10H
			// 
			this.txtSignal10H.Location = new System.Drawing.Point(568, 48);
			this.txtSignal10H.Name = "txtSignal10H";
			this.txtSignal10H.ReadOnly = true;
			this.txtSignal10H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal10H.TabIndex = 65;
			this.txtSignal10H.Text = "";
			// 
			// txtRefSig10
			// 
			this.txtRefSig10.Location = new System.Drawing.Point(568, 24);
			this.txtRefSig10.Name = "txtRefSig10";
			this.txtRefSig10.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig10.TabIndex = 63;
			this.txtRefSig10.Text = "-66.7";
			// 
			// lblBand6
			// 
			this.lblBand6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand6.Image = null;
			this.lblBand6.Location = new System.Drawing.Point(640, 112);
			this.lblBand6.Name = "lblBand6";
			this.lblBand6.Size = new System.Drawing.Size(48, 16);
			this.lblBand6.TabIndex = 76;
			this.lblBand6.Text = "6";
			this.lblBand6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtSignal6H
			// 
			this.txtSignal6H.Location = new System.Drawing.Point(624, 48);
			this.txtSignal6H.Name = "txtSignal6H";
			this.txtSignal6H.ReadOnly = true;
			this.txtSignal6H.Size = new System.Drawing.Size(48, 20);
			this.txtSignal6H.TabIndex = 72;
			this.txtSignal6H.Text = "";
			// 
			// txtRefSig6
			// 
			this.txtRefSig6.Location = new System.Drawing.Point(624, 24);
			this.txtRefSig6.Name = "txtRefSig6";
			this.txtRefSig6.Size = new System.Drawing.Size(48, 20);
			this.txtRefSig6.TabIndex = 70;
			this.txtRefSig6.Text = "-67.6";
			// 
			// lblNoise
			// 
			this.lblNoise.Image = null;
			this.lblNoise.Location = new System.Drawing.Point(16, 48);
			this.lblNoise.Name = "lblNoise";
			this.lblNoise.Size = new System.Drawing.Size(40, 23);
			this.lblNoise.TabIndex = 73;
			this.lblNoise.Text = "Noise:";
			// 
			// lblSigRef
			// 
			this.lblSigRef.Image = null;
			this.lblSigRef.Location = new System.Drawing.Point(16, 24);
			this.lblSigRef.Name = "lblSigRef";
			this.lblSigRef.Size = new System.Drawing.Size(40, 23);
			this.lblSigRef.TabIndex = 72;
			this.lblSigRef.Text = "Ref:";
			// 
			// grpSignal
			// 
			this.grpSignal.Controls.Add(this.labelTS1);
			this.grpSignal.Controls.Add(this.txtSigDelta60);
			this.grpSignal.Controls.Add(this.txtSigDelta17);
			this.grpSignal.Controls.Add(this.txtSigDelta6);
			this.grpSignal.Controls.Add(this.txtSigDelta40);
			this.grpSignal.Controls.Add(this.txtSigDelta160);
			this.grpSignal.Controls.Add(this.txtSigDelta80);
			this.grpSignal.Controls.Add(this.txtSigDelta12);
			this.grpSignal.Controls.Add(this.txtSigDelta20);
			this.grpSignal.Controls.Add(this.txtSigDelta10);
			this.grpSignal.Controls.Add(this.txtSigDelta15);
			this.grpSignal.Controls.Add(this.txtSigDelta30);
			this.grpSignal.Controls.Add(this.lblSignalActual);
			this.grpSignal.Controls.Add(this.txtSignal60H);
			this.grpSignal.Controls.Add(this.txtSignal17H);
			this.grpSignal.Controls.Add(this.txtSignal6H);
			this.grpSignal.Controls.Add(this.txtSignal40H);
			this.grpSignal.Controls.Add(this.txtSignal160H);
			this.grpSignal.Controls.Add(this.txtSignal80H);
			this.grpSignal.Controls.Add(this.txtSignal12H);
			this.grpSignal.Controls.Add(this.txtSignal20H);
			this.grpSignal.Controls.Add(this.txtSignal10H);
			this.grpSignal.Controls.Add(this.txtSignal15H);
			this.grpSignal.Controls.Add(this.txtSignal30H);
			this.grpSignal.Controls.Add(this.txtRefSig40);
			this.grpSignal.Controls.Add(this.txtRefSig6);
			this.grpSignal.Controls.Add(this.lblSigRef);
			this.grpSignal.Controls.Add(this.txtRefSig30);
			this.grpSignal.Controls.Add(this.txtRefSig160);
			this.grpSignal.Controls.Add(this.txtRefSig20);
			this.grpSignal.Controls.Add(this.txtRefSig17);
			this.grpSignal.Controls.Add(this.txtRefSig15);
			this.grpSignal.Controls.Add(this.txtRefSig80);
			this.grpSignal.Controls.Add(this.txtRefSig12);
			this.grpSignal.Controls.Add(this.txtRefSig60);
			this.grpSignal.Controls.Add(this.txtRefSig10);
			this.grpSignal.Location = new System.Drawing.Point(16, 136);
			this.grpSignal.Name = "grpSignal";
			this.grpSignal.Size = new System.Drawing.Size(688, 104);
			this.grpSignal.TabIndex = 78;
			this.grpSignal.TabStop = false;
			this.grpSignal.Text = "Measured Signal (dBm)";
			// 
			// labelTS1
			// 
			this.labelTS1.Image = null;
			this.labelTS1.Location = new System.Drawing.Point(16, 72);
			this.labelTS1.Name = "labelTS1";
			this.labelTS1.Size = new System.Drawing.Size(40, 23);
			this.labelTS1.TabIndex = 87;
			this.labelTS1.Text = "Delta:";
			// 
			// txtSigDelta60
			// 
			this.txtSigDelta60.Location = new System.Drawing.Point(176, 72);
			this.txtSigDelta60.Name = "txtSigDelta60";
			this.txtSigDelta60.ReadOnly = true;
			this.txtSigDelta60.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta60.TabIndex = 78;
			this.txtSigDelta60.Text = "";
			// 
			// txtSigDelta17
			// 
			this.txtSigDelta17.Location = new System.Drawing.Point(400, 72);
			this.txtSigDelta17.Name = "txtSigDelta17";
			this.txtSigDelta17.ReadOnly = true;
			this.txtSigDelta17.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta17.TabIndex = 82;
			this.txtSigDelta17.Text = "";
			// 
			// txtSigDelta6
			// 
			this.txtSigDelta6.Location = new System.Drawing.Point(624, 72);
			this.txtSigDelta6.Name = "txtSigDelta6";
			this.txtSigDelta6.ReadOnly = true;
			this.txtSigDelta6.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta6.TabIndex = 86;
			this.txtSigDelta6.Text = "";
			// 
			// txtSigDelta40
			// 
			this.txtSigDelta40.Location = new System.Drawing.Point(232, 72);
			this.txtSigDelta40.Name = "txtSigDelta40";
			this.txtSigDelta40.ReadOnly = true;
			this.txtSigDelta40.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta40.TabIndex = 79;
			this.txtSigDelta40.Text = "";
			// 
			// txtSigDelta160
			// 
			this.txtSigDelta160.Location = new System.Drawing.Point(64, 72);
			this.txtSigDelta160.Name = "txtSigDelta160";
			this.txtSigDelta160.ReadOnly = true;
			this.txtSigDelta160.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta160.TabIndex = 76;
			this.txtSigDelta160.Text = "";
			// 
			// txtSigDelta80
			// 
			this.txtSigDelta80.Location = new System.Drawing.Point(120, 72);
			this.txtSigDelta80.Name = "txtSigDelta80";
			this.txtSigDelta80.ReadOnly = true;
			this.txtSigDelta80.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta80.TabIndex = 77;
			this.txtSigDelta80.Text = "";
			// 
			// txtSigDelta12
			// 
			this.txtSigDelta12.Location = new System.Drawing.Point(512, 72);
			this.txtSigDelta12.Name = "txtSigDelta12";
			this.txtSigDelta12.ReadOnly = true;
			this.txtSigDelta12.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta12.TabIndex = 84;
			this.txtSigDelta12.Text = "";
			// 
			// txtSigDelta20
			// 
			this.txtSigDelta20.Location = new System.Drawing.Point(344, 72);
			this.txtSigDelta20.Name = "txtSigDelta20";
			this.txtSigDelta20.ReadOnly = true;
			this.txtSigDelta20.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta20.TabIndex = 81;
			this.txtSigDelta20.Text = "";
			// 
			// txtSigDelta10
			// 
			this.txtSigDelta10.Location = new System.Drawing.Point(568, 72);
			this.txtSigDelta10.Name = "txtSigDelta10";
			this.txtSigDelta10.ReadOnly = true;
			this.txtSigDelta10.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta10.TabIndex = 85;
			this.txtSigDelta10.Text = "";
			// 
			// txtSigDelta15
			// 
			this.txtSigDelta15.Location = new System.Drawing.Point(456, 72);
			this.txtSigDelta15.Name = "txtSigDelta15";
			this.txtSigDelta15.ReadOnly = true;
			this.txtSigDelta15.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta15.TabIndex = 83;
			this.txtSigDelta15.Text = "";
			// 
			// txtSigDelta30
			// 
			this.txtSigDelta30.Location = new System.Drawing.Point(288, 72);
			this.txtSigDelta30.Name = "txtSigDelta30";
			this.txtSigDelta30.ReadOnly = true;
			this.txtSigDelta30.Size = new System.Drawing.Size(48, 20);
			this.txtSigDelta30.TabIndex = 80;
			this.txtSigDelta30.Text = "";
			// 
			// lblSignalActual
			// 
			this.lblSignalActual.Image = null;
			this.lblSignalActual.Location = new System.Drawing.Point(16, 48);
			this.lblSignalActual.Name = "lblSignalActual";
			this.lblSignalActual.Size = new System.Drawing.Size(40, 23);
			this.lblSignalActual.TabIndex = 75;
			this.lblSignalActual.Text = "Actual:";
			// 
			// grpNoise
			// 
			this.grpNoise.Controls.Add(this.lblNoiseRef);
			this.grpNoise.Controls.Add(this.txtNoise20);
			this.grpNoise.Controls.Add(this.lblNoise);
			this.grpNoise.Controls.Add(this.txtRefNoise20);
			this.grpNoise.Location = new System.Drawing.Point(16, 248);
			this.grpNoise.Name = "grpNoise";
			this.grpNoise.Size = new System.Drawing.Size(144, 80);
			this.grpNoise.TabIndex = 79;
			this.grpNoise.TabStop = false;
			this.grpNoise.Text = "Measured Noise (dBm)";
			// 
			// lblNoiseRef
			// 
			this.lblNoiseRef.Image = null;
			this.lblNoiseRef.Location = new System.Drawing.Point(16, 25);
			this.lblNoiseRef.Name = "lblNoiseRef";
			this.lblNoiseRef.Size = new System.Drawing.Size(40, 23);
			this.lblNoiseRef.TabIndex = 76;
			this.lblNoiseRef.Text = "Ref:";
			// 
			// lblBand
			// 
			this.lblBand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBand.Image = null;
			this.lblBand.Location = new System.Drawing.Point(32, 112);
			this.lblBand.Name = "lblBand";
			this.lblBand.Size = new System.Drawing.Size(40, 16);
			this.lblBand.TabIndex = 80;
			this.lblBand.Text = "Band";
			// 
			// grpPreamp
			// 
			this.grpPreamp.Controls.Add(this.txtPreAtt);
			this.grpPreamp.Controls.Add(this.lblPreNoise);
			this.grpPreamp.Controls.Add(this.lblPreSignal);
			this.grpPreamp.Controls.Add(this.txtPreGain);
			this.grpPreamp.Location = new System.Drawing.Point(168, 248);
			this.grpPreamp.Name = "grpPreamp";
			this.grpPreamp.Size = new System.Drawing.Size(128, 80);
			this.grpPreamp.TabIndex = 81;
			this.grpPreamp.TabStop = false;
			this.grpPreamp.Text = "Preamp Test (dBm)";
			// 
			// txtPreAtt
			// 
			this.txtPreAtt.Location = new System.Drawing.Point(64, 48);
			this.txtPreAtt.Name = "txtPreAtt";
			this.txtPreAtt.ReadOnly = true;
			this.txtPreAtt.Size = new System.Drawing.Size(48, 20);
			this.txtPreAtt.TabIndex = 4;
			this.txtPreAtt.Text = "";
			// 
			// lblPreNoise
			// 
			this.lblPreNoise.Image = null;
			this.lblPreNoise.Location = new System.Drawing.Point(16, 48);
			this.lblPreNoise.Name = "lblPreNoise";
			this.lblPreNoise.Size = new System.Drawing.Size(48, 16);
			this.lblPreNoise.TabIndex = 2;
			this.lblPreNoise.Text = "Atten:";
			// 
			// lblPreSignal
			// 
			this.lblPreSignal.Image = null;
			this.lblPreSignal.Location = new System.Drawing.Point(16, 24);
			this.lblPreSignal.Name = "lblPreSignal";
			this.lblPreSignal.Size = new System.Drawing.Size(48, 16);
			this.lblPreSignal.TabIndex = 1;
			this.lblPreSignal.Text = "Gain:";
			// 
			// txtPreGain
			// 
			this.txtPreGain.Location = new System.Drawing.Point(64, 24);
			this.txtPreGain.Name = "txtPreGain";
			this.txtPreGain.ReadOnly = true;
			this.txtPreGain.Size = new System.Drawing.Size(48, 20);
			this.txtPreGain.TabIndex = 0;
			this.txtPreGain.Text = "";
			// 
			// grpTests
			// 
			this.grpTests.Controls.Add(this.btnRunPTT);
			this.grpTests.Controls.Add(this.btnRunMic);
			this.grpTests.Controls.Add(this.btnRunBalance);
			this.grpTests.Controls.Add(this.btnRunRFE);
			this.grpTests.Controls.Add(this.btnRunTX);
			this.grpTests.Controls.Add(this.chkRunDash);
			this.grpTests.Controls.Add(this.chkRunDot);
			this.grpTests.Controls.Add(this.btnRunImpulse);
			this.grpTests.Controls.Add(this.btnClearAll);
			this.grpTests.Controls.Add(this.btnCheckAll);
			this.grpTests.Controls.Add(this.btnRunPreamp);
			this.grpTests.Controls.Add(this.btnRunNoise);
			this.grpTests.Controls.Add(this.btnRunSignal);
			this.grpTests.Controls.Add(this.lblSkipCheckedBands);
			this.grpTests.Controls.Add(this.chk6);
			this.grpTests.Controls.Add(this.chk10);
			this.grpTests.Controls.Add(this.chk12);
			this.grpTests.Controls.Add(this.chk15);
			this.grpTests.Controls.Add(this.chk17);
			this.grpTests.Controls.Add(this.chk20);
			this.grpTests.Controls.Add(this.chk30);
			this.grpTests.Controls.Add(this.chk40);
			this.grpTests.Controls.Add(this.chk60);
			this.grpTests.Controls.Add(this.chk80);
			this.grpTests.Controls.Add(this.chk160);
			this.grpTests.Controls.Add(this.btnRunAllTests);
			this.grpTests.Location = new System.Drawing.Point(16, 8);
			this.grpTests.Name = "grpTests";
			this.grpTests.Size = new System.Drawing.Size(688, 88);
			this.grpTests.TabIndex = 82;
			this.grpTests.TabStop = false;
			this.grpTests.Text = "Test Options";
			// 
			// btnRunPTT
			// 
			this.btnRunPTT.Image = null;
			this.btnRunPTT.Location = new System.Drawing.Point(88, 56);
			this.btnRunPTT.Name = "btnRunPTT";
			this.btnRunPTT.Size = new System.Drawing.Size(40, 23);
			this.btnRunPTT.TabIndex = 27;
			this.btnRunPTT.Text = "PTT";
			this.btnRunPTT.Click += new System.EventHandler(this.btnRunPTT_Click);
			// 
			// btnRunMic
			// 
			this.btnRunMic.Image = null;
			this.btnRunMic.Location = new System.Drawing.Point(40, 56);
			this.btnRunMic.Name = "btnRunMic";
			this.btnRunMic.Size = new System.Drawing.Size(40, 23);
			this.btnRunMic.TabIndex = 26;
			this.btnRunMic.Text = "Mic";
			this.btnRunMic.Click += new System.EventHandler(this.btnRunMic_Click);
			// 
			// btnRunBalance
			// 
			this.btnRunBalance.Image = null;
			this.btnRunBalance.Location = new System.Drawing.Point(328, 56);
			this.btnRunBalance.Name = "btnRunBalance";
			this.btnRunBalance.Size = new System.Drawing.Size(56, 23);
			this.btnRunBalance.TabIndex = 25;
			this.btnRunBalance.Text = "Balance";
			this.btnRunBalance.Click += new System.EventHandler(this.btnRunBalance_Click);
			// 
			// btnRunRFE
			// 
			this.btnRunRFE.Image = null;
			this.btnRunRFE.Location = new System.Drawing.Point(280, 56);
			this.btnRunRFE.Name = "btnRunRFE";
			this.btnRunRFE.Size = new System.Drawing.Size(40, 23);
			this.btnRunRFE.TabIndex = 24;
			this.btnRunRFE.Text = "RFE";
			this.btnRunRFE.Click += new System.EventHandler(this.btnRunRFE_Click);
			// 
			// btnRunTX
			// 
			this.btnRunTX.Image = null;
			this.btnRunTX.Location = new System.Drawing.Point(232, 56);
			this.btnRunTX.Name = "btnRunTX";
			this.btnRunTX.Size = new System.Drawing.Size(40, 23);
			this.btnRunTX.TabIndex = 23;
			this.btnRunTX.Text = "TX";
			this.btnRunTX.Click += new System.EventHandler(this.btnRunTX_Click);
			// 
			// chkRunDash
			// 
			this.chkRunDash.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkRunDash.Location = new System.Drawing.Point(184, 56);
			this.chkRunDash.Name = "chkRunDash";
			this.chkRunDash.Size = new System.Drawing.Size(40, 23);
			this.chkRunDash.TabIndex = 22;
			this.chkRunDash.Text = "Dash";
			this.chkRunDash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkRunDash.CheckedChanged += new System.EventHandler(this.chkRunDash_CheckedChanged);
			// 
			// chkRunDot
			// 
			this.chkRunDot.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkRunDot.Location = new System.Drawing.Point(136, 56);
			this.chkRunDot.Name = "chkRunDot";
			this.chkRunDot.Size = new System.Drawing.Size(40, 23);
			this.chkRunDot.TabIndex = 21;
			this.chkRunDot.Text = "Dot";
			this.chkRunDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkRunDot.CheckedChanged += new System.EventHandler(this.chkRunDot_CheckedChanged);
			// 
			// btnRunImpulse
			// 
			this.btnRunImpulse.Image = null;
			this.btnRunImpulse.Location = new System.Drawing.Point(312, 24);
			this.btnRunImpulse.Name = "btnRunImpulse";
			this.btnRunImpulse.Size = new System.Drawing.Size(56, 23);
			this.btnRunImpulse.TabIndex = 18;
			this.btnRunImpulse.Text = "Impulse";
			this.btnRunImpulse.Click += new System.EventHandler(this.btnRunImpulse_Click);
			// 
			// btnClearAll
			// 
			this.btnClearAll.Image = null;
			this.btnClearAll.Location = new System.Drawing.Point(544, 64);
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.Size = new System.Drawing.Size(64, 20);
			this.btnClearAll.TabIndex = 17;
			this.btnClearAll.Text = "Clear All";
			this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
			// 
			// btnCheckAll
			// 
			this.btnCheckAll.Image = null;
			this.btnCheckAll.Location = new System.Drawing.Point(456, 64);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new System.Drawing.Size(64, 20);
			this.btnCheckAll.TabIndex = 16;
			this.btnCheckAll.Text = "Check All";
			this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
			// 
			// btnRunPreamp
			// 
			this.btnRunPreamp.Image = null;
			this.btnRunPreamp.Location = new System.Drawing.Point(248, 24);
			this.btnRunPreamp.Name = "btnRunPreamp";
			this.btnRunPreamp.Size = new System.Drawing.Size(56, 23);
			this.btnRunPreamp.TabIndex = 15;
			this.btnRunPreamp.Text = "Preamp";
			this.btnRunPreamp.Click += new System.EventHandler(this.btnRunPreamp_Click);
			// 
			// btnRunNoise
			// 
			this.btnRunNoise.Image = null;
			this.btnRunNoise.Location = new System.Drawing.Point(192, 24);
			this.btnRunNoise.Name = "btnRunNoise";
			this.btnRunNoise.Size = new System.Drawing.Size(48, 23);
			this.btnRunNoise.TabIndex = 14;
			this.btnRunNoise.Text = "Noise";
			this.btnRunNoise.Click += new System.EventHandler(this.btnRunNoise_Click);
			// 
			// btnRunSignal
			// 
			this.btnRunSignal.Image = null;
			this.btnRunSignal.Location = new System.Drawing.Point(136, 24);
			this.btnRunSignal.Name = "btnRunSignal";
			this.btnRunSignal.Size = new System.Drawing.Size(48, 23);
			this.btnRunSignal.TabIndex = 13;
			this.btnRunSignal.Text = "Signal";
			this.btnRunSignal.Click += new System.EventHandler(this.btnRunSignal_Click);
			// 
			// lblSkipCheckedBands
			// 
			this.lblSkipCheckedBands.Image = null;
			this.lblSkipCheckedBands.Location = new System.Drawing.Point(384, 24);
			this.lblSkipCheckedBands.Name = "lblSkipCheckedBands";
			this.lblSkipCheckedBands.Size = new System.Drawing.Size(56, 40);
			this.lblSkipCheckedBands.TabIndex = 12;
			this.lblSkipCheckedBands.Text = "Skip Checked Bands:";
			// 
			// chk6
			// 
			this.chk6.Image = null;
			this.chk6.Location = new System.Drawing.Point(648, 40);
			this.chk6.Name = "chk6";
			this.chk6.Size = new System.Drawing.Size(32, 24);
			this.chk6.TabIndex = 11;
			this.chk6.Text = "6";
			// 
			// chk10
			// 
			this.chk10.Image = null;
			this.chk10.Location = new System.Drawing.Point(608, 40);
			this.chk10.Name = "chk10";
			this.chk10.Size = new System.Drawing.Size(40, 24);
			this.chk10.TabIndex = 10;
			this.chk10.Text = "10";
			// 
			// chk12
			// 
			this.chk12.Image = null;
			this.chk12.Location = new System.Drawing.Point(568, 40);
			this.chk12.Name = "chk12";
			this.chk12.Size = new System.Drawing.Size(40, 24);
			this.chk12.TabIndex = 9;
			this.chk12.Text = "12";
			// 
			// chk15
			// 
			this.chk15.Image = null;
			this.chk15.Location = new System.Drawing.Point(528, 40);
			this.chk15.Name = "chk15";
			this.chk15.Size = new System.Drawing.Size(40, 24);
			this.chk15.TabIndex = 8;
			this.chk15.Text = "15";
			// 
			// chk17
			// 
			this.chk17.Image = null;
			this.chk17.Location = new System.Drawing.Point(488, 40);
			this.chk17.Name = "chk17";
			this.chk17.Size = new System.Drawing.Size(40, 24);
			this.chk17.TabIndex = 7;
			this.chk17.Text = "17";
			// 
			// chk20
			// 
			this.chk20.Image = null;
			this.chk20.Location = new System.Drawing.Point(448, 40);
			this.chk20.Name = "chk20";
			this.chk20.Size = new System.Drawing.Size(40, 24);
			this.chk20.TabIndex = 6;
			this.chk20.Text = "20";
			// 
			// chk30
			// 
			this.chk30.Image = null;
			this.chk30.Location = new System.Drawing.Point(616, 16);
			this.chk30.Name = "chk30";
			this.chk30.Size = new System.Drawing.Size(40, 24);
			this.chk30.TabIndex = 5;
			this.chk30.Text = "30";
			// 
			// chk40
			// 
			this.chk40.Image = null;
			this.chk40.Location = new System.Drawing.Point(576, 16);
			this.chk40.Name = "chk40";
			this.chk40.Size = new System.Drawing.Size(40, 24);
			this.chk40.TabIndex = 4;
			this.chk40.Text = "40";
			// 
			// chk60
			// 
			this.chk60.Image = null;
			this.chk60.Location = new System.Drawing.Point(536, 16);
			this.chk60.Name = "chk60";
			this.chk60.Size = new System.Drawing.Size(40, 24);
			this.chk60.TabIndex = 3;
			this.chk60.Text = "60";
			// 
			// chk80
			// 
			this.chk80.Image = null;
			this.chk80.Location = new System.Drawing.Point(496, 16);
			this.chk80.Name = "chk80";
			this.chk80.Size = new System.Drawing.Size(40, 24);
			this.chk80.TabIndex = 2;
			this.chk80.Text = "80";
			// 
			// chk160
			// 
			this.chk160.Image = null;
			this.chk160.Location = new System.Drawing.Point(448, 16);
			this.chk160.Name = "chk160";
			this.chk160.Size = new System.Drawing.Size(48, 24);
			this.chk160.TabIndex = 1;
			this.chk160.Text = "160";
			// 
			// btnRunAllTests
			// 
			this.btnRunAllTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnRunAllTests.Image = null;
			this.btnRunAllTests.Location = new System.Drawing.Point(16, 24);
			this.btnRunAllTests.Name = "btnRunAllTests";
			this.btnRunAllTests.Size = new System.Drawing.Size(104, 24);
			this.btnRunAllTests.TabIndex = 0;
			this.btnRunAllTests.Text = "Run All Tests";
			this.toolTip1.SetToolTip(this.btnRunAllTests, "Run all automatic tests.");
			this.btnRunAllTests.Click += new System.EventHandler(this.btnRunAllTests_Click);
			// 
			// grpGenerator
			// 
			this.grpGenerator.Controls.Add(this.udGenClockCorr);
			this.grpGenerator.Controls.Add(this.label1);
			this.grpGenerator.Controls.Add(this.btnGenReset);
			this.grpGenerator.Controls.Add(this.udGenLevel);
			this.grpGenerator.Controls.Add(this.lblGenLevel);
			this.grpGenerator.Controls.Add(this.udGenFreq);
			this.grpGenerator.Controls.Add(this.lblGenFreq);
			this.grpGenerator.Location = new System.Drawing.Point(544, 248);
			this.grpGenerator.Name = "grpGenerator";
			this.grpGenerator.Size = new System.Drawing.Size(152, 144);
			this.grpGenerator.TabIndex = 83;
			this.grpGenerator.TabStop = false;
			this.grpGenerator.Text = "Generator";
			// 
			// udGenClockCorr
			// 
			this.udGenClockCorr.Increment = new System.Decimal(new int[] {
																			 1,
																			 0,
																			 0,
																			 0});
			this.udGenClockCorr.Location = new System.Drawing.Point(80, 96);
			this.udGenClockCorr.Maximum = new System.Decimal(new int[] {
																		   1000000,
																		   0,
																		   0,
																		   0});
			this.udGenClockCorr.Minimum = new System.Decimal(new int[] {
																		   1000000,
																		   0,
																		   0,
																		   -2147483648});
			this.udGenClockCorr.Name = "udGenClockCorr";
			this.udGenClockCorr.Size = new System.Drawing.Size(56, 20);
			this.udGenClockCorr.TabIndex = 6;
			this.udGenClockCorr.Value = new System.Decimal(new int[] {
																		 477,
																		 0,
																		 0,
																		 0});
			this.udGenClockCorr.ValueChanged += new System.EventHandler(this.udGenClockCorr_ValueChanged);
			// 
			// label1
			// 
			this.label1.Image = null;
			this.label1.Location = new System.Drawing.Point(16, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 32);
			this.label1.TabIndex = 5;
			this.label1.Text = "Clock Correction:";
			// 
			// btnGenReset
			// 
			this.btnGenReset.Image = null;
			this.btnGenReset.Location = new System.Drawing.Point(16, 72);
			this.btnGenReset.Name = "btnGenReset";
			this.btnGenReset.Size = new System.Drawing.Size(56, 20);
			this.btnGenReset.TabIndex = 4;
			this.btnGenReset.Text = "Reset";
			this.btnGenReset.Click += new System.EventHandler(this.btnGenReset_Click);
			// 
			// udGenLevel
			// 
			this.udGenLevel.Increment = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 0});
			this.udGenLevel.Location = new System.Drawing.Point(64, 48);
			this.udGenLevel.Maximum = new System.Decimal(new int[] {
																	   4095,
																	   0,
																	   0,
																	   0});
			this.udGenLevel.Minimum = new System.Decimal(new int[] {
																	   0,
																	   0,
																	   0,
																	   0});
			this.udGenLevel.Name = "udGenLevel";
			this.udGenLevel.Size = new System.Drawing.Size(56, 20);
			this.udGenLevel.TabIndex = 3;
			this.udGenLevel.Value = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 0});
			this.udGenLevel.ValueChanged += new System.EventHandler(this.udGenLevel_ValueChanged);
			// 
			// lblGenLevel
			// 
			this.lblGenLevel.Image = null;
			this.lblGenLevel.Location = new System.Drawing.Point(16, 48);
			this.lblGenLevel.Name = "lblGenLevel";
			this.lblGenLevel.Size = new System.Drawing.Size(40, 23);
			this.lblGenLevel.TabIndex = 2;
			this.lblGenLevel.Text = "Level:";
			// 
			// udGenFreq
			// 
			this.udGenFreq.DecimalPlaces = 6;
			this.udGenFreq.Increment = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		0});
			this.udGenFreq.Location = new System.Drawing.Point(64, 24);
			this.udGenFreq.Maximum = new System.Decimal(new int[] {
																	  65,
																	  0,
																	  0,
																	  0});
			this.udGenFreq.Minimum = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.udGenFreq.Name = "udGenFreq";
			this.udGenFreq.Size = new System.Drawing.Size(72, 20);
			this.udGenFreq.TabIndex = 1;
			this.udGenFreq.Value = new System.Decimal(new int[] {
																	70,
																	0,
																	0,
																	65536});
			this.udGenFreq.ValueChanged += new System.EventHandler(this.udGenFreq_ValueChanged);
			// 
			// lblGenFreq
			// 
			this.lblGenFreq.Image = null;
			this.lblGenFreq.Location = new System.Drawing.Point(16, 24);
			this.lblGenFreq.Name = "lblGenFreq";
			this.lblGenFreq.Size = new System.Drawing.Size(32, 23);
			this.lblGenFreq.TabIndex = 0;
			this.lblGenFreq.Text = "Freq:";
			// 
			// grpTolerance
			// 
			this.grpTolerance.Controls.Add(this.txtTolSigHigh);
			this.grpTolerance.Controls.Add(this.labelTS3);
			this.grpTolerance.Controls.Add(this.txtTolSigLow);
			this.grpTolerance.Controls.Add(this.lblTolBalance);
			this.grpTolerance.Controls.Add(this.txtTolBalance);
			this.grpTolerance.Controls.Add(this.lblTolImpulse);
			this.grpTolerance.Controls.Add(this.txtTolImpulse);
			this.grpTolerance.Controls.Add(this.lblTolPreamp);
			this.grpTolerance.Controls.Add(this.txtTolPreamp);
			this.grpTolerance.Controls.Add(this.txtTolNoise);
			this.grpTolerance.Controls.Add(this.lblTolNoise);
			this.grpTolerance.Controls.Add(this.lblTolSigHigh);
			this.grpTolerance.Location = new System.Drawing.Point(304, 248);
			this.grpTolerance.Name = "grpTolerance";
			this.grpTolerance.Size = new System.Drawing.Size(232, 104);
			this.grpTolerance.TabIndex = 84;
			this.grpTolerance.TabStop = false;
			this.grpTolerance.Text = "Tolerance (dBm)";
			// 
			// txtTolSigHigh
			// 
			this.txtTolSigHigh.Location = new System.Drawing.Point(64, 24);
			this.txtTolSigHigh.Name = "txtTolSigHigh";
			this.txtTolSigHigh.Size = new System.Drawing.Size(48, 20);
			this.txtTolSigHigh.TabIndex = 0;
			this.txtTolSigHigh.Text = "3.0";
			// 
			// labelTS3
			// 
			this.labelTS3.Image = null;
			this.labelTS3.Location = new System.Drawing.Point(120, 24);
			this.labelTS3.Name = "labelTS3";
			this.labelTS3.Size = new System.Drawing.Size(48, 16);
			this.labelTS3.TabIndex = 12;
			this.labelTS3.Text = "Sig Low:";
			// 
			// txtTolSigLow
			// 
			this.txtTolSigLow.Location = new System.Drawing.Point(168, 24);
			this.txtTolSigLow.Name = "txtTolSigLow";
			this.txtTolSigLow.Size = new System.Drawing.Size(48, 20);
			this.txtTolSigLow.TabIndex = 11;
			this.txtTolSigLow.Text = "1.5";
			// 
			// lblTolBalance
			// 
			this.lblTolBalance.Image = null;
			this.lblTolBalance.Location = new System.Drawing.Point(120, 72);
			this.lblTolBalance.Name = "lblTolBalance";
			this.lblTolBalance.Size = new System.Drawing.Size(48, 16);
			this.lblTolBalance.TabIndex = 10;
			this.lblTolBalance.Text = "Balance:";
			// 
			// txtTolBalance
			// 
			this.txtTolBalance.Location = new System.Drawing.Point(168, 72);
			this.txtTolBalance.Name = "txtTolBalance";
			this.txtTolBalance.Size = new System.Drawing.Size(48, 20);
			this.txtTolBalance.TabIndex = 9;
			this.txtTolBalance.Text = "0.5";
			// 
			// lblTolImpulse
			// 
			this.lblTolImpulse.Image = null;
			this.lblTolImpulse.Location = new System.Drawing.Point(120, 48);
			this.lblTolImpulse.Name = "lblTolImpulse";
			this.lblTolImpulse.Size = new System.Drawing.Size(48, 16);
			this.lblTolImpulse.TabIndex = 8;
			this.lblTolImpulse.Text = "Impulse:";
			// 
			// txtTolImpulse
			// 
			this.txtTolImpulse.Location = new System.Drawing.Point(168, 48);
			this.txtTolImpulse.Name = "txtTolImpulse";
			this.txtTolImpulse.Size = new System.Drawing.Size(48, 20);
			this.txtTolImpulse.TabIndex = 7;
			this.txtTolImpulse.Text = "3.0";
			// 
			// lblTolPreamp
			// 
			this.lblTolPreamp.Image = null;
			this.lblTolPreamp.Location = new System.Drawing.Point(16, 72);
			this.lblTolPreamp.Name = "lblTolPreamp";
			this.lblTolPreamp.Size = new System.Drawing.Size(48, 16);
			this.lblTolPreamp.TabIndex = 6;
			this.lblTolPreamp.Text = "Preamp:";
			// 
			// txtTolPreamp
			// 
			this.txtTolPreamp.Location = new System.Drawing.Point(64, 72);
			this.txtTolPreamp.Name = "txtTolPreamp";
			this.txtTolPreamp.Size = new System.Drawing.Size(48, 20);
			this.txtTolPreamp.TabIndex = 5;
			this.txtTolPreamp.Text = "0.5";
			// 
			// txtTolNoise
			// 
			this.txtTolNoise.Location = new System.Drawing.Point(64, 48);
			this.txtTolNoise.Name = "txtTolNoise";
			this.txtTolNoise.Size = new System.Drawing.Size(48, 20);
			this.txtTolNoise.TabIndex = 4;
			this.txtTolNoise.Text = "1.5";
			// 
			// lblTolNoise
			// 
			this.lblTolNoise.Image = null;
			this.lblTolNoise.Location = new System.Drawing.Point(16, 48);
			this.lblTolNoise.Name = "lblTolNoise";
			this.lblTolNoise.Size = new System.Drawing.Size(48, 16);
			this.lblTolNoise.TabIndex = 2;
			this.lblTolNoise.Text = "Noise:";
			// 
			// lblTolSigHigh
			// 
			this.lblTolSigHigh.Image = null;
			this.lblTolSigHigh.Location = new System.Drawing.Point(16, 24);
			this.lblTolSigHigh.Name = "lblTolSigHigh";
			this.lblTolSigHigh.Size = new System.Drawing.Size(56, 16);
			this.lblTolSigHigh.TabIndex = 1;
			this.lblTolSigHigh.Text = "Sig High:";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuDebug});
			// 
			// mnuDebug
			// 
			this.mnuDebug.Index = 0;
			this.mnuDebug.Text = "Debug";
			this.mnuDebug.Click += new System.EventHandler(this.mnuDebug_Click);
			// 
			// groupBoxTS1
			// 
			this.groupBoxTS1.Controls.Add(this.udTXTestOffTime);
			this.groupBoxTS1.Controls.Add(this.udTXTestOnTime);
			this.groupBoxTS1.Controls.Add(this.lblTXTestOnTime);
			this.groupBoxTS1.Controls.Add(this.labelTS2);
			this.groupBoxTS1.Location = new System.Drawing.Point(16, 336);
			this.groupBoxTS1.Name = "groupBoxTS1";
			this.groupBoxTS1.Size = new System.Drawing.Size(144, 80);
			this.groupBoxTS1.TabIndex = 85;
			this.groupBoxTS1.TabStop = false;
			this.groupBoxTS1.Text = "Transmit Test";
			// 
			// udTXTestOffTime
			// 
			this.udTXTestOffTime.Increment = new System.Decimal(new int[] {
																			  1,
																			  0,
																			  0,
																			  0});
			this.udTXTestOffTime.Location = new System.Drawing.Point(88, 48);
			this.udTXTestOffTime.Maximum = new System.Decimal(new int[] {
																			9999,
																			0,
																			0,
																			0});
			this.udTXTestOffTime.Minimum = new System.Decimal(new int[] {
																			50,
																			0,
																			0,
																			0});
			this.udTXTestOffTime.Name = "udTXTestOffTime";
			this.udTXTestOffTime.Size = new System.Drawing.Size(48, 20);
			this.udTXTestOffTime.TabIndex = 78;
			this.udTXTestOffTime.Value = new System.Decimal(new int[] {
																		  1000,
																		  0,
																		  0,
																		  0});
			// 
			// udTXTestOnTime
			// 
			this.udTXTestOnTime.Increment = new System.Decimal(new int[] {
																			 1,
																			 0,
																			 0,
																			 0});
			this.udTXTestOnTime.Location = new System.Drawing.Point(88, 24);
			this.udTXTestOnTime.Maximum = new System.Decimal(new int[] {
																		   9999,
																		   0,
																		   0,
																		   0});
			this.udTXTestOnTime.Minimum = new System.Decimal(new int[] {
																		   50,
																		   0,
																		   0,
																		   0});
			this.udTXTestOnTime.Name = "udTXTestOnTime";
			this.udTXTestOnTime.Size = new System.Drawing.Size(48, 20);
			this.udTXTestOnTime.TabIndex = 77;
			this.udTXTestOnTime.Value = new System.Decimal(new int[] {
																		 1000,
																		 0,
																		 0,
																		 0});
			// 
			// lblTXTestOnTime
			// 
			this.lblTXTestOnTime.Image = null;
			this.lblTXTestOnTime.Location = new System.Drawing.Point(16, 24);
			this.lblTXTestOnTime.Name = "lblTXTestOnTime";
			this.lblTXTestOnTime.Size = new System.Drawing.Size(80, 16);
			this.lblTXTestOnTime.TabIndex = 76;
			this.lblTXTestOnTime.Text = "On Time (ms):";
			// 
			// labelTS2
			// 
			this.labelTS2.Image = null;
			this.labelTS2.Location = new System.Drawing.Point(16, 48);
			this.labelTS2.Name = "labelTS2";
			this.labelTS2.Size = new System.Drawing.Size(80, 16);
			this.labelTS2.TabIndex = 73;
			this.labelTS2.Text = "Off Time (ms):";
			// 
			// btnPrintResults
			// 
			this.btnPrintResults.Location = new System.Drawing.Point(184, 336);
			this.btnPrintResults.Name = "btnPrintResults";
			this.btnPrintResults.Size = new System.Drawing.Size(88, 23);
			this.btnPrintResults.TabIndex = 86;
			this.btnPrintResults.Text = "Print Results";
			this.btnPrintResults.Click += new System.EventHandler(this.btnPrintResults_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(176, 368);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 87;
			this.label2.Text = "Serial Num:";
			// 
			// txtSerialNum
			// 
			this.txtSerialNum.Location = new System.Drawing.Point(240, 368);
			this.txtSerialNum.Name = "txtSerialNum";
			this.txtSerialNum.Size = new System.Drawing.Size(72, 20);
			this.txtSerialNum.TabIndex = 88;
			this.txtSerialNum.Text = "";
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(240, 392);
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(288, 20);
			this.txtComments.TabIndex = 89;
			this.txtComments.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(176, 392);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 90;
			this.label3.Text = "Comments:";
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// printPreviewDialog1
			// 
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
			this.printPreviewDialog1.Location = new System.Drawing.Point(581, 32767);
			this.printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty;
			this.printPreviewDialog1.Visible = false;
			// 
			// labelTS4
			// 
			this.labelTS4.Image = null;
			this.labelTS4.Location = new System.Drawing.Point(328, 360);
			this.labelTS4.Name = "labelTS4";
			this.labelTS4.Size = new System.Drawing.Size(32, 16);
			this.labelTS4.TabIndex = 92;
			this.labelTS4.Text = "Mic:";
			// 
			// txtTolMic
			// 
			this.txtTolMic.Location = new System.Drawing.Point(368, 360);
			this.txtTolMic.Name = "txtTolMic";
			this.txtTolMic.Size = new System.Drawing.Size(48, 20);
			this.txtTolMic.TabIndex = 91;
			this.txtTolMic.Text = "1.0";
			// 
			// ProductionTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(712, 425);
			this.Controls.Add(this.labelTS4);
			this.Controls.Add(this.txtTolMic);
			this.Controls.Add(this.txtComments);
			this.Controls.Add(this.txtSerialNum);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnPrintResults);
			this.Controls.Add(this.groupBoxTS1);
			this.Controls.Add(this.grpTolerance);
			this.Controls.Add(this.grpGenerator);
			this.Controls.Add(this.grpTests);
			this.Controls.Add(this.grpPreamp);
			this.Controls.Add(this.lblBand);
			this.Controls.Add(this.lblBand6);
			this.Controls.Add(this.lblBand10);
			this.Controls.Add(this.lblBand12);
			this.Controls.Add(this.lblBand15);
			this.Controls.Add(this.lblBand17);
			this.Controls.Add(this.lblBand20);
			this.Controls.Add(this.lblBand30);
			this.Controls.Add(this.lblBand40);
			this.Controls.Add(this.lblBand60);
			this.Controls.Add(this.lblBand80);
			this.Controls.Add(this.lblBand160);
			this.Controls.Add(this.grpNoise);
			this.Controls.Add(this.grpSignal);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "ProductionTest";
			this.Text = "Production Tests";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductionTest_Closing);
			this.grpSignal.ResumeLayout(false);
			this.grpNoise.ResumeLayout(false);
			this.grpPreamp.ResumeLayout(false);
			this.grpTests.ResumeLayout(false);
			this.grpGenerator.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udGenClockCorr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udGenLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udGenFreq)).EndInit();
			this.grpTolerance.ResumeLayout(false);
			this.groupBoxTS1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udTXTestOffTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udTXTestOnTime)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Misc Routines

		private void DisableTests(Control not_c)
		{
			foreach(Control c in grpTests.Controls)
			{
				if(c.Name.IndexOf("Run") > 0)
				{
					if(c.Name != not_c.Name)
						c.Enabled = false;
				}
			}
		}

		private void EnableTests()
		{
			foreach(Control c in grpTests.Controls)
			{
				if(c.Name.IndexOf("Run") > 0)
				{
					c.Enabled = true;
				}
			}
		}

		private void ClearResults()
		{
			ArrayList controls = new ArrayList();
			Common.ControlList(this, ref controls);

			foreach(Control c in controls)
			{
				if(c.GetType() == typeof(ButtonTS))
				{
					ButtonTS b = (ButtonTS)c;
					if(b.BackColor == Color.Red || b.BackColor == Color.Green || b.BackColor == Color.Yellow)
						b.BackColor = SystemColors.Control;
				}
				else if(c.GetType() == typeof(TextBoxTS))
				{
					TextBoxTS txt = (TextBoxTS)c;
					if(txt.BackColor == Color.Red || txt.BackColor == Color.Green || txt.BackColor == Color.Yellow)
						txt.BackColor = SystemColors.Control;
				}
			}
		}

		#endregion

		#region Test Routines

		private void RunAll()
		{
			ClearResults();

			AD9854.PowerDownFullDigital = false;
			AD9854.ResetDDS();
			AD9854.IAmp = AD9854.QAmp = 5;
			AD9854.SetFreq(50.0);
			Thread.Sleep(1000);
			console.CalibrateFreq(50.0f);

			SignalTest();
			
//			if(progress.Text == "") goto end;
//			progress.Text = "Noise Level Test";
//			progress.SetPercent(0);
//			progress.Show();
//
//			NoiseTest();

			if(progress.Text == "") goto end;
			progress.Text = "Preamp Level Test";
			progress.SetPercent(0);
			progress.Show();

			PreampTest();

			if(progress.Text == "") goto end;
			progress.Text = "Impulse Test";
			progress.SetPercent(0);
			progress.Show();

			ImpulseTest();

			if(progress.Text == "") goto end;
			progress.Text = "Balance Test";
			progress.SetPercent(0);
			progress.Show();

			BalanceTest();

			if(progress.Text == "") goto end;
			progress.Text = "Mic Test";
			progress.SetPercent(0);
			progress.Show();

			MicTest();

			if(progress.Text == "") goto end;
			progress.Text = "PTT Test";
			progress.SetPercent(0);
			progress.Show();

			PTTTest();

			end:
				progress.Hide();

			//AD9854.PowerDownFullDigital = true;
			//console.PowerOn = false;
		}

		private void SignalTest()
		{
			DisableTests(this);

			btnRunSignal.BackColor = Color.Yellow;

			const int DELAY = 1200;

			ArrayList txt = new ArrayList();
			txt.Add(txtSignal160H);
			txt.Add(txtSignal80H);
			txt.Add(txtSignal60H);
			txt.Add(txtSignal40H);
			txt.Add(txtSignal30H);
			txt.Add(txtSignal20H);
			txt.Add(txtSignal17H);
			txt.Add(txtSignal15H);
			txt.Add(txtSignal12H);
			txt.Add(txtSignal10H);
			txt.Add(txtSignal6H);

			ArrayList delta = new ArrayList();
			delta.Add(txtSigDelta160);
			delta.Add(txtSigDelta80);
			delta.Add(txtSigDelta60);
			delta.Add(txtSigDelta40);
			delta.Add(txtSigDelta30);
			delta.Add(txtSigDelta20);
			delta.Add(txtSigDelta17);
			delta.Add(txtSigDelta15);
			delta.Add(txtSigDelta12);
			delta.Add(txtSigDelta10);
			delta.Add(txtSigDelta6);

			ArrayList _ref = new ArrayList();
			_ref.Add(txtRefSig160);
			_ref.Add(txtRefSig80);
			_ref.Add(txtRefSig60);
			_ref.Add(txtRefSig40);
			_ref.Add(txtRefSig30);
			_ref.Add(txtRefSig20);
			_ref.Add(txtRefSig17);
			_ref.Add(txtRefSig15);
			_ref.Add(txtRefSig12);
			_ref.Add(txtRefSig10);
			_ref.Add(txtRefSig6);

			ArrayList chk = new ArrayList();
			chk.Add(chk160);
			chk.Add(chk80);
			chk.Add(chk60);
			chk.Add(chk40);
			chk.Add(chk30);
			chk.Add(chk20);
			chk.Add(chk17);
			chk.Add(chk15);
			chk.Add(chk12);
			chk.Add(chk10);
			chk.Add(chk6);

			float[] freq = { 2.0f, 4.0f, 5.4035f, 7.3f, 10.15f, 14.35f,
				18.168f, 21.45f, 24.99f, 29.7f, 54.0f };

			console.RX1DSPMode = DSPMode.AM;
			console.RX1Filter = Filter.F3;
			console.RX1PreampMode = PreampMode.HIGH;

			AD9854.PowerDownFullDigital = false;
			AD9854.ResetDDS();
			AD9854.IAmp = AD9854.QAmp = 5;
			Thread.Sleep(DELAY);

			for(int i=0; i<freq.Length; i++)
			{
				if(!((CheckBox)chk[i]).Checked)
				{
					((TextBoxTS)txt[i]).BackColor = Color.White;
					AD9854.SetFreq(freq[i]);
					console.VFOAFreq = freq[i];
					Thread.Sleep(2000);

					float level_sum = 0.0f;
					for(int j=0; j<100; j++)
					{
						Thread.Sleep(DELAY/100);
						float num = DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
						num += (console.rx1_meter_cal_offset + console.rx1_filter_size_cal_offset + console.PreampOffset);
						level_sum += num;
						if(!progress.Visible) goto end;
						progress.SetPercent((i*100+j+1)/1100.0f);
					}

					float level = level_sum/100.0f;
					((TextBoxTS)txt[i]).Text = level.ToString("f1");
					((TextBoxTS)delta[i]).Text = (level - TextBoxFloat((TextBoxTS)_ref[i])).ToString("f1");
					((TextBoxTS)txt[i]).BackColor = SystemColors.Control;
				}
			}

		end:
			btnRunSignal.BackColor = SystemColors.Control;
			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			EnableTests();

			CheckSignal();
		}

		private void NoiseTest()
		{
			DisableTests(this);

			btnRunNoise.BackColor = Color.Yellow;

			const int DELAY = 4200;

			console.RX1DSPMode = DSPMode.CWU;
			console.RX1Filter = Filter.F6;
			console.RX1PreampMode = PreampMode.HIGH;

			AD9854.ResetDDS();

			txtNoise20.BackColor = Color.White;
			console.VFOAFreq = 14.1;
			Thread.Sleep(DELAY/4);

			float level_sum = 0.0f;

			for(int i=0; i<100; i++)
			{
				Thread.Sleep(DELAY/100);
				float num = DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
				num += (console.rx1_meter_cal_offset + console.rx1_filter_size_cal_offset + console.PreampOffset);
				level_sum += num;
				if(!progress.Visible) goto end;
				progress.SetPercent((i+1)/100.0f);
			}
			Thread.Sleep(DELAY/4);

			float level = level_sum/100.0f;

			txtNoise20.Text = level.ToString("f1");
			txtNoise20.BackColor = SystemColors.Control;
			if(!progress.Visible) goto end;
			progress.SetPercent(1.0f);

		end:
			btnRunNoise.BackColor = SystemColors.Control;
			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			EnableTests();

			CheckNoise();
		}

		private void PreampTest()
		{
			DisableTests(btnRunPreamp);

			btnRunPreamp.BackColor = Color.Yellow;

			const int DELAY = 1200;

			console.RX1DSPMode = DSPMode.AM;
			console.RX1Filter = Filter.F6;
			console.RX1PreampMode = PreampMode.OFF;

			console.VFOAFreq = 14.175;		

			AD9854.ResetDDS();
			AD9854.IAmp = AD9854.QAmp = 5;
			AD9854.SetFreq(14.175);

			// Preamp Off			
			Thread.Sleep(DELAY);
			float off_level_sum = 0f;
			for(int i=0; i<50; i++)
			{
				off_level_sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
				Thread.Sleep(50);
				if(!progress.Visible) goto end;
				progress.SetPercent((i+1)/200.0f);
			}

			// Preamp Low
			console.RX1PreampMode = PreampMode.LOW;
			Thread.Sleep(DELAY);
			float low_level_sum = 0f;
			for(int i=0; i<50; i++)
			{
				low_level_sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
				Thread.Sleep(50);
				if(!progress.Visible) goto end;
				progress.SetPercent((51+i)/200.0f);
			}

			// Preamp Med
			console.RX1PreampMode = PreampMode.MED;
			Thread.Sleep(DELAY);
			float med_level_sum = 0f;
			for(int i=0; i<50; i++)
			{
				med_level_sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
				Thread.Sleep(50);
				if(!progress.Visible) goto end;
				progress.SetPercent((101+i)/200.0f);
			}

			// Preamp High
			console.RX1PreampMode = PreampMode.HIGH;
			Thread.Sleep(DELAY);
			float high_level_sum = 0f;
			for(int i=0; i<50; i++)
			{
				high_level_sum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.SIGNAL_STRENGTH);
				Thread.Sleep(50);
				if(!progress.Visible) goto end;
				progress.SetPercent((151+i)/200.0f);
			}

			txtPreGain.Text = Math.Abs(med_level_sum/50.0 - off_level_sum/50.0).ToString("f1");
			txtPreAtt.Text = Math.Abs(high_level_sum/50.0 - med_level_sum/50.0).ToString("f1");

		end:
			console.RX1PreampMode = PreampMode.HIGH;
			btnRunPreamp.BackColor = SystemColors.Control;
			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			EnableTests();

			CheckPreamp();
		}

		private void ImpulseTest()
		{
			DisableTests(this);

			btnRunImpulse.BackColor = Color.Yellow;

			console.RX1DSPMode = DSPMode.USB;
			console.RX1Filter = Filter.VAR1;
			console.RX1FilterLow = 7500;
			console.RX1FilterHigh = 8500;
			console.RX1PreampMode = PreampMode.OFF;

			console.VFOAFreq = 14.175;		

			AD9854.ResetDDS();

			Thread.Sleep(1200);

			float level = DttSP.CalculateRXMeter(0, 0,DttSP.MeterType.AVG_SIGNAL_STRENGTH);

			console.Hdw.ImpulseEnable = true;

			for(int i=0; i<2000; i++)
			{
				console.Hdw.Impulse();
				Thread.Sleep(0);
				if(!progress.Visible) goto end;
			}

			float impulse = DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.AVG_SIGNAL_STRENGTH);

			console.Hdw.ImpulseEnable = false;

			Debug.WriteLine("Impulse Diff: "+(impulse-level).ToString("f1"));

			if((impulse-level) > TextBoxFloat(txtTolImpulse))
				btnRunImpulse.BackColor = Color.Green;
			else btnRunImpulse.BackColor = Color.Red;
		end:
			console.RX1PreampMode = PreampMode.HIGH;
			//btnRunImpulse.BackColor = SystemColors.Control;
			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			EnableTests();
		}

		private void MicTest()
		{
			DisableTests(this);

			btnRunMic.BackColor = Color.Yellow;
			console.PWR = 0;
			console.Mic = 0;
			console.MON = true;
			console.AF = 100;
			console.RX1DSPMode = DSPMode.USB;
			console.CurrentMeterTXMode = MeterTXMode.MIC;
			console.MOX = true;
			Thread.Sleep(50);
			console.Hdw.X2 |= 0x04;

			Thread.Sleep(200);

			float start = (float)Math.Max(-20.0, -DttSP.CalculateTXMeter(0, DttSP.MeterType.MIC));
			start = (float)Math.Max(-20.0, -DttSP.CalculateTXMeter(0, DttSP.MeterType.MIC));

			// begin outputting tone
			Audio.TXInputSignal = Audio.SignalSource.SINE;
			Audio.SourceScale = 1.0;

			Thread.Sleep(1000);
			float end = (float)Math.Max(-20.0, -DttSP.CalculateTXMeter(0, DttSP.MeterType.MIC));
			end = (float)Math.Max(-20.0, -DttSP.CalculateTXMeter(0, DttSP.MeterType.MIC));

			console.Hdw.X2 &= 0xFB;
			Audio.TXInputSignal = Audio.SignalSource.RADIO;
			console.MOX = false;
			Thread.Sleep(50);

			if(Math.Abs(end - start) < TextBoxFloat(txtTolMic))
				btnRunMic.BackColor = Color.Red;
			else btnRunMic.BackColor = Color.Green;

			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			EnableTests();
		}

		private void PTTTest()
		{
			DisableTests(this);

			btnRunPTT.BackColor = Color.Yellow;

			console.MOX = false;
			DSPMode dsp_mode = console.RX1DSPMode;
			console.RX1DSPMode = DSPMode.USB;
			console.PreviousPWR = console.PWR;
			console.PWR = 0;
			
			if(console.MOX) goto fail;

			console.Hdw.X2 |= 0x10;
			Thread.Sleep(200);

			bool b = console.MOX;
			console.Hdw.X2 &= 0xEF;

			if(b) goto pass;
			else goto fail;

			fail:
				btnRunPTT.BackColor = Color.Red;
				goto end;
			pass:
				btnRunPTT.BackColor = Color.Green;
				goto end;

			end:

			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			console.PWR = console.PreviousPWR;
			console.RX1DSPMode = dsp_mode;
			EnableTests();
		}

		private void RFETest()
		{
			DisableTests(this);

			btnRunRFE.BackColor = Color.Yellow;

			do
			{
				console.Hdw.TestRFEIC11();
				Thread.Sleep(100);
			} while(progress.Visible);

			btnRunRFE.BackColor = SystemColors.Control;

			EnableTests();
		}

		private void BalanceTest()
		{
			DisableTests(this);

			btnRunBalance.BackColor = Color.Yellow;

			console.RX1DSPMode = DSPMode.AM;
			console.RX1Filter = Filter.F6;
			console.RX1PreampMode = PreampMode.HIGH;

			console.VFOAFreq = 14.175;		

			AD9854.ResetDDS();
			AD9854.IAmp = AD9854.QAmp = 5;
			AD9854.SetFreq(14.175);

			Thread.Sleep(1200);

			float lsum = 0, rsum = 0;
			for(int i=0; i<100; i++)
			{
				lsum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_IMAG);
				rsum += DttSP.CalculateRXMeter(0, 0, DttSP.MeterType.ADC_REAL);
				Thread.Sleep(50);
				if(!progress.Visible) goto end;
				progress.SetPercent((i+1)/100.0f);
			}

			float diff = Math.Abs(lsum/100.0f - rsum/100.0f);
			Debug.WriteLine("Balance Diff: "+diff.ToString("f1"));

            if(diff > TextBoxFloat(txtTolBalance))
				btnRunBalance.BackColor = Color.Red;
			else btnRunBalance.BackColor = Color.Green;

		end:
			if(!progress.Visible) progress.Text = "";
			progress.Hide();
			EnableTests();
		}

		private void TransmitTest()
		{
			DisableTests(this);
			btnRunTX.BackColor = Color.Yellow;

			double vfoa = console.VFOAFreq;

			double[] freq = { 2.0, 4.0, 5.4035, 7.3, 10.15, 14.35,
							   18.168, 21.30, 24.99, 29.0, 50.1 };

			ArrayList chk = new ArrayList();
			chk.Add(chk160);
			chk.Add(chk80);
			chk.Add(chk60);
			chk.Add(chk40);
			chk.Add(chk30);
			chk.Add(chk20);
			chk.Add(chk17);
			chk.Add(chk15);
			chk.Add(chk12);
			chk.Add(chk10);
			chk.Add(chk6);

			console.RX1DSPMode = DSPMode.USB;
			Thread.Sleep(1000);

			for(int i=0; i<freq.Length; i++)
			{
				if(!((CheckBox)chk[i]).Checked)
				{
					console.VFOAFreq = freq[i];
					Thread.Sleep(100);
					console.TUN = true;
					console.PWR = 100;
					for(int j=0; j < udTXTestOnTime.Value; j+=10)
					{
						Thread.Sleep(9);
						if(!progress.Visible) goto end;
					}
					console.TUN = false;
					for(int j=0; j < udTXTestOffTime.Value; j+=10)
					{
						Thread.Sleep(9);
						if(!progress.Visible) goto end;
					}
				}
				progress.SetPercent((i+1)/11.0f);
			}

		end:
			progress.Hide();
			console.VFOAFreq = vfoa;
			console.TUN = false;
			btnRunTX.BackColor = SystemColors.Control;
			EnableTests();
		}

		private void CheckSignal()
		{
			ArrayList txt = new ArrayList();
			txt.Add(txtSigDelta160);
			txt.Add(txtSigDelta80);
			txt.Add(txtSigDelta60);
			txt.Add(txtSigDelta40);
			txt.Add(txtSigDelta30);
			txt.Add(txtSigDelta20);
			txt.Add(txtSigDelta17);
			txt.Add(txtSigDelta15);
			txt.Add(txtSigDelta12);
			txt.Add(txtSigDelta10);
			txt.Add(txtSigDelta6);

			float low_tol = TextBoxFloat(txtTolSigLow);
			float high_tol = TextBoxFloat(txtTolSigHigh);

			foreach(TextBoxTS t in txt)
			{
				float num = TextBoxFloat(t);
				if(num > high_tol || num < -low_tol)
					t.BackColor = Color.Red;
				else t.BackColor = SystemColors.Control;
			}				
		}

		private void CheckNoise()
		{
			float tol = TextBoxFloat(txtTolNoise);

			if(Math.Abs(TextBoxFloat(txtNoise20) - TextBoxFloat(txtRefNoise20)) > tol)
				txtNoise20.BackColor = Color.Red;
			else txtNoise20.BackColor = SystemColors.Control;
		}

		private void CheckPreamp()
		{
			if(Math.Abs(TextBoxFloat(txtPreGain) - 26.0) > TextBoxFloat(txtTolPreamp))
				txtPreGain.BackColor = Color.Red;
			else txtPreGain.BackColor = SystemColors.Control;

			if(Math.Abs(TextBoxFloat(txtPreAtt) - 10.0) > TextBoxFloat(txtTolPreamp))
				txtPreAtt.BackColor = Color.Red;
			else txtPreAtt.BackColor = SystemColors.Control;
		}

		private float TextBoxFloat(TextBox txt)
		{
			try
			{
				return float.Parse(txt.Text);
			}
			catch(Exception)
			{
				return 0.0f;
			}
		}

		private float TextBoxFloat(TextBoxTS txt)
		{
			try
			{
				return float.Parse(txt.Text);
			}
			catch(Exception)
			{
				return 0.0f;
			}
		}

		#endregion

		#region Event Handlers

		private void ProductionTest_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "ProdTest");
		}

		private void btnCheckAll_Click(object sender, System.EventArgs e)
		{
			chk160.Checked = true;
			chk80.Checked = true;
			chk60.Checked = true;
			chk40.Checked = true;
			chk30.Checked = true;
			chk20.Checked = true;
			chk17.Checked = true;
			chk15.Checked = true;
			chk12.Checked = true;
			chk10.Checked = true;
			chk6.Checked = true;
		}

		private void btnClearAll_Click(object sender, System.EventArgs e)
		{
			chk160.Checked = false;
			chk80.Checked = false;
			chk60.Checked = false;
			chk40.Checked = false;
			chk30.Checked = false;
			chk20.Checked = false;
			chk17.Checked = false;
			chk15.Checked = false;
			chk12.Checked = false;
			chk10.Checked = false;
			chk6.Checked = false;
		}

		private void btnRunAllTests_Click(object sender, System.EventArgs e)
		{
			console.PowerOn = true;
			progress = new Progress("Signal Level Test");

			Thread t = new Thread(new ThreadStart(RunAll));
			t.Name = "ProdTestAll";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunSignal_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Signal Level Test");

			Thread t = new Thread(new ThreadStart(SignalTest));
			t.Name = "ProdTestSignal";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunNoise_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Noise Level Test");

			Thread t = new Thread(new ThreadStart(NoiseTest));
			t.Name = "ProdTestNoise";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunPreamp_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Preamp Level Test");

			Thread t = new Thread(new ThreadStart(PreampTest));
			t.Name = "ProdTestPreamp";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunImpulse_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Impulse Test");

			Thread t = new Thread(new ThreadStart(ImpulseTest));
			t.Name = "ImpulseTestPreamp";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void chkRunDot_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkRunDot.Checked)
			{
				if(!console.PowerOn) console.PowerOn = true;
				chkRunDot.BackColor = Color.Yellow;
				DisableTests(chkRunDot);
				console.VFOAFreq = 14.2;
				console.RX1DSPMode = DSPMode.CWU;
				console.RX1Filter = Filter.F6;
				console.PWR = 100;
				
				console.Hdw.X2 |= 0x01;
			}
			else
			{
				EnableTests();
				console.Hdw.X2 &= 0xFE;
				chkRunDot.BackColor = SystemColors.Control;
			}
		}

		private void chkRunDash_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkRunDash.Checked)
			{
				chkRunDash.BackColor = Color.Yellow;
				DisableTests(chkRunDash);
				console.VFOAFreq = 14.2;
				console.RX1DSPMode = DSPMode.CWU;
				console.RX1Filter = Filter.F6;
				console.PWR = 100;

				console.Hdw.X2 |= 0x02;
			}
			else
			{
				EnableTests();
				console.Hdw.X2 &= 0xFD;	
				chkRunDash.BackColor = SystemColors.Control;
			}
		}

		private void btnRunRFE_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("RFE Test");

			Thread t = new Thread(new ThreadStart(RFETest));
			t.Name = "RFETest";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunBalance_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Balance Test");

			Thread t = new Thread(new ThreadStart(BalanceTest));
			t.Name = "BalanceTest";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunMic_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Mic Audio Test");

			Thread t = new Thread(new ThreadStart(MicTest));
			t.Name = "MicTest";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunPTT_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("PTT Test");

			Thread t = new Thread(new ThreadStart(PTTTest));
			t.Name = "PTTTest";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void btnRunTX_Click(object sender, System.EventArgs e)
		{
			progress = new Progress("Transmit Test");

			Thread t = new Thread(new ThreadStart(TransmitTest));
			t.Name = "TransmitTest";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();

			if(console.PowerOn)
				progress.Show();
		}

		private void udGenFreq_ValueChanged(object sender, System.EventArgs e)
		{
			AD9854.SetFreq((double)udGenFreq.Value);
		}

		private void udGenLevel_ValueChanged(object sender, System.EventArgs e)
		{
			if((int)udGenLevel.Value == 0) AD9854.PowerDownFullDigital = true;
			else AD9854.QAmp = AD9854.IAmp = (int)udGenLevel.Value;
		}

		private void btnGenReset_Click(object sender, System.EventArgs e)
		{
			AD9854.PowerDownFullDigital = false;
			AD9854.ResetDDS();
			udGenFreq_ValueChanged(this, EventArgs.Empty);
			udGenLevel_ValueChanged(this, EventArgs.Empty);
		}

		private void udGenClockCorr_ValueChanged(object sender, System.EventArgs e)
		{
			AD9854.ClockCorrection = (int)udGenClockCorr.Value;
			btnGenReset_Click(this, EventArgs.Empty);
		}

		private void mnuDebug_Click(object sender, System.EventArgs e)
		{
			if(debug_form == null || debug_form.IsDisposed)
				debug_form = new ProductionDebug(console);

			debug_form.Show();
			debug_form.Focus();
		}

		private void btnPrintResults_Click(object sender, System.EventArgs e)
		{
			printPreviewDialog1.Document = printDocument1;
			printDocument1.DefaultPageSettings.Landscape = true;
			printPreviewDialog1.ShowDialog();
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			string text = "Date: "+DateTime.Today.ToShortDateString()+"  Time: "+
				DateTime.Now.ToString("HH:mm:ss")+"\n\n";
			text += "FlexRadio SDR-1000 Production Test Results";
			text += "\t\tSerial Number: "+txtSerialNum.Text+"\n";

			text += "\n   Signal Test:\n";
			text += "Band   |  160  |   80  |   60  |   40  |   30  |   20  |   17  |   15  |   12  |   10  |    6  |\n"
				+"------------------------------------------------------------------------------------------------\n"
				+"Ref    | "+txtRefSig160.Text+" | "+txtRefSig80.Text+" | "+txtRefSig60.Text+" | "+txtRefSig40.Text
				+" | "+txtRefSig30.Text+" | "+txtRefSig20.Text+" | "+txtRefSig17.Text+" | "+txtRefSig15.Text
				+" | "+txtRefSig12.Text+" | "+txtRefSig10.Text+" | "+txtRefSig6.Text+" |\n"
				+"Actual | "+txtSignal160H.Text+" | "+txtSignal80H.Text+" | "+txtSignal60H.Text+" | "+txtSignal40H.Text
				+" | "+txtSignal30H.Text+" | "+txtSignal20H.Text+" | "+txtSignal17H.Text+" | "+txtSignal15H.Text
				+" | "+txtSignal12H.Text+" | "+txtSignal10H.Text+" | "+txtSignal6H.Text+" |\n"
				+"------------------------------------------------------------------------------------------------\n"
				+"Delta  |  ";

			ArrayList delta = new ArrayList();
			delta.Add(txtSigDelta160);
			delta.Add(txtSigDelta80);
			delta.Add(txtSigDelta60);
			delta.Add(txtSigDelta40);
			delta.Add(txtSigDelta30);
			delta.Add(txtSigDelta20);
			delta.Add(txtSigDelta17);
			delta.Add(txtSigDelta15);
			delta.Add(txtSigDelta12);
			delta.Add(txtSigDelta10);
			delta.Add(txtSigDelta6);

			foreach(TextBoxTS txt in delta)
			{
				float num = TextBoxFloat(txt);
				if(num >= 0) text += " ";
				text += num.ToString("f1") + " |  ";
			}
			text += "\n";
            
			string impulse_str = null;
			if(btnRunImpulse.BackColor == Color.Green)
				impulse_str = "Passed.";
			else if(btnRunImpulse.BackColor == Color.Red)
				impulse_str = "FAILED.";
			else
				impulse_str = "Not Run";
					
			string balance_str = null;
			if(btnRunBalance.BackColor == Color.Green)
				balance_str = "Passed.";
			else if(btnRunBalance.BackColor == Color.Red)
				balance_str = "FAILED.";
			else
				balance_str = "Not Run";

			text += "\nNoise Test: \t\t\t\tPreamp Test:\t\t\tImpulse Test:\t\tBalance Test:\n";
			text += "Ref:    "+txtRefNoise20.Text+"\t\t\tGain: "+txtPreGain.Text+"\t\t\t"+impulse_str+"\t\t\t"+balance_str
				+"\nActual: "+txtNoise20.Text+"\t\t\tAtten: "+txtPreAtt.Text+"\n";

			if(txtComments.Text != "")
			{
				text += "\nComments: \n\n";
				for(int i=0; i<txtComments.Text.Length; i+=80)
					text += txtComments.Text.Substring(i, Math.Min(80, txtComments.Text.Length-i)) + "\n";
			}

			e.Graphics.DrawString(text, new Font("Lucida Console", 11), Brushes.Black, 80, 80);

		}

		#endregion
	}
}

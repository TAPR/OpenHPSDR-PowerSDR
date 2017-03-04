//=================================================================
// FWCForm.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2007  FlexRadio Systems
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
//    12100 Technology Blvd.
//    Austin, TX 78727
//    USA
//=================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace PowerSDR
{
	/// <summary>
	/// Summary description for FWCForm.
	/// </summary>
	public class FWCForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.NumericUpDown udSlot;
		private System.Windows.Forms.Label lblSlot;
		private System.Windows.Forms.CheckBox chkLED;
		private System.Windows.Forms.CheckBox chkLNA;
		private System.Windows.Forms.GroupBox grpATT;
		private System.Windows.Forms.CheckBox chkATTOn;
		private System.Windows.Forms.NumericUpDown udATTVal;
		private System.Windows.Forms.GroupBox grpClockGen;
		private System.Windows.Forms.ComboBox comboClockGenReg;
		private System.Windows.Forms.TextBox txtClockGenReadVal;
		private System.Windows.Forms.TextBox txtClockGenWriteVal;
		private System.Windows.Forms.Label lblClockGenReg;
		private System.Windows.Forms.Button btnClockGenWrite;
		private System.Windows.Forms.Button btnClockGenRead;
		private System.Windows.Forms.GroupBox grpDDS;
		private System.Windows.Forms.ComboBox comboDDSReg;
		private System.Windows.Forms.TextBox txtDDSWrite;
		private System.Windows.Forms.Label lblDDSReg;
		private System.Windows.Forms.Button btnDDSWrite;
		private System.Windows.Forms.Button btnDDSRead;
		private System.Windows.Forms.ComboBox comboDDSChan;
		private System.Windows.Forms.Label lblDDSChan;
		private System.Windows.Forms.TextBox txtDDSReadVal;
		private System.Windows.Forms.CheckBox chkGPIO7;
		private System.Windows.Forms.CheckBox chkGPIO6;
		private System.Windows.Forms.CheckBox chkGPIO5;
		private System.Windows.Forms.CheckBox chkGPIO4;
		private System.Windows.Forms.CheckBox chkGPIO2;
		private System.Windows.Forms.CheckBox chkGPIO3;
		private System.Windows.Forms.CheckBox chkGPIO1;
		private System.Windows.Forms.CheckBox chkGPIO0;
		private System.Windows.Forms.GroupBox grpGPIO;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtGPIOReadVal;
		private System.Windows.Forms.TextBox txtGPIOWriteVal;
		private System.Windows.Forms.Button btnGPIOWriteVal;
		private System.Windows.Forms.Button btnGPIORead;
		private System.Windows.Forms.GroupBox grpGPIODDR;
		private System.Windows.Forms.TextBox txtGPIODDRReadVal;
		private System.Windows.Forms.TextBox txtGPIODDRWriteVal;
		private System.Windows.Forms.Button btnGPIODDRWrite;
		private System.Windows.Forms.Button btnGPIODDRRead;
		private System.Windows.Forms.CheckBox chkGPIODDR3;
		private System.Windows.Forms.CheckBox chkGPIODDR6;
		private System.Windows.Forms.CheckBox chkGPIODDR1;
		private System.Windows.Forms.CheckBox chkGPIODDR0;
		private System.Windows.Forms.CheckBox chkGPIODDR5;
		private System.Windows.Forms.CheckBox chkGPIODDR4;
		private System.Windows.Forms.CheckBox chkGPIODDR2;
		private System.Windows.Forms.CheckBox chkGPIODDR7;
		private System.Windows.Forms.CheckBox chkClockGenChipSelect;
		private System.Windows.Forms.CheckBox chkDDSChipSelect;
		private System.Windows.Forms.CheckBox chkPIOChipSelect;
		private System.Windows.Forms.Button btnClockGenReset;
		private System.Windows.Forms.Button btnDDSReset;
		private System.Windows.Forms.Button btnDDSIOUpdate;
		private System.Windows.Forms.CheckBox chkTestRelay;
		private System.Windows.Forms.CheckBox chkSigGenRelay;
		private System.Windows.Forms.Label lblATTValue;
		private System.Windows.Forms.GroupBox grpADC;
		private System.Windows.Forms.CheckBox chkADCMaster;
		private System.Windows.Forms.Button btnADCCalState;
		private System.Windows.Forms.CheckBox chkADCHPF;
		private System.Windows.Forms.CheckBox chkADCZCAL;
		private System.Windows.Forms.CheckBox chkEnableQSD;
		private System.Windows.Forms.CheckBox chkPowerDown1;
		private System.Windows.Forms.CheckBox chkPowerDown2;
		private System.Windows.Forms.Button btnPLLStatus;
		private System.Windows.Forms.CheckBox chkDDSMaster;
		private System.Windows.Forms.CheckBox chkImpulse;
		private System.Windows.Forms.CheckBox chkADCReset;
		private System.Windows.Forms.NumericUpDown udOffset;
		private System.Windows.Forms.Label lblDDSAmp;
		private System.Windows.Forms.Label lblSetPort;
		private System.Windows.Forms.NumericUpDown udSetPort;
		private System.Windows.Forms.CheckBox chkPLLEnable;
		private System.Windows.Forms.GroupBox grpPLL;
		private System.Windows.Forms.ComboBox comboPLLRefClock;
		private System.Windows.Forms.CheckBox chkPLLPollStatus;
		private System.Windows.Forms.ComboBox comboPLLStatusMux;
		private System.Windows.Forms.CheckBox chkBypassFilter;
		private System.Windows.Forms.GroupBox grpScanner;
		private System.Windows.Forms.NumericUpDown udScannerStart;
		private System.Windows.Forms.Label lblScannerStart;
		private System.Windows.Forms.Label lblScannerStop;
		private System.Windows.Forms.NumericUpDown udScannerStop;
		private System.Windows.Forms.Label lblScannerStep;
		private System.Windows.Forms.NumericUpDown udScannerStep;
		private System.Windows.Forms.Label lblScannerDelay;
		private System.Windows.Forms.NumericUpDown udScannerDelay;
		private System.Windows.Forms.CheckBox chkScannerGo;
		private System.Windows.Forms.Label lblPLLClock;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboPLLCPMode;
		private System.Windows.Forms.CheckBox chkPLLPFDPol;
		private System.Windows.Forms.Label lblIFFreq;
		private System.Windows.Forms.NumericUpDown udIFFreq;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor and Destructor

		public FWCForm(Console c)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			console = c;
			if(!console.fwc_init)
				console.fwc_init = FWC.Open1394Driver();

			for(int i=0; i<=0x5A; i++)
			{
				if((i >= 1 && i <= 3) ||
					(i >= 0x0E && i <= 0x33) ||
					(i >= 0x37 && i <= 0x3C) ||
					(i >= 0x42 && i <= 0x44) ||
					(i >= 0x46 && i <= 0x49) ||
					(i >= 0x54 && i <= 0x57) ||
					i == 0x59)
				{

				}
				else
				{
					comboClockGenReg.Items.Add(String.Format("{0:X2}", i));
				}
			}

			for(int i=0; i<=0x18; i++)
				comboDDSReg.Items.Add(String.Format("{0:X2}", i));

			comboClockGenReg.SelectedIndex = 0;
			comboDDSChan.SelectedIndex = 0;
			comboDDSReg.SelectedIndex = 0;
			comboPLLRefClock.SelectedIndex = 0;
			comboPLLStatusMux.SelectedIndex = 1;
			comboPLLCPMode.SelectedIndex = 0;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			console.fwc_init = false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FWCForm));
			this.udSlot = new System.Windows.Forms.NumericUpDown();
			this.lblSlot = new System.Windows.Forms.Label();
			this.chkLED = new System.Windows.Forms.CheckBox();
			this.grpATT = new System.Windows.Forms.GroupBox();
			this.udATTVal = new System.Windows.Forms.NumericUpDown();
			this.lblATTValue = new System.Windows.Forms.Label();
			this.chkATTOn = new System.Windows.Forms.CheckBox();
			this.chkLNA = new System.Windows.Forms.CheckBox();
			this.grpClockGen = new System.Windows.Forms.GroupBox();
			this.btnClockGenReset = new System.Windows.Forms.Button();
			this.chkClockGenChipSelect = new System.Windows.Forms.CheckBox();
			this.comboClockGenReg = new System.Windows.Forms.ComboBox();
			this.txtClockGenReadVal = new System.Windows.Forms.TextBox();
			this.txtClockGenWriteVal = new System.Windows.Forms.TextBox();
			this.lblClockGenReg = new System.Windows.Forms.Label();
			this.btnClockGenWrite = new System.Windows.Forms.Button();
			this.btnClockGenRead = new System.Windows.Forms.Button();
			this.grpDDS = new System.Windows.Forms.GroupBox();
			this.chkDDSChipSelect = new System.Windows.Forms.CheckBox();
			this.comboDDSChan = new System.Windows.Forms.ComboBox();
			this.lblDDSChan = new System.Windows.Forms.Label();
			this.comboDDSReg = new System.Windows.Forms.ComboBox();
			this.txtDDSWrite = new System.Windows.Forms.TextBox();
			this.lblDDSReg = new System.Windows.Forms.Label();
			this.btnDDSWrite = new System.Windows.Forms.Button();
			this.txtDDSReadVal = new System.Windows.Forms.TextBox();
			this.btnDDSRead = new System.Windows.Forms.Button();
			this.btnDDSReset = new System.Windows.Forms.Button();
			this.btnDDSIOUpdate = new System.Windows.Forms.Button();
			this.chkGPIO7 = new System.Windows.Forms.CheckBox();
			this.chkGPIO6 = new System.Windows.Forms.CheckBox();
			this.chkGPIO5 = new System.Windows.Forms.CheckBox();
			this.chkGPIO4 = new System.Windows.Forms.CheckBox();
			this.chkGPIO2 = new System.Windows.Forms.CheckBox();
			this.chkGPIO3 = new System.Windows.Forms.CheckBox();
			this.chkGPIO1 = new System.Windows.Forms.CheckBox();
			this.chkGPIO0 = new System.Windows.Forms.CheckBox();
			this.grpGPIO = new System.Windows.Forms.GroupBox();
			this.txtGPIOReadVal = new System.Windows.Forms.TextBox();
			this.txtGPIOWriteVal = new System.Windows.Forms.TextBox();
			this.btnGPIOWriteVal = new System.Windows.Forms.Button();
			this.btnGPIORead = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.grpGPIODDR = new System.Windows.Forms.GroupBox();
			this.txtGPIODDRReadVal = new System.Windows.Forms.TextBox();
			this.txtGPIODDRWriteVal = new System.Windows.Forms.TextBox();
			this.btnGPIODDRWrite = new System.Windows.Forms.Button();
			this.btnGPIODDRRead = new System.Windows.Forms.Button();
			this.chkGPIODDR3 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR6 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR1 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR0 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR5 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR4 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR2 = new System.Windows.Forms.CheckBox();
			this.chkGPIODDR7 = new System.Windows.Forms.CheckBox();
			this.chkPIOChipSelect = new System.Windows.Forms.CheckBox();
			this.chkTestRelay = new System.Windows.Forms.CheckBox();
			this.chkSigGenRelay = new System.Windows.Forms.CheckBox();
			this.grpADC = new System.Windows.Forms.GroupBox();
			this.chkADCReset = new System.Windows.Forms.CheckBox();
			this.chkADCZCAL = new System.Windows.Forms.CheckBox();
			this.chkADCHPF = new System.Windows.Forms.CheckBox();
			this.btnADCCalState = new System.Windows.Forms.Button();
			this.chkADCMaster = new System.Windows.Forms.CheckBox();
			this.chkEnableQSD = new System.Windows.Forms.CheckBox();
			this.chkPowerDown1 = new System.Windows.Forms.CheckBox();
			this.chkPowerDown2 = new System.Windows.Forms.CheckBox();
			this.btnPLLStatus = new System.Windows.Forms.Button();
			this.chkDDSMaster = new System.Windows.Forms.CheckBox();
			this.chkPLLEnable = new System.Windows.Forms.CheckBox();
			this.chkImpulse = new System.Windows.Forms.CheckBox();
			this.udOffset = new System.Windows.Forms.NumericUpDown();
			this.lblDDSAmp = new System.Windows.Forms.Label();
			this.lblSetPort = new System.Windows.Forms.Label();
			this.udSetPort = new System.Windows.Forms.NumericUpDown();
			this.grpPLL = new System.Windows.Forms.GroupBox();
			this.lblPLLClock = new System.Windows.Forms.Label();
			this.chkPLLPFDPol = new System.Windows.Forms.CheckBox();
			this.comboPLLCPMode = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.comboPLLStatusMux = new System.Windows.Forms.ComboBox();
			this.chkPLLPollStatus = new System.Windows.Forms.CheckBox();
			this.comboPLLRefClock = new System.Windows.Forms.ComboBox();
			this.chkBypassFilter = new System.Windows.Forms.CheckBox();
			this.grpScanner = new System.Windows.Forms.GroupBox();
			this.chkScannerGo = new System.Windows.Forms.CheckBox();
			this.udScannerDelay = new System.Windows.Forms.NumericUpDown();
			this.lblScannerDelay = new System.Windows.Forms.Label();
			this.lblScannerStep = new System.Windows.Forms.Label();
			this.udScannerStep = new System.Windows.Forms.NumericUpDown();
			this.lblScannerStop = new System.Windows.Forms.Label();
			this.udScannerStop = new System.Windows.Forms.NumericUpDown();
			this.lblScannerStart = new System.Windows.Forms.Label();
			this.udScannerStart = new System.Windows.Forms.NumericUpDown();
			this.lblIFFreq = new System.Windows.Forms.Label();
			this.udIFFreq = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.udSlot)).BeginInit();
			this.grpATT.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udATTVal)).BeginInit();
			this.grpClockGen.SuspendLayout();
			this.grpDDS.SuspendLayout();
			this.grpGPIO.SuspendLayout();
			this.grpGPIODDR.SuspendLayout();
			this.grpADC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSetPort)).BeginInit();
			this.grpPLL.SuspendLayout();
			this.grpScanner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udScannerDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udScannerStep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udScannerStop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udScannerStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udIFFreq)).BeginInit();
			this.SuspendLayout();
			// 
			// udSlot
			// 
			this.udSlot.Location = new System.Drawing.Point(48, 16);
			this.udSlot.Maximum = new System.Decimal(new int[] {
																   7,
																   0,
																   0,
																   0});
			this.udSlot.Name = "udSlot";
			this.udSlot.Size = new System.Drawing.Size(40, 20);
			this.udSlot.TabIndex = 0;
			// 
			// lblSlot
			// 
			this.lblSlot.Location = new System.Drawing.Point(16, 16);
			this.lblSlot.Name = "lblSlot";
			this.lblSlot.Size = new System.Drawing.Size(32, 16);
			this.lblSlot.TabIndex = 1;
			this.lblSlot.Text = "Slot:";
			// 
			// chkLED
			// 
			this.chkLED.Location = new System.Drawing.Point(16, 48);
			this.chkLED.Name = "chkLED";
			this.chkLED.Size = new System.Drawing.Size(48, 24);
			this.chkLED.TabIndex = 2;
			this.chkLED.Text = "LED";
			this.chkLED.CheckedChanged += new System.EventHandler(this.chkLED_CheckedChanged);
			// 
			// grpATT
			// 
			this.grpATT.Controls.Add(this.udATTVal);
			this.grpATT.Controls.Add(this.lblATTValue);
			this.grpATT.Controls.Add(this.chkATTOn);
			this.grpATT.Location = new System.Drawing.Point(104, 16);
			this.grpATT.Name = "grpATT";
			this.grpATT.Size = new System.Drawing.Size(128, 80);
			this.grpATT.TabIndex = 3;
			this.grpATT.TabStop = false;
			this.grpATT.Text = "ATT";
			// 
			// udATTVal
			// 
			this.udATTVal.Location = new System.Drawing.Point(80, 48);
			this.udATTVal.Maximum = new System.Decimal(new int[] {
																	 31,
																	 0,
																	 0,
																	 0});
			this.udATTVal.Name = "udATTVal";
			this.udATTVal.Size = new System.Drawing.Size(40, 20);
			this.udATTVal.TabIndex = 2;
			this.udATTVal.ValueChanged += new System.EventHandler(this.udATTVal_ValueChanged);
			// 
			// lblATTValue
			// 
			this.lblATTValue.Location = new System.Drawing.Point(16, 48);
			this.lblATTValue.Name = "lblATTValue";
			this.lblATTValue.Size = new System.Drawing.Size(64, 16);
			this.lblATTValue.TabIndex = 1;
			this.lblATTValue.Text = "Value (dB):";
			// 
			// chkATTOn
			// 
			this.chkATTOn.Location = new System.Drawing.Point(16, 24);
			this.chkATTOn.Name = "chkATTOn";
			this.chkATTOn.Size = new System.Drawing.Size(40, 16);
			this.chkATTOn.TabIndex = 0;
			this.chkATTOn.Text = "On";
			this.chkATTOn.CheckedChanged += new System.EventHandler(this.chkATTOn_CheckedChanged);
			// 
			// chkLNA
			// 
			this.chkLNA.Location = new System.Drawing.Point(16, 72);
			this.chkLNA.Name = "chkLNA";
			this.chkLNA.Size = new System.Drawing.Size(48, 24);
			this.chkLNA.TabIndex = 4;
			this.chkLNA.Text = "LNA";
			this.chkLNA.CheckedChanged += new System.EventHandler(this.chkLNA_CheckedChanged);
			// 
			// grpClockGen
			// 
			this.grpClockGen.Controls.Add(this.btnClockGenReset);
			this.grpClockGen.Controls.Add(this.chkClockGenChipSelect);
			this.grpClockGen.Controls.Add(this.comboClockGenReg);
			this.grpClockGen.Controls.Add(this.txtClockGenReadVal);
			this.grpClockGen.Controls.Add(this.txtClockGenWriteVal);
			this.grpClockGen.Controls.Add(this.lblClockGenReg);
			this.grpClockGen.Controls.Add(this.btnClockGenWrite);
			this.grpClockGen.Controls.Add(this.btnClockGenRead);
			this.grpClockGen.Location = new System.Drawing.Point(8, 320);
			this.grpClockGen.Name = "grpClockGen";
			this.grpClockGen.Size = new System.Drawing.Size(168, 112);
			this.grpClockGen.TabIndex = 20;
			this.grpClockGen.TabStop = false;
			this.grpClockGen.Text = "ClockGen";
			// 
			// btnClockGenReset
			// 
			this.btnClockGenReset.Location = new System.Drawing.Point(112, 80);
			this.btnClockGenReset.Name = "btnClockGenReset";
			this.btnClockGenReset.Size = new System.Drawing.Size(48, 23);
			this.btnClockGenReset.TabIndex = 28;
			this.btnClockGenReset.Text = "Reset";
			this.btnClockGenReset.Visible = false;
			this.btnClockGenReset.Click += new System.EventHandler(this.btnClockGenReset_Click);
			// 
			// chkClockGenChipSelect
			// 
			this.chkClockGenChipSelect.Location = new System.Drawing.Point(16, 80);
			this.chkClockGenChipSelect.Name = "chkClockGenChipSelect";
			this.chkClockGenChipSelect.Size = new System.Drawing.Size(88, 24);
			this.chkClockGenChipSelect.TabIndex = 27;
			this.chkClockGenChipSelect.Text = "Chip Select";
			this.chkClockGenChipSelect.Visible = false;
			this.chkClockGenChipSelect.CheckedChanged += new System.EventHandler(this.chkClockGenChipSelect_CheckedChanged);
			// 
			// comboClockGenReg
			// 
			this.comboClockGenReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClockGenReg.Location = new System.Drawing.Point(64, 24);
			this.comboClockGenReg.Name = "comboClockGenReg";
			this.comboClockGenReg.Size = new System.Drawing.Size(40, 21);
			this.comboClockGenReg.TabIndex = 26;
			this.comboClockGenReg.SelectedIndexChanged += new System.EventHandler(this.comboClockGenReg_SelectedIndexChanged);
			// 
			// txtClockGenReadVal
			// 
			this.txtClockGenReadVal.Location = new System.Drawing.Point(64, 48);
			this.txtClockGenReadVal.Name = "txtClockGenReadVal";
			this.txtClockGenReadVal.ReadOnly = true;
			this.txtClockGenReadVal.Size = new System.Drawing.Size(40, 20);
			this.txtClockGenReadVal.TabIndex = 4;
			this.txtClockGenReadVal.Text = "0";
			// 
			// txtClockGenWriteVal
			// 
			this.txtClockGenWriteVal.Location = new System.Drawing.Point(112, 24);
			this.txtClockGenWriteVal.Name = "txtClockGenWriteVal";
			this.txtClockGenWriteVal.Size = new System.Drawing.Size(40, 20);
			this.txtClockGenWriteVal.TabIndex = 3;
			this.txtClockGenWriteVal.Text = "0";
			// 
			// lblClockGenReg
			// 
			this.lblClockGenReg.Location = new System.Drawing.Point(16, 24);
			this.lblClockGenReg.Name = "lblClockGenReg";
			this.lblClockGenReg.Size = new System.Drawing.Size(56, 16);
			this.lblClockGenReg.TabIndex = 1;
			this.lblClockGenReg.Text = "Register:";
			// 
			// btnClockGenWrite
			// 
			this.btnClockGenWrite.Location = new System.Drawing.Point(112, 48);
			this.btnClockGenWrite.Name = "btnClockGenWrite";
			this.btnClockGenWrite.Size = new System.Drawing.Size(40, 23);
			this.btnClockGenWrite.TabIndex = 24;
			this.btnClockGenWrite.Text = "Write";
			this.btnClockGenWrite.Click += new System.EventHandler(this.btnClockGenWrite_Click);
			// 
			// btnClockGenRead
			// 
			this.btnClockGenRead.Location = new System.Drawing.Point(16, 48);
			this.btnClockGenRead.Name = "btnClockGenRead";
			this.btnClockGenRead.Size = new System.Drawing.Size(40, 23);
			this.btnClockGenRead.TabIndex = 25;
			this.btnClockGenRead.Text = "Read";
			this.btnClockGenRead.Click += new System.EventHandler(this.btnClockGenRead_Click);
			// 
			// grpDDS
			// 
			this.grpDDS.Controls.Add(this.chkDDSChipSelect);
			this.grpDDS.Controls.Add(this.comboDDSChan);
			this.grpDDS.Controls.Add(this.lblDDSChan);
			this.grpDDS.Controls.Add(this.comboDDSReg);
			this.grpDDS.Controls.Add(this.txtDDSWrite);
			this.grpDDS.Controls.Add(this.lblDDSReg);
			this.grpDDS.Controls.Add(this.btnDDSWrite);
			this.grpDDS.Controls.Add(this.txtDDSReadVal);
			this.grpDDS.Controls.Add(this.btnDDSRead);
			this.grpDDS.Controls.Add(this.btnDDSReset);
			this.grpDDS.Controls.Add(this.btnDDSIOUpdate);
			this.grpDDS.Location = new System.Drawing.Point(192, 320);
			this.grpDDS.Name = "grpDDS";
			this.grpDDS.Size = new System.Drawing.Size(280, 112);
			this.grpDDS.TabIndex = 21;
			this.grpDDS.TabStop = false;
			this.grpDDS.Text = "DDS";
			// 
			// chkDDSChipSelect
			// 
			this.chkDDSChipSelect.Location = new System.Drawing.Point(112, 80);
			this.chkDDSChipSelect.Name = "chkDDSChipSelect";
			this.chkDDSChipSelect.Size = new System.Drawing.Size(88, 24);
			this.chkDDSChipSelect.TabIndex = 41;
			this.chkDDSChipSelect.Text = "Chip Select";
			this.chkDDSChipSelect.CheckedChanged += new System.EventHandler(this.chkDDSChipSelect_CheckedChanged);
			// 
			// comboDDSChan
			// 
			this.comboDDSChan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDDSChan.Items.AddRange(new object[] {
																 "0",
																 "1",
																 "2",
																 "3"});
			this.comboDDSChan.Location = new System.Drawing.Point(64, 80);
			this.comboDDSChan.Name = "comboDDSChan";
			this.comboDDSChan.Size = new System.Drawing.Size(40, 21);
			this.comboDDSChan.TabIndex = 40;
			this.comboDDSChan.SelectedIndexChanged += new System.EventHandler(this.comboDDSChan_SelectedIndexChanged);
			// 
			// lblDDSChan
			// 
			this.lblDDSChan.Location = new System.Drawing.Point(16, 80);
			this.lblDDSChan.Name = "lblDDSChan";
			this.lblDDSChan.Size = new System.Drawing.Size(56, 16);
			this.lblDDSChan.TabIndex = 39;
			this.lblDDSChan.Text = "Channel:";
			// 
			// comboDDSReg
			// 
			this.comboDDSReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDDSReg.Location = new System.Drawing.Point(64, 24);
			this.comboDDSReg.Name = "comboDDSReg";
			this.comboDDSReg.Size = new System.Drawing.Size(40, 21);
			this.comboDDSReg.TabIndex = 38;
			this.comboDDSReg.SelectedIndexChanged += new System.EventHandler(this.comboDDSReg_SelectedIndexChanged);
			// 
			// txtDDSWrite
			// 
			this.txtDDSWrite.Location = new System.Drawing.Point(112, 24);
			this.txtDDSWrite.Name = "txtDDSWrite";
			this.txtDDSWrite.Size = new System.Drawing.Size(64, 20);
			this.txtDDSWrite.TabIndex = 34;
			this.txtDDSWrite.Text = "0";
			// 
			// lblDDSReg
			// 
			this.lblDDSReg.Location = new System.Drawing.Point(16, 24);
			this.lblDDSReg.Name = "lblDDSReg";
			this.lblDDSReg.Size = new System.Drawing.Size(56, 16);
			this.lblDDSReg.TabIndex = 33;
			this.lblDDSReg.Text = "Register:";
			// 
			// btnDDSWrite
			// 
			this.btnDDSWrite.Location = new System.Drawing.Point(136, 48);
			this.btnDDSWrite.Name = "btnDDSWrite";
			this.btnDDSWrite.Size = new System.Drawing.Size(40, 23);
			this.btnDDSWrite.TabIndex = 36;
			this.btnDDSWrite.Text = "Write";
			this.btnDDSWrite.Click += new System.EventHandler(this.btnDDSWrite_Click);
			// 
			// txtDDSReadVal
			// 
			this.txtDDSReadVal.Location = new System.Drawing.Point(64, 48);
			this.txtDDSReadVal.Name = "txtDDSReadVal";
			this.txtDDSReadVal.ReadOnly = true;
			this.txtDDSReadVal.Size = new System.Drawing.Size(64, 20);
			this.txtDDSReadVal.TabIndex = 26;
			this.txtDDSReadVal.Text = "0";
			// 
			// btnDDSRead
			// 
			this.btnDDSRead.Location = new System.Drawing.Point(16, 48);
			this.btnDDSRead.Name = "btnDDSRead";
			this.btnDDSRead.Size = new System.Drawing.Size(40, 23);
			this.btnDDSRead.TabIndex = 27;
			this.btnDDSRead.Text = "Read";
			this.btnDDSRead.Click += new System.EventHandler(this.btnDDSRead_Click);
			// 
			// btnDDSReset
			// 
			this.btnDDSReset.Location = new System.Drawing.Point(200, 64);
			this.btnDDSReset.Name = "btnDDSReset";
			this.btnDDSReset.Size = new System.Drawing.Size(48, 23);
			this.btnDDSReset.TabIndex = 43;
			this.btnDDSReset.Text = "Reset";
			this.btnDDSReset.Click += new System.EventHandler(this.btnDDSReset_Click);
			// 
			// btnDDSIOUpdate
			// 
			this.btnDDSIOUpdate.Location = new System.Drawing.Point(200, 24);
			this.btnDDSIOUpdate.Name = "btnDDSIOUpdate";
			this.btnDDSIOUpdate.Size = new System.Drawing.Size(64, 23);
			this.btnDDSIOUpdate.TabIndex = 44;
			this.btnDDSIOUpdate.Text = "IO Update";
			this.btnDDSIOUpdate.Click += new System.EventHandler(this.btnDDSUpdate_Click);
			// 
			// chkGPIO7
			// 
			this.chkGPIO7.Location = new System.Drawing.Point(16, 32);
			this.chkGPIO7.Name = "chkGPIO7";
			this.chkGPIO7.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO7.TabIndex = 22;
			this.chkGPIO7.Text = "checkBox1";
			this.chkGPIO7.CheckedChanged += new System.EventHandler(this.chkGPIO7_CheckedChanged);
			// 
			// chkGPIO6
			// 
			this.chkGPIO6.Location = new System.Drawing.Point(40, 32);
			this.chkGPIO6.Name = "chkGPIO6";
			this.chkGPIO6.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO6.TabIndex = 23;
			this.chkGPIO6.Text = "checkBox2";
			this.chkGPIO6.CheckedChanged += new System.EventHandler(this.chkGPIO6_CheckedChanged);
			// 
			// chkGPIO5
			// 
			this.chkGPIO5.Location = new System.Drawing.Point(64, 32);
			this.chkGPIO5.Name = "chkGPIO5";
			this.chkGPIO5.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO5.TabIndex = 25;
			this.chkGPIO5.Text = "checkBox3";
			this.chkGPIO5.CheckedChanged += new System.EventHandler(this.chkGPIO5_CheckedChanged);
			// 
			// chkGPIO4
			// 
			this.chkGPIO4.Location = new System.Drawing.Point(88, 32);
			this.chkGPIO4.Name = "chkGPIO4";
			this.chkGPIO4.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO4.TabIndex = 24;
			this.chkGPIO4.Text = "checkBox4";
			this.chkGPIO4.CheckedChanged += new System.EventHandler(this.chkGPIO4_CheckedChanged);
			// 
			// chkGPIO2
			// 
			this.chkGPIO2.Location = new System.Drawing.Point(136, 32);
			this.chkGPIO2.Name = "chkGPIO2";
			this.chkGPIO2.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO2.TabIndex = 29;
			this.chkGPIO2.Text = "checkBox5";
			this.chkGPIO2.CheckedChanged += new System.EventHandler(this.chkGPIO2_CheckedChanged);
			// 
			// chkGPIO3
			// 
			this.chkGPIO3.Location = new System.Drawing.Point(112, 32);
			this.chkGPIO3.Name = "chkGPIO3";
			this.chkGPIO3.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO3.TabIndex = 28;
			this.chkGPIO3.Text = "checkBox6";
			this.chkGPIO3.CheckedChanged += new System.EventHandler(this.chkGPIO3_CheckedChanged);
			// 
			// chkGPIO1
			// 
			this.chkGPIO1.Location = new System.Drawing.Point(160, 32);
			this.chkGPIO1.Name = "chkGPIO1";
			this.chkGPIO1.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO1.TabIndex = 27;
			this.chkGPIO1.Text = "checkBox7";
			this.chkGPIO1.CheckedChanged += new System.EventHandler(this.chkGPIO1_CheckedChanged);
			// 
			// chkGPIO0
			// 
			this.chkGPIO0.Location = new System.Drawing.Point(184, 32);
			this.chkGPIO0.Name = "chkGPIO0";
			this.chkGPIO0.Size = new System.Drawing.Size(16, 24);
			this.chkGPIO0.TabIndex = 26;
			this.chkGPIO0.Text = "checkBox8";
			this.chkGPIO0.CheckedChanged += new System.EventHandler(this.chkGPIO0_CheckedChanged);
			// 
			// grpGPIO
			// 
			this.grpGPIO.Controls.Add(this.txtGPIOReadVal);
			this.grpGPIO.Controls.Add(this.txtGPIOWriteVal);
			this.grpGPIO.Controls.Add(this.btnGPIOWriteVal);
			this.grpGPIO.Controls.Add(this.btnGPIORead);
			this.grpGPIO.Controls.Add(this.label9);
			this.grpGPIO.Controls.Add(this.label8);
			this.grpGPIO.Controls.Add(this.label6);
			this.grpGPIO.Controls.Add(this.label7);
			this.grpGPIO.Controls.Add(this.label5);
			this.grpGPIO.Controls.Add(this.label4);
			this.grpGPIO.Controls.Add(this.label3);
			this.grpGPIO.Controls.Add(this.label2);
			this.grpGPIO.Controls.Add(this.chkGPIO3);
			this.grpGPIO.Controls.Add(this.chkGPIO6);
			this.grpGPIO.Controls.Add(this.chkGPIO1);
			this.grpGPIO.Controls.Add(this.chkGPIO0);
			this.grpGPIO.Controls.Add(this.chkGPIO5);
			this.grpGPIO.Controls.Add(this.chkGPIO4);
			this.grpGPIO.Controls.Add(this.chkGPIO2);
			this.grpGPIO.Controls.Add(this.chkGPIO7);
			this.grpGPIO.Location = new System.Drawing.Point(8, 200);
			this.grpGPIO.Name = "grpGPIO";
			this.grpGPIO.Size = new System.Drawing.Size(216, 112);
			this.grpGPIO.TabIndex = 30;
			this.grpGPIO.TabStop = false;
			this.grpGPIO.Text = "GPIO";
			// 
			// txtGPIOReadVal
			// 
			this.txtGPIOReadVal.Location = new System.Drawing.Point(64, 80);
			this.txtGPIOReadVal.Name = "txtGPIOReadVal";
			this.txtGPIOReadVal.ReadOnly = true;
			this.txtGPIOReadVal.Size = new System.Drawing.Size(40, 20);
			this.txtGPIOReadVal.TabIndex = 39;
			this.txtGPIOReadVal.Text = "0";
			// 
			// txtGPIOWriteVal
			// 
			this.txtGPIOWriteVal.Location = new System.Drawing.Point(160, 80);
			this.txtGPIOWriteVal.Name = "txtGPIOWriteVal";
			this.txtGPIOWriteVal.Size = new System.Drawing.Size(40, 20);
			this.txtGPIOWriteVal.TabIndex = 38;
			this.txtGPIOWriteVal.Text = "0";
			// 
			// btnGPIOWriteVal
			// 
			this.btnGPIOWriteVal.Location = new System.Drawing.Point(112, 80);
			this.btnGPIOWriteVal.Name = "btnGPIOWriteVal";
			this.btnGPIOWriteVal.Size = new System.Drawing.Size(40, 23);
			this.btnGPIOWriteVal.TabIndex = 40;
			this.btnGPIOWriteVal.Text = "Write";
			this.btnGPIOWriteVal.Click += new System.EventHandler(this.btnGPIOWriteVal_Click);
			// 
			// btnGPIORead
			// 
			this.btnGPIORead.Location = new System.Drawing.Point(16, 80);
			this.btnGPIORead.Name = "btnGPIORead";
			this.btnGPIORead.Size = new System.Drawing.Size(40, 23);
			this.btnGPIORead.TabIndex = 41;
			this.btnGPIORead.Text = "Read";
			this.btnGPIORead.Click += new System.EventHandler(this.btnGPIORead_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(32, 16);
			this.label9.TabIndex = 37;
			this.label9.Text = "/INT";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(32, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 16);
			this.label8.TabIndex = 36;
			this.label8.Text = "PIOTX";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(80, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 34;
			this.label6.Text = "PCLK";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(56, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 35;
			this.label7.Text = "IOUD";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(112, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 16);
			this.label5.TabIndex = 33;
			this.label5.Text = "D3";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(136, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 16);
			this.label4.TabIndex = 32;
			this.label4.Text = "D2";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(160, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 16);
			this.label3.TabIndex = 31;
			this.label3.Text = "D1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(184, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 30;
			this.label2.Text = "D0";
			// 
			// grpGPIODDR
			// 
			this.grpGPIODDR.Controls.Add(this.txtGPIODDRReadVal);
			this.grpGPIODDR.Controls.Add(this.txtGPIODDRWriteVal);
			this.grpGPIODDR.Controls.Add(this.btnGPIODDRWrite);
			this.grpGPIODDR.Controls.Add(this.btnGPIODDRRead);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR3);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR6);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR1);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR0);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR5);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR4);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR2);
			this.grpGPIODDR.Controls.Add(this.chkGPIODDR7);
			this.grpGPIODDR.Location = new System.Drawing.Point(232, 200);
			this.grpGPIODDR.Name = "grpGPIODDR";
			this.grpGPIODDR.Size = new System.Drawing.Size(216, 112);
			this.grpGPIODDR.TabIndex = 31;
			this.grpGPIODDR.TabStop = false;
			this.grpGPIODDR.Text = "GPIO DDR";
			// 
			// txtGPIODDRReadVal
			// 
			this.txtGPIODDRReadVal.Location = new System.Drawing.Point(64, 80);
			this.txtGPIODDRReadVal.Name = "txtGPIODDRReadVal";
			this.txtGPIODDRReadVal.ReadOnly = true;
			this.txtGPIODDRReadVal.Size = new System.Drawing.Size(40, 20);
			this.txtGPIODDRReadVal.TabIndex = 39;
			this.txtGPIODDRReadVal.Text = "0";
			// 
			// txtGPIODDRWriteVal
			// 
			this.txtGPIODDRWriteVal.Location = new System.Drawing.Point(160, 80);
			this.txtGPIODDRWriteVal.Name = "txtGPIODDRWriteVal";
			this.txtGPIODDRWriteVal.Size = new System.Drawing.Size(40, 20);
			this.txtGPIODDRWriteVal.TabIndex = 38;
			this.txtGPIODDRWriteVal.Text = "0";
			// 
			// btnGPIODDRWrite
			// 
			this.btnGPIODDRWrite.Location = new System.Drawing.Point(112, 80);
			this.btnGPIODDRWrite.Name = "btnGPIODDRWrite";
			this.btnGPIODDRWrite.Size = new System.Drawing.Size(40, 23);
			this.btnGPIODDRWrite.TabIndex = 40;
			this.btnGPIODDRWrite.Text = "Write";
			this.btnGPIODDRWrite.Click += new System.EventHandler(this.btnGPIODDRWrite_Click);
			// 
			// btnGPIODDRRead
			// 
			this.btnGPIODDRRead.Location = new System.Drawing.Point(16, 80);
			this.btnGPIODDRRead.Name = "btnGPIODDRRead";
			this.btnGPIODDRRead.Size = new System.Drawing.Size(40, 23);
			this.btnGPIODDRRead.TabIndex = 41;
			this.btnGPIODDRRead.Text = "Read";
			this.btnGPIODDRRead.Click += new System.EventHandler(this.btnGPIODDRRead_Click);
			// 
			// chkGPIODDR3
			// 
			this.chkGPIODDR3.Location = new System.Drawing.Point(112, 32);
			this.chkGPIODDR3.Name = "chkGPIODDR3";
			this.chkGPIODDR3.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR3.TabIndex = 28;
			this.chkGPIODDR3.Text = "checkBox6";
			this.chkGPIODDR3.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR6
			// 
			this.chkGPIODDR6.Location = new System.Drawing.Point(40, 32);
			this.chkGPIODDR6.Name = "chkGPIODDR6";
			this.chkGPIODDR6.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR6.TabIndex = 23;
			this.chkGPIODDR6.Text = "checkBox2";
			this.chkGPIODDR6.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR1
			// 
			this.chkGPIODDR1.Location = new System.Drawing.Point(160, 32);
			this.chkGPIODDR1.Name = "chkGPIODDR1";
			this.chkGPIODDR1.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR1.TabIndex = 27;
			this.chkGPIODDR1.Text = "checkBox7";
			this.chkGPIODDR1.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR0
			// 
			this.chkGPIODDR0.Location = new System.Drawing.Point(184, 32);
			this.chkGPIODDR0.Name = "chkGPIODDR0";
			this.chkGPIODDR0.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR0.TabIndex = 26;
			this.chkGPIODDR0.Text = "checkBox8";
			this.chkGPIODDR0.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR5
			// 
			this.chkGPIODDR5.Location = new System.Drawing.Point(64, 32);
			this.chkGPIODDR5.Name = "chkGPIODDR5";
			this.chkGPIODDR5.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR5.TabIndex = 25;
			this.chkGPIODDR5.Text = "checkBox3";
			this.chkGPIODDR5.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR4
			// 
			this.chkGPIODDR4.Location = new System.Drawing.Point(88, 32);
			this.chkGPIODDR4.Name = "chkGPIODDR4";
			this.chkGPIODDR4.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR4.TabIndex = 24;
			this.chkGPIODDR4.Text = "checkBox4";
			this.chkGPIODDR4.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR2
			// 
			this.chkGPIODDR2.Location = new System.Drawing.Point(136, 32);
			this.chkGPIODDR2.Name = "chkGPIODDR2";
			this.chkGPIODDR2.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR2.TabIndex = 29;
			this.chkGPIODDR2.Text = "checkBox5";
			this.chkGPIODDR2.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkGPIODDR7
			// 
			this.chkGPIODDR7.Location = new System.Drawing.Point(16, 32);
			this.chkGPIODDR7.Name = "chkGPIODDR7";
			this.chkGPIODDR7.Size = new System.Drawing.Size(16, 24);
			this.chkGPIODDR7.TabIndex = 22;
			this.chkGPIODDR7.Text = "checkBox1";
			this.chkGPIODDR7.CheckedChanged += new System.EventHandler(this.chkGPIODDR_CheckedChanged);
			// 
			// chkPIOChipSelect
			// 
			this.chkPIOChipSelect.Location = new System.Drawing.Point(24, 440);
			this.chkPIOChipSelect.Name = "chkPIOChipSelect";
			this.chkPIOChipSelect.TabIndex = 42;
			this.chkPIOChipSelect.Text = "PIO Chip Select";
			this.chkPIOChipSelect.CheckedChanged += new System.EventHandler(this.chkPIOChipSelect_CheckedChanged);
			// 
			// chkTestRelay
			// 
			this.chkTestRelay.Location = new System.Drawing.Point(16, 96);
			this.chkTestRelay.Name = "chkTestRelay";
			this.chkTestRelay.Size = new System.Drawing.Size(80, 24);
			this.chkTestRelay.TabIndex = 43;
			this.chkTestRelay.Text = "Test Relay";
			this.chkTestRelay.CheckedChanged += new System.EventHandler(this.chkTestRelay_CheckedChanged);
			// 
			// chkSigGenRelay
			// 
			this.chkSigGenRelay.Location = new System.Drawing.Point(16, 120);
			this.chkSigGenRelay.Name = "chkSigGenRelay";
			this.chkSigGenRelay.Size = new System.Drawing.Size(96, 24);
			this.chkSigGenRelay.TabIndex = 44;
			this.chkSigGenRelay.Text = "Sig Gen Relay";
			this.chkSigGenRelay.CheckedChanged += new System.EventHandler(this.chkSigGenRelay_CheckedChanged);
			// 
			// grpADC
			// 
			this.grpADC.Controls.Add(this.chkADCReset);
			this.grpADC.Controls.Add(this.chkADCZCAL);
			this.grpADC.Controls.Add(this.chkADCHPF);
			this.grpADC.Controls.Add(this.btnADCCalState);
			this.grpADC.Controls.Add(this.chkADCMaster);
			this.grpADC.Location = new System.Drawing.Point(240, 16);
			this.grpADC.Name = "grpADC";
			this.grpADC.Size = new System.Drawing.Size(200, 104);
			this.grpADC.TabIndex = 45;
			this.grpADC.TabStop = false;
			this.grpADC.Text = "ADC";
			// 
			// chkADCReset
			// 
			this.chkADCReset.Location = new System.Drawing.Point(80, 72);
			this.chkADCReset.Name = "chkADCReset";
			this.chkADCReset.Size = new System.Drawing.Size(72, 24);
			this.chkADCReset.TabIndex = 4;
			this.chkADCReset.Text = "Reset";
			this.chkADCReset.CheckedChanged += new System.EventHandler(this.chkADCReset_CheckedChanged);
			// 
			// chkADCZCAL
			// 
			this.chkADCZCAL.Location = new System.Drawing.Point(16, 72);
			this.chkADCZCAL.Name = "chkADCZCAL";
			this.chkADCZCAL.Size = new System.Drawing.Size(56, 24);
			this.chkADCZCAL.TabIndex = 3;
			this.chkADCZCAL.Text = "ZCAL";
			this.chkADCZCAL.CheckedChanged += new System.EventHandler(this.chkADCZCAL_CheckedChanged);
			// 
			// chkADCHPF
			// 
			this.chkADCHPF.Checked = true;
			this.chkADCHPF.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkADCHPF.Location = new System.Drawing.Point(16, 48);
			this.chkADCHPF.Name = "chkADCHPF";
			this.chkADCHPF.TabIndex = 2;
			this.chkADCHPF.Text = "High Pass Filter";
			this.chkADCHPF.CheckedChanged += new System.EventHandler(this.chkADCHPF_CheckedChanged);
			// 
			// btnADCCalState
			// 
			this.btnADCCalState.Location = new System.Drawing.Point(112, 24);
			this.btnADCCalState.Name = "btnADCCalState";
			this.btnADCCalState.Size = new System.Drawing.Size(64, 23);
			this.btnADCCalState.TabIndex = 1;
			this.btnADCCalState.Text = "Cal State";
			this.btnADCCalState.Click += new System.EventHandler(this.btnADCCalState_Click);
			// 
			// chkADCMaster
			// 
			this.chkADCMaster.Checked = true;
			this.chkADCMaster.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkADCMaster.Location = new System.Drawing.Point(16, 24);
			this.chkADCMaster.Name = "chkADCMaster";
			this.chkADCMaster.Size = new System.Drawing.Size(64, 24);
			this.chkADCMaster.TabIndex = 0;
			this.chkADCMaster.Text = "Master";
			this.chkADCMaster.CheckedChanged += new System.EventHandler(this.chkADCMaster_CheckedChanged);
			// 
			// chkEnableQSD
			// 
			this.chkEnableQSD.Checked = true;
			this.chkEnableQSD.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEnableQSD.Location = new System.Drawing.Point(16, 144);
			this.chkEnableQSD.Name = "chkEnableQSD";
			this.chkEnableQSD.Size = new System.Drawing.Size(88, 24);
			this.chkEnableQSD.TabIndex = 46;
			this.chkEnableQSD.Text = "Enable QSD";
			this.chkEnableQSD.CheckedChanged += new System.EventHandler(this.chkEnableQSD_CheckedChanged);
			// 
			// chkPowerDown1
			// 
			this.chkPowerDown1.Location = new System.Drawing.Point(112, 120);
			this.chkPowerDown1.Name = "chkPowerDown1";
			this.chkPowerDown1.TabIndex = 47;
			this.chkPowerDown1.Text = "Power Down 1";
			this.chkPowerDown1.CheckedChanged += new System.EventHandler(this.chkPowerDown1_CheckedChanged);
			// 
			// chkPowerDown2
			// 
			this.chkPowerDown2.Location = new System.Drawing.Point(112, 144);
			this.chkPowerDown2.Name = "chkPowerDown2";
			this.chkPowerDown2.TabIndex = 48;
			this.chkPowerDown2.Text = "Power Down 2";
			this.chkPowerDown2.CheckedChanged += new System.EventHandler(this.chkPowerDown2_CheckedChanged);
			// 
			// btnPLLStatus
			// 
			this.btnPLLStatus.Location = new System.Drawing.Point(16, 48);
			this.btnPLLStatus.Name = "btnPLLStatus";
			this.btnPLLStatus.Size = new System.Drawing.Size(48, 23);
			this.btnPLLStatus.TabIndex = 49;
			this.btnPLLStatus.Text = "Status";
			this.btnPLLStatus.Click += new System.EventHandler(this.btnPLLStatus_Click);
			// 
			// chkDDSMaster
			// 
			this.chkDDSMaster.Location = new System.Drawing.Point(16, 168);
			this.chkDDSMaster.Name = "chkDDSMaster";
			this.chkDDSMaster.Size = new System.Drawing.Size(96, 24);
			this.chkDDSMaster.TabIndex = 50;
			this.chkDDSMaster.Text = "DDS Master";
			this.chkDDSMaster.CheckedChanged += new System.EventHandler(this.chkDDSMaster_CheckedChanged);
			// 
			// chkPLLEnable
			// 
			this.chkPLLEnable.Location = new System.Drawing.Point(16, 24);
			this.chkPLLEnable.Name = "chkPLLEnable";
			this.chkPLLEnable.Size = new System.Drawing.Size(64, 24);
			this.chkPLLEnable.TabIndex = 51;
			this.chkPLLEnable.Text = "Enable";
			this.chkPLLEnable.CheckedChanged += new System.EventHandler(this.chkChargePump_CheckedChanged);
			// 
			// chkImpulse
			// 
			this.chkImpulse.Location = new System.Drawing.Point(224, 144);
			this.chkImpulse.Name = "chkImpulse";
			this.chkImpulse.Size = new System.Drawing.Size(88, 24);
			this.chkImpulse.TabIndex = 52;
			this.chkImpulse.Text = "Impulse";
			this.chkImpulse.CheckedChanged += new System.EventHandler(this.chkImpulse_CheckedChanged);
			// 
			// udOffset
			// 
			this.udOffset.Location = new System.Drawing.Point(392, 144);
			this.udOffset.Maximum = new System.Decimal(new int[] {
																	 1023,
																	 0,
																	 0,
																	 0});
			this.udOffset.Name = "udOffset";
			this.udOffset.Size = new System.Drawing.Size(56, 20);
			this.udOffset.TabIndex = 53;
			this.udOffset.ValueChanged += new System.EventHandler(this.udOffset_ValueChanged);
			// 
			// lblDDSAmp
			// 
			this.lblDDSAmp.Location = new System.Drawing.Point(304, 144);
			this.lblDDSAmp.Name = "lblDDSAmp";
			this.lblDDSAmp.Size = new System.Drawing.Size(88, 23);
			this.lblDDSAmp.TabIndex = 54;
			this.lblDDSAmp.Text = "DDS Amplitude:";
			// 
			// lblSetPort
			// 
			this.lblSetPort.Location = new System.Drawing.Point(304, 168);
			this.lblSetPort.Name = "lblSetPort";
			this.lblSetPort.Size = new System.Drawing.Size(56, 23);
			this.lblSetPort.TabIndex = 57;
			this.lblSetPort.Text = "Set Port:";
			// 
			// udSetPort
			// 
			this.udSetPort.Location = new System.Drawing.Point(392, 168);
			this.udSetPort.Maximum = new System.Decimal(new int[] {
																	  31,
																	  0,
																	  0,
																	  0});
			this.udSetPort.Name = "udSetPort";
			this.udSetPort.Size = new System.Drawing.Size(56, 20);
			this.udSetPort.TabIndex = 56;
			this.udSetPort.ValueChanged += new System.EventHandler(this.udSetPort_ValueChanged);
			// 
			// grpPLL
			// 
			this.grpPLL.Controls.Add(this.lblPLLClock);
			this.grpPLL.Controls.Add(this.chkPLLPFDPol);
			this.grpPLL.Controls.Add(this.comboPLLCPMode);
			this.grpPLL.Controls.Add(this.label10);
			this.grpPLL.Controls.Add(this.comboPLLStatusMux);
			this.grpPLL.Controls.Add(this.chkPLLPollStatus);
			this.grpPLL.Controls.Add(this.comboPLLRefClock);
			this.grpPLL.Controls.Add(this.chkPLLEnable);
			this.grpPLL.Controls.Add(this.btnPLLStatus);
			this.grpPLL.Location = new System.Drawing.Point(448, 16);
			this.grpPLL.Name = "grpPLL";
			this.grpPLL.Size = new System.Drawing.Size(248, 144);
			this.grpPLL.TabIndex = 58;
			this.grpPLL.TabStop = false;
			this.grpPLL.Text = "PLL";
			// 
			// lblPLLClock
			// 
			this.lblPLLClock.Location = new System.Drawing.Point(136, 24);
			this.lblPLLClock.Name = "lblPLLClock";
			this.lblPLLClock.Size = new System.Drawing.Size(64, 23);
			this.lblPLLClock.TabIndex = 55;
			this.lblPLLClock.Text = "PLL Clock:";
			// 
			// chkPLLPFDPol
			// 
			this.chkPLLPFDPol.Location = new System.Drawing.Point(16, 104);
			this.chkPLLPFDPol.Name = "chkPLLPFDPol";
			this.chkPLLPFDPol.Size = new System.Drawing.Size(88, 24);
			this.chkPLLPFDPol.TabIndex = 59;
			this.chkPLLPFDPol.Text = "PFD Polarity";
			this.chkPLLPFDPol.CheckedChanged += new System.EventHandler(this.chkPLLPFDPol_CheckedChanged);
			// 
			// comboPLLCPMode
			// 
			this.comboPLLCPMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPLLCPMode.Items.AddRange(new object[] {
																"Tri-Stated",
																"Pump-Up",
																"Pump-Down",
																"Normal"});
			this.comboPLLCPMode.Location = new System.Drawing.Point(184, 53);
			this.comboPLLCPMode.Name = "comboPLLCPMode";
			this.comboPLLCPMode.Size = new System.Drawing.Size(56, 21);
			this.comboPLLCPMode.TabIndex = 57;
			this.comboPLLCPMode.SelectedIndexChanged += new System.EventHandler(this.comboPLLCPMode_SelectedIndexChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(128, 53);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(56, 23);
			this.label10.TabIndex = 58;
			this.label10.Text = "CP Mode:";
			// 
			// comboPLLStatusMux
			// 
			this.comboPLLStatusMux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPLLStatusMux.DropDownWidth = 224;
			this.comboPLLStatusMux.Items.AddRange(new object[] {
																   "Off",
																   "Digital Lock Detect (Active High)",
																   "N Divider Output",
																   "Digital Lock Detect (Active Low)",
																   "R Divider Output",
																   "Analog Lock Detect (N Channel, Open-Drain)",
																   "A Counter Output",
																   "Prescaler Output (NCLK)",
																   "PFD Up Pulse",
																   "PFD Down Pulse",
																   "Loss-of-Reference (Active High)",
																   "Tri-State",
																   "Analog Lock Detect (P Channel, Open-Drain)",
																   "Loss-of-Reference or Loss-of-Lock (Active High)",
																   "Loss-of-Reference or Loss-of-Lock (Active Low)",
																   "Loss-of-Reference (Active Low)"});
			this.comboPLLStatusMux.Location = new System.Drawing.Point(16, 80);
			this.comboPLLStatusMux.Name = "comboPLLStatusMux";
			this.comboPLLStatusMux.Size = new System.Drawing.Size(224, 21);
			this.comboPLLStatusMux.TabIndex = 56;
			this.comboPLLStatusMux.SelectedIndexChanged += new System.EventHandler(this.comboPLLStatusMux_SelectedIndexChanged);
			// 
			// chkPLLPollStatus
			// 
			this.chkPLLPollStatus.Location = new System.Drawing.Point(80, 48);
			this.chkPLLPollStatus.Name = "chkPLLPollStatus";
			this.chkPLLPollStatus.Size = new System.Drawing.Size(48, 24);
			this.chkPLLPollStatus.TabIndex = 53;
			this.chkPLLPollStatus.Text = "Poll";
			this.chkPLLPollStatus.CheckedChanged += new System.EventHandler(this.chkPLLPollStatus_CheckedChanged);
			// 
			// comboPLLRefClock
			// 
			this.comboPLLRefClock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPLLRefClock.Items.AddRange(new object[] {
																  "10",
																  "20"});
			this.comboPLLRefClock.Location = new System.Drawing.Point(200, 24);
			this.comboPLLRefClock.Name = "comboPLLRefClock";
			this.comboPLLRefClock.Size = new System.Drawing.Size(40, 21);
			this.comboPLLRefClock.TabIndex = 52;
			this.comboPLLRefClock.SelectedIndexChanged += new System.EventHandler(this.comboPLLRefClock_SelectedIndexChanged);
			// 
			// chkBypassFilter
			// 
			this.chkBypassFilter.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkBypassFilter.Location = new System.Drawing.Point(224, 168);
			this.chkBypassFilter.Name = "chkBypassFilter";
			this.chkBypassFilter.Size = new System.Drawing.Size(80, 24);
			this.chkBypassFilter.TabIndex = 59;
			this.chkBypassFilter.Text = "Bypass Filter";
			this.chkBypassFilter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkBypassFilter.CheckedChanged += new System.EventHandler(this.chkBypassFilter_CheckedChanged);
			// 
			// grpScanner
			// 
			this.grpScanner.Controls.Add(this.chkScannerGo);
			this.grpScanner.Controls.Add(this.udScannerDelay);
			this.grpScanner.Controls.Add(this.lblScannerDelay);
			this.grpScanner.Controls.Add(this.lblScannerStep);
			this.grpScanner.Controls.Add(this.udScannerStep);
			this.grpScanner.Controls.Add(this.lblScannerStop);
			this.grpScanner.Controls.Add(this.udScannerStop);
			this.grpScanner.Controls.Add(this.lblScannerStart);
			this.grpScanner.Controls.Add(this.udScannerStart);
			this.grpScanner.Location = new System.Drawing.Point(456, 168);
			this.grpScanner.Name = "grpScanner";
			this.grpScanner.Size = new System.Drawing.Size(240, 120);
			this.grpScanner.TabIndex = 60;
			this.grpScanner.TabStop = false;
			this.grpScanner.Text = "Scanner";
			// 
			// chkScannerGo
			// 
			this.chkScannerGo.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkScannerGo.Location = new System.Drawing.Point(80, 80);
			this.chkScannerGo.Name = "chkScannerGo";
			this.chkScannerGo.Size = new System.Drawing.Size(80, 24);
			this.chkScannerGo.TabIndex = 8;
			this.chkScannerGo.Text = "Go";
			this.chkScannerGo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkScannerGo.CheckedChanged += new System.EventHandler(this.chkScannerGo_CheckedChanged);
			// 
			// udScannerDelay
			// 
			this.udScannerDelay.Location = new System.Drawing.Point(152, 49);
			this.udScannerDelay.Maximum = new System.Decimal(new int[] {
																		   10000,
																		   0,
																		   0,
																		   0});
			this.udScannerDelay.Minimum = new System.Decimal(new int[] {
																		   20,
																		   0,
																		   0,
																		   0});
			this.udScannerDelay.Name = "udScannerDelay";
			this.udScannerDelay.Size = new System.Drawing.Size(80, 20);
			this.udScannerDelay.TabIndex = 6;
			this.udScannerDelay.Value = new System.Decimal(new int[] {
																		 250,
																		 0,
																		 0,
																		 0});
			// 
			// lblScannerDelay
			// 
			this.lblScannerDelay.Location = new System.Drawing.Point(120, 49);
			this.lblScannerDelay.Name = "lblScannerDelay";
			this.lblScannerDelay.Size = new System.Drawing.Size(40, 23);
			this.lblScannerDelay.TabIndex = 7;
			this.lblScannerDelay.Text = "Delay:";
			// 
			// lblScannerStep
			// 
			this.lblScannerStep.Location = new System.Drawing.Point(8, 49);
			this.lblScannerStep.Name = "lblScannerStep";
			this.lblScannerStep.Size = new System.Drawing.Size(32, 23);
			this.lblScannerStep.TabIndex = 5;
			this.lblScannerStep.Text = "Step:";
			// 
			// udScannerStep
			// 
			this.udScannerStep.DecimalPlaces = 6;
			this.udScannerStep.Increment = new System.Decimal(new int[] {
																			100,
																			0,
																			0,
																			327680});
			this.udScannerStep.Location = new System.Drawing.Point(40, 49);
			this.udScannerStep.Maximum = new System.Decimal(new int[] {
																		  55,
																		  0,
																		  0,
																		  0});
			this.udScannerStep.Name = "udScannerStep";
			this.udScannerStep.Size = new System.Drawing.Size(80, 20);
			this.udScannerStep.TabIndex = 4;
			this.udScannerStep.Value = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		65536});
			// 
			// lblScannerStop
			// 
			this.lblScannerStop.Location = new System.Drawing.Point(120, 24);
			this.lblScannerStop.Name = "lblScannerStop";
			this.lblScannerStop.Size = new System.Drawing.Size(32, 23);
			this.lblScannerStop.TabIndex = 3;
			this.lblScannerStop.Text = "Stop:";
			// 
			// udScannerStop
			// 
			this.udScannerStop.DecimalPlaces = 6;
			this.udScannerStop.Location = new System.Drawing.Point(152, 24);
			this.udScannerStop.Maximum = new System.Decimal(new int[] {
																		  55,
																		  0,
																		  0,
																		  0});
			this.udScannerStop.Name = "udScannerStop";
			this.udScannerStop.Size = new System.Drawing.Size(80, 20);
			this.udScannerStop.TabIndex = 2;
			this.udScannerStop.Value = new System.Decimal(new int[] {
																		55,
																		0,
																		0,
																		0});
			// 
			// lblScannerStart
			// 
			this.lblScannerStart.Location = new System.Drawing.Point(8, 24);
			this.lblScannerStart.Name = "lblScannerStart";
			this.lblScannerStart.Size = new System.Drawing.Size(32, 23);
			this.lblScannerStart.TabIndex = 1;
			this.lblScannerStart.Text = "Start:";
			// 
			// udScannerStart
			// 
			this.udScannerStart.DecimalPlaces = 6;
			this.udScannerStart.Location = new System.Drawing.Point(40, 24);
			this.udScannerStart.Maximum = new System.Decimal(new int[] {
																		   55,
																		   0,
																		   0,
																		   0});
			this.udScannerStart.Name = "udScannerStart";
			this.udScannerStart.Size = new System.Drawing.Size(80, 20);
			this.udScannerStart.TabIndex = 0;
			this.udScannerStart.Value = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 0});
			// 
			// lblIFFreq
			// 
			this.lblIFFreq.Location = new System.Drawing.Point(488, 304);
			this.lblIFFreq.Name = "lblIFFreq";
			this.lblIFFreq.Size = new System.Drawing.Size(48, 23);
			this.lblIFFreq.TabIndex = 62;
			this.lblIFFreq.Text = "IF Freq:";
			// 
			// udIFFreq
			// 
			this.udIFFreq.DecimalPlaces = 6;
			this.udIFFreq.Increment = new System.Decimal(new int[] {
																	   100,
																	   0,
																	   0,
																	   327680});
			this.udIFFreq.Location = new System.Drawing.Point(536, 304);
			this.udIFFreq.Maximum = new System.Decimal(new int[] {
																	 96,
																	 0,
																	 0,
																	 196608});
			this.udIFFreq.Minimum = new System.Decimal(new int[] {
																	 96,
																	 0,
																	 0,
																	 -2147287040});
			this.udIFFreq.Name = "udIFFreq";
			this.udIFFreq.Size = new System.Drawing.Size(80, 20);
			this.udIFFreq.TabIndex = 61;
			this.udIFFreq.Value = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   196608});
			this.udIFFreq.ValueChanged += new System.EventHandler(this.udIFFreq_ValueChanged);
			// 
			// FWCForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(704, 470);
			this.Controls.Add(this.lblIFFreq);
			this.Controls.Add(this.udIFFreq);
			this.Controls.Add(this.grpScanner);
			this.Controls.Add(this.chkBypassFilter);
			this.Controls.Add(this.grpPLL);
			this.Controls.Add(this.lblSetPort);
			this.Controls.Add(this.udSetPort);
			this.Controls.Add(this.lblDDSAmp);
			this.Controls.Add(this.udOffset);
			this.Controls.Add(this.chkImpulse);
			this.Controls.Add(this.chkDDSMaster);
			this.Controls.Add(this.chkPowerDown2);
			this.Controls.Add(this.chkPowerDown1);
			this.Controls.Add(this.chkEnableQSD);
			this.Controls.Add(this.grpADC);
			this.Controls.Add(this.chkSigGenRelay);
			this.Controls.Add(this.chkTestRelay);
			this.Controls.Add(this.chkPIOChipSelect);
			this.Controls.Add(this.grpGPIODDR);
			this.Controls.Add(this.grpGPIO);
			this.Controls.Add(this.grpClockGen);
			this.Controls.Add(this.grpDDS);
			this.Controls.Add(this.chkLNA);
			this.Controls.Add(this.grpATT);
			this.Controls.Add(this.chkLED);
			this.Controls.Add(this.lblSlot);
			this.Controls.Add(this.udSlot);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FWCForm";
			this.Text = "FWC";
			((System.ComponentModel.ISupportInitialize)(this.udSlot)).EndInit();
			this.grpATT.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udATTVal)).EndInit();
			this.grpClockGen.ResumeLayout(false);
			this.grpDDS.ResumeLayout(false);
			this.grpGPIO.ResumeLayout(false);
			this.grpGPIODDR.ResumeLayout(false);
			this.grpADC.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSetPort)).EndInit();
			this.grpPLL.ResumeLayout(false);
			this.grpScanner.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udScannerDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udScannerStep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udScannerStop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udScannerStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udIFFreq)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Event Handlers

		private void chkLED_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetLED(console.fwc_index, chkLED.Checked);
		}

		private void chkLNA_CheckedChanged(object sender, System.EventArgs e)
		{
			console.FWCPreampOn = chkLNA.Checked;
		}

		private void chkATTOn_CheckedChanged(object sender, System.EventArgs e)
		{
			console.FWCATTOn = chkATTOn.Checked;
		}

		private void udATTVal_ValueChanged(object sender, System.EventArgs e)
		{
			console.FWCATTVal = (int)udATTVal.Value;
		}

		private void comboClockGenReg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(comboClockGenReg.Text != "")
				btnClockGenRead_Click(this, EventArgs.Empty);
		}

		private void btnClockGenRead_Click(object sender, System.EventArgs e)
		{
			if(comboClockGenReg.Text == "") return;
			txtClockGenReadVal.BackColor = Color.Red;
			Application.DoEvents();
			int reg = int.Parse(comboClockGenReg.Text, System.Globalization.NumberStyles.HexNumber);
			uint data = 0;
			if(FWC.ReadClockGen(console.fwc_index, reg, out data) != 0)
			{
				MessageBox.Show("Error in ClockGen Read.");
				console.fwc_init = false;
			}
			
			txtClockGenReadVal.BackColor = SystemColors.Control;
			txtClockGenReadVal.Text = String.Format("{0:X2}", data);
		}

		private void btnClockGenWrite_Click(object sender, System.EventArgs e)
		{
			if(comboClockGenReg.Text == "") return;
			if(txtClockGenWriteVal.Text == "") return;
			txtClockGenWriteVal.BackColor = Color.Red;
			Application.DoEvents();
			int reg = int.Parse(comboClockGenReg.Text, System.Globalization.NumberStyles.HexNumber);
			uint val = uint.Parse(txtClockGenWriteVal.Text, System.Globalization.NumberStyles.HexNumber);
			
			if(FWC.WriteClockGen(console.fwc_index, reg, val) != 0)
				MessageBox.Show("Error in ClockGen Write.");

			txtClockGenWriteVal.BackColor = SystemColors.Window;
			btnClockGenRead_Click(this, EventArgs.Empty);
		}

		private void comboDDSReg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(comboDDSReg.Text != "" && comboDDSChan.Text != "")
				btnDDSRead_Click(this, EventArgs.Empty);
		}

		private void btnDDSRead_Click(object sender, System.EventArgs e)
		{
			if(comboDDSReg.Text == "") return;
			if(comboDDSChan.Text == "") return;
			txtDDSReadVal.BackColor = Color.Red;
			Application.DoEvents();
			int chan = int.Parse(comboDDSChan.Text);
			int reg = int.Parse(comboDDSReg.Text, System.Globalization.NumberStyles.HexNumber);
			uint data = 0;
			if(FWC.ReadDDS(console.fwc_index, chan, reg, out data) != 0)
			{
				MessageBox.Show("Error in DDS Read.");
				console.fwc_init = false;
			}
			
			txtDDSReadVal.BackColor = SystemColors.Control;
			txtDDSReadVal.Text = String.Format("{0:X8}", data);
		}

		private void comboDDSChan_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(comboDDSReg.Text != "" && comboDDSChan.Text != "")
				btnDDSRead_Click(this, EventArgs.Empty);		
		}

		private void btnDDSWrite_Click(object sender, System.EventArgs e)
		{
			if(comboDDSReg.Text == "") return;
			if(txtDDSWrite.Text == "") return;
			txtDDSWrite.BackColor = Color.Red;
			Application.DoEvents();
			int chan = int.Parse(comboDDSChan.Text);
			int reg = int.Parse(comboDDSReg.Text, System.Globalization.NumberStyles.HexNumber);
			uint val = uint.Parse(txtDDSWrite.Text, System.Globalization.NumberStyles.HexNumber);
			
			if(FWC.WriteDDS(console.fwc_index, chan, reg, val) != 0)
				MessageBox.Show("Error in DDS Write.");

			txtDDSWrite.BackColor = SystemColors.Window;
			btnDDSRead_Click(this, EventArgs.Empty);
		}

		private void btnGPIORead_Click(object sender, System.EventArgs e)
		{
			txtGPIOReadVal.BackColor = Color.Red;
			Application.DoEvents();
			uint data;
			if(FWC.GPIORead(out data) != 0)
				MessageBox.Show("Error in GPIO Read.");

			txtGPIOReadVal.BackColor = SystemColors.Window;
			txtGPIOReadVal.Text = String.Format("{0:X2}", data);
		}

		private void btnGPIOWriteVal_Click(object sender, System.EventArgs e)
		{
			if(txtGPIOWriteVal.Text == "") return;
			txtGPIOWriteVal.BackColor = Color.Red;
			Application.DoEvents();

			uint val = uint.Parse(txtGPIOWriteVal.Text, System.Globalization.NumberStyles.HexNumber);

			if(FWC.GPIOWrite(val) != 0)
				MessageBox.Show("Error in GPIO Write.");

			txtGPIOWriteVal.BackColor = SystemColors.Window;
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIODDR_CheckedChanged(object sender, System.EventArgs e)
		{
			CheckBox c = (CheckBox)sender;
			int index = int.Parse(c.Name.Substring(10, 1));

			uint val;
			FWC.GPIODDRRead(out val);
			if(c.Checked)
				val |= ((uint)1<<index);
			else
				val &= (0xFFFF - (uint)1<<index);

			FWC.GPIODDRWrite(val);
			btnGPIODDRRead_Click(this, EventArgs.Empty);
		}

		private void btnGPIODDRRead_Click(object sender, System.EventArgs e)
		{
			txtGPIODDRReadVal.BackColor = Color.Red;
			Application.DoEvents();
			uint data;
			if(FWC.GPIODDRRead(out data) != 0)
				MessageBox.Show("Error in GPIO DDR Read.");

			txtGPIODDRReadVal.BackColor = SystemColors.Window;
			txtGPIODDRReadVal.Text = String.Format("{0:X2}", data);
		}

		private void btnGPIODDRWrite_Click(object sender, System.EventArgs e)
		{
			if(txtGPIODDRWriteVal.Text == "") return;
			txtGPIODDRWriteVal.BackColor = Color.Red;
			Application.DoEvents();

			uint val = uint.Parse(txtGPIODDRWriteVal.Text, System.Globalization.NumberStyles.HexNumber);

			if(FWC.GPIODDRWrite(0xFF00 | val) != 0)
				MessageBox.Show("Error in GPIO Write.");

			txtGPIODDRWriteVal.BackColor = SystemColors.Window;
			btnGPIODDRRead_Click(this, EventArgs.Empty);
		}

		private void chkClockGenChipSelect_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.I2C_WriteValue(0x40, 0x3);
			uint val;
			FWC.I2C_ReadValue(0x40, out val);

			if(!chkClockGenChipSelect.Checked)
				FWC.I2C_Write2Value(0x40, 0x3, (byte)(val | 0x01));
			else
				FWC.I2C_Write2Value(0x40, 0x3, (byte)(val & 0xFE));
		}

		private void chkDDSChipSelect_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.I2C_WriteValue(0x40, 0x3);
			uint val;
			FWC.I2C_ReadValue(0x40, out val);

			if(!chkDDSChipSelect.Checked)
				FWC.I2C_Write2Value(0x40, 0x3, (byte)(val | 0x02));
			else
				FWC.I2C_Write2Value(0x40, 0x3, (byte)(val & 0xFD));		
		}

		private void chkPIOChipSelect_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.I2C_WriteValue(0x40, 0x2);
			uint val;
			FWC.I2C_ReadValue(0x40, out val);

			if(!chkPIOChipSelect.Checked)
				FWC.I2C_Write2Value(0x40, 0x2, (byte)(val | 0x80));
			else
				FWC.I2C_Write2Value(0x40, 0x2, (byte)(val & 0x7F));
		}

		private void btnClockGenReset_Click(object sender, System.EventArgs e)
		{
			FWC.I2C_WriteValue(0x40, 0x3);
			uint val;
			FWC.I2C_ReadValue(0x40, out val);

			FWC.I2C_Write2Value(0x40, 0x3, (byte)(val & 0xF7));
			FWC.I2C_Write2Value(0x40, 0x3, (byte)(val | 0x08));
		}

		private void btnDDSReset_Click(object sender, System.EventArgs e)
		{
			FWC.I2C_WriteValue(0x40, 0x3);
			uint val;
			FWC.I2C_ReadValue(0x40, out val);

			FWC.I2C_Write2Value(0x40, 0x3, (byte)(val | 0x10));
			FWC.I2C_Write2Value(0x40, 0x3, (byte)val);
		}

		private void chkGPIO0_CheckedChanged(object sender, System.EventArgs e)
		{		
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO0.Checked)
				val |= 0x01;
			else
				val &= 0xFE;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO1_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO1.Checked)
				val |= 0x02;
			else
				val &= 0xFD;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO2_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO2.Checked)
				val |= 0x04;
			else
				val &= 0xFB;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO3_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO3.Checked)
				val |= 0x08;
			else
				val &= 0xF7;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO4_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO4.Checked)
				val |= 0x10;
			else
				val &= 0xEF;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO5_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO5.Checked)
				val |= 0x20;
			else
				val &= 0xDF;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO6_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO6.Checked)
				val |= 0x40;
			else
				val &= 0xBF;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void chkGPIO7_CheckedChanged(object sender, System.EventArgs e)
		{
			uint val;
			FWC.GPIORead(out val);
			if(chkGPIO7.Checked)
				val |= 0x80;
			else
				val &= 0x7F;

			FWC.GPIOWrite(val);
			btnGPIORead_Click(this, EventArgs.Empty);
		}

		private void SendBit(int b)
		{
			chkGPIO0.Checked = (b != 0);
			chkGPIO4.Checked = false;
			chkGPIO4.Checked = true;  
		}

		private void SendByte(byte b)
		{
			for(int i=7; i>=0; i--)
				SendBit((int)((b>>i)&1));
		}

		private void btnDDSUpdate_Click(object sender, System.EventArgs e)
		{
			FWC.UpdateDDS();
		}

		private void chkTestRelay_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetTestRelay(console.fwc_index, chkTestRelay.Checked);
		}

		private void chkSigGenRelay_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetSigGenRelay(console.fwc_index, chkSigGenRelay.Checked);
		}

		private void chkADCMaster_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetADCMaster(console.fwc_index, chkADCMaster.Checked);
		}

		private void btnADCCalState_Click(object sender, System.EventArgs e)
		{
			bool b;
			FWC.GetADCCalState(console.fwc_index, out b);
			if(b) btnADCCalState.BackColor = Color.Green;
			else btnADCCalState.BackColor = Color.Red;
		}

		private void chkADCHPF_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetADCHPF(console.fwc_index, chkADCHPF.Checked);
		}

		private void chkADCZCAL_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetADCZCAL(console.fwc_index, chkADCZCAL.Checked);
		}

		private void chkEnableQSD_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetEnableQSD(console.fwc_index, chkEnableQSD.Checked);
		}

		private void chkPowerDown1_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetPowerDown1(console.fwc_index, chkPowerDown1.Checked);
		}

		private void chkPowerDown2_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetPowerDown2(console.fwc_index, chkPowerDown2.Checked);
		}

		#endregion		

		private void btnPLLStatus_Click(object sender, System.EventArgs e)
		{
			bool b;
			FWC.GetPLLStatus(console.fwc_index, out b);
			if(b) btnPLLStatus.BackColor = Color.Green;
			else btnPLLStatus.BackColor = Color.Red;
		}

		private void chkDDSMaster_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetDDSMaster(console.fwc_index, chkDDSMaster.Checked);
		}

		private void chkChargePump_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetPLL(console.fwc_index, chkPLLEnable.Checked);
			if(chkPLLEnable.Checked)
				comboPLLCPMode.Text = "Normal";
			else
				comboPLLCPMode.Text = "Tri-Stated";
		}

		private void chkImpulse_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetImpulse(console.fwc_index, chkImpulse.Checked);
		}

		private void chkADCReset_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetADCReset(console.fwc_index, chkADCReset.Checked);
		}

		private void udOffset_ValueChanged(object sender, System.EventArgs e)
		{
			FWC.SetDDSChan(console.fwc_index, 0x8);
			FWC.SetDDSAmplitude(console.fwc_index, (int)udOffset.Value);
			FWC.SetDDSChan(console.fwc_index, 0x3);
		}

		private void udSetPort_ValueChanged(object sender, System.EventArgs e)
		{
			FWC.SetPort(console.fwc_index, (int)udSetPort.Value);
		}

		private void comboPLLRefClock_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			uint val = 0;
			switch(comboPLLRefClock.Text)
			{
				case "10": val = 0x01; break;
				case "20": val = 0x02; break;
			}
			uint old_val;
			if(FWC.ReadClockGen(console.fwc_index, 0x0C, out old_val) != 0)
				MessageBox.Show("Error in ClockGen Read.");
			if(old_val != val)
			{
				if(FWC.WriteClockGen(console.fwc_index, 0x0C, val) != 0)
					MessageBox.Show("Error in ClockGen Write.");
			}			
		}

		private Thread t;
		private void chkPLLPollStatus_CheckedChanged(object sender, System.EventArgs e)
		{
			btnPLLStatus.Enabled = !chkPLLPollStatus.Checked;

			if(chkPLLPollStatus.Checked)
			{
				t = new Thread(new ThreadStart(PollStatus));
				t.Name = "Poll Status Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();
			}
		}

		private void PollStatus()
		{
			while(chkPLLPollStatus.Checked)
			{
				btnPLLStatus_Click(this, EventArgs.Empty);
				Thread.Sleep(250);
			}
		}

		private void comboPLLStatusMux_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			uint old_val;
			if(FWC.ReadClockGen(console.fwc_index, 0x08, out old_val) != 0)
				MessageBox.Show("Error in ClockGen Write.");
			uint val = (uint)((old_val&0xC3) | (uint)(comboPLLStatusMux.SelectedIndex << 2));
			if(old_val != val)
			{
				if(FWC.WriteClockGen(console.fwc_index, 0x08, val) != 0)
					MessageBox.Show("Error in ClockGen Write.");
			}
		}

		private void chkBypassFilter_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkBypassFilter.Checked)
			{
				chkBypassFilter.BackColor = console.ButtonSelectedColor;
				FWC.SetFilter(console.fwc_index, -1.0f);				
			}
			else
			{
				chkBypassFilter.BackColor = SystemColors.Control;
				FWC.SetFilter(console.fwc_index, (float)console.VFOAFreq);				
			}
		}

		private void chkScannerGo_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkScannerGo.Checked)
			{
				chkScannerGo.BackColor = console.ButtonSelectedColor;
				Thread t = new Thread(new ThreadStart(Scan));
				t.Name = "Scanner Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();
			}
			else chkScannerGo.BackColor = SystemColors.Control;
		}

		private void Scan()
		{
			console.VFOAFreq = (double)udScannerStart.Value;
			while(chkScannerGo.Checked)
			{
				Thread.Sleep((int)udScannerDelay.Value);
				console.VFOAFreq += (double)udScannerStep.Value;
				if(console.VFOAFreq > (double)udScannerStop.Value)
					chkScannerGo.Checked = false;
			}
		}

		private void comboPLLCPMode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			uint old_val;
			if(FWC.ReadClockGen(console.fwc_index, 0x08, out old_val) != 0)
				MessageBox.Show("Error in ClockGen Write.");
			uint val = (uint)((old_val&0xFC) | (uint)comboPLLCPMode.SelectedIndex);
			if(old_val != val)
			{
				if(FWC.WriteClockGen(console.fwc_index, 0x08, val) != 0)
					MessageBox.Show("Error in ClockGen Write.");
			}
		}

		private void chkPLLPFDPol_CheckedChanged(object sender, System.EventArgs e)
		{
			uint old_val;
			if(FWC.ReadClockGen(console.fwc_index, 0x08, out old_val) != 0)
				MessageBox.Show("Error in ClockGen Write.");

			uint val = 0;
			if(chkPLLPFDPol.Checked) val = 0x40;
			val = (uint)((old_val&0xBF) | val);
			if(old_val != val)
			{
				if(FWC.WriteClockGen(console.fwc_index, 0x08, val) != 0)
					MessageBox.Show("Error in ClockGen Write.");
			}
		}

		private void udIFFreq_ValueChanged(object sender, System.EventArgs e)
		{
			console.IFFreq = (double)udIFFreq.Value;
		}
	}
}

//=================================================================
// FLEX5000DebugForm.cs
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
	public class FLEX5000DebugForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private FLEX5000LLHWForm flex5000LLHWForm;
		bool trx_ok = true;
		bool pa_ok = true;
		bool rfio_ok = true;
		//bool rx2_ok = true;

		private System.Windows.Forms.NumericUpDownTS udFreq1;
		private System.Windows.Forms.NumericUpDownTS udFreq2;
		private System.Windows.Forms.GroupBoxTS grpFreqControl;
		private System.Windows.Forms.LabelTS lblRX1;
		private System.Windows.Forms.LabelTS lblTXGen;
		private System.Windows.Forms.CheckBoxTS chkImpulse;
		private System.Windows.Forms.CheckBoxTS chkQSD;
		private System.Windows.Forms.CheckBoxTS chkXREF;
		private System.Windows.Forms.CheckBoxTS chkIntSpkr;
		private System.Windows.Forms.LabelTS label1;
		private System.Windows.Forms.LabelTS label2;
		private System.Windows.Forms.LabelTS label3;
		private System.Windows.Forms.LabelTS label4;
		private System.Windows.Forms.GroupBoxTS grpSwitches;
		private System.Windows.Forms.GroupBoxTS grpKey;
		private System.Windows.Forms.CheckBoxTS chkDot;
		private System.Windows.Forms.CheckBoxTS chkDash;
		private System.Windows.Forms.CheckBoxTS chkHeadphone;
		private System.Windows.Forms.CheckBoxTS chkRCATX1;
		private System.Windows.Forms.CheckBoxTS chkRCATX2;
		private System.Windows.Forms.CheckBoxTS chkRCATX3;
		private System.Windows.Forms.CheckBoxTS chkFan;
		private System.Windows.Forms.GroupBoxTS grpPLL;
		private System.Windows.Forms.LabelTS lblPLLClock;
		private System.Windows.Forms.LabelTS label10;
		private System.Windows.Forms.ComboBox comboPLLStatusMux;
		private System.Windows.Forms.ButtonTS btnPLLStatus;
		private System.Windows.Forms.GroupBoxTS grpScanner;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.NumericUpDownTS udScanDelayA;
		private System.Windows.Forms.LabelTS lblScanDelayA;
		private System.Windows.Forms.LabelTS lblScanStepA;
		private System.Windows.Forms.NumericUpDownTS udScanStepA;
		private System.Windows.Forms.LabelTS lblScanStopA;
		private System.Windows.Forms.NumericUpDownTS udScanStopA;
		private System.Windows.Forms.LabelTS lblScanStartA;
		private System.Windows.Forms.NumericUpDownTS udScanStartA;
		private System.Windows.Forms.NumericUpDownTS udScanDelayB;
		private System.Windows.Forms.LabelTS lblScanDelayB;
		private System.Windows.Forms.LabelTS lblScanStepB;
		private System.Windows.Forms.NumericUpDownTS udScanStepB;
		private System.Windows.Forms.LabelTS lblScanStopB;
		private System.Windows.Forms.NumericUpDownTS udScanStopB;
		private System.Windows.Forms.LabelTS lblScanStartB;
		private System.Windows.Forms.NumericUpDownTS udScanStartB;
		private System.Windows.Forms.CheckBoxTS chkRCAPTT;
		private System.Windows.Forms.CheckBoxTS chkMicPTT;
		private System.Windows.Forms.CheckBoxTS chkXVTXEN;
		private System.Windows.Forms.TrackBarTS tbTRXPot0;
		private System.Windows.Forms.NumericUpDownTS udTRXPot0;
		private System.Windows.Forms.GroupBoxTS grpTRXPots;
		private System.Windows.Forms.NumericUpDownTS udTRXPot3;
		private System.Windows.Forms.TrackBarTS tbTRXPot3;
		private System.Windows.Forms.NumericUpDownTS udTRXPot2;
		private System.Windows.Forms.TrackBarTS tbTRXPot2;
		private System.Windows.Forms.NumericUpDownTS udTRXPot1;
		private System.Windows.Forms.TrackBarTS tbTRXPot1;
		private System.Windows.Forms.NumericUpDownTS udPAPot3;
		private System.Windows.Forms.TrackBarTS tbPAPot3;
		private System.Windows.Forms.NumericUpDownTS udPAPot0;
		private System.Windows.Forms.TrackBarTS tbPAPot0;
		private System.Windows.Forms.CheckBoxTS chkFPLED;
		private System.Windows.Forms.CheckBoxTS chkCTS;
		private System.Windows.Forms.CheckBoxTS chkRTS;
		private System.Windows.Forms.CheckBoxTS chkReset;
		private System.Windows.Forms.CheckBoxTS chkPCPwr;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBoxTS chkXVCOM;
		private System.Windows.Forms.CheckBoxTS chkEN2M;
		private System.Windows.Forms.CheckBoxTS chkKEY2M;
		private System.Windows.Forms.CheckBoxTS chkXVTR;
		private System.Windows.Forms.CheckBoxTS ckPABias;
		private System.Windows.Forms.CheckBoxTS chkIntLED;
		private System.Windows.Forms.NumericUpDownTS udPAPot1;
		private System.Windows.Forms.TrackBarTS tbPAPot1;
		private System.Windows.Forms.NumericUpDownTS udPAPot2;
		private System.Windows.Forms.TrackBarTS tbPAPot2;
		private System.Windows.Forms.TrackBarTS tbPAPot4;
		private System.Windows.Forms.NumericUpDownTS udPAPot7;
		private System.Windows.Forms.TrackBarTS tbPAPot7;
		private System.Windows.Forms.NumericUpDownTS udPAPot5;
		private System.Windows.Forms.TrackBarTS tbPAPot5;
		private System.Windows.Forms.NumericUpDownTS udPAPot6;
		private System.Windows.Forms.TrackBarTS tbPAPot6;
		private System.Windows.Forms.NumericUpDownTS udPAPot4;
		private System.Windows.Forms.CheckBoxTS ckTest;
		private System.Windows.Forms.CheckBoxTS ckGen;
		private System.Windows.Forms.CheckBoxTS ckSig;
		private System.Windows.Forms.CheckBoxTS ckXVEN;
		private System.Windows.Forms.CheckBoxTS ckQSE;
		private System.Windows.Forms.CheckBoxTS ckTXMon;
		private System.Windows.Forms.CheckBoxTS ckTR;
		private System.Windows.Forms.GroupBoxTS grpDriverBias;
		private System.Windows.Forms.GroupBoxTS grpFinalBias;
		private System.Windows.Forms.CheckBoxTS ckPAFilter6m;
		private System.Windows.Forms.CheckBoxTS ckTXFilterBypass;
		private System.Windows.Forms.CheckBoxTS ckRX1FilterBypass;
		private System.Windows.Forms.GroupBoxTS grpADC;
		private System.Windows.Forms.TextBox txtADCRead;
		private System.Windows.Forms.ButtonTS btnADCRead;
		private System.Windows.Forms.ComboBoxTS comboADCChan;
		private System.Windows.Forms.LabelTS label18;
		private System.Windows.Forms.CheckBoxTS ckADCPoll;
		private System.Windows.Forms.CheckBoxTS ckKeyPoll;
		private System.Windows.Forms.CheckBoxTS ckPLLPollStatus;
		private System.Windows.Forms.GroupBox grpTap;
		private System.Windows.Forms.RadioButton radTapOff;
		private System.Windows.Forms.RadioButton radTapFinal;
		private System.Windows.Forms.RadioButton radTapPreDriver;
		private System.Windows.Forms.CheckBoxTS chkRX1Out;
		private System.Windows.Forms.NumericUpDownTS udTXTrace;
		private System.Windows.Forms.GroupBox grpTXTrace;
		private System.Windows.Forms.CheckBoxTS ckPLL;
		private System.Windows.Forms.CheckBoxTS ckPLLPFDPol;
		private System.Windows.Forms.ComboBox cmboPLLCPMode;
		private System.Windows.Forms.ComboBox cmboPLLRefClock;
		private System.Windows.Forms.CheckBoxTS ckTXTrace;
		private System.Windows.Forms.CheckBoxTS ckScanB;
		private System.Windows.Forms.CheckBoxTS ckScanA;
		private System.Windows.Forms.CheckBoxTS chkRX1Tap;
		private System.Windows.Forms.CheckBoxTS chkRX2On;
		private System.Windows.Forms.CheckBoxTS ckPAOff;
		private System.Windows.Forms.CheckBoxTS ckRX2FilterBypass;
		private System.Windows.Forms.GroupBox grpChanSel;
		private System.Windows.Forms.LabelTS lblTXR;
		private System.Windows.Forms.ComboBox comboTXR;
		private System.Windows.Forms.LabelTS lblTXL;
		private System.Windows.Forms.ComboBox comboTXL;
		private System.Windows.Forms.LabelTS lblRXR;
		private System.Windows.Forms.ComboBox comboRXR;
		private System.Windows.Forms.LabelTS lblRXL;
		private System.Windows.Forms.ComboBox comboRXL;
		private System.Windows.Forms.CheckBoxTS chkRXAttn;
		private System.Windows.Forms.CheckBoxTS chkPDrvMon;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.CheckBoxTS ckTXOut;
		private System.Windows.Forms.LabelTS lblQ3Fine;
		private System.Windows.Forms.LabelTS lblQ2Fine;
		private System.Windows.Forms.LabelTS lblQ2Coarse;
		private System.Windows.Forms.LabelTS lblQ3Coarse;
		private System.Windows.Forms.LabelTS lblQ1Fine;
		private System.Windows.Forms.LabelTS lblQ4Coarse;
		private System.Windows.Forms.LabelTS lblQ4Fine;
		private System.Windows.Forms.LabelTS lblQ1Coarse;
        private System.Windows.Forms.CheckBoxTS ckPreamp;

		#endregion
        private MenuItem menuItem3;
        private IContainer components;

		#region Constructor and Destructor

		public FLEX5000DebugForm(Console c)
		{
			InitializeComponent();
			console = c;

			trx_ok = FWCEEPROM.TRXOK;
			pa_ok = FWCEEPROM.PAOK;
			rfio_ok = FWCEEPROM.RFIOOK;
			//rx2_ok = FWCEEPROM.RX2OK;

			grpDriverBias.Enabled = pa_ok;
			grpFinalBias.Enabled = pa_ok;
			ckPABias.Enabled = pa_ok;
			ckPAOff.Enabled = pa_ok;
			chkCTS.Enabled = pa_ok;
			chkRTS.Enabled = pa_ok;
            chkReset.Enabled = pa_ok;
			chkPCPwr.Enabled = pa_ok;
			grpADC.Enabled = pa_ok;

			bool error = !trx_ok || !pa_ok;
			if(console.CurrentModel == Model.FLEX5000)
				error |= !rfio_ok;
			if(error /*|| !rx2_ok*/)
			{
				string s = "";
				if(!trx_ok) s += "TRX: Error or not present\n";
				if(!pa_ok) s += "PA: Error or not present\n";
				if(!rfio_ok) s += "RFIO: Error or not present\n";
				//if(!rx2_ok) s += "RX2: Error or not present\n";
				MessageBox.Show(s);
			}

			Common.RestoreForm(this, "FLEX5000DebugForm", false);

			cmboPLLRefClock.Text = "10";
			cmboPLLCPMode.Text = "Normal";
			comboPLLStatusMux.SelectedIndex = 0;
			comboADCChan.SelectedIndex = 0;

			if(console.CurrentModel == Model.FLEX3000)
			{
				grpChanSel.Visible = true;
				grpChanSel.BringToFront();
				menuItem2.Visible = true;

				comboADCChan.Items.Clear();
				comboADCChan.Items.Add("Final Bias");
				comboADCChan.Items.Add("Driver Bias");
				comboADCChan.Items.Add("13.8V Ref");
				comboADCChan.Items.Add("Temp");
				comboADCChan.Items.Add("Rev Pow");
				comboADCChan.Items.Add("Fwd Pow");
				comboADCChan.Items.Add("Phase");
				comboADCChan.Items.Add("Mag");
				comboADCChan.SelectedIndex = 0;

				lblQ1Coarse.Text = "Q2 Coarse:";
				lblQ1Fine.Text = "Q2 Fine:";
				lblQ2Coarse.Text = "Q3 Coarse:";
				lblQ2Fine.Text = "Q3 Fine:";
				lblQ3Coarse.Text = "Q4 Coarse:";
				lblQ3Fine.Text = "Q4 Fine:";
				lblQ4Coarse.Text = "Q5 Coarse:";
				lblQ4Fine.Text = "Q5 Fine:";
			}

            if(console.CurrentModel == Model.FLEX5000 &&
                FWCEEPROM.VUOK)
                menuItem3.Visible = true; // change to only if 5K and VU present
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLEX5000DebugForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.grpTap = new System.Windows.Forms.GroupBox();
            this.radTapOff = new System.Windows.Forms.RadioButton();
            this.radTapFinal = new System.Windows.Forms.RadioButton();
            this.radTapPreDriver = new System.Windows.Forms.RadioButton();
            this.grpTXTrace = new System.Windows.Forms.GroupBox();
            this.grpChanSel = new System.Windows.Forms.GroupBox();
            this.comboTXR = new System.Windows.Forms.ComboBox();
            this.comboTXL = new System.Windows.Forms.ComboBox();
            this.comboRXR = new System.Windows.Forms.ComboBox();
            this.comboRXL = new System.Windows.Forms.ComboBox();
            this.ckPreamp = new System.Windows.Forms.CheckBoxTS();
            this.ckTXOut = new System.Windows.Forms.CheckBoxTS();
            this.chkRXAttn = new System.Windows.Forms.CheckBoxTS();
            this.chkPDrvMon = new System.Windows.Forms.CheckBoxTS();
            this.lblTXR = new System.Windows.Forms.LabelTS();
            this.lblTXL = new System.Windows.Forms.LabelTS();
            this.lblRXR = new System.Windows.Forms.LabelTS();
            this.lblRXL = new System.Windows.Forms.LabelTS();
            this.ckRX2FilterBypass = new System.Windows.Forms.CheckBoxTS();
            this.chkRX2On = new System.Windows.Forms.CheckBoxTS();
            this.ckTXTrace = new System.Windows.Forms.CheckBoxTS();
            this.udTXTrace = new System.Windows.Forms.NumericUpDownTS();
            this.grpADC = new System.Windows.Forms.GroupBoxTS();
            this.ckADCPoll = new System.Windows.Forms.CheckBoxTS();
            this.txtADCRead = new System.Windows.Forms.TextBox();
            this.btnADCRead = new System.Windows.Forms.ButtonTS();
            this.comboADCChan = new System.Windows.Forms.ComboBoxTS();
            this.label18 = new System.Windows.Forms.LabelTS();
            this.grpFinalBias = new System.Windows.Forms.GroupBoxTS();
            this.tbPAPot4 = new System.Windows.Forms.TrackBarTS();
            this.lblQ1Fine = new System.Windows.Forms.LabelTS();
            this.lblQ4Coarse = new System.Windows.Forms.LabelTS();
            this.udPAPot7 = new System.Windows.Forms.NumericUpDownTS();
            this.tbPAPot7 = new System.Windows.Forms.TrackBarTS();
            this.udPAPot4 = new System.Windows.Forms.NumericUpDownTS();
            this.udPAPot5 = new System.Windows.Forms.NumericUpDownTS();
            this.lblQ4Fine = new System.Windows.Forms.LabelTS();
            this.tbPAPot5 = new System.Windows.Forms.TrackBarTS();
            this.tbPAPot6 = new System.Windows.Forms.TrackBarTS();
            this.udPAPot6 = new System.Windows.Forms.NumericUpDownTS();
            this.lblQ1Coarse = new System.Windows.Forms.LabelTS();
            this.grpDriverBias = new System.Windows.Forms.GroupBoxTS();
            this.tbPAPot0 = new System.Windows.Forms.TrackBarTS();
            this.lblQ3Fine = new System.Windows.Forms.LabelTS();
            this.lblQ2Fine = new System.Windows.Forms.LabelTS();
            this.lblQ2Coarse = new System.Windows.Forms.LabelTS();
            this.udPAPot3 = new System.Windows.Forms.NumericUpDownTS();
            this.tbPAPot3 = new System.Windows.Forms.TrackBarTS();
            this.udPAPot1 = new System.Windows.Forms.NumericUpDownTS();
            this.tbPAPot1 = new System.Windows.Forms.TrackBarTS();
            this.udPAPot0 = new System.Windows.Forms.NumericUpDownTS();
            this.tbPAPot2 = new System.Windows.Forms.TrackBarTS();
            this.udPAPot2 = new System.Windows.Forms.NumericUpDownTS();
            this.lblQ3Coarse = new System.Windows.Forms.LabelTS();
            this.grpScanner = new System.Windows.Forms.GroupBoxTS();
            this.ckScanB = new System.Windows.Forms.CheckBoxTS();
            this.udScanDelayB = new System.Windows.Forms.NumericUpDownTS();
            this.lblScanDelayB = new System.Windows.Forms.LabelTS();
            this.lblScanStepB = new System.Windows.Forms.LabelTS();
            this.udScanStepB = new System.Windows.Forms.NumericUpDownTS();
            this.lblScanStopB = new System.Windows.Forms.LabelTS();
            this.udScanStopB = new System.Windows.Forms.NumericUpDownTS();
            this.lblScanStartB = new System.Windows.Forms.LabelTS();
            this.udScanStartB = new System.Windows.Forms.NumericUpDownTS();
            this.ckScanA = new System.Windows.Forms.CheckBoxTS();
            this.udScanDelayA = new System.Windows.Forms.NumericUpDownTS();
            this.lblScanDelayA = new System.Windows.Forms.LabelTS();
            this.lblScanStepA = new System.Windows.Forms.LabelTS();
            this.udScanStepA = new System.Windows.Forms.NumericUpDownTS();
            this.lblScanStopA = new System.Windows.Forms.LabelTS();
            this.udScanStopA = new System.Windows.Forms.NumericUpDownTS();
            this.lblScanStartA = new System.Windows.Forms.LabelTS();
            this.udScanStartA = new System.Windows.Forms.NumericUpDownTS();
            this.grpPLL = new System.Windows.Forms.GroupBoxTS();
            this.lblPLLClock = new System.Windows.Forms.LabelTS();
            this.ckPLLPFDPol = new System.Windows.Forms.CheckBoxTS();
            this.cmboPLLCPMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.LabelTS();
            this.comboPLLStatusMux = new System.Windows.Forms.ComboBox();
            this.ckPLLPollStatus = new System.Windows.Forms.CheckBoxTS();
            this.cmboPLLRefClock = new System.Windows.Forms.ComboBox();
            this.btnPLLStatus = new System.Windows.Forms.ButtonTS();
            this.ckPLL = new System.Windows.Forms.CheckBoxTS();
            this.grpKey = new System.Windows.Forms.GroupBoxTS();
            this.chkRCAPTT = new System.Windows.Forms.CheckBoxTS();
            this.chkMicPTT = new System.Windows.Forms.CheckBoxTS();
            this.ckKeyPoll = new System.Windows.Forms.CheckBoxTS();
            this.chkDash = new System.Windows.Forms.CheckBoxTS();
            this.chkDot = new System.Windows.Forms.CheckBoxTS();
            this.grpSwitches = new System.Windows.Forms.GroupBoxTS();
            this.chkIntLED = new System.Windows.Forms.CheckBoxTS();
            this.chkXVTR = new System.Windows.Forms.CheckBoxTS();
            this.button1 = new System.Windows.Forms.Button();
            this.chkPCPwr = new System.Windows.Forms.CheckBoxTS();
            this.chkReset = new System.Windows.Forms.CheckBoxTS();
            this.chkRTS = new System.Windows.Forms.CheckBoxTS();
            this.chkCTS = new System.Windows.Forms.CheckBoxTS();
            this.chkFPLED = new System.Windows.Forms.CheckBoxTS();
            this.ckPAOff = new System.Windows.Forms.CheckBoxTS();
            this.ckPABias = new System.Windows.Forms.CheckBoxTS();
            this.chkXVTXEN = new System.Windows.Forms.CheckBoxTS();
            this.chkKEY2M = new System.Windows.Forms.CheckBoxTS();
            this.chkEN2M = new System.Windows.Forms.CheckBoxTS();
            this.chkXVCOM = new System.Windows.Forms.CheckBoxTS();
            this.ckTXMon = new System.Windows.Forms.CheckBoxTS();
            this.ckTR = new System.Windows.Forms.CheckBoxTS();
            this.chkRX1Out = new System.Windows.Forms.CheckBoxTS();
            this.chkFan = new System.Windows.Forms.CheckBoxTS();
            this.chkRCATX3 = new System.Windows.Forms.CheckBoxTS();
            this.chkRCATX2 = new System.Windows.Forms.CheckBoxTS();
            this.chkRCATX1 = new System.Windows.Forms.CheckBoxTS();
            this.chkHeadphone = new System.Windows.Forms.CheckBoxTS();
            this.chkIntSpkr = new System.Windows.Forms.CheckBoxTS();
            this.chkRX1Tap = new System.Windows.Forms.CheckBoxTS();
            this.chkQSD = new System.Windows.Forms.CheckBoxTS();
            this.ckTest = new System.Windows.Forms.CheckBoxTS();
            this.ckGen = new System.Windows.Forms.CheckBoxTS();
            this.ckSig = new System.Windows.Forms.CheckBoxTS();
            this.ckXVEN = new System.Windows.Forms.CheckBoxTS();
            this.chkXREF = new System.Windows.Forms.CheckBoxTS();
            this.ckQSE = new System.Windows.Forms.CheckBoxTS();
            this.chkImpulse = new System.Windows.Forms.CheckBoxTS();
            this.grpTRXPots = new System.Windows.Forms.GroupBoxTS();
            this.label4 = new System.Windows.Forms.LabelTS();
            this.label3 = new System.Windows.Forms.LabelTS();
            this.label2 = new System.Windows.Forms.LabelTS();
            this.label1 = new System.Windows.Forms.LabelTS();
            this.udTRXPot3 = new System.Windows.Forms.NumericUpDownTS();
            this.tbTRXPot3 = new System.Windows.Forms.TrackBarTS();
            this.udTRXPot2 = new System.Windows.Forms.NumericUpDownTS();
            this.tbTRXPot2 = new System.Windows.Forms.TrackBarTS();
            this.udTRXPot1 = new System.Windows.Forms.NumericUpDownTS();
            this.tbTRXPot1 = new System.Windows.Forms.TrackBarTS();
            this.udTRXPot0 = new System.Windows.Forms.NumericUpDownTS();
            this.tbTRXPot0 = new System.Windows.Forms.TrackBarTS();
            this.grpFreqControl = new System.Windows.Forms.GroupBoxTS();
            this.ckPAFilter6m = new System.Windows.Forms.CheckBoxTS();
            this.ckTXFilterBypass = new System.Windows.Forms.CheckBoxTS();
            this.ckRX1FilterBypass = new System.Windows.Forms.CheckBoxTS();
            this.lblTXGen = new System.Windows.Forms.LabelTS();
            this.udFreq2 = new System.Windows.Forms.NumericUpDownTS();
            this.lblRX1 = new System.Windows.Forms.LabelTS();
            this.udFreq1 = new System.Windows.Forms.NumericUpDownTS();
            this.grpTap.SuspendLayout();
            this.grpTXTrace.SuspendLayout();
            this.grpChanSel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTXTrace)).BeginInit();
            this.grpADC.SuspendLayout();
            this.grpFinalBias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot6)).BeginInit();
            this.grpDriverBias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot2)).BeginInit();
            this.grpScanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udScanDelayB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStepB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStopB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStartB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanDelayA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStepA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStopA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStartA)).BeginInit();
            this.grpPLL.SuspendLayout();
            this.grpKey.SuspendLayout();
            this.grpSwitches.SuspendLayout();
            this.grpTRXPots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot0)).BeginInit();
            this.grpFreqControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Low Level Hardware Control";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "3000";
            this.menuItem2.Visible = false;
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "VU";
            this.menuItem3.Visible = false;
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // grpTap
            // 
            this.grpTap.Controls.Add(this.radTapOff);
            this.grpTap.Controls.Add(this.radTapFinal);
            this.grpTap.Controls.Add(this.radTapPreDriver);
            this.grpTap.Location = new System.Drawing.Point(448, 328);
            this.grpTap.Name = "grpTap";
            this.grpTap.Size = new System.Drawing.Size(136, 104);
            this.grpTap.TabIndex = 67;
            this.grpTap.TabStop = false;
            this.grpTap.Text = "Full Duplex RX Tap";
            // 
            // radTapOff
            // 
            this.radTapOff.Checked = true;
            this.radTapOff.Location = new System.Drawing.Point(16, 72);
            this.radTapOff.Name = "radTapOff";
            this.radTapOff.Size = new System.Drawing.Size(112, 24);
            this.radTapOff.TabIndex = 3;
            this.radTapOff.TabStop = true;
            this.radTapOff.Text = "Off";
            this.radTapOff.CheckedChanged += new System.EventHandler(this.radTapOff_CheckedChanged);
            // 
            // radTapFinal
            // 
            this.radTapFinal.Location = new System.Drawing.Point(16, 48);
            this.radTapFinal.Name = "radTapFinal";
            this.radTapFinal.Size = new System.Drawing.Size(112, 24);
            this.radTapFinal.TabIndex = 2;
            this.radTapFinal.Text = "Finals (PA)";
            this.radTapFinal.CheckedChanged += new System.EventHandler(this.radTapFinal_CheckedChanged);
            // 
            // radTapPreDriver
            // 
            this.radTapPreDriver.Location = new System.Drawing.Point(16, 24);
            this.radTapPreDriver.Name = "radTapPreDriver";
            this.radTapPreDriver.Size = new System.Drawing.Size(112, 24);
            this.radTapPreDriver.TabIndex = 0;
            this.radTapPreDriver.Text = "Pre-Driver (QSE)";
            this.radTapPreDriver.CheckedChanged += new System.EventHandler(this.radTapPreDriver_CheckedChanged);
            // 
            // grpTXTrace
            // 
            this.grpTXTrace.Controls.Add(this.ckTXTrace);
            this.grpTXTrace.Controls.Add(this.udTXTrace);
            this.grpTXTrace.Location = new System.Drawing.Point(592, 328);
            this.grpTXTrace.Name = "grpTXTrace";
            this.grpTXTrace.Size = new System.Drawing.Size(96, 104);
            this.grpTXTrace.TabIndex = 70;
            this.grpTXTrace.TabStop = false;
            this.grpTXTrace.Text = "Transmit Trace";
            // 
            // grpChanSel
            // 
            this.grpChanSel.Controls.Add(this.lblTXR);
            this.grpChanSel.Controls.Add(this.comboTXR);
            this.grpChanSel.Controls.Add(this.lblTXL);
            this.grpChanSel.Controls.Add(this.comboTXL);
            this.grpChanSel.Controls.Add(this.lblRXR);
            this.grpChanSel.Controls.Add(this.comboRXR);
            this.grpChanSel.Controls.Add(this.lblRXL);
            this.grpChanSel.Controls.Add(this.comboRXL);
            this.grpChanSel.Location = new System.Drawing.Point(560, 192);
            this.grpChanSel.Name = "grpChanSel";
            this.grpChanSel.Size = new System.Drawing.Size(248, 128);
            this.grpChanSel.TabIndex = 77;
            this.grpChanSel.TabStop = false;
            this.grpChanSel.Text = "Input Channel Selection";
            this.grpChanSel.Visible = false;
            // 
            // comboTXR
            // 
            this.comboTXR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTXR.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboTXR.Location = new System.Drawing.Point(152, 64);
            this.comboTXR.Name = "comboTXR";
            this.comboTXR.Size = new System.Drawing.Size(40, 21);
            this.comboTXR.TabIndex = 81;
            this.comboTXR.SelectedIndexChanged += new System.EventHandler(this.comboTXR_SelectedIndexChanged);
            // 
            // comboTXL
            // 
            this.comboTXL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTXL.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboTXL.Location = new System.Drawing.Point(48, 64);
            this.comboTXL.Name = "comboTXL";
            this.comboTXL.Size = new System.Drawing.Size(40, 21);
            this.comboTXL.TabIndex = 79;
            this.comboTXL.SelectedIndexChanged += new System.EventHandler(this.comboTXL_SelectedIndexChanged);
            // 
            // comboRXR
            // 
            this.comboRXR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRXR.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboRXR.Location = new System.Drawing.Point(152, 24);
            this.comboRXR.Name = "comboRXR";
            this.comboRXR.Size = new System.Drawing.Size(40, 21);
            this.comboRXR.TabIndex = 77;
            this.comboRXR.SelectedIndexChanged += new System.EventHandler(this.comboRXR_SelectedIndexChanged);
            // 
            // comboRXL
            // 
            this.comboRXL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRXL.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboRXL.Location = new System.Drawing.Point(48, 24);
            this.comboRXL.Name = "comboRXL";
            this.comboRXL.Size = new System.Drawing.Size(40, 21);
            this.comboRXL.TabIndex = 75;
            this.comboRXL.SelectedIndexChanged += new System.EventHandler(this.comboRXL_SelectedIndexChanged);
            // 
            // ckPreamp
            // 
            this.ckPreamp.Image = null;
            this.ckPreamp.Location = new System.Drawing.Point(760, 400);
            this.ckPreamp.Name = "ckPreamp";
            this.ckPreamp.Size = new System.Drawing.Size(48, 16);
            this.ckPreamp.TabIndex = 82;
            this.ckPreamp.Text = "Pre";
            this.ckPreamp.CheckedChanged += new System.EventHandler(this.ckPreamp_CheckedChanged);
            // 
            // ckTXOut
            // 
            this.ckTXOut.Checked = true;
            this.ckTXOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTXOut.Image = null;
            this.ckTXOut.Location = new System.Drawing.Point(696, 400);
            this.ckTXOut.Name = "ckTXOut";
            this.ckTXOut.Size = new System.Drawing.Size(64, 16);
            this.ckTXOut.TabIndex = 81;
            this.ckTXOut.Text = "TX Out";
            this.ckTXOut.CheckedChanged += new System.EventHandler(this.ckTXOut_CheckedChanged);
            // 
            // chkRXAttn
            // 
            this.chkRXAttn.Image = null;
            this.chkRXAttn.Location = new System.Drawing.Point(696, 384);
            this.chkRXAttn.Name = "chkRXAttn";
            this.chkRXAttn.Size = new System.Drawing.Size(72, 16);
            this.chkRXAttn.TabIndex = 80;
            this.chkRXAttn.Text = "RX Attn";
            this.chkRXAttn.CheckedChanged += new System.EventHandler(this.chkRXAttn_CheckedChanged);
            // 
            // chkPDrvMon
            // 
            this.chkPDrvMon.Image = null;
            this.chkPDrvMon.Location = new System.Drawing.Point(696, 368);
            this.chkPDrvMon.Name = "chkPDrvMon";
            this.chkPDrvMon.Size = new System.Drawing.Size(72, 16);
            this.chkPDrvMon.TabIndex = 79;
            this.chkPDrvMon.Text = "PDrvMon";
            this.chkPDrvMon.CheckedChanged += new System.EventHandler(this.chkPDrvMon_CheckedChanged);
            // 
            // lblTXR
            // 
            this.lblTXR.Image = null;
            this.lblTXR.Location = new System.Drawing.Point(112, 64);
            this.lblTXR.Name = "lblTXR";
            this.lblTXR.Size = new System.Drawing.Size(40, 16);
            this.lblTXR.TabIndex = 82;
            this.lblTXR.Text = "TX R:";
            // 
            // lblTXL
            // 
            this.lblTXL.Image = null;
            this.lblTXL.Location = new System.Drawing.Point(8, 64);
            this.lblTXL.Name = "lblTXL";
            this.lblTXL.Size = new System.Drawing.Size(32, 16);
            this.lblTXL.TabIndex = 80;
            this.lblTXL.Text = "TX L:";
            // 
            // lblRXR
            // 
            this.lblRXR.Image = null;
            this.lblRXR.Location = new System.Drawing.Point(112, 24);
            this.lblRXR.Name = "lblRXR";
            this.lblRXR.Size = new System.Drawing.Size(40, 16);
            this.lblRXR.TabIndex = 78;
            this.lblRXR.Text = "RX R:";
            // 
            // lblRXL
            // 
            this.lblRXL.Image = null;
            this.lblRXL.Location = new System.Drawing.Point(8, 24);
            this.lblRXL.Name = "lblRXL";
            this.lblRXL.Size = new System.Drawing.Size(32, 16);
            this.lblRXL.TabIndex = 76;
            this.lblRXL.Text = "RX L:";
            // 
            // ckRX2FilterBypass
            // 
            this.ckRX2FilterBypass.Image = null;
            this.ckRX2FilterBypass.Location = new System.Drawing.Point(696, 352);
            this.ckRX2FilterBypass.Name = "ckRX2FilterBypass";
            this.ckRX2FilterBypass.Size = new System.Drawing.Size(120, 16);
            this.ckRX2FilterBypass.TabIndex = 74;
            this.ckRX2FilterBypass.Text = "RX2 Filter Bypass";
            this.ckRX2FilterBypass.CheckedChanged += new System.EventHandler(this.ckRX2FilterBypass_CheckedChanged);
            // 
            // chkRX2On
            // 
            this.chkRX2On.Image = null;
            this.chkRX2On.Location = new System.Drawing.Point(696, 336);
            this.chkRX2On.Name = "chkRX2On";
            this.chkRX2On.Size = new System.Drawing.Size(64, 16);
            this.chkRX2On.TabIndex = 71;
            this.chkRX2On.Text = "RX2 On";
            this.chkRX2On.CheckedChanged += new System.EventHandler(this.chkRX2On_CheckedChanged);
            // 
            // ckTXTrace
            // 
            this.ckTXTrace.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckTXTrace.Image = null;
            this.ckTXTrace.Location = new System.Drawing.Point(16, 24);
            this.ckTXTrace.Name = "ckTXTrace";
            this.ckTXTrace.Size = new System.Drawing.Size(64, 24);
            this.ckTXTrace.TabIndex = 68;
            this.ckTXTrace.Text = "Transmit";
            this.ckTXTrace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckTXTrace.CheckedChanged += new System.EventHandler(this.chkTXTrace_CheckedChanged);
            // 
            // udTXTrace
            // 
            this.udTXTrace.DecimalPlaces = 3;
            this.udTXTrace.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.udTXTrace.Location = new System.Drawing.Point(16, 64);
            this.udTXTrace.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udTXTrace.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udTXTrace.Name = "udTXTrace";
            this.udTXTrace.Size = new System.Drawing.Size(56, 20);
            this.udTXTrace.TabIndex = 69;
            this.udTXTrace.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.udTXTrace.ValueChanged += new System.EventHandler(this.udTXTrace_ValueChanged);
            // 
            // grpADC
            // 
            this.grpADC.Controls.Add(this.ckADCPoll);
            this.grpADC.Controls.Add(this.txtADCRead);
            this.grpADC.Controls.Add(this.btnADCRead);
            this.grpADC.Controls.Add(this.comboADCChan);
            this.grpADC.Controls.Add(this.label18);
            this.grpADC.Location = new System.Drawing.Point(8, 328);
            this.grpADC.Name = "grpADC";
            this.grpADC.Size = new System.Drawing.Size(192, 96);
            this.grpADC.TabIndex = 66;
            this.grpADC.TabStop = false;
            this.grpADC.Text = "ADC";
            // 
            // ckADCPoll
            // 
            this.ckADCPoll.Image = null;
            this.ckADCPoll.Location = new System.Drawing.Point(136, 64);
            this.ckADCPoll.Name = "ckADCPoll";
            this.ckADCPoll.Size = new System.Drawing.Size(48, 24);
            this.ckADCPoll.TabIndex = 34;
            this.ckADCPoll.Text = "Poll";
            this.ckADCPoll.CheckedChanged += new System.EventHandler(this.ckADCPoll_CheckedChanged);
            // 
            // txtADCRead
            // 
            this.txtADCRead.Location = new System.Drawing.Point(64, 61);
            this.txtADCRead.Name = "txtADCRead";
            this.txtADCRead.ReadOnly = true;
            this.txtADCRead.Size = new System.Drawing.Size(64, 20);
            this.txtADCRead.TabIndex = 32;
            this.txtADCRead.Text = "0";
            this.txtADCRead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtADCRead_MouseDown);
            // 
            // btnADCRead
            // 
            this.btnADCRead.Image = null;
            this.btnADCRead.Location = new System.Drawing.Point(16, 61);
            this.btnADCRead.Name = "btnADCRead";
            this.btnADCRead.Size = new System.Drawing.Size(40, 23);
            this.btnADCRead.TabIndex = 33;
            this.btnADCRead.Text = "Read";
            this.btnADCRead.Click += new System.EventHandler(this.btnADCRead_Click);
            // 
            // comboADCChan
            // 
            this.comboADCChan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboADCChan.DropDownWidth = 96;
            this.comboADCChan.Items.AddRange(new object[] {
            "Final Bias",
            "Driver Bias",
            "13.8V Ref",
            "Drive Mon",
            "Temp",
            "N/C",
            "Ref Pow",
            "Fwd Pow"});
            this.comboADCChan.Location = new System.Drawing.Point(56, 29);
            this.comboADCChan.Name = "comboADCChan";
            this.comboADCChan.Size = new System.Drawing.Size(96, 21);
            this.comboADCChan.TabIndex = 30;
            this.comboADCChan.SelectedIndexChanged += new System.EventHandler(this.comboADCChan_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Image = null;
            this.label18.Location = new System.Drawing.Point(16, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 23);
            this.label18.TabIndex = 31;
            this.label18.Text = "Chan:";
            // 
            // grpFinalBias
            // 
            this.grpFinalBias.Controls.Add(this.tbPAPot4);
            this.grpFinalBias.Controls.Add(this.lblQ1Fine);
            this.grpFinalBias.Controls.Add(this.lblQ4Coarse);
            this.grpFinalBias.Controls.Add(this.udPAPot7);
            this.grpFinalBias.Controls.Add(this.tbPAPot7);
            this.grpFinalBias.Controls.Add(this.udPAPot4);
            this.grpFinalBias.Controls.Add(this.udPAPot5);
            this.grpFinalBias.Controls.Add(this.lblQ4Fine);
            this.grpFinalBias.Controls.Add(this.tbPAPot5);
            this.grpFinalBias.Controls.Add(this.tbPAPot6);
            this.grpFinalBias.Controls.Add(this.udPAPot6);
            this.grpFinalBias.Controls.Add(this.lblQ1Coarse);
            this.grpFinalBias.Location = new System.Drawing.Point(208, 192);
            this.grpFinalBias.Name = "grpFinalBias";
            this.grpFinalBias.Size = new System.Drawing.Size(192, 128);
            this.grpFinalBias.TabIndex = 65;
            this.grpFinalBias.TabStop = false;
            this.grpFinalBias.Text = "Final Bias";
            // 
            // tbPAPot4
            // 
            this.tbPAPot4.AutoSize = false;
            this.tbPAPot4.LargeChange = 4;
            this.tbPAPot4.Location = new System.Drawing.Point(64, 24);
            this.tbPAPot4.Maximum = 255;
            this.tbPAPot4.Name = "tbPAPot4";
            this.tbPAPot4.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot4.TabIndex = 12;
            this.tbPAPot4.TickFrequency = 32;
            this.tbPAPot4.Scroll += new System.EventHandler(this.tbPAPot4_Scroll);
            // 
            // lblQ1Fine
            // 
            this.lblQ1Fine.Image = null;
            this.lblQ1Fine.Location = new System.Drawing.Point(8, 96);
            this.lblQ1Fine.Name = "lblQ1Fine";
            this.lblQ1Fine.Size = new System.Drawing.Size(48, 16);
            this.lblQ1Fine.TabIndex = 23;
            this.lblQ1Fine.Text = "Q1 Fine:";
            // 
            // lblQ4Coarse
            // 
            this.lblQ4Coarse.Image = null;
            this.lblQ4Coarse.Location = new System.Drawing.Point(8, 24);
            this.lblQ4Coarse.Name = "lblQ4Coarse";
            this.lblQ4Coarse.Size = new System.Drawing.Size(64, 16);
            this.lblQ4Coarse.TabIndex = 20;
            this.lblQ4Coarse.Text = "Q4 Coarse:";
            // 
            // udPAPot7
            // 
            this.udPAPot7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot7.Location = new System.Drawing.Point(136, 96);
            this.udPAPot7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot7.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot7.Name = "udPAPot7";
            this.udPAPot7.Size = new System.Drawing.Size(48, 20);
            this.udPAPot7.TabIndex = 19;
            this.udPAPot7.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot7.ValueChanged += new System.EventHandler(this.udPAPot7_ValueChanged);
            // 
            // tbPAPot7
            // 
            this.tbPAPot7.AutoSize = false;
            this.tbPAPot7.LargeChange = 4;
            this.tbPAPot7.Location = new System.Drawing.Point(64, 96);
            this.tbPAPot7.Maximum = 255;
            this.tbPAPot7.Name = "tbPAPot7";
            this.tbPAPot7.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot7.TabIndex = 18;
            this.tbPAPot7.TickFrequency = 32;
            this.tbPAPot7.Scroll += new System.EventHandler(this.tbPAPot7_Scroll);
            // 
            // udPAPot4
            // 
            this.udPAPot4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot4.Location = new System.Drawing.Point(136, 24);
            this.udPAPot4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot4.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot4.Name = "udPAPot4";
            this.udPAPot4.Size = new System.Drawing.Size(48, 20);
            this.udPAPot4.TabIndex = 13;
            this.udPAPot4.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot4.ValueChanged += new System.EventHandler(this.udPAPot4_ValueChanged);
            // 
            // udPAPot5
            // 
            this.udPAPot5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot5.Location = new System.Drawing.Point(136, 48);
            this.udPAPot5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot5.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot5.Name = "udPAPot5";
            this.udPAPot5.Size = new System.Drawing.Size(48, 20);
            this.udPAPot5.TabIndex = 17;
            this.udPAPot5.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot5.ValueChanged += new System.EventHandler(this.udPAPot5_ValueChanged);
            // 
            // lblQ4Fine
            // 
            this.lblQ4Fine.Image = null;
            this.lblQ4Fine.Location = new System.Drawing.Point(8, 48);
            this.lblQ4Fine.Name = "lblQ4Fine";
            this.lblQ4Fine.Size = new System.Drawing.Size(48, 16);
            this.lblQ4Fine.TabIndex = 22;
            this.lblQ4Fine.Text = "Q4 Fine:";
            // 
            // tbPAPot5
            // 
            this.tbPAPot5.AutoSize = false;
            this.tbPAPot5.LargeChange = 4;
            this.tbPAPot5.Location = new System.Drawing.Point(64, 48);
            this.tbPAPot5.Maximum = 255;
            this.tbPAPot5.Name = "tbPAPot5";
            this.tbPAPot5.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot5.TabIndex = 16;
            this.tbPAPot5.TickFrequency = 32;
            this.tbPAPot5.Scroll += new System.EventHandler(this.tbPAPot5_Scroll);
            // 
            // tbPAPot6
            // 
            this.tbPAPot6.AutoSize = false;
            this.tbPAPot6.LargeChange = 4;
            this.tbPAPot6.Location = new System.Drawing.Point(64, 72);
            this.tbPAPot6.Maximum = 255;
            this.tbPAPot6.Name = "tbPAPot6";
            this.tbPAPot6.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot6.TabIndex = 14;
            this.tbPAPot6.TickFrequency = 32;
            this.tbPAPot6.Scroll += new System.EventHandler(this.tbPAPot6_Scroll);
            // 
            // udPAPot6
            // 
            this.udPAPot6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot6.Location = new System.Drawing.Point(136, 72);
            this.udPAPot6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot6.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot6.Name = "udPAPot6";
            this.udPAPot6.Size = new System.Drawing.Size(48, 20);
            this.udPAPot6.TabIndex = 15;
            this.udPAPot6.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot6.ValueChanged += new System.EventHandler(this.udPAPot6_ValueChanged);
            // 
            // lblQ1Coarse
            // 
            this.lblQ1Coarse.Image = null;
            this.lblQ1Coarse.Location = new System.Drawing.Point(8, 72);
            this.lblQ1Coarse.Name = "lblQ1Coarse";
            this.lblQ1Coarse.Size = new System.Drawing.Size(64, 16);
            this.lblQ1Coarse.TabIndex = 21;
            this.lblQ1Coarse.Text = "Q1 Coarse:";
            // 
            // grpDriverBias
            // 
            this.grpDriverBias.Controls.Add(this.tbPAPot0);
            this.grpDriverBias.Controls.Add(this.lblQ3Fine);
            this.grpDriverBias.Controls.Add(this.lblQ2Fine);
            this.grpDriverBias.Controls.Add(this.lblQ2Coarse);
            this.grpDriverBias.Controls.Add(this.udPAPot3);
            this.grpDriverBias.Controls.Add(this.tbPAPot3);
            this.grpDriverBias.Controls.Add(this.udPAPot1);
            this.grpDriverBias.Controls.Add(this.tbPAPot1);
            this.grpDriverBias.Controls.Add(this.udPAPot0);
            this.grpDriverBias.Controls.Add(this.tbPAPot2);
            this.grpDriverBias.Controls.Add(this.udPAPot2);
            this.grpDriverBias.Controls.Add(this.lblQ3Coarse);
            this.grpDriverBias.Location = new System.Drawing.Point(8, 192);
            this.grpDriverBias.Name = "grpDriverBias";
            this.grpDriverBias.Size = new System.Drawing.Size(192, 128);
            this.grpDriverBias.TabIndex = 62;
            this.grpDriverBias.TabStop = false;
            this.grpDriverBias.Text = "Driver Bias";
            // 
            // tbPAPot0
            // 
            this.tbPAPot0.AutoSize = false;
            this.tbPAPot0.LargeChange = 4;
            this.tbPAPot0.Location = new System.Drawing.Point(64, 24);
            this.tbPAPot0.Maximum = 255;
            this.tbPAPot0.Name = "tbPAPot0";
            this.tbPAPot0.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot0.TabIndex = 12;
            this.tbPAPot0.TickFrequency = 32;
            this.tbPAPot0.Scroll += new System.EventHandler(this.tbPAPot0_Scroll);
            // 
            // lblQ3Fine
            // 
            this.lblQ3Fine.Image = null;
            this.lblQ3Fine.Location = new System.Drawing.Point(8, 96);
            this.lblQ3Fine.Name = "lblQ3Fine";
            this.lblQ3Fine.Size = new System.Drawing.Size(48, 16);
            this.lblQ3Fine.TabIndex = 23;
            this.lblQ3Fine.Text = "Q3 Fine:";
            // 
            // lblQ2Fine
            // 
            this.lblQ2Fine.Image = null;
            this.lblQ2Fine.Location = new System.Drawing.Point(8, 48);
            this.lblQ2Fine.Name = "lblQ2Fine";
            this.lblQ2Fine.Size = new System.Drawing.Size(48, 16);
            this.lblQ2Fine.TabIndex = 22;
            this.lblQ2Fine.Text = "Q2 Fine:";
            // 
            // lblQ2Coarse
            // 
            this.lblQ2Coarse.Image = null;
            this.lblQ2Coarse.Location = new System.Drawing.Point(8, 24);
            this.lblQ2Coarse.Name = "lblQ2Coarse";
            this.lblQ2Coarse.Size = new System.Drawing.Size(64, 16);
            this.lblQ2Coarse.TabIndex = 20;
            this.lblQ2Coarse.Text = "Q2 Coarse:";
            // 
            // udPAPot3
            // 
            this.udPAPot3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot3.Location = new System.Drawing.Point(136, 96);
            this.udPAPot3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot3.Name = "udPAPot3";
            this.udPAPot3.Size = new System.Drawing.Size(48, 20);
            this.udPAPot3.TabIndex = 19;
            this.udPAPot3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot3.ValueChanged += new System.EventHandler(this.udPAPot3_ValueChanged);
            // 
            // tbPAPot3
            // 
            this.tbPAPot3.AutoSize = false;
            this.tbPAPot3.LargeChange = 4;
            this.tbPAPot3.Location = new System.Drawing.Point(64, 96);
            this.tbPAPot3.Maximum = 255;
            this.tbPAPot3.Name = "tbPAPot3";
            this.tbPAPot3.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot3.TabIndex = 18;
            this.tbPAPot3.TickFrequency = 32;
            this.tbPAPot3.Scroll += new System.EventHandler(this.tbPAPot3_Scroll);
            // 
            // udPAPot1
            // 
            this.udPAPot1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot1.Location = new System.Drawing.Point(136, 48);
            this.udPAPot1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot1.Name = "udPAPot1";
            this.udPAPot1.Size = new System.Drawing.Size(48, 20);
            this.udPAPot1.TabIndex = 17;
            this.udPAPot1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot1.ValueChanged += new System.EventHandler(this.udPAPot1_ValueChanged);
            // 
            // tbPAPot1
            // 
            this.tbPAPot1.AutoSize = false;
            this.tbPAPot1.LargeChange = 4;
            this.tbPAPot1.Location = new System.Drawing.Point(64, 48);
            this.tbPAPot1.Maximum = 255;
            this.tbPAPot1.Name = "tbPAPot1";
            this.tbPAPot1.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot1.TabIndex = 16;
            this.tbPAPot1.TickFrequency = 32;
            this.tbPAPot1.Scroll += new System.EventHandler(this.tbPAPot1_Scroll);
            // 
            // udPAPot0
            // 
            this.udPAPot0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot0.Location = new System.Drawing.Point(136, 24);
            this.udPAPot0.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot0.Name = "udPAPot0";
            this.udPAPot0.Size = new System.Drawing.Size(48, 20);
            this.udPAPot0.TabIndex = 13;
            this.udPAPot0.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot0.ValueChanged += new System.EventHandler(this.udPAPot0_ValueChanged);
            // 
            // tbPAPot2
            // 
            this.tbPAPot2.AutoSize = false;
            this.tbPAPot2.LargeChange = 4;
            this.tbPAPot2.Location = new System.Drawing.Point(64, 72);
            this.tbPAPot2.Maximum = 255;
            this.tbPAPot2.Name = "tbPAPot2";
            this.tbPAPot2.Size = new System.Drawing.Size(72, 16);
            this.tbPAPot2.TabIndex = 14;
            this.tbPAPot2.TickFrequency = 32;
            this.tbPAPot2.Scroll += new System.EventHandler(this.tbPAPot2_Scroll);
            // 
            // udPAPot2
            // 
            this.udPAPot2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPAPot2.Location = new System.Drawing.Point(136, 72);
            this.udPAPot2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udPAPot2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot2.Name = "udPAPot2";
            this.udPAPot2.Size = new System.Drawing.Size(48, 20);
            this.udPAPot2.TabIndex = 15;
            this.udPAPot2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPAPot2.ValueChanged += new System.EventHandler(this.udPAPot2_ValueChanged);
            // 
            // lblQ3Coarse
            // 
            this.lblQ3Coarse.Image = null;
            this.lblQ3Coarse.Location = new System.Drawing.Point(8, 72);
            this.lblQ3Coarse.Name = "lblQ3Coarse";
            this.lblQ3Coarse.Size = new System.Drawing.Size(64, 16);
            this.lblQ3Coarse.TabIndex = 21;
            this.lblQ3Coarse.Text = "Q3 Coarse:";
            // 
            // grpScanner
            // 
            this.grpScanner.Controls.Add(this.ckScanB);
            this.grpScanner.Controls.Add(this.udScanDelayB);
            this.grpScanner.Controls.Add(this.lblScanDelayB);
            this.grpScanner.Controls.Add(this.lblScanStepB);
            this.grpScanner.Controls.Add(this.udScanStepB);
            this.grpScanner.Controls.Add(this.lblScanStopB);
            this.grpScanner.Controls.Add(this.udScanStopB);
            this.grpScanner.Controls.Add(this.lblScanStartB);
            this.grpScanner.Controls.Add(this.udScanStartB);
            this.grpScanner.Controls.Add(this.ckScanA);
            this.grpScanner.Controls.Add(this.udScanDelayA);
            this.grpScanner.Controls.Add(this.lblScanDelayA);
            this.grpScanner.Controls.Add(this.lblScanStepA);
            this.grpScanner.Controls.Add(this.udScanStepA);
            this.grpScanner.Controls.Add(this.lblScanStopA);
            this.grpScanner.Controls.Add(this.udScanStopA);
            this.grpScanner.Controls.Add(this.lblScanStartA);
            this.grpScanner.Controls.Add(this.udScanStartA);
            this.grpScanner.Location = new System.Drawing.Point(480, 8);
            this.grpScanner.Name = "grpScanner";
            this.grpScanner.Size = new System.Drawing.Size(304, 136);
            this.grpScanner.TabIndex = 61;
            this.grpScanner.TabStop = false;
            this.grpScanner.Text = "Scanner";
            // 
            // ckScanB
            // 
            this.ckScanB.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckScanB.Image = null;
            this.ckScanB.Location = new System.Drawing.Point(240, 88);
            this.ckScanB.Name = "ckScanB";
            this.ckScanB.Size = new System.Drawing.Size(56, 24);
            this.ckScanB.TabIndex = 17;
            this.ckScanB.Text = "Scan B";
            this.ckScanB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckScanB.CheckedChanged += new System.EventHandler(this.chkScanB_CheckedChanged);
            // 
            // udScanDelayB
            // 
            this.udScanDelayB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udScanDelayB.Location = new System.Drawing.Point(152, 104);
            this.udScanDelayB.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udScanDelayB.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udScanDelayB.Name = "udScanDelayB";
            this.udScanDelayB.Size = new System.Drawing.Size(80, 20);
            this.udScanDelayB.TabIndex = 15;
            this.udScanDelayB.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // lblScanDelayB
            // 
            this.lblScanDelayB.Image = null;
            this.lblScanDelayB.Location = new System.Drawing.Point(120, 104);
            this.lblScanDelayB.Name = "lblScanDelayB";
            this.lblScanDelayB.Size = new System.Drawing.Size(40, 23);
            this.lblScanDelayB.TabIndex = 16;
            this.lblScanDelayB.Text = "Delay:";
            // 
            // lblScanStepB
            // 
            this.lblScanStepB.Image = null;
            this.lblScanStepB.Location = new System.Drawing.Point(8, 104);
            this.lblScanStepB.Name = "lblScanStepB";
            this.lblScanStepB.Size = new System.Drawing.Size(32, 23);
            this.lblScanStepB.TabIndex = 14;
            this.lblScanStepB.Text = "Step:";
            // 
            // udScanStepB
            // 
            this.udScanStepB.DecimalPlaces = 6;
            this.udScanStepB.Increment = new decimal(new int[] {
            100,
            0,
            0,
            327680});
            this.udScanStepB.Location = new System.Drawing.Point(40, 104);
            this.udScanStepB.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udScanStepB.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udScanStepB.Name = "udScanStepB";
            this.udScanStepB.Size = new System.Drawing.Size(80, 20);
            this.udScanStepB.TabIndex = 13;
            this.udScanStepB.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lblScanStopB
            // 
            this.lblScanStopB.Image = null;
            this.lblScanStopB.Location = new System.Drawing.Point(120, 80);
            this.lblScanStopB.Name = "lblScanStopB";
            this.lblScanStopB.Size = new System.Drawing.Size(32, 23);
            this.lblScanStopB.TabIndex = 12;
            this.lblScanStopB.Text = "Stop:";
            // 
            // udScanStopB
            // 
            this.udScanStopB.DecimalPlaces = 6;
            this.udScanStopB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udScanStopB.Location = new System.Drawing.Point(152, 80);
            this.udScanStopB.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udScanStopB.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udScanStopB.Name = "udScanStopB";
            this.udScanStopB.Size = new System.Drawing.Size(80, 20);
            this.udScanStopB.TabIndex = 11;
            this.udScanStopB.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            // 
            // lblScanStartB
            // 
            this.lblScanStartB.Image = null;
            this.lblScanStartB.Location = new System.Drawing.Point(8, 80);
            this.lblScanStartB.Name = "lblScanStartB";
            this.lblScanStartB.Size = new System.Drawing.Size(32, 23);
            this.lblScanStartB.TabIndex = 10;
            this.lblScanStartB.Text = "Start:";
            // 
            // udScanStartB
            // 
            this.udScanStartB.DecimalPlaces = 6;
            this.udScanStartB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udScanStartB.Location = new System.Drawing.Point(40, 80);
            this.udScanStartB.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udScanStartB.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udScanStartB.Name = "udScanStartB";
            this.udScanStartB.Size = new System.Drawing.Size(80, 20);
            this.udScanStartB.TabIndex = 9;
            this.udScanStartB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ckScanA
            // 
            this.ckScanA.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckScanA.Image = null;
            this.ckScanA.Location = new System.Drawing.Point(240, 32);
            this.ckScanA.Name = "ckScanA";
            this.ckScanA.Size = new System.Drawing.Size(56, 24);
            this.ckScanA.TabIndex = 8;
            this.ckScanA.Text = "Scan A";
            this.ckScanA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckScanA.CheckedChanged += new System.EventHandler(this.chkScanA_CheckedChanged);
            // 
            // udScanDelayA
            // 
            this.udScanDelayA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udScanDelayA.Location = new System.Drawing.Point(152, 49);
            this.udScanDelayA.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udScanDelayA.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udScanDelayA.Name = "udScanDelayA";
            this.udScanDelayA.Size = new System.Drawing.Size(80, 20);
            this.udScanDelayA.TabIndex = 6;
            this.udScanDelayA.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // lblScanDelayA
            // 
            this.lblScanDelayA.Image = null;
            this.lblScanDelayA.Location = new System.Drawing.Point(120, 49);
            this.lblScanDelayA.Name = "lblScanDelayA";
            this.lblScanDelayA.Size = new System.Drawing.Size(40, 23);
            this.lblScanDelayA.TabIndex = 7;
            this.lblScanDelayA.Text = "Delay:";
            // 
            // lblScanStepA
            // 
            this.lblScanStepA.Image = null;
            this.lblScanStepA.Location = new System.Drawing.Point(8, 49);
            this.lblScanStepA.Name = "lblScanStepA";
            this.lblScanStepA.Size = new System.Drawing.Size(32, 23);
            this.lblScanStepA.TabIndex = 5;
            this.lblScanStepA.Text = "Step:";
            // 
            // udScanStepA
            // 
            this.udScanStepA.DecimalPlaces = 6;
            this.udScanStepA.Increment = new decimal(new int[] {
            100,
            0,
            0,
            327680});
            this.udScanStepA.Location = new System.Drawing.Point(40, 49);
            this.udScanStepA.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udScanStepA.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udScanStepA.Name = "udScanStepA";
            this.udScanStepA.Size = new System.Drawing.Size(80, 20);
            this.udScanStepA.TabIndex = 4;
            this.udScanStepA.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lblScanStopA
            // 
            this.lblScanStopA.Image = null;
            this.lblScanStopA.Location = new System.Drawing.Point(120, 24);
            this.lblScanStopA.Name = "lblScanStopA";
            this.lblScanStopA.Size = new System.Drawing.Size(32, 23);
            this.lblScanStopA.TabIndex = 3;
            this.lblScanStopA.Text = "Stop:";
            // 
            // udScanStopA
            // 
            this.udScanStopA.DecimalPlaces = 6;
            this.udScanStopA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udScanStopA.Location = new System.Drawing.Point(152, 24);
            this.udScanStopA.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udScanStopA.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udScanStopA.Name = "udScanStopA";
            this.udScanStopA.Size = new System.Drawing.Size(80, 20);
            this.udScanStopA.TabIndex = 2;
            this.udScanStopA.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            // 
            // lblScanStartA
            // 
            this.lblScanStartA.Image = null;
            this.lblScanStartA.Location = new System.Drawing.Point(8, 24);
            this.lblScanStartA.Name = "lblScanStartA";
            this.lblScanStartA.Size = new System.Drawing.Size(32, 23);
            this.lblScanStartA.TabIndex = 1;
            this.lblScanStartA.Text = "Start:";
            // 
            // udScanStartA
            // 
            this.udScanStartA.DecimalPlaces = 6;
            this.udScanStartA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udScanStartA.Location = new System.Drawing.Point(40, 24);
            this.udScanStartA.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udScanStartA.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udScanStartA.Name = "udScanStartA";
            this.udScanStartA.Size = new System.Drawing.Size(80, 20);
            this.udScanStartA.TabIndex = 0;
            this.udScanStartA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpPLL
            // 
            this.grpPLL.Controls.Add(this.lblPLLClock);
            this.grpPLL.Controls.Add(this.ckPLLPFDPol);
            this.grpPLL.Controls.Add(this.cmboPLLCPMode);
            this.grpPLL.Controls.Add(this.label10);
            this.grpPLL.Controls.Add(this.comboPLLStatusMux);
            this.grpPLL.Controls.Add(this.ckPLLPollStatus);
            this.grpPLL.Controls.Add(this.cmboPLLRefClock);
            this.grpPLL.Controls.Add(this.btnPLLStatus);
            this.grpPLL.Controls.Add(this.ckPLL);
            this.grpPLL.Location = new System.Drawing.Point(560, 192);
            this.grpPLL.Name = "grpPLL";
            this.grpPLL.Size = new System.Drawing.Size(248, 128);
            this.grpPLL.TabIndex = 59;
            this.grpPLL.TabStop = false;
            this.grpPLL.Text = "PLL";
            // 
            // lblPLLClock
            // 
            this.lblPLLClock.Image = null;
            this.lblPLLClock.Location = new System.Drawing.Point(16, 48);
            this.lblPLLClock.Name = "lblPLLClock";
            this.lblPLLClock.Size = new System.Drawing.Size(64, 23);
            this.lblPLLClock.TabIndex = 55;
            this.lblPLLClock.Text = "PLL Clock:";
            // 
            // ckPLLPFDPol
            // 
            this.ckPLLPFDPol.Checked = true;
            this.ckPLLPFDPol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckPLLPFDPol.Image = null;
            this.ckPLLPFDPol.Location = new System.Drawing.Point(16, 96);
            this.ckPLLPFDPol.Name = "ckPLLPFDPol";
            this.ckPLLPFDPol.Size = new System.Drawing.Size(88, 24);
            this.ckPLLPFDPol.TabIndex = 59;
            this.ckPLLPFDPol.Text = "PFD Polarity";
            this.ckPLLPFDPol.CheckedChanged += new System.EventHandler(this.chkPLLPFDPol_CheckedChanged);
            // 
            // cmboPLLCPMode
            // 
            this.cmboPLLCPMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboPLLCPMode.Items.AddRange(new object[] {
            "Tri-Stated",
            "Pump-Up",
            "Pump-Down",
            "Normal"});
            this.cmboPLLCPMode.Location = new System.Drawing.Point(184, 48);
            this.cmboPLLCPMode.Name = "cmboPLLCPMode";
            this.cmboPLLCPMode.Size = new System.Drawing.Size(56, 21);
            this.cmboPLLCPMode.TabIndex = 57;
            this.cmboPLLCPMode.SelectedIndexChanged += new System.EventHandler(this.comboPLLCPMode_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Image = null;
            this.label10.Location = new System.Drawing.Point(128, 48);
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
            this.comboPLLStatusMux.Location = new System.Drawing.Point(16, 72);
            this.comboPLLStatusMux.Name = "comboPLLStatusMux";
            this.comboPLLStatusMux.Size = new System.Drawing.Size(224, 21);
            this.comboPLLStatusMux.TabIndex = 56;
            this.comboPLLStatusMux.SelectedIndexChanged += new System.EventHandler(this.comboPLLStatusMux_SelectedIndexChanged);
            // 
            // ckPLLPollStatus
            // 
            this.ckPLLPollStatus.Image = null;
            this.ckPLLPollStatus.Location = new System.Drawing.Point(168, 96);
            this.ckPLLPollStatus.Name = "ckPLLPollStatus";
            this.ckPLLPollStatus.Size = new System.Drawing.Size(48, 24);
            this.ckPLLPollStatus.TabIndex = 53;
            this.ckPLLPollStatus.Text = "Poll";
            this.ckPLLPollStatus.CheckedChanged += new System.EventHandler(this.ckPLLPollStatus_CheckedChanged);
            // 
            // cmboPLLRefClock
            // 
            this.cmboPLLRefClock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboPLLRefClock.Items.AddRange(new object[] {
            "10",
            "20"});
            this.cmboPLLRefClock.Location = new System.Drawing.Point(80, 48);
            this.cmboPLLRefClock.Name = "cmboPLLRefClock";
            this.cmboPLLRefClock.Size = new System.Drawing.Size(40, 21);
            this.cmboPLLRefClock.TabIndex = 52;
            this.cmboPLLRefClock.SelectedIndexChanged += new System.EventHandler(this.comboPLLRefClock_SelectedIndexChanged);
            // 
            // btnPLLStatus
            // 
            this.btnPLLStatus.Image = null;
            this.btnPLLStatus.Location = new System.Drawing.Point(104, 96);
            this.btnPLLStatus.Name = "btnPLLStatus";
            this.btnPLLStatus.Size = new System.Drawing.Size(48, 23);
            this.btnPLLStatus.TabIndex = 49;
            this.btnPLLStatus.Text = "Status";
            this.btnPLLStatus.Click += new System.EventHandler(this.btnPLLStatus_Click);
            // 
            // ckPLL
            // 
            this.ckPLL.Checked = true;
            this.ckPLL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckPLL.Image = null;
            this.ckPLL.Location = new System.Drawing.Point(16, 24);
            this.ckPLL.Name = "ckPLL";
            this.ckPLL.Size = new System.Drawing.Size(64, 24);
            this.ckPLL.TabIndex = 13;
            this.ckPLL.Text = "Enable";
            this.ckPLL.CheckedChanged += new System.EventHandler(this.chkPLL_CheckedChanged);
            // 
            // grpKey
            // 
            this.grpKey.Controls.Add(this.chkRCAPTT);
            this.grpKey.Controls.Add(this.chkMicPTT);
            this.grpKey.Controls.Add(this.ckKeyPoll);
            this.grpKey.Controls.Add(this.chkDash);
            this.grpKey.Controls.Add(this.chkDot);
            this.grpKey.Location = new System.Drawing.Point(480, 144);
            this.grpKey.Name = "grpKey";
            this.grpKey.Size = new System.Drawing.Size(304, 48);
            this.grpKey.TabIndex = 16;
            this.grpKey.TabStop = false;
            this.grpKey.Text = "Key";
            // 
            // chkRCAPTT
            // 
            this.chkRCAPTT.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRCAPTT.Image = null;
            this.chkRCAPTT.Location = new System.Drawing.Point(128, 16);
            this.chkRCAPTT.Name = "chkRCAPTT";
            this.chkRCAPTT.Size = new System.Drawing.Size(48, 24);
            this.chkRCAPTT.TabIndex = 4;
            this.chkRCAPTT.Text = "RCA";
            this.chkRCAPTT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRCAPTT.CheckedChanged += new System.EventHandler(this.chkRCAPTT_CheckedChanged);
            // 
            // chkMicPTT
            // 
            this.chkMicPTT.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMicPTT.Image = null;
            this.chkMicPTT.Location = new System.Drawing.Point(184, 16);
            this.chkMicPTT.Name = "chkMicPTT";
            this.chkMicPTT.Size = new System.Drawing.Size(48, 24);
            this.chkMicPTT.TabIndex = 3;
            this.chkMicPTT.Text = "Mic";
            this.chkMicPTT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMicPTT.CheckedChanged += new System.EventHandler(this.chkMicPTT_CheckedChanged);
            // 
            // ckKeyPoll
            // 
            this.ckKeyPoll.Image = null;
            this.ckKeyPoll.Location = new System.Drawing.Point(248, 16);
            this.ckKeyPoll.Name = "ckKeyPoll";
            this.ckKeyPoll.Size = new System.Drawing.Size(48, 24);
            this.ckKeyPoll.TabIndex = 2;
            this.ckKeyPoll.Text = "Poll";
            this.ckKeyPoll.CheckedChanged += new System.EventHandler(this.ckKeyPoll_CheckedChanged);
            // 
            // chkDash
            // 
            this.chkDash.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDash.Image = null;
            this.chkDash.Location = new System.Drawing.Point(72, 16);
            this.chkDash.Name = "chkDash";
            this.chkDash.Size = new System.Drawing.Size(48, 24);
            this.chkDash.TabIndex = 1;
            this.chkDash.Text = "Dash";
            this.chkDash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDash.CheckedChanged += new System.EventHandler(this.chkDash_CheckedChanged);
            // 
            // chkDot
            // 
            this.chkDot.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDot.Image = null;
            this.chkDot.Location = new System.Drawing.Point(16, 16);
            this.chkDot.Name = "chkDot";
            this.chkDot.Size = new System.Drawing.Size(48, 24);
            this.chkDot.TabIndex = 0;
            this.chkDot.Text = "Dot";
            this.chkDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDot.CheckedChanged += new System.EventHandler(this.chkDot_CheckedChanged);
            // 
            // grpSwitches
            // 
            this.grpSwitches.Controls.Add(this.chkIntLED);
            this.grpSwitches.Controls.Add(this.chkXVTR);
            this.grpSwitches.Controls.Add(this.button1);
            this.grpSwitches.Controls.Add(this.chkPCPwr);
            this.grpSwitches.Controls.Add(this.chkReset);
            this.grpSwitches.Controls.Add(this.chkRTS);
            this.grpSwitches.Controls.Add(this.chkCTS);
            this.grpSwitches.Controls.Add(this.chkFPLED);
            this.grpSwitches.Controls.Add(this.ckPAOff);
            this.grpSwitches.Controls.Add(this.ckPABias);
            this.grpSwitches.Controls.Add(this.chkXVTXEN);
            this.grpSwitches.Controls.Add(this.chkKEY2M);
            this.grpSwitches.Controls.Add(this.chkEN2M);
            this.grpSwitches.Controls.Add(this.chkXVCOM);
            this.grpSwitches.Controls.Add(this.ckTXMon);
            this.grpSwitches.Controls.Add(this.ckTR);
            this.grpSwitches.Controls.Add(this.chkRX1Out);
            this.grpSwitches.Controls.Add(this.chkFan);
            this.grpSwitches.Controls.Add(this.chkRCATX3);
            this.grpSwitches.Controls.Add(this.chkRCATX2);
            this.grpSwitches.Controls.Add(this.chkRCATX1);
            this.grpSwitches.Controls.Add(this.chkHeadphone);
            this.grpSwitches.Controls.Add(this.chkIntSpkr);
            this.grpSwitches.Controls.Add(this.chkRX1Tap);
            this.grpSwitches.Controls.Add(this.chkQSD);
            this.grpSwitches.Controls.Add(this.ckTest);
            this.grpSwitches.Controls.Add(this.ckGen);
            this.grpSwitches.Controls.Add(this.ckSig);
            this.grpSwitches.Controls.Add(this.ckXVEN);
            this.grpSwitches.Controls.Add(this.chkXREF);
            this.grpSwitches.Controls.Add(this.ckQSE);
            this.grpSwitches.Controls.Add(this.chkImpulse);
            this.grpSwitches.Location = new System.Drawing.Point(8, 8);
            this.grpSwitches.Name = "grpSwitches";
            this.grpSwitches.Size = new System.Drawing.Size(464, 176);
            this.grpSwitches.TabIndex = 15;
            this.grpSwitches.TabStop = false;
            this.grpSwitches.Text = "Switches";
            // 
            // chkIntLED
            // 
            this.chkIntLED.Image = null;
            this.chkIntLED.Location = new System.Drawing.Point(296, 96);
            this.chkIntLED.Name = "chkIntLED";
            this.chkIntLED.Size = new System.Drawing.Size(64, 24);
            this.chkIntLED.TabIndex = 36;
            this.chkIntLED.Text = "Int LED";
            this.chkIntLED.CheckedChanged += new System.EventHandler(this.chkIntLED_CheckedChanged);
            // 
            // chkXVTR
            // 
            this.chkXVTR.Image = null;
            this.chkXVTR.Location = new System.Drawing.Point(296, 72);
            this.chkXVTR.Name = "chkXVTR";
            this.chkXVTR.Size = new System.Drawing.Size(64, 24);
            this.chkXVTR.TabIndex = 35;
            this.chkXVTR.Text = "XVTR";
            this.chkXVTR.CheckedChanged += new System.EventHandler(this.chkXVTR_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Run Imp";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkPCPwr
            // 
            this.chkPCPwr.Image = null;
            this.chkPCPwr.Location = new System.Drawing.Point(368, 144);
            this.chkPCPwr.Name = "chkPCPwr";
            this.chkPCPwr.Size = new System.Drawing.Size(80, 24);
            this.chkPCPwr.TabIndex = 33;
            this.chkPCPwr.Text = "Pwr Button";
            this.chkPCPwr.CheckedChanged += new System.EventHandler(this.chkPCPwr_CheckedChanged);
            // 
            // chkReset
            // 
            this.chkReset.Image = null;
            this.chkReset.Location = new System.Drawing.Point(304, 144);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(56, 24);
            this.chkReset.TabIndex = 32;
            this.chkReset.Text = "Reset";
            this.chkReset.CheckedChanged += new System.EventHandler(this.chkReset_CheckedChanged);
            // 
            // chkRTS
            // 
            this.chkRTS.Image = null;
            this.chkRTS.Location = new System.Drawing.Point(248, 144);
            this.chkRTS.Name = "chkRTS";
            this.chkRTS.Size = new System.Drawing.Size(48, 24);
            this.chkRTS.TabIndex = 31;
            this.chkRTS.Text = "RTS";
            this.chkRTS.CheckedChanged += new System.EventHandler(this.chkRTS_CheckedChanged);
            // 
            // chkCTS
            // 
            this.chkCTS.Image = null;
            this.chkCTS.Location = new System.Drawing.Point(192, 144);
            this.chkCTS.Name = "chkCTS";
            this.chkCTS.Size = new System.Drawing.Size(48, 24);
            this.chkCTS.TabIndex = 30;
            this.chkCTS.Text = "CTS";
            this.chkCTS.CheckedChanged += new System.EventHandler(this.chkCTS_CheckedChanged);
            // 
            // chkFPLED
            // 
            this.chkFPLED.Image = null;
            this.chkFPLED.Location = new System.Drawing.Point(120, 144);
            this.chkFPLED.Name = "chkFPLED";
            this.chkFPLED.Size = new System.Drawing.Size(64, 24);
            this.chkFPLED.TabIndex = 29;
            this.chkFPLED.Text = "FPLED";
            this.chkFPLED.CheckedChanged += new System.EventHandler(this.chkFPLED_CheckedChanged);
            // 
            // ckPAOff
            // 
            this.ckPAOff.Image = null;
            this.ckPAOff.Location = new System.Drawing.Point(72, 144);
            this.ckPAOff.Name = "ckPAOff";
            this.ckPAOff.Size = new System.Drawing.Size(40, 24);
            this.ckPAOff.TabIndex = 28;
            this.ckPAOff.Text = "Off";
            this.ckPAOff.CheckedChanged += new System.EventHandler(this.chkPAOff_CheckedChanged);
            // 
            // ckPABias
            // 
            this.ckPABias.Image = null;
            this.ckPABias.Location = new System.Drawing.Point(16, 144);
            this.ckPABias.Name = "ckPABias";
            this.ckPABias.Size = new System.Drawing.Size(48, 24);
            this.ckPABias.TabIndex = 27;
            this.ckPABias.Text = "Bias";
            this.ckPABias.CheckedChanged += new System.EventHandler(this.ckPABias_CheckedChanged);
            // 
            // chkXVTXEN
            // 
            this.chkXVTXEN.Image = null;
            this.chkXVTXEN.Location = new System.Drawing.Point(96, 48);
            this.chkXVTXEN.Name = "chkXVTXEN";
            this.chkXVTXEN.Size = new System.Drawing.Size(96, 24);
            this.chkXVTXEN.TabIndex = 26;
            this.chkXVTXEN.Text = "XVTXEN";
            this.chkXVTXEN.CheckedChanged += new System.EventHandler(this.chkXVTXEN_CheckedChanged);
            // 
            // chkKEY2M
            // 
            this.chkKEY2M.Image = null;
            this.chkKEY2M.Location = new System.Drawing.Point(384, 96);
            this.chkKEY2M.Name = "chkKEY2M";
            this.chkKEY2M.Size = new System.Drawing.Size(72, 24);
            this.chkKEY2M.TabIndex = 25;
            this.chkKEY2M.Tag = "KEY 2M";
            this.chkKEY2M.Text = "EXT/R";
            this.chkKEY2M.CheckedChanged += new System.EventHandler(this.chkKEY2M_CheckedChanged);
            // 
            // chkEN2M
            // 
            this.chkEN2M.Image = null;
            this.chkEN2M.Location = new System.Drawing.Point(384, 72);
            this.chkEN2M.Name = "chkEN2M";
            this.chkEN2M.Size = new System.Drawing.Size(64, 24);
            this.chkEN2M.TabIndex = 24;
            this.chkEN2M.Tag = "EN 2M";
            this.chkEN2M.Text = "XVINT";
            this.chkEN2M.CheckedChanged += new System.EventHandler(this.chkEN2M_CheckedChanged);
            // 
            // chkXVCOM
            // 
            this.chkXVCOM.Image = null;
            this.chkXVCOM.Location = new System.Drawing.Point(384, 48);
            this.chkXVCOM.Name = "chkXVCOM";
            this.chkXVCOM.Size = new System.Drawing.Size(72, 24);
            this.chkXVCOM.TabIndex = 23;
            this.chkXVCOM.Text = "XVCOM";
            this.chkXVCOM.CheckedChanged += new System.EventHandler(this.chkXVCOM_CheckedChanged);
            // 
            // ckTXMon
            // 
            this.ckTXMon.Image = null;
            this.ckTXMon.Location = new System.Drawing.Point(384, 120);
            this.ckTXMon.Name = "ckTXMon";
            this.ckTXMon.Size = new System.Drawing.Size(72, 24);
            this.ckTXMon.TabIndex = 22;
            this.ckTXMon.Text = "TX MON";
            this.ckTXMon.CheckedChanged += new System.EventHandler(this.chkTXMon_CheckedChanged);
            // 
            // ckTR
            // 
            this.ckTR.Image = null;
            this.ckTR.Location = new System.Drawing.Point(296, 48);
            this.ckTR.Name = "ckTR";
            this.ckTR.Size = new System.Drawing.Size(96, 24);
            this.ckTR.TabIndex = 19;
            this.ckTR.Text = "TR (Receive)";
            this.ckTR.CheckedChanged += new System.EventHandler(this.chkTR_CheckedChanged);
            // 
            // chkRX1Out
            // 
            this.chkRX1Out.Image = null;
            this.chkRX1Out.Location = new System.Drawing.Point(296, 24);
            this.chkRX1Out.Name = "chkRX1Out";
            this.chkRX1Out.Size = new System.Drawing.Size(88, 24);
            this.chkRX1Out.TabIndex = 18;
            this.chkRX1Out.Text = "RX1 Out";
            this.chkRX1Out.CheckedChanged += new System.EventHandler(this.chkRX1Out_CheckedChanged);
            // 
            // chkFan
            // 
            this.chkFan.Checked = true;
            this.chkFan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFan.Image = null;
            this.chkFan.Location = new System.Drawing.Point(16, 120);
            this.chkFan.Name = "chkFan";
            this.chkFan.Size = new System.Drawing.Size(64, 24);
            this.chkFan.TabIndex = 17;
            this.chkFan.Text = "Fan";
            this.chkFan.CheckedChanged += new System.EventHandler(this.chkFan_CheckedChanged);
            // 
            // chkRCATX3
            // 
            this.chkRCATX3.Image = null;
            this.chkRCATX3.Location = new System.Drawing.Point(296, 120);
            this.chkRCATX3.Name = "chkRCATX3";
            this.chkRCATX3.Size = new System.Drawing.Size(72, 24);
            this.chkRCATX3.TabIndex = 16;
            this.chkRCATX3.Text = "RCA TX3";
            this.chkRCATX3.CheckedChanged += new System.EventHandler(this.chkRCATX3_CheckedChanged);
            // 
            // chkRCATX2
            // 
            this.chkRCATX2.Image = null;
            this.chkRCATX2.Location = new System.Drawing.Point(200, 120);
            this.chkRCATX2.Name = "chkRCATX2";
            this.chkRCATX2.Size = new System.Drawing.Size(72, 24);
            this.chkRCATX2.TabIndex = 15;
            this.chkRCATX2.Text = "RCA TX2";
            this.chkRCATX2.CheckedChanged += new System.EventHandler(this.chkRCATX2_CheckedChanged);
            // 
            // chkRCATX1
            // 
            this.chkRCATX1.Image = null;
            this.chkRCATX1.Location = new System.Drawing.Point(96, 120);
            this.chkRCATX1.Name = "chkRCATX1";
            this.chkRCATX1.Size = new System.Drawing.Size(72, 24);
            this.chkRCATX1.TabIndex = 14;
            this.chkRCATX1.Text = "RCA TX1";
            this.chkRCATX1.CheckedChanged += new System.EventHandler(this.chkRCATX1_CheckedChanged);
            // 
            // chkHeadphone
            // 
            this.chkHeadphone.Image = null;
            this.chkHeadphone.Location = new System.Drawing.Point(200, 72);
            this.chkHeadphone.Name = "chkHeadphone";
            this.chkHeadphone.Size = new System.Drawing.Size(96, 24);
            this.chkHeadphone.TabIndex = 12;
            this.chkHeadphone.Text = "Headphone";
            this.chkHeadphone.CheckedChanged += new System.EventHandler(this.chkHeadphone_CheckedChanged);
            // 
            // chkIntSpkr
            // 
            this.chkIntSpkr.Image = null;
            this.chkIntSpkr.Location = new System.Drawing.Point(200, 48);
            this.chkIntSpkr.Name = "chkIntSpkr";
            this.chkIntSpkr.Size = new System.Drawing.Size(96, 24);
            this.chkIntSpkr.TabIndex = 10;
            this.chkIntSpkr.Text = "Int. Speaker";
            this.chkIntSpkr.CheckedChanged += new System.EventHandler(this.chkIntSpkr_CheckedChanged);
            // 
            // chkRX1Tap
            // 
            this.chkRX1Tap.Image = null;
            this.chkRX1Tap.Location = new System.Drawing.Point(200, 96);
            this.chkRX1Tap.Name = "chkRX1Tap";
            this.chkRX1Tap.Size = new System.Drawing.Size(96, 24);
            this.chkRX1Tap.TabIndex = 11;
            this.chkRX1Tap.Text = "RX1 Tap";
            this.chkRX1Tap.CheckedChanged += new System.EventHandler(this.chkRX1Tap_CheckedChanged);
            // 
            // chkQSD
            // 
            this.chkQSD.Checked = true;
            this.chkQSD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQSD.Image = null;
            this.chkQSD.Location = new System.Drawing.Point(96, 72);
            this.chkQSD.Name = "chkQSD";
            this.chkQSD.Size = new System.Drawing.Size(96, 24);
            this.chkQSD.TabIndex = 7;
            this.chkQSD.Text = "QSD";
            this.chkQSD.CheckedChanged += new System.EventHandler(this.chkQSD_CheckedChanged);
            // 
            // ckTest
            // 
            this.ckTest.Image = null;
            this.ckTest.Location = new System.Drawing.Point(16, 48);
            this.ckTest.Name = "ckTest";
            this.ckTest.Size = new System.Drawing.Size(64, 24);
            this.ckTest.TabIndex = 2;
            this.ckTest.Text = "Test";
            this.ckTest.CheckedChanged += new System.EventHandler(this.chkTest_CheckedChanged);
            // 
            // ckGen
            // 
            this.ckGen.Image = null;
            this.ckGen.Location = new System.Drawing.Point(16, 72);
            this.ckGen.Name = "ckGen";
            this.ckGen.Size = new System.Drawing.Size(64, 24);
            this.ckGen.TabIndex = 3;
            this.ckGen.Text = "Gen";
            this.ckGen.CheckedChanged += new System.EventHandler(this.chkGen_CheckedChanged);
            // 
            // ckSig
            // 
            this.ckSig.Image = null;
            this.ckSig.Location = new System.Drawing.Point(16, 96);
            this.ckSig.Name = "ckSig";
            this.ckSig.Size = new System.Drawing.Size(64, 24);
            this.ckSig.TabIndex = 4;
            this.ckSig.Text = "Sig";
            this.ckSig.CheckedChanged += new System.EventHandler(this.chkSig_CheckedChanged);
            // 
            // ckXVEN
            // 
            this.ckXVEN.Image = null;
            this.ckXVEN.Location = new System.Drawing.Point(384, 24);
            this.ckXVEN.Name = "ckXVEN";
            this.ckXVEN.Size = new System.Drawing.Size(64, 24);
            this.ckXVEN.TabIndex = 6;
            this.ckXVEN.Text = "XVEN";
            this.ckXVEN.CheckedChanged += new System.EventHandler(this.chkXVEN_CheckedChanged);
            // 
            // chkXREF
            // 
            this.chkXREF.Image = null;
            this.chkXREF.Location = new System.Drawing.Point(200, 24);
            this.chkXREF.Name = "chkXREF";
            this.chkXREF.Size = new System.Drawing.Size(96, 24);
            this.chkXREF.TabIndex = 9;
            this.chkXREF.Text = "External Ref.";
            this.chkXREF.CheckedChanged += new System.EventHandler(this.chkXREF_CheckedChanged);
            // 
            // ckQSE
            // 
            this.ckQSE.Image = null;
            this.ckQSE.Location = new System.Drawing.Point(96, 96);
            this.ckQSE.Name = "ckQSE";
            this.ckQSE.Size = new System.Drawing.Size(96, 24);
            this.ckQSE.TabIndex = 8;
            this.ckQSE.Text = "QSE";
            this.ckQSE.CheckedChanged += new System.EventHandler(this.chkQSE_CheckedChanged);
            // 
            // chkImpulse
            // 
            this.chkImpulse.Image = null;
            this.chkImpulse.Location = new System.Drawing.Point(96, 24);
            this.chkImpulse.Name = "chkImpulse";
            this.chkImpulse.Size = new System.Drawing.Size(64, 24);
            this.chkImpulse.TabIndex = 5;
            this.chkImpulse.Text = "Impulse";
            this.chkImpulse.CheckedChanged += new System.EventHandler(this.chkImpulse_CheckedChanged);
            // 
            // grpTRXPots
            // 
            this.grpTRXPots.Controls.Add(this.label4);
            this.grpTRXPots.Controls.Add(this.label3);
            this.grpTRXPots.Controls.Add(this.label2);
            this.grpTRXPots.Controls.Add(this.label1);
            this.grpTRXPots.Controls.Add(this.udTRXPot3);
            this.grpTRXPots.Controls.Add(this.tbTRXPot3);
            this.grpTRXPots.Controls.Add(this.udTRXPot2);
            this.grpTRXPots.Controls.Add(this.tbTRXPot2);
            this.grpTRXPots.Controls.Add(this.udTRXPot1);
            this.grpTRXPots.Controls.Add(this.tbTRXPot1);
            this.grpTRXPots.Controls.Add(this.udTRXPot0);
            this.grpTRXPots.Controls.Add(this.tbTRXPot0);
            this.grpTRXPots.Location = new System.Drawing.Point(408, 192);
            this.grpTRXPots.Name = "grpTRXPots";
            this.grpTRXPots.Size = new System.Drawing.Size(144, 128);
            this.grpTRXPots.TabIndex = 14;
            this.grpTRXPots.TabStop = false;
            this.grpTRXPots.Text = "Carrier Null";
            // 
            // label4
            // 
            this.label4.Image = null;
            this.label4.Location = new System.Drawing.Point(8, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "3";
            // 
            // label3
            // 
            this.label3.Image = null;
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(8, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "2";
            // 
            // label2
            // 
            this.label2.Image = null;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "1";
            // 
            // label1
            // 
            this.label1.Image = null;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(8, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "0";
            // 
            // udTRXPot3
            // 
            this.udTRXPot3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTRXPot3.Location = new System.Drawing.Point(88, 96);
            this.udTRXPot3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udTRXPot3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udTRXPot3.Name = "udTRXPot3";
            this.udTRXPot3.Size = new System.Drawing.Size(48, 20);
            this.udTRXPot3.TabIndex = 19;
            this.udTRXPot3.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.udTRXPot3.ValueChanged += new System.EventHandler(this.udTRXPot3_ValueChanged);
            // 
            // tbTRXPot3
            // 
            this.tbTRXPot3.AutoSize = false;
            this.tbTRXPot3.LargeChange = 4;
            this.tbTRXPot3.Location = new System.Drawing.Point(16, 96);
            this.tbTRXPot3.Maximum = 255;
            this.tbTRXPot3.Name = "tbTRXPot3";
            this.tbTRXPot3.Size = new System.Drawing.Size(72, 16);
            this.tbTRXPot3.TabIndex = 18;
            this.tbTRXPot3.TickFrequency = 32;
            this.tbTRXPot3.Value = 128;
            this.tbTRXPot3.Scroll += new System.EventHandler(this.tbTRXPot3_Scroll);
            // 
            // udTRXPot2
            // 
            this.udTRXPot2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTRXPot2.Location = new System.Drawing.Point(88, 72);
            this.udTRXPot2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udTRXPot2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udTRXPot2.Name = "udTRXPot2";
            this.udTRXPot2.Size = new System.Drawing.Size(48, 20);
            this.udTRXPot2.TabIndex = 17;
            this.udTRXPot2.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.udTRXPot2.ValueChanged += new System.EventHandler(this.udTRXPot2_ValueChanged);
            // 
            // tbTRXPot2
            // 
            this.tbTRXPot2.AutoSize = false;
            this.tbTRXPot2.LargeChange = 4;
            this.tbTRXPot2.Location = new System.Drawing.Point(16, 72);
            this.tbTRXPot2.Maximum = 255;
            this.tbTRXPot2.Name = "tbTRXPot2";
            this.tbTRXPot2.Size = new System.Drawing.Size(72, 16);
            this.tbTRXPot2.TabIndex = 16;
            this.tbTRXPot2.TickFrequency = 32;
            this.tbTRXPot2.Value = 128;
            this.tbTRXPot2.Scroll += new System.EventHandler(this.tbTRXPot2_Scroll);
            // 
            // udTRXPot1
            // 
            this.udTRXPot1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTRXPot1.Location = new System.Drawing.Point(88, 48);
            this.udTRXPot1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udTRXPot1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udTRXPot1.Name = "udTRXPot1";
            this.udTRXPot1.Size = new System.Drawing.Size(48, 20);
            this.udTRXPot1.TabIndex = 15;
            this.udTRXPot1.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.udTRXPot1.ValueChanged += new System.EventHandler(this.udTRXPot1_ValueChanged);
            // 
            // tbTRXPot1
            // 
            this.tbTRXPot1.AutoSize = false;
            this.tbTRXPot1.LargeChange = 4;
            this.tbTRXPot1.Location = new System.Drawing.Point(16, 48);
            this.tbTRXPot1.Maximum = 255;
            this.tbTRXPot1.Name = "tbTRXPot1";
            this.tbTRXPot1.Size = new System.Drawing.Size(72, 16);
            this.tbTRXPot1.TabIndex = 14;
            this.tbTRXPot1.TickFrequency = 32;
            this.tbTRXPot1.Value = 128;
            this.tbTRXPot1.Scroll += new System.EventHandler(this.tbTRXPot1_Scroll);
            // 
            // udTRXPot0
            // 
            this.udTRXPot0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTRXPot0.Location = new System.Drawing.Point(88, 24);
            this.udTRXPot0.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udTRXPot0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udTRXPot0.Name = "udTRXPot0";
            this.udTRXPot0.Size = new System.Drawing.Size(48, 20);
            this.udTRXPot0.TabIndex = 13;
            this.udTRXPot0.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.udTRXPot0.ValueChanged += new System.EventHandler(this.udTRXPot0_ValueChanged);
            // 
            // tbTRXPot0
            // 
            this.tbTRXPot0.AutoSize = false;
            this.tbTRXPot0.LargeChange = 4;
            this.tbTRXPot0.Location = new System.Drawing.Point(16, 24);
            this.tbTRXPot0.Maximum = 255;
            this.tbTRXPot0.Name = "tbTRXPot0";
            this.tbTRXPot0.Size = new System.Drawing.Size(72, 16);
            this.tbTRXPot0.TabIndex = 12;
            this.tbTRXPot0.TickFrequency = 32;
            this.tbTRXPot0.Value = 128;
            this.tbTRXPot0.Scroll += new System.EventHandler(this.tbTRXPot0_Scroll);
            // 
            // grpFreqControl
            // 
            this.grpFreqControl.Controls.Add(this.ckPAFilter6m);
            this.grpFreqControl.Controls.Add(this.ckTXFilterBypass);
            this.grpFreqControl.Controls.Add(this.ckRX1FilterBypass);
            this.grpFreqControl.Controls.Add(this.lblTXGen);
            this.grpFreqControl.Controls.Add(this.udFreq2);
            this.grpFreqControl.Controls.Add(this.lblRX1);
            this.grpFreqControl.Controls.Add(this.udFreq1);
            this.grpFreqControl.Location = new System.Drawing.Point(208, 328);
            this.grpFreqControl.Name = "grpFreqControl";
            this.grpFreqControl.Size = new System.Drawing.Size(232, 96);
            this.grpFreqControl.TabIndex = 0;
            this.grpFreqControl.TabStop = false;
            this.grpFreqControl.Text = "Freq Control";
            // 
            // ckPAFilter6m
            // 
            this.ckPAFilter6m.Image = null;
            this.ckPAFilter6m.Location = new System.Drawing.Point(128, 72);
            this.ckPAFilter6m.Name = "ckPAFilter6m";
            this.ckPAFilter6m.Size = new System.Drawing.Size(96, 16);
            this.ckPAFilter6m.TabIndex = 6;
            this.ckPAFilter6m.Text = "PA Filter 6m";
            this.ckPAFilter6m.CheckedChanged += new System.EventHandler(this.chkPAFilter6m_CheckedChanged);
            // 
            // ckTXFilterBypass
            // 
            this.ckTXFilterBypass.Image = null;
            this.ckTXFilterBypass.Location = new System.Drawing.Point(128, 56);
            this.ckTXFilterBypass.Name = "ckTXFilterBypass";
            this.ckTXFilterBypass.Size = new System.Drawing.Size(96, 16);
            this.ckTXFilterBypass.TabIndex = 5;
            this.ckTXFilterBypass.Text = "Filter Bypass";
            this.ckTXFilterBypass.CheckedChanged += new System.EventHandler(this.chkTXFilterBypass_CheckedChanged);
            // 
            // ckRX1FilterBypass
            // 
            this.ckRX1FilterBypass.Image = null;
            this.ckRX1FilterBypass.Location = new System.Drawing.Point(16, 64);
            this.ckRX1FilterBypass.Name = "ckRX1FilterBypass";
            this.ckRX1FilterBypass.Size = new System.Drawing.Size(96, 24);
            this.ckRX1FilterBypass.TabIndex = 4;
            this.ckRX1FilterBypass.Text = "Filter Bypass";
            this.ckRX1FilterBypass.CheckedChanged += new System.EventHandler(this.chkRX1FilterBypass_CheckedChanged);
            // 
            // lblTXGen
            // 
            this.lblTXGen.Image = null;
            this.lblTXGen.Location = new System.Drawing.Point(128, 16);
            this.lblTXGen.Name = "lblTXGen";
            this.lblTXGen.Size = new System.Drawing.Size(80, 16);
            this.lblTXGen.TabIndex = 3;
            this.lblTXGen.Text = "TX/Gen";
            // 
            // udFreq2
            // 
            this.udFreq2.DecimalPlaces = 6;
            this.udFreq2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.udFreq2.Location = new System.Drawing.Point(128, 32);
            this.udFreq2.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udFreq2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udFreq2.Name = "udFreq2";
            this.udFreq2.Size = new System.Drawing.Size(88, 20);
            this.udFreq2.TabIndex = 2;
            this.udFreq2.Value = new decimal(new int[] {
            6990,
            0,
            0,
            196608});
            this.udFreq2.ValueChanged += new System.EventHandler(this.udFreq2_ValueChanged);
            // 
            // lblRX1
            // 
            this.lblRX1.Image = null;
            this.lblRX1.Location = new System.Drawing.Point(16, 16);
            this.lblRX1.Name = "lblRX1";
            this.lblRX1.Size = new System.Drawing.Size(80, 16);
            this.lblRX1.TabIndex = 1;
            this.lblRX1.Text = "RX1";
            // 
            // udFreq1
            // 
            this.udFreq1.DecimalPlaces = 6;
            this.udFreq1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.udFreq1.Location = new System.Drawing.Point(16, 32);
            this.udFreq1.Maximum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.udFreq1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udFreq1.Name = "udFreq1";
            this.udFreq1.Size = new System.Drawing.Size(88, 20);
            this.udFreq1.TabIndex = 0;
            this.udFreq1.Value = new decimal(new int[] {
            70,
            0,
            0,
            65536});
            this.udFreq1.ValueChanged += new System.EventHandler(this.udFreq1_ValueChanged);
            // 
            // FLEX5000DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(816, 438);
            this.Controls.Add(this.ckPreamp);
            this.Controls.Add(this.ckTXOut);
            this.Controls.Add(this.chkRXAttn);
            this.Controls.Add(this.chkPDrvMon);
            this.Controls.Add(this.grpChanSel);
            this.Controls.Add(this.ckRX2FilterBypass);
            this.Controls.Add(this.chkRX2On);
            this.Controls.Add(this.grpTXTrace);
            this.Controls.Add(this.grpTap);
            this.Controls.Add(this.grpADC);
            this.Controls.Add(this.grpFinalBias);
            this.Controls.Add(this.grpDriverBias);
            this.Controls.Add(this.grpScanner);
            this.Controls.Add(this.grpPLL);
            this.Controls.Add(this.grpKey);
            this.Controls.Add(this.grpSwitches);
            this.Controls.Add(this.grpTRXPots);
            this.Controls.Add(this.grpFreqControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "FLEX5000DebugForm";
            this.Text = "FLEX-5000 Debug";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FLEX5000DebugForm_Closing);
            this.grpTap.ResumeLayout(false);
            this.grpTXTrace.ResumeLayout(false);
            this.grpChanSel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udTXTrace)).EndInit();
            this.grpADC.ResumeLayout(false);
            this.grpADC.PerformLayout();
            this.grpFinalBias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot6)).EndInit();
            this.grpDriverBias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPAPot2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAPot2)).EndInit();
            this.grpScanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udScanDelayB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStepB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStopB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStartB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanDelayA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStepA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStopA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScanStartA)).EndInit();
            this.grpPLL.ResumeLayout(false);
            this.grpKey.ResumeLayout(false);
            this.grpSwitches.ResumeLayout(false);
            this.grpTRXPots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTRXPot0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTRXPot0)).EndInit();
            this.grpFreqControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udFreq2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Misc Routines

		public void SetADCText(string s)
		{
			txtADCRead.Text = s;
		}

		public void SetTRXPot(int index, byte val)
		{
			switch(index)
			{
				case 0: udTRXPot0.Value = val; break;
				case 1: udTRXPot1.Value = val; break;
				case 2: udTRXPot2.Value = val; break;
				case 3: udTRXPot3.Value = val; break;
			}
		}

		public byte GetPAPot(int index)
		{
			byte ret_val = 0;
			switch(index)
			{
				case 0: ret_val = (byte)udPAPot0.Value; break;
				case 1: ret_val = (byte)udPAPot1.Value; break;
				case 2: ret_val = (byte)udPAPot2.Value; break;
				case 3: ret_val = (byte)udPAPot3.Value; break;
				case 4: ret_val = (byte)udPAPot4.Value; break;
				case 5: ret_val = (byte)udPAPot5.Value; break;
				case 6: ret_val = (byte)udPAPot6.Value; break;
				case 7: ret_val = (byte)udPAPot7.Value; break;
			}
			return ret_val;
		}

		public void SetPAPot(int index, byte val)
		{
			switch(index)
			{
				case 0: udPAPot0.Value = val; break;
				case 1: udPAPot1.Value = val; break;
				case 2: udPAPot2.Value = val; break;
				case 3: udPAPot3.Value = val; break;
				case 4: udPAPot4.Value = val; break;
				case 5: udPAPot5.Value = val; break;
				case 6: udPAPot6.Value = val; break;
				case 7: udPAPot7.Value = val; break;
			}
		}

		#endregion

		#region Event Handlers

		private void udFreq1_ValueChanged(object sender, System.EventArgs e)
		{
			/*if(FWC.SetRX1Freq((float)udFreq1.Value) == 0)
				MessageBox.Show("Error in SetRX1Freq");*/
		}

		private void udFreq2_ValueChanged(object sender, System.EventArgs e)
		{
			/*if(FWC.SetTXFreq((float)udFreq2.Value) == 0)
				MessageBox.Show("Error in SetTXFreq");*/
		}

		private void chkTest_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetTest(ckTest.Checked) == 0)
				MessageBox.Show("Error in SetTest");
		}

		private void chkGen_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetGen(ckGen.Checked) == 0)
				MessageBox.Show("Error in SetGen");
		}

		private void chkSig_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetSig(ckSig.Checked) == 0)
				MessageBox.Show("Error in SetSig");
		}

		private void chkImpulse_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetImpulse(chkImpulse.Checked) == 0)
				MessageBox.Show("Error in SetImpulse");
		}

		private void chkXVEN_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetXVEN(ckXVEN.Checked) == 0)
				MessageBox.Show("Error in SetXVEN");
		}

		private void chkQSD_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetQSD(chkQSD.Checked) == 0)
				MessageBox.Show("Error in SetQSD");
		}

		private void chkQSE_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetQSE(ckQSE.Checked) == 0)
				MessageBox.Show("Error in SetQSE");
		}

		private void chkXREF_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetXREF(chkXREF.Checked) == 0)
				MessageBox.Show("Error in SetXREF");
		}

		private void chkIntSpkr_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetIntSpkr(chkIntSpkr.Checked) == 0)
				MessageBox.Show("Error in SetIntSpkr");
		}

		private void chkRX1Tap_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRX1Tap(chkRX1Tap.Checked) == 0)
				MessageBox.Show("Error in SetRX1Tap");
		}

		private void udTRXPot0_ValueChanged(object sender, System.EventArgs e)
		{
			if(udTRXPot0.Focused || tbTRXPot0.Focused)
			{
				if(FWC.TRXPotSetRDAC(0, (int)udTRXPot0.Value) == 0)
					MessageBox.Show("Error in TRXPotSetRDAC");
			}

			tbTRXPot0.Value = (int)udTRXPot0.Value;
		}

		private void tbTRXPot0_Scroll(object sender, System.EventArgs e)
		{
			udTRXPot0.Value = tbTRXPot0.Value;
		}

		private void udTRXPot1_ValueChanged(object sender, System.EventArgs e)
		{
			if(udTRXPot1.Focused || tbTRXPot1.Focused)
			{
				if(FWC.TRXPotSetRDAC(1, (int)udTRXPot1.Value) == 0)
					MessageBox.Show("Error in TRXPotSetRDAC");
			}

			tbTRXPot1.Value = (int)udTRXPot1.Value;
		}

		private void tbTRXPot1_Scroll(object sender, System.EventArgs e)
		{
			udTRXPot1.Value = tbTRXPot1.Value;
		}

		private void udTRXPot2_ValueChanged(object sender, System.EventArgs e)
		{
			if(udTRXPot2.Focused || tbTRXPot2.Focused)
			{
				if(FWC.TRXPotSetRDAC(2, (int)udTRXPot2.Value) == 0)
					MessageBox.Show("Error in TRXPotSetRDAC");
			}

			tbTRXPot2.Value = (int)udTRXPot2.Value;
		}

		private void tbTRXPot2_Scroll(object sender, System.EventArgs e)
		{
			udTRXPot2.Value = tbTRXPot2.Value;
		}

		private void udTRXPot3_ValueChanged(object sender, System.EventArgs e)
		{
			if(udTRXPot3.Focused || tbTRXPot3.Focused)
			{
				if(FWC.TRXPotSetRDAC(3, (int)udTRXPot3.Value) == 0)
					MessageBox.Show("Error in TRXPotSetRDAC");
			}

			tbTRXPot3.Value = (int)udTRXPot3.Value;
		}

		private void tbTRXPot3_Scroll(object sender, System.EventArgs e)
		{
			udTRXPot3.Value = tbTRXPot3.Value;
		}

		private void chkRX1FilterBypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.BypassRX1Filter(ckRX1FilterBypass.Checked) == 0)
				MessageBox.Show("Error in BypassRX1Filter");
		}

		private void chkTXFilterBypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.BypassTXFilter(ckTXFilterBypass.Checked) == 0)
				MessageBox.Show("Error in BypassTXFilter");
		}

		private void chkDot_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkDot.Checked) chkDot.BackColor = Color.Yellow;
			else chkDot.BackColor = SystemColors.Control;
		}

		private void chkDash_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkDash.Checked) chkDash.BackColor = Color.Yellow;
			else chkDash.BackColor = SystemColors.Control;
		}

		private void PollKey()
		{
			bool dot, dash, rca_ptt, mic_ptt;
			while(ckKeyPoll.Checked)
			{
				if(FWC.ReadPTT(out dot, out dash, out rca_ptt, out mic_ptt) == 0)
				{
					MessageBox.Show("Error in ReadKey");
					break;
				}
				chkDot.Checked = dot;
				chkDash.Checked = dash;
				chkRCAPTT.Checked = rca_ptt;
				chkMicPTT.Checked = mic_ptt;
				Thread.Sleep(20);
			}
		}

		private void ckKeyPoll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(ckKeyPoll.Checked)
			{
				Thread t = new Thread(new ThreadStart(PollKey));
				t.Name = "Poll Key Thread";
				t.Priority = ThreadPriority.Normal;
				t.IsBackground = true;
				t.Start();
			}
		}

		private void chkHeadphone_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetHeadphone(chkHeadphone.Checked) == 0)
				MessageBox.Show("Error in SetHeadphone");
		}

		private void chkPLL_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetPLL(ckPLL.Checked) == 0)
				MessageBox.Show("Error in SetPLL");
		}

		private void chkRCATX1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRCATX1(chkRCATX1.Checked) == 0)
				MessageBox.Show("Error in SetRCATX1");
		}

		private void chkRCATX2_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRCATX2(chkRCATX2.Checked) == 0)
				MessageBox.Show("Error in SetRCATX2");
		}

		private void chkRCATX3_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRCATX3(chkRCATX3.Checked) == 0)
				MessageBox.Show("Error in SetRCATX3");
		}

		private void comboPLLRefClock_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			int val = 0;
			switch(cmboPLLRefClock.Text)
			{
				case "10": val = 0x01; break;
				case "20": val = 0x02; break;
			}
			int old_val;
			if(FWC.ReadClockReg(0x0C, out old_val) == 0)
				MessageBox.Show("Error in ReadClockReg.");
			if(old_val != val)
			{
				if(FWC.WriteClockReg(0x0C, val) == 0)
					MessageBox.Show("Error in WriteClockReg.");
			}			
		}

		private void comboPLLCPMode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			int old_val;
			if(FWC.ReadClockReg(0x08, out old_val) == 0)
				MessageBox.Show("Error in ReadClockReg");
			int val = (int)((old_val&0xFC) | (int)cmboPLLCPMode.SelectedIndex);
			if(old_val != val)
			{
				if(FWC.WriteClockReg(0x08, val) == 0)
					MessageBox.Show("Error in WriteClockReg");
			}
		}

		private void comboPLLStatusMux_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			int old_val;
			if(FWC.ReadClockReg(0x08, out old_val) == 0)
				MessageBox.Show("Error in ReadClockReg");
			int val = (int)((old_val&0xC3) | (int)(comboPLLStatusMux.SelectedIndex << 2));
			if(old_val != val)
			{
				if(FWC.WriteClockReg(0x08, val) == 0)
					MessageBox.Show("Error in WriteClockReg");
			}
		}

		private void chkPLLPFDPol_CheckedChanged(object sender, System.EventArgs e)
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			int old_val;
			if(FWC.ReadClockReg(0x08, out old_val) == 0)
				MessageBox.Show("Error in ReadClockReg");

			int val = 0;
			if(ckPLLPFDPol.Checked) val = 0x40;
			val = (int)((old_val&0xBF) | val);
			if(old_val != val)
			{
				if(FWC.WriteClockReg(0x08, val) == 0)
					MessageBox.Show("Error in WriteClockReg");
			}
		}

		private Thread poll_status_thread;
		private void ckPLLPollStatus_CheckedChanged(object sender, System.EventArgs e)
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			btnPLLStatus.Enabled = !ckPLLPollStatus.Checked;

			if(ckPLLPollStatus.Checked)
			{
				poll_status_thread = new Thread(new ThreadStart(PollStatus));
				poll_status_thread.Name = "Poll Status Thread";
				poll_status_thread.IsBackground = true;
				poll_status_thread.Priority = ThreadPriority.Normal;
				poll_status_thread.Start();
			}
		}

		private void PollStatus()
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			while(ckPLLPollStatus.Checked)
			{
				btnPLLStatus_Click(this, EventArgs.Empty);
				Thread.Sleep(250);
			}
		}

		private void btnPLLStatus_Click(object sender, System.EventArgs e)
		{
			if(console.CurrentModel != Model.FLEX5000) return;
			bool b;
			FWC.GetPLLStatus2(out b);
			if(b) btnPLLStatus.BackColor = Color.Green;
			else btnPLLStatus.BackColor = Color.Red;
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			if(flex5000LLHWForm == null || flex5000LLHWForm.IsDisposed)
				flex5000LLHWForm = new FLEX5000LLHWForm(console);
			flex5000LLHWForm.Show();
		}

		private void chkScanA_CheckedChanged(object sender, System.EventArgs e)
		{
			if(ckScanA.Checked)
			{
				ckScanA.BackColor = console.ButtonSelectedColor;
				Thread t = new Thread(new ThreadStart(ScanA));
				t.Name = "Scan A Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();
			}
			else ckScanA.BackColor = SystemColors.Control;
		}

		private void ScanA()
		{
			console.VFOAFreq = (double)udScanStartA.Value;
			while(ckScanA.Checked)
			{
				Thread.Sleep((int)udScanDelayA.Value);
				console.VFOAFreq += (double)udScanStepA.Value;
				if(console.VFOAFreq >= (double)udScanStopA.Value)
					ckScanA.Checked = false;
			}
		}

		private void chkScanB_CheckedChanged(object sender, System.EventArgs e)
		{
			if(ckScanB.Checked)
			{
				ckScanB.BackColor = console.ButtonSelectedColor;
				Thread t = new Thread(new ThreadStart(ScanB));
				t.Name = "Scan B Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();
			}
			else ckScanB.BackColor = SystemColors.Control;
		}

		private void ScanB()
		{
			console.VFOBFreq = (double)udScanStartB.Value;
			while(ckScanB.Checked)
			{
				Thread.Sleep((int)udScanDelayB.Value);
				console.VFOBFreq += (double)udScanStepB.Value;
				if(console.VFOBFreq >= (double)udScanStopB.Value)
					ckScanB.Checked = false;
			}
		}

		private void chkRX1Out_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRX1Out(chkRX1Out.Checked) == 0)
				MessageBox.Show("Error in SetRX1Out");
		}

		private void chkTR_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetTR(ckTR.Checked) == 0)
				MessageBox.Show("Error in SetTR");
		}

		private void chkTXMon_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetTXMon(ckTXMon.Checked) == 0)
				MessageBox.Show("Error in SetTXMon");
		}

		private void chkXVCOM_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetXVCOM(chkXVCOM.Checked) == 0)
				MessageBox.Show("Error in SetXVCOM");
		}

		private void chkEN2M_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetEN2M(chkEN2M.Checked) == 0)
				MessageBox.Show("Error in SetEN2M");
		}

		private void chkKEY2M_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetKey2M(chkKEY2M.Checked) == 0)
				MessageBox.Show("Error in SetKEY2M");
		}

		private void chkRCAPTT_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkRCAPTT.Checked) chkRCAPTT.BackColor = console.ButtonSelectedColor;
			else chkRCAPTT.BackColor = SystemColors.Control;
		}

		private void chkMicPTT_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkMicPTT.Checked) chkMicPTT.BackColor = console.ButtonSelectedColor;
			else chkMicPTT.BackColor = SystemColors.Control;
		}

		private void chkXVTXEN_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetXVTXEN(chkXVTXEN.Checked) == 0)
				MessageBox.Show("Error in SetXVTXEN");
		}

		private void udPAPot0_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(0, (int)udPAPot0.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot0.Value = (int)udPAPot0.Value;
		}

		private void tbPAPot0_Scroll(object sender, System.EventArgs e)
		{
			udPAPot0.Value = tbPAPot0.Value;
		}

		private void udPAPot1_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(1, 255-(int)udPAPot1.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot1.Value = (int)udPAPot1.Value;		
		}

		private void tbPAPot1_Scroll(object sender, System.EventArgs e)
		{
			udPAPot1.Value = tbPAPot1.Value;
		}

		private void udPAPot2_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(2, (int)udPAPot2.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot2.Value = (int)udPAPot2.Value;	
		}

		private void tbPAPot2_Scroll(object sender, System.EventArgs e)
		{
			udPAPot2.Value = tbPAPot2.Value;
		}

		private void udPAPot3_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(3, 255-(int)udPAPot3.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot3.Value = (int)udPAPot3.Value;	
		}

		private void tbPAPot3_Scroll(object sender, System.EventArgs e)
		{
			udPAPot3.Value = tbPAPot3.Value;
		}

		private void udPAPot4_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(4, (int)udPAPot4.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot4.Value = (int)udPAPot4.Value;	
		}

		private void tbPAPot4_Scroll(object sender, System.EventArgs e)
		{
			udPAPot4.Value = tbPAPot4.Value;
		}

		private void udPAPot5_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(5, 255-(int)udPAPot5.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot5.Value = (int)udPAPot5.Value;	
		}

		private void tbPAPot5_Scroll(object sender, System.EventArgs e)
		{
			udPAPot5.Value = tbPAPot5.Value;
		}

		private void udPAPot6_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(6, (int)udPAPot6.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot6.Value = (int)udPAPot6.Value;	
		}

		private void tbPAPot6_Scroll(object sender, System.EventArgs e)
		{
			udPAPot6.Value = tbPAPot6.Value;
		}

		private void udPAPot7_ValueChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.PAPotSetRDAC(7, 255-(int)udPAPot7.Value) == 0)
				MessageBox.Show("Error in PAPotSetRDAC");

			tbPAPot7.Value = (int)udPAPot7.Value;	
		}

		private void tbPAPot7_Scroll(object sender, System.EventArgs e)
		{
			udPAPot7.Value = tbPAPot7.Value;
		}		

		private void FLEX5000DebugForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FLEX5000DebugForm");
			if(flex5000LLHWForm != null) 
			{
				flex5000LLHWForm.Hide();
				flex5000LLHWForm.Close();
			}
		}

		private void ckPABias_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.SetPABias(ckPABias.Checked) == 0)
				MessageBox.Show("Error in SetPABias");
			if(console.CurrentModel == Model.FLEX3000 && !ckPABias.Checked)
				FWC.SetFan(false);
		}

		private void chkPAOff_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(ckPAOff.Checked)
			{
				if(FWC.SetPowerOff() == 0)
					MessageBox.Show("Error in SetPowerOff");
			}
		}

		private void chkFPLED_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetFPLED(chkFPLED.Checked) == 0)
				MessageBox.Show("Error in SetFPLED");
		}

		private void chkCTS_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.SetCTS(chkCTS.Checked) == 0)
				MessageBox.Show("Error in SetCTS");
		}

		private void chkRTS_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.SetRTS(chkRTS.Checked) == 0)
				MessageBox.Show("Error in SetRTS");
		}

		private void chkReset_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.SetPCReset(chkReset.Checked) == 0)
				MessageBox.Show("Error in SetPCReset");
		}

		private void chkPCPwr_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(FWC.SetPCPWRBT(chkPCPwr.Checked) == 0)
				MessageBox.Show("Error in SetPCPWRBT");
		}

		#endregion		

		private void button1_Click(object sender, System.EventArgs e)
		{
			Thread t = new Thread(new ThreadStart(RunImpulse));
			t.Name = "Run Impulse Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Highest;
			t.Start();
		}

		private void RunImpulse()
		{
			HiPerfTimer t1 = new HiPerfTimer();
			for(int i=0; i<100; i++)
			{
				//t1.Start();
				FWC.SetImpulse(true);				
				//t1.Stop();
				//while(t1.DurationMsec < 10) t1.Stop();
				
				//t1.Start();
				FWC.SetImpulse(false);				
				//t1.Stop();
				//while(t1.DurationMsec < 10) t1.Stop();
			}
		}

		private void chkXVTR_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetXVTR(chkXVTR.Checked) == 0)
				MessageBox.Show("Error in SetXVTR");
		}

		private void chkFan_CheckedChanged(object sender, System.EventArgs e)
		{
			/*if(FWC.SetFan(chkFan.Checked) == 0)
				MessageBox.Show("Error in SetFan");*/
		}

		private void chkIntLED_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetIntLED(chkIntLED.Checked) == 0)
				MessageBox.Show("Error in SetIntLED");
		}

		private void chkPAFilter6m_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.BypassPAFilter(ckPAFilter6m.Checked);
		}

		private void btnADCRead_Click(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(comboADCChan.Text == "") return;
			int chan = comboADCChan.SelectedIndex;

			int val;
			if(FWC.ReadPAADC(chan, out val) == 0)
				MessageBox.Show("Error in ReadPAADC");

			float volts = (float)val/4096*2.5f;
			string output = "";
			switch(comboADCChan.Text)
			{
				case "Final Bias": output = (volts*10).ToString("f3")+" A"; break;
				case "Driver Bias":
					if(((byte)(FWCEEPROM.PARev>>8)) >= 0)
						output = (volts).ToString("f3")+" A"; // 50 milliohm
					else output = (volts/2).ToString("f3")+" A"; // 100 milliohm
					break;
				case "13.8V Ref": output = (volts*11).ToString("f1")+" V"; break;
				case "Temp": 
					double temp_c = 301-volts*1000/2.2;
					switch(temp_format)
					{
						case TempFormat.Fahrenheit:
							output = ((temp_c*1.8)+32).ToString("f1")+"° F"; break;
						case TempFormat.Celsius:
							output = temp_c.ToString("f1")+"° C"; break;
					}
					break;
				default: output = volts.ToString("f3")+" V"; break;
			}
			txtADCRead.Text = output;
		}

		private void comboADCChan_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnADCRead_Click(this, EventArgs.Empty);
		}

		private void ckADCPoll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!pa_ok) return;
			if(ckADCPoll.Checked)
			{
				Thread t = new Thread(new ThreadStart(PollADC));
				t.Name = "Poll ADC Thread";
				t.Priority = ThreadPriority.Normal;
				t.IsBackground = true;
				t.Start();
			}
		}

		private void PollADC()
		{
			int val = 0;
			double save_val = 0;
			while(ckADCPoll.Checked)
			{
				if(comboADCChan.SelectedIndex >= 0)
				{
					if(FWC.ReadPAADC(comboADCChan.SelectedIndex, out val) == 0)
					{
						MessageBox.Show("Error in ReadPAADC");
						break;
					}

					double avg_val = (save_val = 0.8*save_val + 0.2*val);
					//double avg_val = val;

					double volts = (float)avg_val/4096*2.5f;
					string output;
					switch(comboADCChan.Text)
					{
						case "Final Bias": output = (volts*10).ToString("f3")+" A"; break;
						case "Driver Bias": 
							if(((byte)(FWCEEPROM.PARev>>8)) > 0)
								output = (volts).ToString("f3")+" A"; // 50 milliohm
							else output = (volts/2).ToString("f3")+" A"; // 100 milliohm
							break;
						case "13.8V Ref": output = (volts*11).ToString("f1")+" V"; break;
						case "Temp": output = (293-volts*1000/2.2f).ToString("f1")+"° C"; break;
						default: output = volts.ToString("f3")+" V"; break;
					}

					txtADCRead.Text = output;
				}
				Thread.Sleep(100);
			}
		}

		private void radTapPreDriver_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radTapPreDriver.Checked)
			{
				console.FullDuplex = true;
				FWC.SetQSD(true);
				FWC.SetQSE(true);
				FWC.SetTR(true);
				FWC.SetSig(true);
				FWC.SetGen(false);
				FWC.SetTest(true);
				FWC.SetTXMon(false);
			}
		}

		private void radTapFinal_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radTapFinal.Checked)
			{
				console.FullDuplex = true;
				FWC.SetQSD(true);
				FWC.SetQSE(true);
				FWC.SetTR(true);
				FWC.SetSig(false);
				FWC.SetGen(false);
				FWC.SetTest(false);
				FWC.SetTXMon(true);
			}
		}

		private void radTapOff_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radTapOff.Checked)
			{
				console.FullDuplex = false;
				FWC.SetQSD(true);
				FWC.SetQSE(false);	
				FWC.SetTR(false);
				FWC.SetSig(false);
				FWC.SetGen(false);
				FWC.SetTest(false);
				FWC.SetTXMon(false);
			}
		}

		private void udTXTrace_ValueChanged(object sender, System.EventArgs e)
		{
			if(ckTXTrace.Checked)
				Audio.SourceScale = (double)udTXTrace.Value;
		}

		private void chkTXTrace_CheckedChanged(object sender, System.EventArgs e)
		{
			if(ckTXTrace.Checked)
			{
				if(!console.PowerOn)
				{
					MessageBox.Show("Power must be on in order to run TX Trace.", "Power Is Off",
						MessageBoxButtons.OK, MessageBoxIcon.Stop);
					ckTXTrace.Checked = false;
					return;
				}

				ckTXTrace.BackColor = console.ButtonSelectedColor;
				console.FullDuplex = true;

				FWC.SetQSD(true);
				FWC.SetQSE(true);
				FWC.SetTR(true);
				FWC.SetSig(true);
				FWC.SetGen(false);
				FWC.SetTest(true);
				FWC.SetTXMon(false);
				FWC.SetPDrvMon(true);

				if(console.CurrentModel == Model.FLEX3000)
					FWC.SetAmpTX1(false);

				console.RX1DSPMode = DSPMode.USB;

				FWC.SetMOX(true);
				Audio.TXOutputSignal = Audio.SignalSource.SINE;
				Audio.SourceScale = (double)udTXTrace.Value;
			}
			else
			{
				FWC.SetMOX(false);
				FWC.SetQSD(true);
				FWC.SetQSE(false);	
				FWC.SetTR(false);
				FWC.SetSig(false);
				FWC.SetGen(false);
				FWC.SetTest(false);
				FWC.SetTXMon(false);
				FWC.SetPDrvMon(false);
				Audio.TXOutputSignal = Audio.SignalSource.RADIO;
				console.FullDuplex = false;
				ckTXTrace.BackColor = SystemColors.Control;
				if(console.CurrentModel == Model.FLEX3000)
				{
					FWC.SetFan(false);
					if(ckTXOut.Checked) FWC.SetAmpTX1(true);
				}
			}
		}

		private enum TempFormat
		{
			Celsius = 0,
			Fahrenheit,
		}

		private TempFormat temp_format = TempFormat.Celsius;
		private void txtADCRead_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			switch(temp_format)
			{
				case TempFormat.Celsius:
					temp_format = TempFormat.Fahrenheit;
					break;
				case TempFormat.Fahrenheit:
					temp_format = TempFormat.Celsius;
					break;
			}
			if(comboADCChan.SelectedIndex == 4)
				btnADCRead_Click(this, EventArgs.Empty);
		}

		private void chkRX2On_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRX2On(chkRX2On.Checked) == 0)
				MessageBox.Show("Error in SetRX2On");
		}

		private void ckRX2FilterBypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.BypassRX2Filter(ckRX2FilterBypass.Checked) == 0)
				MessageBox.Show("Error in BypassRX2Filter");
		}

		private void comboRXL_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Audio.IN_RX1_L = comboRXL.SelectedIndex;
		}

		private void comboRXR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Audio.IN_RX1_R = comboRXR.SelectedIndex;
		}

		private void comboTXL_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Audio.IN_TX_L = comboTXL.SelectedIndex;
		}

		private void comboTXR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Audio.IN_TX_R = comboTXR.SelectedIndex;
		}

		private void chkPDrvMon_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetPDrvMon(chkPDrvMon.Checked) == 0)
				MessageBox.Show("Error in SetPDrvMon");
		}

		private void chkRXAttn_CheckedChanged(object sender, System.EventArgs e)
		{
			if(FWC.SetRXAttn(chkRXAttn.Checked) == 0)
				MessageBox.Show("Error in SetRXAttn");
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if(console.flex3000TestForm == null || console.flex3000TestForm.IsDisposed)
				console.flex3000TestForm = new FLEX3000TestForm(console);
			console.flex3000TestForm.Show();
			console.flex3000TestForm.Focus();
		}

		private void ckTXOut_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetAmpTX1(ckTXOut.Checked);
		}

		private void ckPreamp_CheckedChanged(object sender, System.EventArgs e)
		{
			FWC.SetTRXPreamp(ckPreamp.Checked);
		}

        private VUForm vuForm;
        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (vuForm == null || vuForm.IsDisposed)
                vuForm = new VUForm();
            vuForm.Show();
            vuForm.Focus();
        }
	}
}

//=================================================================
// fwcatuform.cs
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
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
	/// <summary>
	/// Summary description for fwcatuform.
	/// </summary>
	public class FWCATUForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.GroupBoxTS grpMode;
		private System.Windows.Forms.RadioButtonTS radModeBypass;
		private System.Windows.Forms.RadioButtonTS radModeSemiAuto;
		private System.Windows.Forms.RadioButtonTS radModeAuto;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ButtonTS btnTuneMemory;
		private System.Windows.Forms.ButtonTS btnTuneFull;
		private System.Windows.Forms.GroupBoxTS grpTune;
		private System.Windows.Forms.GroupBoxTS grpFeedback;
		private System.Windows.Forms.LabelTS lblFreq;
		private System.Windows.Forms.LabelTS lblPower;
		private System.Windows.Forms.LabelTS lblReflected;
		private System.Windows.Forms.LabelTS lblSWR;
		private System.Windows.Forms.LabelTS lblForward;
		private System.Windows.Forms.LabelTS lblTuneComplete;
		private System.Windows.Forms.GroupBoxTS grpSWRThreshold;
		private System.Windows.Forms.ComboBoxTS comboSWRThresh;
		private System.Windows.Forms.CheckBoxTS chkUseTUN;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor and Destructor

		public FWCATUForm(Console c)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			console = c;
			comboSWRThresh.Text = "3.0 : 1";
			Common.RestoreForm(this, "FWCATU", false);

			if(radModeSemiAuto.Checked)
			{
				radModeBypass.Checked = true;
			}
			else if(radModeBypass.Checked)
			{
				radModeBypass_CheckedChanged(this, EventArgs.Empty);
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FWCATUForm));
			this.grpMode = new System.Windows.Forms.GroupBoxTS();
			this.radModeAuto = new System.Windows.Forms.RadioButtonTS();
			this.radModeSemiAuto = new System.Windows.Forms.RadioButtonTS();
			this.radModeBypass = new System.Windows.Forms.RadioButtonTS();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnTuneMemory = new System.Windows.Forms.ButtonTS();
			this.btnTuneFull = new System.Windows.Forms.ButtonTS();
			this.chkUseTUN = new System.Windows.Forms.CheckBoxTS();
			this.grpFeedback = new System.Windows.Forms.GroupBoxTS();
			this.lblTuneComplete = new System.Windows.Forms.LabelTS();
			this.lblSWR = new System.Windows.Forms.LabelTS();
			this.lblReflected = new System.Windows.Forms.LabelTS();
			this.lblPower = new System.Windows.Forms.LabelTS();
			this.lblFreq = new System.Windows.Forms.LabelTS();
			this.lblForward = new System.Windows.Forms.LabelTS();
			this.grpSWRThreshold = new System.Windows.Forms.GroupBoxTS();
			this.comboSWRThresh = new System.Windows.Forms.ComboBoxTS();
			this.grpTune = new System.Windows.Forms.GroupBoxTS();
			this.grpMode.SuspendLayout();
			this.grpFeedback.SuspendLayout();
			this.grpSWRThreshold.SuspendLayout();
			this.grpTune.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpMode
			// 
			this.grpMode.Controls.Add(this.radModeAuto);
			this.grpMode.Controls.Add(this.radModeSemiAuto);
			this.grpMode.Controls.Add(this.radModeBypass);
			this.grpMode.Location = new System.Drawing.Point(8, 8);
			this.grpMode.Name = "grpMode";
			this.grpMode.Size = new System.Drawing.Size(128, 120);
			this.grpMode.TabIndex = 0;
			this.grpMode.TabStop = false;
			this.grpMode.Text = "Operating Mode";
			// 
			// radModeAuto
			// 
			this.radModeAuto.Image = null;
			this.radModeAuto.Location = new System.Drawing.Point(16, 88);
			this.radModeAuto.Name = "radModeAuto";
			this.radModeAuto.TabIndex = 2;
			this.radModeAuto.Text = "Automatic";
			this.toolTip1.SetToolTip(this.radModeAuto, "Activates the ATU requiring just RF to automatically begin a tuning sequence.  Th" +
				"is works for all transmission modes.  Note that manually tuning is still possibl" +
				"e in this mode.");
			this.radModeAuto.CheckedChanged += new System.EventHandler(this.radModeAuto_CheckedChanged);
			// 
			// radModeSemiAuto
			// 
			this.radModeSemiAuto.Image = null;
			this.radModeSemiAuto.Location = new System.Drawing.Point(16, 56);
			this.radModeSemiAuto.Name = "radModeSemiAuto";
			this.radModeSemiAuto.TabIndex = 1;
			this.radModeSemiAuto.Text = "Semi-Automatic";
			this.toolTip1.SetToolTip(this.radModeSemiAuto, "Enables the ATU requiring the user to activate a tune sequence.");
			this.radModeSemiAuto.CheckedChanged += new System.EventHandler(this.radModeSemiAuto_CheckedChanged);
			// 
			// radModeBypass
			// 
			this.radModeBypass.Checked = true;
			this.radModeBypass.Image = null;
			this.radModeBypass.Location = new System.Drawing.Point(16, 24);
			this.radModeBypass.Name = "radModeBypass";
			this.radModeBypass.Size = new System.Drawing.Size(88, 24);
			this.radModeBypass.TabIndex = 0;
			this.radModeBypass.TabStop = true;
			this.radModeBypass.Text = "Bypass";
			this.toolTip1.SetToolTip(this.radModeBypass, "Effectively deactivates the ATU by unlatching all relays.");
			this.radModeBypass.CheckedChanged += new System.EventHandler(this.radModeBypass_CheckedChanged);
			// 
			// btnTuneMemory
			// 
			this.btnTuneMemory.Image = null;
			this.btnTuneMemory.Location = new System.Drawing.Point(16, 24);
			this.btnTuneMemory.Name = "btnTuneMemory";
			this.btnTuneMemory.Size = new System.Drawing.Size(88, 23);
			this.btnTuneMemory.TabIndex = 1;
			this.btnTuneMemory.Text = "Memory Tune";
			this.toolTip1.SetToolTip(this.btnTuneMemory, "Performs a Memory Tune.  Uses previously saved tune settings if found for the cur" +
				"rent frequency.  If a previous setting is not found, a Full Tune is performed.");
			this.btnTuneMemory.Click += new System.EventHandler(this.btnTuneMemory_Click);
			// 
			// btnTuneFull
			// 
			this.btnTuneFull.Image = null;
			this.btnTuneFull.Location = new System.Drawing.Point(16, 56);
			this.btnTuneFull.Name = "btnTuneFull";
			this.btnTuneFull.Size = new System.Drawing.Size(88, 23);
			this.btnTuneFull.TabIndex = 2;
			this.btnTuneFull.Text = "Full Tune";
			this.toolTip1.SetToolTip(this.btnTuneFull, "Perform Full Tune.  Ignores any previous saved tunes on the current frequency.");
			this.btnTuneFull.Click += new System.EventHandler(this.btnTuneFull_Click);
			// 
			// chkUseTUN
			// 
			this.chkUseTUN.Checked = true;
			this.chkUseTUN.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseTUN.Image = null;
			this.chkUseTUN.Location = new System.Drawing.Point(24, 88);
			this.chkUseTUN.Name = "chkUseTUN";
			this.chkUseTUN.Size = new System.Drawing.Size(80, 24);
			this.chkUseTUN.TabIndex = 3;
			this.chkUseTUN.Text = "Use TUN";
			this.toolTip1.SetToolTip(this.chkUseTUN, "Checking this box will enable the front panel TUN function when using the Memory " +
				"or Full Tune functions above.");
			// 
			// grpFeedback
			// 
			this.grpFeedback.BackColor = System.Drawing.SystemColors.Control;
			this.grpFeedback.Controls.Add(this.lblTuneComplete);
			this.grpFeedback.Controls.Add(this.lblSWR);
			this.grpFeedback.Controls.Add(this.lblReflected);
			this.grpFeedback.Controls.Add(this.lblPower);
			this.grpFeedback.Controls.Add(this.lblFreq);
			this.grpFeedback.Controls.Add(this.lblForward);
			this.grpFeedback.Location = new System.Drawing.Point(8, 136);
			this.grpFeedback.Name = "grpFeedback";
			this.grpFeedback.Size = new System.Drawing.Size(264, 96);
			this.grpFeedback.TabIndex = 5;
			this.grpFeedback.TabStop = false;
			this.grpFeedback.Text = "Tuner Feedback";
			this.toolTip1.SetToolTip(this.grpFeedback, "The information in this window is returned from the ATU after a tune sequence.");
			// 
			// lblTuneComplete
			// 
			this.lblTuneComplete.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTuneComplete.ForeColor = System.Drawing.Color.Green;
			this.lblTuneComplete.Image = null;
			this.lblTuneComplete.Location = new System.Drawing.Point(16, 24);
			this.lblTuneComplete.Name = "lblTuneComplete";
			this.lblTuneComplete.Size = new System.Drawing.Size(232, 16);
			this.lblTuneComplete.TabIndex = 7;
			this.lblTuneComplete.Text = "Tune Completed Successfully";
			// 
			// lblSWR
			// 
			this.lblSWR.Image = null;
			this.lblSWR.Location = new System.Drawing.Point(176, 48);
			this.lblSWR.Name = "lblSWR";
			this.lblSWR.Size = new System.Drawing.Size(72, 16);
			this.lblSWR.TabIndex = 3;
			this.lblSWR.Text = "SWR: 1.0:1";
			// 
			// lblReflected
			// 
			this.lblReflected.Image = null;
			this.lblReflected.Location = new System.Drawing.Point(168, 72);
			this.lblReflected.Name = "lblReflected";
			this.lblReflected.Size = new System.Drawing.Size(80, 16);
			this.lblReflected.TabIndex = 2;
			this.lblReflected.Text = "Reflected: 0";
			// 
			// lblPower
			// 
			this.lblPower.Image = null;
			this.lblPower.Location = new System.Drawing.Point(16, 72);
			this.lblPower.Name = "lblPower";
			this.lblPower.Size = new System.Drawing.Size(64, 16);
			this.lblPower.TabIndex = 1;
			this.lblPower.Text = "Power (W):   ";
			// 
			// lblFreq
			// 
			this.lblFreq.Image = null;
			this.lblFreq.Location = new System.Drawing.Point(16, 48);
			this.lblFreq.Name = "lblFreq";
			this.lblFreq.Size = new System.Drawing.Size(152, 16);
			this.lblFreq.TabIndex = 0;
			this.lblFreq.Text = "Freq (MHz): ";
			// 
			// lblForward
			// 
			this.lblForward.Image = null;
			this.lblForward.Location = new System.Drawing.Point(80, 72);
			this.lblForward.Name = "lblForward";
			this.lblForward.Size = new System.Drawing.Size(80, 16);
			this.lblForward.TabIndex = 6;
			this.lblForward.Text = "Forward: 0";
			// 
			// grpSWRThreshold
			// 
			this.grpSWRThreshold.Controls.Add(this.comboSWRThresh);
			this.grpSWRThreshold.Location = new System.Drawing.Point(280, 8);
			this.grpSWRThreshold.Name = "grpSWRThreshold";
			this.grpSWRThreshold.Size = new System.Drawing.Size(104, 56);
			this.grpSWRThreshold.TabIndex = 6;
			this.grpSWRThreshold.TabStop = false;
			this.grpSWRThreshold.Text = "SWR Threshold";
			this.toolTip1.SetToolTip(this.grpSWRThreshold, "Sets the threshold below which constitutes a successful tune.");
			// 
			// comboSWRThresh
			// 
			this.comboSWRThresh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSWRThresh.DropDownWidth = 64;
			this.comboSWRThresh.Items.AddRange(new object[] {
																"1.1 : 1",
																"1.3 : 1",
																"1.5 : 1",
																"1.7 : 1",
																"2.0 : 1",
																"2.5 : 1",
																"3.0 : 1"});
			this.comboSWRThresh.Location = new System.Drawing.Point(16, 24);
			this.comboSWRThresh.Name = "comboSWRThresh";
			this.comboSWRThresh.Size = new System.Drawing.Size(64, 21);
			this.comboSWRThresh.TabIndex = 4;
			this.comboSWRThresh.SelectedIndexChanged += new System.EventHandler(this.comboSWRThresh_SelectedIndexChanged);
			// 
			// grpTune
			// 
			this.grpTune.Controls.Add(this.chkUseTUN);
			this.grpTune.Controls.Add(this.btnTuneFull);
			this.grpTune.Controls.Add(this.btnTuneMemory);
			this.grpTune.Location = new System.Drawing.Point(144, 8);
			this.grpTune.Name = "grpTune";
			this.grpTune.Size = new System.Drawing.Size(128, 120);
			this.grpTune.TabIndex = 4;
			this.grpTune.TabStop = false;
			this.grpTune.Text = "Tuning Options";
			// 
			// FWCATUForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 238);
			this.Controls.Add(this.grpSWRThreshold);
			this.Controls.Add(this.grpFeedback);
			this.Controls.Add(this.grpTune);
			this.Controls.Add(this.grpMode);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FWCATUForm";
			this.Text = "FLEX-5000 ATU Settings";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FWCATUForm_Closing);
			this.grpMode.ResumeLayout(false);
			this.grpFeedback.ResumeLayout(false);
			this.grpSWRThreshold.ResumeLayout(false);
			this.grpTune.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties

		private FWCATUMode current_tune_mode = FWCATUMode.Bypass;
		public FWCATUMode CurrentTuneMode
		{
			get { return current_tune_mode; }
			set 
			{
				switch(value)
				{
					case FWCATUMode.Bypass:
						radModeBypass.Checked = true;
						break;
					case FWCATUMode.SemiAutomatic:
						radModeSemiAuto.Checked = true;
						break;
					case FWCATUMode.Automatic:
						radModeAuto.Checked = true;
						break;
				}
			}
		}

		#endregion

		#region Event Handlers

		private void FWCATUForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FWCATU");
		}

		public void DoBypass()
		{
			radModeBypass.Checked = true;
		}

		private void radModeBypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radModeBypass.Checked)
			{
				if(FWCATU.AutoStatus == 1)
					FWCATU.AutoTuning(false);
				if(FWCATU.Active)
					FWCATU.Activate(false);
				FWC.ATUSendCmd(9, 0, 0);
				current_tune_mode = FWCATUMode.Bypass;
				console.FWCATUBypass();
			}
			grpTune.Enabled = !radModeBypass.Checked;
		}

		private void radModeSemiAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radModeSemiAuto.Checked)
			{
				if(!FWCATU.Active)
					FWCATU.Activate(true);
				if(FWCATU.AutoStatus == 1)
					FWCATU.AutoTuning(false);
				current_tune_mode = FWCATUMode.SemiAutomatic;
			}
		}

		private void radModeAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radModeAuto.Checked)
			{
				if(!FWCATU.Active)
					FWCATU.Activate(true);
				if(FWCATU.AutoStatus == 0)
					FWCATU.AutoTuning(true);
				current_tune_mode = FWCATUMode.Automatic;
			}
		}

		public void DoTuneMemory()
		{
			radModeSemiAuto.Checked = true;
			chkUseTUN.Checked = true;
			btnTuneMemory_Click(this, EventArgs.Empty);
		}

		private void btnTuneMemory_Click(object sender, System.EventArgs e)
		{
			Thread t = new Thread(new ThreadStart(TuneMemory));
			t.Name = "Memory Tune Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void TuneMemory()
		{
			int old_tun_pwr = 50;
			btnTuneMemory.BackColor = console.ButtonSelectedColor;
			if(chkUseTUN.Checked)
			{			
				console.TUN = true;
				old_tun_pwr = console.PWR;
				console.PWR = 10;
			}
			FWCATU.MemoryTune();
			if(chkUseTUN.Checked)
			{
				console.PWR = old_tun_pwr;
				console.TUN = false;				
			}
			btnTuneMemory.BackColor = SystemColors.Control;
			UpdateFeedback();
			console.FWCATUTuned();
		}

		public void DoTuneFull()
		{
			radModeSemiAuto.Checked = true;
			chkUseTUN.Checked = true;
			btnTuneFull_Click(this, EventArgs.Empty);
		}

		private void btnTuneFull_Click(object sender, System.EventArgs e)
		{
			Thread t = new Thread(new ThreadStart(TuneFull));
			t.Name = "Full Tune Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void TuneFull()
		{
			int old_tun_pwr = 50;
			btnTuneFull.BackColor = console.ButtonSelectedColor;
			if(chkUseTUN.Checked)
			{
				console.TUN = true;
				old_tun_pwr = console.PWR;
				console.PWR = 10;
			}
			FWCATU.FullTune();
			if(chkUseTUN.Checked)
			{
				console.PWR = old_tun_pwr;
				console.TUN = false;				
			}
			btnTuneFull.BackColor = SystemColors.Control;
			UpdateFeedback();
			console.FWCATUTuned();
		}

		private void UpdateFeedback()
		{
			if(FWCATU.TunePass)
			{
				lblTuneComplete.ForeColor = Color.Green;
				lblTuneComplete.Text = "Tune Completed Successfully";
				lblFreq.Text = "Freq (MHz): "+FWCATU.TXFreq.ToString("f2");
				lblForward.Text = "Forward: "+FWCATU.ForwardPower.ToString("f0");
				lblReflected.Text = "Reflected: "+FWCATU.ReflectedPower.ToString("f0");
				lblSWR.Text = "SWR: "+FWCATU.SWR.ToString("f1");
			}
			else
			{
				lblTuneComplete.ForeColor = Color.Red;
				switch(FWCATU.TuneFail)
				{
					case 0:
						lblTuneComplete.Text = "Tune Failed: No RF Detected";
						break;
					case 1:
						lblTuneComplete.Text = "Tune Failed: RF Carrier Lost";
						break;
					case 2:
						lblTuneComplete.Text = "Tune Failed: Could Not Bring Down SWR";
						break;
				}
				lblFreq.Text = "Freq (MHz):";
				lblForward.Text = "Forward:";
				lblReflected.Text = "Reflected:";
				lblSWR.Text = "SWR:";
			}
		}

		private void comboSWRThresh_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			double swr_thresh = 0.0;
			switch((byte)comboSWRThresh.SelectedIndex)
			{
				case 0: swr_thresh = 1.1; break;
				case 1: swr_thresh = 1.3; break;
				case 2: swr_thresh = 1.5; break;
				case 3: swr_thresh = 1.7; break;
				case 4: swr_thresh = 2.0; break;
				case 5: swr_thresh = 2.5; break;
				case 6: swr_thresh = 3.0; break;
			}
			if(FWCATU.SWRThreshold != swr_thresh)
				FWCATU.SetSWRThreshold(swr_thresh);
		}

		#endregion
	}
}

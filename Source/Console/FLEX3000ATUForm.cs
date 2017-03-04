//=================================================================
// FLEX3000ATUForm.cs
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
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace PowerSDR
{
	public enum TuneResult
	{
		TuneSuccessful = 0,
		TuneOK, // above thresh
		TuneFailedHighSWR,
		TuneFailedLostRF,
		TunerNotNeeded, // good match on bypass
		TuneFailedNoRF,
		TuneAborted,
	}

	public class FLEX3000ATUForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.GroupBoxTS grpFeedback;
		private System.Windows.Forms.LabelTS lblReflected;
		private System.Windows.Forms.LabelTS lblSWR;
		private System.Windows.Forms.LabelTS lblForward;
		private System.Windows.Forms.LabelTS lblTuneComplete;
		private System.Windows.Forms.LabelTS lblByp;
		private System.Windows.Forms.LabelTS labelTS1;
		private System.Windows.Forms.LabelTS lblBypSWR;
		private System.Windows.Forms.LabelTS lblTunSWR;
		private System.Windows.Forms.LabelTS lblBypFwdPow;
		private System.Windows.Forms.LabelTS lblTunFwdPow;
		private System.Windows.Forms.LabelTS lblBypRefPow;
		private System.Windows.Forms.LabelTS lblTunRefPow;
		private System.Windows.Forms.PictureBox picSWR;
		private System.Windows.Forms.TextBox txtSWR;
		private System.Windows.Forms.PictureBox picFwdPow;
		private System.Windows.Forms.TextBox txtFwdPow;
		private System.Windows.Forms.GroupBoxTS grpSWR;
		private System.Windows.Forms.GroupBoxTS grpFwdPow;
		private System.Windows.Forms.GroupBoxTS grpRefPow;
		private System.Windows.Forms.PictureBox picRefPow;
		private System.Windows.Forms.TextBox txtRefPow;
		private System.Windows.Forms.RadioButton rdBypass;
		private System.Windows.Forms.RadioButton rdTune;
		private System.Windows.Forms.Label lblC;
		private System.Windows.Forms.Label lblL;
		private System.Windows.Forms.Label lblHiZ;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.NumericUpDown udSleepTime;
		private System.Windows.Forms.NumericUpDown udHighSWR;
		private System.Windows.Forms.NumericUpDown udOffSleep;
		private System.Windows.Forms.CheckBox chkDoNotPress;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown udTunPower;
        private Label label4;
		private System.ComponentModel.IContainer components = null;

		#endregion

		#region Constructor and Destructor

		public FLEX3000ATUForm(Console c)
		{
			InitializeComponent();
			console = c;
			lblBypSWR.Text = "";
			lblTunSWR.Text = "";
			lblBypFwdPow.Text = "";
			lblTunFwdPow.Text = "";
			lblBypRefPow.Text = "";
			lblTunRefPow.Text = "";
			rdBypass.Checked = true;
			Common.RestoreForm(this, "FLEX3000ATUForm", false);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLEX3000ATUForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpFeedback = new System.Windows.Forms.GroupBoxTS();
            this.lblHiZ = new System.Windows.Forms.Label();
            this.lblTunRefPow = new System.Windows.Forms.LabelTS();
            this.lblBypRefPow = new System.Windows.Forms.LabelTS();
            this.lblTunFwdPow = new System.Windows.Forms.LabelTS();
            this.lblBypFwdPow = new System.Windows.Forms.LabelTS();
            this.lblTunSWR = new System.Windows.Forms.LabelTS();
            this.lblBypSWR = new System.Windows.Forms.LabelTS();
            this.labelTS1 = new System.Windows.Forms.LabelTS();
            this.lblByp = new System.Windows.Forms.LabelTS();
            this.lblSWR = new System.Windows.Forms.LabelTS();
            this.lblReflected = new System.Windows.Forms.LabelTS();
            this.lblForward = new System.Windows.Forms.LabelTS();
            this.lblTuneComplete = new System.Windows.Forms.LabelTS();
            this.picSWR = new System.Windows.Forms.PictureBox();
            this.txtSWR = new System.Windows.Forms.TextBox();
            this.picFwdPow = new System.Windows.Forms.PictureBox();
            this.txtFwdPow = new System.Windows.Forms.TextBox();
            this.grpSWR = new System.Windows.Forms.GroupBoxTS();
            this.grpFwdPow = new System.Windows.Forms.GroupBoxTS();
            this.grpRefPow = new System.Windows.Forms.GroupBoxTS();
            this.picRefPow = new System.Windows.Forms.PictureBox();
            this.txtRefPow = new System.Windows.Forms.TextBox();
            this.rdBypass = new System.Windows.Forms.RadioButton();
            this.rdTune = new System.Windows.Forms.RadioButton();
            this.lblC = new System.Windows.Forms.Label();
            this.lblL = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.udSleepTime = new System.Windows.Forms.NumericUpDown();
            this.udHighSWR = new System.Windows.Forms.NumericUpDown();
            this.udOffSleep = new System.Windows.Forms.NumericUpDown();
            this.chkDoNotPress = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.udTunPower = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.grpFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSWR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFwdPow)).BeginInit();
            this.grpSWR.SuspendLayout();
            this.grpFwdPow.SuspendLayout();
            this.grpRefPow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRefPow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSleepTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHighSWR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udOffSleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTunPower)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFeedback
            // 
            this.grpFeedback.BackColor = System.Drawing.SystemColors.Control;
            this.grpFeedback.Controls.Add(this.lblHiZ);
            this.grpFeedback.Controls.Add(this.lblTunRefPow);
            this.grpFeedback.Controls.Add(this.lblBypRefPow);
            this.grpFeedback.Controls.Add(this.lblTunFwdPow);
            this.grpFeedback.Controls.Add(this.lblBypFwdPow);
            this.grpFeedback.Controls.Add(this.lblTunSWR);
            this.grpFeedback.Controls.Add(this.lblBypSWR);
            this.grpFeedback.Controls.Add(this.labelTS1);
            this.grpFeedback.Controls.Add(this.lblByp);
            this.grpFeedback.Controls.Add(this.lblSWR);
            this.grpFeedback.Controls.Add(this.lblReflected);
            this.grpFeedback.Controls.Add(this.lblForward);
            this.grpFeedback.Location = new System.Drawing.Point(16, 104);
            this.grpFeedback.Name = "grpFeedback";
            this.grpFeedback.Size = new System.Drawing.Size(208, 120);
            this.grpFeedback.TabIndex = 5;
            this.grpFeedback.TabStop = false;
            this.grpFeedback.Text = "Tuner Feedback";
            this.toolTip1.SetToolTip(this.grpFeedback, "The information in this window is returned from the ATU after a tune sequence.");
            // 
            // lblHiZ
            // 
            this.lblHiZ.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblHiZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHiZ.ForeColor = System.Drawing.Color.White;
            this.lblHiZ.Location = new System.Drawing.Point(16, 24);
            this.lblHiZ.Name = "lblHiZ";
            this.lblHiZ.Size = new System.Drawing.Size(32, 16);
            this.lblHiZ.TabIndex = 16;
            this.lblHiZ.Text = "Hi Z";
            this.lblHiZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTunRefPow
            // 
            this.lblTunRefPow.Image = null;
            this.lblTunRefPow.Location = new System.Drawing.Point(136, 96);
            this.lblTunRefPow.Name = "lblTunRefPow";
            this.lblTunRefPow.Size = new System.Drawing.Size(56, 16);
            this.lblTunRefPow.TabIndex = 15;
            this.lblTunRefPow.Text = "2.3";
            this.lblTunRefPow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBypRefPow
            // 
            this.lblBypRefPow.Image = null;
            this.lblBypRefPow.Location = new System.Drawing.Point(72, 96);
            this.lblBypRefPow.Name = "lblBypRefPow";
            this.lblBypRefPow.Size = new System.Drawing.Size(56, 16);
            this.lblBypRefPow.TabIndex = 14;
            this.lblBypRefPow.Text = "2.3";
            this.lblBypRefPow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTunFwdPow
            // 
            this.lblTunFwdPow.Image = null;
            this.lblTunFwdPow.Location = new System.Drawing.Point(136, 72);
            this.lblTunFwdPow.Name = "lblTunFwdPow";
            this.lblTunFwdPow.Size = new System.Drawing.Size(56, 16);
            this.lblTunFwdPow.TabIndex = 13;
            this.lblTunFwdPow.Text = "2.3";
            this.lblTunFwdPow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBypFwdPow
            // 
            this.lblBypFwdPow.Image = null;
            this.lblBypFwdPow.Location = new System.Drawing.Point(72, 72);
            this.lblBypFwdPow.Name = "lblBypFwdPow";
            this.lblBypFwdPow.Size = new System.Drawing.Size(56, 16);
            this.lblBypFwdPow.TabIndex = 12;
            this.lblBypFwdPow.Text = "2.3";
            this.lblBypFwdPow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTunSWR
            // 
            this.lblTunSWR.Image = null;
            this.lblTunSWR.Location = new System.Drawing.Point(136, 48);
            this.lblTunSWR.Name = "lblTunSWR";
            this.lblTunSWR.Size = new System.Drawing.Size(56, 16);
            this.lblTunSWR.TabIndex = 11;
            this.lblTunSWR.Text = "1.3 : 1";
            this.lblTunSWR.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBypSWR
            // 
            this.lblBypSWR.Image = null;
            this.lblBypSWR.Location = new System.Drawing.Point(72, 48);
            this.lblBypSWR.Name = "lblBypSWR";
            this.lblBypSWR.Size = new System.Drawing.Size(56, 16);
            this.lblBypSWR.TabIndex = 10;
            this.lblBypSWR.Text = "1.3 : 1";
            this.lblBypSWR.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelTS1
            // 
            this.labelTS1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTS1.Image = null;
            this.labelTS1.Location = new System.Drawing.Point(136, 24);
            this.labelTS1.Name = "labelTS1";
            this.labelTS1.Size = new System.Drawing.Size(56, 16);
            this.labelTS1.TabIndex = 9;
            this.labelTS1.Text = "Tuned";
            this.labelTS1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblByp
            // 
            this.lblByp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByp.Image = null;
            this.lblByp.Location = new System.Drawing.Point(72, 24);
            this.lblByp.Name = "lblByp";
            this.lblByp.Size = new System.Drawing.Size(56, 16);
            this.lblByp.TabIndex = 8;
            this.lblByp.Text = "Bypassed";
            this.lblByp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSWR
            // 
            this.lblSWR.Image = null;
            this.lblSWR.Location = new System.Drawing.Point(8, 48);
            this.lblSWR.Name = "lblSWR";
            this.lblSWR.Size = new System.Drawing.Size(56, 16);
            this.lblSWR.TabIndex = 3;
            this.lblSWR.Text = "SWR:";
            this.lblSWR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblReflected
            // 
            this.lblReflected.Image = null;
            this.lblReflected.Location = new System.Drawing.Point(8, 96);
            this.lblReflected.Name = "lblReflected";
            this.lblReflected.Size = new System.Drawing.Size(56, 16);
            this.lblReflected.TabIndex = 2;
            this.lblReflected.Text = "Ref Pow:";
            this.lblReflected.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblForward
            // 
            this.lblForward.Image = null;
            this.lblForward.Location = new System.Drawing.Point(8, 72);
            this.lblForward.Name = "lblForward";
            this.lblForward.Size = new System.Drawing.Size(56, 16);
            this.lblForward.TabIndex = 6;
            this.lblForward.Text = "Fwd Pow:";
            this.lblForward.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTuneComplete
            // 
            this.lblTuneComplete.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuneComplete.ForeColor = System.Drawing.Color.Black;
            this.lblTuneComplete.Image = null;
            this.lblTuneComplete.Location = new System.Drawing.Point(8, 80);
            this.lblTuneComplete.Name = "lblTuneComplete";
            this.lblTuneComplete.Size = new System.Drawing.Size(224, 16);
            this.lblTuneComplete.TabIndex = 7;
            this.lblTuneComplete.Text = "Tuner Bypassed";
            // 
            // picSWR
            // 
            this.picSWR.BackColor = System.Drawing.Color.Black;
            this.picSWR.Location = new System.Drawing.Point(9, 43);
            this.picSWR.Name = "picSWR";
            this.picSWR.Size = new System.Drawing.Size(69, 16);
            this.picSWR.TabIndex = 74;
            this.picSWR.TabStop = false;
            this.picSWR.Paint += new System.Windows.Forms.PaintEventHandler(this.picSWR_Paint);
            // 
            // txtSWR
            // 
            this.txtSWR.BackColor = System.Drawing.Color.Black;
            this.txtSWR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSWR.ForeColor = System.Drawing.Color.White;
            this.txtSWR.Location = new System.Drawing.Point(8, 16);
            this.txtSWR.Name = "txtSWR";
            this.txtSWR.Size = new System.Drawing.Size(72, 26);
            this.txtSWR.TabIndex = 72;
            // 
            // picFwdPow
            // 
            this.picFwdPow.BackColor = System.Drawing.Color.Black;
            this.picFwdPow.Location = new System.Drawing.Point(9, 43);
            this.picFwdPow.Name = "picFwdPow";
            this.picFwdPow.Size = new System.Drawing.Size(69, 16);
            this.picFwdPow.TabIndex = 77;
            this.picFwdPow.TabStop = false;
            this.picFwdPow.Paint += new System.Windows.Forms.PaintEventHandler(this.picFwdPow_Paint);
            // 
            // txtFwdPow
            // 
            this.txtFwdPow.BackColor = System.Drawing.Color.Black;
            this.txtFwdPow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFwdPow.ForeColor = System.Drawing.Color.White;
            this.txtFwdPow.Location = new System.Drawing.Point(8, 16);
            this.txtFwdPow.Name = "txtFwdPow";
            this.txtFwdPow.Size = new System.Drawing.Size(72, 26);
            this.txtFwdPow.TabIndex = 75;
            // 
            // grpSWR
            // 
            this.grpSWR.Controls.Add(this.picSWR);
            this.grpSWR.Controls.Add(this.txtSWR);
            this.grpSWR.Location = new System.Drawing.Point(240, 8);
            this.grpSWR.Name = "grpSWR";
            this.grpSWR.Size = new System.Drawing.Size(88, 64);
            this.grpSWR.TabIndex = 7;
            this.grpSWR.TabStop = false;
            this.grpSWR.Text = "SWR";
            // 
            // grpFwdPow
            // 
            this.grpFwdPow.Controls.Add(this.picFwdPow);
            this.grpFwdPow.Controls.Add(this.txtFwdPow);
            this.grpFwdPow.Location = new System.Drawing.Point(240, 84);
            this.grpFwdPow.Name = "grpFwdPow";
            this.grpFwdPow.Size = new System.Drawing.Size(88, 64);
            this.grpFwdPow.TabIndex = 75;
            this.grpFwdPow.TabStop = false;
            this.grpFwdPow.Text = "Fwd Pow";
            // 
            // grpRefPow
            // 
            this.grpRefPow.Controls.Add(this.picRefPow);
            this.grpRefPow.Controls.Add(this.txtRefPow);
            this.grpRefPow.Location = new System.Drawing.Point(240, 160);
            this.grpRefPow.Name = "grpRefPow";
            this.grpRefPow.Size = new System.Drawing.Size(88, 64);
            this.grpRefPow.TabIndex = 78;
            this.grpRefPow.TabStop = false;
            this.grpRefPow.Text = "Ref Pow";
            // 
            // picRefPow
            // 
            this.picRefPow.BackColor = System.Drawing.Color.Black;
            this.picRefPow.Location = new System.Drawing.Point(9, 43);
            this.picRefPow.Name = "picRefPow";
            this.picRefPow.Size = new System.Drawing.Size(69, 16);
            this.picRefPow.TabIndex = 77;
            this.picRefPow.TabStop = false;
            this.picRefPow.Paint += new System.Windows.Forms.PaintEventHandler(this.picRefPow_Paint);
            // 
            // txtRefPow
            // 
            this.txtRefPow.BackColor = System.Drawing.Color.Black;
            this.txtRefPow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefPow.ForeColor = System.Drawing.Color.White;
            this.txtRefPow.Location = new System.Drawing.Point(8, 16);
            this.txtRefPow.Name = "txtRefPow";
            this.txtRefPow.Size = new System.Drawing.Size(72, 26);
            this.txtRefPow.TabIndex = 75;
            // 
            // rdBypass
            // 
            this.rdBypass.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdBypass.Location = new System.Drawing.Point(16, 48);
            this.rdBypass.Name = "rdBypass";
            this.rdBypass.Size = new System.Drawing.Size(104, 24);
            this.rdBypass.TabIndex = 79;
            this.rdBypass.Text = "Bypass";
            this.rdBypass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdBypass.CheckedChanged += new System.EventHandler(this.rdBypass_CheckedChanged);
            // 
            // rdTune
            // 
            this.rdTune.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdTune.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTune.Location = new System.Drawing.Point(16, 16);
            this.rdTune.Name = "rdTune";
            this.rdTune.Size = new System.Drawing.Size(104, 24);
            this.rdTune.TabIndex = 80;
            this.rdTune.Text = "Tune";
            this.rdTune.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdTune.CheckedChanged += new System.EventHandler(this.rdTune_CheckedChanged);
            // 
            // lblC
            // 
            this.lblC.Location = new System.Drawing.Point(64, 248);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(40, 23);
            this.lblC.TabIndex = 81;
            this.lblC.Text = "C: ";
            // 
            // lblL
            // 
            this.lblL.Location = new System.Drawing.Point(16, 248);
            this.lblL.Name = "lblL";
            this.lblL.Size = new System.Drawing.Size(40, 23);
            this.lblL.TabIndex = 82;
            this.lblL.Text = "L: ";
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(112, 248);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(64, 16);
            this.lblTime.TabIndex = 83;
            this.lblTime.Text = "Time: ";
            // 
            // udSleepTime
            // 
            this.udSleepTime.Location = new System.Drawing.Point(176, 248);
            this.udSleepTime.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.udSleepTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udSleepTime.Name = "udSleepTime";
            this.udSleepTime.Size = new System.Drawing.Size(56, 20);
            this.udSleepTime.TabIndex = 84;
            this.udSleepTime.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // udHighSWR
            // 
            this.udHighSWR.DecimalPlaces = 1;
            this.udHighSWR.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udHighSWR.Location = new System.Drawing.Point(240, 248);
            this.udHighSWR.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            65536});
            this.udHighSWR.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.udHighSWR.Name = "udHighSWR";
            this.udHighSWR.Size = new System.Drawing.Size(48, 20);
            this.udHighSWR.TabIndex = 85;
            this.udHighSWR.Value = new decimal(new int[] {
            40,
            0,
            0,
            65536});
            // 
            // udOffSleep
            // 
            this.udOffSleep.Location = new System.Drawing.Point(296, 248);
            this.udOffSleep.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.udOffSleep.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udOffSleep.Name = "udOffSleep";
            this.udOffSleep.Size = new System.Drawing.Size(48, 20);
            this.udOffSleep.TabIndex = 86;
            this.udOffSleep.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chkDoNotPress
            // 
            this.chkDoNotPress.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoNotPress.Location = new System.Drawing.Point(16, 272);
            this.chkDoNotPress.Name = "chkDoNotPress";
            this.chkDoNotPress.Size = new System.Drawing.Size(104, 24);
            this.chkDoNotPress.TabIndex = 87;
            this.chkDoNotPress.Text = "Long Test";
            this.chkDoNotPress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoNotPress.CheckedChanged += new System.EventHandler(this.chkDoNotPress_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Off Sleep";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Settle Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "SWR Limit";
            // 
            // udTunPower
            // 
            this.udTunPower.Location = new System.Drawing.Point(176, 308);
            this.udTunPower.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udTunPower.Name = "udTunPower";
            this.udTunPower.Size = new System.Drawing.Size(48, 20);
            this.udTunPower.TabIndex = 91;
            this.udTunPower.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 92;
            this.label4.Text = "Tune Power";
            // 
            // FLEX3000ATUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 232);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.udTunPower);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkDoNotPress);
            this.Controls.Add(this.udOffSleep);
            this.Controls.Add(this.udHighSWR);
            this.Controls.Add(this.udSleepTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblL);
            this.Controls.Add(this.lblC);
            this.Controls.Add(this.rdTune);
            this.Controls.Add(this.rdBypass);
            this.Controls.Add(this.grpRefPow);
            this.Controls.Add(this.grpFwdPow);
            this.Controls.Add(this.grpSWR);
            this.Controls.Add(this.grpFeedback);
            this.Controls.Add(this.lblTuneComplete);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FLEX3000ATUForm";
            this.Text = "FLEX-3000 ATU Settings";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FWCATUForm_Closing);
            this.grpFeedback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSWR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFwdPow)).EndInit();
            this.grpSWR.ResumeLayout(false);
            this.grpSWR.PerformLayout();
            this.grpFwdPow.ResumeLayout(false);
            this.grpFwdPow.PerformLayout();
            this.grpRefPow.ResumeLayout(false);
            this.grpRefPow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRefPow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSleepTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHighSWR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udOffSleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTunPower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
						rdBypass.Checked = true;
						break;
					default:
						break;
				}
			}
		}

		private bool show_feedback_popup = true;
		public bool ShowFeedbackPopup
		{
			get { return show_feedback_popup; }
			set { show_feedback_popup = value; }
		}

		#endregion

		#region Event Handlers

		private void FWCATUForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FLEX3000ATUForm");
		}

		private bool abort = false;
		public void AbortTune()
		{
			if(rdTune.Checked)
				abort = true;
		}

		public void DoBypass()
		{
			rdBypass.Checked = true;
		}

		private void rdBypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rdBypass.Checked)
			{
				rdBypass.BackColor = console.ButtonSelectedColor;
				rdTune.Checked = false;
				current_tune_mode = FWCATUMode.Bypass;
				if(console.MOX)
				{
					int power = console.PWR;
					console.PWR = 0;
					Thread.Sleep(50);
					FWC.SetATUEnable(false);
					console.PWR = power;
				}
				else FWC.SetATUEnable(false);

				console.FWCATUBypass();
			}
			else rdBypass.BackColor = SystemColors.Control;
		}

		public void DoTune()
		{
			rdTune.Checked = true;
		}

		private void rdTune_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rdTune.Checked)
			{
				rdTune.BackColor = console.ButtonSelectedColor;

				Thread t = new Thread(new ThreadStart(Tune));
				t.Name = "Tune Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();

				Thread t2 = new Thread(new ThreadStart(UpdateMeters));
				t2.Name = "Update Meters Thread";
				t2.IsBackground = true;
				t2.Priority = ThreadPriority.BelowNormal;
				t2.Start();

				lblTuneComplete.ForeColor = Color.Black;
				lblTuneComplete.Text = "Tuning...";
			}
			else rdTune.BackColor = SystemColors.Control;
		}

		private bool tuning = false;
		private TuneResult last_tune_result = TuneResult.TuneSuccessful;
		private void Tune()
		{
			int fwd, rev;
			HiPerfTimer t1 = new HiPerfTimer();
			t1.Start();

			//const int SLEEP_TIME = 300;
			int SLEEP_TIME = (int)udSleepTime.Value;
			double HIGH_SWR = (double)udHighSWR.Value;
			double swr_thresh = 1.1; // start at 1.1, switch to 1.5 after 5 sec.
			int OFF_SLEEP = (int)udOffSleep.Value;
			int MaxL = 256;
			int MaxC = 128;
			int TUN_LVL = (int)udTunPower.Value;

			console.atu_tuning = true;

			// check in bypass
			FWC.SetATUEnable(false);
			FWC.SetHiZ(false);
			Thread.Sleep(50);
			lblHiZ.Visible = false;
			int old_power = console.TunePower;
			console.TunePower = TUN_LVL;		
			console.TUN = true;
			tuning = true;
			Thread.Sleep(500);
			if(abort)
			{
				last_tune_result = TuneResult.TuneAborted;
				goto end;
			}

			// if the match is good enough, then just leave it bypassed
			FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
			FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
			byp_fwd_pow = console.FWCPAPower(console.pa_fwd_power);
			byp_ref_pow = console.FWCPAPower(console.pa_rev_power)*console.atu_swr_table[(int)console.TXBand];
			byp_swr = SWR(console.pa_fwd_power, console.pa_rev_power);
			tun_swr = byp_swr;

			if(byp_fwd_pow < 1)
			{
				rdBypass.Checked = true;
				last_tune_result = TuneResult.TuneFailedNoRF;
				goto end;
			}

			if(byp_swr < swr_thresh)
			{
				rdBypass.Checked = true;
				last_tune_result = TuneResult.TunerNotNeeded;
				goto end;
			}
			else if(byp_swr > HIGH_SWR)
			{
				rdBypass.Checked = true;
				last_tune_result = TuneResult.TuneFailedHighSWR;
				goto end;
			}

			// check HiZ
			console.TunePower = 0;
			Thread.Sleep(OFF_SLEEP);
			FWC.SetATUEnable(true);
			Thread.Sleep(OFF_SLEEP);
			SetC(8);
			SetL(8);
			FWC.SetHiZ(false);	
			console.TunePower = TUN_LVL;
			Thread.Sleep(SLEEP_TIME);
			if(abort)
			{
				last_tune_result = TuneResult.TuneAborted;
				goto end;
			}

			FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
			FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
			double lo_z_swr = SWR(console.pa_fwd_power, console.pa_rev_power);
			tun_swr = lo_z_swr;

			console.TunePower = 0;
			Thread.Sleep(OFF_SLEEP);
			FWC.SetHiZ(true);
			Thread.Sleep(OFF_SLEEP);
			console.TunePower = TUN_LVL;
			Thread.Sleep(SLEEP_TIME);
			if(abort)
			{
				last_tune_result = TuneResult.TuneAborted;
				goto end;
			}

			FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
			FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
			double hi_z_swr = SWR(console.pa_fwd_power, console.pa_rev_power);
			tun_swr = hi_z_swr;
			Debug.WriteLine("lo: "+lo_z_swr.ToString("f1")+"  hi: "+hi_z_swr.ToString("f1"));

			double min_swr = double.MaxValue;
			if(hi_z_swr < lo_z_swr)
			{
				lblHiZ.Visible = true;
				min_swr = hi_z_swr;
			}
			else
			{
				lblHiZ.Visible = false;
				console.TunePower = 0;
				Thread.Sleep(OFF_SLEEP);
				FWC.SetHiZ(false);
				Thread.Sleep(OFF_SLEEP);
				console.TunePower = TUN_LVL;
				min_swr = lo_z_swr;
			}

			tun_fwd_pow = console.FWCPAPower(console.pa_fwd_power);
			if(byp_fwd_pow < 1)
			{
				rdBypass.Checked = true;
				last_tune_result = TuneResult.TuneFailedLostRF;
				goto end;
			}

			if(min_swr < swr_thresh)
			{
				rdTune.Checked = false;
				last_tune_result = TuneResult.TuneSuccessful;
				goto end;
			}

			console.TunePower = 0;
			Thread.Sleep(OFF_SLEEP);
			SetL(0);
			SetC(0);
			Thread.Sleep(OFF_SLEEP);
			console.TunePower = TUN_LVL;
			Thread.Sleep(SLEEP_TIME);
			if(abort)
			{
				rdBypass.Checked = true;
				last_tune_result = TuneResult.TuneAborted;
				goto end;
			}

			int count = 0;
			int l_step = 8;
			int c_step = 8;
			int min_l = 0, L = 0;
			int min_c = 0, C = 0;
			int no_progress = 0;

			FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
			FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
			min_swr = SWR(console.pa_fwd_power, console.pa_rev_power);
			bool first_time = true;
			
			while(rdTune.Checked)
			{
				double start_swr = min_swr;
				while(L >= 0 && L <= MaxL && rdTune.Checked)
				{
					if(!first_time) Thread.Sleep(SLEEP_TIME);
					else first_time = false;
					if(abort)
					{
						rdBypass.Checked = true;
						last_tune_result = TuneResult.TuneAborted;
						goto end;
					}

					t1.Stop();
					if(t1.Duration > 5.0 && swr_thresh < 1.5)
						swr_thresh = 1.5;
					if(t1.Duration > 15.0) no_progress = 100;

					FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
					FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
					tun_fwd_pow = console.FWCPAPower(console.pa_fwd_power);
					tun_ref_pow = console.FWCPAPower(console.pa_rev_power)*console.atu_swr_table[(int)console.TXBand];
					if(byp_fwd_pow < 1)
					{
						rdBypass.Checked = true;
						last_tune_result = TuneResult.TuneFailedLostRF;
						goto end;
					}

					tun_swr = SWR(console.pa_fwd_power, console.pa_rev_power);
					Debug.WriteLine("swr ("+L+", "+C+"): "+tun_swr.ToString("f1")+"  min: "+min_swr.ToString("f1")+"  start: "+start_swr.ToString("f1"));
					if(tun_swr < swr_thresh) 
					{
						rdTune.Checked = false;
						last_tune_result = TuneResult.TuneSuccessful;
						goto end;
					}

					if(tun_swr < min_swr)
					{
						min_swr = tun_swr;
						min_l = L;
					}

					if(tun_swr > min_swr+0.3)
					{
						l_step *= -1;
						break;
					}

					if(count++ * Math.Abs(l_step) > 32 && min_swr >= start_swr-0.05)
						break;

					if(!rdTune.Checked)
						break;

					console.TunePower = 0;
					Thread.Sleep(OFF_SLEEP);
					SetL(L+=l_step);
					Thread.Sleep(OFF_SLEEP);
					console.TunePower = TUN_LVL;
				}

				console.TunePower = 0;
				Thread.Sleep(OFF_SLEEP);
				SetL(min_l);
				Thread.Sleep(OFF_SLEEP);
				console.TunePower = TUN_LVL;
				L = min_l;

				if(min_swr >= start_swr-0.05)
					no_progress++;

				if(no_progress > 6)
				{
					rdTune.Checked = false;
					last_tune_result = TuneResult.TuneOK;
					goto end;
				}

				Debug.Write("C");

				count = 0;
				start_swr = min_swr;

				while(C >= 0 && C <= MaxC && rdTune.Checked)
				{
					Thread.Sleep(SLEEP_TIME);
					if(abort)
					{
						rdBypass.Checked = true;
						last_tune_result = TuneResult.TuneAborted;
						goto end;
					}

					t1.Stop();
					if(t1.Duration > 5.0 && swr_thresh < 1.5)
						swr_thresh = 1.5;
					if(t1.Duration > 15.0) no_progress = 100;

					FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
					FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
					tun_fwd_pow = console.FWCPAPower(console.pa_fwd_power);
					tun_ref_pow = console.FWCPAPower(console.pa_rev_power)*console.atu_swr_table[(int)console.TXBand];
					if(byp_fwd_pow < 1)
					{
						rdBypass.Checked = true;
						last_tune_result = TuneResult.TuneFailedLostRF;
						goto end;
					}

					tun_swr = SWR(console.pa_fwd_power, console.pa_rev_power);
					Debug.WriteLine("swr ("+L+", "+C+"): "+tun_swr.ToString("f1")+"  min: "+min_swr.ToString("f1")+"  start: "+start_swr.ToString("f1"));
					if(tun_swr < swr_thresh) 
					{
						rdTune.Checked = false;
						last_tune_result = TuneResult.TuneSuccessful;
						goto end;
					}

					if(tun_swr < min_swr)
					{
						min_swr = tun_swr;
						min_c = C;
					}

					if(tun_swr > min_swr+0.3)
					{
						c_step *= -1;
						break;
					}

					if(count++ * Math.Abs(c_step) > 32 && min_swr >= start_swr-0.05)
						break;

					if(!rdTune.Checked)
						break;

					console.TunePower = 0;
					Thread.Sleep(OFF_SLEEP);
					SetC(C+=c_step);
					Thread.Sleep(OFF_SLEEP);
					console.TunePower = TUN_LVL;
				}

				console.TunePower = 0;
				Thread.Sleep(OFF_SLEEP);
				SetC(min_c);
				Thread.Sleep(OFF_SLEEP);
				console.TunePower = TUN_LVL;
				C = min_c;
				count = 0;

				if(min_swr >= start_swr-0.05)
					no_progress++;

				if(no_progress > 6)
				{
					rdTune.Checked = false;
					if(byp_swr > tun_swr)
					{
						last_tune_result = TuneResult.TuneOK;
					}
					else
					{
						last_tune_result = TuneResult.TuneFailedHighSWR;
						rdBypass.Checked = true;
					}
					goto end;
				}

				if(Math.Abs(l_step) > 1) l_step /= 2;
				if(Math.Abs(c_step) > 1) c_step /= 2;

				Debug.Write("L");
			}
			
		end:
			switch(last_tune_result)
			{
				case TuneResult.TuneSuccessful:
				case TuneResult.TuneOK:
					Thread.Sleep(SLEEP_TIME);
					FWC.ReadPAADC(5, out fwd); console.pa_fwd_power = fwd;
					FWC.ReadPAADC(4, out rev); console.pa_rev_power = rev;
					tun_fwd_pow = console.FWCPAPower(console.pa_fwd_power);
					tun_ref_pow = console.FWCPAPower(console.pa_rev_power)*console.atu_swr_table[(int)console.TXBand];
					tun_swr = SWR(console.pa_fwd_power, console.pa_rev_power);

					if(tun_swr > byp_swr)
					{
						last_tune_result = TuneResult.TunerNotNeeded;
						rdBypass.Checked = true;
					}
					Thread.Sleep(100);
					break;
			}
			//Debug.WriteLine("swr: "+swr.ToString("f1"));
			// cleanup
			tuning = false;
			console.TUN = false;
			console.TunePower = old_power;
			rdTune.Checked = false;
			console.atu_tuning = false;
			abort = false;

			//Invoke(new MethodInvoker(UpdateFeedback));
			UpdateFeedback();

			t1.Stop();
			lblTime.Text = "Time: "+t1.Duration.ToString("f1");
		}

		public double SWR(int adc_fwd, int adc_rev)
		{
			double f = console.FWCPAPower(adc_fwd);
			double r = console.FWCPAPower(adc_rev)*console.atu_swr_table[(int)console.TXBand];
			//Debug.WriteLine("FWCSWR: fwd:"+adc_fwd+" rev:"+adc_rev+" f:"+f.ToString("f2")+" r:"+r.ToString("f2"));
			
			if(adc_fwd == 0 && adc_rev == 0) return 1.0;
			if(r > f) return 50.0;

			double sqrt_r_over_f = Math.Sqrt(r/f);
			double swr = (1.0+sqrt_r_over_f)/(1.0-sqrt_r_over_f);
			if(swr < 1.0) swr = 1.0;
			if(swr > 50.0) swr = 50.0;
			return swr;
		}

		private byte SwapBits(byte b)
		{
			byte temp=b, result=0;

			for(int i=0; i<8; i++)
			{
				result <<= 1;
				if((temp&1) == 1) result += 1;
				temp >>= 1;
			}

			return result;
		}

		private void SetL(int val)
		{
			if(val > 255 || val < 0) return;
			FWC.I2C_Write2Value(0x4E, 0x02, SwapBits((byte)val));
			lblL.Text = "L: "+val.ToString();
		}

		private void SetC(int val)
		{
			if(val > 128 || val < 0) return;
			FWC.I2C_Write2Value(0x4E, 0x03, (byte)val);
			lblC.Text = "C: "+val.ToString();
		}

		private void UpdateMeters()
		{
			while(rdTune.Checked || tuning)
			{
				// get swr
				/*double swr = console.FWCSWR(console.pa_fwd_power, console.pa_rev_power);
				if(saved_swr == 999999.0)
					saved_swr = swr;
				else saved_swr = saved_swr * 0.8 + swr * 0.2;*/
				txtSWR.Text = tun_swr.ToString("f1")+" : 1";

				// get fwd pow
				double fwd_pow = console.FWCPAPower(console.pa_fwd_power);
				if(saved_fwd_pow == 999999.0)
					saved_fwd_pow = fwd_pow;
				else saved_fwd_pow = saved_fwd_pow * 0.8 + fwd_pow * 0.2;
				txtFwdPow.Text = saved_fwd_pow.ToString("f1")+" W";

				// get ref pow
				double ref_pow = console.FWCPAPower(console.pa_rev_power)*console.atu_swr_table[(int)console.TXBand];
				if(saved_ref_pow == 999999.0)
					saved_ref_pow = ref_pow;
				else saved_ref_pow = saved_ref_pow * 0.8 + ref_pow * 0.2;
				txtRefPow.Text = saved_ref_pow.ToString("f1")+" W";

				picSWR.Invalidate();
				picFwdPow.Invalidate();
				picRefPow.Invalidate();

				Thread.Sleep(50);
			}
		}

		private double byp_swr = 1.0;
		private double tun_swr = 1.0;
		private double byp_fwd_pow = 0.0;
		private double tun_fwd_pow = 0.0;
		private double byp_ref_pow = 0.0;
		private double tun_ref_pow = 0.0;
		private void UpdateFeedback()
		{
			switch(last_tune_result)
			{
				case TuneResult.TuneSuccessful:
					console.FWCATUTuned();
					lblTuneComplete.ForeColor = Color.Green;
					lblTuneComplete.Text = "Tune Completed Successfully";

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = tun_swr.ToString("f1")+" : 1";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = tun_fwd_pow.ToString("f1");
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = tun_ref_pow.ToString("f1");

					if(show_feedback_popup)
						MessageBox.Show("Tune Completed Successfully ("+tun_swr.ToString("f1")+":1)", "ATU Feedback",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
					console.SetATUFeedback("Tune Completed Successfully ("+tun_swr.ToString("f1")+":1)");
					break;
				case TuneResult.TuneOK:
					console.FWCATUTuned();
					if(tun_swr < 2.0)
					{
						lblTuneComplete.ForeColor = Color.Green;
						lblTuneComplete.Text = "Tune OK: Above Threshold";
						if(show_feedback_popup)
							MessageBox.Show("Tune OK ("+tun_swr.ToString("f1")+":1)", "ATU Feedback",
								MessageBoxButtons.OK, MessageBoxIcon.Information);
						console.SetATUFeedback("Tune OK ("+tun_swr.ToString("f1")+":1)");
					}
					else
					{
						lblTuneComplete.ForeColor = Color.Red;
						lblTuneComplete.Text = "Tune Failed: High SWR";
						if(show_feedback_popup)
							MessageBox.Show("Tune Failed ("+tun_swr.ToString("f1")+":1)", "ATU Feedback",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						console.SetATUFeedback("Tune Failed ("+tun_swr.ToString("f1")+":1)");
					}

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = tun_swr.ToString("f1")+" : 1";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = tun_fwd_pow.ToString("f1");
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = tun_ref_pow.ToString("f1");
					break;
				case TuneResult.TunerNotNeeded:
					console.FWCATUBypass();
					lblTuneComplete.ForeColor = Color.Green;
					lblTuneComplete.Text = "Tuner Bypassed: Good match";

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = "";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = "";
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = "";
					if(show_feedback_popup)
						MessageBox.Show("Tuner Bypassed: Good match ("+tun_swr.ToString("f1")+":1)", "ATU Feedback",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
					console.SetATUFeedback("Tuner Bypassed: Good match ("+tun_swr.ToString("f1")+":1)");
					break;
				case TuneResult.TuneAborted:
					console.FWCATUBypass();
					lblTuneComplete.ForeColor = Color.Orange;
					lblTuneComplete.Text = "Tune Aborted";

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = "";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = "";
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = "";
					if(show_feedback_popup)
						MessageBox.Show("Tune Aborted, Bypassed", "ATU Feedback",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					console.SetATUFeedback("Tune Aborted, Bypassed");
					break;
				case TuneResult.TuneFailedHighSWR:
					console.FWCATUFailed();
					lblTuneComplete.ForeColor = Color.Red;
					lblTuneComplete.Text = "Tune Failed: SWR Out of Range (16.7 - 150O)";

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = "";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = "";
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = "";
					if(show_feedback_popup)
						MessageBox.Show("Tune Failed: SWR Out of Range (16.7 - 150O)", "ATU Feedback",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					console.SetATUFeedback("Tune Failed: SWR Out of Range (16.7 - 150O)");
					break;
				case TuneResult.TuneFailedLostRF:
					console.FWCATUFailed();
					lblTuneComplete.ForeColor = Color.Red;
					lblTuneComplete.Text = "Tune Failed: RF Carrier Lost";

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = "";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = "";
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = "";
					if(show_feedback_popup)
						MessageBox.Show("Tune Failed: RF Carrier Lost", "ATU Feedback",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					console.SetATUFeedback("Tune Failed: RF Carrier Lost");
					break;
				case TuneResult.TuneFailedNoRF:
					console.FWCATUFailed();
					lblTuneComplete.ForeColor = Color.Red;
					lblTuneComplete.Text = "Tune Failed: No RF Detected";

					lblBypSWR.Text = byp_swr.ToString("f1")+" : 1";
					lblTunSWR.Text = "";
					lblBypFwdPow.Text = byp_fwd_pow.ToString("f1");
					lblTunFwdPow.Text = "";
					lblBypRefPow.Text = byp_ref_pow.ToString("f1");
					lblTunRefPow.Text = "";
					if(show_feedback_popup)
						MessageBox.Show("Tune Failed: No RF Detected", "ATU Feedback",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					console.SetATUFeedback("Tune Failed: No RF Detected");
					break;
			}
		}

		#endregion

		//private double saved_swr = 999999.0;
		private void picSWR_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int W = picSWR.Width;
			int H = picSWR.Height;
			if(tuning)
			{
				/*// draw threshold
				int x = (int)((swr_thresh-1) / 2.0 * W);
				e.Graphics.DrawLine(Pens.Red, x, 0, x, H);*/

				// draw current value
				int x = (int)((tun_swr-1) / 2.0 * W);
				if(x < 1) x = 1;
				if(x > W-1) x = W-1;
				e.Graphics.DrawLine(Pens.Gold, x-1, 1, x-1, H-1);
				e.Graphics.DrawLine(Pens.Yellow, x, 1, x, H-1);
				e.Graphics.DrawLine(Pens.Gold, x+1, 1, x+1, H-1);
			}
			else
			{
				/*// draw threshold
				int x = (int)((swr_thresh-1) / 2.0 * W);
				e.Graphics.DrawLine(Pens.Red, x, 0, x, H);*/
			}
		}

		private double saved_fwd_pow = 999999.0;
		private void picFwdPow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int W = picFwdPow.Width;
			int H = picFwdPow.Height;
			if(tuning)
			{				
				int x = (int)((saved_fwd_pow) / 10.0 * W);
				if(x < 1) x = 1;
				if(x > W-1) x = W-1;
				e.Graphics.DrawLine(Pens.Gold, x-1, 0, x-1, H);
				e.Graphics.DrawLine(Pens.Yellow, x, 0, x, H);
				e.Graphics.DrawLine(Pens.Gold, x+1, 0, x+1, H);
			}
			else
			{
				e.Graphics.FillRectangle(Brushes.Black, 0, 0, W, H);
			}
		}

		private double saved_ref_pow = 999999.0;
		private void picRefPow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int W = picRefPow.Width;
			int H = picRefPow.Height;
			if(tuning)
			{				
				int x = (int)((saved_ref_pow) / 10.0 * W);
				if(x < 1) x = 1;
				if(x > W-1) x = W-1;
				e.Graphics.DrawLine(Pens.Gold, x-1, 0, x-1, H);
				e.Graphics.DrawLine(Pens.Yellow, x, 0, x, H);
				e.Graphics.DrawLine(Pens.Gold, x+1, 0, x+1, H);
			}
			else
			{
				e.Graphics.FillRectangle(Brushes.Black, 0, 0, W, H);
			}
		}

		private void chkDoNotPress_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkDoNotPress.Checked)
			{
				chkDoNotPress.BackColor = console.ButtonSelectedColor;

				Thread t = new Thread(new ThreadStart(LongTest));
				t.Name = "Long Test Thread";
				t.IsBackground = true;
				t.Priority = ThreadPriority.Normal;
				t.Start();
			}
			else
			{
				chkDoNotPress.BackColor = SystemColors.Control;
			}
		}

		private void LongTest()
		{
			while(chkDoNotPress.Checked)
			{
				if(!rdTune.Checked)
				{
					Thread.Sleep(10000);
					rdTune.Checked = true;
				}
				else Thread.Sleep(1000);
			}
		}
	}
}

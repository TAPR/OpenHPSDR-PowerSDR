//=================================================================
// fwcmixform.cs
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
using System.Windows.Forms;

namespace PowerSDR
{
	/// <summary>
	/// Summary description for fwcmixform.
	/// </summary>
	public class FWCMixForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.TrackBarTS tbMic;
		private System.Windows.Forms.GroupBoxTS grpInput;
		private System.Windows.Forms.LabelTS lblMic;
		private System.Windows.Forms.GroupBoxTS grpOutput;
		private System.Windows.Forms.LabelTS lblIntSpkr;
		private System.Windows.Forms.CheckBoxTS chkInputMuteAll;
		private System.Windows.Forms.CheckBoxTS chkMicSel;
		private System.Windows.Forms.LabelTS lblLineInDB9;
		private System.Windows.Forms.CheckBoxTS chkLineInDB9Sel;
		private System.Windows.Forms.TrackBarTS tbLineInDB9;
		private System.Windows.Forms.LabelTS lblLineInPhono;
		private System.Windows.Forms.CheckBoxTS chkLineInPhonoSel;
		private System.Windows.Forms.LabelTS lblLineInRCA;
		private System.Windows.Forms.CheckBoxTS chkLineInRCASel;
		private System.Windows.Forms.TrackBarTS tbLineInRCA;
		private System.Windows.Forms.CheckBoxTS chkIntSpkrSel;
		private System.Windows.Forms.TrackBarTS tbIntSpkr;
		private System.Windows.Forms.CheckBoxTS chkOutputMuteAll;
		private System.Windows.Forms.LabelTS lblExtSpkr;
		private System.Windows.Forms.CheckBoxTS chkExtSpkrSel;
		private System.Windows.Forms.TrackBarTS tbExtSpkr;
		private System.Windows.Forms.LabelTS lblLineOutRCA;
		private System.Windows.Forms.TrackBarTS tbLineOutRCA;
		private System.Windows.Forms.LabelTS lblHeadphone;
		private System.Windows.Forms.CheckBoxTS chkHeadphoneSel;
		private System.Windows.Forms.TrackBarTS tbHeadphone;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TrackBarTS tbLineInPhono;
		private System.Windows.Forms.CheckBoxTS chkLineOutRCASel;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor and Destructor

		public FWCMixForm(Console c)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			console = c;

			if(FWCEEPROM.Model == 0)
			{
				lblIntSpkr.Enabled = false;
				tbIntSpkr.Enabled = false;
				chkIntSpkrSel.Enabled = false;
			}

			Common.RestoreForm(this, "FWCMixer", false);

			chkMicSel_CheckedChanged(this, EventArgs.Empty);
			chkLineInRCASel_CheckedChanged(this, EventArgs.Empty);
			chkLineInPhonoSel_CheckedChanged(this, EventArgs.Empty);
			chkLineInDB9Sel_CheckedChanged(this, EventArgs.Empty);
			chkIntSpkrSel_CheckedChanged(this, EventArgs.Empty);
			chkExtSpkrSel_CheckedChanged(this, EventArgs.Empty);
			chkHeadphoneSel_CheckedChanged(this, EventArgs.Empty);
			chkLineOutRCASel_CheckedChanged(this, EventArgs.Empty);
			tbMic_Scroll(this, EventArgs.Empty);
			tbLineInRCA_Scroll(this, EventArgs.Empty);
			tbLineInPhono_Scroll(this, EventArgs.Empty);
			tbLineInDB9_Scroll(this, EventArgs.Empty);
			tbIntSpkr_Scroll(this, EventArgs.Empty);
			tbExtSpkr_Scroll(this, EventArgs.Empty);
			tbHeadphone_Scroll(this, EventArgs.Empty);
			tbLineOutRCA_Scroll(this, EventArgs.Empty);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FWCMixForm));
            this.tbMic = new System.Windows.Forms.TrackBarTS();
            this.chkMicSel = new System.Windows.Forms.CheckBoxTS();
            this.grpInput = new System.Windows.Forms.GroupBoxTS();
            this.chkInputMuteAll = new System.Windows.Forms.CheckBoxTS();
            this.lblLineInDB9 = new System.Windows.Forms.LabelTS();
            this.chkLineInDB9Sel = new System.Windows.Forms.CheckBoxTS();
            this.tbLineInDB9 = new System.Windows.Forms.TrackBarTS();
            this.lblLineInPhono = new System.Windows.Forms.LabelTS();
            this.chkLineInPhonoSel = new System.Windows.Forms.CheckBoxTS();
            this.tbLineInPhono = new System.Windows.Forms.TrackBarTS();
            this.lblLineInRCA = new System.Windows.Forms.LabelTS();
            this.chkLineInRCASel = new System.Windows.Forms.CheckBoxTS();
            this.tbLineInRCA = new System.Windows.Forms.TrackBarTS();
            this.lblMic = new System.Windows.Forms.LabelTS();
            this.grpOutput = new System.Windows.Forms.GroupBoxTS();
            this.lblLineOutRCA = new System.Windows.Forms.LabelTS();
            this.chkLineOutRCASel = new System.Windows.Forms.CheckBoxTS();
            this.tbLineOutRCA = new System.Windows.Forms.TrackBarTS();
            this.lblHeadphone = new System.Windows.Forms.LabelTS();
            this.chkHeadphoneSel = new System.Windows.Forms.CheckBoxTS();
            this.tbHeadphone = new System.Windows.Forms.TrackBarTS();
            this.lblExtSpkr = new System.Windows.Forms.LabelTS();
            this.chkExtSpkrSel = new System.Windows.Forms.CheckBoxTS();
            this.tbExtSpkr = new System.Windows.Forms.TrackBarTS();
            this.chkOutputMuteAll = new System.Windows.Forms.CheckBoxTS();
            this.lblIntSpkr = new System.Windows.Forms.LabelTS();
            this.chkIntSpkrSel = new System.Windows.Forms.CheckBoxTS();
            this.tbIntSpkr = new System.Windows.Forms.TrackBarTS();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbMic)).BeginInit();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInDB9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInPhono)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInRCA)).BeginInit();
            this.grpOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineOutRCA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHeadphone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbExtSpkr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntSpkr)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMic
            // 
            this.tbMic.Location = new System.Drawing.Point(16, 48);
            this.tbMic.Maximum = 0;
            this.tbMic.Minimum = -128;
            this.tbMic.Name = "tbMic";
            this.tbMic.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbMic.Size = new System.Drawing.Size(45, 104);
            this.tbMic.TabIndex = 0;
            this.tbMic.TickFrequency = 16;
            this.tbMic.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbMic, "Adjusts the Mic Input Volume");
            this.tbMic.Scroll += new System.EventHandler(this.tbMic_Scroll);
            // 
            // chkMicSel
            // 
            this.chkMicSel.Checked = true;
            this.chkMicSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMicSel.Image = null;
            this.chkMicSel.Location = new System.Drawing.Point(30, 144);
            this.chkMicSel.Name = "chkMicSel";
            this.chkMicSel.Size = new System.Drawing.Size(16, 24);
            this.chkMicSel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.chkMicSel, "Selects/Unselects the Mic Input");
            this.chkMicSel.CheckedChanged += new System.EventHandler(this.chkMicSel_CheckedChanged);
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.chkInputMuteAll);
            this.grpInput.Controls.Add(this.lblLineInDB9);
            this.grpInput.Controls.Add(this.chkLineInDB9Sel);
            this.grpInput.Controls.Add(this.tbLineInDB9);
            this.grpInput.Controls.Add(this.lblLineInPhono);
            this.grpInput.Controls.Add(this.chkLineInPhonoSel);
            this.grpInput.Controls.Add(this.tbLineInPhono);
            this.grpInput.Controls.Add(this.lblLineInRCA);
            this.grpInput.Controls.Add(this.chkLineInRCASel);
            this.grpInput.Controls.Add(this.tbLineInRCA);
            this.grpInput.Controls.Add(this.lblMic);
            this.grpInput.Controls.Add(this.chkMicSel);
            this.grpInput.Controls.Add(this.tbMic);
            this.grpInput.Location = new System.Drawing.Point(8, 8);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(216, 216);
            this.grpInput.TabIndex = 2;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input";
            // 
            // chkInputMuteAll
            // 
            this.chkInputMuteAll.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInputMuteAll.Image = null;
            this.chkInputMuteAll.Location = new System.Drawing.Point(16, 176);
            this.chkInputMuteAll.Name = "chkInputMuteAll";
            this.chkInputMuteAll.Size = new System.Drawing.Size(184, 24);
            this.chkInputMuteAll.TabIndex = 12;
            this.chkInputMuteAll.Text = "Mute All Inputs";
            this.chkInputMuteAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkInputMuteAll, "Mute All Input Lines");
            this.chkInputMuteAll.CheckedChanged += new System.EventHandler(this.chkInputMuteAll_CheckedChanged);
            // 
            // lblLineInDB9
            // 
            this.lblLineInDB9.Image = null;
            this.lblLineInDB9.Location = new System.Drawing.Point(157, 24);
            this.lblLineInDB9.Name = "lblLineInDB9";
            this.lblLineInDB9.Size = new System.Drawing.Size(49, 24);
            this.lblLineInDB9.TabIndex = 11;
            this.lblLineInDB9.Text = "FlexWire In";
            this.lblLineInDB9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblLineInDB9, "FlexWire Audio Input (DB9 on back panel)");
            // 
            // chkLineInDB9Sel
            // 
            this.chkLineInDB9Sel.Image = null;
            this.chkLineInDB9Sel.Location = new System.Drawing.Point(174, 144);
            this.chkLineInDB9Sel.Name = "chkLineInDB9Sel";
            this.chkLineInDB9Sel.Size = new System.Drawing.Size(16, 24);
            this.chkLineInDB9Sel.TabIndex = 10;
            this.toolTip1.SetToolTip(this.chkLineInDB9Sel, "Selects/Unselects the Line In DB9 Input");
            this.chkLineInDB9Sel.CheckedChanged += new System.EventHandler(this.chkLineInDB9Sel_CheckedChanged);
            // 
            // tbLineInDB9
            // 
            this.tbLineInDB9.Location = new System.Drawing.Point(160, 48);
            this.tbLineInDB9.Maximum = 0;
            this.tbLineInDB9.Minimum = -128;
            this.tbLineInDB9.Name = "tbLineInDB9";
            this.tbLineInDB9.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLineInDB9.Size = new System.Drawing.Size(45, 104);
            this.tbLineInDB9.TabIndex = 9;
            this.tbLineInDB9.TickFrequency = 16;
            this.tbLineInDB9.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbLineInDB9, "Adjusts the Line In DB9 Input Volume");
            this.tbLineInDB9.Scroll += new System.EventHandler(this.tbLineInDB9_Scroll);
            // 
            // lblLineInPhono
            // 
            this.lblLineInPhono.Image = null;
            this.lblLineInPhono.Location = new System.Drawing.Point(112, 24);
            this.lblLineInPhono.Name = "lblLineInPhono";
            this.lblLineInPhono.Size = new System.Drawing.Size(45, 24);
            this.lblLineInPhono.TabIndex = 8;
            this.lblLineInPhono.Text = "Bal Line In";
            this.lblLineInPhono.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblLineInPhono, "Balanced Line Input (Phono Plug on back panel)");
            // 
            // chkLineInPhonoSel
            // 
            this.chkLineInPhonoSel.Image = null;
            this.chkLineInPhonoSel.Location = new System.Drawing.Point(126, 144);
            this.chkLineInPhonoSel.Name = "chkLineInPhonoSel";
            this.chkLineInPhonoSel.Size = new System.Drawing.Size(16, 24);
            this.chkLineInPhonoSel.TabIndex = 7;
            this.toolTip1.SetToolTip(this.chkLineInPhonoSel, "Selects/Unselects the Line In Phono Input");
            this.chkLineInPhonoSel.CheckedChanged += new System.EventHandler(this.chkLineInPhonoSel_CheckedChanged);
            // 
            // tbLineInPhono
            // 
            this.tbLineInPhono.Location = new System.Drawing.Point(112, 48);
            this.tbLineInPhono.Maximum = 0;
            this.tbLineInPhono.Minimum = -128;
            this.tbLineInPhono.Name = "tbLineInPhono";
            this.tbLineInPhono.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLineInPhono.Size = new System.Drawing.Size(45, 104);
            this.tbLineInPhono.TabIndex = 6;
            this.tbLineInPhono.TickFrequency = 16;
            this.tbLineInPhono.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbLineInPhono, "Adjusts the Line In Phono Input Volume");
            this.tbLineInPhono.Scroll += new System.EventHandler(this.tbLineInPhono_Scroll);
            // 
            // lblLineInRCA
            // 
            this.lblLineInRCA.Image = null;
            this.lblLineInRCA.Location = new System.Drawing.Point(64, 24);
            this.lblLineInRCA.Name = "lblLineInRCA";
            this.lblLineInRCA.Size = new System.Drawing.Size(45, 24);
            this.lblLineInRCA.TabIndex = 5;
            this.lblLineInRCA.Text = "Line In RCA";
            this.lblLineInRCA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblLineInRCA, "Line Input (White RCA on back panel)");
            // 
            // chkLineInRCASel
            // 
            this.chkLineInRCASel.Image = null;
            this.chkLineInRCASel.Location = new System.Drawing.Point(78, 144);
            this.chkLineInRCASel.Name = "chkLineInRCASel";
            this.chkLineInRCASel.Size = new System.Drawing.Size(16, 24);
            this.chkLineInRCASel.TabIndex = 4;
            this.toolTip1.SetToolTip(this.chkLineInRCASel, "Selects/Unselects the Line In RCA Input");
            this.chkLineInRCASel.CheckedChanged += new System.EventHandler(this.chkLineInRCASel_CheckedChanged);
            // 
            // tbLineInRCA
            // 
            this.tbLineInRCA.Location = new System.Drawing.Point(64, 48);
            this.tbLineInRCA.Maximum = 0;
            this.tbLineInRCA.Minimum = -128;
            this.tbLineInRCA.Name = "tbLineInRCA";
            this.tbLineInRCA.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLineInRCA.Size = new System.Drawing.Size(45, 104);
            this.tbLineInRCA.TabIndex = 3;
            this.tbLineInRCA.TickFrequency = 16;
            this.tbLineInRCA.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbLineInRCA, "Adjusts the Line In RCA Input Volume");
            this.tbLineInRCA.Scroll += new System.EventHandler(this.tbLineInRCA_Scroll);
            // 
            // lblMic
            // 
            this.lblMic.Image = null;
            this.lblMic.Location = new System.Drawing.Point(16, 24);
            this.lblMic.Name = "lblMic";
            this.lblMic.Size = new System.Drawing.Size(45, 24);
            this.lblMic.TabIndex = 2;
            this.lblMic.Text = "Mic";
            this.lblMic.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblMic, "8 Pin front panel connector");
            // 
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.lblLineOutRCA);
            this.grpOutput.Controls.Add(this.chkLineOutRCASel);
            this.grpOutput.Controls.Add(this.tbLineOutRCA);
            this.grpOutput.Controls.Add(this.lblHeadphone);
            this.grpOutput.Controls.Add(this.chkHeadphoneSel);
            this.grpOutput.Controls.Add(this.tbHeadphone);
            this.grpOutput.Controls.Add(this.lblExtSpkr);
            this.grpOutput.Controls.Add(this.chkExtSpkrSel);
            this.grpOutput.Controls.Add(this.tbExtSpkr);
            this.grpOutput.Controls.Add(this.chkOutputMuteAll);
            this.grpOutput.Controls.Add(this.lblIntSpkr);
            this.grpOutput.Controls.Add(this.chkIntSpkrSel);
            this.grpOutput.Controls.Add(this.tbIntSpkr);
            this.grpOutput.Location = new System.Drawing.Point(232, 8);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(216, 216);
            this.grpOutput.TabIndex = 3;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output";
            // 
            // lblLineOutRCA
            // 
            this.lblLineOutRCA.Image = null;
            this.lblLineOutRCA.Location = new System.Drawing.Point(158, 24);
            this.lblLineOutRCA.Name = "lblLineOutRCA";
            this.lblLineOutRCA.Size = new System.Drawing.Size(48, 32);
            this.lblLineOutRCA.TabIndex = 22;
            this.lblLineOutRCA.Text = "Line Out RCA";
            this.lblLineOutRCA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkLineOutRCASel
            // 
            this.chkLineOutRCASel.Checked = true;
            this.chkLineOutRCASel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLineOutRCASel.Image = null;
            this.chkLineOutRCASel.Location = new System.Drawing.Point(174, 144);
            this.chkLineOutRCASel.Name = "chkLineOutRCASel";
            this.chkLineOutRCASel.Size = new System.Drawing.Size(16, 24);
            this.chkLineOutRCASel.TabIndex = 21;
            this.toolTip1.SetToolTip(this.chkLineOutRCASel, "Selects/Unselects the Line Out RCA Output");
            this.chkLineOutRCASel.CheckedChanged += new System.EventHandler(this.chkLineOutRCASel_CheckedChanged);
            // 
            // tbLineOutRCA
            // 
            this.tbLineOutRCA.Location = new System.Drawing.Point(160, 48);
            this.tbLineOutRCA.Maximum = 255;
            this.tbLineOutRCA.Minimum = 128;
            this.tbLineOutRCA.Name = "tbLineOutRCA";
            this.tbLineOutRCA.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLineOutRCA.Size = new System.Drawing.Size(45, 104);
            this.tbLineOutRCA.TabIndex = 20;
            this.tbLineOutRCA.TickFrequency = 16;
            this.tbLineOutRCA.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbLineOutRCA, "Adjusts the Line Out RCA Volume");
            this.tbLineOutRCA.Value = 248;
            this.tbLineOutRCA.Scroll += new System.EventHandler(this.tbLineOutRCA_Scroll);
            // 
            // lblHeadphone
            // 
            this.lblHeadphone.Image = null;
            this.lblHeadphone.Location = new System.Drawing.Point(110, 24);
            this.lblHeadphone.Name = "lblHeadphone";
            this.lblHeadphone.Size = new System.Drawing.Size(48, 32);
            this.lblHeadphone.TabIndex = 19;
            this.lblHeadphone.Text = "Head Phones";
            this.lblHeadphone.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkHeadphoneSel
            // 
            this.chkHeadphoneSel.Checked = true;
            this.chkHeadphoneSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHeadphoneSel.Image = null;
            this.chkHeadphoneSel.Location = new System.Drawing.Point(126, 144);
            this.chkHeadphoneSel.Name = "chkHeadphoneSel";
            this.chkHeadphoneSel.Size = new System.Drawing.Size(16, 24);
            this.chkHeadphoneSel.TabIndex = 18;
            this.toolTip1.SetToolTip(this.chkHeadphoneSel, "Selects/Unselects the Headphone Output");
            this.chkHeadphoneSel.CheckedChanged += new System.EventHandler(this.chkHeadphoneSel_CheckedChanged);
            // 
            // tbHeadphone
            // 
            this.tbHeadphone.Location = new System.Drawing.Point(112, 48);
            this.tbHeadphone.Maximum = 255;
            this.tbHeadphone.Minimum = 128;
            this.tbHeadphone.Name = "tbHeadphone";
            this.tbHeadphone.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbHeadphone.Size = new System.Drawing.Size(45, 104);
            this.tbHeadphone.TabIndex = 17;
            this.tbHeadphone.TickFrequency = 16;
            this.tbHeadphone.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbHeadphone, "Adjusts the Headphone Volume");
            this.tbHeadphone.Value = 248;
            this.tbHeadphone.Scroll += new System.EventHandler(this.tbHeadphone_Scroll);
            // 
            // lblExtSpkr
            // 
            this.lblExtSpkr.Image = null;
            this.lblExtSpkr.Location = new System.Drawing.Point(58, 24);
            this.lblExtSpkr.Name = "lblExtSpkr";
            this.lblExtSpkr.Size = new System.Drawing.Size(56, 32);
            this.lblExtSpkr.TabIndex = 16;
            this.lblExtSpkr.Text = "Pow Spkr Line Out";
            this.lblExtSpkr.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkExtSpkrSel
            // 
            this.chkExtSpkrSel.Checked = true;
            this.chkExtSpkrSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExtSpkrSel.Image = null;
            this.chkExtSpkrSel.Location = new System.Drawing.Point(78, 144);
            this.chkExtSpkrSel.Name = "chkExtSpkrSel";
            this.chkExtSpkrSel.Size = new System.Drawing.Size(16, 24);
            this.chkExtSpkrSel.TabIndex = 15;
            this.toolTip1.SetToolTip(this.chkExtSpkrSel, "Selects/Unselects the External Speaker Output");
            this.chkExtSpkrSel.CheckedChanged += new System.EventHandler(this.chkExtSpkrSel_CheckedChanged);
            // 
            // tbExtSpkr
            // 
            this.tbExtSpkr.Location = new System.Drawing.Point(64, 48);
            this.tbExtSpkr.Maximum = 255;
            this.tbExtSpkr.Minimum = 128;
            this.tbExtSpkr.Name = "tbExtSpkr";
            this.tbExtSpkr.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbExtSpkr.Size = new System.Drawing.Size(45, 104);
            this.tbExtSpkr.TabIndex = 14;
            this.tbExtSpkr.TickFrequency = 16;
            this.tbExtSpkr.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbExtSpkr, "Adjusts the External Speaker Volume");
            this.tbExtSpkr.Value = 248;
            this.tbExtSpkr.Scroll += new System.EventHandler(this.tbExtSpkr_Scroll);
            // 
            // chkOutputMuteAll
            // 
            this.chkOutputMuteAll.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkOutputMuteAll.Image = null;
            this.chkOutputMuteAll.Location = new System.Drawing.Point(16, 176);
            this.chkOutputMuteAll.Name = "chkOutputMuteAll";
            this.chkOutputMuteAll.Size = new System.Drawing.Size(184, 24);
            this.chkOutputMuteAll.TabIndex = 13;
            this.chkOutputMuteAll.Text = "Mute All Outputs";
            this.chkOutputMuteAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkOutputMuteAll, "Mute All Output Lines");
            this.chkOutputMuteAll.CheckedChanged += new System.EventHandler(this.chkOutputMuteAll_CheckedChanged);
            // 
            // lblIntSpkr
            // 
            this.lblIntSpkr.Image = null;
            this.lblIntSpkr.Location = new System.Drawing.Point(14, 24);
            this.lblIntSpkr.Name = "lblIntSpkr";
            this.lblIntSpkr.Size = new System.Drawing.Size(48, 32);
            this.lblIntSpkr.TabIndex = 2;
            this.lblIntSpkr.Text = "Internal Speaker";
            this.lblIntSpkr.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkIntSpkrSel
            // 
            this.chkIntSpkrSel.Checked = true;
            this.chkIntSpkrSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntSpkrSel.Image = null;
            this.chkIntSpkrSel.Location = new System.Drawing.Point(31, 144);
            this.chkIntSpkrSel.Name = "chkIntSpkrSel";
            this.chkIntSpkrSel.Size = new System.Drawing.Size(16, 24);
            this.chkIntSpkrSel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.chkIntSpkrSel, "Selects/Unselects the Internal Speaker Output");
            this.chkIntSpkrSel.CheckedChanged += new System.EventHandler(this.chkIntSpkrSel_CheckedChanged);
            // 
            // tbIntSpkr
            // 
            this.tbIntSpkr.Location = new System.Drawing.Point(16, 48);
            this.tbIntSpkr.Maximum = 255;
            this.tbIntSpkr.Minimum = 128;
            this.tbIntSpkr.Name = "tbIntSpkr";
            this.tbIntSpkr.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbIntSpkr.Size = new System.Drawing.Size(45, 104);
            this.tbIntSpkr.TabIndex = 0;
            this.tbIntSpkr.TickFrequency = 16;
            this.tbIntSpkr.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbIntSpkr, "Adjusts the Internal Speaker  Volume");
            this.tbIntSpkr.Value = 230;
            this.tbIntSpkr.Scroll += new System.EventHandler(this.tbIntSpkr_Scroll);
            // 
            // FWCMixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(456, 230);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FWCMixForm";
            this.Text = "FLEX-5000 Audio Mixer";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FWCMixForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.tbMic)).EndInit();
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInDB9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInPhono)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInRCA)).EndInit();
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineOutRCA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHeadphone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbExtSpkr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntSpkr)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Misc Routines

		private int InputSliderToRegVal(int slider)
		{
			int retval;
			if(slider < 0) retval = 0x100 + slider;
			else retval = slider;
			//Debug.WriteLine("slider: "+slider+" Reg: "+retval.ToString("X"));
			return retval;
		}

		#endregion

		#region Properties

		public int MicInput
		{
			get{return tbMic.Value;}
			set{tbMic.Value = value;
				tbMic_Scroll(this,EventArgs.Empty);}
		}

		public int LineInRCA
		{
			get{return tbLineInRCA.Value;}
			set
			{
				tbLineInRCA.Value = value;
				tbLineInRCA_Scroll(this,EventArgs.Empty);}
		}

		public int LineInPhono
		{
			get{return tbLineInPhono.Value;}
			set
			{
				tbLineInPhono.Value = value;
				tbLineInPhono_Scroll(this,EventArgs.Empty);}
		}

		public int LineInDB9
		{
			get{return tbLineInDB9.Value;}
			set
			{
				tbLineInDB9.Value = value;
				tbLineInDB9_Scroll(this,EventArgs.Empty);}
		}

		public string MicInputSelected
		{
			get{
				if(chkMicSel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkMicSel.Checked = true;
				else
					chkMicSel.Checked = false;
			}
		}

		public string LineInRCASelected
		{
			get
			{
				if(chkLineInRCASel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkLineInRCASel.Checked = true;
				else
					chkLineInRCASel.Checked = false;
			}
		}

		public string LineInPhonoSelected
		{
			get
			{
				if(chkLineInPhonoSel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkLineInPhonoSel.Checked = true;
				else
					chkLineInPhonoSel.Checked = false;
			}
		}

		public string LineInDB9Selected
		{
			get
			{
				if(chkLineInDB9Sel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkLineInDB9Sel.Checked = true;
				else
					chkLineInDB9Sel.Checked = false;
			}
		}

		public string InputMuteAll
		{
			get
			{
				if(chkInputMuteAll.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkInputMuteAll.Checked = true;
				else
					chkInputMuteAll.Checked = false;
			}
		}

		public int InternalSpkr
		{
			get{return tbIntSpkr.Value;}
			set
			{
				tbIntSpkr.Value = value;
				tbIntSpkr_Scroll(this,EventArgs.Empty);}
		}

		public int ExternalSpkr
		{
			get{return tbExtSpkr.Value;}
			set
			{
				tbExtSpkr.Value = value;
				tbExtSpkr_Scroll(this,EventArgs.Empty);}
		}

		public int Headphone
		{
			get{return tbHeadphone.Value;}
			set
			{
				tbHeadphone.Value = value;
				tbHeadphone_Scroll(this,EventArgs.Empty);}
		}

		public int LineOutRCA
		{
			get{return tbLineOutRCA.Value;}
			set
			{
				tbLineOutRCA.Value = value;
				tbLineOutRCA_Scroll(this,EventArgs.Empty);}
		}

		public string InternalSpkrSelected
		{
			get
			{
				if(chkIntSpkrSel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkIntSpkrSel.Checked = true;
				else
					chkIntSpkrSel.Checked = false;
			}
		}

		public string ExternalSpkrSelected
		{
			get
			{
				if(chkExtSpkrSel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkExtSpkrSel.Checked = true;
				else
					chkExtSpkrSel.Checked = false;
			}
		}

		public string HeadphoneSelected
		{
			get
			{
				if(chkHeadphoneSel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkHeadphoneSel.Checked = true;
				else
					chkHeadphoneSel.Checked = false;
			}
		}

		public string LineOutRCASelected
		{
			get
			{
				if(chkLineOutRCASel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkLineOutRCASel.Checked = true;
				else
					chkLineOutRCASel.Checked = false;
			}
		}

		public string OutputMuteAll
		{
			get
			{
				if(chkOutputMuteAll.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkOutputMuteAll.Checked = true;
				else
					chkOutputMuteAll.Checked = false;
			}
		}




		#endregion Properties

		#region Input Event Handlers

		private void tbMic_Scroll(object sender, System.EventArgs e)
		{
			if(chkMicSel.Checked && !chkInputMuteAll.Checked)
				FWC.WriteCodecReg(0x16, InputSliderToRegVal(tbMic.Value));
		}

		private void tbLineInRCA_Scroll(object sender, System.EventArgs e)
		{
			if(chkLineInRCASel.Checked && !chkInputMuteAll.Checked)
				FWC.WriteCodecReg(0x14, InputSliderToRegVal(tbLineInRCA.Value));
		}

		private void tbLineInPhono_Scroll(object sender, System.EventArgs e)
		{
			if(chkLineInPhonoSel.Checked && !chkInputMuteAll.Checked)
				FWC.WriteCodecReg(0x15, InputSliderToRegVal(tbLineInPhono.Value));
		}

		private void tbLineInDB9_Scroll(object sender, System.EventArgs e)
		{
			if(chkLineInDB9Sel.Checked && !chkInputMuteAll.Checked)
				FWC.WriteCodecReg(0x13, InputSliderToRegVal(tbLineInDB9.Value));
		}

		private void chkMicSel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkMicSel.Checked)
			{ 
				chkLineInRCASel.Checked = false;
				chkLineInPhonoSel.Checked = false;
				chkLineInDB9Sel.Checked = false;

				if(!chkInputMuteAll.Checked) 
					FWC.WriteCodecReg(0x16, InputSliderToRegVal(tbMic.Value));
				Audio.IN_TX_L = 7;
			}
			else FWC.WriteCodecReg(0x16, 0x80);
		}

		private void chkLineInRCASel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkLineInRCASel.Checked)
			{
				chkMicSel.Checked = false;
				chkLineInPhonoSel.Checked = false;
				chkLineInDB9Sel.Checked = false;

				if(!chkInputMuteAll.Checked) 
					FWC.WriteCodecReg(0x14, InputSliderToRegVal(tbLineInRCA.Value));
				Audio.IN_TX_L = 5;				
			}
			else FWC.WriteCodecReg(0x14, 0x80);
		}

		private void chkLineInPhonoSel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkLineInPhonoSel.Checked) 
			{
				chkMicSel.Checked = false;
				chkLineInRCASel.Checked = false;
				chkLineInDB9Sel.Checked = false;

				if(!chkInputMuteAll.Checked) 
					FWC.WriteCodecReg(0x15, InputSliderToRegVal(tbLineInPhono.Value));
				Audio.IN_TX_L = 6;
			}
			else FWC.WriteCodecReg(0x15, 0x80);
		}

		private void chkLineInDB9Sel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkLineInDB9Sel.Checked)
			{
				chkMicSel.Checked = false;
				chkLineInRCASel.Checked = false;
				chkLineInPhonoSel.Checked = false;

				if(!chkInputMuteAll.Checked)
					FWC.WriteCodecReg(0x13, InputSliderToRegVal(tbLineInDB9.Value));
				Audio.IN_TX_L = 4;
			}
			else FWC.WriteCodecReg(0x13, 0x80);
		}

		private void chkInputMuteAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkInputMuteAll.Checked)
			{
				chkInputMuteAll.BackColor = console.ButtonSelectedColor;
				for(int i=0x13; i<=0x16; i++)
					FWC.WriteCodecReg(i, 0x80);
			}
			else
			{
				chkInputMuteAll.BackColor = SystemColors.Control;
				if(chkMicSel.Checked) tbMic_Scroll(this, EventArgs.Empty);
				if(chkLineInRCASel.Checked) tbLineInRCA_Scroll(this, EventArgs.Empty);
				if(chkLineInPhonoSel.Checked) tbLineInPhono_Scroll(this, EventArgs.Empty);
				if(chkLineInDB9Sel.Checked) tbLineInDB9_Scroll(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Output Event Handlers

		private void tbIntSpkr_Scroll(object sender, System.EventArgs e)
		{
			FWC.WriteCodecReg(0x0F, 0xFF-tbIntSpkr.Value);
		}

		private void tbExtSpkr_Scroll(object sender, System.EventArgs e)
		{
			FWC.WriteCodecReg(0x0C, 0xFF-tbExtSpkr.Value);
			FWC.WriteCodecReg(0x0D, 0xFF-tbExtSpkr.Value);
		}

		private void tbHeadphone_Scroll(object sender, System.EventArgs e)
		{
			FWC.WriteCodecReg(0x0A, 0xFF-tbHeadphone.Value);
			FWC.WriteCodecReg(0x0B, 0xFF-tbHeadphone.Value);
		}

		private void tbLineOutRCA_Scroll(object sender, System.EventArgs e)
		{
			FWC.WriteCodecReg(0x0E, 0xFF-tbLineOutRCA.Value);
		}

		private void chkIntSpkrSel_CheckedChanged(object sender, System.EventArgs e)
		{
			int val = 0;
			FWC.ReadCodecReg(7, out val);
			if(chkIntSpkrSel.Checked)
			{
				FWC.SetIntSpkr(true);
				FWC.WriteCodecReg(7, val & 0x7F);
			}
			else
			{
				FWC.WriteCodecReg(7, val | 0x80);
				FWC.SetIntSpkr(false);
			}
		}

		private void chkExtSpkrSel_CheckedChanged(object sender, System.EventArgs e)
		{
			int val = 0;
			FWC.ReadCodecReg(7, out val);
			if(chkExtSpkrSel.Checked)
				FWC.WriteCodecReg(7, val & 0xCF);
			else
				FWC.WriteCodecReg(7, val | 0x30);
		}

		private void chkHeadphoneSel_CheckedChanged(object sender, System.EventArgs e)
		{
			int val = 0;
			FWC.ReadCodecReg(7, out val);
			if(chkHeadphoneSel.Checked)
			{
				FWC.SetHeadphone(true);
				FWC.WriteCodecReg(7, val & 0xF3);
			}
			else
			{
				FWC.WriteCodecReg(7, val | 0x0C);
				FWC.SetHeadphone(false);
			}
		}

		private void chkLineOutRCASel_CheckedChanged(object sender, System.EventArgs e)
		{
			int val = 0;
			FWC.ReadCodecReg(7, out val);
			if(chkLineOutRCASel.Checked)
				FWC.WriteCodecReg(7, val & 0xBF);
			else
				FWC.WriteCodecReg(7, val | 0x40);
		}

		private void chkOutputMuteAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkOutputMuteAll.Checked)
			{
				chkOutputMuteAll.BackColor = console.ButtonSelectedColor;
				FWC.WriteCodecReg(7, 0xFC);
			}
			else
			{
				chkOutputMuteAll.BackColor = SystemColors.Control;
				chkIntSpkrSel_CheckedChanged(this, EventArgs.Empty);
				chkExtSpkrSel_CheckedChanged(this, EventArgs.Empty);
				chkHeadphoneSel_CheckedChanged(this, EventArgs.Empty);
				chkLineOutRCASel_CheckedChanged(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Other Event Handlers

		private void FWCMixForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FWCMixer");
		}

		#endregion
	}
}

//=================================================================
// FLEX3000MixerForm.cs
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
	public class FLEX3000MixerForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.TrackBarTS tbMic;
		private System.Windows.Forms.GroupBoxTS grpInput;
		private System.Windows.Forms.LabelTS lblMic;
		private System.Windows.Forms.GroupBoxTS grpOutput;
		private System.Windows.Forms.CheckBoxTS chkInputMuteAll;
		private System.Windows.Forms.CheckBoxTS chkMicSel;
		private System.Windows.Forms.LabelTS lblLineInDB9;
		private System.Windows.Forms.CheckBoxTS chkLineInDB9Sel;
		private System.Windows.Forms.TrackBarTS tbLineInDB9;
		private System.Windows.Forms.CheckBoxTS chkOutputMuteAll;
		private System.Windows.Forms.LabelTS lblExtSpkr;
		private System.Windows.Forms.CheckBoxTS chkExtSpkrSel;
		private System.Windows.Forms.TrackBarTS tbExtSpkr;
		private System.Windows.Forms.LabelTS lblHeadphone;
		private System.Windows.Forms.CheckBoxTS chkHeadphoneSel;
		private System.Windows.Forms.TrackBarTS tbHeadphone;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.LabelTS lblLineOutDB9;
		private System.Windows.Forms.CheckBoxTS chkLineOutDB9Sel;
		private System.Windows.Forms.TrackBarTS tbLineOutDB9;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor and Destructor

		public FLEX3000MixerForm(Console c)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			console = c;
			
			Common.RestoreForm(this, "FLEX3000MixerForm", false);

			chkMicSel_CheckedChanged(this, EventArgs.Empty);
			chkLineInDB9Sel_CheckedChanged(this, EventArgs.Empty);
			chkExtSpkrSel_CheckedChanged(this, EventArgs.Empty);
			chkHeadphoneSel_CheckedChanged(this, EventArgs.Empty);
			chkLineOutDB9Sel_CheckedChanged(this, EventArgs.Empty);
			tbMic_Scroll(this, EventArgs.Empty);
			tbLineInDB9_Scroll(this, EventArgs.Empty);
			tbExtSpkr_Scroll(this, EventArgs.Empty);
			tbHeadphone_Scroll(this, EventArgs.Empty);
			tbLineOutDB9_Scroll(this, EventArgs.Empty);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLEX3000MixerForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkLineOutDB9Sel = new System.Windows.Forms.CheckBoxTS();
            this.tbLineOutDB9 = new System.Windows.Forms.TrackBarTS();
            this.chkHeadphoneSel = new System.Windows.Forms.CheckBoxTS();
            this.tbHeadphone = new System.Windows.Forms.TrackBarTS();
            this.chkExtSpkrSel = new System.Windows.Forms.CheckBoxTS();
            this.tbExtSpkr = new System.Windows.Forms.TrackBarTS();
            this.chkOutputMuteAll = new System.Windows.Forms.CheckBoxTS();
            this.chkInputMuteAll = new System.Windows.Forms.CheckBoxTS();
            this.lblLineInDB9 = new System.Windows.Forms.LabelTS();
            this.chkLineInDB9Sel = new System.Windows.Forms.CheckBoxTS();
            this.tbLineInDB9 = new System.Windows.Forms.TrackBarTS();
            this.lblMic = new System.Windows.Forms.LabelTS();
            this.chkMicSel = new System.Windows.Forms.CheckBoxTS();
            this.tbMic = new System.Windows.Forms.TrackBarTS();
            this.grpOutput = new System.Windows.Forms.GroupBoxTS();
            this.lblLineOutDB9 = new System.Windows.Forms.LabelTS();
            this.lblHeadphone = new System.Windows.Forms.LabelTS();
            this.lblExtSpkr = new System.Windows.Forms.LabelTS();
            this.grpInput = new System.Windows.Forms.GroupBoxTS();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineOutDB9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHeadphone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbExtSpkr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInDB9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMic)).BeginInit();
            this.grpOutput.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkLineOutDB9Sel
            // 
            this.chkLineOutDB9Sel.Checked = true;
            this.chkLineOutDB9Sel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLineOutDB9Sel.Image = null;
            this.chkLineOutDB9Sel.Location = new System.Drawing.Point(136, 144);
            this.chkLineOutDB9Sel.Name = "chkLineOutDB9Sel";
            this.chkLineOutDB9Sel.Size = new System.Drawing.Size(16, 24);
            this.chkLineOutDB9Sel.TabIndex = 21;
            this.chkLineOutDB9Sel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.chkLineOutDB9Sel, "Selects/Unselects the FlexWire Line Out Output");
            this.chkLineOutDB9Sel.CheckedChanged += new System.EventHandler(this.chkLineOutDB9Sel_CheckedChanged);
            // 
            // tbLineOutDB9
            // 
            this.tbLineOutDB9.Location = new System.Drawing.Point(124, 48);
            this.tbLineOutDB9.Maximum = 255;
            this.tbLineOutDB9.Minimum = 128;
            this.tbLineOutDB9.Name = "tbLineOutDB9";
            this.tbLineOutDB9.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLineOutDB9.Size = new System.Drawing.Size(45, 104);
            this.tbLineOutDB9.TabIndex = 20;
            this.tbLineOutDB9.TickFrequency = 16;
            this.tbLineOutDB9.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbLineOutDB9, "Adjusts the Line Out RCA Volume");
            this.tbLineOutDB9.Value = 248;
            this.tbLineOutDB9.Scroll += new System.EventHandler(this.tbLineOutDB9_Scroll);
            // 
            // chkHeadphoneSel
            // 
            this.chkHeadphoneSel.Checked = true;
            this.chkHeadphoneSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHeadphoneSel.Image = null;
            this.chkHeadphoneSel.Location = new System.Drawing.Point(88, 144);
            this.chkHeadphoneSel.Name = "chkHeadphoneSel";
            this.chkHeadphoneSel.Size = new System.Drawing.Size(16, 24);
            this.chkHeadphoneSel.TabIndex = 18;
            this.chkHeadphoneSel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.chkHeadphoneSel, "Selects/Unselects the Headphone Output");
            this.chkHeadphoneSel.CheckedChanged += new System.EventHandler(this.chkHeadphoneSel_CheckedChanged);
            // 
            // tbHeadphone
            // 
            this.tbHeadphone.Location = new System.Drawing.Point(74, 48);
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
            // chkExtSpkrSel
            // 
            this.chkExtSpkrSel.Checked = true;
            this.chkExtSpkrSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExtSpkrSel.Image = null;
            this.chkExtSpkrSel.Location = new System.Drawing.Point(40, 144);
            this.chkExtSpkrSel.Name = "chkExtSpkrSel";
            this.chkExtSpkrSel.Size = new System.Drawing.Size(16, 24);
            this.chkExtSpkrSel.TabIndex = 15;
            this.chkExtSpkrSel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.chkExtSpkrSel, "Selects/Unselects the External Speaker Output");
            this.chkExtSpkrSel.CheckedChanged += new System.EventHandler(this.chkExtSpkrSel_CheckedChanged);
            // 
            // tbExtSpkr
            // 
            this.tbExtSpkr.Location = new System.Drawing.Point(24, 48);
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
            this.chkOutputMuteAll.Size = new System.Drawing.Size(144, 24);
            this.chkOutputMuteAll.TabIndex = 13;
            this.chkOutputMuteAll.Text = "Mute All Outputs";
            this.chkOutputMuteAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkOutputMuteAll, "Mute All Output Lines");
            this.chkOutputMuteAll.CheckedChanged += new System.EventHandler(this.chkOutputMuteAll_CheckedChanged);
            // 
            // chkInputMuteAll
            // 
            this.chkInputMuteAll.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInputMuteAll.Image = null;
            this.chkInputMuteAll.Location = new System.Drawing.Point(16, 176);
            this.chkInputMuteAll.Name = "chkInputMuteAll";
            this.chkInputMuteAll.Size = new System.Drawing.Size(88, 24);
            this.chkInputMuteAll.TabIndex = 12;
            this.chkInputMuteAll.Text = "Mute All Inputs";
            this.chkInputMuteAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkInputMuteAll, "Mute All Input Lines");
            this.chkInputMuteAll.CheckedChanged += new System.EventHandler(this.chkInputMuteAll_CheckedChanged);
            // 
            // lblLineInDB9
            // 
            this.lblLineInDB9.Image = null;
            this.lblLineInDB9.Location = new System.Drawing.Point(64, 24);
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
            this.chkLineInDB9Sel.Location = new System.Drawing.Point(81, 144);
            this.chkLineInDB9Sel.Name = "chkLineInDB9Sel";
            this.chkLineInDB9Sel.Size = new System.Drawing.Size(16, 24);
            this.chkLineInDB9Sel.TabIndex = 10;
            this.chkLineInDB9Sel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.chkLineInDB9Sel, "Selects/Unselects the Line In DB9 Input");
            this.chkLineInDB9Sel.CheckedChanged += new System.EventHandler(this.chkLineInDB9Sel_CheckedChanged);
            // 
            // tbLineInDB9
            // 
            this.tbLineInDB9.Location = new System.Drawing.Point(68, 48);
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
            // chkMicSel
            // 
            this.chkMicSel.Checked = true;
            this.chkMicSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMicSel.Image = null;
            this.chkMicSel.Location = new System.Drawing.Point(30, 144);
            this.chkMicSel.Name = "chkMicSel";
            this.chkMicSel.Size = new System.Drawing.Size(16, 24);
            this.chkMicSel.TabIndex = 1;
            this.chkMicSel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.chkMicSel, "Selects/Unselects the Mic Input");
            this.chkMicSel.CheckedChanged += new System.EventHandler(this.chkMicSel_CheckedChanged);
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
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.lblLineOutDB9);
            this.grpOutput.Controls.Add(this.chkLineOutDB9Sel);
            this.grpOutput.Controls.Add(this.tbLineOutDB9);
            this.grpOutput.Controls.Add(this.lblHeadphone);
            this.grpOutput.Controls.Add(this.chkHeadphoneSel);
            this.grpOutput.Controls.Add(this.tbHeadphone);
            this.grpOutput.Controls.Add(this.lblExtSpkr);
            this.grpOutput.Controls.Add(this.chkExtSpkrSel);
            this.grpOutput.Controls.Add(this.tbExtSpkr);
            this.grpOutput.Controls.Add(this.chkOutputMuteAll);
            this.grpOutput.Location = new System.Drawing.Point(136, 8);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(176, 216);
            this.grpOutput.TabIndex = 3;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output";
            // 
            // lblLineOutDB9
            // 
            this.lblLineOutDB9.Image = null;
            this.lblLineOutDB9.Location = new System.Drawing.Point(120, 24);
            this.lblLineOutDB9.Name = "lblLineOutDB9";
            this.lblLineOutDB9.Size = new System.Drawing.Size(49, 32);
            this.lblLineOutDB9.TabIndex = 22;
            this.lblLineOutDB9.Text = "FlexWire Out";
            this.lblLineOutDB9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblHeadphone
            // 
            this.lblHeadphone.Image = null;
            this.lblHeadphone.Location = new System.Drawing.Point(72, 24);
            this.lblHeadphone.Name = "lblHeadphone";
            this.lblHeadphone.Size = new System.Drawing.Size(48, 32);
            this.lblHeadphone.TabIndex = 19;
            this.lblHeadphone.Text = "Head Phones";
            this.lblHeadphone.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExtSpkr
            // 
            this.lblExtSpkr.Image = null;
            this.lblExtSpkr.Location = new System.Drawing.Point(16, 24);
            this.lblExtSpkr.Name = "lblExtSpkr";
            this.lblExtSpkr.Size = new System.Drawing.Size(56, 32);
            this.lblExtSpkr.TabIndex = 16;
            this.lblExtSpkr.Text = "Pow Spkr Out";
            this.lblExtSpkr.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.chkInputMuteAll);
            this.grpInput.Controls.Add(this.lblLineInDB9);
            this.grpInput.Controls.Add(this.chkLineInDB9Sel);
            this.grpInput.Controls.Add(this.tbLineInDB9);
            this.grpInput.Controls.Add(this.lblMic);
            this.grpInput.Controls.Add(this.chkMicSel);
            this.grpInput.Controls.Add(this.tbMic);
            this.grpInput.Location = new System.Drawing.Point(8, 8);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(120, 216);
            this.grpInput.TabIndex = 2;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input";
            // 
            // FLEX3000MixerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(320, 230);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FLEX3000MixerForm";
            this.Text = "FLEX-3000 Audio Mixer";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FWCMixForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.tbLineOutDB9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHeadphone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbExtSpkr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLineInDB9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMic)).EndInit();
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
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
			set
			{
				tbMic.Value = value;
				tbMic_Scroll(this,EventArgs.Empty);}
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
			get
			{
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

		public int LineOutDB9
		{
			get{return tbLineOutDB9.Value;}
			set
			{
				tbLineOutDB9.Value = value;
				tbLineOutDB9_Scroll(this,EventArgs.Empty);}
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

		public string LineOutDB9Selected
		{
			get
			{
				if(chkLineOutDB9Sel.Checked)
					return "1";
				else
					return "0";}

			set
			{
				if(value == "1")
					chkLineOutDB9Sel.Checked = true;
				else
					chkLineOutDB9Sel.Checked = false;
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
				FWC.WriteCodecReg(0x12, InputSliderToRegVal(tbMic.Value));
		}

		private void tbLineInDB9_Scroll(object sender, System.EventArgs e)
		{
			if(chkLineInDB9Sel.Checked && !chkInputMuteAll.Checked)
				FWC.WriteCodecReg(0x11, InputSliderToRegVal(tbLineInDB9.Value));
		}

		private void chkMicSel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkMicSel.Checked)
			{ 
				chkLineInDB9Sel.Checked = false;

				if(!chkInputMuteAll.Checked) 
					FWC.WriteCodecReg(0x12, InputSliderToRegVal(tbMic.Value));
				Audio.IN_TX_L = 3;
			}
			else FWC.WriteCodecReg(0x12, 0x80);
		}

		private void chkLineInDB9Sel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkLineInDB9Sel.Checked)
			{
				chkMicSel.Checked = false;

				if(!chkInputMuteAll.Checked)
					FWC.WriteCodecReg(0x11, InputSliderToRegVal(tbLineInDB9.Value));
				Audio.IN_TX_L = 2;
			}
			else FWC.WriteCodecReg(0x11, 0x80);
		}

		private void chkInputMuteAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkInputMuteAll.Checked)
			{
				chkInputMuteAll.BackColor = console.ButtonSelectedColor;
				for(int i=0x11; i<=0x12; i++)
					FWC.WriteCodecReg(i, 0x80);
			}
			else
			{
				chkInputMuteAll.BackColor = SystemColors.Control;
				if(chkMicSel.Checked) tbMic_Scroll(this, EventArgs.Empty);
				if(chkLineInDB9Sel.Checked) tbLineInDB9_Scroll(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Output Event Handlers

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

		private void tbLineOutDB9_Scroll(object sender, System.EventArgs e)
		{
			FWC.WriteCodecReg(0x0E, 0xFF-tbLineOutDB9.Value);
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

		private void chkLineOutDB9Sel_CheckedChanged(object sender, System.EventArgs e)
		{
			int val = 0;
			FWC.ReadCodecReg(7, out val);
			if(chkLineOutDB9Sel.Checked)
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
				chkExtSpkrSel_CheckedChanged(this, EventArgs.Empty);
				chkHeadphoneSel_CheckedChanged(this, EventArgs.Empty);
				chkLineOutDB9Sel_CheckedChanged(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Other Event Handlers

		private void FWCMixForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
			Common.SaveForm(this, "FLEX3000MixerForm");
		}

		#endregion
	}
}

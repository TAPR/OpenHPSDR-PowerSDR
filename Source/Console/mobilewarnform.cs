//=================================================================
// alphawarnform.cs
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
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace PowerSDR
{
    public class MobileWarnForm : Form
    {
        private readonly Container components;
        private Button btnContinue;
        private CheckBox chkShowThisOnStartup;
        private Console console;
        private RichTextBox rtxtWarning;

        public MobileWarnForm(Console c)
        {
            InitializeComponent();
            console = c;
            ActiveControl = btnContinue;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private Stream GetResource(string name)
        {
            return GetType().Assembly.GetManifestResourceStream(name);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MobileWarnForm_Closing(object sender, CancelEventArgs e)
        {
            Hide();
            if (!chkShowThisOnStartup.Checked)
            {
                var a = new ArrayList();
                a.Add("show_mobile_warning/False");
                DB.SaveVars("State", ref a);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.Resources.ResourceManager(typeof (MobileWarnForm));
            this.rtxtWarning = new System.Windows.Forms.RichTextBox();
            this.chkShowThisOnStartup = new System.Windows.Forms.CheckBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtxtWarning
            // 
            this.rtxtWarning.BackColor = System.Drawing.SystemColors.ControlText;
            this.rtxtWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F,
                                                            System.Drawing.FontStyle.Regular,
                                                            System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.rtxtWarning.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.rtxtWarning.Location = new System.Drawing.Point(16, 16);
            this.rtxtWarning.Name = "rtxtWarning";
            this.rtxtWarning.ReadOnly = true;
            this.rtxtWarning.Size = new System.Drawing.Size(384, 216);
            this.rtxtWarning.TabIndex = 1;
            this.rtxtWarning.Text =
                @"If you choose to use your radio for mobile operation:

1. Adjust all settings and operating parameters while the vehicle is at a complete stop.
2. Comply with all traffic laws.
3. Remain attentive to driving and look at the screen only briefly when necessary.

Clicking continue confirms your agreement to comply with these operating guidelines.";
            // 
            // chkShowThisOnStartup
            // 
            this.chkShowThisOnStartup.BackColor = System.Drawing.Color.Black;
            this.chkShowThisOnStartup.Checked = true;
            this.chkShowThisOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowThisOnStartup.ForeColor = System.Drawing.Color.White;
            this.chkShowThisOnStartup.Location = new System.Drawing.Point(16, 240);
            this.chkShowThisOnStartup.Name = "chkShowThisOnStartup";
            this.chkShowThisOnStartup.Size = new System.Drawing.Size(168, 24);
            this.chkShowThisOnStartup.TabIndex = 2;
            this.chkShowThisOnStartup.Text = "Show this warning on startup";
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.SystemColors.Control;
            this.btnContinue.Location = new System.Drawing.Point(256, 240);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.TabIndex = 3;
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // MobileWarnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(416, 268);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.chkShowThisOnStartup);
            this.Controls.Add(this.rtxtWarning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MobileWarnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alpha Warning";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MobileWarnForm_Closing);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
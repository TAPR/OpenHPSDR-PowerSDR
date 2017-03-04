//=================================================================
// fwcCalForm.cs
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
	public class FWCCalForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.ButtonTS btnSaveToEEPROM;
		private System.Windows.Forms.ButtonTS btnRestoreFromEEPROM;
		private System.Windows.Forms.GroupBox grpTRX;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ButtonTS btnSaveRX2ToEEPROM;
		private System.Windows.Forms.ButtonTS btnRestoreRX2FromEEPROM;
		private System.Windows.Forms.Button btnResetTRXChecksums;
		private System.Windows.Forms.Button btnResetRX2Checksums;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor and Destructor

		public FWCCalForm(Console c)
		{
			InitializeComponent();
			console = c;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FWCCalForm));
			this.btnSaveToEEPROM = new System.Windows.Forms.ButtonTS();
			this.btnRestoreFromEEPROM = new System.Windows.Forms.ButtonTS();
			this.grpTRX = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRestoreRX2FromEEPROM = new System.Windows.Forms.ButtonTS();
			this.btnSaveRX2ToEEPROM = new System.Windows.Forms.ButtonTS();
			this.btnResetTRXChecksums = new System.Windows.Forms.Button();
			this.btnResetRX2Checksums = new System.Windows.Forms.Button();
			this.grpTRX.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSaveToEEPROM
			// 
			this.btnSaveToEEPROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSaveToEEPROM.Image = null;
			this.btnSaveToEEPROM.Location = new System.Drawing.Point(128, 24);
			this.btnSaveToEEPROM.Name = "btnSaveToEEPROM";
			this.btnSaveToEEPROM.Size = new System.Drawing.Size(112, 48);
			this.btnSaveToEEPROM.TabIndex = 0;
			this.btnSaveToEEPROM.Text = "Save Calibration Data To EEPROM";
			this.btnSaveToEEPROM.Click += new System.EventHandler(this.btnSaveToEEPROM_Click);
			// 
			// btnRestoreFromEEPROM
			// 
			this.btnRestoreFromEEPROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnRestoreFromEEPROM.Image = null;
			this.btnRestoreFromEEPROM.Location = new System.Drawing.Point(8, 24);
			this.btnRestoreFromEEPROM.Name = "btnRestoreFromEEPROM";
			this.btnRestoreFromEEPROM.Size = new System.Drawing.Size(112, 48);
			this.btnRestoreFromEEPROM.TabIndex = 15;
			this.btnRestoreFromEEPROM.Text = "Restore Calibration Data To Database from EEPROM";
			this.btnRestoreFromEEPROM.Click += new System.EventHandler(this.btnRestoreFromEEPROM_Click);
			// 
			// grpTRX
			// 
			this.grpTRX.Controls.Add(this.btnRestoreFromEEPROM);
			this.grpTRX.Controls.Add(this.btnSaveToEEPROM);
			this.grpTRX.Controls.Add(this.btnResetTRXChecksums);
			this.grpTRX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpTRX.Location = new System.Drawing.Point(8, 8);
			this.grpTRX.Name = "grpTRX";
			this.grpTRX.Size = new System.Drawing.Size(256, 136);
			this.grpTRX.TabIndex = 16;
			this.grpTRX.TabStop = false;
			this.grpTRX.Text = "RX1 / PA";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnResetRX2Checksums);
			this.groupBox1.Controls.Add(this.btnRestoreRX2FromEEPROM);
			this.groupBox1.Controls.Add(this.btnSaveRX2ToEEPROM);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(8, 152);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 136);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "RX2";
			// 
			// btnRestoreRX2FromEEPROM
			// 
			this.btnRestoreRX2FromEEPROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnRestoreRX2FromEEPROM.Image = null;
			this.btnRestoreRX2FromEEPROM.Location = new System.Drawing.Point(8, 24);
			this.btnRestoreRX2FromEEPROM.Name = "btnRestoreRX2FromEEPROM";
			this.btnRestoreRX2FromEEPROM.Size = new System.Drawing.Size(112, 48);
			this.btnRestoreRX2FromEEPROM.TabIndex = 15;
			this.btnRestoreRX2FromEEPROM.Text = "Restore Calibration Data To Database from EEPROM";
			this.btnRestoreRX2FromEEPROM.Click += new System.EventHandler(this.btnRestoreRX2FromEEPROM_Click);
			// 
			// btnSaveRX2ToEEPROM
			// 
			this.btnSaveRX2ToEEPROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSaveRX2ToEEPROM.Image = null;
			this.btnSaveRX2ToEEPROM.Location = new System.Drawing.Point(128, 24);
			this.btnSaveRX2ToEEPROM.Name = "btnSaveRX2ToEEPROM";
			this.btnSaveRX2ToEEPROM.Size = new System.Drawing.Size(112, 48);
			this.btnSaveRX2ToEEPROM.TabIndex = 0;
			this.btnSaveRX2ToEEPROM.Text = "Save Calibration Data To EEPROM";
			this.btnSaveRX2ToEEPROM.Click += new System.EventHandler(this.btnSaveRX2ToEEPROM_Click);
			// 
			// btnResetTRXChecksums
			// 
			this.btnResetTRXChecksums.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnResetTRXChecksums.Location = new System.Drawing.Point(8, 80);
			this.btnResetTRXChecksums.Name = "btnResetTRXChecksums";
			this.btnResetTRXChecksums.Size = new System.Drawing.Size(112, 48);
			this.btnResetTRXChecksums.TabIndex = 18;
			this.btnResetTRXChecksums.Text = "Reset RX1/PA Checksums";
			this.btnResetTRXChecksums.Click += new System.EventHandler(this.btnResetTRXChecksums_Click);
			// 
			// btnResetRX2Checksums
			// 
			this.btnResetRX2Checksums.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnResetRX2Checksums.Location = new System.Drawing.Point(8, 80);
			this.btnResetRX2Checksums.Name = "btnResetRX2Checksums";
			this.btnResetRX2Checksums.Size = new System.Drawing.Size(112, 48);
			this.btnResetRX2Checksums.TabIndex = 19;
			this.btnResetRX2Checksums.Text = "Reset RX2 Checksums";
			this.btnResetRX2Checksums.Click += new System.EventHandler(this.btnResetRX2Checksums_Click);
			// 
			// FWCCalForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 294);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpTRX);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FWCCalForm";
			this.Text = "FLEX-5000 EEPROM";
			this.grpTRX.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Event Handlers

		private void btnRestoreFromEEPROM_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to read the current RX1/PA EEPROM data into\n"+
				"the database overwriting any current values?",
				"Overwrite database?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if(dr == DialogResult.No) return;
			
			btnRestoreFromEEPROM.BackColor = console.ButtonSelectedColor;
			btnRestoreFromEEPROM.Enabled = false;
			Thread t = new Thread(new ThreadStart(RestoreFromEEPROM));
			t.Name = "Restore From EEPROM Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void RestoreFromEEPROM()
		{
			console.RestoreCalData();
			btnRestoreFromEEPROM.BackColor = SystemColors.Control;
			btnRestoreFromEEPROM.Enabled = true;
		}

		private void btnSaveToEEPROM_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to write the current RX1/PA database calibration values to the EEPROM?",
				"Overwrite EEPROM?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if(dr == DialogResult.No) return;
			
			btnSaveToEEPROM.BackColor = console.ButtonSelectedColor;
			btnSaveToEEPROM.Enabled = false;
			Thread t = new Thread(new ThreadStart(WriteToEEPROM));
			t.Name = "Write To EEPROM Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void WriteToEEPROM()
		{
			console.WriteCalData();
			btnSaveToEEPROM.BackColor = SystemColors.Control;
			btnSaveToEEPROM.Enabled = true;
		}

		#endregion

		private void btnRestoreRX2FromEEPROM_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to read the current RX2 EEPROM data into\n"+
				"the database overwriting any current values?",
				"Overwrite database?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if(dr == DialogResult.No) return;
			
			btnRestoreRX2FromEEPROM.BackColor = console.ButtonSelectedColor;
			btnRestoreRX2FromEEPROM.Enabled = false;
			Thread t = new Thread(new ThreadStart(RestoreRX2FromEEPROM));
			t.Name = "Restore RX2 from EEPROM Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void RestoreRX2FromEEPROM()
		{
			console.RX2RestoreCalData();
			btnRestoreRX2FromEEPROM.BackColor = SystemColors.Control;
			btnRestoreRX2FromEEPROM.Enabled = true;
		}

		private void btnSaveRX2ToEEPROM_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to write the current RX2 database calibration values to the EEPROM?",
				"Overwrite EEPROM?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if(dr == DialogResult.No) return;
			
			btnSaveRX2ToEEPROM.BackColor = console.ButtonSelectedColor;
			btnSaveRX2ToEEPROM.Enabled = false;
			Thread t = new Thread(new ThreadStart(WriteRX2ToEEPROM));
			t.Name = "Write RX2 To EEPROM Thread";
			t.IsBackground = true;
			t.Priority = ThreadPriority.Normal;
			t.Start();
		}

		private void WriteRX2ToEEPROM()
		{
			console.WriteCalData();
			btnSaveRX2ToEEPROM.BackColor = SystemColors.Control;
			btnSaveRX2ToEEPROM.Enabled = true;
		}

		private void btnResetTRXChecksums_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to reset the RX1/PA checksums?",
				"Reset Checksums?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if(dr == DialogResult.No) return;
			FWCEEPROM.TRXChecksumPresent = false;
		}

		private void btnResetRX2Checksums_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Are you sure you want to reset the RX2 checksums?",
				"Reset Checksums?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if(dr == DialogResult.No) return;
			FWCEEPROM.RX2ChecksumPresent = false;
		}
	}
}

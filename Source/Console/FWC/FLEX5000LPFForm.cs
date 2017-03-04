//=================================================================
// FLEX5000LPFForm.cs
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
using System.Windows.Forms;

namespace PowerSDR
{
	public class FLEX5000LPFForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private System.Windows.Forms.GroupBox grpRX1;
		private System.Windows.Forms.RadioButton radRX1Bypass;
		private System.Windows.Forms.RadioButton radRX1B6;
		private System.Windows.Forms.RadioButton radRX1B10;
		private System.Windows.Forms.RadioButton radRX1B12;
		private System.Windows.Forms.RadioButton radRX1B15;
		private System.Windows.Forms.RadioButton radRX1B17;
		private System.Windows.Forms.RadioButton radRX1B20;
		private System.Windows.Forms.RadioButton radRX1B30;
		private System.Windows.Forms.RadioButton radRX1B40;
		private System.Windows.Forms.RadioButton radRX1B60;
		private System.Windows.Forms.RadioButton radRX1B80;
		private System.Windows.Forms.RadioButton radRX1B160;
		private System.Windows.Forms.RadioButton radRX1Auto;
		private System.Windows.Forms.GroupBox grpRX2;
		private System.Windows.Forms.RadioButton radRX2Bypass;
		private System.Windows.Forms.RadioButton radRX2B6;
		private System.Windows.Forms.RadioButton radRX2B10;
		private System.Windows.Forms.RadioButton radRX2B12;
		private System.Windows.Forms.RadioButton radRX2B15;
		private System.Windows.Forms.RadioButton radRX2B17;
		private System.Windows.Forms.RadioButton radRX2B20;
		private System.Windows.Forms.RadioButton radRX2B30;
		private System.Windows.Forms.RadioButton radRX2B40;
		private System.Windows.Forms.RadioButton radRX2B60;
		private System.Windows.Forms.RadioButton radRX2B80;
		private System.Windows.Forms.RadioButton radRX2B160;
		private System.Windows.Forms.RadioButton radRX2Auto;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor and Destructor

		public FLEX5000LPFForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			grpRX2.Enabled = FWCEEPROM.RX2OK;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FLEX5000LPFForm));
			this.grpRX1 = new System.Windows.Forms.GroupBox();
			this.radRX1Bypass = new System.Windows.Forms.RadioButton();
			this.radRX1B6 = new System.Windows.Forms.RadioButton();
			this.radRX1B10 = new System.Windows.Forms.RadioButton();
			this.radRX1B12 = new System.Windows.Forms.RadioButton();
			this.radRX1B15 = new System.Windows.Forms.RadioButton();
			this.radRX1B17 = new System.Windows.Forms.RadioButton();
			this.radRX1B20 = new System.Windows.Forms.RadioButton();
			this.radRX1B30 = new System.Windows.Forms.RadioButton();
			this.radRX1B40 = new System.Windows.Forms.RadioButton();
			this.radRX1B60 = new System.Windows.Forms.RadioButton();
			this.radRX1B80 = new System.Windows.Forms.RadioButton();
			this.radRX1B160 = new System.Windows.Forms.RadioButton();
			this.radRX1Auto = new System.Windows.Forms.RadioButton();
			this.grpRX2 = new System.Windows.Forms.GroupBox();
			this.radRX2Bypass = new System.Windows.Forms.RadioButton();
			this.radRX2B6 = new System.Windows.Forms.RadioButton();
			this.radRX2B10 = new System.Windows.Forms.RadioButton();
			this.radRX2B12 = new System.Windows.Forms.RadioButton();
			this.radRX2B15 = new System.Windows.Forms.RadioButton();
			this.radRX2B17 = new System.Windows.Forms.RadioButton();
			this.radRX2B20 = new System.Windows.Forms.RadioButton();
			this.radRX2B30 = new System.Windows.Forms.RadioButton();
			this.radRX2B40 = new System.Windows.Forms.RadioButton();
			this.radRX2B60 = new System.Windows.Forms.RadioButton();
			this.radRX2B80 = new System.Windows.Forms.RadioButton();
			this.radRX2B160 = new System.Windows.Forms.RadioButton();
			this.radRX2Auto = new System.Windows.Forms.RadioButton();
			this.grpRX1.SuspendLayout();
			this.grpRX2.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpRX1
			// 
			this.grpRX1.Controls.Add(this.radRX1Bypass);
			this.grpRX1.Controls.Add(this.radRX1B6);
			this.grpRX1.Controls.Add(this.radRX1B10);
			this.grpRX1.Controls.Add(this.radRX1B12);
			this.grpRX1.Controls.Add(this.radRX1B15);
			this.grpRX1.Controls.Add(this.radRX1B17);
			this.grpRX1.Controls.Add(this.radRX1B20);
			this.grpRX1.Controls.Add(this.radRX1B30);
			this.grpRX1.Controls.Add(this.radRX1B40);
			this.grpRX1.Controls.Add(this.radRX1B60);
			this.grpRX1.Controls.Add(this.radRX1B80);
			this.grpRX1.Controls.Add(this.radRX1B160);
			this.grpRX1.Controls.Add(this.radRX1Auto);
			this.grpRX1.Location = new System.Drawing.Point(8, 8);
			this.grpRX1.Name = "grpRX1";
			this.grpRX1.Size = new System.Drawing.Size(160, 200);
			this.grpRX1.TabIndex = 0;
			this.grpRX1.TabStop = false;
			this.grpRX1.Text = "RX1";
			// 
			// radRX1Bypass
			// 
			this.radRX1Bypass.Location = new System.Drawing.Point(80, 144);
			this.radRX1Bypass.Name = "radRX1Bypass";
			this.radRX1Bypass.Size = new System.Drawing.Size(60, 24);
			this.radRX1Bypass.TabIndex = 12;
			this.radRX1Bypass.Text = "Bypass";
			this.radRX1Bypass.CheckedChanged += new System.EventHandler(this.radRX1Bypass_CheckedChanged);
			// 
			// radRX1B6
			// 
			this.radRX1B6.Location = new System.Drawing.Point(80, 120);
			this.radRX1B6.Name = "radRX1B6";
			this.radRX1B6.Size = new System.Drawing.Size(64, 24);
			this.radRX1B6.TabIndex = 11;
			this.radRX1B6.Text = "6m";
			this.radRX1B6.CheckedChanged += new System.EventHandler(this.radRX1B6_CheckedChanged);
			// 
			// radRX1B10
			// 
			this.radRX1B10.Location = new System.Drawing.Point(80, 96);
			this.radRX1B10.Name = "radRX1B10";
			this.radRX1B10.Size = new System.Drawing.Size(64, 24);
			this.radRX1B10.TabIndex = 10;
			this.radRX1B10.Text = "10m";
			this.radRX1B10.CheckedChanged += new System.EventHandler(this.radRX1B10_CheckedChanged);
			// 
			// radRX1B12
			// 
			this.radRX1B12.Location = new System.Drawing.Point(80, 72);
			this.radRX1B12.Name = "radRX1B12";
			this.radRX1B12.Size = new System.Drawing.Size(64, 24);
			this.radRX1B12.TabIndex = 9;
			this.radRX1B12.Text = "12m";
			this.radRX1B12.CheckedChanged += new System.EventHandler(this.radRX1B12_CheckedChanged);
			// 
			// radRX1B15
			// 
			this.radRX1B15.Location = new System.Drawing.Point(80, 48);
			this.radRX1B15.Name = "radRX1B15";
			this.radRX1B15.Size = new System.Drawing.Size(64, 24);
			this.radRX1B15.TabIndex = 8;
			this.radRX1B15.Text = "15m";
			this.radRX1B15.CheckedChanged += new System.EventHandler(this.radRX1B15_CheckedChanged);
			// 
			// radRX1B17
			// 
			this.radRX1B17.Location = new System.Drawing.Point(80, 24);
			this.radRX1B17.Name = "radRX1B17";
			this.radRX1B17.Size = new System.Drawing.Size(64, 24);
			this.radRX1B17.TabIndex = 7;
			this.radRX1B17.Text = "17m";
			this.radRX1B17.CheckedChanged += new System.EventHandler(this.radRX1B17_CheckedChanged);
			// 
			// radRX1B20
			// 
			this.radRX1B20.Location = new System.Drawing.Point(16, 168);
			this.radRX1B20.Name = "radRX1B20";
			this.radRX1B20.Size = new System.Drawing.Size(64, 24);
			this.radRX1B20.TabIndex = 6;
			this.radRX1B20.Text = "20m";
			this.radRX1B20.CheckedChanged += new System.EventHandler(this.radRX1B20_CheckedChanged);
			// 
			// radRX1B30
			// 
			this.radRX1B30.Location = new System.Drawing.Point(16, 144);
			this.radRX1B30.Name = "radRX1B30";
			this.radRX1B30.Size = new System.Drawing.Size(64, 24);
			this.radRX1B30.TabIndex = 5;
			this.radRX1B30.Text = "30m";
			this.radRX1B30.CheckedChanged += new System.EventHandler(this.radRX1B30_CheckedChanged);
			// 
			// radRX1B40
			// 
			this.radRX1B40.Location = new System.Drawing.Point(16, 120);
			this.radRX1B40.Name = "radRX1B40";
			this.radRX1B40.Size = new System.Drawing.Size(64, 24);
			this.radRX1B40.TabIndex = 4;
			this.radRX1B40.Text = "40m";
			this.radRX1B40.CheckedChanged += new System.EventHandler(this.radRX1B40_CheckedChanged);
			// 
			// radRX1B60
			// 
			this.radRX1B60.Location = new System.Drawing.Point(16, 96);
			this.radRX1B60.Name = "radRX1B60";
			this.radRX1B60.Size = new System.Drawing.Size(64, 24);
			this.radRX1B60.TabIndex = 3;
			this.radRX1B60.Text = "60m";
			this.radRX1B60.CheckedChanged += new System.EventHandler(this.radRX1B60_CheckedChanged);
			// 
			// radRX1B80
			// 
			this.radRX1B80.Location = new System.Drawing.Point(16, 72);
			this.radRX1B80.Name = "radRX1B80";
			this.radRX1B80.Size = new System.Drawing.Size(64, 24);
			this.radRX1B80.TabIndex = 2;
			this.radRX1B80.Text = "80m";
			this.radRX1B80.CheckedChanged += new System.EventHandler(this.radRX1B80_CheckedChanged);
			// 
			// radRX1B160
			// 
			this.radRX1B160.Location = new System.Drawing.Point(16, 48);
			this.radRX1B160.Name = "radRX1B160";
			this.radRX1B160.Size = new System.Drawing.Size(64, 24);
			this.radRX1B160.TabIndex = 1;
			this.radRX1B160.Text = "160m";
			this.radRX1B160.CheckedChanged += new System.EventHandler(this.radRX1B160_CheckedChanged);
			// 
			// radRX1Auto
			// 
			this.radRX1Auto.Checked = true;
			this.radRX1Auto.Location = new System.Drawing.Point(16, 24);
			this.radRX1Auto.Name = "radRX1Auto";
			this.radRX1Auto.Size = new System.Drawing.Size(64, 24);
			this.radRX1Auto.TabIndex = 0;
			this.radRX1Auto.TabStop = true;
			this.radRX1Auto.Text = "Auto";
			this.radRX1Auto.CheckedChanged += new System.EventHandler(this.radRX1Auto_CheckedChanged);
			// 
			// grpRX2
			// 
			this.grpRX2.Controls.Add(this.radRX2Bypass);
			this.grpRX2.Controls.Add(this.radRX2B6);
			this.grpRX2.Controls.Add(this.radRX2B10);
			this.grpRX2.Controls.Add(this.radRX2B12);
			this.grpRX2.Controls.Add(this.radRX2B15);
			this.grpRX2.Controls.Add(this.radRX2B17);
			this.grpRX2.Controls.Add(this.radRX2B20);
			this.grpRX2.Controls.Add(this.radRX2B30);
			this.grpRX2.Controls.Add(this.radRX2B40);
			this.grpRX2.Controls.Add(this.radRX2B60);
			this.grpRX2.Controls.Add(this.radRX2B80);
			this.grpRX2.Controls.Add(this.radRX2B160);
			this.grpRX2.Controls.Add(this.radRX2Auto);
			this.grpRX2.Location = new System.Drawing.Point(176, 8);
			this.grpRX2.Name = "grpRX2";
			this.grpRX2.Size = new System.Drawing.Size(160, 200);
			this.grpRX2.TabIndex = 1;
			this.grpRX2.TabStop = false;
			this.grpRX2.Text = "RX2";
			// 
			// radRX2Bypass
			// 
			this.radRX2Bypass.Location = new System.Drawing.Point(80, 144);
			this.radRX2Bypass.Name = "radRX2Bypass";
			this.radRX2Bypass.Size = new System.Drawing.Size(60, 24);
			this.radRX2Bypass.TabIndex = 12;
			this.radRX2Bypass.Text = "Bypass";
			this.radRX2Bypass.CheckedChanged += new System.EventHandler(this.radRX2Bypass_CheckedChanged);
			// 
			// radRX2B6
			// 
			this.radRX2B6.Location = new System.Drawing.Point(80, 120);
			this.radRX2B6.Name = "radRX2B6";
			this.radRX2B6.Size = new System.Drawing.Size(64, 24);
			this.radRX2B6.TabIndex = 11;
			this.radRX2B6.Text = "6m";
			this.radRX2B6.CheckedChanged += new System.EventHandler(this.radRX2B6_CheckedChanged);
			// 
			// radRX2B10
			// 
			this.radRX2B10.Location = new System.Drawing.Point(80, 96);
			this.radRX2B10.Name = "radRX2B10";
			this.radRX2B10.Size = new System.Drawing.Size(64, 24);
			this.radRX2B10.TabIndex = 10;
			this.radRX2B10.Text = "10m";
			this.radRX2B10.CheckedChanged += new System.EventHandler(this.radRX2B10_CheckedChanged);
			// 
			// radRX2B12
			// 
			this.radRX2B12.Location = new System.Drawing.Point(80, 72);
			this.radRX2B12.Name = "radRX2B12";
			this.radRX2B12.Size = new System.Drawing.Size(64, 24);
			this.radRX2B12.TabIndex = 9;
			this.radRX2B12.Text = "12m";
			this.radRX2B12.CheckedChanged += new System.EventHandler(this.radRX2B12_CheckedChanged);
			// 
			// radRX2B15
			// 
			this.radRX2B15.Location = new System.Drawing.Point(80, 48);
			this.radRX2B15.Name = "radRX2B15";
			this.radRX2B15.Size = new System.Drawing.Size(64, 24);
			this.radRX2B15.TabIndex = 8;
			this.radRX2B15.Text = "15m";
			this.radRX2B15.CheckedChanged += new System.EventHandler(this.radRX2B15_CheckedChanged);
			// 
			// radRX2B17
			// 
			this.radRX2B17.Location = new System.Drawing.Point(80, 24);
			this.radRX2B17.Name = "radRX2B17";
			this.radRX2B17.Size = new System.Drawing.Size(64, 24);
			this.radRX2B17.TabIndex = 7;
			this.radRX2B17.Text = "17m";
			this.radRX2B17.CheckedChanged += new System.EventHandler(this.radRX2B17_CheckedChanged);
			// 
			// radRX2B20
			// 
			this.radRX2B20.Location = new System.Drawing.Point(16, 168);
			this.radRX2B20.Name = "radRX2B20";
			this.radRX2B20.Size = new System.Drawing.Size(64, 24);
			this.radRX2B20.TabIndex = 6;
			this.radRX2B20.Text = "20m";
			this.radRX2B20.CheckedChanged += new System.EventHandler(this.radRX2B20_CheckedChanged);
			// 
			// radRX2B30
			// 
			this.radRX2B30.Location = new System.Drawing.Point(16, 144);
			this.radRX2B30.Name = "radRX2B30";
			this.radRX2B30.Size = new System.Drawing.Size(64, 24);
			this.radRX2B30.TabIndex = 5;
			this.radRX2B30.Text = "30m";
			this.radRX2B30.CheckedChanged += new System.EventHandler(this.radRX2B30_CheckedChanged);
			// 
			// radRX2B40
			// 
			this.radRX2B40.Location = new System.Drawing.Point(16, 120);
			this.radRX2B40.Name = "radRX2B40";
			this.radRX2B40.Size = new System.Drawing.Size(64, 24);
			this.radRX2B40.TabIndex = 4;
			this.radRX2B40.Text = "40m";
			this.radRX2B40.CheckedChanged += new System.EventHandler(this.radRX2B40_CheckedChanged);
			// 
			// radRX2B60
			// 
			this.radRX2B60.Location = new System.Drawing.Point(16, 96);
			this.radRX2B60.Name = "radRX2B60";
			this.radRX2B60.Size = new System.Drawing.Size(64, 24);
			this.radRX2B60.TabIndex = 3;
			this.radRX2B60.Text = "60m";
			this.radRX2B60.CheckedChanged += new System.EventHandler(this.radRX2B60_CheckedChanged);
			// 
			// radRX2B80
			// 
			this.radRX2B80.Location = new System.Drawing.Point(16, 72);
			this.radRX2B80.Name = "radRX2B80";
			this.radRX2B80.Size = new System.Drawing.Size(64, 24);
			this.radRX2B80.TabIndex = 2;
			this.radRX2B80.Text = "80m";
			this.radRX2B80.CheckedChanged += new System.EventHandler(this.radRX2B80_CheckedChanged);
			// 
			// radRX2B160
			// 
			this.radRX2B160.Location = new System.Drawing.Point(16, 48);
			this.radRX2B160.Name = "radRX2B160";
			this.radRX2B160.Size = new System.Drawing.Size(64, 24);
			this.radRX2B160.TabIndex = 1;
			this.radRX2B160.Text = "160m";
			this.radRX2B160.CheckedChanged += new System.EventHandler(this.radRX2B160_CheckedChanged);
			// 
			// radRX2Auto
			// 
			this.radRX2Auto.Checked = true;
			this.radRX2Auto.Location = new System.Drawing.Point(16, 24);
			this.radRX2Auto.Name = "radRX2Auto";
			this.radRX2Auto.Size = new System.Drawing.Size(64, 24);
			this.radRX2Auto.TabIndex = 0;
			this.radRX2Auto.TabStop = true;
			this.radRX2Auto.Text = "Auto";
			this.radRX2Auto.CheckedChanged += new System.EventHandler(this.radRX2Auto_CheckedChanged);
			// 
			// FLEX5000LPFForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 214);
			this.Controls.Add(this.grpRX2);
			this.Controls.Add(this.grpRX1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FLEX5000LPFForm";
			this.Text = "FLEX-5000 LPF Override";
			this.grpRX1.ResumeLayout(false);
			this.grpRX2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region RX1

		private void radRX1Auto_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1Auto.Checked)
			{
				FWC.SetManualRX1Filter(false);
				FWC.BypassRX1Filter(false);
			}
		}

		private void radRX1B160_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B160.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(1.9f);
			}
		}

		private void radRX1B80_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B80.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(3.6f);
			}
		}

		private void radRX1B60_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B60.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(5.3465f);
			}
		}

		private void radRX1B40_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B40.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(7.1f);
			}
		}

		private void radRX1B30_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B30.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(10.12f);
			}
		}

		private void radRX1B20_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B20.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(14.1f);
			}
		}

		private void radRX1B17_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B17.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(18.11f);
			}
		}

		private void radRX1B15_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B15.Checked)
			{
				FWC.SetManualRX1Filter(true);	
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(21.1f);
			}
		}

		private void radRX1B12_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B12.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(24.9f);
			}
		}

		private void radRX1B10_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B10.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(28.4f);
			}
		}

		private void radRX1B6_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1B6.Checked)
			{
				FWC.SetManualRX1Filter(true);
				FWC.BypassRX1Filter(false);
				FWC.SetRX1Filter(50.4f);
			}
		}

		private void radRX1Bypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX1Bypass.Checked)
			{
				FWC.SetManualRX1Filter(false);
				FWC.BypassRX1Filter(true);
			}
		}

		#endregion

		#region RX2

		private void radRX2Auto_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2Auto.Checked)
			{
				FWC.SetManualRX2Filter(false);
				FWC.BypassRX2Filter(false);
			}
		}

		private void radRX2B160_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B160.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(1.9f);
			}
		}

		private void radRX2B80_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B80.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(3.6f);
			}
		}

		private void radRX2B60_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B60.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(5.3465f);
			}
		}

		private void radRX2B40_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B40.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(7.1f);
			}
		}

		private void radRX2B30_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B30.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(10.12f);
			}
		}

		private void radRX2B20_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B20.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(14.1f);
			}
		}

		private void radRX2B17_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B17.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(18.11f);
			}
		}

		private void radRX2B15_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B15.Checked)
			{
				FWC.SetManualRX2Filter(true);	
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(21.1f);
			}
		}

		private void radRX2B12_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B12.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(24.9f);
			}
		}

		private void radRX2B10_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B10.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(28.4f);
			}
		}

		private void radRX2B6_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2B6.Checked)
			{
				FWC.SetManualRX2Filter(true);
				FWC.BypassRX2Filter(false);
				FWC.SetRX2Filter(50.4f);
			}
		}

		private void radRX2Bypass_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radRX2Bypass.Checked)
			{
				FWC.SetManualRX2Filter(false);
				FWC.BypassRX2Filter(true);
			}
		}

		#endregion
	}
}

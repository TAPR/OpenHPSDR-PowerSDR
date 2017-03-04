//=================================================================
// FLEX5000RelayForm.cs
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
	public class FLEX5000RelayForm : System.Windows.Forms.Form
	{
		#region Variable Declaration

		private Console console;
		private System.Windows.Forms.Label lblTurn;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label lblWhen;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.ComboBox comboBox5;
		private System.Windows.Forms.Button btnMoreLess;
		private System.Windows.Forms.ComboBox comboBox6;
		private System.Windows.Forms.ComboBox comboBox7;
		private System.Windows.Forms.ComboBox comboBox8;
		private System.Windows.Forms.Label lblAnd;
		private System.Windows.Forms.ListBox lstRules;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnMoveToTop;
		private System.Windows.Forms.Button btnMoveToEnd;
		private System.Windows.Forms.Button btnTX1;
		private System.Windows.Forms.Button btnTX2;
		private System.Windows.Forms.Button btnTX3;
		private System.Windows.Forms.Button btnUpdate;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor and Destructor

		public FLEX5000RelayForm(Console c)
		{
			InitializeComponent();
			console = c;

			comboBox1.SelectedIndex = 0;
			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 0;
			comboBox4.SelectedIndex = 0;
			comboBox6.SelectedIndex = 0;
			comboBox7.SelectedIndex = 0;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FLEX5000RelayForm));
			this.lblTurn = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.lblWhen = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.comboBox5 = new System.Windows.Forms.ComboBox();
			this.btnMoreLess = new System.Windows.Forms.Button();
			this.comboBox6 = new System.Windows.Forms.ComboBox();
			this.comboBox7 = new System.Windows.Forms.ComboBox();
			this.comboBox8 = new System.Windows.Forms.ComboBox();
			this.lblAnd = new System.Windows.Forms.Label();
			this.lstRules = new System.Windows.Forms.ListBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnMoveToTop = new System.Windows.Forms.Button();
			this.btnMoveToEnd = new System.Windows.Forms.Button();
			this.btnTX1 = new System.Windows.Forms.Button();
			this.btnTX2 = new System.Windows.Forms.Button();
			this.btnTX3 = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTurn
			// 
			this.lblTurn.Location = new System.Drawing.Point(16, 16);
			this.lblTurn.Name = "lblTurn";
			this.lblTurn.Size = new System.Drawing.Size(32, 23);
			this.lblTurn.TabIndex = 0;
			this.lblTurn.Text = "Turn";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Items.AddRange(new object[] {
														   "On",
														   "Off"});
			this.comboBox1.Location = new System.Drawing.Point(48, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(48, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.Items.AddRange(new object[] {
														   "TX1",
														   "TX2",
														   "TX3"});
			this.comboBox2.Location = new System.Drawing.Point(104, 8);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(48, 21);
			this.comboBox2.TabIndex = 2;
			// 
			// lblWhen
			// 
			this.lblWhen.Location = new System.Drawing.Point(160, 16);
			this.lblWhen.Name = "lblWhen";
			this.lblWhen.Size = new System.Drawing.Size(32, 23);
			this.lblWhen.TabIndex = 3;
			this.lblWhen.Text = "when";
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.Items.AddRange(new object[] {
														   "Power",
														   "Band",
														   "Mode"});
			this.comboBox3.Location = new System.Drawing.Point(200, 8);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(56, 21);
			this.comboBox3.TabIndex = 4;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
			// 
			// comboBox4
			// 
			this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox4.Items.AddRange(new object[] {
														   "Is",
														   "Is_Not"});
			this.comboBox4.Location = new System.Drawing.Point(264, 8);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(56, 21);
			this.comboBox4.TabIndex = 5;
			// 
			// comboBox5
			// 
			this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox5.Items.AddRange(new object[] {
														   "On",
														   "Off"});
			this.comboBox5.Location = new System.Drawing.Point(328, 8);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new System.Drawing.Size(56, 21);
			this.comboBox5.TabIndex = 6;
			// 
			// btnMoreLess
			// 
			this.btnMoreLess.Location = new System.Drawing.Point(392, 8);
			this.btnMoreLess.Name = "btnMoreLess";
			this.btnMoreLess.Size = new System.Drawing.Size(40, 23);
			this.btnMoreLess.TabIndex = 7;
			this.btnMoreLess.Text = "More";
			this.btnMoreLess.Click += new System.EventHandler(this.btnMoreLess_Click);
			// 
			// comboBox6
			// 
			this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox6.Items.AddRange(new object[] {
														   "Power",
														   "Band",
														   "Mode"});
			this.comboBox6.Location = new System.Drawing.Point(200, 40);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new System.Drawing.Size(56, 21);
			this.comboBox6.TabIndex = 11;
			this.comboBox6.Visible = false;
			this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
			// 
			// comboBox7
			// 
			this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox7.Items.AddRange(new object[] {
														   "Is",
														   "Is_Not"});
			this.comboBox7.Location = new System.Drawing.Point(264, 40);
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new System.Drawing.Size(56, 21);
			this.comboBox7.TabIndex = 10;
			this.comboBox7.Visible = false;
			// 
			// comboBox8
			// 
			this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox8.Items.AddRange(new object[] {
														   "On",
														   "Off"});
			this.comboBox8.Location = new System.Drawing.Point(328, 40);
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new System.Drawing.Size(56, 21);
			this.comboBox8.TabIndex = 9;
			this.comboBox8.Visible = false;
			// 
			// lblAnd
			// 
			this.lblAnd.Location = new System.Drawing.Point(160, 48);
			this.lblAnd.Name = "lblAnd";
			this.lblAnd.Size = new System.Drawing.Size(32, 23);
			this.lblAnd.TabIndex = 8;
			this.lblAnd.Text = "and";
			this.lblAnd.Visible = false;
			// 
			// lstRules
			// 
			this.lstRules.Location = new System.Drawing.Point(24, 88);
			this.lstRules.Name = "lstRules";
			this.lstRules.Size = new System.Drawing.Size(288, 121);
			this.lstRules.TabIndex = 12;
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(120, 224);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.TabIndex = 13;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(32, 224);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.TabIndex = 14;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Location = new System.Drawing.Point(328, 120);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(80, 23);
			this.btnMoveUp.TabIndex = 15;
			this.btnMoveUp.Text = "Move Up";
			this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Location = new System.Drawing.Point(328, 152);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(80, 23);
			this.btnMoveDown.TabIndex = 16;
			this.btnMoveDown.Text = "Move Down";
			this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(208, 224);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 17;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnMoveToTop
			// 
			this.btnMoveToTop.Location = new System.Drawing.Point(328, 88);
			this.btnMoveToTop.Name = "btnMoveToTop";
			this.btnMoveToTop.Size = new System.Drawing.Size(80, 23);
			this.btnMoveToTop.TabIndex = 18;
			this.btnMoveToTop.Text = "Move To Top";
			this.btnMoveToTop.Click += new System.EventHandler(this.btnMoveToTop_Click);
			// 
			// btnMoveToEnd
			// 
			this.btnMoveToEnd.Location = new System.Drawing.Point(328, 184);
			this.btnMoveToEnd.Name = "btnMoveToEnd";
			this.btnMoveToEnd.Size = new System.Drawing.Size(80, 23);
			this.btnMoveToEnd.TabIndex = 19;
			this.btnMoveToEnd.Text = "Move To End";
			this.btnMoveToEnd.Click += new System.EventHandler(this.btnMoveToEnd_Click);
			// 
			// btnTX1
			// 
			this.btnTX1.BackColor = System.Drawing.Color.Red;
			this.btnTX1.Location = new System.Drawing.Point(320, 232);
			this.btnTX1.Name = "btnTX1";
			this.btnTX1.Size = new System.Drawing.Size(40, 23);
			this.btnTX1.TabIndex = 20;
			this.btnTX1.Text = "TX1";
			// 
			// btnTX2
			// 
			this.btnTX2.BackColor = System.Drawing.Color.Red;
			this.btnTX2.Location = new System.Drawing.Point(368, 232);
			this.btnTX2.Name = "btnTX2";
			this.btnTX2.Size = new System.Drawing.Size(40, 23);
			this.btnTX2.TabIndex = 21;
			this.btnTX2.Text = "TX2";
			// 
			// btnTX3
			// 
			this.btnTX3.BackColor = System.Drawing.Color.Red;
			this.btnTX3.Location = new System.Drawing.Point(416, 232);
			this.btnTX3.Name = "btnTX3";
			this.btnTX3.Size = new System.Drawing.Size(40, 23);
			this.btnTX3.TabIndex = 22;
			this.btnTX3.Text = "TX3";
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(232, 264);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.TabIndex = 23;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// FLEX5000RelayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 294);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.btnTX3);
			this.Controls.Add(this.btnTX2);
			this.Controls.Add(this.btnTX1);
			this.Controls.Add(this.btnMoveToEnd);
			this.Controls.Add(this.btnMoveToTop);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.lstRules);
			this.Controls.Add(this.comboBox6);
			this.Controls.Add(this.comboBox7);
			this.Controls.Add(this.comboBox8);
			this.Controls.Add(this.lblAnd);
			this.Controls.Add(this.btnMoreLess);
			this.Controls.Add(this.comboBox5);
			this.Controls.Add(this.comboBox4);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.lblWhen);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.lblTurn);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FLEX5000RelayForm";
			this.Text = "FLEX-5000 Relay Controls";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FLEX5000RelayForm_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		#region Misc Routines

		private string BandToString(Band b)
		{
			string ret_val = "";
			switch(b)
			{
				case Band.GEN: ret_val = "GEN"; break;
				case Band.B160M: ret_val = "160m"; break;
				case Band.B80M: ret_val = "80m"; break;
				case Band.B60M: ret_val = "60m"; break;
				case Band.B40M: ret_val = "40m"; break;
				case Band.B30M: ret_val = "30m"; break;
				case Band.B20M: ret_val = "20m"; break;
				case Band.B17M: ret_val = "17m"; break;
				case Band.B15M: ret_val = "15m"; break;
				case Band.B12M: ret_val = "12m"; break;
				case Band.B10M: ret_val = "10m"; break;
				case Band.B6M: ret_val = "6m"; break;
				case Band.B2M: ret_val = "2m"; break;
				case Band.WWV: ret_val = "WWV"; break;
				case Band.VHF0: ret_val = "VHF0"; break;
				case Band.VHF1: ret_val = "VHF1"; break;
				case Band.VHF2: ret_val = "VHF2"; break;
				case Band.VHF3: ret_val = "VHF3"; break;
				case Band.VHF4: ret_val = "VHF4"; break;
				case Band.VHF5: ret_val = "VHF5"; break;
				case Band.VHF6: ret_val = "VHF6"; break;
				case Band.VHF7: ret_val = "VHF7"; break;
				case Band.VHF8: ret_val = "VHF8"; break;
				case Band.VHF9: ret_val = "VHF9"; break;
				case Band.VHF10: ret_val = "VHF10"; break;
				case Band.VHF11: ret_val = "VHF11"; break;
				case Band.VHF12: ret_val = "VHF12"; break;
				case Band.VHF13: ret_val = "VHF13"; break;
			}
			return ret_val;
		}

		private Band StringToBand(string s)
		{
			Band b = Band.GEN;
			switch(s)
			{
				case "GEN": b = Band.GEN; break;
				case "160m": b = Band.B160M; break;
				case "80m": b = Band.B80M; break;
				case "60m": b = Band.B60M; break;
				case "40m": b = Band.B40M; break;
				case "30m": b = Band.B30M; break;
				case "20m": b = Band.B20M; break;
				case "17m": b = Band.B17M; break;
				case "15m": b = Band.B15M; break;
				case "12m": b = Band.B12M; break;
				case "10m": b = Band.B10M; break;
				case "6m": b = Band.B6M; break;
				case "2m": b = Band.B2M; break;
				case "WWV": b = Band.WWV; break;
				case "VFO0": b = Band.VHF0; break;
				case "VFO1": b = Band.VHF1; break;
				case "VFO2": b = Band.VHF2; break;
				case "VFO3": b = Band.VHF3; break;
				case "VFO4": b = Band.VHF4; break;
				case "VFO5": b = Band.VHF5; break;
				case "VFO6": b = Band.VHF6; break;
				case "VFO7": b = Band.VHF7; break;
				case "VFO8": b = Band.VHF8; break;
				case "VFO9": b = Band.VHF9; break;
				case "VFO10": b = Band.VHF10; break;
				case "VFO11": b = Band.VHF11; break;
				case "VFO12": b = Band.VHF12; break;
				case "VFO13": b = Band.VHF13; break;
			}
			return b;
		}

		public void UpdateRelayState(out bool tx1, out bool tx2, out bool tx3)
		{
			bool t1=false, t2=false, t3=false;
			bool power = console.PowerOn;
			Band band = console.TXBand;
			DSPMode mode = console.RX1DSPMode;

			for(int i=lstRules.Items.Count-1; i>=0; i--)
			{
				string s = (string)lstRules.Items[i];
				string[] words = s.Split(' ');

				bool c1=false, c2=false;
				switch(words[4])
				{
					case "Power":
						if((words[5] == "Is" && 
							((words[6] == "On" && power) ||
							(words[6] == "Off" && !power))) ||
							(words[5] == "Is_Not" &&
							((words[6] == "On" && !power) ||
							(words[6] == "Off" && power))))
						{
							c1 = true;
						}
						break;
					case "Band":
						if((words[5] == "Is" && StringToBand(words[6]) == band) ||
							(words[5] == "Is_Not" && StringToBand(words[6]) != band))
						{
							c1 = true;
						}
						break;
					case "Mode":
						if((words[5] == "Is" && words[6] == mode.ToString()) ||
							(words[5] == "Is_Not" && words[6] != mode.ToString()))
						{
							c1 = true;
						}
						break;
				}

				if(words.Length == 7) c2 = true;
				else if(words.Length == 11)
				{
					switch(words[8])
					{
						case "Power":
							if((words[9] == "Is" && 
								((words[10] == "On" && power) ||
								(words[10] == "Off" && !power))) ||
								(words[9] == "Is_Not" &&
								((words[10] == "On" && !power) ||
								(words[10] == "Off" && power))))
							{
								c2 = true;
							}
							break;
						case "Band":
							if((words[9] == "Is" && StringToBand(words[10]) == band) ||
								(words[9] == "Is_Not" && StringToBand(words[10]) != band))
							{
								c2 = true;
							}
							break;
						case "Mode":
							if((words[9] == "Is" && words[10] == mode.ToString()) ||
								(words[9] == "Is_Not" && words[10] != mode.ToString()))
							{
								c2 = true;
							}
							break;
					}
				}

				if(c1 && c2)
				{
					if(words[1] == "On")
					{
						switch(words[2])
						{
							case "TX1": t1 = true; break;
							case "TX2": t2 = true; break;
							case "TX3": t3 = true; break;
						}
					}
					else // words[1] == "Off"
					{
						switch(words[2])
						{
							case "TX1": t1 = false; break;
							case "TX2": t2 = false; break;
							case "TX3": t3 = false; break;
						}
					}
				}
				else
				{
					if(words[1] == "On")
					{
						switch(words[2])
						{
							case "TX1": t1 = false; break;
							case "TX2": t2 = false; break;
							case "TX3": t3 = false; break;
						}
					}
					else // words[1] == "Off"
					{
						switch(words[2])
						{
							case "TX1": t1 = true; break;
							case "TX2": t2 = true; break;
							case "TX3": t3 = true; break;
						}
					}
				}
			}

			tx1 = t1;
			tx2 = t2;
			tx3 = t3;

			if(tx1) btnTX1.BackColor = Color.Green;
			else btnTX1.BackColor = Color.Red;

			if(tx2) btnTX2.BackColor = Color.Green;
			else btnTX2.BackColor = Color.Red;

			if(tx3) btnTX3.BackColor = Color.Green;
			else btnTX3.BackColor = Color.Red;
		}

		#endregion

		#region Event Handlers

		private void comboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			comboBox5.Items.Clear();
			switch(comboBox3.Text)
			{
				case "Power":					
					comboBox5.Items.Add("On");
					comboBox5.Items.Add("Off");
					break;
				case "Band":
					for(int i = (int)Band.FIRST+1; i<(int)Band.LAST; i++)
						comboBox5.Items.Add(BandToString((Band)i));
							break;
				case "Mode":
					for(int i = (int)DSPMode.FIRST+1; i<(int)DSPMode.LAST; i++)
						comboBox5.Items.Add(((DSPMode)i).ToString());
					break;
			}
			comboBox5.SelectedIndex = 0;
		}

		private void comboBox6_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			comboBox8.Items.Clear();
			switch(comboBox6.Text)
			{
				case "Power":					
					comboBox8.Items.Add("On");
					comboBox8.Items.Add("Off");
					break;
				case "Band":
					for(int i = (int)Band.FIRST+1; i<(int)Band.LAST; i++)
						comboBox8.Items.Add(BandToString((Band)i));
					break;
				case "Mode":
					for(int i = (int)DSPMode.FIRST+1; i<(int)DSPMode.LAST; i++)
						comboBox8.Items.Add(((DSPMode)i).ToString());
					break;
			}
			comboBox8.SelectedIndex = 0;

		}	

		private void btnMoreLess_Click(object sender, System.EventArgs e)
		{
			if(btnMoreLess.Text == "More")
			{
				lblAnd.Visible = true;
				comboBox6.Visible = true;
				comboBox7.Visible = true;
				comboBox8.Visible = true;
				btnMoreLess.Text = "Less";
			}
			else if(btnMoreLess.Text == "Less")
			{
				lblAnd.Visible = false;
				comboBox6.Visible = false;
				comboBox7.Visible = false;
				comboBox8.Visible = false;
				btnMoreLess.Text = "More";
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			string s = "Turn "+comboBox1.Text+" "+comboBox2.Text+" when "+comboBox3.Text+
				" "+comboBox4.Text+" "+comboBox5.Text;
			if(btnMoreLess.Text == "Less")
				s += " and "+comboBox6.Text+" "+comboBox7.Text+" "+comboBox8.Text;
			lstRules.Items.Add(s);
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if(lstRules.SelectedIndex >= 0)
				lstRules.Items.RemoveAt(lstRules.SelectedIndex);
		}

		private void btnMoveToTop_Click(object sender, System.EventArgs e)
		{
			int index = lstRules.SelectedIndex;
			if(index >= 1 && lstRules.Items.Count > 1)
			{
				object temp = lstRules.Items[index];
				lstRules.Items.RemoveAt(index);
				lstRules.Items.Insert(0, temp);
				lstRules.SelectedIndex = 0;
			}
		}

		private void btnMoveUp_Click(object sender, System.EventArgs e)
		{
			int index = lstRules.SelectedIndex;
			if(index >= 1 && lstRules.Items.Count > 1)
			{
				object temp = lstRules.Items[index];
				lstRules.Items.RemoveAt(index);
				lstRules.Items.Insert(index-1, temp);
				lstRules.SelectedIndex = index-1;
			}
		}

		private void btnMoveDown_Click(object sender, System.EventArgs e)
		{
			int index = lstRules.SelectedIndex;
			if(index >= 0 && index < lstRules.Items.Count-1 && lstRules.Items.Count > 1)
			{
				object temp = lstRules.Items[index];
				lstRules.Items.RemoveAt(index);
				lstRules.Items.Insert(index+1, temp);
				lstRules.SelectedIndex = index+1;
			}
		}

		private void btnMoveToEnd_Click(object sender, System.EventArgs e)
		{
			int index = lstRules.SelectedIndex;
			if(index >= 0 && index < lstRules.Items.Count-1 && lstRules.Items.Count > 1)
			{
				object temp = lstRules.Items[index];
				lstRules.Items.RemoveAt(index);
				lstRules.Items.Add(temp);
				lstRules.SelectedIndex = lstRules.Items.Count-1;
			}		
		}

		private void FLEX5000RelayForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			lstRules.Items.Clear();
		}

		#endregion

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			bool b1, b2, b3;
			UpdateRelayState(out b1, out b2, out b3);
		}
	}
}

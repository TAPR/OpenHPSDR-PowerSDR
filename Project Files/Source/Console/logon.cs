//=================================================================
// logon.cs
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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PowerSDR
{
	public class LogOn : System.Windows.Forms.Form
	{
		Console console;
		string data;
		int count;

		private System.Windows.Forms.TextBoxTS txtUser;
		private System.Windows.Forms.LabelTS lblUser;
		private System.Windows.Forms.LabelTS lblPass;
		private System.Windows.Forms.TextBoxTS txtPass;
		private System.Windows.Forms.ButtonTS btnSubmit;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.ComponentModel.Container components = null;

		public LogOn(Console c)
		{
			InitializeComponent();
			console = c;

			StreamReader sr = File.OpenText("extended.edf");
			data = sr.ReadLine();
			sr.Close();

			count = 4;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtUser = new System.Windows.Forms.TextBoxTS();
			this.lblUser = new System.Windows.Forms.LabelTS();
			this.lblPass = new System.Windows.Forms.LabelTS();
			this.txtPass = new System.Windows.Forms.TextBoxTS();
			this.btnSubmit = new System.Windows.Forms.ButtonTS();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(88, 16);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(128, 20);
			this.txtUser.TabIndex = 0;
			this.txtUser.Text = "";
			// 
			// lblUser
			// 
			this.lblUser.Location = new System.Drawing.Point(8, 16);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(72, 16);
			this.lblUser.TabIndex = 1;
			this.lblUser.Text = "User Name:";
			// 
			// lblPass
			// 
			this.lblPass.Location = new System.Drawing.Point(8, 40);
			this.lblPass.Name = "lblPass";
			this.lblPass.Size = new System.Drawing.Size(72, 16);
			this.lblPass.TabIndex = 3;
			this.lblPass.Text = "Password:";
			// 
			// txtPass
			// 
			this.txtPass.Location = new System.Drawing.Point(88, 40);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.Size = new System.Drawing.Size(128, 20);
			this.txtPass.TabIndex = 2;
			this.txtPass.Text = "";
			// 
			// btnSubmit
			// 
			this.btnSubmit.Location = new System.Drawing.Point(80, 80);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.TabIndex = 4;
			this.btnSubmit.Text = "Submit";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 110);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(232, 22);
			this.statusBar1.TabIndex = 5;
			// 
			// UserPass
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 132);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.lblPass);
			this.Controls.Add(this.txtPass);
			this.Controls.Add(this.lblUser);
			this.Controls.Add(this.txtUser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MinimizeBox = false;
			this.Name = "UserPass";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Logon";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			Encryption enc = new Encryption("FlexRadio Systems", "PowerSDR");
			string text = enc.Encrypt(txtUser.Text+"/"+txtPass.Text);
			if(text == data)
			{
				MessageBox.Show("Access Granted");
				console.Extended = true;
				
				ArrayList a = new ArrayList();
				a.Add("extended/"+text);
				DB.SaveVars("State", ref a);
                
				this.Close();
				return;
			}
			else
			{
				count--;
				if(count == 0)
				{
					MessageBox.Show("Access Denied");
					this.Close();
					return;
				}
				statusBar1.Text = "Invalid User/Pass.  "+count.ToString()+" tries left.";
			}
		}
	}

	public class Encryption
	{
		public Encryption(string k, string v)
		{
			key = k.PadRight(24, '0');
			vector = v.PadRight(8, '0');
		}

		private TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

		private string key;
		private string vector;

		private Byte[] Key
		{
			get { return Encoding.Default.GetBytes(key); }
		}

		private Byte[] Vector
		{
			get { return Encoding.Default.GetBytes(vector); }
		}

		public string Encrypt(string text)
		{
			return Transform(text, des.CreateEncryptor(Key, Vector));
		}

		public string Decrypt(string encryptedText)
		{
			return Transform(encryptedText, des.CreateDecryptor(Key, Vector));
		}
	        
		private string Transform(string Text, ICryptoTransform CryptoTransform)
		{
			MemoryStream stream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(stream, CryptoTransform, CryptoStreamMode.Write);
			Byte[] input = Encoding.Default.GetBytes(Text);

			cryptoStream.Write(input, 0, input.Length);
			cryptoStream.FlushFinalBlock();

			return Encoding.Default.GetString(stream.ToArray());
		}
	}
}

namespace PowerSDR
{
    partial class PSForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnPSRestore = new System.Windows.Forms.ButtonTS();
            this.btnPSSave = new System.Windows.Forms.ButtonTS();
            this.comboPSTint = new System.Windows.Forms.ComboBoxTS();
            this.lblPSTint = new System.Windows.Forms.LabelTS();
            this.chkPSStbl = new System.Windows.Forms.CheckBoxTS();
            this.chkPSMap = new System.Windows.Forms.CheckBoxTS();
            this.chkPSPin = new System.Windows.Forms.CheckBoxTS();
            this.btnPSAdvanced = new System.Windows.Forms.ButtonTS();
            this.chkPSAutoAttenuate = new System.Windows.Forms.CheckBoxTS();
            this.btnPSAmpView = new System.Windows.Forms.ButtonTS();
            this.chkPSRelaxPtol = new System.Windows.Forms.CheckBoxTS();
            this.lblDisabled = new System.Windows.Forms.LabelTS();
            this.btnPSTwoToneGen = new System.Windows.Forms.ButtonTS();
            this.labelTS4 = new System.Windows.Forms.LabelTS();
            this.udPSMoxDelay = new System.Windows.Forms.NumericUpDownTS();
            this.labelTS2 = new System.Windows.Forms.LabelTS();
            this.udPSPhnum = new System.Windows.Forms.NumericUpDownTS();
            this.grpPSInfo = new System.Windows.Forms.GroupBoxTS();
            this.lblPSInfo5 = new System.Windows.Forms.LabelTS();
            this.labelTS14 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo13 = new System.Windows.Forms.LabelTS();
            this.labelTS12 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo6 = new System.Windows.Forms.LabelTS();
            this.labelTS10 = new System.Windows.Forms.LabelTS();
            this.PSdispRX = new System.Windows.Forms.TextBoxTS();
            this.labelTS6 = new System.Windows.Forms.LabelTS();
            this.GetPSpeak = new System.Windows.Forms.TextBoxTS();
            this.labelTS3 = new System.Windows.Forms.LabelTS();
            this.PSpeak = new System.Windows.Forms.TextBoxTS();
            this.labelTS5 = new System.Windows.Forms.LabelTS();
            this.lblPSfb2 = new System.Windows.Forms.LabelTS();
            this.labelTS1 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo15 = new System.Windows.Forms.LabelTS();
            this.labelTS146 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo3 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo2 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo1 = new System.Windows.Forms.LabelTS();
            this.lblPSInfo0 = new System.Windows.Forms.LabelTS();
            this.labelTS143 = new System.Windows.Forms.LabelTS();
            this.labelTS144 = new System.Windows.Forms.LabelTS();
            this.labelTS142 = new System.Windows.Forms.LabelTS();
            this.labelTS141 = new System.Windows.Forms.LabelTS();
            this.btnPSReset = new System.Windows.Forms.ButtonTS();
            this.btnPSCalibrate = new System.Windows.Forms.ButtonTS();
            this.labelTS140 = new System.Windows.Forms.LabelTS();
            this.udPSCalWait = new System.Windows.Forms.NumericUpDownTS();
            this.chkPSOnTop = new System.Windows.Forms.CheckBoxTS();
            ((System.ComponentModel.ISupportInitialize)(this.udPSMoxDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPSPhnum)).BeginInit();
            this.grpPSInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPSCalWait)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnPSRestore
            // 
            this.btnPSRestore.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSRestore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPSRestore.Image = null;
            this.btnPSRestore.Location = new System.Drawing.Point(399, 12);
            this.btnPSRestore.Name = "btnPSRestore";
            this.btnPSRestore.Size = new System.Drawing.Size(71, 20);
            this.btnPSRestore.TabIndex = 0;
            this.btnPSRestore.Text = "Restore";
            this.btnPSRestore.UseVisualStyleBackColor = false;
            this.btnPSRestore.Click += new System.EventHandler(this.btnPSRestore_Click);
            // 
            // btnPSSave
            // 
            this.btnPSSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPSSave.Image = null;
            this.btnPSSave.Location = new System.Drawing.Point(322, 12);
            this.btnPSSave.Name = "btnPSSave";
            this.btnPSSave.Size = new System.Drawing.Size(71, 20);
            this.btnPSSave.TabIndex = 4;
            this.btnPSSave.Text = "Save";
            this.btnPSSave.UseVisualStyleBackColor = false;
            this.btnPSSave.Click += new System.EventHandler(this.btnPSSave_Click);
            // 
            // comboPSTint
            // 
            this.comboPSTint.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboPSTint.FormattingEnabled = true;
            this.comboPSTint.Items.AddRange(new object[] {
            "0.5",
            "1.1",
            "2.5"});
            this.comboPSTint.Location = new System.Drawing.Point(490, 126);
            this.comboPSTint.Name = "comboPSTint";
            this.comboPSTint.Size = new System.Drawing.Size(57, 21);
            this.comboPSTint.TabIndex = 48;
            this.comboPSTint.Text = "0.5";
            this.comboPSTint.SelectedIndexChanged += new System.EventHandler(this.comboPSTint_SelectedIndexChanged);
            // 
            // lblPSTint
            // 
            this.lblPSTint.AutoSize = true;
            this.lblPSTint.ForeColor = System.Drawing.Color.White;
            this.lblPSTint.Image = null;
            this.lblPSTint.Location = new System.Drawing.Point(431, 130);
            this.lblPSTint.Name = "lblPSTint";
            this.lblPSTint.Size = new System.Drawing.Size(54, 13);
            this.lblPSTint.TabIndex = 47;
            this.lblPSTint.Text = "TINT (dB)";
            // 
            // chkPSStbl
            // 
            this.chkPSStbl.AutoSize = true;
            this.chkPSStbl.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.chkPSStbl.Image = null;
            this.chkPSStbl.Location = new System.Drawing.Point(434, 108);
            this.chkPSStbl.Name = "chkPSStbl";
            this.chkPSStbl.Size = new System.Drawing.Size(53, 17);
            this.chkPSStbl.TabIndex = 45;
            this.chkPSStbl.Text = "STBL";
            this.chkPSStbl.UseVisualStyleBackColor = true;
            this.chkPSStbl.CheckedChanged += new System.EventHandler(this.chkPSStbl_CheckedChanged);
            // 
            // chkPSMap
            // 
            this.chkPSMap.AutoSize = true;
            this.chkPSMap.Checked = true;
            this.chkPSMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPSMap.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.chkPSMap.Image = null;
            this.chkPSMap.Location = new System.Drawing.Point(434, 85);
            this.chkPSMap.Name = "chkPSMap";
            this.chkPSMap.Size = new System.Drawing.Size(49, 17);
            this.chkPSMap.TabIndex = 44;
            this.chkPSMap.Text = "MAP";
            this.chkPSMap.UseVisualStyleBackColor = true;
            this.chkPSMap.CheckedChanged += new System.EventHandler(this.chkPSMap_CheckedChanged);
            // 
            // chkPSPin
            // 
            this.chkPSPin.AutoSize = true;
            this.chkPSPin.Checked = true;
            this.chkPSPin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPSPin.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.chkPSPin.Image = null;
            this.chkPSPin.Location = new System.Drawing.Point(434, 62);
            this.chkPSPin.Name = "chkPSPin";
            this.chkPSPin.Size = new System.Drawing.Size(44, 17);
            this.chkPSPin.TabIndex = 43;
            this.chkPSPin.Text = "PIN";
            this.chkPSPin.UseVisualStyleBackColor = true;
            this.chkPSPin.CheckedChanged += new System.EventHandler(this.chkPSPin_CheckedChanged);
            // 
            // btnPSAdvanced
            // 
            this.btnPSAdvanced.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSAdvanced.Image = null;
            this.btnPSAdvanced.Location = new System.Drawing.Point(245, 12);
            this.btnPSAdvanced.Name = "btnPSAdvanced";
            this.btnPSAdvanced.Size = new System.Drawing.Size(71, 20);
            this.btnPSAdvanced.TabIndex = 42;
            this.btnPSAdvanced.Text = "Advanced";
            this.btnPSAdvanced.UseVisualStyleBackColor = false;
            this.btnPSAdvanced.Click += new System.EventHandler(this.btnPSAdvanced_Click);
            // 
            // chkPSAutoAttenuate
            // 
            this.chkPSAutoAttenuate.AutoSize = true;
            this.chkPSAutoAttenuate.Checked = true;
            this.chkPSAutoAttenuate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPSAutoAttenuate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPSAutoAttenuate.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.chkPSAutoAttenuate.Image = null;
            this.chkPSAutoAttenuate.Location = new System.Drawing.Point(184, 62);
            this.chkPSAutoAttenuate.Name = "chkPSAutoAttenuate";
            this.chkPSAutoAttenuate.Size = new System.Drawing.Size(97, 17);
            this.chkPSAutoAttenuate.TabIndex = 41;
            this.chkPSAutoAttenuate.Text = "Auto-Attenuate";
            this.chkPSAutoAttenuate.UseVisualStyleBackColor = true;
            this.chkPSAutoAttenuate.CheckedChanged += new System.EventHandler(this.chkPSAutoAttenuate_CheckedChanged);
            // 
            // btnPSAmpView
            // 
            this.btnPSAmpView.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSAmpView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPSAmpView.Image = null;
            this.btnPSAmpView.Location = new System.Drawing.Point(168, 12);
            this.btnPSAmpView.Name = "btnPSAmpView";
            this.btnPSAmpView.Size = new System.Drawing.Size(71, 20);
            this.btnPSAmpView.TabIndex = 40;
            this.btnPSAmpView.Text = "AmpView";
            this.btnPSAmpView.UseVisualStyleBackColor = false;
            this.btnPSAmpView.Click += new System.EventHandler(this.btnPSAmpView_Click);
            // 
            // chkPSRelaxPtol
            // 
            this.chkPSRelaxPtol.AutoSize = true;
            this.chkPSRelaxPtol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPSRelaxPtol.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.chkPSRelaxPtol.Image = null;
            this.chkPSRelaxPtol.Location = new System.Drawing.Point(184, 85);
            this.chkPSRelaxPtol.Name = "chkPSRelaxPtol";
            this.chkPSRelaxPtol.Size = new System.Drawing.Size(104, 17);
            this.chkPSRelaxPtol.TabIndex = 39;
            this.chkPSRelaxPtol.Text = "Relax Tolerance";
            this.chkPSRelaxPtol.UseVisualStyleBackColor = true;
            this.chkPSRelaxPtol.CheckedChanged += new System.EventHandler(this.chkPSRelaxPtol_CheckedChanged);
            // 
            // lblDisabled
            // 
            this.lblDisabled.AutoSize = true;
            this.lblDisabled.Font = new System.Drawing.Font("Cambria", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisabled.ForeColor = System.Drawing.Color.Crimson;
            this.lblDisabled.Image = null;
            this.lblDisabled.Location = new System.Drawing.Point(14, 35);
            this.lblDisabled.Name = "lblDisabled";
            this.lblDisabled.Size = new System.Drawing.Size(238, 19);
            this.lblDisabled.TabIndex = 38;
            this.lblDisabled.Text = "PureSignal is Disabled in Setup!";
            // 
            // btnPSTwoToneGen
            // 
            this.btnPSTwoToneGen.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSTwoToneGen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPSTwoToneGen.Image = null;
            this.btnPSTwoToneGen.Location = new System.Drawing.Point(14, 12);
            this.btnPSTwoToneGen.Name = "btnPSTwoToneGen";
            this.btnPSTwoToneGen.Size = new System.Drawing.Size(71, 20);
            this.btnPSTwoToneGen.TabIndex = 37;
            this.btnPSTwoToneGen.Text = "Two-Tone";
            this.btnPSTwoToneGen.UseVisualStyleBackColor = false;
            this.btnPSTwoToneGen.Click += new System.EventHandler(this.btnPSTwoToneGen_Click);
            // 
            // labelTS4
            // 
            this.labelTS4.AutoSize = true;
            this.labelTS4.ForeColor = System.Drawing.Color.White;
            this.labelTS4.Image = null;
            this.labelTS4.Location = new System.Drawing.Point(11, 63);
            this.labelTS4.Name = "labelTS4";
            this.labelTS4.Size = new System.Drawing.Size(82, 13);
            this.labelTS4.TabIndex = 30;
            this.labelTS4.Text = "MOX Wait (sec)";
            // 
            // udPSMoxDelay
            // 
            this.udPSMoxDelay.DecimalPlaces = 1;
            this.udPSMoxDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPSMoxDelay.Location = new System.Drawing.Point(99, 61);
            this.udPSMoxDelay.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.udPSMoxDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPSMoxDelay.Name = "udPSMoxDelay";
            this.udPSMoxDelay.Size = new System.Drawing.Size(51, 20);
            this.udPSMoxDelay.TabIndex = 29;
            this.udPSMoxDelay.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.udPSMoxDelay.ValueChanged += new System.EventHandler(this.udPSMoxDelay_ValueChanged);
            // 
            // labelTS2
            // 
            this.labelTS2.AutoSize = true;
            this.labelTS2.ForeColor = System.Drawing.Color.White;
            this.labelTS2.Image = null;
            this.labelTS2.Location = new System.Drawing.Point(11, 117);
            this.labelTS2.Name = "labelTS2";
            this.labelTS2.Size = new System.Drawing.Size(80, 13);
            this.labelTS2.TabIndex = 26;
            this.labelTS2.Text = "AMP Delay (ns)";
            // 
            // udPSPhnum
            // 
            this.udPSPhnum.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udPSPhnum.Location = new System.Drawing.Point(99, 115);
            this.udPSPhnum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.udPSPhnum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPSPhnum.Name = "udPSPhnum";
            this.udPSPhnum.Size = new System.Drawing.Size(51, 20);
            this.udPSPhnum.TabIndex = 25;
            this.udPSPhnum.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.udPSPhnum.ValueChanged += new System.EventHandler(this.udPSPhnum_ValueChanged);
            // 
            // grpPSInfo
            // 
            this.grpPSInfo.Controls.Add(this.lblPSInfo5);
            this.grpPSInfo.Controls.Add(this.labelTS14);
            this.grpPSInfo.Controls.Add(this.lblPSInfo13);
            this.grpPSInfo.Controls.Add(this.labelTS12);
            this.grpPSInfo.Controls.Add(this.lblPSInfo6);
            this.grpPSInfo.Controls.Add(this.labelTS10);
            this.grpPSInfo.Controls.Add(this.PSdispRX);
            this.grpPSInfo.Controls.Add(this.labelTS6);
            this.grpPSInfo.Controls.Add(this.GetPSpeak);
            this.grpPSInfo.Controls.Add(this.labelTS3);
            this.grpPSInfo.Controls.Add(this.PSpeak);
            this.grpPSInfo.Controls.Add(this.labelTS5);
            this.grpPSInfo.Controls.Add(this.lblPSfb2);
            this.grpPSInfo.Controls.Add(this.labelTS1);
            this.grpPSInfo.Controls.Add(this.lblPSInfo15);
            this.grpPSInfo.Controls.Add(this.labelTS146);
            this.grpPSInfo.Controls.Add(this.lblPSInfo3);
            this.grpPSInfo.Controls.Add(this.lblPSInfo2);
            this.grpPSInfo.Controls.Add(this.lblPSInfo1);
            this.grpPSInfo.Controls.Add(this.lblPSInfo0);
            this.grpPSInfo.Controls.Add(this.labelTS143);
            this.grpPSInfo.Controls.Add(this.labelTS144);
            this.grpPSInfo.Controls.Add(this.labelTS142);
            this.grpPSInfo.Controls.Add(this.labelTS141);
            this.grpPSInfo.ForeColor = System.Drawing.Color.White;
            this.grpPSInfo.Location = new System.Drawing.Point(14, 145);
            this.grpPSInfo.Name = "grpPSInfo";
            this.grpPSInfo.Size = new System.Drawing.Size(356, 148);
            this.grpPSInfo.TabIndex = 21;
            this.grpPSInfo.TabStop = false;
            this.grpPSInfo.Text = "Calibration Information";
            // 
            // lblPSInfo5
            // 
            this.lblPSInfo5.AutoSize = true;
            this.lblPSInfo5.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo5.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo5.Image = null;
            this.lblPSInfo5.Location = new System.Drawing.Point(194, 72);
            this.lblPSInfo5.Name = "lblPSInfo5";
            this.lblPSInfo5.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo5.TabIndex = 23;
            // 
            // labelTS14
            // 
            this.labelTS14.AutoSize = true;
            this.labelTS14.Image = null;
            this.labelTS14.Location = new System.Drawing.Point(128, 72);
            this.labelTS14.Name = "labelTS14";
            this.labelTS14.Size = new System.Drawing.Size(40, 13);
            this.labelTS14.TabIndex = 22;
            this.labelTS14.Text = "cor.cnt";
            // 
            // lblPSInfo13
            // 
            this.lblPSInfo13.AutoSize = true;
            this.lblPSInfo13.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo13.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo13.Image = null;
            this.lblPSInfo13.Location = new System.Drawing.Point(194, 48);
            this.lblPSInfo13.Name = "lblPSInfo13";
            this.lblPSInfo13.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo13.TabIndex = 21;
            // 
            // labelTS12
            // 
            this.labelTS12.AutoSize = true;
            this.labelTS12.Image = null;
            this.labelTS12.Location = new System.Drawing.Point(128, 48);
            this.labelTS12.Name = "labelTS12";
            this.labelTS12.Size = new System.Drawing.Size(37, 13);
            this.labelTS12.TabIndex = 20;
            this.labelTS12.Text = "dg.cnt";
            // 
            // lblPSInfo6
            // 
            this.lblPSInfo6.AutoSize = true;
            this.lblPSInfo6.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo6.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo6.Image = null;
            this.lblPSInfo6.Location = new System.Drawing.Point(194, 24);
            this.lblPSInfo6.Name = "lblPSInfo6";
            this.lblPSInfo6.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo6.TabIndex = 19;
            // 
            // labelTS10
            // 
            this.labelTS10.AutoSize = true;
            this.labelTS10.Image = null;
            this.labelTS10.Location = new System.Drawing.Point(128, 24);
            this.labelTS10.Name = "labelTS10";
            this.labelTS10.Size = new System.Drawing.Size(41, 13);
            this.labelTS10.TabIndex = 18;
            this.labelTS10.Text = "sln.chk";
            // 
            // PSdispRX
            // 
            this.PSdispRX.BackColor = System.Drawing.Color.Bisque;
            this.PSdispRX.Location = new System.Drawing.Point(170, 118);
            this.PSdispRX.Name = "PSdispRX";
            this.PSdispRX.Size = new System.Drawing.Size(38, 20);
            this.PSdispRX.TabIndex = 17;
            this.PSdispRX.TextChanged += new System.EventHandler(this.PSdispRX_TextChanged);
            // 
            // labelTS6
            // 
            this.labelTS6.AutoSize = true;
            this.labelTS6.Image = null;
            this.labelTS6.Location = new System.Drawing.Point(6, 121);
            this.labelTS6.Name = "labelTS6";
            this.labelTS6.Size = new System.Drawing.Size(158, 13);
            this.labelTS6.TabIndex = 16;
            this.labelTS6.Text = "Bottom Panadapter Displays RX";
            // 
            // GetPSpeak
            // 
            this.GetPSpeak.BackColor = System.Drawing.Color.Bisque;
            this.GetPSpeak.Location = new System.Drawing.Point(287, 69);
            this.GetPSpeak.Name = "GetPSpeak";
            this.GetPSpeak.Size = new System.Drawing.Size(57, 20);
            this.GetPSpeak.TabIndex = 15;
            // 
            // labelTS3
            // 
            this.labelTS3.AutoSize = true;
            this.labelTS3.Image = null;
            this.labelTS3.Location = new System.Drawing.Point(250, 72);
            this.labelTS3.Name = "labelTS3";
            this.labelTS3.Size = new System.Drawing.Size(37, 13);
            this.labelTS3.TabIndex = 14;
            this.labelTS3.Text = "GetPk";
            // 
            // PSpeak
            // 
            this.PSpeak.BackColor = System.Drawing.Color.Bisque;
            this.PSpeak.Location = new System.Drawing.Point(287, 93);
            this.PSpeak.Name = "PSpeak";
            this.PSpeak.Size = new System.Drawing.Size(57, 20);
            this.PSpeak.TabIndex = 13;
            this.PSpeak.TextChanged += new System.EventHandler(this.PSpeak_TextChanged);
            // 
            // labelTS5
            // 
            this.labelTS5.AutoSize = true;
            this.labelTS5.Image = null;
            this.labelTS5.Location = new System.Drawing.Point(250, 96);
            this.labelTS5.Name = "labelTS5";
            this.labelTS5.Size = new System.Drawing.Size(36, 13);
            this.labelTS5.TabIndex = 12;
            this.labelTS5.Text = "SetPk";
            // 
            // lblPSfb2
            // 
            this.lblPSfb2.AutoSize = true;
            this.lblPSfb2.BackColor = System.Drawing.Color.Bisque;
            this.lblPSfb2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSfb2.ForeColor = System.Drawing.Color.Black;
            this.lblPSfb2.Image = null;
            this.lblPSfb2.Location = new System.Drawing.Point(309, 48);
            this.lblPSfb2.Name = "lblPSfb2";
            this.lblPSfb2.Size = new System.Drawing.Size(2, 15);
            this.lblPSfb2.TabIndex = 11;
            // 
            // labelTS1
            // 
            this.labelTS1.AutoSize = true;
            this.labelTS1.Image = null;
            this.labelTS1.Location = new System.Drawing.Point(250, 48);
            this.labelTS1.Name = "labelTS1";
            this.labelTS1.Size = new System.Drawing.Size(40, 13);
            this.labelTS1.TabIndex = 10;
            this.labelTS1.Text = "feedbk";
            // 
            // lblPSInfo15
            // 
            this.lblPSInfo15.AutoSize = true;
            this.lblPSInfo15.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo15.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo15.Image = null;
            this.lblPSInfo15.Location = new System.Drawing.Point(309, 24);
            this.lblPSInfo15.Name = "lblPSInfo15";
            this.lblPSInfo15.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo15.TabIndex = 9;
            // 
            // labelTS146
            // 
            this.labelTS146.AutoSize = true;
            this.labelTS146.Image = null;
            this.labelTS146.Location = new System.Drawing.Point(250, 24);
            this.labelTS146.Name = "labelTS146";
            this.labelTS146.Size = new System.Drawing.Size(30, 13);
            this.labelTS146.TabIndex = 8;
            this.labelTS146.Text = "state";
            // 
            // lblPSInfo3
            // 
            this.lblPSInfo3.AutoSize = true;
            this.lblPSInfo3.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo3.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo3.Image = null;
            this.lblPSInfo3.Location = new System.Drawing.Point(72, 96);
            this.lblPSInfo3.Name = "lblPSInfo3";
            this.lblPSInfo3.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo3.TabIndex = 7;
            // 
            // lblPSInfo2
            // 
            this.lblPSInfo2.AutoSize = true;
            this.lblPSInfo2.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo2.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo2.Image = null;
            this.lblPSInfo2.Location = new System.Drawing.Point(72, 72);
            this.lblPSInfo2.Name = "lblPSInfo2";
            this.lblPSInfo2.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo2.TabIndex = 6;
            // 
            // lblPSInfo1
            // 
            this.lblPSInfo1.AutoSize = true;
            this.lblPSInfo1.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo1.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo1.Image = null;
            this.lblPSInfo1.Location = new System.Drawing.Point(72, 48);
            this.lblPSInfo1.Name = "lblPSInfo1";
            this.lblPSInfo1.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo1.TabIndex = 5;
            // 
            // lblPSInfo0
            // 
            this.lblPSInfo0.AutoSize = true;
            this.lblPSInfo0.BackColor = System.Drawing.Color.Bisque;
            this.lblPSInfo0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPSInfo0.ForeColor = System.Drawing.Color.Black;
            this.lblPSInfo0.Image = null;
            this.lblPSInfo0.Location = new System.Drawing.Point(72, 24);
            this.lblPSInfo0.Name = "lblPSInfo0";
            this.lblPSInfo0.Size = new System.Drawing.Size(2, 15);
            this.lblPSInfo0.TabIndex = 4;
            // 
            // labelTS143
            // 
            this.labelTS143.AutoSize = true;
            this.labelTS143.Image = null;
            this.labelTS143.Location = new System.Drawing.Point(6, 96);
            this.labelTS143.Name = "labelTS143";
            this.labelTS143.Size = new System.Drawing.Size(38, 13);
            this.labelTS143.TabIndex = 3;
            this.labelTS143.Text = "bldr.cs";
            // 
            // labelTS144
            // 
            this.labelTS144.AutoSize = true;
            this.labelTS144.Image = null;
            this.labelTS144.Location = new System.Drawing.Point(6, 72);
            this.labelTS144.Name = "labelTS144";
            this.labelTS144.Size = new System.Drawing.Size(39, 13);
            this.labelTS144.TabIndex = 2;
            this.labelTS144.Text = "bldr.cc";
            // 
            // labelTS142
            // 
            this.labelTS142.AutoSize = true;
            this.labelTS142.Image = null;
            this.labelTS142.Location = new System.Drawing.Point(6, 48);
            this.labelTS142.Name = "labelTS142";
            this.labelTS142.Size = new System.Drawing.Size(41, 13);
            this.labelTS142.TabIndex = 1;
            this.labelTS142.Text = "bldr.cm";
            // 
            // labelTS141
            // 
            this.labelTS141.AutoSize = true;
            this.labelTS141.Image = null;
            this.labelTS141.Location = new System.Drawing.Point(6, 24);
            this.labelTS141.Name = "labelTS141";
            this.labelTS141.Size = new System.Drawing.Size(35, 13);
            this.labelTS141.TabIndex = 0;
            this.labelTS141.Text = "bldr.rx";
            // 
            // btnPSReset
            // 
            this.btnPSReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSReset.Image = null;
            this.btnPSReset.Location = new System.Drawing.Point(476, 12);
            this.btnPSReset.Name = "btnPSReset";
            this.btnPSReset.Size = new System.Drawing.Size(71, 20);
            this.btnPSReset.TabIndex = 20;
            this.btnPSReset.Text = "OFF";
            this.btnPSReset.UseVisualStyleBackColor = false;
            this.btnPSReset.Click += new System.EventHandler(this.btnPSReset_Click);
            // 
            // btnPSCalibrate
            // 
            this.btnPSCalibrate.BackColor = System.Drawing.SystemColors.Control;
            this.btnPSCalibrate.Image = null;
            this.btnPSCalibrate.Location = new System.Drawing.Point(91, 12);
            this.btnPSCalibrate.Name = "btnPSCalibrate";
            this.btnPSCalibrate.Size = new System.Drawing.Size(71, 20);
            this.btnPSCalibrate.TabIndex = 19;
            this.btnPSCalibrate.Text = "Single Cal";
            this.btnPSCalibrate.UseVisualStyleBackColor = false;
            this.btnPSCalibrate.Click += new System.EventHandler(this.btnPSCalibrate_Click);
            // 
            // labelTS140
            // 
            this.labelTS140.AutoSize = true;
            this.labelTS140.ForeColor = System.Drawing.Color.White;
            this.labelTS140.Image = null;
            this.labelTS140.Location = new System.Drawing.Point(11, 90);
            this.labelTS140.Name = "labelTS140";
            this.labelTS140.Size = new System.Drawing.Size(78, 13);
            this.labelTS140.TabIndex = 17;
            this.labelTS140.Text = "CAL Wait (sec)";
            // 
            // udPSCalWait
            // 
            this.udPSCalWait.DecimalPlaces = 1;
            this.udPSCalWait.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPSCalWait.Location = new System.Drawing.Point(99, 88);
            this.udPSCalWait.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udPSCalWait.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPSCalWait.Name = "udPSCalWait";
            this.udPSCalWait.Size = new System.Drawing.Size(51, 20);
            this.udPSCalWait.TabIndex = 16;
            this.udPSCalWait.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.udPSCalWait.ValueChanged += new System.EventHandler(this.udPSCalWait_ValueChanged);
            // 
            // chkPSOnTop
            // 
            this.chkPSOnTop.AutoSize = true;
            this.chkPSOnTop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chkPSOnTop.Image = null;
            this.chkPSOnTop.Location = new System.Drawing.Point(434, 266);
            this.chkPSOnTop.Name = "chkPSOnTop";
            this.chkPSOnTop.Size = new System.Drawing.Size(98, 17);
            this.chkPSOnTop.TabIndex = 49;
            this.chkPSOnTop.Text = "Always On Top";
            this.chkPSOnTop.UseVisualStyleBackColor = true;
            this.chkPSOnTop.CheckedChanged += new System.EventHandler(this.chkPSOnTop_CheckedChanged);
            // 
            // PSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(560, 303);
            this.Controls.Add(this.chkPSOnTop);
            this.Controls.Add(this.btnPSRestore);
            this.Controls.Add(this.btnPSSave);
            this.Controls.Add(this.comboPSTint);
            this.Controls.Add(this.lblPSTint);
            this.Controls.Add(this.chkPSStbl);
            this.Controls.Add(this.chkPSMap);
            this.Controls.Add(this.chkPSPin);
            this.Controls.Add(this.btnPSAdvanced);
            this.Controls.Add(this.chkPSAutoAttenuate);
            this.Controls.Add(this.btnPSAmpView);
            this.Controls.Add(this.chkPSRelaxPtol);
            this.Controls.Add(this.lblDisabled);
            this.Controls.Add(this.btnPSTwoToneGen);
            this.Controls.Add(this.labelTS4);
            this.Controls.Add(this.udPSMoxDelay);
            this.Controls.Add(this.labelTS2);
            this.Controls.Add(this.udPSPhnum);
            this.Controls.Add(this.grpPSInfo);
            this.Controls.Add(this.btnPSReset);
            this.Controls.Add(this.btnPSCalibrate);
            this.Controls.Add(this.labelTS140);
            this.Controls.Add(this.udPSCalWait);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PSForm";
            this.Text = "PureSignal 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PSForm_Closing);
            this.Load += new System.EventHandler(this.PSForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udPSMoxDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPSPhnum)).EndInit();
            this.grpPSInfo.ResumeLayout(false);
            this.grpPSInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPSCalWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBoxTS grpPSInfo;
        private System.Windows.Forms.LabelTS lblPSInfo15;
        private System.Windows.Forms.LabelTS labelTS146;
        private System.Windows.Forms.LabelTS lblPSInfo3;
        private System.Windows.Forms.LabelTS lblPSInfo2;
        private System.Windows.Forms.LabelTS lblPSInfo1;
        private System.Windows.Forms.LabelTS lblPSInfo0;
        private System.Windows.Forms.LabelTS labelTS143;
        private System.Windows.Forms.LabelTS labelTS144;
        private System.Windows.Forms.LabelTS labelTS142;
        private System.Windows.Forms.LabelTS labelTS141;
        private System.Windows.Forms.ButtonTS btnPSReset;
        private System.Windows.Forms.ButtonTS btnPSCalibrate;
        private System.Windows.Forms.LabelTS labelTS140;
        private System.Windows.Forms.NumericUpDownTS udPSCalWait;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LabelTS labelTS2;
        private System.Windows.Forms.NumericUpDownTS udPSPhnum;
        private System.Windows.Forms.LabelTS labelTS4;
        private System.Windows.Forms.NumericUpDownTS udPSMoxDelay;
        private System.Windows.Forms.ButtonTS btnPSSave;
        private System.Windows.Forms.ButtonTS btnPSRestore;
        private System.Windows.Forms.ButtonTS btnPSTwoToneGen;
        private System.Windows.Forms.LabelTS lblPSfb2;
        private System.Windows.Forms.LabelTS labelTS1;
        private System.Windows.Forms.LabelTS lblDisabled;
        private System.Windows.Forms.LabelTS labelTS5;
        private System.Windows.Forms.TextBoxTS PSpeak;
        private System.Windows.Forms.TextBoxTS GetPSpeak;
        private System.Windows.Forms.LabelTS labelTS3;
        private System.Windows.Forms.TextBoxTS PSdispRX;
        private System.Windows.Forms.LabelTS labelTS6;
        private System.Windows.Forms.CheckBoxTS chkPSRelaxPtol;
        private System.Windows.Forms.ButtonTS btnPSAmpView;
        private System.Windows.Forms.CheckBoxTS chkPSAutoAttenuate;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.LabelTS lblPSInfo5;
        private System.Windows.Forms.LabelTS labelTS14;
        private System.Windows.Forms.LabelTS lblPSInfo13;
        private System.Windows.Forms.LabelTS labelTS12;
        private System.Windows.Forms.LabelTS lblPSInfo6;
        private System.Windows.Forms.LabelTS labelTS10;
        private System.Windows.Forms.ButtonTS btnPSAdvanced;
        private System.Windows.Forms.CheckBoxTS chkPSPin;
        private System.Windows.Forms.CheckBoxTS chkPSMap;
        private System.Windows.Forms.CheckBoxTS chkPSStbl;
        private System.Windows.Forms.LabelTS lblPSTint;
        private System.Windows.Forms.ComboBoxTS comboPSTint;
        private System.Windows.Forms.CheckBoxTS chkPSOnTop;
    }
}
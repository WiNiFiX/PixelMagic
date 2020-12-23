namespace ShadowMagic.GUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblHotkeyInfo = new System.Windows.Forms.Label();
            this.txtTargetCasting = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spellbookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadAddonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToByteArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingPixelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseAgreementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nudPulse = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.transparentLabel6 = new System.Windows.Forms.Label();
            this.transparentLabel5 = new System.Windows.Forms.Label();
            this.txtPlayerHealth = new System.Windows.Forms.TextBox();
            this.transparentLabel1 = new System.Windows.Forms.Label();
            this.txtPlayerPower = new System.Windows.Forms.TextBox();
            this.transparentLabel2 = new System.Windows.Forms.Label();
            this.txtTargetHealth = new System.Windows.Forms.TextBox();
            this.transparentLabel3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdRotationSettings = new System.Windows.Forms.Button();
            this.cmdStartBot = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulse)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.BackColor = System.Drawing.Color.White;
            this.rtbLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rtbLog.Location = new System.Drawing.Point(0, 29);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbLog.Size = new System.Drawing.Size(786, 503);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // lblHotkeyInfo
            // 
            this.lblHotkeyInfo.AutoSize = true;
            this.lblHotkeyInfo.Location = new System.Drawing.Point(476, 312);
            this.lblHotkeyInfo.Name = "lblHotkeyInfo";
            this.lblHotkeyInfo.Size = new System.Drawing.Size(68, 13);
            this.lblHotkeyInfo.TabIndex = 3;
            this.lblHotkeyInfo.Text = "[Hotkey Info]";
            // 
            // txtTargetCasting
            // 
            this.txtTargetCasting.ForeColor = System.Drawing.Color.White;
            this.txtTargetCasting.Location = new System.Drawing.Point(895, 188);
            this.txtTargetCasting.Name = "txtTargetCasting";
            this.txtTargetCasting.ReadOnly = true;
            this.txtTargetCasting.Size = new System.Drawing.Size(45, 20);
            this.txtTargetCasting.TabIndex = 11;
            this.txtTargetCasting.TextChanged += new System.EventHandler(this.txtTargetCasting_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.licenseAgreementToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(951, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRotationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadRotationToolStripMenuItem
            // 
            this.loadRotationToolStripMenuItem.Name = "loadRotationToolStripMenuItem";
            this.loadRotationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.loadRotationToolStripMenuItem.Text = "Load Rotation...";
            this.loadRotationToolStripMenuItem.Click += new System.EventHandler(this.loadRotationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hotkeysToolStripMenuItem,
            this.spellbookToolStripMenuItem,
            this.reloadAddonToolStripMenuItem});
            this.setupToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.setupToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("setupToolStripMenuItem.Image")));
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // hotkeysToolStripMenuItem
            // 
            this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
            this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.hotkeysToolStripMenuItem.Text = "Hotkeys";
            this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
            // 
            // spellbookToolStripMenuItem
            // 
            this.spellbookToolStripMenuItem.Enabled = false;
            this.spellbookToolStripMenuItem.Name = "spellbookToolStripMenuItem";
            this.spellbookToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.spellbookToolStripMenuItem.Text = "Keybinds to Spells";
            this.spellbookToolStripMenuItem.Click += new System.EventHandler(this.spellbookToolStripMenuItem_Click);
            // 
            // reloadAddonToolStripMenuItem
            // 
            this.reloadAddonToolStripMenuItem.Name = "reloadAddonToolStripMenuItem";
            this.reloadAddonToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.reloadAddonToolStripMenuItem.Text = "Reload Addon";
            this.reloadAddonToolStripMenuItem.Click += new System.EventHandler(this.reloadAddonToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToByteArrayToolStripMenuItem,
            this.testingPixelsToolStripMenuItem,
            this.encryptCRToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.toolsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripMenuItem.Image")));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // imageToByteArrayToolStripMenuItem
            // 
            this.imageToByteArrayToolStripMenuItem.Name = "imageToByteArrayToolStripMenuItem";
            this.imageToByteArrayToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.imageToByteArrayToolStripMenuItem.Text = "Image to byte array";
            this.imageToByteArrayToolStripMenuItem.Click += new System.EventHandler(this.imageToByteArrayToolStripMenuItem_Click);
            // 
            // testingPixelsToolStripMenuItem
            // 
            this.testingPixelsToolStripMenuItem.Name = "testingPixelsToolStripMenuItem";
            this.testingPixelsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.testingPixelsToolStripMenuItem.Text = "Testing Pixels";
            this.testingPixelsToolStripMenuItem.Click += new System.EventHandler(this.testingPixelsToolStripMenuItem_Click);
            // 
            // encryptCRToolStripMenuItem
            // 
            this.encryptCRToolStripMenuItem.Name = "encryptCRToolStripMenuItem";
            this.encryptCRToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.encryptCRToolStripMenuItem.Text = "Encrypt CR";
            this.encryptCRToolStripMenuItem.Click += new System.EventHandler(this.encryptCRToolStripMenuItem_Click);
            // 
            // licenseAgreementToolStripMenuItem
            // 
            this.licenseAgreementToolStripMenuItem.Name = "licenseAgreementToolStripMenuItem";
            this.licenseAgreementToolStripMenuItem.Size = new System.Drawing.Size(12, 4);
            // 
            // nudPulse
            // 
            this.nudPulse.ForeColor = System.Drawing.Color.Black;
            this.nudPulse.Location = new System.Drawing.Point(879, 500);
            this.nudPulse.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPulse.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPulse.Name = "nudPulse";
            this.nudPulse.Size = new System.Drawing.Size(61, 20);
            this.nudPulse.TabIndex = 21;
            this.nudPulse.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPulse.ValueChanged += new System.EventHandler(this.nudPulse_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel6});
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(951, 24);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.MintCream;
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(213, 19);
            this.toolStripStatusLabel2.Text = "ShadowMagic (developed by WiNiFiX)";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(69, 19);
            this.toolStripStatusLabel3.Text = "Version: {0}";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(85, 19);
            this.toolStripStatusLabel1.Text = "Build Date: {0}";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.AutoSize = false;
            this.toolStripStatusLabel5.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel5.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel5.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(205, 19);
            this.toolStripStatusLabel5.Spring = true;
            this.toolStripStatusLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(227, 19);
            this.toolStripStatusLabel4.Text = "Hoykey: Ctrl + F5 = Reload Rotation && UI";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel6.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel6.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(154, 19);
            this.toolStripStatusLabel6.Text = "Ctrl + F6 = Reload Rotation";
            // 
            // transparentLabel6
            // 
            this.transparentLabel6.AutoSize = true;
            this.transparentLabel6.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel6.Location = new System.Drawing.Point(798, 503);
            this.transparentLabel6.Name = "transparentLabel6";
            this.transparentLabel6.Size = new System.Drawing.Size(55, 13);
            this.transparentLabel6.TabIndex = 35;
            this.transparentLabel6.Text = "Pulse (ms)";
            // 
            // transparentLabel5
            // 
            this.transparentLabel5.AutoSize = true;
            this.transparentLabel5.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel5.Location = new System.Drawing.Point(799, 191);
            this.transparentLabel5.Name = "transparentLabel5";
            this.transparentLabel5.Size = new System.Drawing.Size(76, 13);
            this.transparentLabel5.TabIndex = 34;
            this.transparentLabel5.Text = "Target Casting";
            // 
            // txtPlayerHealth
            // 
            this.txtPlayerHealth.ForeColor = System.Drawing.Color.White;
            this.txtPlayerHealth.Location = new System.Drawing.Point(895, 114);
            this.txtPlayerHealth.Name = "txtPlayerHealth";
            this.txtPlayerHealth.ReadOnly = true;
            this.txtPlayerHealth.Size = new System.Drawing.Size(45, 20);
            this.txtPlayerHealth.TabIndex = 46;
            // 
            // transparentLabel1
            // 
            this.transparentLabel1.AutoSize = true;
            this.transparentLabel1.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel1.Location = new System.Drawing.Point(799, 117);
            this.transparentLabel1.Name = "transparentLabel1";
            this.transparentLabel1.Size = new System.Drawing.Size(70, 13);
            this.transparentLabel1.TabIndex = 47;
            this.transparentLabel1.Text = "Player Health";
            // 
            // txtPlayerPower
            // 
            this.txtPlayerPower.ForeColor = System.Drawing.Color.White;
            this.txtPlayerPower.Location = new System.Drawing.Point(895, 139);
            this.txtPlayerPower.Name = "txtPlayerPower";
            this.txtPlayerPower.ReadOnly = true;
            this.txtPlayerPower.Size = new System.Drawing.Size(45, 20);
            this.txtPlayerPower.TabIndex = 48;
            // 
            // transparentLabel2
            // 
            this.transparentLabel2.AutoSize = true;
            this.transparentLabel2.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel2.Location = new System.Drawing.Point(799, 142);
            this.transparentLabel2.Name = "transparentLabel2";
            this.transparentLabel2.Size = new System.Drawing.Size(54, 13);
            this.transparentLabel2.TabIndex = 49;
            this.transparentLabel2.Text = "My Power";
            // 
            // txtTargetHealth
            // 
            this.txtTargetHealth.ForeColor = System.Drawing.Color.White;
            this.txtTargetHealth.Location = new System.Drawing.Point(895, 163);
            this.txtTargetHealth.Name = "txtTargetHealth";
            this.txtTargetHealth.ReadOnly = true;
            this.txtTargetHealth.Size = new System.Drawing.Size(45, 20);
            this.txtTargetHealth.TabIndex = 50;
            // 
            // transparentLabel3
            // 
            this.transparentLabel3.AutoSize = true;
            this.transparentLabel3.ForeColor = System.Drawing.Color.Black;
            this.transparentLabel3.Location = new System.Drawing.Point(799, 166);
            this.transparentLabel3.Name = "transparentLabel3";
            this.transparentLabel3.Size = new System.Drawing.Size(72, 13);
            this.transparentLabel3.TabIndex = 51;
            this.transparentLabel3.Text = "Target Health";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(799, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Enemies in Range";
            // 
            // txtRange
            // 
            this.txtRange.ForeColor = System.Drawing.Color.White;
            this.txtRange.Location = new System.Drawing.Point(895, 214);
            this.txtRange.Name = "txtRange";
            this.txtRange.ReadOnly = true;
            this.txtRange.Size = new System.Drawing.Size(45, 20);
            this.txtRange.TabIndex = 52;
            // 
            // cmdClose
            // 
            this.cmdClose.ForeColor = System.Drawing.Color.Black;
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.Location = new System.Drawing.Point(950, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(21, 21);
            this.cmdClose.TabIndex = 45;
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdRotationSettings
            // 
            this.cmdRotationSettings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdRotationSettings.Enabled = false;
            this.cmdRotationSettings.ForeColor = System.Drawing.Color.Black;
            this.cmdRotationSettings.Image = ((System.Drawing.Image)(resources.GetObject("cmdRotationSettings.Image")));
            this.cmdRotationSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRotationSettings.Location = new System.Drawing.Point(799, 72);
            this.cmdRotationSettings.Name = "cmdRotationSettings";
            this.cmdRotationSettings.Size = new System.Drawing.Size(142, 36);
            this.cmdRotationSettings.TabIndex = 26;
            this.cmdRotationSettings.Text = "Rotation settings...";
            this.cmdRotationSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRotationSettings.UseVisualStyleBackColor = false;
            this.cmdRotationSettings.Click += new System.EventHandler(this.cmdRotationSettings_Click);
            // 
            // cmdStartBot
            // 
            this.cmdStartBot.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdStartBot.Enabled = false;
            this.cmdStartBot.ForeColor = System.Drawing.Color.Black;
            this.cmdStartBot.Image = ((System.Drawing.Image)(resources.GetObject("cmdStartBot.Image")));
            this.cmdStartBot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdStartBot.Location = new System.Drawing.Point(799, 31);
            this.cmdStartBot.Name = "cmdStartBot";
            this.cmdStartBot.Size = new System.Drawing.Size(142, 36);
            this.cmdStartBot.TabIndex = 1;
            this.cmdStartBot.Text = "Start Rotation ...     ";
            this.cmdStartBot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdStartBot.UseVisualStyleBackColor = false;
            this.cmdStartBot.Click += new System.EventHandler(this.cmdStartBot_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(951, 557);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRange);
            this.Controls.Add(this.txtTargetHealth);
            this.Controls.Add(this.transparentLabel3);
            this.Controls.Add(this.txtPlayerPower);
            this.Controls.Add(this.transparentLabel2);
            this.Controls.Add(this.txtPlayerHealth);
            this.Controls.Add(this.transparentLabel1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txtTargetCasting);
            this.Controls.Add(this.nudPulse);
            this.Controls.Add(this.transparentLabel6);
            this.Controls.Add(this.cmdRotationSettings);
            this.Controls.Add(this.transparentLabel5);
            this.Controls.Add(this.cmdStartBot);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.lblHotkeyInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(971, 600);
            this.MinimumSize = new System.Drawing.Size(971, 600);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShadowMagic .. where the magic happens";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulse)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblHotkeyInfo;
        public System.Windows.Forms.TextBox txtTargetCasting;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotkeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spellbookToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown nudPulse;
        private System.Windows.Forms.ToolStripMenuItem reloadAddonToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        internal System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Button cmdRotationSettings;
        private System.Windows.Forms.Label transparentLabel5;
        private System.Windows.Forms.Label transparentLabel6;
        private System.Windows.Forms.ToolStripMenuItem imageToByteArrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ToolStripMenuItem licenseAgreementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingPixelsToolStripMenuItem;
        public System.Windows.Forms.Button cmdStartBot;
        private System.Windows.Forms.ToolStripMenuItem encryptCRToolStripMenuItem;
        public System.Windows.Forms.TextBox txtPlayerHealth;
        private System.Windows.Forms.Label transparentLabel1;
        public System.Windows.Forms.TextBox txtPlayerPower;
        private System.Windows.Forms.Label transparentLabel2;
        public System.Windows.Forms.TextBox txtTargetHealth;
        private System.Windows.Forms.Label transparentLabel3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtRange;
    }
}


﻿namespace ShadowMagic.GUI
{
    partial class SetupHotkeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupHotkeys));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStartRotationModifierKey = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStartRotationKey = new System.Windows.Forms.ComboBox();
            this.cmbSingleTargetKey = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSingleTargetModifierKey = new System.Windows.Forms.ComboBox();
            this.cmbAOEKey = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbAOEModifierKey = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdDefaults = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start/Stop Rotation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Single Target";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Single Target Cleave / AOE";
            // 
            // cmbStartRotationModifierKey
            // 
            this.cmbStartRotationModifierKey.FormattingEnabled = true;
            this.cmbStartRotationModifierKey.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift",
            "None"});
            this.cmbStartRotationModifierKey.Location = new System.Drawing.Point(157, 28);
            this.cmbStartRotationModifierKey.Name = "cmbStartRotationModifierKey";
            this.cmbStartRotationModifierKey.Size = new System.Drawing.Size(85, 21);
            this.cmbStartRotationModifierKey.TabIndex = 4;
            this.cmbStartRotationModifierKey.Text = "Ctrl";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "+";
            // 
            // cmbStartRotationKey
            // 
            this.cmbStartRotationKey.FormattingEnabled = true;
            this.cmbStartRotationKey.Items.AddRange(new object[] {
            "None",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0"});
            this.cmbStartRotationKey.Location = new System.Drawing.Point(267, 28);
            this.cmbStartRotationKey.Name = "cmbStartRotationKey";
            this.cmbStartRotationKey.Size = new System.Drawing.Size(85, 21);
            this.cmbStartRotationKey.TabIndex = 6;
            this.cmbStartRotationKey.Text = "S";
            // 
            // cmbSingleTargetKey
            // 
            this.cmbSingleTargetKey.FormattingEnabled = true;
            this.cmbSingleTargetKey.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0"});
            this.cmbSingleTargetKey.Location = new System.Drawing.Point(267, 27);
            this.cmbSingleTargetKey.Name = "cmbSingleTargetKey";
            this.cmbSingleTargetKey.Size = new System.Drawing.Size(85, 21);
            this.cmbSingleTargetKey.TabIndex = 12;
            this.cmbSingleTargetKey.Text = "S";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "+";
            // 
            // cmbSingleTargetModifierKey
            // 
            this.cmbSingleTargetModifierKey.FormattingEnabled = true;
            this.cmbSingleTargetModifierKey.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift",
            "None"});
            this.cmbSingleTargetModifierKey.Location = new System.Drawing.Point(157, 27);
            this.cmbSingleTargetModifierKey.Name = "cmbSingleTargetModifierKey";
            this.cmbSingleTargetModifierKey.Size = new System.Drawing.Size(85, 21);
            this.cmbSingleTargetModifierKey.TabIndex = 10;
            this.cmbSingleTargetModifierKey.Text = "Alt";
            // 
            // cmbAOEKey
            // 
            this.cmbAOEKey.FormattingEnabled = true;
            this.cmbAOEKey.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0"});
            this.cmbAOEKey.Location = new System.Drawing.Point(267, 54);
            this.cmbAOEKey.Name = "cmbAOEKey";
            this.cmbAOEKey.Size = new System.Drawing.Size(85, 21);
            this.cmbAOEKey.TabIndex = 15;
            this.cmbAOEKey.Text = "A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(248, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "+";
            // 
            // cmbAOEModifierKey
            // 
            this.cmbAOEModifierKey.FormattingEnabled = true;
            this.cmbAOEModifierKey.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift",
            "None"});
            this.cmbAOEModifierKey.Location = new System.Drawing.Point(157, 54);
            this.cmbAOEModifierKey.Name = "cmbAOEModifierKey";
            this.cmbAOEModifierKey.Size = new System.Drawing.Size(85, 21);
            this.cmbAOEModifierKey.TabIndex = 13;
            this.cmbAOEModifierKey.Text = "Alt";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStartRotationModifierKey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbStartRotationKey);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 100);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotation Hotkeys";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "+";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbAOEKey);
            this.groupBox2.Controls.Add(this.cmbSingleTargetModifierKey);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbAOEModifierKey);
            this.groupBox2.Controls.Add(this.cmbSingleTargetKey);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 100);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rotation Type";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(351, 229);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 18;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDefaults
            // 
            this.cmdDefaults.Location = new System.Drawing.Point(270, 229);
            this.cmdDefaults.Name = "cmdDefaults";
            this.cmdDefaults.Size = new System.Drawing.Size(75, 23);
            this.cmdDefaults.TabIndex = 19;
            this.cmdDefaults.Text = "Defaults";
            this.cmdDefaults.UseVisualStyleBackColor = true;
            this.cmdDefaults.Click += new System.EventHandler(this.cmdDefaults_Click);
            // 
            // SetupHotkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 262);
            this.Controls.Add(this.cmdDefaults);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupHotkeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Hotkeys";
            this.Load += new System.EventHandler(this.SetupHotkeys_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStartRotationModifierKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbStartRotationKey;
        private System.Windows.Forms.ComboBox cmbSingleTargetKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSingleTargetModifierKey;
        private System.Windows.Forms.ComboBox cmbAOEKey;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbAOEModifierKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdDefaults;
        private System.Windows.Forms.Label label6;
    }
}
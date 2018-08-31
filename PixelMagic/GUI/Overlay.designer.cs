namespace PixelMagic.GUI
{
    partial class Overlay
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
            this.cmdRotationSettings = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Cooldowns = new System.Windows.Forms.Button();
            this.RotationMode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdRotationSettings
            // 
            this.cmdRotationSettings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmdRotationSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdRotationSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.cmdRotationSettings.ForeColor = System.Drawing.Color.DarkGreen;
            this.cmdRotationSettings.Location = new System.Drawing.Point(12, 135);
            this.cmdRotationSettings.Name = "cmdRotationSettings";
            this.cmdRotationSettings.Size = new System.Drawing.Size(181, 35);
            this.cmdRotationSettings.TabIndex = 3;
            this.cmdRotationSettings.Text = "Settings...";
            this.cmdRotationSettings.UseVisualStyleBackColor = false;
            this.cmdRotationSettings.Click += new System.EventHandler(this.cmdRotationSettings_Click);
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.Red;
            this.Status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.Status.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Status.Location = new System.Drawing.Point(133, 12);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(60, 35);
            this.Status.TabIndex = 4;
            this.Status.Text = "OFF";
            this.Status.UseVisualStyleBackColor = false;
            this.Status.Click += new System.EventHandler(this.Status_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Rotation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cooldowns";
            // 
            // Cooldowns
            // 
            this.Cooldowns.BackColor = System.Drawing.Color.Red;
            this.Cooldowns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cooldowns.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.Cooldowns.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Cooldowns.Location = new System.Drawing.Point(133, 53);
            this.Cooldowns.Name = "Cooldowns";
            this.Cooldowns.Size = new System.Drawing.Size(60, 35);
            this.Cooldowns.TabIndex = 6;
            this.Cooldowns.Text = "OFF";
            this.Cooldowns.UseVisualStyleBackColor = false;
            this.Cooldowns.Click += new System.EventHandler(this.Cooldowns_Click);
            // 
            // RotationMode
            // 
            this.RotationMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.RotationMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RotationMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.RotationMode.ForeColor = System.Drawing.Color.DarkGreen;
            this.RotationMode.Location = new System.Drawing.Point(12, 94);
            this.RotationMode.Name = "RotationMode";
            this.RotationMode.Size = new System.Drawing.Size(181, 35);
            this.RotationMode.TabIndex = 8;
            this.RotationMode.Text = "Single-Target";
            this.RotationMode.UseVisualStyleBackColor = false;
            this.RotationMode.Click += new System.EventHandler(this.RotationMode_Click);
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(210, 182);
            this.Controls.Add(this.RotationMode);
            this.Controls.Add(this.Cooldowns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.cmdRotationSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Overlay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Overlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.Overlay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdRotationSettings;
        private System.Windows.Forms.Button Status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cooldowns;
        private System.Windows.Forms.Button RotationMode;
    }
}
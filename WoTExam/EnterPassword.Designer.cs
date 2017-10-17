namespace WoTExam
{
    partial class EnterPassword
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
            this.EnterClanEditHeader = new System.Windows.Forms.Label();
            this.ClanEditPassword = new System.Windows.Forms.TextBox();
            this.SubmitPassword = new System.Windows.Forms.Button();
            this.OverrideDatabaseLock = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // EnterClanEditHeader
            // 
            this.EnterClanEditHeader.AutoSize = true;
            this.EnterClanEditHeader.Location = new System.Drawing.Point(12, 9);
            this.EnterClanEditHeader.Name = "EnterClanEditHeader";
            this.EnterClanEditHeader.Size = new System.Drawing.Size(123, 13);
            this.EnterClanEditHeader.TabIndex = 0;
            this.EnterClanEditHeader.Text = "Enter clan edit password";
            // 
            // ClanEditPassword
            // 
            this.ClanEditPassword.Location = new System.Drawing.Point(15, 25);
            this.ClanEditPassword.Name = "ClanEditPassword";
            this.ClanEditPassword.PasswordChar = '*';
            this.ClanEditPassword.Size = new System.Drawing.Size(181, 20);
            this.ClanEditPassword.TabIndex = 1;
            this.ClanEditPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // SubmitPassword
            // 
            this.SubmitPassword.Location = new System.Drawing.Point(15, 51);
            this.SubmitPassword.Name = "SubmitPassword";
            this.SubmitPassword.Size = new System.Drawing.Size(181, 23);
            this.SubmitPassword.TabIndex = 2;
            this.SubmitPassword.Text = "enter";
            this.SubmitPassword.UseVisualStyleBackColor = true;
            this.SubmitPassword.Click += new System.EventHandler(this.SubmitPassword_Click);
            // 
            // OverrideDatabaseLock
            // 
            this.OverrideDatabaseLock.AutoSize = true;
            this.OverrideDatabaseLock.Location = new System.Drawing.Point(15, 80);
            this.OverrideDatabaseLock.Name = "OverrideDatabaseLock";
            this.OverrideDatabaseLock.Size = new System.Drawing.Size(108, 17);
            this.OverrideDatabaseLock.TabIndex = 4;
            this.OverrideDatabaseLock.Text = "dev override lock";
            this.OverrideDatabaseLock.UseVisualStyleBackColor = true;
            // 
            // EnterPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 109);
            this.Controls.Add(this.OverrideDatabaseLock);
            this.Controls.Add(this.SubmitPassword);
            this.Controls.Add(this.ClanEditPassword);
            this.Controls.Add(this.EnterClanEditHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EnterPassword";
            this.Text = "EnterPassword";
            this.Load += new System.EventHandler(this.EnterPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EnterClanEditHeader;
        private System.Windows.Forms.Button SubmitPassword;
        public System.Windows.Forms.CheckBox OverrideDatabaseLock;
        public System.Windows.Forms.TextBox ClanEditPassword;
    }
}
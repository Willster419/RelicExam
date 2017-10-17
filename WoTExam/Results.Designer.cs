namespace WoTExam
{
    partial class Results
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
            this.percentCorrectLabel = new System.Windows.Forms.Label();
            this.verificationCodeLabel = new System.Windows.Forms.Label();
            this.closeBurron = new System.Windows.Forms.Button();
            this.vCodeLabel = new System.Windows.Forms.TextBox();
            this.ResultsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // percentCorrectLabel
            // 
            this.percentCorrectLabel.AutoSize = true;
            this.percentCorrectLabel.Location = new System.Drawing.Point(12, 32);
            this.percentCorrectLabel.Name = "percentCorrectLabel";
            this.percentCorrectLabel.Size = new System.Drawing.Size(43, 13);
            this.percentCorrectLabel.TabIndex = 4;
            this.percentCorrectLabel.Text = "percent";
            // 
            // verificationCodeLabel
            // 
            this.verificationCodeLabel.AutoSize = true;
            this.verificationCodeLabel.Location = new System.Drawing.Point(12, 54);
            this.verificationCodeLabel.Name = "verificationCodeLabel";
            this.verificationCodeLabel.Size = new System.Drawing.Size(118, 13);
            this.verificationCodeLabel.TabIndex = 6;
            this.verificationCodeLabel.Text = "your verification code is";
            // 
            // closeBurron
            // 
            this.closeBurron.Location = new System.Drawing.Point(83, 110);
            this.closeBurron.Name = "closeBurron";
            this.closeBurron.Size = new System.Drawing.Size(75, 23);
            this.closeBurron.TabIndex = 8;
            this.closeBurron.Text = "dismiss";
            this.closeBurron.UseVisualStyleBackColor = true;
            this.closeBurron.Click += new System.EventHandler(this.button1_Click);
            // 
            // vCodeLabel
            // 
            this.vCodeLabel.Location = new System.Drawing.Point(136, 51);
            this.vCodeLabel.Name = "vCodeLabel";
            this.vCodeLabel.ReadOnly = true;
            this.vCodeLabel.Size = new System.Drawing.Size(106, 20);
            this.vCodeLabel.TabIndex = 9;
            // 
            // ResultsLabel
            // 
            this.ResultsLabel.AutoSize = true;
            this.ResultsLabel.Location = new System.Drawing.Point(12, 9);
            this.ResultsLabel.Name = "ResultsLabel";
            this.ResultsLabel.Size = new System.Drawing.Size(68, 13);
            this.ResultsLabel.TabIndex = 10;
            this.ResultsLabel.Text = "ResultsLabel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Please save the above code";
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 145);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResultsLabel);
            this.Controls.Add(this.vCodeLabel);
            this.Controls.Add(this.closeBurron);
            this.Controls.Add(this.verificationCodeLabel);
            this.Controls.Add(this.percentCorrectLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Results";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.Results_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label percentCorrectLabel;
        private System.Windows.Forms.Label verificationCodeLabel;
        private System.Windows.Forms.Button closeBurron;
        private System.Windows.Forms.TextBox vCodeLabel;
        private System.Windows.Forms.Label ResultsLabel;
        private System.Windows.Forms.Label label1;
    }
}
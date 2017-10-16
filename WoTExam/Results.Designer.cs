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
            this.scoreLabel1 = new System.Windows.Forms.Label();
            this.totalQuestionsLabel = new System.Windows.Forms.Label();
            this.scoreLabel2 = new System.Windows.Forms.Label();
            this.numberCorrectLabel = new System.Windows.Forms.Label();
            this.percentCorrectLabel = new System.Windows.Forms.Label();
            this.percentLabel = new System.Windows.Forms.Label();
            this.verificationCodeLabel = new System.Windows.Forms.Label();
            this.closeBurron = new System.Windows.Forms.Button();
            this.vCodeLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // scoreLabel1
            // 
            this.scoreLabel1.AutoSize = true;
            this.scoreLabel1.Location = new System.Drawing.Point(12, 19);
            this.scoreLabel1.Name = "scoreLabel1";
            this.scoreLabel1.Size = new System.Drawing.Size(70, 13);
            this.scoreLabel1.TabIndex = 0;
            this.scoreLabel1.Text = "You scored a";
            // 
            // totalQuestionsLabel
            // 
            this.totalQuestionsLabel.AutoSize = true;
            this.totalQuestionsLabel.Location = new System.Drawing.Point(224, 19);
            this.totalQuestionsLabel.Name = "totalQuestionsLabel";
            this.totalQuestionsLabel.Size = new System.Drawing.Size(27, 13);
            this.totalQuestionsLabel.TabIndex = 1;
            this.totalQuestionsLabel.Text = "total";
            // 
            // scoreLabel2
            // 
            this.scoreLabel2.AutoSize = true;
            this.scoreLabel2.Location = new System.Drawing.Point(134, 19);
            this.scoreLabel2.Name = "scoreLabel2";
            this.scoreLabel2.Size = new System.Drawing.Size(84, 13);
            this.scoreLabel2.TabIndex = 2;
            this.scoreLabel2.Text = "out of a possible";
            // 
            // numberCorrectLabel
            // 
            this.numberCorrectLabel.AutoSize = true;
            this.numberCorrectLabel.Location = new System.Drawing.Point(88, 19);
            this.numberCorrectLabel.Name = "numberCorrectLabel";
            this.numberCorrectLabel.Size = new System.Drawing.Size(40, 13);
            this.numberCorrectLabel.TabIndex = 3;
            this.numberCorrectLabel.Text = "correct";
            // 
            // percentCorrectLabel
            // 
            this.percentCorrectLabel.AutoSize = true;
            this.percentCorrectLabel.Location = new System.Drawing.Point(85, 52);
            this.percentCorrectLabel.Name = "percentCorrectLabel";
            this.percentCorrectLabel.Size = new System.Drawing.Size(43, 13);
            this.percentCorrectLabel.TabIndex = 4;
            this.percentCorrectLabel.Text = "percent";
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Location = new System.Drawing.Point(159, 52);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(15, 13);
            this.percentLabel.TabIndex = 5;
            this.percentLabel.Text = "%";
            // 
            // verificationCodeLabel
            // 
            this.verificationCodeLabel.AutoSize = true;
            this.verificationCodeLabel.Location = new System.Drawing.Point(22, 82);
            this.verificationCodeLabel.Name = "verificationCodeLabel";
            this.verificationCodeLabel.Size = new System.Drawing.Size(118, 13);
            this.verificationCodeLabel.TabIndex = 6;
            this.verificationCodeLabel.Text = "your verification code is";
            // 
            // closeBurron
            // 
            this.closeBurron.Location = new System.Drawing.Point(88, 104);
            this.closeBurron.Name = "closeBurron";
            this.closeBurron.Size = new System.Drawing.Size(75, 23);
            this.closeBurron.TabIndex = 8;
            this.closeBurron.Text = "dismiss";
            this.closeBurron.UseVisualStyleBackColor = true;
            this.closeBurron.Click += new System.EventHandler(this.button1_Click);
            // 
            // vCodeLabel
            // 
            this.vCodeLabel.Location = new System.Drawing.Point(146, 79);
            this.vCodeLabel.Name = "vCodeLabel";
            this.vCodeLabel.ReadOnly = true;
            this.vCodeLabel.Size = new System.Drawing.Size(100, 20);
            this.vCodeLabel.TabIndex = 9;
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 139);
            this.Controls.Add(this.vCodeLabel);
            this.Controls.Add(this.closeBurron);
            this.Controls.Add(this.verificationCodeLabel);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.percentCorrectLabel);
            this.Controls.Add(this.numberCorrectLabel);
            this.Controls.Add(this.scoreLabel2);
            this.Controls.Add(this.totalQuestionsLabel);
            this.Controls.Add(this.scoreLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Results";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.Results_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scoreLabel1;
        private System.Windows.Forms.Label totalQuestionsLabel;
        private System.Windows.Forms.Label scoreLabel2;
        private System.Windows.Forms.Label numberCorrectLabel;
        private System.Windows.Forms.Label percentCorrectLabel;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.Label verificationCodeLabel;
        private System.Windows.Forms.Button closeBurron;
        private System.Windows.Forms.TextBox vCodeLabel;
    }
}
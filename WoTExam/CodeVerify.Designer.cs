namespace WoTExam
{
    partial class CodeVerify
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
            this.codeLabel = new System.Windows.Forms.Label();
            this.expectedLabel = new System.Windows.Forms.Label();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.APCLabel = new System.Windows.Forms.Label();
            this.expectedOutputTextBox = new System.Windows.Forms.TextBox();
            this.percentCorrectLabel = new System.Windows.Forms.Label();
            this.verifyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(26, 18);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(32, 13);
            this.codeLabel.TabIndex = 0;
            this.codeLabel.Text = "Code";
            // 
            // expectedLabel
            // 
            this.expectedLabel.AutoSize = true;
            this.expectedLabel.Location = new System.Drawing.Point(141, 18);
            this.expectedLabel.Name = "expectedLabel";
            this.expectedLabel.Size = new System.Drawing.Size(140, 13);
            this.expectedLabel.TabIndex = 1;
            this.expectedLabel.Text = "Expected Output (% correct)";
            // 
            // codeTextBox
            // 
            this.codeTextBox.Location = new System.Drawing.Point(12, 34);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(100, 20);
            this.codeTextBox.TabIndex = 2;
            // 
            // APCLabel
            // 
            this.APCLabel.AutoSize = true;
            this.APCLabel.Location = new System.Drawing.Point(12, 83);
            this.APCLabel.Name = "APCLabel";
            this.APCLabel.Size = new System.Drawing.Size(114, 13);
            this.APCLabel.TabIndex = 3;
            this.APCLabel.Text = "Actual Percent Correct";
            // 
            // expectedOutputTextBox
            // 
            this.expectedOutputTextBox.Location = new System.Drawing.Point(156, 34);
            this.expectedOutputTextBox.Name = "expectedOutputTextBox";
            this.expectedOutputTextBox.ReadOnly = true;
            this.expectedOutputTextBox.Size = new System.Drawing.Size(100, 20);
            this.expectedOutputTextBox.TabIndex = 4;
            // 
            // percentCorrectLabel
            // 
            this.percentCorrectLabel.AutoSize = true;
            this.percentCorrectLabel.Location = new System.Drawing.Point(132, 83);
            this.percentCorrectLabel.Name = "percentCorrectLabel";
            this.percentCorrectLabel.Size = new System.Drawing.Size(35, 13);
            this.percentCorrectLabel.TabIndex = 5;
            this.percentCorrectLabel.Text = "label4";
            // 
            // verifyButton
            // 
            this.verifyButton.Location = new System.Drawing.Point(206, 78);
            this.verifyButton.Name = "verifyButton";
            this.verifyButton.Size = new System.Drawing.Size(75, 23);
            this.verifyButton.TabIndex = 6;
            this.verifyButton.Text = "Verify";
            this.verifyButton.UseVisualStyleBackColor = true;
            this.verifyButton.Click += new System.EventHandler(this.verifyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "%";
            // 
            // CodeVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 124);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.verifyButton);
            this.Controls.Add(this.percentCorrectLabel);
            this.Controls.Add(this.expectedOutputTextBox);
            this.Controls.Add(this.APCLabel);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.expectedLabel);
            this.Controls.Add(this.codeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CodeVerify";
            this.Text = "CodeVerify";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.Label expectedLabel;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Label APCLabel;
        private System.Windows.Forms.TextBox expectedOutputTextBox;
        private System.Windows.Forms.Label percentCorrectLabel;
        private System.Windows.Forms.Button verifyButton;
        private System.Windows.Forms.Label label1;
    }
}
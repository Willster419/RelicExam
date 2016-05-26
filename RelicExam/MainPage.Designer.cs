namespace RelicExam
{
    partial class MainPage
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
            this.ServiceModeButton = new System.Windows.Forms.Button();
            this.relicExamWelcome = new System.Windows.Forms.Label();
            this.selectTestType = new System.Windows.Forms.ComboBox();
            this.BeginTestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ServiceModeButton
            // 
            this.ServiceModeButton.Location = new System.Drawing.Point(15, 52);
            this.ServiceModeButton.Name = "ServiceModeButton";
            this.ServiceModeButton.Size = new System.Drawing.Size(101, 23);
            this.ServiceModeButton.TabIndex = 0;
            this.ServiceModeButton.Text = "Service Questions";
            this.ServiceModeButton.UseVisualStyleBackColor = true;
            this.ServiceModeButton.Click += new System.EventHandler(this.ServiceModeButton_Click);
            // 
            // relicExamWelcome
            // 
            this.relicExamWelcome.AutoSize = true;
            this.relicExamWelcome.Location = new System.Drawing.Point(12, 9);
            this.relicExamWelcome.Name = "relicExamWelcome";
            this.relicExamWelcome.Size = new System.Drawing.Size(258, 13);
            this.relicExamWelcome.TabIndex = 1;
            this.relicExamWelcome.Text = "Welcome to the Relic Exam Application! Best of luck!";
            // 
            // selectTestType
            // 
            this.selectTestType.FormattingEnabled = true;
            this.selectTestType.Location = new System.Drawing.Point(15, 25);
            this.selectTestType.Name = "selectTestType";
            this.selectTestType.Size = new System.Drawing.Size(121, 21);
            this.selectTestType.TabIndex = 2;
            this.selectTestType.Text = "Make A selection...";
            // 
            // BeginTestButton
            // 
            this.BeginTestButton.Location = new System.Drawing.Point(142, 25);
            this.BeginTestButton.Name = "BeginTestButton";
            this.BeginTestButton.Size = new System.Drawing.Size(75, 23);
            this.BeginTestButton.TabIndex = 3;
            this.BeginTestButton.Text = "Begin!";
            this.BeginTestButton.UseVisualStyleBackColor = true;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 80);
            this.Controls.Add(this.BeginTestButton);
            this.Controls.Add(this.selectTestType);
            this.Controls.Add(this.relicExamWelcome);
            this.Controls.Add(this.ServiceModeButton);
            this.Name = "MainPage";
            this.Text = "Relic Exam V";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ServiceModeButton;
        private System.Windows.Forms.Label relicExamWelcome;
        private System.Windows.Forms.ComboBox selectTestType;
        private System.Windows.Forms.Button BeginTestButton;
    }
}


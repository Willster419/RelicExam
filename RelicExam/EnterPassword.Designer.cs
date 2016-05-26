namespace RelicExam
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
            this.enterPasswordLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.explainEnter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // enterPasswordLabel
            // 
            this.enterPasswordLabel.AutoSize = true;
            this.enterPasswordLabel.Location = new System.Drawing.Point(12, 9);
            this.enterPasswordLabel.Name = "enterPasswordLabel";
            this.enterPasswordLabel.Size = new System.Drawing.Size(198, 13);
            this.enterPasswordLabel.TabIndex = 0;
            this.enterPasswordLabel.Text = "Please enter the service mode password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(191, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // explainEnter
            // 
            this.explainEnter.AutoSize = true;
            this.explainEnter.Location = new System.Drawing.Point(12, 48);
            this.explainEnter.Name = "explainEnter";
            this.explainEnter.Size = new System.Drawing.Size(201, 13);
            this.explainEnter.TabIndex = 3;
            this.explainEnter.Text = "Enter password and press enter or button";
            // 
            // EnterPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 91);
            this.Controls.Add(this.explainEnter);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.enterPasswordLabel);
            this.Name = "EnterPassword";
            this.Text = "EnterPassword";
            this.Load += new System.EventHandler(this.EnterPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enterPasswordLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label explainEnter;
    }
}
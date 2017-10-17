namespace WoTExam
{
    partial class LoadingWindow
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
            this.MainLoadingLabel = new System.Windows.Forms.Label();
            this.LoadingOuterPanel = new System.Windows.Forms.Panel();
            this.LoadingInnerPanel = new System.Windows.Forms.Panel();
            this.TextLabel = new System.Windows.Forms.Label();
            this.LoadingOuterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLoadingLabel
            // 
            this.MainLoadingLabel.AutoSize = true;
            this.MainLoadingLabel.Location = new System.Drawing.Point(12, 9);
            this.MainLoadingLabel.Name = "MainLoadingLabel";
            this.MainLoadingLabel.Size = new System.Drawing.Size(134, 13);
            this.MainLoadingLabel.TabIndex = 0;
            this.MainLoadingLabel.Text = "Please wait while loading...";
            // 
            // LoadingOuterPanel
            // 
            this.LoadingOuterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoadingOuterPanel.Controls.Add(this.LoadingInnerPanel);
            this.LoadingOuterPanel.Location = new System.Drawing.Point(12, 45);
            this.LoadingOuterPanel.Name = "LoadingOuterPanel";
            this.LoadingOuterPanel.Size = new System.Drawing.Size(260, 24);
            this.LoadingOuterPanel.TabIndex = 1;
            // 
            // LoadingInnerPanel
            // 
            this.LoadingInnerPanel.BackColor = System.Drawing.Color.Blue;
            this.LoadingInnerPanel.Location = new System.Drawing.Point(3, 3);
            this.LoadingInnerPanel.Name = "LoadingInnerPanel";
            this.LoadingInnerPanel.Size = new System.Drawing.Size(252, 16);
            this.LoadingInnerPanel.TabIndex = 2;
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.Location = new System.Drawing.Point(12, 29);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(59, 13);
            this.TextLabel.TabIndex = 2;
            this.TextLabel.Text = "Message...";
            // 
            // LoadingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 81);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.LoadingOuterPanel);
            this.Controls.Add(this.MainLoadingLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoadingWindow";
            this.Text = "PleaseWait";
            this.Load += new System.EventHandler(this.LoadingWindow_Load);
            this.LoadingOuterPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel LoadingOuterPanel;
        private System.Windows.Forms.Panel LoadingInnerPanel;
        private System.Windows.Forms.Label MainLoadingLabel;
        public System.Windows.Forms.Label TextLabel;
    }
}
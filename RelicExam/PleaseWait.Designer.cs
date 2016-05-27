namespace RelicExam
{
    partial class PleaseWait
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
            this.databaseLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // databaseLoading
            // 
            this.databaseLoading.AutoSize = true;
            this.databaseLoading.Location = new System.Drawing.Point(36, 11);
            this.databaseLoading.Name = "databaseLoading";
            this.databaseLoading.Size = new System.Drawing.Size(207, 13);
            this.databaseLoading.TabIndex = 0;
            this.databaseLoading.Text = "Please wait while the database is loaded...";
            // 
            // PleaseWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 33);
            this.Controls.Add(this.databaseLoading);
            this.Name = "PleaseWait";
            this.Text = "PleaseWait";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label databaseLoading;
    }
}
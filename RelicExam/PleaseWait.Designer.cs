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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.loadingUpdate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // databaseLoading
            // 
            this.databaseLoading.AutoSize = true;
            this.databaseLoading.Location = new System.Drawing.Point(9, 9);
            this.databaseLoading.Name = "databaseLoading";
            this.databaseLoading.Size = new System.Drawing.Size(134, 13);
            this.databaseLoading.TabIndex = 0;
            this.databaseLoading.Text = "Please wait while loading...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 44);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(260, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // loadingUpdate
            // 
            this.loadingUpdate.AutoSize = true;
            this.loadingUpdate.Location = new System.Drawing.Point(68, 28);
            this.loadingUpdate.Name = "loadingUpdate";
            this.loadingUpdate.Size = new System.Drawing.Size(65, 13);
            this.loadingUpdate.TabIndex = 2;
            this.loadingUpdate.Text = "doing stuff...";
            this.loadingUpdate.Visible = false;
            // 
            // PleaseWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 79);
            this.Controls.Add(this.loadingUpdate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.databaseLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PleaseWait";
            this.Text = "PleaseWait";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label loadingUpdate;
        public System.Windows.Forms.Label databaseLoading;
    }
}
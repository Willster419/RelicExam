namespace WoTExam
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
            this.EditQuestions = new System.Windows.Forms.Button();
            this.BeginTestButton = new System.Windows.Forms.Button();
            this.NumQuestions = new System.Windows.Forms.ComboBox();
            this.numQuestionsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ClanList = new System.Windows.Forms.ComboBox();
            this.VerifyCode = new System.Windows.Forms.Button();
            this.mainPageDatabaseLoader = new System.ComponentModel.BackgroundWorker();
            this.ClanMessageHeader = new System.Windows.Forms.Label();
            this.ClanMessage = new System.Windows.Forms.RichTextBox();
            this.Categories = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // EditQuestions
            // 
            this.EditQuestions.Location = new System.Drawing.Point(210, 250);
            this.EditQuestions.Name = "EditQuestions";
            this.EditQuestions.Size = new System.Drawing.Size(101, 23);
            this.EditQuestions.TabIndex = 0;
            this.EditQuestions.Text = "Edit Questions";
            this.EditQuestions.UseVisualStyleBackColor = true;
            this.EditQuestions.Click += new System.EventHandler(this.EditQuestions_Click);
            // 
            // BeginTestButton
            // 
            this.BeginTestButton.Location = new System.Drawing.Point(236, 4);
            this.BeginTestButton.Name = "BeginTestButton";
            this.BeginTestButton.Size = new System.Drawing.Size(75, 23);
            this.BeginTestButton.TabIndex = 3;
            this.BeginTestButton.Text = "Begin!";
            this.BeginTestButton.UseVisualStyleBackColor = true;
            this.BeginTestButton.Click += new System.EventHandler(this.BeginTestButton_Click);
            // 
            // NumQuestions
            // 
            this.NumQuestions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumQuestions.FormattingEnabled = true;
            this.NumQuestions.Items.AddRange(new object[] {
            "10",
            "20",
            "25",
            "50"});
            this.NumQuestions.Location = new System.Drawing.Point(185, 6);
            this.NumQuestions.Name = "NumQuestions";
            this.NumQuestions.Size = new System.Drawing.Size(45, 21);
            this.NumQuestions.TabIndex = 4;
            // 
            // numQuestionsLabel
            // 
            this.numQuestionsLabel.AutoSize = true;
            this.numQuestionsLabel.Location = new System.Drawing.Point(126, 9);
            this.numQuestionsLabel.Name = "numQuestionsLabel";
            this.numQuestionsLabel.Size = new System.Drawing.Size(54, 13);
            this.numQuestionsLabel.TabIndex = 5;
            this.numQuestionsLabel.Text = "Questions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Categories";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Clan";
            // 
            // ClanList
            // 
            this.ClanList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClanList.FormattingEnabled = true;
            this.ClanList.Location = new System.Drawing.Point(43, 6);
            this.ClanList.Name = "ClanList";
            this.ClanList.Size = new System.Drawing.Size(77, 21);
            this.ClanList.TabIndex = 9;
            this.ClanList.SelectedIndexChanged += new System.EventHandler(this.ClanList_SelectedIndexChanged);
            // 
            // VerifyCode
            // 
            this.VerifyCode.Location = new System.Drawing.Point(125, 250);
            this.VerifyCode.Name = "VerifyCode";
            this.VerifyCode.Size = new System.Drawing.Size(79, 23);
            this.VerifyCode.TabIndex = 10;
            this.VerifyCode.Text = "Verify Code";
            this.VerifyCode.UseVisualStyleBackColor = true;
            this.VerifyCode.Click += new System.EventHandler(this.VerifyCode_Click);
            // 
            // ClanMessageHeader
            // 
            this.ClanMessageHeader.AutoSize = true;
            this.ClanMessageHeader.Location = new System.Drawing.Point(12, 151);
            this.ClanMessageHeader.Name = "ClanMessageHeader";
            this.ClanMessageHeader.Size = new System.Drawing.Size(74, 13);
            this.ClanMessageHeader.TabIndex = 11;
            this.ClanMessageHeader.Text = "Clan Message";
            // 
            // ClanMessage
            // 
            this.ClanMessage.Location = new System.Drawing.Point(11, 167);
            this.ClanMessage.Name = "ClanMessage";
            this.ClanMessage.ReadOnly = true;
            this.ClanMessage.Size = new System.Drawing.Size(300, 77);
            this.ClanMessage.TabIndex = 12;
            this.ClanMessage.Text = "";
            // 
            // Categories
            // 
            this.Categories.FormattingEnabled = true;
            this.Categories.Location = new System.Drawing.Point(11, 54);
            this.Categories.Name = "Categories";
            this.Categories.Size = new System.Drawing.Size(300, 94);
            this.Categories.TabIndex = 13;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 285);
            this.Controls.Add(this.Categories);
            this.Controls.Add(this.ClanMessage);
            this.Controls.Add(this.ClanMessageHeader);
            this.Controls.Add(this.VerifyCode);
            this.Controls.Add(this.ClanList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numQuestionsLabel);
            this.Controls.Add(this.NumQuestions);
            this.Controls.Add(this.BeginTestButton);
            this.Controls.Add(this.EditQuestions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainPage";
            this.Text = "WoT Exam ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainPage_FormClosing);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EditQuestions;
        private System.Windows.Forms.Button BeginTestButton;
        private System.Windows.Forms.ComboBox NumQuestions;
        private System.Windows.Forms.Label numQuestionsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ClanList;
        private System.Windows.Forms.Button VerifyCode;
        private System.ComponentModel.BackgroundWorker mainPageDatabaseLoader;
        private System.Windows.Forms.Label ClanMessageHeader;
        private System.Windows.Forms.RichTextBox ClanMessage;
        private System.Windows.Forms.CheckedListBox Categories;
    }
}


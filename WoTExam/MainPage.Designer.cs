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
            this.ServiceModeButton = new System.Windows.Forms.Button();
            this.relicExamWelcome = new System.Windows.Forms.Label();
            this.selectTestType = new System.Windows.Forms.ComboBox();
            this.BeginTestButton = new System.Windows.Forms.Button();
            this.selectNumQuestions = new System.Windows.Forms.ComboBox();
            this.numQuestionsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.specifyTypeBox = new System.Windows.Forms.ComboBox();
            this.verifyCodeButton = new System.Windows.Forms.Button();
            this.mainPageDatabaseLoader = new System.ComponentModel.BackgroundWorker();
            this.refreshDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ServiceModeButton
            // 
            this.ServiceModeButton.Location = new System.Drawing.Point(12, 130);
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
            this.selectTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectTestType.FormattingEnabled = true;
            this.selectTestType.Items.AddRange(new object[] {
            "Map",
            "Catagory",
            "Random (All)"});
            this.selectTestType.Location = new System.Drawing.Point(56, 53);
            this.selectTestType.Name = "selectTestType";
            this.selectTestType.Size = new System.Drawing.Size(121, 21);
            this.selectTestType.TabIndex = 2;
            this.selectTestType.SelectedIndexChanged += new System.EventHandler(this.selectTestType_SelectedIndexChanged);
            // 
            // BeginTestButton
            // 
            this.BeginTestButton.Location = new System.Drawing.Point(249, 94);
            this.BeginTestButton.Name = "BeginTestButton";
            this.BeginTestButton.Size = new System.Drawing.Size(75, 23);
            this.BeginTestButton.TabIndex = 3;
            this.BeginTestButton.Text = "Begin!";
            this.BeginTestButton.UseVisualStyleBackColor = true;
            this.BeginTestButton.Click += new System.EventHandler(this.BeginTestButton_Click);
            // 
            // selectNumQuestions
            // 
            this.selectNumQuestions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectNumQuestions.FormattingEnabled = true;
            this.selectNumQuestions.Items.AddRange(new object[] {
            "10",
            "20",
            "25",
            "50"});
            this.selectNumQuestions.Location = new System.Drawing.Point(227, 53);
            this.selectNumQuestions.Name = "selectNumQuestions";
            this.selectNumQuestions.Size = new System.Drawing.Size(45, 21);
            this.selectNumQuestions.TabIndex = 4;
            // 
            // numQuestionsLabel
            // 
            this.numQuestionsLabel.AutoSize = true;
            this.numQuestionsLabel.Location = new System.Drawing.Point(278, 56);
            this.numQuestionsLabel.Name = "numQuestionsLabel";
            this.numQuestionsLabel.Size = new System.Drawing.Size(54, 13);
            this.numQuestionsLabel.TabIndex = 5;
            this.numQuestionsLabel.Text = "Questions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Take a";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "quiz of";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Specify Type";
            // 
            // specifyTypeBox
            // 
            this.specifyTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.specifyTypeBox.FormattingEnabled = true;
            this.specifyTypeBox.Location = new System.Drawing.Point(122, 94);
            this.specifyTypeBox.Name = "specifyTypeBox";
            this.specifyTypeBox.Size = new System.Drawing.Size(121, 21);
            this.specifyTypeBox.TabIndex = 9;
            // 
            // verifyCodeButton
            // 
            this.verifyCodeButton.Location = new System.Drawing.Point(119, 130);
            this.verifyCodeButton.Name = "verifyCodeButton";
            this.verifyCodeButton.Size = new System.Drawing.Size(79, 23);
            this.verifyCodeButton.TabIndex = 10;
            this.verifyCodeButton.Text = "Verify Code";
            this.verifyCodeButton.UseVisualStyleBackColor = true;
            this.verifyCodeButton.Click += new System.EventHandler(this.verifyCodeButton_Click);
            // 
            // mainPageDatabaseLoader
            // 
            this.mainPageDatabaseLoader.WorkerReportsProgress = true;
            this.mainPageDatabaseLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mainPageDatabaseLoader_DoWork);
            this.mainPageDatabaseLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mainPageDatabaseLoader_ProgressChanged);
            this.mainPageDatabaseLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mainPageDatabaseLoader_RunWorkerCompleted);
            // 
            // refreshDatabase
            // 
            this.refreshDatabase.Location = new System.Drawing.Point(204, 130);
            this.refreshDatabase.Name = "refreshDatabase";
            this.refreshDatabase.Size = new System.Drawing.Size(120, 23);
            this.refreshDatabase.TabIndex = 11;
            this.refreshDatabase.Text = "Refresh Database";
            this.refreshDatabase.UseVisualStyleBackColor = true;
            this.refreshDatabase.Click += new System.EventHandler(this.refreshDatabase_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 160);
            this.Controls.Add(this.refreshDatabase);
            this.Controls.Add(this.verifyCodeButton);
            this.Controls.Add(this.specifyTypeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numQuestionsLabel);
            this.Controls.Add(this.selectNumQuestions);
            this.Controls.Add(this.BeginTestButton);
            this.Controls.Add(this.selectTestType);
            this.Controls.Add(this.relicExamWelcome);
            this.Controls.Add(this.ServiceModeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private System.Windows.Forms.ComboBox selectNumQuestions;
        private System.Windows.Forms.Label numQuestionsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox specifyTypeBox;
        private System.Windows.Forms.Button verifyCodeButton;
        private System.ComponentModel.BackgroundWorker mainPageDatabaseLoader;
        private System.Windows.Forms.Button refreshDatabase;
    }
}


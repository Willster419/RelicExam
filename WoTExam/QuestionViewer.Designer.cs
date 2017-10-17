namespace WoTExam
{
    partial class QuestionViewer
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
            this.components = new System.ComponentModel.Container();
            this.QuestionNumber = new System.Windows.Forms.Label();
            this.QuestionTitle = new System.Windows.Forms.Label();
            this.QuestionText = new System.Windows.Forms.RichTextBox();
            this.AnswerExplanation = new System.Windows.Forms.RichTextBox();
            this.numSecondsLeft = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.AnswersPanel = new System.Windows.Forms.Panel();
            this.SubmitAnswer = new System.Windows.Forms.Button();
            this.QuestionCategory = new System.Windows.Forms.Label();
            this.TimeLeft = new System.Windows.Forms.ProgressBar();
            this.Next = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            this.PictureViewer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // QuestionNumber
            // 
            this.QuestionNumber.AutoSize = true;
            this.QuestionNumber.Location = new System.Drawing.Point(12, 9);
            this.QuestionNumber.Name = "QuestionNumber";
            this.QuestionNumber.Size = new System.Drawing.Size(49, 13);
            this.QuestionNumber.TabIndex = 1;
            this.QuestionNumber.Text = "Question";
            // 
            // QuestionTitle
            // 
            this.QuestionTitle.AutoSize = true;
            this.QuestionTitle.Location = new System.Drawing.Point(12, 35);
            this.QuestionTitle.Name = "QuestionTitle";
            this.QuestionTitle.Size = new System.Drawing.Size(30, 13);
            this.QuestionTitle.TabIndex = 4;
            this.QuestionTitle.Text = "Title:";
            // 
            // QuestionText
            // 
            this.QuestionText.Location = new System.Drawing.Point(12, 51);
            this.QuestionText.Name = "QuestionText";
            this.QuestionText.ReadOnly = true;
            this.QuestionText.Size = new System.Drawing.Size(407, 66);
            this.QuestionText.TabIndex = 11;
            this.QuestionText.Text = "";
            // 
            // AnswerExplanation
            // 
            this.AnswerExplanation.Location = new System.Drawing.Point(12, 337);
            this.AnswerExplanation.Name = "AnswerExplanation";
            this.AnswerExplanation.ReadOnly = true;
            this.AnswerExplanation.Size = new System.Drawing.Size(407, 51);
            this.AnswerExplanation.TabIndex = 12;
            this.AnswerExplanation.Text = "";
            // 
            // numSecondsLeft
            // 
            this.numSecondsLeft.AutoSize = true;
            this.numSecondsLeft.Location = new System.Drawing.Point(176, 396);
            this.numSecondsLeft.Name = "numSecondsLeft";
            this.numSecondsLeft.Size = new System.Drawing.Size(27, 13);
            this.numSecondsLeft.TabIndex = 14;
            this.numSecondsLeft.Text = "num";
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // AnswersPanel
            // 
            this.AnswersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AnswersPanel.Location = new System.Drawing.Point(12, 123);
            this.AnswersPanel.Name = "AnswersPanel";
            this.AnswersPanel.Size = new System.Drawing.Size(407, 179);
            this.AnswersPanel.TabIndex = 17;
            // 
            // SubmitAnswer
            // 
            this.SubmitAnswer.Enabled = false;
            this.SubmitAnswer.Location = new System.Drawing.Point(202, 308);
            this.SubmitAnswer.Name = "SubmitAnswer";
            this.SubmitAnswer.Size = new System.Drawing.Size(217, 23);
            this.SubmitAnswer.TabIndex = 20;
            this.SubmitAnswer.Text = "Submit";
            this.SubmitAnswer.UseVisualStyleBackColor = true;
            this.SubmitAnswer.Click += new System.EventHandler(this.SubmitAnswer_Click);
            // 
            // QuestionCategory
            // 
            this.QuestionCategory.Location = new System.Drawing.Point(12, 22);
            this.QuestionCategory.Name = "QuestionCategory";
            this.QuestionCategory.Size = new System.Drawing.Size(51, 13);
            this.QuestionCategory.TabIndex = 21;
            this.QuestionCategory.Text = "Catagory:";
            // 
            // TimeLeft
            // 
            this.TimeLeft.Location = new System.Drawing.Point(93, 412);
            this.TimeLeft.Name = "TimeLeft";
            this.TimeLeft.Size = new System.Drawing.Size(245, 23);
            this.TimeLeft.TabIndex = 20;
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(341, 394);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 41);
            this.Next.TabIndex = 22;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.NextQuestion_Click);
            // 
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(12, 394);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(75, 41);
            this.Quit.TabIndex = 23;
            this.Quit.Text = "RageQuit";
            this.Quit.UseVisualStyleBackColor = true;
            this.Quit.Click += new System.EventHandler(this.RageQuit_Click);
            // 
            // PictureViewer
            // 
            this.PictureViewer.Location = new System.Drawing.Point(422, 12);
            this.PictureViewer.Name = "PictureViewer";
            this.PictureViewer.Size = new System.Drawing.Size(315, 423);
            this.PictureViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureViewer.TabIndex = 25;
            this.PictureViewer.TabStop = false;
            // 
            // QuestionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 440);
            this.Controls.Add(this.numSecondsLeft);
            this.Controls.Add(this.TimeLeft);
            this.Controls.Add(this.SubmitAnswer);
            this.Controls.Add(this.AnswerExplanation);
            this.Controls.Add(this.QuestionCategory);
            this.Controls.Add(this.QuestionNumber);
            this.Controls.Add(this.QuestionTitle);
            this.Controls.Add(this.PictureViewer);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.AnswersPanel);
            this.Controls.Add(this.QuestionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "QuestionViewer";
            this.Text = "QuestionViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestionViewer_FormClosing);
            this.Load += new System.EventHandler(this.QuestionViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label QuestionNumber;
        private System.Windows.Forms.Label QuestionTitle;
        private System.Windows.Forms.RichTextBox QuestionText;
        private System.Windows.Forms.RichTextBox AnswerExplanation;
        private System.Windows.Forms.Label numSecondsLeft;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Panel AnswersPanel;
        private System.Windows.Forms.Label QuestionCategory;
        private System.Windows.Forms.ProgressBar TimeLeft;
        private System.Windows.Forms.Button SubmitAnswer;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Quit;
        private System.Windows.Forms.PictureBox PictureViewer;
    }
}
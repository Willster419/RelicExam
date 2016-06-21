namespace RelicExam
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
            this.currentQuestionNum = new System.Windows.Forms.Label();
            this.questionLabel = new System.Windows.Forms.Label();
            this.totalQuestions = new System.Windows.Forms.Label();
            this.of = new System.Windows.Forms.Label();
            this.questionTitleLabel = new System.Windows.Forms.Label();
            this.questionTitle = new System.Windows.Forms.Label();
            this.radioButtonA = new System.Windows.Forms.RadioButton();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonC = new System.Windows.Forms.RadioButton();
            this.radioButtonD = new System.Windows.Forms.RadioButton();
            this.theQuestionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.correctOrNot = new System.Windows.Forms.Label();
            this.numSecondsLeft = new System.Windows.Forms.Label();
            this.secondsLeftLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.responseD = new System.Windows.Forms.TextBox();
            this.responseC = new System.Windows.Forms.TextBox();
            this.responseB = new System.Windows.Forms.TextBox();
            this.submitAnswer = new System.Windows.Forms.Button();
            this.responseA = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.theMap = new System.Windows.Forms.Label();
            this.mapLabel = new System.Windows.Forms.Label();
            this.theCatagory = new System.Windows.Forms.Label();
            this.catagoryLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.outOfTime = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.nextQuestion = new System.Windows.Forms.Button();
            this.rageQuit = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentQuestionNum
            // 
            this.currentQuestionNum.AutoSize = true;
            this.currentQuestionNum.Location = new System.Drawing.Point(56, 9);
            this.currentQuestionNum.Name = "currentQuestionNum";
            this.currentQuestionNum.Size = new System.Drawing.Size(27, 13);
            this.currentQuestionNum.TabIndex = 0;
            this.currentQuestionNum.Text = "num";
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(3, 9);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(49, 13);
            this.questionLabel.TabIndex = 1;
            this.questionLabel.Text = "Question";
            // 
            // totalQuestions
            // 
            this.totalQuestions.AutoSize = true;
            this.totalQuestions.Location = new System.Drawing.Point(111, 9);
            this.totalQuestions.Name = "totalQuestions";
            this.totalQuestions.Size = new System.Drawing.Size(27, 13);
            this.totalQuestions.TabIndex = 2;
            this.totalQuestions.Text = "num";
            // 
            // of
            // 
            this.of.AutoSize = true;
            this.of.Location = new System.Drawing.Point(89, 9);
            this.of.Name = "of";
            this.of.Size = new System.Drawing.Size(16, 13);
            this.of.TabIndex = 3;
            this.of.Text = "of";
            // 
            // questionTitleLabel
            // 
            this.questionTitleLabel.AutoSize = true;
            this.questionTitleLabel.Location = new System.Drawing.Point(42, 5);
            this.questionTitleLabel.Name = "questionTitleLabel";
            this.questionTitleLabel.Size = new System.Drawing.Size(30, 13);
            this.questionTitleLabel.TabIndex = 4;
            this.questionTitleLabel.Text = "Title:";
            // 
            // questionTitle
            // 
            this.questionTitle.AutoSize = true;
            this.questionTitle.Location = new System.Drawing.Point(111, 5);
            this.questionTitle.Name = "questionTitle";
            this.questionTitle.Size = new System.Drawing.Size(35, 13);
            this.questionTitle.TabIndex = 5;
            this.questionTitle.Text = "label6";
            // 
            // radioButtonA
            // 
            this.radioButtonA.AutoSize = true;
            this.radioButtonA.Location = new System.Drawing.Point(3, 3);
            this.radioButtonA.Name = "radioButtonA";
            this.radioButtonA.Size = new System.Drawing.Size(32, 17);
            this.radioButtonA.TabIndex = 7;
            this.radioButtonA.TabStop = true;
            this.radioButtonA.Text = "A";
            this.radioButtonA.UseVisualStyleBackColor = true;
            // 
            // radioButtonB
            // 
            this.radioButtonB.AutoSize = true;
            this.radioButtonB.Location = new System.Drawing.Point(3, 26);
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.Size = new System.Drawing.Size(32, 17);
            this.radioButtonB.TabIndex = 8;
            this.radioButtonB.TabStop = true;
            this.radioButtonB.Text = "B";
            this.radioButtonB.UseVisualStyleBackColor = true;
            // 
            // radioButtonC
            // 
            this.radioButtonC.AutoSize = true;
            this.radioButtonC.Location = new System.Drawing.Point(3, 49);
            this.radioButtonC.Name = "radioButtonC";
            this.radioButtonC.Size = new System.Drawing.Size(32, 17);
            this.radioButtonC.TabIndex = 9;
            this.radioButtonC.TabStop = true;
            this.radioButtonC.Text = "C";
            this.radioButtonC.UseVisualStyleBackColor = true;
            // 
            // radioButtonD
            // 
            this.radioButtonD.AutoSize = true;
            this.radioButtonD.Location = new System.Drawing.Point(3, 72);
            this.radioButtonD.Name = "radioButtonD";
            this.radioButtonD.Size = new System.Drawing.Size(33, 17);
            this.radioButtonD.TabIndex = 10;
            this.radioButtonD.TabStop = true;
            this.radioButtonD.Text = "D";
            this.radioButtonD.UseVisualStyleBackColor = true;
            // 
            // theQuestionRichTextBox
            // 
            this.theQuestionRichTextBox.Location = new System.Drawing.Point(12, 103);
            this.theQuestionRichTextBox.Name = "theQuestionRichTextBox";
            this.theQuestionRichTextBox.ReadOnly = true;
            this.theQuestionRichTextBox.Size = new System.Drawing.Size(612, 50);
            this.theQuestionRichTextBox.TabIndex = 11;
            this.theQuestionRichTextBox.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(3, 16);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(606, 81);
            this.richTextBox2.TabIndex = 12;
            this.richTextBox2.Text = "";
            // 
            // correctOrNot
            // 
            this.correctOrNot.AutoSize = true;
            this.correctOrNot.Location = new System.Drawing.Point(281, 0);
            this.correctOrNot.Name = "correctOrNot";
            this.correctOrNot.Size = new System.Drawing.Size(47, 13);
            this.correctOrNot.TabIndex = 13;
            this.correctOrNot.Text = "Correct?";
            // 
            // numSecondsLeft
            // 
            this.numSecondsLeft.AutoSize = true;
            this.numSecondsLeft.Location = new System.Drawing.Point(45, 14);
            this.numSecondsLeft.Name = "numSecondsLeft";
            this.numSecondsLeft.Size = new System.Drawing.Size(27, 13);
            this.numSecondsLeft.TabIndex = 14;
            this.numSecondsLeft.Text = "num";
            // 
            // secondsLeftLabel
            // 
            this.secondsLeftLabel.AutoSize = true;
            this.secondsLeftLabel.Location = new System.Drawing.Point(144, 14);
            this.secondsLeftLabel.Name = "secondsLeftLabel";
            this.secondsLeftLabel.Size = new System.Drawing.Size(64, 13);
            this.secondsLeftLabel.TabIndex = 15;
            this.secondsLeftLabel.Text = "seconds left";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.responseD);
            this.panel1.Controls.Add(this.responseC);
            this.panel1.Controls.Add(this.responseB);
            this.panel1.Controls.Add(this.submitAnswer);
            this.panel1.Controls.Add(this.responseA);
            this.panel1.Controls.Add(this.radioButtonA);
            this.panel1.Controls.Add(this.radioButtonB);
            this.panel1.Controls.Add(this.radioButtonC);
            this.panel1.Controls.Add(this.radioButtonD);
            this.panel1.Location = new System.Drawing.Point(12, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 129);
            this.panel1.TabIndex = 17;
            // 
            // responseD
            // 
            this.responseD.Location = new System.Drawing.Point(42, 74);
            this.responseD.Name = "responseD";
            this.responseD.ReadOnly = true;
            this.responseD.Size = new System.Drawing.Size(567, 20);
            this.responseD.TabIndex = 21;
            // 
            // responseC
            // 
            this.responseC.Location = new System.Drawing.Point(42, 48);
            this.responseC.Name = "responseC";
            this.responseC.ReadOnly = true;
            this.responseC.Size = new System.Drawing.Size(567, 20);
            this.responseC.TabIndex = 22;
            // 
            // responseB
            // 
            this.responseB.Location = new System.Drawing.Point(42, 26);
            this.responseB.Name = "responseB";
            this.responseB.ReadOnly = true;
            this.responseB.Size = new System.Drawing.Size(567, 20);
            this.responseB.TabIndex = 23;
            // 
            // submitAnswer
            // 
            this.submitAnswer.Location = new System.Drawing.Point(3, 100);
            this.submitAnswer.Name = "submitAnswer";
            this.submitAnswer.Size = new System.Drawing.Size(606, 23);
            this.submitAnswer.TabIndex = 20;
            this.submitAnswer.Text = "submit";
            this.submitAnswer.UseVisualStyleBackColor = true;
            this.submitAnswer.Click += new System.EventHandler(this.submitAnswer_Click);
            // 
            // responseA
            // 
            this.responseA.Location = new System.Drawing.Point(42, 3);
            this.responseA.Name = "responseA";
            this.responseA.ReadOnly = true;
            this.responseA.Size = new System.Drawing.Size(567, 20);
            this.responseA.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.theMap);
            this.panel2.Controls.Add(this.mapLabel);
            this.panel2.Controls.Add(this.currentQuestionNum);
            this.panel2.Controls.Add(this.theCatagory);
            this.panel2.Controls.Add(this.questionLabel);
            this.panel2.Controls.Add(this.catagoryLabel);
            this.panel2.Controls.Add(this.totalQuestions);
            this.panel2.Controls.Add(this.of);
            this.panel2.Location = new System.Drawing.Point(12, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(612, 38);
            this.panel2.TabIndex = 18;
            // 
            // theMap
            // 
            this.theMap.Location = new System.Drawing.Point(534, 12);
            this.theMap.Name = "theMap";
            this.theMap.Size = new System.Drawing.Size(83, 16);
            this.theMap.TabIndex = 0;
            this.theMap.Text = "label10";
            // 
            // mapLabel
            // 
            this.mapLabel.Location = new System.Drawing.Point(509, 12);
            this.mapLabel.Name = "mapLabel";
            this.mapLabel.Size = new System.Drawing.Size(32, 16);
            this.mapLabel.TabIndex = 23;
            this.mapLabel.Text = "Map:";
            // 
            // theCatagory
            // 
            this.theCatagory.Location = new System.Drawing.Point(417, 12);
            this.theCatagory.Name = "theCatagory";
            this.theCatagory.Size = new System.Drawing.Size(86, 16);
            this.theCatagory.TabIndex = 22;
            this.theCatagory.Text = "label12";
            // 
            // catagoryLabel
            // 
            this.catagoryLabel.Location = new System.Drawing.Point(369, 12);
            this.catagoryLabel.Name = "catagoryLabel";
            this.catagoryLabel.Size = new System.Drawing.Size(51, 13);
            this.catagoryLabel.TabIndex = 21;
            this.catagoryLabel.Text = "Catagory:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.outOfTime);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Controls.Add(this.numSecondsLeft);
            this.panel3.Controls.Add(this.secondsLeftLabel);
            this.panel3.Location = new System.Drawing.Point(12, 294);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(612, 73);
            this.panel3.TabIndex = 19;
            // 
            // outOfTime
            // 
            this.outOfTime.AutoSize = true;
            this.outOfTime.Location = new System.Drawing.Point(369, 14);
            this.outOfTime.Name = "outOfTime";
            this.outOfTime.Size = new System.Drawing.Size(61, 13);
            this.outOfTime.TabIndex = 21;
            this.outOfTime.Text = "Out of time!";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 38);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(606, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.questionTitleLabel);
            this.panel4.Controls.Add(this.questionTitle);
            this.panel4.Location = new System.Drawing.Point(12, 73);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(612, 24);
            this.panel4.TabIndex = 21;
            // 
            // nextQuestion
            // 
            this.nextQuestion.Location = new System.Drawing.Point(549, 481);
            this.nextQuestion.Name = "nextQuestion";
            this.nextQuestion.Size = new System.Drawing.Size(75, 23);
            this.nextQuestion.TabIndex = 22;
            this.nextQuestion.Text = "Next";
            this.nextQuestion.UseVisualStyleBackColor = true;
            this.nextQuestion.Click += new System.EventHandler(this.nextQuestion_Click);
            // 
            // rageQuit
            // 
            this.rageQuit.Location = new System.Drawing.Point(9, 481);
            this.rageQuit.Name = "rageQuit";
            this.rageQuit.Size = new System.Drawing.Size(75, 23);
            this.rageQuit.TabIndex = 23;
            this.rageQuit.Text = "RageQuit";
            this.rageQuit.UseVisualStyleBackColor = true;
            this.rageQuit.Click += new System.EventHandler(this.rageQuit_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.correctOrNot);
            this.panel5.Controls.Add(this.richTextBox2);
            this.panel5.Location = new System.Drawing.Point(12, 375);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(612, 100);
            this.panel5.TabIndex = 24;
            // 
            // QuestionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 516);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.rageQuit);
            this.Controls.Add(this.nextQuestion);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.theQuestionRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "QuestionViewer";
            this.Text = "QuestionViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestionViewer_FormClosing);
            this.Load += new System.EventHandler(this.QuestionViewer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label currentQuestionNum;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.Label totalQuestions;
        private System.Windows.Forms.Label of;
        private System.Windows.Forms.Label questionTitleLabel;
        private System.Windows.Forms.Label questionTitle;
        private System.Windows.Forms.RadioButton radioButtonA;
        private System.Windows.Forms.RadioButton radioButtonB;
        private System.Windows.Forms.RadioButton radioButtonC;
        private System.Windows.Forms.RadioButton radioButtonD;
        private System.Windows.Forms.RichTextBox theQuestionRichTextBox;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label correctOrNot;
        private System.Windows.Forms.Label numSecondsLeft;
        private System.Windows.Forms.Label secondsLeftLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox responseD;
        private System.Windows.Forms.TextBox responseC;
        private System.Windows.Forms.TextBox responseB;
        private System.Windows.Forms.TextBox responseA;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label theMap;
        private System.Windows.Forms.Label mapLabel;
        private System.Windows.Forms.Label theCatagory;
        private System.Windows.Forms.Label catagoryLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button submitAnswer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button nextQuestion;
        private System.Windows.Forms.Button rageQuit;
        private System.Windows.Forms.Label outOfTime;
        private System.Windows.Forms.Panel panel5;
    }
}
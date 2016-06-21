﻿namespace RelicExam
{
    partial class DatabaseManager
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
            this.questionComboBox = new System.Windows.Forms.ComboBox();
            this.selectQuestion = new System.Windows.Forms.Label();
            this.responseATextBox = new System.Windows.Forms.TextBox();
            this.responseBTextBox = new System.Windows.Forms.TextBox();
            this.responseCTextBox = new System.Windows.Forms.TextBox();
            this.answerCEnable = new System.Windows.Forms.CheckBox();
            this.responseDTextBox = new System.Windows.Forms.TextBox();
            this.answerDEnable = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.answerMarkD = new System.Windows.Forms.RadioButton();
            this.answerMarkC = new System.Windows.Forms.RadioButton();
            this.answerMarkB = new System.Windows.Forms.RadioButton();
            this.answerMarkA = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.questionTextBox = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.timeToAnswerTextBox = new System.Windows.Forms.TextBox();
            this.timeToAnswerLabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.mapChoiceLabel = new System.Windows.Forms.Label();
            this.mapComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.clearForm = new System.Windows.Forms.Button();
            this.catagoryComboBox = new System.Windows.Forms.ComboBox();
            this.catagoryLabel = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.expTextBox = new System.Windows.Forms.RichTextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.theQuestionTitle = new System.Windows.Forms.TextBox();
            this.questionTitle = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.currentModeLabel = new System.Windows.Forms.Label();
            this.currentModeHeader = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.verifyCode = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // questionComboBox
            // 
            this.questionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.questionComboBox.FormattingEnabled = true;
            this.questionComboBox.Items.AddRange(new object[] {
            "-no selection / new question -"});
            this.questionComboBox.Location = new System.Drawing.Point(3, 17);
            this.questionComboBox.Name = "questionComboBox";
            this.questionComboBox.Size = new System.Drawing.Size(436, 21);
            this.questionComboBox.TabIndex = 0;
            this.questionComboBox.SelectedIndexChanged += new System.EventHandler(this.questionComboBox_SelectedIndexChanged);
            // 
            // selectQuestion
            // 
            this.selectQuestion.AutoSize = true;
            this.selectQuestion.Location = new System.Drawing.Point(3, 0);
            this.selectQuestion.Name = "selectQuestion";
            this.selectQuestion.Size = new System.Drawing.Size(82, 13);
            this.selectQuestion.TabIndex = 1;
            this.selectQuestion.Text = "Select Question";
            // 
            // responseATextBox
            // 
            this.responseATextBox.Location = new System.Drawing.Point(90, 3);
            this.responseATextBox.Name = "responseATextBox";
            this.responseATextBox.Size = new System.Drawing.Size(258, 20);
            this.responseATextBox.TabIndex = 4;
            // 
            // responseBTextBox
            // 
            this.responseBTextBox.Location = new System.Drawing.Point(90, 26);
            this.responseBTextBox.Name = "responseBTextBox";
            this.responseBTextBox.Size = new System.Drawing.Size(258, 20);
            this.responseBTextBox.TabIndex = 6;
            // 
            // responseCTextBox
            // 
            this.responseCTextBox.Location = new System.Drawing.Point(90, 49);
            this.responseCTextBox.Name = "responseCTextBox";
            this.responseCTextBox.Size = new System.Drawing.Size(258, 20);
            this.responseCTextBox.TabIndex = 9;
            // 
            // answerCEnable
            // 
            this.answerCEnable.AutoSize = true;
            this.answerCEnable.Location = new System.Drawing.Point(3, 49);
            this.answerCEnable.Name = "answerCEnable";
            this.answerCEnable.Size = new System.Drawing.Size(65, 17);
            this.answerCEnable.TabIndex = 8;
            this.answerCEnable.Text = "Enabled";
            this.answerCEnable.UseVisualStyleBackColor = true;
            this.answerCEnable.CheckedChanged += new System.EventHandler(this.answerCEnable_CheckedChanged);
            this.answerCEnable.CheckStateChanged += new System.EventHandler(this.answerCEnable_CheckStateChanged);
            this.answerCEnable.Click += new System.EventHandler(this.answerCEnable_Click);
            // 
            // responseDTextBox
            // 
            this.responseDTextBox.Location = new System.Drawing.Point(90, 72);
            this.responseDTextBox.Name = "responseDTextBox";
            this.responseDTextBox.Size = new System.Drawing.Size(258, 20);
            this.responseDTextBox.TabIndex = 12;
            // 
            // answerDEnable
            // 
            this.answerDEnable.AutoSize = true;
            this.answerDEnable.Location = new System.Drawing.Point(3, 72);
            this.answerDEnable.Name = "answerDEnable";
            this.answerDEnable.Size = new System.Drawing.Size(65, 17);
            this.answerDEnable.TabIndex = 11;
            this.answerDEnable.Text = "Enabled";
            this.answerDEnable.UseVisualStyleBackColor = true;
            this.answerDEnable.CheckedChanged += new System.EventHandler(this.answerDEnable_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectQuestion);
            this.panel1.Controls.Add(this.questionComboBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 42);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.answerMarkD);
            this.panel2.Controls.Add(this.answerMarkC);
            this.panel2.Controls.Add(this.responseATextBox);
            this.panel2.Controls.Add(this.answerMarkB);
            this.panel2.Controls.Add(this.responseDTextBox);
            this.panel2.Controls.Add(this.answerMarkA);
            this.panel2.Controls.Add(this.answerDEnable);
            this.panel2.Controls.Add(this.responseBTextBox);
            this.panel2.Controls.Add(this.responseCTextBox);
            this.panel2.Controls.Add(this.answerCEnable);
            this.panel2.Location = new System.Drawing.Point(12, 141);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 100);
            this.panel2.TabIndex = 4;
            // 
            // answerMarkD
            // 
            this.answerMarkD.AutoSize = true;
            this.answerMarkD.Location = new System.Drawing.Point(357, 73);
            this.answerMarkD.Name = "answerMarkD";
            this.answerMarkD.Size = new System.Drawing.Size(60, 17);
            this.answerMarkD.TabIndex = 13;
            this.answerMarkD.TabStop = true;
            this.answerMarkD.Text = "Answer";
            this.answerMarkD.UseVisualStyleBackColor = true;
            // 
            // answerMarkC
            // 
            this.answerMarkC.AutoSize = true;
            this.answerMarkC.Location = new System.Drawing.Point(357, 50);
            this.answerMarkC.Name = "answerMarkC";
            this.answerMarkC.Size = new System.Drawing.Size(60, 17);
            this.answerMarkC.TabIndex = 10;
            this.answerMarkC.TabStop = true;
            this.answerMarkC.Text = "Answer";
            this.answerMarkC.UseVisualStyleBackColor = true;
            // 
            // answerMarkB
            // 
            this.answerMarkB.AutoSize = true;
            this.answerMarkB.Location = new System.Drawing.Point(357, 29);
            this.answerMarkB.Name = "answerMarkB";
            this.answerMarkB.Size = new System.Drawing.Size(60, 17);
            this.answerMarkB.TabIndex = 7;
            this.answerMarkB.TabStop = true;
            this.answerMarkB.Text = "Answer";
            this.answerMarkB.UseVisualStyleBackColor = true;
            // 
            // answerMarkA
            // 
            this.answerMarkA.AutoSize = true;
            this.answerMarkA.Location = new System.Drawing.Point(357, 6);
            this.answerMarkA.Name = "answerMarkA";
            this.answerMarkA.Size = new System.Drawing.Size(60, 17);
            this.answerMarkA.TabIndex = 5;
            this.answerMarkA.TabStop = true;
            this.answerMarkA.Text = "Answer";
            this.answerMarkA.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.questionTextBox);
            this.panel3.Location = new System.Drawing.Point(12, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(445, 50);
            this.panel3.TabIndex = 3;
            // 
            // questionTextBox
            // 
            this.questionTextBox.Location = new System.Drawing.Point(6, 4);
            this.questionTextBox.Name = "questionTextBox";
            this.questionTextBox.Size = new System.Drawing.Size(436, 43);
            this.questionTextBox.TabIndex = 3;
            this.questionTextBox.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.secondsLabel);
            this.panel4.Controls.Add(this.timeToAnswerTextBox);
            this.panel4.Controls.Add(this.timeToAnswerLabel);
            this.panel4.Location = new System.Drawing.Point(12, 299);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 30);
            this.panel4.TabIndex = 6;
            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.Location = new System.Drawing.Point(152, 7);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(49, 13);
            this.secondsLabel.TabIndex = 2;
            this.secondsLabel.Text = "Seconds";
            // 
            // timeToAnswerTextBox
            // 
            this.timeToAnswerTextBox.Location = new System.Drawing.Point(94, 4);
            this.timeToAnswerTextBox.Name = "timeToAnswerTextBox";
            this.timeToAnswerTextBox.Size = new System.Drawing.Size(52, 20);
            this.timeToAnswerTextBox.TabIndex = 14;
            this.timeToAnswerTextBox.Enter += new System.EventHandler(this.timeToAnswerTextBox_Enter);
            this.timeToAnswerTextBox.Leave += new System.EventHandler(this.timeToAnswerTextBox_Leave);
            this.timeToAnswerTextBox.MouseLeave += new System.EventHandler(this.timeToAnswerTextBox_MouseLeave);
            // 
            // timeToAnswerLabel
            // 
            this.timeToAnswerLabel.AutoSize = true;
            this.timeToAnswerLabel.Location = new System.Drawing.Point(4, 4);
            this.timeToAnswerLabel.Name = "timeToAnswerLabel";
            this.timeToAnswerLabel.Size = new System.Drawing.Size(84, 13);
            this.timeToAnswerLabel.TabIndex = 0;
            this.timeToAnswerLabel.Text = "Time To Answer";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.mapChoiceLabel);
            this.panel5.Controls.Add(this.mapComboBox);
            this.panel5.Location = new System.Drawing.Point(12, 335);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(231, 50);
            this.panel5.TabIndex = 9;
            // 
            // mapChoiceLabel
            // 
            this.mapChoiceLabel.AutoSize = true;
            this.mapChoiceLabel.Location = new System.Drawing.Point(4, 19);
            this.mapChoiceLabel.Name = "mapChoiceLabel";
            this.mapChoiceLabel.Size = new System.Drawing.Size(88, 13);
            this.mapChoiceLabel.TabIndex = 17;
            this.mapChoiceLabel.Text = "is specific to map";
            // 
            // mapComboBox
            // 
            this.mapComboBox.FormattingEnabled = true;
            this.mapComboBox.Items.AddRange(new object[] {
            "NONE"});
            this.mapComboBox.Location = new System.Drawing.Point(107, 16);
            this.mapComboBox.Name = "mapComboBox";
            this.mapComboBox.Size = new System.Drawing.Size(121, 21);
            this.mapComboBox.TabIndex = 16;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(14, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 17;
            this.saveButton.Text = "update";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(379, 460);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "read database";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(3, 460);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "write sample database";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // clearForm
            // 
            this.clearForm.Location = new System.Drawing.Point(299, 0);
            this.clearForm.Name = "clearForm";
            this.clearForm.Size = new System.Drawing.Size(140, 23);
            this.clearForm.TabIndex = 2;
            this.clearForm.Text = "reset GUI";
            this.clearForm.UseVisualStyleBackColor = true;
            this.clearForm.Click += new System.EventHandler(this.clearForm_Click);
            // 
            // catagoryComboBox
            // 
            this.catagoryComboBox.FormattingEnabled = true;
            this.catagoryComboBox.Location = new System.Drawing.Point(79, 6);
            this.catagoryComboBox.Name = "catagoryComboBox";
            this.catagoryComboBox.Size = new System.Drawing.Size(121, 21);
            this.catagoryComboBox.TabIndex = 15;
            // 
            // catagoryLabel
            // 
            this.catagoryLabel.AutoSize = true;
            this.catagoryLabel.Location = new System.Drawing.Point(3, 7);
            this.catagoryLabel.Name = "catagoryLabel";
            this.catagoryLabel.Size = new System.Drawing.Size(70, 13);
            this.catagoryLabel.TabIndex = 15;
            this.catagoryLabel.Text = "is of catagory";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.expTextBox);
            this.panel6.Location = new System.Drawing.Point(12, 247);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(445, 46);
            this.panel6.TabIndex = 5;
            // 
            // expTextBox
            // 
            this.expTextBox.Location = new System.Drawing.Point(6, 3);
            this.expTextBox.Name = "expTextBox";
            this.expTextBox.Size = new System.Drawing.Size(433, 40);
            this.expTextBox.TabIndex = 1;
            this.expTextBox.Text = "Explanation of Answer...";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.theQuestionTitle);
            this.panel7.Controls.Add(this.questionTitle);
            this.panel7.Controls.Add(this.clearForm);
            this.panel7.Location = new System.Drawing.Point(12, 56);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(445, 27);
            this.panel7.TabIndex = 1;
            // 
            // theQuestionTitle
            // 
            this.theQuestionTitle.Location = new System.Drawing.Point(36, 3);
            this.theQuestionTitle.Name = "theQuestionTitle";
            this.theQuestionTitle.Size = new System.Drawing.Size(257, 20);
            this.theQuestionTitle.TabIndex = 1;
            // 
            // questionTitle
            // 
            this.questionTitle.AutoSize = true;
            this.questionTitle.Location = new System.Drawing.Point(3, 1);
            this.questionTitle.Name = "questionTitle";
            this.questionTitle.Size = new System.Drawing.Size(27, 13);
            this.questionTitle.TabIndex = 0;
            this.questionTitle.Text = "Title";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.catagoryLabel);
            this.panel8.Controls.Add(this.catagoryComboBox);
            this.panel8.Location = new System.Drawing.Point(229, 299);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(228, 30);
            this.panel8.TabIndex = 8;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.currentModeLabel);
            this.panel9.Controls.Add(this.currentModeHeader);
            this.panel9.Location = new System.Drawing.Point(356, 335);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(101, 50);
            this.panel9.TabIndex = 22;
            // 
            // currentModeLabel
            // 
            this.currentModeLabel.AutoSize = true;
            this.currentModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentModeLabel.ForeColor = System.Drawing.Color.Crimson;
            this.currentModeLabel.Location = new System.Drawing.Point(3, 19);
            this.currentModeLabel.Name = "currentModeLabel";
            this.currentModeLabel.Size = new System.Drawing.Size(63, 25);
            this.currentModeLabel.TabIndex = 1;
            this.currentModeLabel.Text = "EDIT";
            this.currentModeLabel.Visible = false;
            // 
            // currentModeHeader
            // 
            this.currentModeHeader.AutoSize = true;
            this.currentModeHeader.Location = new System.Drawing.Point(3, 6);
            this.currentModeHeader.Name = "currentModeHeader";
            this.currentModeHeader.Size = new System.Drawing.Size(72, 13);
            this.currentModeHeader.TabIndex = 0;
            this.currentModeHeader.Text = "current mode:";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(14, 24);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 18;
            this.removeButton.Text = "remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.removeButton);
            this.panel10.Controls.Add(this.saveButton);
            this.panel10.Location = new System.Drawing.Point(359, 388);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(95, 50);
            this.panel10.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(132, 460);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Create Blank Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Dev Options Below";
            // 
            // verifyCode
            // 
            this.verifyCode.Location = new System.Drawing.Point(265, 344);
            this.verifyCode.Name = "verifyCode";
            this.verifyCode.Size = new System.Drawing.Size(75, 23);
            this.verifyCode.TabIndex = 24;
            this.verifyCode.Text = "Verify Code";
            this.verifyCode.UseVisualStyleBackColor = true;
            this.verifyCode.Click += new System.EventHandler(this.verifyCode_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Enabled = false;
            this.uploadButton.Location = new System.Drawing.Point(265, 460);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(108, 23);
            this.uploadButton.TabIndex = 25;
            this.uploadButton.Text = "Upload to Dropbox";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.button2);
            this.panel11.Controls.Add(this.label2);
            this.panel11.Controls.Add(this.comboBox1);
            this.panel11.Location = new System.Drawing.Point(12, 388);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(338, 50);
            this.panel11.TabIndex = 26;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(268, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "add new...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Select Photo";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "NONE"});
            this.comboBox1.Location = new System.Drawing.Point(6, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(256, 21);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"Supported Picture Types|*.jpg;*.jpeg;*.png;*.bmp;*.gif\"";
            // 
            // DatabaseManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 495);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.verifyCode);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DatabaseManager";
            this.Text = "DatabaseManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseManager_FormClosing);
            this.Load += new System.EventHandler(this.DatabaseManager_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox questionComboBox;
        private System.Windows.Forms.Label selectQuestion;
        private System.Windows.Forms.TextBox responseATextBox;
        private System.Windows.Forms.TextBox responseBTextBox;
        private System.Windows.Forms.TextBox responseCTextBox;
        private System.Windows.Forms.CheckBox answerCEnable;
        private System.Windows.Forms.TextBox responseDTextBox;
        private System.Windows.Forms.CheckBox answerDEnable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton answerMarkD;
        private System.Windows.Forms.RadioButton answerMarkC;
        private System.Windows.Forms.RadioButton answerMarkB;
        private System.Windows.Forms.RadioButton answerMarkA;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox questionTextBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label secondsLabel;
        private System.Windows.Forms.TextBox timeToAnswerTextBox;
        private System.Windows.Forms.Label timeToAnswerLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox mapComboBox;
        private System.Windows.Forms.ComboBox catagoryComboBox;
        private System.Windows.Forms.Label catagoryLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button clearForm;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RichTextBox expTextBox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox theQuestionTitle;
        private System.Windows.Forms.Label questionTitle;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label currentModeHeader;
        private System.Windows.Forms.Label currentModeLabel;
        private System.Windows.Forms.Label mapChoiceLabel;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button verifyCode;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
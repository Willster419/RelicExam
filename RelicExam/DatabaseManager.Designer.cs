namespace RelicExam
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
            this.AnswerMarkD = new System.Windows.Forms.RadioButton();
            this.AnswerMarkC = new System.Windows.Forms.RadioButton();
            this.answerMarkB = new System.Windows.Forms.RadioButton();
            this.answerMarkA = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.questionTextBox = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.explanationEnabled = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.explanationLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // questionComboBox
            // 
            this.questionComboBox.FormattingEnabled = true;
            this.questionComboBox.Location = new System.Drawing.Point(3, 17);
            this.questionComboBox.Name = "questionComboBox";
            this.questionComboBox.Size = new System.Drawing.Size(436, 21);
            this.questionComboBox.TabIndex = 0;
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
            this.responseATextBox.TabIndex = 3;
            // 
            // responseBTextBox
            // 
            this.responseBTextBox.Location = new System.Drawing.Point(90, 26);
            this.responseBTextBox.Name = "responseBTextBox";
            this.responseBTextBox.Size = new System.Drawing.Size(258, 20);
            this.responseBTextBox.TabIndex = 5;
            // 
            // responseCTextBox
            // 
            this.responseCTextBox.Location = new System.Drawing.Point(90, 49);
            this.responseCTextBox.Name = "responseCTextBox";
            this.responseCTextBox.Size = new System.Drawing.Size(258, 20);
            this.responseCTextBox.TabIndex = 7;
            // 
            // answerCEnable
            // 
            this.answerCEnable.AutoSize = true;
            this.answerCEnable.Location = new System.Drawing.Point(3, 49);
            this.answerCEnable.Name = "answerCEnable";
            this.answerCEnable.Size = new System.Drawing.Size(65, 17);
            this.answerCEnable.TabIndex = 6;
            this.answerCEnable.Text = "Enabled";
            this.answerCEnable.UseVisualStyleBackColor = true;
            this.answerCEnable.CheckedChanged += new System.EventHandler(this.answerCEnable_CheckedChanged);
            // 
            // responseDTextBox
            // 
            this.responseDTextBox.Location = new System.Drawing.Point(90, 72);
            this.responseDTextBox.Name = "responseDTextBox";
            this.responseDTextBox.Size = new System.Drawing.Size(258, 20);
            this.responseDTextBox.TabIndex = 9;
            // 
            // answerDEnable
            // 
            this.answerDEnable.AutoSize = true;
            this.answerDEnable.Location = new System.Drawing.Point(3, 72);
            this.answerDEnable.Name = "answerDEnable";
            this.answerDEnable.Size = new System.Drawing.Size(65, 17);
            this.answerDEnable.TabIndex = 8;
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
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AnswerMarkD);
            this.panel2.Controls.Add(this.AnswerMarkC);
            this.panel2.Controls.Add(this.responseATextBox);
            this.panel2.Controls.Add(this.answerMarkB);
            this.panel2.Controls.Add(this.responseDTextBox);
            this.panel2.Controls.Add(this.answerMarkA);
            this.panel2.Controls.Add(this.answerDEnable);
            this.panel2.Controls.Add(this.responseBTextBox);
            this.panel2.Controls.Add(this.responseCTextBox);
            this.panel2.Controls.Add(this.answerCEnable);
            this.panel2.Location = new System.Drawing.Point(12, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 100);
            this.panel2.TabIndex = 11;
            // 
            // AnswerMarkD
            // 
            this.AnswerMarkD.AutoSize = true;
            this.AnswerMarkD.Location = new System.Drawing.Point(357, 73);
            this.AnswerMarkD.Name = "AnswerMarkD";
            this.AnswerMarkD.Size = new System.Drawing.Size(60, 17);
            this.AnswerMarkD.TabIndex = 4;
            this.AnswerMarkD.TabStop = true;
            this.AnswerMarkD.Text = "Answer";
            this.AnswerMarkD.UseVisualStyleBackColor = true;
            // 
            // AnswerMarkC
            // 
            this.AnswerMarkC.AutoSize = true;
            this.AnswerMarkC.Location = new System.Drawing.Point(357, 50);
            this.AnswerMarkC.Name = "AnswerMarkC";
            this.AnswerMarkC.Size = new System.Drawing.Size(60, 17);
            this.AnswerMarkC.TabIndex = 3;
            this.AnswerMarkC.TabStop = true;
            this.AnswerMarkC.Text = "Answer";
            this.AnswerMarkC.UseVisualStyleBackColor = true;
            // 
            // answerMarkB
            // 
            this.answerMarkB.AutoSize = true;
            this.answerMarkB.Location = new System.Drawing.Point(357, 29);
            this.answerMarkB.Name = "answerMarkB";
            this.answerMarkB.Size = new System.Drawing.Size(60, 17);
            this.answerMarkB.TabIndex = 2;
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
            this.answerMarkA.TabIndex = 1;
            this.answerMarkA.TabStop = true;
            this.answerMarkA.Text = "Answer";
            this.answerMarkA.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.questionTextBox);
            this.panel3.Location = new System.Drawing.Point(12, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(445, 50);
            this.panel3.TabIndex = 12;
            // 
            // questionTextBox
            // 
            this.questionTextBox.Location = new System.Drawing.Point(6, 4);
            this.questionTextBox.Name = "questionTextBox";
            this.questionTextBox.Size = new System.Drawing.Size(436, 43);
            this.questionTextBox.TabIndex = 0;
            this.questionTextBox.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.textBox5);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(12, 274);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(445, 30);
            this.panel4.TabIndex = 13;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(193, 6);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(65, 17);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "Enabled";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(45, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button4);
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.textBox6);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.comboBox3);
            this.panel5.Controls.Add(this.comboBox2);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(12, 310);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(445, 89);
            this.panel5.TabIndex = 14;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(300, 40);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "test read";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(218, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "test write";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(84, 41);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(299, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(172, 11);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 16;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(45, 11);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "label4";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.explanationEnabled);
            this.panel6.Controls.Add(this.richTextBox1);
            this.panel6.Controls.Add(this.explanationLabel);
            this.panel6.Location = new System.Drawing.Point(12, 222);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(445, 46);
            this.panel6.TabIndex = 15;
            // 
            // explanationEnabled
            // 
            this.explanationEnabled.AutoSize = true;
            this.explanationEnabled.Location = new System.Drawing.Point(3, 26);
            this.explanationEnabled.Name = "explanationEnabled";
            this.explanationEnabled.Size = new System.Drawing.Size(65, 17);
            this.explanationEnabled.TabIndex = 22;
            this.explanationEnabled.Text = "Enabled";
            this.explanationEnabled.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(71, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(368, 40);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // explanationLabel
            // 
            this.explanationLabel.AutoSize = true;
            this.explanationLabel.Location = new System.Drawing.Point(3, 3);
            this.explanationLabel.Name = "explanationLabel";
            this.explanationLabel.Size = new System.Drawing.Size(62, 13);
            this.explanationLabel.TabIndex = 0;
            this.explanationLabel.Text = "Explanation";
            // 
            // DatabaseManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 411);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DatabaseManager";
            this.Text = "DatabaseManager";
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
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.RadioButton AnswerMarkD;
        private System.Windows.Forms.RadioButton AnswerMarkC;
        private System.Windows.Forms.RadioButton answerMarkB;
        private System.Windows.Forms.RadioButton answerMarkA;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox questionTextBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox explanationEnabled;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label explanationLabel;
    }
}
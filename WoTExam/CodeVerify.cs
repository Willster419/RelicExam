﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace WoTExam
{
    public partial class CodeVerify : Form
    {
        private int code;
        private int expectedOutput;
        private int output;
        public CodeVerify()
        {
            InitializeComponent();
            code = 0;
            expectedOutput = 0;
            output = 0;
        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            try
            {
                char delimiter = System.Convert.ToChar("-");
                string[] s = codeTextBox.Text.Split(delimiter);
                code = int.Parse(s[0]);
                expectedOutput = int.Parse(s[1]);
                expectedOutputTextBox.Text = "" + expectedOutput;
                output = code / 420;
                double doubleOutput = Math.Sqrt(output);
                output = (int)doubleOutput;
                percentCorrectLabel.Text = "" + output;
                if (output == expectedOutput)
                {
                    percentCorrectLabel.ForeColor = Color.Green;
                }
                else
                {
                    percentCorrectLabel.ForeColor = Color.Red;
                }
            }
            catch (FormatException f) { MessageBox.Show(f.Message); }
        }
    }
}

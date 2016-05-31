using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RelicExam
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
                code = int.Parse(codeTextBox.Text);
                expectedOutput = int.Parse(expectedOutputTextBox.Text);
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

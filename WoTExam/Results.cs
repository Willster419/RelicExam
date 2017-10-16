using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WoTExam
{
    public partial class Results : Form
    {
        private int percentCorrect;
        private int numCorrect;
        private int totalQuestions;
        private int verificationCode;
        public Results()
        {
            InitializeComponent();
        }

        public Results(int numKorrect, int totalQs)
        {
            numCorrect = numKorrect;
            totalQuestions = totalQs;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            //edit the gui and create the verification code
            numberCorrectLabel.Text = "" + numCorrect;
            totalQuestionsLabel.Text = "" + totalQuestions;
            double decimalNumCorrect = numCorrect;
            double decimalTotalQuestions = totalQuestions;
            double decimalCorrect = decimalNumCorrect / decimalTotalQuestions;
            decimalCorrect = decimalCorrect * 100;
            percentCorrect = (int)decimalCorrect;
            percentCorrectLabel.Text = "" + percentCorrect;
            verificationCode = percentCorrect * percentCorrect;
            verificationCode = verificationCode * 420;
            vCodeLabel.Text = "" + verificationCode + "-" + percentCorrect;
        }
    }
}

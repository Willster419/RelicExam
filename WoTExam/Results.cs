using System;
using System.Windows.Forms;

namespace WoTExam
{
    public partial class Results : Form
    {
        public int percentCorrect;
        public int numCorrect;
        public int totalQuestions;
        public int verificationCode;

        public Results()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            //edit the gui and create the verification code
            ResultsLabel.Text = "You scored " + numCorrect + " correct out of " + totalQuestions;
            double decimalNumCorrect = numCorrect;
            double decimalTotalQuestions = totalQuestions;
            double decimalCorrect = decimalNumCorrect / decimalTotalQuestions;
            decimalCorrect = decimalCorrect * 100;
            percentCorrect = (int)decimalCorrect;
            percentCorrectLabel.Text = "" + percentCorrect + " %";
            verificationCode = percentCorrect * percentCorrect;
            verificationCode = verificationCode * 420;
            vCodeLabel.Text = "" + verificationCode + "-" + percentCorrect;
        }
    }
}

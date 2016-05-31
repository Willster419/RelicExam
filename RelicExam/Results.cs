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
    public partial class Results : Form
    {
        private int average;
        private int numCorrect;
        private int totalQuestions;
        public Results()
        {
            InitializeComponent();
        }

        public Results(int numKorrect, int totalQs)
        {
            numCorrect = numKorrect;
            totalQuestions = totalQs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            //edit the gui and create the verification code
        }
    }
}

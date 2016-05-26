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
    public partial class EnterPassword : Form
    {
        //declared variables we will need
        public bool correct = false;
        private string password = "relic1";
        public int justClosing = 0;
        public EnterPassword()
        {
            InitializeComponent();
        }
        //event handler for if any key is pressed down
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if the key is enter
            if(e.KeyCode == Keys.Enter)
            {
                this.checkPassword(textBox1.Text);
                this.Hide();
            }
        }
        //same idea as method above, but handler is for methods
        private void button1_Click(object sender, EventArgs e)
        {
            this.checkPassword(textBox1.Text);
            this.Hide();
        }
        //the most secure method of checking passwords 2k16
        private void checkPassword(string enteredPassword)
        {
            justClosing++;
            if (enteredPassword.Equals(password))
            {
                correct = true;
                
            }
        }
        //event handler for when the form is loaded
        private void EnterPassword_Load(object sender, EventArgs e)
        {
            correct = false;
            justClosing = 0;
        }
    }
}

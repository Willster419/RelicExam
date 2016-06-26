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
        private string password;
        public EnterPassword()
        {
            InitializeComponent();
        }
        public EnterPassword(string pass)
        {
            InitializeComponent();
            password = pass;
        }
        //event handler for if any key is pressed down
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if the key is enter
            if (e.KeyCode == Keys.Enter && password == null)
            {
                //standard password asking
                this.Hide();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //password asking with feedback
                if (passwordTextBox.Text.Equals(password))
                {
                    //correct password
                    this.Hide();
                }
                else
                {
                    //invalid password
                    enterPasswordLabel.Text = "Incorect Password";
                }
            }
        }
        //same idea as method above, but handler is for methods
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EnterPassword_Load(object sender, EventArgs e)
        {
            passwordTextBox.Text = "";
            enterPasswordLabel.Text = "Please enter the service mode password";
        }
    }
}

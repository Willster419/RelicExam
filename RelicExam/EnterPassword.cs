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
                this.Hide();
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
        }
    }
}

using System;
using System.Windows.Forms;

namespace WoTExam
{
    public partial class EnterPassword : Form
    {
        public string password;
        public EnterPassword()
        {
            InitializeComponent();
        }
        //event handler for if any key is pressed down
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if the key is enter
            if (e.KeyCode == Keys.Enter)
            {
                if (ClanEditPassword.Text.Equals(password))
                    this.DialogResult = DialogResult.OK;
                else
                    this.Close();
            }
        }

        private void EnterPassword_Load(object sender, EventArgs e)
        {
            ClanEditPassword.Text = "";
            EnterClanEditHeader.Text = "Please enter the service mode password";
        }

        private void SubmitPassword_Click(object sender, EventArgs e)
        {
            if (ClanEditPassword.Text.Equals(password))
                this.DialogResult = DialogResult.OK;
            else
                this.Close();
        }
    }
}

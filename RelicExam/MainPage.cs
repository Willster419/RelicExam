//Declare all libraries we will be using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace RelicExam
{
    public partial class MainPage : Form
    {
        //All variables that will exist throughout the lifetime of the program
        private Results resultsViewer;
        private DatabaseManager dataBaseManager;
        private EnterPassword enterPassword;
        private QuestionViewer questionViewer;
        private PhotoViewer photoViewer;
        public MainPage()
        {
            InitializeComponent();
        }
        //more code for the best secure password checker
        private void ServiceModeButton_Click(object sender, EventArgs e)
        {
            enterPassword.ShowDialog();
            if (!enterPassword.correct && enterPassword.justClosing %2 == 1)
            {
                MessageBox.Show("Password Incorrect");
            }
            else if (!enterPassword.correct && enterPassword.justClosing % 2 == 0)
            {
                //do nothing, exept close
            }
            else
            {
                dataBaseManager.ShowDialog();
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            //make instances of all the sub forms withen this form
            resultsViewer = new Results();
            dataBaseManager = new DatabaseManager();
            enterPassword = new EnterPassword();
            questionViewer = new QuestionViewer();
            photoViewer = new PhotoViewer();
        }
    }
}

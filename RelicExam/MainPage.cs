//Declare all libraries we will be using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Microsoft.Win32;
using System.Threading;
using System.Xml;
using System.Collections;

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
        public XmlTextReader questionBaseReader;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionPath;
        public string questionBase;

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
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            questionBase = "questionBase.xml";

            //make instances of all the sub forms withen this form
            resultsViewer = new Results();
            dataBaseManager = new DatabaseManager();
            enterPassword = new EnterPassword();
            //new up the xml reader and stuff
            questionList = new List<Question>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            //read the catagories and maps
            if (!File.Exists(questionPath + "\\" + questionBase))
            {
                //BLANK DATABASE/FIRST RUN
            }
            else
            {
                //read question base
                while (questionBaseReader.Read())
                {
                    if (questionBaseReader.IsStartElement())
                    {
                        switch (questionBaseReader.Name)
                        {
                            case "catagory":
                                catagoryList.Add(new Catagory(questionBaseReader.ReadString()));
                                break;
                            case "map":
                                mapList.Add(new Map(questionBaseReader.ReadString()));
                                break;
                        }
                    }
                }
                questionBaseReader.Close();
            }
        }

        private void selectTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectTestType.SelectedIndex == 0)
            {
                specifyTypeBox.Show();
                label3.Show();
                label3.Text = "Specify Map";
                //parse the lists to the gui
                while (specifyTypeBox.Items.Count > 0)
                {
                    specifyTypeBox.Items.RemoveAt(0);
                }
                foreach (Map m in mapList)
                {
                    specifyTypeBox.Items.Add(m);
                }
            }
            else if (selectTestType.SelectedIndex == 1)
            {
                specifyTypeBox.Show();
                label3.Show();
                label3.Text = "Specify Catagory";
                //parse the lists to the gui
                while (specifyTypeBox.Items.Count > 0)
                {
                    specifyTypeBox.Items.RemoveAt(0);
                }
                foreach (Catagory c in catagoryList)
                {
                    specifyTypeBox.Items.Add(c);
                }
            }
            else if (selectTestType.SelectedIndex == 2)
            {
                label3.Hide();
                specifyTypeBox.Hide();
            }
            else { }
        }

        private void BeginTestButton_Click(object sender, EventArgs e)
        {
            int numQuestions;
            numQuestions = int.Parse(selectNumQuestions.SelectedItem.ToString());
            if (selectTestType.SelectedIndex == 0)
            {
                //MAP
                string selectedType = specifyTypeBox.SelectedItem.ToString();
                questionViewer = new QuestionViewer(numQuestions, null, selectedType);
                photoViewer = new PhotoViewer();
                questionViewer.Show();
            }
            else if (selectTestType.SelectedIndex == 1)
            {
               //CATAGORY
                string selectedType = specifyTypeBox.SelectedItem.ToString();
                questionViewer = new QuestionViewer(numQuestions,selectedType,null);
                photoViewer = new PhotoViewer();
                questionViewer.Show();
            }
            else if (selectTestType.SelectedIndex == 2)
            {
                //RANDOM
                questionViewer = new QuestionViewer(numQuestions,null,null);
                photoViewer = new PhotoViewer();
                questionViewer.Show();
            }
            else { }
        }
    }
}

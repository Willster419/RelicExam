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
using System.Diagnostics;
using AppLimit.CloudComputing.SharpBox;
using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;
using System.Web;


namespace RelicExam
{
    public partial class MainPage : Form
    {
        //All variables that will exist throughout the lifetime of the program
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
        private WebClient client = new WebClient();
        private string version = "Beta3";
        private PleaseWait wait;

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
                if (!dataBaseManager.close) dataBaseManager.ShowDialog();
                else { }
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            wait = new PleaseWait(10,1);
            Application.DoEvents();
            wait.Show();
            Application.DoEvents();

            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            questionBase = "questionBase.xml";
            
            //check if dependencies are downloaded
            if (!File.Exists(appPath + "\\AppLimit.CloudComputing.SharpBox.dll")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/AppLimit.CloudComputing.SharpBox.dll", appPath + "\\AppLimit.CloudComputing.SharpBox.dll");
            if (!File.Exists(appPath + "\\key.txt")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/key.txt", appPath + "\\key.txt");
            if (!File.Exists(appPath + "\\Newtonsoft.Json.Net40.dll")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Newtonsoft.Json.Net40.dll",appPath + "\\Newtonsoft.Json.Net40.dll");

            //check for updates
            Directory.CreateDirectory(dataBasePath);
            if (File.Exists(dataBasePath + "\\version.txt")) File.Delete(dataBasePath + "\\version.txt");
            client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/version.txt",dataBasePath + "\\version.txt");
            string tempVersion = File.ReadAllText(dataBasePath + "\\version.txt");
            if (!tempVersion.Equals(version))
            {
                DialogResult res = MessageBox.Show("New version found. Update now?", "Update", MessageBoxButtons.YesNo);
                if (res == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    try
                    {
                        client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/RelicExam.exe", appPath + "\\RelicExam_V" + tempVersion + ".exe");
                    }
                    catch (WebException w)
                    {
                        MessageBox.Show(w.Message);
                        return;
                    }

                    System.Diagnostics.Process.Start(appPath + "\\RelicExam_V" + tempVersion + ".exe");
                    this.Close();
                }
            }
            wait.setProgress(3);
            Application.DoEvents();
            //make instances of all the sub forms withen this form
            dataBaseManager = new DatabaseManager();
            enterPassword = new EnterPassword();
            //DEV CODE HERE
            if (!File.Exists(questionPath + "\\" + questionBase))
            {
                //database is blank
                DialogResult r = MessageBox.Show("Empty Database. Create Sample Database? If you select no, the\napplication will crash when loading a quiz", "Empty Database", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    DatabaseManager m = new DatabaseManager();
                    m.Show();
                    m.Hide();
                    m.createDataBase(true);
                    m.setupSampleXmlFiles();
                }
                else {  }
            }
            //END DEV CODE

            wait.setProgress(4);
            Application.DoEvents();
            
            //new up the xml reader and stuff
            questionList = new List<Question>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            wait.setProgress(5);
            Application.DoEvents();
            //Download the Latest QuestionBase


            wait.setProgress(9);
            Application.DoEvents();
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
            wait.setProgress(10);
            Application.DoEvents();
            wait.Close();
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
                specifyTypeBox.Items.Add(new Map("NONE"));
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
            if ((selectNumQuestions.SelectedIndex == -1 || selectTestType.SelectedIndex == -1) || ((2 > selectTestType.SelectedIndex && selectTestType.SelectedIndex > -1) && specifyTypeBox.SelectedIndex == -1))
            {
                MessageBox.Show("Please select all options");
                return;
            }
            int numQuestions;
            numQuestions = int.Parse(selectNumQuestions.SelectedItem.ToString());
            if (selectTestType.SelectedIndex == 0)
            {
                //MAP
                string selectedType = specifyTypeBox.SelectedItem.ToString();
                questionViewer = new QuestionViewer(numQuestions, null, selectedType);
                photoViewer = new PhotoViewer();
                questionViewer.ShowDialog();
            }
            else if (selectTestType.SelectedIndex == 1)
            {
               //CATAGORY
                string selectedType = specifyTypeBox.SelectedItem.ToString();
                questionViewer = new QuestionViewer(numQuestions,selectedType,null);
                photoViewer = new PhotoViewer();
                questionViewer.ShowDialog();
            }
            else if (selectTestType.SelectedIndex == 2)
            {
                //RANDOM
                questionViewer = new QuestionViewer(numQuestions,null,null);
                photoViewer = new PhotoViewer();
                questionViewer.ShowDialog();
            }
            else { }
        }
    }
}

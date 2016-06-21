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
        private WebClient client;
        private string version = "Beta3";
        private PleaseWait wait;

        public MainPage()
        {
            InitializeComponent();
        }
        //the most secure password checker ever
        private void ServiceModeButton_Click(object sender, EventArgs e)
        {
            enterPassword.ShowDialog();
            this.Hide();
            if (enterPassword.passwordTextBox.Text.Equals("relic1"))
            {
                if (enterPassword.overrideLockCheckBox.Checked)
                {
                    //create cloud storage instance
                    CloudStorage dropBoxStorage = new CloudStorage();
                    // get the configuration for dropbox
                    var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
                    // declare an access token
                    ICloudStorageAccessToken accessToken = null;
                    // load a valid security token from file
                    using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
                    }
                    // open the connection 
                    var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
                    // get a specific directory in the cloud storage, e.g. /Public 
                    //var questionsFolder = dropBoxStorage.GetFolder("/Public/RelicExam");
                    //String srcFile = Environment.ExpandEnvironmentVariables(null);
                    dropBoxStorage.DeleteFileSystemEntry("/Public/RelicExam/inUse.txt");
                    //dropBoxStorage.UploadFile(srcFile, questionsFolder);
                    dropBoxStorage.Close();
                    dataBaseManager.ShowDialog();
                }
                else
                {
                    dataBaseManager.ShowDialog();
                }
            }
            else
            {

            }
            this.Show();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            //TODO: Make multi-threaded version of the loader
            //TODO: Implement database version checking so it does not have to constantly download everything
            //TODO: Fix currentl xml implementation of questions
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            questionBase = "questionBase.xml";
            //new up required objects
            client = new WebClient();
            dataBaseManager = new DatabaseManager();
            enterPassword = new EnterPassword();
            questionList = new List<Question>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            wait = new PleaseWait();
            //show loading screen
            wait.Show();
            Application.DoEvents();
            //check if dependencies are downloaded
            if (!File.Exists(appPath + "\\AppLimit.CloudComputing.SharpBox.dll")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/AppLimit.CloudComputing.SharpBox.dll", appPath + "\\AppLimit.CloudComputing.SharpBox.dll");
            if (!File.Exists(appPath + "\\key.txt")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/key.txt", appPath + "\\key.txt");
            if (!File.Exists(appPath + "\\Newtonsoft.Json.Net40.dll")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Newtonsoft.Json.Net40.dll",appPath + "\\Newtonsoft.Json.Net40.dll");
            //check for blank database
            if (!Directory.Exists(dataBasePath)) Directory.CreateDirectory(dataBasePath);
            //check for updates
            if (File.Exists(dataBasePath + "\\version.txt")) File.Delete(dataBasePath + "\\version.txt");
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/version.txt", dataBasePath + "\\version.txt");
            }
            catch (WebException)
            {
                MessageBox.Show("ERROR 404: File not found");
                this.Close();
            }
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
                    catch (WebException)
                    {
                        MessageBox.Show("ERROR 404: File not found");
                        this.Close();
                    }

                    Process.Start(appPath + "\\RelicExam_V" + tempVersion + ".exe");
                    this.Close();
                }
            }
            if (!File.Exists(questionPath + "\\" + questionBase))
            {
                //database is blank
                //DEV CODE
                /*
                DialogResult r = MessageBox.Show("Empty Database. Create Sample Database? If you select no, the\napplication will crash when loading a quiz", "Empty Database", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    DatabaseManager m = new DatabaseManager();
                    m.Show();
                    m.Hide();
                    m.createDataBase(true);
                    m.setupSampleXmlFiles();
                }
                else {  }*/
                //END DEV CODE
            }
            wait.Close();
            //create a temp databaseManager to download the database
            DatabaseManager m = new DatabaseManager();
            m.parseFilePaths();
            //m.createDataBase(true);
            m.loadDataBase();
            m.Dispose();
            //read question base for maps and catagory only
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

        private void selectTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //parse the type of quiz requested
            if (selectTestType.SelectedIndex == 0)
            {
                //map
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
                //catagory
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
                //both
                label3.Hide();
                specifyTypeBox.Hide();
            }
            else { }
        }

        private void BeginTestButton_Click(object sender, EventArgs e)
        {
            if ((selectNumQuestions.SelectedIndex == -1 || selectTestType.SelectedIndex == -1) || ((2 > selectTestType.SelectedIndex && selectTestType.SelectedIndex > -1) && specifyTypeBox.SelectedIndex == -1))
            {
                //incorrect options selected
                MessageBox.Show("Please select all options");
                return;
            }
            int numQuestions = int.Parse(selectNumQuestions.SelectedItem.ToString());
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

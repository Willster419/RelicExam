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
using System.Security.Cryptography;


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
        private CodeVerify cv;
        private PleaseWait pw;
        private MD5 hash;

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
                    CloudStorage dropBoxStorage = new CloudStorage();
                    var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
                    ICloudStorageAccessToken accessToken = null;
                    using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
                    }
                    var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
                    dropBoxStorage.DeleteFileSystemEntry("/Public/RelicExam/inUse.txt");
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
        //called when the application has finished system loading
        private void MainPage_Load(object sender, EventArgs e)
        {
            this.Hide();
            pw = new PleaseWait(100, 0);
            //this.loadLiterallyEverything();
            //pw.Show();
            this.Hide();
            Application.DoEvents();
            mainPageDatabaseLoader.RunWorkerAsync();
            pw.ShowDialog();
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

        private void verifyCodeButton_Click(object sender, EventArgs e)
        {
            enterPassword.overrideLockCheckBox.Visible = false;
            enterPassword.ShowDialog();
            this.Hide();
            if (enterPassword.passwordTextBox.Text.Equals("relic1"))
            {
                cv.ShowDialog();
            }
            else
            {

            }
            enterPassword.overrideLockCheckBox.Visible = true;
            this.Show();
        }

        private void mainPageDatabaseLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            this.loadLiterallyEverything();
        }
        //reports an int of the progress level
        private void mainPageDatabaseLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pw.setProgress(e.ProgressPercentage);
        }
        //runs when the work is complete
        private void mainPageDatabaseLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pw.Hide();
            this.Show();
        }
        //self explanatory
        private void loadLiterallyEverything()
        {
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
            cv = new CodeVerify();
            //show loading screen (old)
            //wait.Show();
            //Application.DoEvents();
            mainPageDatabaseLoader.ReportProgress(10);
            //check if dependencies are downloaded
            if (!File.Exists(appPath + "\\AppLimit.CloudComputing.SharpBox.dll")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/AppLimit.CloudComputing.SharpBox.dll", appPath + "\\AppLimit.CloudComputing.SharpBox.dll");
            mainPageDatabaseLoader.ReportProgress(20);
            if (!File.Exists(appPath + "\\key.txt")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/key.txt", appPath + "\\key.txt");
            mainPageDatabaseLoader.ReportProgress(30);
            if (!File.Exists(appPath + "\\Newtonsoft.Json.Net40.dll")) client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Newtonsoft.Json.Net40.dll", appPath + "\\Newtonsoft.Json.Net40.dll");
            mainPageDatabaseLoader.ReportProgress(40);
            //check for blank database
            if (!Directory.Exists(dataBasePath)) Directory.CreateDirectory(dataBasePath);
            //check for updates to application
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
            mainPageDatabaseLoader.ReportProgress(50);
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
            //check for blank databse
            if (!File.Exists(questionPath + "\\" + questionBase))
            {
                //database is blank
                Directory.CreateDirectory(questionPath);
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
            //(old) close the waiting window cause loading is done
            //wait.Close();
            mainPageDatabaseLoader.ReportProgress(40);
            //determine if the database has been updated since last use
            client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questionBase.xml", questionPath + "\\tempQuestionBase.xml");
            hash = MD5.Create();
            string newHash = this.GetMd5Hash(hash,File.ReadAllText(questionPath + "\\tempQuestionBase.xml"));
            string oldHash = null;
            if (File.Exists(questionPath + "\\QuestionBase.xml"))
            {
                oldHash = this.GetMd5Hash(hash, File.ReadAllText(questionPath + "\\QuestionBase.xml"));
            }
            if (!newHash.Equals(oldHash))
            {
                //database has been updated or is blank, need to download new one
                this.downloadLatestDatabase();
            }
            if (File.Exists(questionPath + "\\tempQuestionBase.xml")) File.Delete(questionPath + "\\tempQuestionBase.xml");
            mainPageDatabaseLoader.ReportProgress(90);
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
            //set progress to 99 and sleep it so the progress bar updates for those with fancy themes
            mainPageDatabaseLoader.ReportProgress(99);
            Thread.Sleep(500);
            mainPageDatabaseLoader.ReportProgress(100);
        }
        //gets a string md5 hash checksum of the input string. in this case, the input string is the file
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        //self-explanatory
        private void downloadLatestDatabase()
        {
            //new up temp lists
            ArrayList questionReaderList = new ArrayList();
            XmlTextReader tempQuestionBaseReader;
            ArrayList pictureNameList = new ArrayList();
            string picturePath = dataBasePath + "\\pictures";
            //download latest questionBase
            if (File.Exists(questionPath + "\\" + questionBase)) File.Delete(questionPath + "\\" + questionBase);
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questionBase.xml", questionPath + "\\" + questionBase);
            }
            catch (WebException)
            {
                MessageBox.Show("ERROR 404: file not found");
                this.Close();
                return;
                //File.Move(questionPath + "\\questionBase.tmp",questionPath + "\\" + questionBase);
            }
            mainPageDatabaseLoader.ReportProgress(45);
            //then we can new up the xml readers
            tempQuestionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            List<string> pictureFileNameList = new List<string>();

            //read question base for picture and question names
            while (tempQuestionBaseReader.Read())
            {
                if (tempQuestionBaseReader.IsStartElement())
                {
                    switch (tempQuestionBaseReader.Name)
                    {
                        case "question":
                            questionReaderList.Add(tempQuestionBaseReader.ReadString());
                            break;
                        case "pictureFilePath":
                            pictureNameList.Add(tempQuestionBaseReader.ReadString());
                            break;
                    }
                }
            }
            tempQuestionBaseReader.Close();
            if (Directory.Exists(picturePath)) Directory.Delete(picturePath, true);
            Directory.CreateDirectory(picturePath);
            mainPageDatabaseLoader.ReportProgress(50);
            //download each picture. 50-70
            int tempProg = 50;
            foreach (String pp in pictureNameList)
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/pictures/" + pp, picturePath + "\\" + pp);
                tempProg = tempProg + 2;
                mainPageDatabaseLoader.ReportProgress(tempProg);
            }
            //download each question. 70-90
            tempProg = 70;
            foreach (String q in questionReaderList)
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/questions/" + q, questionPath + "\\" + q);
                tempProg = tempProg + 2;
                mainPageDatabaseLoader.ReportProgress(tempProg);
            }
            mainPageDatabaseLoader.ReportProgress(90);
        }
    }
}

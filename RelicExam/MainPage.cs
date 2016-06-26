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
        public XmlTextReader questionReader;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private List<Picture> pictureList;
        private List<Question> tempQuestionList;
        private List<Map> tempMapList;
        private List<Catagory> tempCatagoryList;
        private List<Picture> tempPictureList;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionBaseFullPath;
        public string questionBase;
        private string picturePath;
        private WebClient client;
        private string version = "Beta5.1";
        private PleaseWait wait;
        private CodeVerify cv;
        private PleaseWait pw;
        private MD5 hash;
        private Question tempQuestion;
        private bool exit;

        public MainPage()
        {
            InitializeComponent();
            exit = false;
        }
        //the most secure password checker ever
        private void ServiceModeButton_Click(object sender, EventArgs e)
        {
            //save the current database in memory
            tempCatagoryList = this.copyCatagoryList(catagoryList);
            tempMapList = this.copyMapList(mapList);
            tempQuestionList = this.copyQuestionList(questionList);
            tempPictureList = this.copyPictureList(pictureList);
            //remake the password form
            enterPassword = new EnterPassword("relic1");
            //change this to true to enable dev lock over-ride
            enterPassword.overrideLockCheckBox.Visible = true;
            enterPassword.ShowDialog();
            this.Hide();
            wait = new PleaseWait();
            wait.Show();
            Application.DoEvents();
            dataBaseManager = new DatabaseManager(questionList, mapList, catagoryList, pictureList);
            //redundant lol
            if (enterPassword.passwordTextBox.Text.Equals("relic1"))
            {
                //correct password
                if (enterPassword.overrideLockCheckBox.Checked)
                {
                    //and override the file lock
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
                    
                }
                else
                {
                    
                }
                wait.Close();
                dataBaseManager.ShowDialog();
                //pass up all the changes IF THERE ARE CHANGES
                if (dataBaseManager.discardedChanges)
                {
                    //put the origional lists back
                    this.questionList = this.copyQuestionList(tempQuestionList);
                    this.mapList = this.copyMapList(tempMapList);
                    this.catagoryList = this.copyCatagoryList(tempCatagoryList);
                    this.pictureList = this.copyPictureList(tempPictureList);
                }
                else
                {
                    //update these lists
                    this.questionList = this.copyQuestionList(dataBaseManager.questionList);
                    this.mapList = this.copyMapList(dataBaseManager.mapList);
                    this.catagoryList = this.copyCatagoryList(dataBaseManager.catagoryList);
                    this.pictureList = this.copyPictureList(dataBaseManager.pictureList);
                }
                //then close the form
                dataBaseManager.Close();
                dataBaseManager.Dispose();
                //this is cancer. do not use
                //Application.Restart();
            }
            else
            {
                //closed on incorrect password
                if (wait != null) wait.Close();
            }
            enterPassword.Close();
            this.Show();
        }
        //called when the application has finished system loading
        private void MainPage_Load(object sender, EventArgs e)
        {
            this.Text = "Reic Exam V " + version;
            pw = new PleaseWait(100, 0);
            //this.loadLiterallyEverything(); OLD single thread version
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
            this.Hide();
            //check if you have to refresh the database first
            client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questions.xml", dataBasePath + "\\tempQuestions.xml");
            hash = MD5.Create();
            string newHash = this.GetMd5Hash(hash,File.ReadAllText(dataBasePath + "\\tempQuestions.xml"));
            string oldHash = null;
            if (File.Exists(questionBaseFullPath))
            {
                oldHash = this.GetMd5Hash(hash, File.ReadAllText(questionBaseFullPath));
            }
            if (!newHash.Equals(oldHash))
            {
                //you need to refresh the database before you can continue
            }


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
                questionViewer = new QuestionViewer(numQuestions, null, selectedType,questionList,catagoryList,mapList);
                questionViewer.ShowDialog();
            }
            else if (selectTestType.SelectedIndex == 1)
            {
               //CATAGORY
                string selectedType = specifyTypeBox.SelectedItem.ToString();
                questionViewer = new QuestionViewer(numQuestions,selectedType,null,questionList,catagoryList,mapList);
                questionViewer.ShowDialog();
            }
            else if (selectTestType.SelectedIndex == 2)
            {
                //RANDOM
                questionViewer = new QuestionViewer(numQuestions,null,null,questionList,catagoryList,mapList);
                questionViewer.ShowDialog();
            }
            else { }
            this.Show();
        }

        private void verifyCodeButton_Click(object sender, EventArgs e)
        {
            enterPassword = new EnterPassword("relic1");
            enterPassword.overrideLockCheckBox.Visible = false;
            enterPassword.ShowDialog();
            this.Hide();
            if (enterPassword.passwordTextBox.Text.Equals("relic1"))
            {
                //correct password
                cv.ShowDialog();
            }
            else
            {
                //incorrect password
            }
            enterPassword.overrideLockCheckBox.Visible = true;
            enterPassword.Close();
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
            if (exit)
            {
                this.Close();
            }
            else
            {
                pw.Hide();
                this.Show();
            }
        }
        //self explanatory
        private void loadLiterallyEverything()
        {
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionBaseFullPath = dataBasePath + "\\questions.xml";
            questionBase = "questions.xml";
            picturePath = dataBasePath + "\\pictures";
            //new up required objects
            client = new WebClient();
            dataBaseManager = new DatabaseManager();
            questionList = new List<Question>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
            pictureList = new List<Picture>();
            questionReader = new XmlTextReader(questionBaseFullPath);
            wait = new PleaseWait();
            cv = new CodeVerify();
            tempQuestion = new Question();
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
                    //close the form
                    exit = true;
                    return;
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
            //(old) close the waiting window cause loading is done
            //wait.Close();
            mainPageDatabaseLoader.ReportProgress(40);
            //check if this is a new version or first time loading
            if (!File.Exists(dataBasePath + "\\testFile.txt"))
            {
                //first time load or new version first time load
                //delete the entire database, download the new one
                try
                {
                    Directory.Delete(dataBasePath, true);
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.Restart();
                }
                Directory.CreateDirectory(dataBasePath);
                File.WriteAllText(dataBasePath + "\\testFile.txt", "not the first time lol");
            }
            //determine if the database has been updated since last use
            client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questions.xml", dataBasePath + "\\tempQuestions.xml");
            hash = MD5.Create();
            string newHash = this.GetMd5Hash(hash,File.ReadAllText(dataBasePath + "\\tempQuestions.xml"));
            string oldHash = null;
            if (File.Exists(questionBaseFullPath))
            {
                oldHash = this.GetMd5Hash(hash, File.ReadAllText(questionBaseFullPath));
            }
            if (!newHash.Equals(oldHash))
            {
                if (!Directory.Exists(dataBasePath)) Directory.CreateDirectory(dataBasePath);
                //database has been updated or is blank, need to download new one
                this.downloadLatestDatabase();
            }
            if (File.Exists(dataBasePath + "\\tempQuestions.xml")) File.Delete(dataBasePath + "\\tempQuestions.xml");
            mainPageDatabaseLoader.ReportProgress(90);
            //NEW: read entire database to parse into other windows
            questionReader.Read();
            questionReader.Read();
            questionReader.Read();
            questionReader.Read();
            questionReader.Read();
            questionReader.Read();
            while (questionReader.Read())
            {
                if (questionReader.Name.Equals("question"))
                {
                    tempQuestion.p = new Picture();
                    while (questionReader.Read())
                    {
                        if (questionReader.IsStartElement())
                        {
                            switch (questionReader.Name)
                            {
                                //parse everything into the temp question, as well as making
                                //a list of all maps and catagories that arn't duplicate
                                case "title":
                                    tempQuestion.title = questionReader.ReadString();
                                    break;
                                case "catagory":
                                    tempQuestion.cat = new Catagory(questionReader.ReadString());
                                    this.addCatagoryIfNotDuplicate(tempQuestion.cat);
                                    break;
                                case "theQuestion":
                                    tempQuestion.theQuestion = questionReader.ReadString();
                                    break;
                                case "responseA":
                                    tempQuestion.responseA = questionReader.ReadString();
                                    break;
                                case "responseB":
                                    tempQuestion.responseB = questionReader.ReadString();
                                    break;
                                case "responseC":
                                    tempQuestion.responseC = questionReader.ReadString();
                                    break;
                                case "responseCEnabled":
                                    tempQuestion.responseCEnabled = Boolean.Parse(questionReader.ReadString());
                                    break;
                                case "responseD":
                                    tempQuestion.responseD = questionReader.ReadString();
                                    break;
                                case "responseDEnabled":
                                    tempQuestion.responseDEnabled = Boolean.Parse(questionReader.ReadString());
                                    break;
                                case "answer":
                                    tempQuestion.answer = questionReader.ReadString();
                                    break;
                                case "timeToAnswer":
                                    tempQuestion.timeToAnswer = int.Parse(questionReader.ReadString());
                                    break;
                                case "explanationOfAnswer":
                                    tempQuestion.explanationOfAnswer = questionReader.ReadString();
                                    break;
                                case "map":
                                    tempQuestion.m = new Map(questionReader.ReadString());
                                    this.addMapIfNotDuplicate(tempQuestion.m);
                                    break;
                                case "picture":
                                    tempQuestion.p.photoFileName = questionReader.ReadString();
                                    break;
                                    //pictureName HAS to be the last thing read from each question node for this to work
                                case "pictureName":
                                    tempQuestion.p.photoTitle = questionReader.ReadString();
                                    this.addPictureIfNotDuplicate(tempQuestion.p);
                                    break;
                            }
                        }
                        if (questionReader.Name.Equals("pictureName"))
                        {
                            //add the question to memory and reset the temp question
                            questionList.Add(tempQuestion);
                            tempQuestion = new Question();
                            break;
                        }
                    }
                }
            }
            questionReader.Close();
            mainPageDatabaseLoader.ReportProgress(50);
            //re-download all pictures if we have to
            if (!newHash.Equals(oldHash)) this.downloadPictures();
            //set progress to 99 and sleep it so the progress bar updates for those with fancy themes
            mainPageDatabaseLoader.ReportProgress(99);
            Thread.Sleep(500);
            mainPageDatabaseLoader.ReportProgress(100);
            //by the end of this, we now have loaded all question catagory and map lists into memory for other windows
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
        //downloads latest questionbase
        private void downloadLatestDatabase()
        {
            //download latest questionBase
            if (File.Exists(questionBaseFullPath)) File.Delete(questionBaseFullPath);
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questions.xml", questionBaseFullPath);
            }
            catch (WebException)
            {
                MessageBox.Show("ERROR 404: file not found");
                this.Close();
                return;
            }
            mainPageDatabaseLoader.ReportProgress(45);
        }
        //self-explanatory
        private void addMapIfNotDuplicate(Map mm)
        {
            //first check for a NONE map
            if (mm.getMap().Equals("NONE")) return;
            //traverse the array, if it does not find another it will not break
            //therefore adding it to the list
            foreach (Map m in mapList)
            {
                if (m.getMap().Equals(mm.getMap())) return;
            }
            mapList.Add(mm);
        }
        //self-explanatory
        private void addCatagoryIfNotDuplicate(Catagory cc)
        {
            //traverse the array, if it does not find another it will not break
            //therefore adding it to the list
            foreach (Catagory c in catagoryList)
            {
                if (c.getCatagory().Equals(cc.getCatagory())) return;
            }
            catagoryList.Add(cc);
        }
        //self-explanatory
        private void addPictureIfNotDuplicate(Picture pp)
        {
            if (pp.photoFileName.Equals("NONE") || pp.photoFileName.Equals("null.jpg")) return;
            //traverse the array, if it does not find another it will not break
            //therefore adding it to the list
            foreach (Picture p in pictureList)
            {
                if (p.photoFileName.Equals(pp.photoFileName)) return;
            }
            pictureList.Add(pp);
        }
        //deletes the local database and downloads the new one from the server
        private void refreshDatabase_Click(object sender, EventArgs e)
        {
            //delete the entire database, download the new one
            try
            {
                Directory.Delete(dataBasePath, true);
            }
            catch (IOException)
            {
                MessageBox.Show("Failed to delete folder because of some bullshit io error. please try again");
                return;
            }
            this.MainPage_Load(null, null);
        }
        //re-downloads all the pictures
        private void downloadPictures()
        {
            if(Directory.Exists(picturePath)) Directory.Delete(picturePath, true);
            Directory.CreateDirectory(picturePath);
            mainPageDatabaseLoader.ReportProgress(50);
            //download each picture. 50-90
            int tempProg = 50;
            foreach (Question q in questionList)
            {
                if (q.p.photoFileName.Equals("NONE") || q.p.photoFileName.Equals("null.jpg"))
                {
                    tempProg = tempProg + 2;
                    mainPageDatabaseLoader.ReportProgress(tempProg);
                }
                else
                {
                    client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/pictures/" + q.p.photoFileName, picturePath + "\\" + q.p.photoFileName);
                    tempProg = tempProg + 2;
                    mainPageDatabaseLoader.ReportProgress(tempProg);
                }
            }
            mainPageDatabaseLoader.ReportProgress(90);
        }
        //copies every object to another list
        private List<Question> copyQuestionList(List<Question> l)
        {
            List<Question> newList = new List<Question>();
            for (int i = 0; i < l.Count; i++)
            {
                Question temp = new Question();
                Catagory tempCat = new Catagory();
                Map tempMap = new Map();
                Picture tempPic = new Picture();
                temp.title = l[i].title;
                tempCat.setCatagory(l[i].cat.getCatagory());
                temp.cat = tempCat;
                temp.theQuestion = l[i].theQuestion;
                temp.responseA = l[i].responseA;
                temp.responseB = l[i].responseB;
                temp.responseC = l[i].responseC;
                temp.responseCEnabled = l[i].responseCEnabled;
                temp.responseD = l[i].responseD;
                temp.responseDEnabled = l[i].responseDEnabled;
                temp.answer = l[i].answer;
                temp.timeToAnswer = l[i].timeToAnswer;
                temp.explanationOfAnswer = l[i].explanationOfAnswer;
                tempMap.setMap(l[i].m.getMap());
                temp.m = tempMap;
                tempPic.photoFileName = l[i].p.photoFileName;
                tempPic.photoTitle = l[i].p.photoTitle;
                temp.p = tempPic;
                newList.Add(temp);
            }
            return newList;
        }

        private List<Catagory> copyCatagoryList(List<Catagory> l)
        {
            List<Catagory> newList = new List<Catagory>();
            for (int i = 0; i < l.Count; i++)
            {
                Catagory temp = new Catagory();
                temp.setCatagory(l[i].getCatagory());
                newList.Add(temp);
            }
            return newList;
        }

        private List<Map> copyMapList(List<Map> l)
        {
            List<Map> newList = new List<Map>();
            for (int i = 0; i < l.Count; i++)
            {
                Map temp = new Map(l[i].getMap());
                newList.Add(temp);
            }
            return newList;
        }

        private List<Picture> copyPictureList(List<Picture> l)
        {
            List<Picture> newList = new List<Picture>();
            for (int i = 0; i < l.Count; i++)
            {
                Picture temp = new Picture();
                temp.photoFileName = l[i].photoFileName;
                temp.photoTitle = l[i].photoTitle;
                newList.Add(temp);
            }
            return newList;
        }
    }
}

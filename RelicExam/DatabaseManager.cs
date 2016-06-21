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
    public partial class DatabaseManager : Form
    {
        public XmlTextWriter questionBaseWriter;
        public XmlTextReader questionBaseReader;
        public XmlTextWriter playerBaseWriter;
        public XmlTextReader playerBaseReader;
        public XmlTextWriter playerWriter;
        public XmlTextReader playerReader;
        public XmlTextWriter questionWriter;
        public XmlTextReader questionReader;
        private Question tempQuestion;
        private Player tempPlayer;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionPath;
        public string playerPath;
        public string questionBase;
        public string playerBase;
        private PleaseWait wait;
        private string[] sampleCatagories;
        private string[] sampleMaps;
        private ArrayList questionReaderList;
        private ArrayList playerReaderList;
        private string questionPrefix = "question";
        private int questionNumber = 0;
        private string xmlExtension = ".xml";
        private string playerPrefix = "player";
        private int playerNumber = 0;
        private List<Player> playerList;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private int lastNumber;
        private bool hasMadeChanges;
        private CloudStorage dropBoxStorage;
        private WebClient client = new WebClient();
        public bool close;
        private string pictureName;
        private PhotoViewer chooser;
        private List<Picture> pictureList;
        private Point pictureSpawnPoint;
        private string picturePath;
        private bool actuallyLoad;

        public DatabaseManager()
        {
            InitializeComponent();
        }

        private void DatabaseManager_Load(object sender, EventArgs e)
        {
            //some boolean logic hack i don't know
            actuallyLoad = false;
            hasMadeChanges = false;
            //display a loading window and load the database
            wait = new PleaseWait();
            wait.Show();
            Application.DoEvents();
            //parse all file paths
            this.parseFilePaths();
            //declare all temp objects
            tempQuestion = new Question();
            tempPlayer = new Player();
            //check for another user on the system
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/inUse.txt", tempPath + "\\inUse.txt");
                MessageBox.Show("Database is currently being edited, try again later");
                //this.Close();
                wait.Close();
                close = true;
            }
            catch (WebException)
            {
                //if none, you are the user now
                //make a random file to use a single instance file lock
                File.WriteAllText(tempPath + "\\inUse.txt", "the service is in use");
                dropBoxStorage = new CloudStorage();
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
                var questionsFolder = dropBoxStorage.GetFolder("/Public/RelicExam");
                String srcFile = Environment.ExpandEnvironmentVariables(tempPath + "\\inUse.txt");
                dropBoxStorage.UploadFile(srcFile, questionsFolder);
                dropBoxStorage.Close();
            }
            if (close)
            {
                this.Close();
                return;
            }
            //System.Threading.Thread.Sleep(100);
            this.answerCEnable_CheckedChanged(null, null);
            this.answerDEnable_CheckedChanged(null, null);
            currentModeLabel.Visible = false;
            removeButton.Enabled = false;
            //determine if the database has been updated since last use
            /*client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questionBase.xml", questionPath + "\\tempQuestionBase.xml");
            hash = MD5.Create();
            string newHash = this.GetMd5Hash(hash, File.ReadAllText(questionPath + "\\tempQuestionBase.xml"));
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
            if (File.Exists(questionPath + "\\tempQuestionBase.xml")) File.Delete(questionPath + "\\tempQuestionBase.xml");*/
            //all done checking/updating the database
            wait.Close();
            //update gui
            this.loadDataBase(true);
            this.resetGUI();
            
        }

        public void loadDataBase(bool displayProgress)
        {
            //new up everything
            tempQuestion = new Question();
            tempPlayer = new Player();
            questionList = new List<Question>();
            playerList = new List<Player>();
            mapList = new List<Map>();
            pictureList = new List<Picture>();
            catagoryList = new List<Catagory>();
            playerReaderList = new ArrayList();
            questionReaderList = new ArrayList();

            //download latest questionBase
            if (File.Exists(questionPath + "\\" + questionBase)) File.Delete(questionPath + "\\" + questionBase);
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/questionBase.xml", questionPath + "\\" + questionBase);
            }
            catch (WebException)
            {
                MessageBox.Show("404 nooooooooo");
                this.Close();
                return;
                //File.Move(questionPath + "\\questionBase.tmp",questionPath + "\\" + questionBase);
            }

            //then we can new up the xml readers
            playerBaseReader = new XmlTextReader(playerPath + "\\" + playerBase);
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            List<string> pictureFileNameList = new List<string>();

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
                        case "question":
                            questionReaderList.Add(questionBaseReader.ReadString());
                            break;
                        case "picture":
                            pictureList.Add(new Picture(questionBaseReader.ReadString()));
                            break;
                        case "pictureFilePath":
                            pictureFileNameList.Add(questionBaseReader.ReadString());
                            break;
                    }
                }
            }
            questionBaseReader.Close();

            //parse fileNames
            for (int i = 0; i < pictureList.Count; i++)
            {
                string fileName = Path.GetFileName(pictureFileNameList[i]);
                pictureList[i].photoFileName = picturePath + "\\" + fileName;
            }

            //create the loading window
            if(displayProgress) wait = new PleaseWait(questionReaderList.Count+pictureList.Count, 0);
            if(displayProgress) wait.Show();
            int progg = 0;
            //read player base
            /*while (playerBaseReader.Read())
            {
                if (playerBaseReader.IsStartElement())
                {
                    switch (playerBaseReader.Name)
                    {
                        case "player":
                            playerReaderList.Add(playerBaseReader.ReadString());
                            break;
                    }
                }
            }
            playerBaseReader.Close();
            //parse players (TODO)
            //playerReader.Close();*/

            //download pictures
            //System.Threading.Thread.Sleep(100);
            if (chooser != null) chooser.Close();
            if (Directory.Exists(picturePath)) Directory.Delete(picturePath,true);
            Directory.CreateDirectory(picturePath);
            foreach (Picture pp in pictureList)
            {
                if(displayProgress) wait.setProgress(progg++);
                String fileName = Path.GetFileName(pp.photoFileName);
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/pictures/" + fileName, picturePath + "\\" + fileName);
            }

            //parse questions
            foreach (string q in questionReaderList)
            {
                if(displayProgress) wait.setProgress(progg++);
                //download them first tho
                if (File.Exists(questionPath + "\\" + q)) File.Delete(questionPath + "\\" + q);
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/Questions/" + q, questionPath + "\\" + q);
                questionReader = new XmlTextReader(questionPath + "\\" + q);
                tempQuestion = new Question();
                while (questionReader.Read())
                {
                    if (questionReader.IsStartElement())
                    {
                        switch (questionReader.Name)
                        {
                            //parse everything
                            case "title":
                                tempQuestion.title = questionReader.ReadString();
                                break;
                            case "catagory":
                                tempQuestion.cat.setCatagory(questionReader.ReadString());
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
                                tempQuestion.m.setMap(questionReader.ReadString());
                                break;
                            case "picture":
                                string result = questionReader.ReadString();
                                if (result.Equals("NONE") || result.Equals(""))
                                {
                                    tempQuestion.p = new Picture("NONE", "null.jpg");
                                }
                                else
                                {
                                    tempQuestion.p = pictureList[getPicture(result)-1];
                                }
                                break;
                        }
                    }
                }
                questionList.Add(tempQuestion);
                questionReader.Close();
            }
            if(displayProgress)wait.Close();
        }

        private void resetGUI()
        {
            questionComboBox.SelectedIndex = -1;
            mapComboBox.SelectedIndex = -1;
            catagoryComboBox.SelectedIndex = -1;
            while (questionComboBox.Items.Count > 1)
            {
                questionComboBox.Items.RemoveAt(1);
            }
            while (mapComboBox.Items.Count > 1)
            {
                mapComboBox.Items.RemoveAt(1);
            }
            while (catagoryComboBox.Items.Count != 0)
            {
                catagoryComboBox.Items.RemoveAt(0);
            }
            foreach (Question q in questionList)
            {
                questionComboBox.Items.Add(q);
            }
            foreach (Map m in mapList)
            {
                mapComboBox.Items.Add(m);
            }
            foreach (Catagory c in catagoryList)
            {
                catagoryComboBox.Items.Add(c);
            }
            while (photoComboBox.Items.Count > 1)
            {
                photoComboBox.Items.RemoveAt(1);
            }
            foreach (Picture pp in pictureList)
            {
                photoComboBox.Items.Add(pp);
            }
            questionTextBox.Text = "";
            responseATextBox.Text = "";
            responseBTextBox.Text = "";
            answerCEnable.Checked = false;
            responseCTextBox.Text = "";
            answerDEnable.Checked = false;
            responseDTextBox.Text = "";
            answerMarkA.Checked = false;
            answerMarkB.Checked = false;
            answerMarkC.Checked = false;
            answerMarkD.Checked = false;
            expTextBox.Text = "";
            theQuestionTitle.Text = "";
            timeToAnswerTextBox.Text = "" + "";
            currentModeLabel.Visible = false;
            this.photoComboBox_SelectedIndexChanged(null, null);
        }

        private void answerCEnable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void answerDEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (answerMarkD.Checked)
            {
                MessageBox.Show("Cannot Disable the Answer Question!!");
                answerDEnable.Checked = true;
                return;
            }
            if (answerDEnable.Checked)
            {
                responseDTextBox.ReadOnly = false;
                answerMarkD.Enabled = true;
            }
            else
            {
                responseDTextBox.ReadOnly = true;
                answerMarkD.Enabled = false;
            }
        }

        public void createDataBase(bool cleanSweep)
        {
            if (cleanSweep)
            {
                try
                {
                    if (Directory.Exists(dataBasePath)) Directory.Delete(dataBasePath, true);
                }
                catch (IOException)
                {
                    MessageBox.Show("get out of my database");
                }
                Directory.CreateDirectory(dataBasePath);
                Directory.CreateDirectory(questionPath);
                Directory.CreateDirectory(playerPath);
                Directory.CreateDirectory(picturePath);
            }
            else
            {
                try
                {
                    Directory.Delete(playerPath, true);
                    Directory.Delete(questionPath, true);
                    Directory.CreateDirectory(playerPath);
                    Directory.CreateDirectory(questionPath);
                }
                catch (IOException)
                {
                    MessageBox.Show("get out of my database");
                }
            }
        }

        public void setupSampleXmlFiles()
        {
            //setup sample questionBase arrayLists
            sampleCatagories = new string[3];
            sampleCatagories[0] = "How2Pen";
            sampleCatagories[1] = "Command";
            sampleCatagories[2] = "Calling";
            sampleMaps = new string[3];
            sampleMaps[0] = "Campinovka";
            sampleMaps[1] = "Derpinberg";
            sampleMaps[2] = "Kittenguard";
            questionNumber = 0;
            playerNumber = 0;
            //each method is self-explanatory...
            this.setupSampleQuestionBase();
            this.setupSamplePlayerBase();
            this.setupSamplePlayers();
            this.setupSampleQuestions();
        }

        private void setupSampleQuestionBase()
        {
            //File.CreateText(questionPath + "\\" + questionBase);
            //File.AppendAllText(questionPath + "\\" + questionBase, "fuck this");
            //stream = new FileStream(questionPath + "\\" + questionBase, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            questionBaseWriter = new XmlTextWriter(questionPath + "\\" + questionBase, Encoding.UTF8);
            questionBaseWriter.Formatting = Formatting.Indented;

            questionBaseWriter.WriteStartDocument();
            questionBaseWriter.WriteStartElement("QuestionBase.xml");

            questionBaseWriter.WriteComment("Catagories List");
            questionBaseWriter.WriteStartElement("catagories");
            questionBaseWriter.WriteElementString("catagory", sampleCatagories[0]);
            questionBaseWriter.WriteElementString("catagory", sampleCatagories[1]);
            questionBaseWriter.WriteElementString("catagory", sampleCatagories[2]);
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteComment("Maps List");
            questionBaseWriter.WriteStartElement("maps");
            questionBaseWriter.WriteElementString("map", sampleMaps[0]);
            questionBaseWriter.WriteElementString("map", sampleMaps[1]);
            questionBaseWriter.WriteElementString("map", sampleMaps[2]);
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteComment("Questions");
            questionBaseWriter.WriteStartElement("questions");
            questionBaseWriter.WriteElementString(questionPrefix, questionPrefix + questionNumber++ + xmlExtension);
            questionBaseWriter.WriteElementString(questionPrefix, questionPrefix + questionNumber++ + xmlExtension);
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteEndElement();
            questionBaseWriter.Close();
            //stream.Unlock(0,4);
            //stream.Close();
        }

        private void setupSamplePlayers()
        {
            //new everything
            Player player0 = new Player();
            Player player1 = new Player();
            Player[] samplePlayerList = new Player[2];

            //describe each player like it was from an xml file
            player0.name = "Willster419";
            player0.totalTimesPlayed = 1;
            player0.percentCorrent = 100;
            player0.lastPercentCorrect = -1;
            player0.totalSessionQuestions = 10;
            player0.lastTotalSessionQuestions = -1;

            player1.name = "TechnoD";
            player1.totalTimesPlayed = 420;
            player1.percentCorrent = 69;
            player1.lastPercentCorrect = -1;
            player1.totalSessionQuestions = 20;
            player1.lastTotalSessionQuestions = -1;

            //add them to the list
            samplePlayerList[0] = player0;
            samplePlayerList[1] = player1;

            //write each player from the list
            for (int i = 0; i < samplePlayerList.Count(); i++)
            {
                playerWriter = new XmlTextWriter(playerPath + "\\" + playerPrefix + i + xmlExtension, Encoding.UTF8);
                playerWriter.Formatting = Formatting.Indented;

                playerWriter.WriteStartDocument();
                playerWriter.WriteStartElement(playerPrefix + i + xmlExtension);
                playerWriter.WriteStartElement("player");
                playerWriter.WriteElementString("name", samplePlayerList[i].name);
                playerWriter.WriteElementString("totalTimesPlayed", "" + samplePlayerList[i].totalTimesPlayed);
                playerWriter.WriteElementString("percentCorrect", "" + samplePlayerList[i].percentCorrent);
                playerWriter.WriteElementString("lastPercentCorrent", "" + samplePlayerList[i].lastPercentCorrect);
                playerWriter.WriteElementString("totalSessionQuestions", "" + samplePlayerList[i].totalSessionQuestions);
                playerWriter.WriteElementString("lastTotalSessionQuestions", "" + samplePlayerList[i].lastTotalSessionQuestions);
                playerWriter.WriteEndElement();
                playerWriter.WriteEndElement();
                playerWriter.Close();
            }
        }

        private void setupSamplePlayerBase()
        {
            playerBaseWriter = new XmlTextWriter(playerPath + "\\" + playerBase, Encoding.UTF8);
            playerBaseWriter.Formatting = Formatting.Indented;

            playerBaseWriter.WriteStartDocument();
            playerBaseWriter.WriteStartElement("playerBase.xml");

            playerBaseWriter.WriteComment("Player List");
            playerBaseWriter.WriteStartElement("players");
            playerBaseWriter.WriteElementString(playerPrefix, playerPrefix + playerNumber++ + xmlExtension);
            playerBaseWriter.WriteElementString(playerPrefix, playerPrefix + playerNumber++ + xmlExtension);
            playerBaseWriter.WriteEndElement();

            playerBaseWriter.WriteEndElement();
            playerBaseWriter.Close();
        }

        private void setupSampleQuestions()
        {
            //new everyting
            Question question0 = new Question();
            Question question1 = new Question();
            Question[] sampleQuestionList = new Question[2];

            //questions
            question0.title = "Sample Question 1";
            question0.cat.setCatagory(sampleCatagories[0]);
            question0.theQuestion = "This is the Sample Question";
            question0.responseA = "response a";
            question0.responseB = "response b";
            question0.responseC = "response c";
            question0.responseCEnabled = true;
            question0.responseD = "response d";
            question0.responseDEnabled = true;
            question0.answer = "c";
            question0.timeToAnswer = 20;
            question0.explanationOfAnswer = "explanation";
            question0.m.setMap(sampleMaps[0]);

            question1.title = "Sample Question 2";
            question1.cat.setCatagory(sampleCatagories[1]);
            question1.theQuestion = "This is the Sample Question2";
            question1.responseA = "response a2";
            question1.responseB = "response b2";
            question1.responseC = "response c2";
            question1.responseCEnabled = true;
            question1.responseD = "response d2";
            question1.responseDEnabled = true;
            question1.answer = "b";
            question1.timeToAnswer = 20;
            question1.explanationOfAnswer = "explanation2";
            question1.m.setMap(sampleMaps[1]);

            //add to list
            sampleQuestionList[0] = question0;
            sampleQuestionList[1] = question1;

            //write each question from the list
            for (int i = 0; i < sampleQuestionList.Count(); i++)
            {
                questionWriter = new XmlTextWriter(questionPath + "\\" + questionPrefix + i + xmlExtension, Encoding.UTF8);
                questionWriter.Formatting = Formatting.Indented;

                questionWriter.WriteStartDocument();
                questionWriter.WriteStartElement(questionPrefix + i + xmlExtension);
                questionWriter.WriteStartElement("question");
                questionWriter.WriteElementString("title", sampleQuestionList[i].title);
                questionWriter.WriteElementString("catagory", sampleQuestionList[i].cat.getCatagory());
                questionWriter.WriteElementString("theQuestion", sampleQuestionList[i].theQuestion);
                questionWriter.WriteElementString("responseA", sampleQuestionList[i].responseA);
                questionWriter.WriteElementString("responseB", sampleQuestionList[i].responseB);
                questionWriter.WriteElementString("responseC", sampleQuestionList[i].responseC);
                questionWriter.WriteElementString("responseCEnabled", "" + sampleQuestionList[i].responseCEnabled);
                questionWriter.WriteElementString("responseD", sampleQuestionList[i].responseD);
                questionWriter.WriteElementString("responseDEnabled", "" + sampleQuestionList[i].responseDEnabled);
                questionWriter.WriteElementString("answer", sampleQuestionList[i].answer);
                questionWriter.WriteElementString("timeToAnswer", "" + sampleQuestionList[i].timeToAnswer);
                questionWriter.WriteElementString("explanationOfAnswer", sampleQuestionList[i].explanationOfAnswer);
                questionWriter.WriteElementString("map", sampleQuestionList[i].m.getMap());
                questionWriter.WriteEndElement();
                questionWriter.WriteEndElement();
                questionWriter.Close();
            }
        }
        //OLD! DO NOT USE!
        private void button3_Click(object sender, EventArgs e)
        {
            this.createDataBase(true);
            this.setupSampleXmlFiles();
            button1.Enabled = false;
        }
        //OLD! DO NOT USE!
        private void button4_Click(object sender, EventArgs e)
        {
            //check if the database is blank
            if (!File.Exists(questionPath + "\\" + questionBase))
            {
                MessageBox.Show("Database is blank");
                resetForm_Click(null, null);
                return;
            }
            this.loadDataBase(true);
            this.resetGUI();
        }

        private void questionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chooser != null)chooser.Close();
            actuallyLoad = true;
            addPictureButton.Enabled = true;
            pictureName = "";
            saveButton.Enabled = true;
            if (questionComboBox.SelectedIndex == -1) return;
            //determine if a new question is being made or if one is being updated
            if (questionComboBox.SelectedIndex == 0)
            {
                //warn the user this is "new question mode"
                currentModeLabel.Text = "CREATE";
                currentModeLabel.Visible = true;
                saveButton.Text = "create";
                removeButton.Enabled = false;
                photoComboBox.SelectedIndex = -1;
            }
            else
            {
                //warn the user this is "update question mode"
                mapComboBox.SelectedIndex = 0;
                currentModeLabel.Text = "UPDATE";
                currentModeLabel.Visible = true;
                saveButton.Text = "update";
                removeButton.Enabled = true;
                //load the selected question
                int questionIndex = questionComboBox.SelectedIndex;
                questionIndex--;
                Question q2Load = questionList[questionIndex];
                questionTextBox.Text = q2Load.theQuestion;
                responseATextBox.Text = q2Load.responseA;
                responseBTextBox.Text = q2Load.responseB;
                if (q2Load.responseCEnabled)
                {
                    answerCEnable.Checked = q2Load.responseCEnabled;
                    responseCTextBox.Text = q2Load.responseC;
                }
                if (q2Load.responseDEnabled)
                {
                    answerDEnable.Checked = q2Load.responseDEnabled;
                    responseDTextBox.Text = q2Load.responseD;
                }
                if (q2Load.answer.Equals("a"))
                {
                    answerMarkA.Checked = true;
                }
                if (q2Load.answer.Equals("b"))
                {
                    answerMarkB.Checked = true;
                }
                if (q2Load.answer.Equals("c"))
                {
                    answerMarkC.Checked = true;
                }
                if (q2Load.answer.Equals("d"))
                {
                    answerMarkD.Checked = true;
                }
                expTextBox.Text = q2Load.explanationOfAnswer;
                theQuestionTitle.Text = q2Load.title;
                timeToAnswerTextBox.Text = "" + q2Load.timeToAnswer;
                string questionCatagoryTemp = q2Load.cat.getCatagory();
                string questionMapTemp = q2Load.m.getMap();
                //for catagory and map, find the match in the List using for loop
                //use that int as the index set for the comboBox
                for (int i = 0; i < catagoryList.Count; i++)
                {
                    if (questionCatagoryTemp.Equals(catagoryList[i].getCatagory()))
                    {
                        catagoryComboBox.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < mapList.Count; i++)
                {
                    if (questionMapTemp.Equals(mapList[i].getMap()))
                    {
                        mapComboBox.SelectedIndex = ++i;
                        break;
                    }
                    //if it gets to here it means that the map is NONE
                    mapComboBox.SelectedIndex = 0;
                }
                //get the picture selected
                string tempPictureName = q2Load.p.photoTitle;
                if (tempPictureName == null)
                {
                    photoComboBox.SelectedIndex = 0;
                    return;
                }
                if (tempPictureName.Equals("NONE") && tempPictureName !=null )
                {
                    photoComboBox.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0; i < pictureList.Count; i++)
                    {
                        if (tempPictureName.Equals(pictureList[i].photoTitle))
                        {
                            photoComboBox.SelectedIndex = ++i;
                            break;
                        }
                    }
                }
                
            }
            photoComboBox_SelectedIndexChanged(null, null);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////
            //(seperate)
            //determine if the user is making a new question or updating one.
            //ask if they are sure
            //process the add/update request in memory
            //(same)
            //reflect the update in disk database (delete and re-create)
            //reload the entire database to memory
            //upload to dropbox
            //update the gui
            //////////////////////////////////////////////////////////////////
            //but first make sure everything is selected
            if (theQuestionTitle.Text.Equals("") || questionTextBox.Text.Equals("") || responseATextBox.Text.Equals("") || responseBTextBox.Text.Equals("") || timeToAnswerTextBox.Text.Equals("0") || (answerCEnable.Checked && responseCTextBox.Text.Equals("")) || (answerDEnable.Checked && responseDTextBox.Text.Equals("")) || (catagoryComboBox.Text.Equals("") && catagoryComboBox.SelectedIndex == -1) || (mapComboBox.Text.Equals("") && mapComboBox.SelectedIndex == -1) || (!answerMarkA.Checked && !answerMarkB.Checked && !answerMarkC.Checked && !answerMarkD.Checked) || photoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("please make sure everything is filled out properly before continuing!");
                return;
            }
            if (questionComboBox.SelectedIndex == 0)
            {
                //ask if the user is sure they would like to create this question
                DialogResult result = MessageBox.Show("Are you sure you would like to create this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    chooser.Close();
                    hasMadeChanges = true;
                    //procede...
                    //create a new question class with everything from the GUI
                    Question newQ = new Question();
                    newQ.theQuestion = questionTextBox.Text;
                    newQ.responseA = responseATextBox.Text;
                    newQ.responseB = responseBTextBox.Text;
                    if (answerCEnable.Checked)
                    {
                        newQ.responseCEnabled = answerCEnable.Checked;
                        newQ.responseC = responseCTextBox.Text;
                    }
                    if (answerDEnable.Checked)
                    {
                        newQ.responseDEnabled = answerDEnable.Checked;
                        newQ.responseD = responseDTextBox.Text;
                    }
                    if (answerMarkA.Checked)
                    {
                        newQ.answer = "a";
                    }
                    if (answerMarkB.Checked)
                    {
                        newQ.answer = "b";
                    }
                    if (answerMarkC.Checked)
                    {
                        newQ.answer = "c";
                    }
                    if (answerMarkD.Checked)
                    {
                        newQ.answer = "d";
                    }
                    newQ.explanationOfAnswer = expTextBox.Text;
                    newQ.title = theQuestionTitle.Text;
                    newQ.timeToAnswer = int.Parse(timeToAnswerTextBox.Text);
                    int catagoryInt = catagoryComboBox.SelectedIndex;
                    //newQ.cat.setCatagory(catagoryComboBox.SelectedText);
                    if (catagoryInt == -1)
                    {
                        //NEW CATAGORY
                        //catagoryList.Add(new Catagory(catagoryComboBox.Text));
                        newQ.cat.setCatagory(catagoryComboBox.Text);
                        catagoryComboBox.Text = "";
                    }
                    else
                    {
                        newQ.cat.setCatagory(catagoryList[catagoryInt].getCatagory());
                    }
                    //newQ.cat.setCatagory(catagoryList[catagoryInt].getCatagory());
                    int mapInt = mapComboBox.SelectedIndex-1;
                    //newQ.m.setMap(mapComboBox.SelectedText);
                    if (mapInt == -1)
                    {
                        newQ.m.setMap("NONE");
                    }
                    else if (mapInt == -2)
                    {
                        //NEW MAP
                        // mapList.Add(new Map(mapComboBox.Text));
                        newQ.m.setMap(mapComboBox.Text);
                        mapComboBox.Text = "";
                    }
                    else
                    {
                        newQ.m.setMap(mapList[mapInt].getMap());
                    }
                    //Determine if first entry in the database
                    if (catagoryList == null)
                    {
                        //First Entry of catagory
                        catagoryList = new List<Catagory>();
                    }
                    if (mapList == null)
                    {
                        //First Entry of catagory
                        mapList = new List<Map>();
                    }
                    //CATAGORY
                    //determine if it needs to create another entry for a new catagory
                    int numHits = 0;
                    //run through the catagory list and count number of times the catagory to add shows up
                    foreach (Catagory catTT in catagoryList)
                    {
                        if (newQ.cat.getCatagory().Equals(catTT.getCatagory())) numHits++;
                    }
                    //if it's 0 hits then add it
                    if (numHits == 0)
                    {
                        catagoryList.Add(new Catagory(newQ.cat.getCatagory()));
                    }
                    //MAP
                    //determine if it needs to create another entry for a new map
                    int numMapHits = 0;
                    //run through the map list and count number of times the map to add shows up
                    foreach (Map mapPP in mapList)
                    {
                        if (newQ.m.getMap().Equals(mapPP.getMap())) numMapHits++;
                    }
                    //if it's 0 hits then add it
                    if (numHits == 0)
                    {
                        if(!newQ.m.getMap().Equals("NONE"))
                        {
                            mapList.Add(new Map(newQ.m.getMap()));
                        }
                    }
                    //and add the question
                    if (questionList == null)
                    {
                        //FIRST ENTRY
                        questionList = new List<Question>();
                    }
                    //parse new picture
                    if (photoComboBox.SelectedIndex != 0)
                    {
                        newQ.p = pictureList[photoComboBox.SelectedIndex - 1];
                    }
                    else
                    {
                       newQ.p = new Picture("NONE","null.jpg");
                    }

                    questionList.Add(newQ);
                    this.cleanupCatagories();
                    this.cleanUpPictures();
                    this.createDataBase(false);
                    this.setupQuestionBase();
                    this.setupQuestions();
                    this.uploadButton_Click(null, null);
                    this.loadDataBase(true);
                    this.resetGUI();
                }
            }
            else
            {
                //ask if the user is sure they would like to update the selected question
                DialogResult result = MessageBox.Show("Are you sure you would like to update this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    chooser.Close();
                    hasMadeChanges = true;
                    //procede...
                    //load the question from the question index
                    Question q2Edit = questionList[questionComboBox.SelectedIndex-1];
                    q2Edit.theQuestion = questionTextBox.Text;
                    q2Edit.responseA = responseATextBox.Text;
                    q2Edit.responseB = responseBTextBox.Text;

                    q2Edit.responseCEnabled = answerCEnable.Checked;
                    if (answerCEnable.Checked)
                    {
                        q2Edit.responseC = responseCTextBox.Text;
                    }
                    else
                    {
                        q2Edit.responseC = "";
                    }

                    q2Edit.responseDEnabled = answerDEnable.Checked;
                    if (answerDEnable.Checked)
                    {
                        q2Edit.responseD = responseDTextBox.Text;
                    }
                    else
                    {
                        q2Edit.responseD = "";
                    }

                    if (answerMarkA.Checked)
                    {
                        q2Edit.answer = "a";
                    }
                    if (answerMarkB.Checked)
                    {
                        q2Edit.answer = "b";
                    }
                    if (answerMarkC.Checked)
                    {
                        q2Edit.answer = "c";
                    }
                    if (answerMarkD.Checked)
                    {
                        q2Edit.answer = "d";
                    }
                    q2Edit.explanationOfAnswer = expTextBox.Text;
                    q2Edit.title = theQuestionTitle.Text;
                    q2Edit.timeToAnswer = int.Parse(timeToAnswerTextBox.Text);
                    string oldCatagory = q2Edit.cat.getCatagory();
                    string oldMap = q2Edit.m.getMap();
                    int catagoryInt = catagoryComboBox.SelectedIndex;
                    //newQ.cat.setCatagory(catagoryComboBox.SelectedText);
                    if (catagoryInt == -1)
                    {
                        //NEW CATAGORY
                        //catagoryList.Add(new Catagory(catagoryComboBox.Text));
                        q2Edit.cat.setCatagory(catagoryComboBox.Text);
                        catagoryComboBox.Text = "";
                    }
                    else
                    {
                        q2Edit.cat.setCatagory(catagoryList[catagoryInt].getCatagory());
                    }
                    int mapInt = mapComboBox.SelectedIndex - 1;
                    //newQ.m.setMap(mapComboBox.SelectedText);
                    if (mapInt == -1)
                    {
                        q2Edit.m.setMap("NONE");
                    }
                    else if (mapInt == -2)
                    {
                        //NEW MAP
                       // mapList.Add(new Map(mapComboBox.Text));
                        q2Edit.m.setMap(mapComboBox.Text);
                        mapComboBox.Text = "";
                    }
                    else
                    {
                        q2Edit.m.setMap(mapList[mapInt].getMap());
                    }
                    //CATAGORY
                    //determine if the catagory has changed
                    if (catagoryComboBox.SelectedItem!=null)
                    {
                        //nothing needs to be done
                    }
                    else
                    {
                        //if so, determine if old one was the last one of a catagory
                        int numHitsRemove = 0;
                        //run through the catagory list and count number of times the catagory to remove shows up
                        foreach (Catagory cat in catagoryList)
                        {
                            if (cat.Equals(oldCatagory)) numHitsRemove++;
                        }
                        //and determine if new one needs a new entry for the new catagory
                        int numHitsAdd = 0;
                        foreach (Catagory cat in catagoryList)
                        {
                            if (cat.Equals(q2Edit.cat.getCatagory())) numHitsAdd++;
                        }
                        //if remove is only 1 then remove it
                        if (numHitsRemove == 1)
                        {
                            for (int i = 0; i < catagoryList.Count; i++)
                            {
                                string temp = catagoryList[i].getCatagory();
                                if (oldCatagory.Equals(temp))
                                {
                                    catagoryList.RemoveAt(i);
                                }
                            }
                        }
                        //if add is 0 then add it
                        if (numHitsAdd == 0)
                        {
                            catagoryList.Add(new Catagory(q2Edit.cat.getCatagory()));
                        }
                    }
                    //MAP
                    //determine if the map has changed
                    if (mapComboBox.SelectedItem!=null)
                    {
                        //nothing needs to be done
                    }
                    else
                    {
                        //if so, determine if old one was the last one of a map
                        int numMapHitsRemove = 0;
                        //run through the map list and count number of times the map to remove shows up
                        foreach (Map cat in mapList)
                        {
                            if (cat.Equals(oldMap)) numMapHitsRemove++;
                        }
                        //and determine if new one needs a new entry for the new map
                        int numMapHitsAdd = 0;
                        foreach (Map cat in mapList)
                        {
                            if (cat.Equals(q2Edit.m.getMap())) numMapHitsAdd++;
                        }
                        //if remove is only 1 then remove it
                        if (numMapHitsRemove == 1)
                        {
                            for (int i = 0; i < mapList.Count; i++)
                            {
                                string temp = mapList[i].getMap();
                                if (oldMap.Equals(temp))
                                {
                                    mapList.RemoveAt(i);
                                }
                            }
                        }
                        //if add is 0 then add it
                        if (numMapHitsAdd == 0)
                        {
                            mapList.Add(new Map(q2Edit.m.getMap()));
                        }
                    }
                    //PARSE picture
                    if (photoComboBox.SelectedIndex != 0)
                    {
                        q2Edit.p = pictureList[photoComboBox.SelectedIndex - 1];
                    }
                    else
                    {
                        q2Edit.p = new Picture("NONE","null.jpg");
                    }

                    this.cleanupCatagories();
                    this.cleanUpPictures();
                    this.createDataBase(false);
                    this.setupQuestionBase();
                    this.setupQuestions();
                    this.uploadButton_Click(null, null);
                    this.loadDataBase(true);
                    this.resetGUI();
                }
            }
            this.saveButton.Enabled = false;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////
            //(seperate)
            //ask if the user is sure they would like to remove this question from the database
            //process the remove request in memory
            //(same)
            //reflect the update in disk database (delete and re-create)
            //reload the entire database to memory
            //update the gui
            /////////////////////////////////////////////////////////////////////////////////////
            DialogResult result = MessageBox.Show("Are you sure you would like to remove this question?", "Are you Sure", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                hasMadeChanges = true;
                //procede...
                int indexToRemove = questionComboBox.SelectedIndex;
                Question q2Rem = questionList[indexToRemove - 1];
                //CATAGORY
                //determine if it's the last one of a catagory
                int numHits = 0;
                Catagory tempCat = q2Rem.cat;
                string catT = tempCat.getCatagory();
                //run through the catagory list and count number of times the catagory to remove shows up
                /*foreach (Catagory cat in catagoryList)
                {
                    if (catT.Equals(cat.getCatagory())) numHits++;
                }*/
                foreach (Question q in questionList)
                {
                    if (catT.Equals(q.cat.getCatagory())) numHits++;
                }
                //if it's only 1 then remove it
                if (numHits == 1)
                {
                    for (int i = 0; i < catagoryList.Count; i++)
                    {
                        string temp = catagoryList[i].getCatagory();
                        if (catT.Equals(catagoryList[i].getCatagory()))
                        {
                            catagoryList.RemoveAt(i);
                        }
                    }
                }
                //MAP
                //determine if it's the last one of a map
                int numMapHits = 0;
                Map tempMap = q2Rem.m;
                string MapP = tempMap.getMap();
                //run through the catagory list and count number of times the catagory to remove shows up
                foreach (Map mapppp in mapList)
                {
                    string mapListString = mapppp.getMap();
                    if (MapP.Equals(mapListString)) numMapHits++;
                }
                //if it's only 1 then remove it
                if (numHits == 1)
                {
                    for (int i = 0; i < mapList.Count; i++)
                    {
                        if (MapP.Equals(mapList[i].getMap()))
                        {
                            mapList.RemoveAt(i);
                        }
                    }
                }
                //and remove the question
                questionList.RemoveAt(indexToRemove-1);
                this.cleanupCatagories();
                this.cleanUpPictures();
                this.createDataBase(false);
                this.setupQuestionBase();
                this.setupQuestions();
                this.uploadButton_Click(null, null);
                this.loadDataBase(true);
                this.resetGUI();
                removeButton.Enabled = false;
            }
        }

        private void createQuestion()
        {

            //determine if it needs to create another entry for a new catagory or map
        }

        private void updateQuestion()
        {
            //determine if the map or catagory has changed
            //if so, if old one was the last one of a catagory or ma
            //and determine if new one needs to create another entry for a new catagory or map
        }

        private void removeQuestion(int indexToRemove)
        {

        }

        private void setupQuestionBase()
        {
            questionNumber = 0;
            playerNumber = 0;
            questionBaseWriter = new XmlTextWriter(questionPath + "\\" + questionBase, Encoding.UTF8);
            questionBaseWriter.Formatting = Formatting.Indented;

            questionBaseWriter.WriteStartDocument();
            questionBaseWriter.WriteStartElement("QuestionBase.xml");

            questionBaseWriter.WriteComment("Catagories List");
            questionBaseWriter.WriteStartElement("catagories");
            foreach (Catagory cat in catagoryList)
            {
                questionBaseWriter.WriteElementString("catagory", cat.getCatagory());
            }
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteComment("Maps List");
            questionBaseWriter.WriteStartElement("maps");
            foreach (Map m in mapList)
            {
                questionBaseWriter.WriteElementString("map", m.getMap());
            }
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteComment("Pictures List");
            questionBaseWriter.WriteStartElement("pictures");
            foreach (Picture pp in pictureList)
            {
                questionBaseWriter.WriteElementString("picture", pp.photoTitle);
            }
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteComment("Pictures File Paths");
            questionBaseWriter.WriteStartElement("pictureFilePaths");
            foreach (Picture pp in pictureList)
            {
                string fileName = Path.GetFileName(pp.photoFileName);
                questionBaseWriter.WriteElementString("pictureFilePath", fileName);
            }
            questionBaseWriter.WriteEndElement();


            
            questionBaseWriter.WriteComment("Questions");
            questionBaseWriter.WriteStartElement("questions");
            foreach (Question q in questionList)
            {
                questionBaseWriter.WriteElementString(questionPrefix, questionPrefix + questionNumber++ + xmlExtension);
            }
            questionBaseWriter.WriteEndElement();

            questionBaseWriter.WriteEndElement();
            questionBaseWriter.Close();
        }

        private void setupQuestions()
        {
            //write each question from the list
            for (int i = 0; i < questionList.Count; i++)
            {
                questionWriter = new XmlTextWriter(questionPath + "\\" + questionPrefix + i + xmlExtension, Encoding.UTF8);
                questionWriter.Formatting = Formatting.Indented;

                questionWriter.WriteStartDocument();
                questionWriter.WriteStartElement(questionPrefix + i + xmlExtension);
                questionWriter.WriteStartElement("question");
                questionWriter.WriteElementString("title", questionList[i].title);
                questionWriter.WriteElementString("catagory", questionList[i].cat.getCatagory());
                questionWriter.WriteElementString("theQuestion", questionList[i].theQuestion);
                questionWriter.WriteElementString("responseA", questionList[i].responseA);
                questionWriter.WriteElementString("responseB", questionList[i].responseB);
                questionWriter.WriteElementString("responseC", questionList[i].responseC);
                questionWriter.WriteElementString("responseCEnabled", "" + questionList[i].responseCEnabled);
                questionWriter.WriteElementString("responseD", questionList[i].responseD);
                questionWriter.WriteElementString("responseDEnabled", "" + questionList[i].responseDEnabled);
                questionWriter.WriteElementString("answer", questionList[i].answer);
                questionWriter.WriteElementString("timeToAnswer", "" + questionList[i].timeToAnswer);
                questionWriter.WriteElementString("explanationOfAnswer", "" + questionList[i].explanationOfAnswer);
                questionWriter.WriteElementString("map", "" + questionList[i].m.getMap());
                questionWriter.WriteElementString("picture", questionList[i].p.photoTitle);
                questionWriter.WriteEndElement();
                questionWriter.WriteEndElement();
                questionWriter.Close();
            }
        }

        private void timeToAnswerTextBox_MouseLeave(object sender, EventArgs e)
        {
            /*try
            {
                int temp = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This must be a whole number");
                timeToAnswerTextBox.Text = "" + lastNumber;
            }*/
        }

        private void timeToAnswerTextBox_Leave(object sender, EventArgs e)
        {
            int temp = 0;
            try
            {
                if (timeToAnswerTextBox.Text.Equals(""))
                {
                    timeToAnswerTextBox.Text = "0";
                    return;
                }
                temp = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This must be a positive whole number");
                timeToAnswerTextBox.Text = "" + lastNumber;
            }
            if (temp < 0)
            {
                MessageBox.Show("This must be a positive whole number");
                timeToAnswerTextBox.Text = "" + lastNumber;
            }
        }

        private void timeToAnswerTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                lastNumber = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {

            }
        }
        //OLD! DO NOT USE!
        private void button1_Click(object sender, EventArgs e)
        {
            this.createDataBase(true);
            button3.Enabled = false;
        }

        private void resetForm_Click(object sender, EventArgs e)
        {
            questionComboBox.SelectedIndex = -1;
            mapComboBox.SelectedIndex = -1;
            catagoryComboBox.SelectedIndex = -1;
            questionTextBox.Text = "";
            responseATextBox.Text = "";
            responseBTextBox.Text = "";
            answerCEnable.Checked = false;
            responseCTextBox.Text = "";
            answerDEnable.Checked = false;
            responseDTextBox.Text = "";
            answerMarkA.Checked = false;
            answerMarkB.Checked = false;
            answerMarkC.Checked = false;
            answerMarkD.Checked = false;
            expTextBox.Text = "";
            theQuestionTitle.Text = "";
            timeToAnswerTextBox.Text = "" + "";
            currentModeLabel.Visible = false;
            photoComboBox_SelectedIndexChanged(null, null);
        }
        private void cleanupCatagories()
        {
            //for each catagory
            //for each question
            //run through the list and see if it is still in use
            //CATAGORY
            int catagoryHits;
            string catagory;
            for (int i = 0; i < catagoryList.Count; i++)
            {
                catagoryHits = 0;
                catagory = catagoryList[i].getCatagory();
                for (int j = 0; j < questionList.Count; j++)
                {
                    if (questionList[j].cat.getCatagory().Equals(catagory)) catagoryHits++;
                }
                if (catagoryHits == 0) catagoryList.RemoveAt(i);
            }
            //MAP
            //(remember to exclude index 0 (NONE)
            int mapHits;
            string map;
            for (int i = 0; i < mapList.Count; i++)
            {
                mapHits = 0;
                map = mapList[i].getMap();
                for (int j = 0; j < questionList.Count; j++)
                {
                    if (questionList[j].m.getMap().Equals(map)) mapHits++;
                }
                if (mapHits == 0) mapList.RemoveAt(i);
            }
        }

        private void verifyCode_Click(object sender, EventArgs e)
        {
            CodeVerify v = new CodeVerify();
            v.ShowDialog();
        }
        //OLD! DO NOT USE!
        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (!hasMadeChanges)
            {
                MessageBox.Show("No changes Made");
                return;
            }
            //WORKING DROPBOX CODE FOR UPLOAD FILES
            string[] fileList = Directory.GetFiles(questionPath);
            string[] pictureLizt = Directory.GetFiles(picturePath);
            wait = new PleaseWait(fileList.Count()+pictureLizt.Count(), 0);
            int prog = 0;
            wait.Show();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            dropBoxStorage = new CloudStorage();
            // get the configuration for dropbox
            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            // declare an access token
            ICloudStorageAccessToken accessToken = null;
            // load a valid security token from file
            using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }
            wait.setProgress(prog++);
            // open the connection 
            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
            // get a specific directory in the cloud storage, e.g. /Public 
            var questionsFolder = dropBoxStorage.GetFolder("/Public/RelicExam/questions");
            dropBoxStorage.DeleteFileSystemEntry(questionsFolder);
            wait.setProgress(prog++);
            
            for (int i = 0; i < fileList.Count(); i++)
            {
                // upload a testfile from temp directory into public folder of DropBox
                String srcFile = Environment.ExpandEnvironmentVariables(fileList[i]);
                dropBoxStorage.UploadFile(srcFile, questionsFolder);
                wait.setProgress(prog++);
            }

            var questionsFolder2 = dropBoxStorage.GetFolder("/Public/RelicExam/pictures");
            dropBoxStorage.DeleteFileSystemEntry(questionsFolder2);
            for (int i = 0; i < pictureLizt.Count(); i++)
            {
                String srcFile = Environment.ExpandEnvironmentVariables(pictureLizt[i]);
                dropBoxStorage.UploadFile(srcFile, questionsFolder2);
                wait.setProgress(prog++);
            }

            dropBoxStorage.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            wait.Close();
        }

        private void answerCEnable_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void answerCEnable_Click(object sender, EventArgs e)
        {
            if (answerMarkC.Checked && !answerCEnable.Checked)
            {
                MessageBox.Show("Cannot Disable the Answer Question!!");
                answerCEnable.Checked = true;
                return;
            }
            if (answerCEnable.Checked)
            {
                responseCTextBox.ReadOnly = false;
                answerMarkC.Enabled = true;
            }
            else
            {
                responseCTextBox.ReadOnly = true;
                answerMarkC.Enabled = false;
            }
        }

        private void DatabaseManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            PleaseWait pw = new PleaseWait();
            if(close){}
            else
            {
                
                pw.Show();
                Application.DoEvents();
                if (chooser != null) chooser.Close();
                dropBoxStorage = new CloudStorage();
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
                pw.Close();
            }
        }

        private void addPictureButton_Click(object sender, EventArgs e)
        {
            if (chooser != null) chooser.Close();
            pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
            chooser = new PhotoViewer(pictureSpawnPoint);
            chooser.passInList(pictureList);
            chooser.Location = pictureSpawnPoint;
            string pictureLocationz;
            string extension;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                pictureLocationz = openFileDialog1.FileName;
                extension = Path.GetExtension(pictureLocationz);
                chooser.setPicture(pictureLocationz);
                chooser.ShowDialog();
            }
            if (chooser.cancel) return;
            string[] list = Directory.GetFiles(picturePath);
            string newName = picturePath + "\\picture" + list.Length + extension;
            File.Copy(pictureLocationz, picturePath + "\\picture" + list.Length + extension);
            Picture temp = new Picture(chooser.photoNamee, newName);
            pictureList.Add(temp);
            //update the picture combo box
            photoComboBox.Items.Add(temp);
            photoComboBox.SelectedIndex = getPicture(chooser.photoNamee);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void photoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actuallyLoad)
            {
                //close and photoviewers 
                if (chooser != null) chooser.Close();
                if (photoComboBox.SelectedIndex == 0 || photoComboBox.SelectedIndex == -1)
                {
                    pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
                    chooser = new PhotoViewer(pictureSpawnPoint);
                    chooser.Location = pictureSpawnPoint;
                    chooser.Show();
                    return;
                }
                Picture tempPic = pictureList[photoComboBox.SelectedIndex - 1];
                pictureName = tempPic.photoTitle;
                pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
                chooser = new PhotoViewer(pictureSpawnPoint);
                chooser.Location = pictureSpawnPoint;
                chooser.setPicture(tempPic.photoFileName);
                chooser.Show();
            }
        }
        //gets the index from pictureList of desired picture based on title
        private int getPicture(string title)
        {
            if (title.Equals("NONE")) return 0;
            for (int i = 0; i < pictureList.Count; i++)
            {
                if (title.Equals(pictureList[i].photoTitle))
                {
                    return i+1;
                }
            }
            return 0;
        }
        //load every picture in the picture database(done?)
        //load every picture in the folder
        //for each picture in folder, run through the list and 
        //count the number of hits it is used using the picture's filePath
        private void cleanUpPictures()
        {
            for (int i = 0; i < pictureList.Count; i++)
            {
                int numHits = 0;
                for (int j = 0; j < questionList.Count; j++)
                {
                    if (pictureList[i].photoFileName.Equals(questionList[j].p.photoFileName)) numHits++;
                }
                if (numHits == 0)
                {
                    File.Delete(pictureList[i].photoFileName);
                    pictureList.RemoveAt(i);
                }
            }
        }
        //parses all file paths required upon application load
        public void parseFilePaths()
        {
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            playerPath = dataBasePath + "\\players";
            questionBase = "questionBase.xml";
            playerBase = "playerBase.xml";
            picturePath = dataBasePath + "\\pictures";
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}

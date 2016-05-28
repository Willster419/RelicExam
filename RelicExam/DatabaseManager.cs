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
        private ArrayList catagories;
        private ArrayList maps;
        private ArrayList questionReaderList;
        private ArrayList playerReaderList;
        private string questionPrefix = "question";
        private int questionNumber = 0;
        private string xmlExtension = ".xml";
        private string playerPrefix = "player";
        private int playerNumber = 0;
        private List<Player> playerList;
        private List<Question> questionList;

        public DatabaseManager()
        {
            InitializeComponent();
        }

        private void DatabaseManager_Load(object sender, EventArgs e)
        {
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            playerPath = dataBasePath + "\\players";
            questionBase = "questionBase.xml";
            playerBase = "playerBase.xml";
            
            //declare all private classes
            tempQuestion = new Question();
            tempPlayer = new Player();

            //display a loading window and load the database
            wait = new PleaseWait();
            Application.DoEvents();
            wait.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            //this.loadDataBase();
            wait.Close();
        }

        private void loadDataBase()
        {
            if (!Directory.Exists(dataBasePath))
            {
                this.createDataBase();
                this.loadDataBase();
                return;
            }
            //more cool code here
        }

        private void createDataBase()
        {
            Directory.CreateDirectory(dataBasePath);
            Directory.CreateDirectory(questionPath);
            Directory.CreateDirectory(playerPath);
            this.setupSampleXmlFiles();
        }

        private void setupSampleXmlFiles()
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
            this.setupSampleQuestionBase();
            this.setupSamplePlayers();
            this.setupSampleQuestions();
            this.setupSamplePlayerBase();
        }

        private void setupSampleQuestionBase()
        {
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
        }

        private void setupSamplePlayers()
        {
            //new everything
            Player player0 = new Player();
            Player player1 = new Player();
            playerList = new List<Player>();

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
            playerList.Add(player0);
            playerList.Add(player1);

            //write each player from the list
            for (int i = 0; i < playerList.Count; i++)
            {
                playerWriter = new XmlTextWriter(playerPath + "\\" + playerPrefix + i + xmlExtension, Encoding.UTF8);
                playerWriter.Formatting = Formatting.Indented;

                playerWriter.WriteStartDocument();
                playerWriter.WriteStartElement(playerPrefix + i + xmlExtension);
                playerWriter.WriteStartElement("player");
                playerWriter.WriteElementString("name", playerList[i].name);
                playerWriter.WriteElementString("totalTimesPlayed", "" + playerList[i].totalTimesPlayed);
                playerWriter.WriteElementString("percentCorrect", "" + playerList[i].percentCorrent);
                playerWriter.WriteElementString("lastPercentCorrent", "" + playerList[i].lastPercentCorrect);
                playerWriter.WriteElementString("totalSessionQuestions", "" + playerList[i].totalSessionQuestions);
                playerWriter.WriteElementString("lastTotalSessionQuestions", "" + playerList[i].lastTotalSessionQuestions);
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
            questionList = new List<Question>();

            //questions
            question0.title = "Sample Question 1";
            question0.catagory = sampleCatagories[0];
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
            question0.map = sampleMaps[0];

            question1.title = "Sample Question 2";
            question1.catagory = sampleCatagories[1];
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
            question1.map = sampleMaps[1];

            //add to list
            questionList.Add(question0);
            questionList.Add(question1);

            //write each question from the list
            for (int i = 0; i < questionList.Count; i++)
            {
                questionWriter = new XmlTextWriter(questionPath + "\\" + questionPrefix + i + xmlExtension, Encoding.UTF8);
                questionWriter.Formatting = Formatting.Indented;

                questionWriter.WriteStartDocument();
                questionWriter.WriteStartElement(questionPrefix + i + xmlExtension);
                questionWriter.WriteStartElement("question");
                questionWriter.WriteElementString("title", questionList[i].title);
                questionWriter.WriteElementString("catagory", questionList[i].catagory);
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
                questionWriter.WriteElementString("map", "" + questionList[i].map);
                questionWriter.WriteEndElement();
                questionWriter.WriteEndElement();
                questionWriter.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(dataBasePath)) Directory.Delete(dataBasePath, true);
            }
            catch (IOException ex)
            { 
                MessageBox.Show(ex.Message);
                return;
            }
            this.createDataBase();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //new up everything
            catagories = new ArrayList();
            maps = new ArrayList();
            tempQuestion = new Question();
            tempPlayer = new Player();
            questionList = new List<Question>();
            playerList = new List<Player>();
            playerBaseReader = new XmlTextReader(playerPath + "\\" + playerBase);
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            playerReaderList = new ArrayList();
            questionReaderList = new ArrayList();

            //read question base
            while (questionBaseReader.Read())
            {
                if (questionBaseReader.IsStartElement())
                {
                    switch (questionBaseReader.Name)
                    {
                        case "catagory":
                            catagories.Add(questionBaseReader.ReadString());
                            break;
                        case "map":
                            maps.Add(questionBaseReader.ReadString());
                            break;
                        case "question":
                            questionReaderList.Add(questionBaseReader.ReadString());
                            break;

                    }
                }
            }

            //read player base
            while (playerBaseReader.Read())
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

            //parse players (TODO)

            //parse questions
            foreach (string q in questionReaderList)
            {
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
                                tempQuestion.catagory = questionReader.ReadString();
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
                                //more work needed here
                                tempQuestion.responseCEnabled = Boolean.Parse(questionReader.ReadString());
                                break;
                            case "responseD":
                                tempQuestion.responseD = questionReader.ReadString();
                                break;
                            case "responseDEnabled":
                                //moar werk needed hear
                                tempQuestion.responseDEnabled = Boolean.Parse(questionReader.ReadString());
                                break;
                            case "answer":
                                tempQuestion.answer = questionReader.ReadString();
                                break;
                            case "timeToAnswer":
                                //more twerk needed herez
                                tempQuestion.timeToAnswer = int.Parse(questionReader.ReadString());
                                break;
                            case "explanationOfAnswer":
                                tempQuestion.explanationOfAnswer = questionReader.ReadString();
                                break;
                            case "map":
                                tempQuestion.map = questionReader.ReadString();
                                break;
                        }
                    }
                }
                questionList.Add(tempQuestion);
            }
            this.parseDataBaseToGUI();
        }

        private void parseDataBaseToGUI()
        {
            questionComboBox.Items.Add(questionList[0]);
        }

        private void answerCEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (answerCEnable.Checked)
            {
                responseCTextBox.ReadOnly = false;
            }
            else
            {
                responseCTextBox.ReadOnly = true;
            }
        }

        private void answerDEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (answerDEnable.Checked)
            {
                responseDTextBox.ReadOnly = false;
            }
            else
            {
                responseDTextBox.ReadOnly = true;
            }
        }
    }
}

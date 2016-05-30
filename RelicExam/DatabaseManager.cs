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
        private FileStream stream;

        public DatabaseManager()
        {
            InitializeComponent();
        }

        private void DatabaseManager_Load(object sender, EventArgs e)
        {
            //display a loading window and load the database
            wait = new PleaseWait();
            Application.DoEvents();
            wait.Show();
            Application.DoEvents();
            //do all intensive code here

            //write a file saying someone is editing the questionList to DropBox


            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            playerPath = dataBasePath + "\\players";
            questionBase = "questionBase.xml";
            playerBase = "playerBase.xml";

            //declare all temp objects
            tempQuestion = new Question();
            tempPlayer = new Player();

            //update gui
            //System.Threading.Thread.Sleep(100);
            this.answerCEnable_CheckedChanged(null, null);
            this.answerDEnable_CheckedChanged(null, null);
            currentModeLabel.Visible = false;
            removeButton.Enabled = false;
            //this.loadDataBase();
            //this.resetGUI();
            wait.Close();
        }

        private void loadDataBase()
        {
            //new up everything
            tempQuestion = new Question();
            tempPlayer = new Player();
            questionList = new List<Question>();
            playerList = new List<Player>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
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
                            catagoryList.Add(new Catagory(questionBaseReader.ReadString()));
                            break;
                        case "map":
                            mapList.Add(new Map(questionBaseReader.ReadString()));
                            break;
                        case "question":
                            questionReaderList.Add(questionBaseReader.ReadString());
                            break;
                    }
                }
            }
            questionBaseReader.Close();
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
                        }
                    }
                }
                questionList.Add(tempQuestion);
                questionReader.Close();
            }

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
        }

        private void answerCEnable_CheckedChanged(object sender, EventArgs e)
        {
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

        private void answerDEnable_CheckedChanged(object sender, EventArgs e)
        {
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

        private void createDataBase()
        {
            try
            {
                if (Directory.Exists(dataBasePath)) Directory.Delete(dataBasePath, true);
            }
            catch (IOException ex)
            {
                MessageBox.Show("get out of my database");
            }
            Directory.CreateDirectory(dataBasePath);
            Directory.CreateDirectory(questionPath);
            Directory.CreateDirectory(playerPath);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.createDataBase();
            this.setupSampleXmlFiles();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.loadDataBase();
            this.resetGUI();
        }

        private void questionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (questionComboBox.SelectedIndex == -1) return;
            //determine if a new question is being made or if one is being updated
            if (questionComboBox.SelectedIndex == 0)
            {
                //warn the user this is "new question mode"
                currentModeLabel.Text = "CREATE";
                currentModeLabel.Visible = true;
                saveButton.Text = "create";
                removeButton.Enabled = false;
            }
            else
            {
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
                }
            }

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
            //update the gui
            //////////////////////////////////////////////////////////////////
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
                    newQ.cat.setCatagory(catagoryComboBox.SelectedText);
                    newQ.m.setMap(mapComboBox.SelectedText);
                    //determine if it needs to create another entry for a new catagory or map
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
                    //and add the question
                    questionList.Add(newQ);
                    this.createDataBase();
                    this.setupQuestionBase();
                    this.setupQuestions();
                    this.loadDataBase();
                    this.resetGUI();
                }
            }
            else
            {
                //ask if the user is sure they would like to update the selected question
                DialogResult result = MessageBox.Show("Are you sure you would like to upodate this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    //procede...
                    //load the question from the question index
                    Question q2Edit = questionList[questionComboBox.SelectedIndex-1];
                    q2Edit.theQuestion = questionTextBox.Text;
                    q2Edit.responseA = responseATextBox.Text;
                    q2Edit.responseB = responseBTextBox.Text;
                    if (answerCEnable.Checked)
                    {
                        q2Edit.responseCEnabled = answerCEnable.Checked;
                        q2Edit.responseC = responseCTextBox.Text;
                    }
                    if (answerDEnable.Checked)
                    {
                        q2Edit.responseDEnabled = answerDEnable.Checked;
                        q2Edit.responseD = responseDTextBox.Text;
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
                    q2Edit.cat.setCatagory(catagoryComboBox.SelectedText);
                    q2Edit.m.setMap(mapComboBox.SelectedText);
                    //determine if the map or catagory has changed
                    if (catagoryComboBox.SelectedText.Equals(q2Edit.cat.getCatagory()))
                    {
                        //nothing needs to be done
                    }
                    else
                    {
                        //if so, determine if old one was the last one of a catagory or map
                        int numHitsRemove = 0;
                        //run through the catagory list and count number of times the catagory to remove shows up
                        foreach (Catagory cat in catagoryList)
                        {
                            if (cat.Equals(oldCatagory)) numHitsRemove++;
                        }
                        //and determine if new one needs a new entry for the new catagory or map
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
                    this.createDataBase();
                    this.setupQuestionBase();
                    this.setupQuestions();
                    this.loadDataBase();
                    this.resetGUI();
                }
            }
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
                //procede...
                int indexToRemove = questionComboBox.SelectedIndex;

                //determine if it's the last one of a catagory or map
                int numHits = 0;
                Question q2Rem = questionList[indexToRemove-1];
                Catagory tempCat = q2Rem.cat;
                string catT = tempCat.getCatagory();
                //run through the catagory list and count number of times the catagory to remove shows up
                foreach (Catagory cat in catagoryList)
                {
                    string catListString = cat.getCatagory();
                    if (catT.Equals(catListString)) numHits++;
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
                //and remove the question
                questionList.RemoveAt(indexToRemove-1);
                this.createDataBase();
                this.setupQuestionBase();
                this.setupQuestions();
                this.loadDataBase();
                this.resetGUI();
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
                questionWriter.WriteEndElement();
                questionWriter.WriteEndElement();
                questionWriter.Close();
            }
        }

        private void timeToAnswerTextBox_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This must be a whole number");
                timeToAnswerTextBox.Text = "" + lastNumber;
            }
        }

        private void timeToAnswerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This must be a whole number");
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;

namespace RelicExam
{
    public partial class QuestionViewer : Form
    {
        //list all static objects to exist throughout the program
        public int numQuestions;
        private string catagory;
        private string map;
        private int currentQuestion;
        private Question tempQuestion;
        Question loadedQuestion;
        public int numCorrect;
        public bool correct;
        private XmlTextReader questionBaseReader;
        private XmlTextReader questionReader;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private List<Picture> pictureList;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionPath;
        public string questionBase;
        public string picturePath;
        private ArrayList questionReaderList;
        private string questionPrefix = "question";
        private int questionNumber = 0;
        private string xmlExtension = ".xml";
        private List<Question> questionDisplayList;
        private Random rng;
        private Results theResults;
        private int timeLeft;
        public bool canSubmit;
        private PhotoViewer foto;
        private Point pictureSpawnPoint;

        public QuestionViewer()
        {
            InitializeComponent();
        }

        public QuestionViewer(int numQuestionss, string catagoryy, string mapp)
        {
            InitializeComponent();
            numQuestions = numQuestionss;
            catagory = catagoryy;
            map = mapp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //update timer stuff
            progressBar1.Value = timeLeft;
            numSecondsLeft.Text = "" + timeLeft--;
            //handle timout
            if (timeLeft < 0)
            {
                outOfTime.Visible = true;
                correct = false;
                submitAnswer.Enabled = false;
                this.postQuestion();
            }
        }

        private void submitAnswer_Click(object sender, EventArgs e)
        {
            submitAnswer.Enabled = false;
            this.postQuestion();
        }

        private void QuestionViewer_Load(object sender, EventArgs e)
        {
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            questionBase = "questionBase.xml";
            picturePath = dataBasePath + "\\pictures";
            //new up everything
            theResults = new Results();
            questionList = new List<Question>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            questionReaderList = new ArrayList();
            tempQuestion = new Question();
            questionDisplayList = new List<Question>();
            pictureList = new List<Picture>();
            rng = new Random();
            List<string> pictureFileNameList = new List<string>();
            //reset  stuff
            currentQuestion = 0;
            timer1.Stop();
            timer1.Enabled = false;
            currentQuestion = 0;
            questionNumber = 0;
            outOfTime.Visible = false;
            //parse questionBase to this form
            while (questionBaseReader.Read())
            {
                if (questionBaseReader.IsStartElement())
                {
                    switch (questionBaseReader.Name)
                    {
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
            //add picture filenames to pictures list
            for (int i = 0; i < pictureList.Count; i++)
            {
                pictureList[i].photoFileName = picturePath + "\\" + Path.GetFileName(pictureFileNameList[i]);
            }
            //parse questions to question list
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
                            case "picture":
                                string result = questionReader.ReadString();
                                if (result.Equals("NONE") || result.Equals(""))
                                {
                                    tempQuestion.p = new Picture("NONE", "null.jpg");
                                }
                                else
                                {
                                    tempQuestion.p = pictureList[getPicture(result)];
                                }
                                break;
                        }
                    }
                }
                questionList.Add(tempQuestion);
                questionReader.Close();
            }//end for loop
            //determine if filtering is required
            if (catagory == null && map == null)
            {
                //no filtering required
            }
            else if (catagory != null)
            {
                //filtering of type catagory
                for (int i = 0; i < questionList.Count; i++)
                {
                    if (!catagory.Equals(questionList[i].cat.getCatagory()))
                    {
                        questionList.RemoveAt(i);
                    }
                }
            }
            else
            {
                //filtering of type map
                for (int i = 0; i < questionList.Count; i++)
                {
                    if (!map.Equals(questionList[i].m.getMap()))
                    {
                        questionList.RemoveAt(i);
                    }
                }
            }
            //let user know if number of askable questions < questions in database
            if (questionList.Count < numQuestions)
            {
                //question list maxed out
                MessageBox.Show("Database cannot fill request, quiz of only " + questionList.Count + " questions will be shown");
                numQuestions = questionList.Count;
            }
            //fill list of questions to display
            for (int i = 0; i < numQuestions; i++)
            {
                double randomChoice = questionList.Count;
                randomChoice = randomChoice * rng.NextDouble();
                int realRandomChoice = (int)randomChoice;
                questionDisplayList.Add(questionList[realRandomChoice]);
                questionList.RemoveAt(realRandomChoice);
            }
            this.displayNextQuestion();
        }

        private void rageQuit_Click(object sender, EventArgs e)
        {
            //RAGEQUIT BUTTON
            if (foto != null) foto.Close();
            this.Hide();
        }

        private void nextQuestion_Click(object sender, EventArgs e)
        {
            //NEXT BUTTON
            if (currentQuestion == numQuestions)
            {
                theResults = new Results(numCorrect,numQuestions);
                if (foto != null) foto.Close();
                theResults.ShowDialog();
                this.Hide();
                return;
            }
            else
            {
                canSubmit = true;
                this.displayNextQuestion();
            }
        }
        //resets the gui with the question
        private void displayNextQuestion()
        {
            //soft reset of some gui componets
            submitAnswer.Enabled = true;
            if (foto != null) foto.Close();
            loadedQuestion = questionDisplayList[currentQuestion];
            //display every single UI componet of the question
            currentQuestionNum.Text = "" + (currentQuestion+1);
            totalQuestions.Text = "" + numQuestions;
            theCatagory.Text = loadedQuestion.cat.getCatagory();
            theMap.Text = loadedQuestion.m.getMap();
            questionTitle.Text = loadedQuestion.title;
            theQuestionRichTextBox.Text = loadedQuestion.theQuestion;
            responseA.Text = loadedQuestion.responseA;
            responseB.Text = loadedQuestion.responseB;

            if (loadedQuestion.responseCEnabled)
            {
                responseC.Enabled = true;
                responseC.Text = loadedQuestion.responseC;
            }
            else
            {
                responseC.Enabled = false;
            }
            if (loadedQuestion.responseDEnabled)
            {
                responseD.Enabled = true;
                responseD.Text = loadedQuestion.responseD;
            }
            else
            {
                responseD.Enabled = false;
            }
            radioButtonA.Checked = false;
            radioButtonB.Checked = false;
            radioButtonC.Checked = false;
            radioButtonD.Checked = false;
            pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
            foto = new PhotoViewer(pictureSpawnPoint);
            foto.Location = pictureSpawnPoint;
            if (loadedQuestion.p.photoFileName.Equals("null.jpg"))
            {
                //no picture to display
            }
            else
            {
                foto.setPicture(loadedQuestion.p.photoFileName);
            }
            foto.Show();
            //good for debugging, bad for quizing
            /*
            if (loadedQuestion.answer.Equals("a"))
            {
                radioButtonA.Checked = true;
            }
            if (loadedQuestion.answer.Equals("b"))
            {
                radioButtonB.Checked = true;
            }
            if (loadedQuestion.answer.Equals("c"))
            {
                radioButtonC.Checked = true;
            }
            if (loadedQuestion.answer.Equals("d"))
            {
                radioButtonD.Checked = true;
            }*/
            correctOrNot.Visible = false;
            richTextBox2.Text = loadedQuestion.explanationOfAnswer;
            richTextBox2.Visible = false;
            numSecondsLeft.Text = "" + loadedQuestion.timeToAnswer;
            timeLeft = loadedQuestion.timeToAnswer;
            outOfTime.Visible = false;
            progressBar1.Value = 0;
            progressBar1.Maximum = loadedQuestion.timeToAnswer;
            progressBar1.Minimum = 0;
            nextQuestion.Enabled = false;
            canSubmit = true;
            //start the countdown
            timer1.Enabled = true;
            timer1.Start();
            currentQuestion++;
        }
        private void QuestionViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (foto != null) foto.Close();
            this.Hide();
        }
        //does all the post processing after a question has been answered
        private void postQuestion()
        {
            //enable the next question
            nextQuestion.Enabled = true;
            //stop the timer
            timer1.Stop();
            timer1.Enabled = false;
            //logic to determine the right answer
            string selectedAnswer = "";
            if (radioButtonA.Checked) selectedAnswer = "a";
            if (radioButtonB.Checked) selectedAnswer = "b";
            if (radioButtonC.Checked) selectedAnswer = "c";
            if (radioButtonD.Checked) selectedAnswer = "d";
            //display right or wrong and the explanation
            //the entirety of this quiz comes down to this line
            if(selectedAnswer.Equals(loadedQuestion.answer)&& !outOfTime.Visible)
            {
                correct = true;
            }
            else
            {
                correct = false;
            }
            if (correct)
            {
                //run correct code. get it, "correct" code?
                correctOrNot.Text = "Correct!";
                correctOrNot.Visible = true;
                richTextBox2.Visible = true;
                numCorrect++;
            }
            else
            {
                //run incorrect code (lol)
                correctOrNot.Text = "Incorrect: Answer was " + loadedQuestion.answer;
                correctOrNot.Visible = true;
                richTextBox2.Visible = true;
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
                    return i;
                }
            }
            return -1;
        }
    }
}

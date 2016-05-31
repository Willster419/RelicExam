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
using System.Collections;
using System.IO;

namespace RelicExam
{
    public partial class QuestionViewer : Form
    {
        public int numQuestions;
        private string catagory;
        private string map;
        private int currentQuestion = 0;
        private Question tempQuestion;
        Question loadedQuestion;
        Catagory loadedCatagory;
        Map loadedMap;
        public int numCorrect;
        public bool correct;
        private XmlTextReader questionBaseReader;
        private XmlTextReader questionReader;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionPath;
        public string questionBase;
        private ArrayList questionReaderList;
        private string questionPrefix = "question";
        private int questionNumber = 0;
        private string xmlExtension = ".xml";
        private List<Question> questionDisplayList;
        private Random rng;
        private Results theResults;
        private int timeLeft;
        bool canSubmit;

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
            if (timeLeft == -1)
            {
                outOfTime.Visible = true;
                correct = false;
                this.postQuestion();
                //TIMEOUT
            }
        }

        private void submitAnswer_Click(object sender, EventArgs e)
        {
            if (canSubmit)
            {
                canSubmit = false;
            }
            else
            {
                MessageBox.Show("You already submitted for this question");
            }
            this.postQuestion();
        }

        private void QuestionViewer_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            currentQuestion = 0;
            questionNumber = 0;
            outOfTime.Visible = false;
            theResults = new Results();
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            questionBase = "questionBase.xml";

            //load the questions into memory:
            //new up everything for single use
            questionList = new List<Question>();
            mapList = new List<Map>();
            catagoryList = new List<Catagory>();
            questionBaseReader = new XmlTextReader(questionPath + "\\" + questionBase);
            questionReaderList = new ArrayList();
            tempQuestion = new Question();
            questionDisplayList = new List<Question>();
            rng = new Random();

            //parse questionBase
            while (questionBaseReader.Read())
            {
                if (questionBaseReader.IsStartElement())
                {
                    switch (questionBaseReader.Name)
                    {
                        case "question":
                            questionReaderList.Add(questionBaseReader.ReadString());
                            break;
                    }
                }
            }
            questionBaseReader.Close();

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
            //determine if it needs to filter questions based on catagory or map

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
            //let user know if number of askable questions < questions in list
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

        private void button3_Click(object sender, EventArgs e)
        {
            //RAGEQUIT BUTTON
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NEXT BUTTON
            if (currentQuestion == numQuestions)
            {
                theResults = new Results(numCorrect,numQuestions);
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

        private void displayNextQuestion()
        {
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
            this.Hide();
        }

        private void postQuestion()
        {
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
    }
}

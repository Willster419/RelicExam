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

namespace WoTExam
{
    public partial class QuestionViewer : Form
    {
        //list all static objects to exist throughout the program
        public int numQuestions;
        private string catagory;
        private string map;
        private int currentQuestion;
        Question loadedQuestion;
        public int numCorrect;
        public bool correct;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private string tempPath;
        public string dataBasePath;
        public string questionPath;
        public string questionBase;
        public string picturePath;
        private List<Question> questionDisplayList;
        private Random rng;
        private Results theResults;
        private int timeLeft;
        public bool canSubmit;
        private PhotoViewer foto;
        private Point pictureSpawnPoint;
        //generic constructor
        public QuestionViewer()
        {
            InitializeComponent();
        }
        //contstructor that somehow knows which type it is. honestly i forget how i got this to work, it just does
        public QuestionViewer(int numQuestionss, string catagoryy, string mapp, List<Question> QList, List<Catagory> CList, List<Map> MList)
        {
            InitializeComponent();
            numQuestions = numQuestionss;
            catagory = catagoryy;
            map = mapp;
            questionList = QList;
            catagoryList = CList;
            mapList = MList;
        }
        //the countdown for how much time you have left to answer
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
        //self-explanatory
        private void submitAnswer_Click(object sender, EventArgs e)
        {
            submitAnswer.Enabled = false;
            this.postQuestion();
        }
        //called when the questionviewer is loaded (TODO: one or multiple times)
        private void QuestionViewer_Load(object sender, EventArgs e)
        {
            //parse all file paths
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            picturePath = dataBasePath + "\\pictures";
            //new up everything
            theResults = new Results();
            questionDisplayList = new List<Question>();
            rng = new Random();
            //reset  stuff
            currentQuestion = 0;
            timer1.Stop();
            timer1.Enabled = false;
            currentQuestion = 0;
            outOfTime.Visible = false;
            //determine if filtering is required
            if (catagory == null && map == null)
            {
                //no filtering required
            }
            else if (catagory != null)
            {
                //filtering of type catagory
                int catTotal = questionList.Count;
                List<Question> filteredList = new List<Question>();
                for (int i = 0; i < catTotal; i++)
                {
                    if (catagory.Equals(questionList[i].cat.getCatagory()))
                    {
                        filteredList.Add(questionList[i]);
                    }
                }
                questionList = filteredList;
            }
            else
            {
                //filtering of type map
                int mapTotal = questionList.Count;
                List<Question> filteredList = new List<Question>();
                for (int i = 0; i < mapTotal; i++)
                {
                    if (map.Equals(questionList[i].m.getMap()))
                    {
                        filteredList.Add(questionList[i]);
                    }
                }
                questionList = filteredList;
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
        //rage quit the exam
        private void rageQuit_Click(object sender, EventArgs e)
        {
            //RAGEQUIT BUTTON
            if (foto != null) foto.Close();
            this.Hide();
        }
        //get the next question
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
            foto = new PhotoViewer(pictureSpawnPoint,3);
            foto.Location = pictureSpawnPoint;
            if (loadedQuestion.p.photoFileName.Equals("null.jpg") || loadedQuestion.p.photoFileName.Equals("NONE"))
            {
                //no picture to display
            }
            else
            {
                foto.parsePicture(loadedQuestion.p);
                foto.Show();
            }
            
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
        //called when the question viewer is about to be closed
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
    }
}

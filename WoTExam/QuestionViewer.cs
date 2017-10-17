using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WoTExam
{
    public partial class QuestionViewer : Form
    {
        //list all static objects to exist throughout the program
        public List<Question> Questions;
        private int QuestionCounter = -1;
        private int numCorrect = 0;
        private bool allowNext;
        //generic constructor
        public QuestionViewer()
        {
            InitializeComponent();
        }
        //the countdown for how much time you have left to answer
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLeft.Value++;
            numSecondsLeft.Text = "" + (TimeLeft.Maximum - TimeLeft.Value) + " Seconds remain";
            if(TimeLeft.Maximum+1 == TimeLeft.Value)
            {
                numSecondsLeft.Text = "Time Expired";
                AnswerExplanation.Text = Questions[QuestionCounter].Explanation;
                allowNext = true;
            }
        }
        //self-explanatory
        private void SubmitAnswer_Click(object sender, EventArgs e)
        {
            if (!ValidSubmission())
            {
                return;
            }
            else if (IsUserCorrect())
            {
                numSecondsLeft.Text = "Correct!";
                numCorrect++;
            }
            else
            {
                numSecondsLeft.Text = "Incorrect!";
            }
            Timer.Stop();
            allowNext = true;
            TimeLeft.Value = TimeLeft.Maximum;
            foreach(Answer ans in Questions[QuestionCounter].Answers)
            {
                if (ans.IsCorrect)
                    ans.AnswerButton.BackColor = System.Drawing.Color.Green;
                else
                    ans.AnswerButton.BackColor = System.Drawing.Color.Red;
            }
            AnswerExplanation.Text = Questions[QuestionCounter].Explanation;
        }

        private void QuestionViewer_Load(object sender, EventArgs e)
        {
            Timer.Stop();
            allowNext = true;
        }
        //rage quit the exam
        private void RageQuit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        //get the next question
        private void NextQuestion_Click(object sender, EventArgs e)
        {
            if (!allowNext)
                return;
            if (!SubmitAnswer.Enabled)
            {
                SubmitAnswer.Enabled = true;
                allowNext = false;
            }
            if (QuestionCounter + 1 == Questions.Count)
            {
                using (Results r = new Results()
                {
                    numCorrect = this.numCorrect,
                    totalQuestions = this.Questions.Count
                })
                {
                    r.ShowDialog();
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                DisplayQuestion();
            }
        }
        //called when the question viewer is about to be closed
        private void QuestionViewer_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        //displays the next question
        private void DisplayQuestion()
        {
            QuestionCounter++;
            QuestionNumber.Text = "Question " + QuestionCounter + " of " + Questions.Count;
            QuestionCategory.Text = "Category: " + Questions[QuestionCounter].Category;
            QuestionTitle.Text = "Title: " + Questions[QuestionCounter].Title;
            QuestionText.Text = Questions[QuestionCounter].Text;
            AnswerExplanation.Text = "";
            TimeLeft.Minimum = 0;
            TimeLeft.Maximum = Questions[QuestionCounter].TimeToAnswer;
            PictureViewer.Image = WoTExam.Properties.Resources.LoadingImage;
            if (Questions[QuestionCounter].Picture.Equals("none"))
            {
                PictureViewer.Visible = false;
                PictureViewer.Image = null;
            }
            else
            {
                PictureViewer.Visible = true;
                PictureViewer.LoadAsync(Questions[QuestionCounter].Picture);
            }
            foreach (Answer ans in Questions[QuestionCounter].Answers)
            {
                RadioButton rb = new RadioButton()
                {
                    Text = ans.Text,
                    Location = new System.Drawing.Point(6, GetYLocation(AnswersPanel.Controls))
                };
                ans.AnswerButton = rb;
                AnswersPanel.Controls.Add(rb);
            }
            Timer.Start();
        }

        private bool ValidSubmission()
        {
            foreach(Answer ans in Questions[QuestionCounter].Answers)
            {
                if (ans.AnswerButton.Checked)
                    return true;
            }
            return false;
        }

        private bool IsUserCorrect()
        {
            foreach (Answer ans in Questions[QuestionCounter].Answers)
            {
                if (ans.AnswerButton.Checked && ans.IsCorrect)
                    return true;
            }
            return false;
        }

        private int GetYLocation(Control.ControlCollection Controls)
        {
            int y = 0;
            foreach(Control c in Controls)
            {
                y += c.Size.Height;
                y += 3;
            }
            return y;
        }
    }
}

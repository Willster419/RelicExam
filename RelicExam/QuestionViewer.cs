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

namespace RelicExam
{
    public partial class QuestionViewer : Form
    {
        int numQuestions;
        string catagory;
        string map;
        int currentQuestion = 0;
        Question loadedQuestion;
        bool correct;
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

        }

        private void submitAnswer_Click(object sender, EventArgs e)
        {

        }

        private void QuestionViewer_Load(object sender, EventArgs e)
        {
            //load the questions into memory:
            //parse questionBase
            //parse questions
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //RAGEQUIT BUTTON

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NEXT BUTTON

        }

        private void displayNextQuestion()
        {
            //display every single UI componet of the question

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RelicExam
{
    class Question
    {
        //all parameters we will need in this class
        public string title;
        public string catagory;
        public string theQuestion;
        public string responseA;
        public string responseB;
        public string responseC;
        public bool responseCEnabled;
        public string responseD;
        public bool responseDEnabled;
        public string answer;
        public int timeToAnswer;
        public string explanationOfAnswer;
        public string map;
        public Question(){}
        public override string ToString()
        {
            return title + " - " + theQuestion.Substring(0, 20);
        }
    }
}

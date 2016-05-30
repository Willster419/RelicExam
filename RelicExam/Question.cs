﻿using System;
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
        public Catagory cat;
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
        public Map m;
        public Question(){
            cat = new Catagory();
            m = new Map();
        }
        public override string ToString()
        {
            return title + " - " + theQuestion;
        }
    }
}
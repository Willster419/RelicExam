using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RelicExam
{
    public class Question
    {
        //all parameters we will need in this class
        public string title{get; set;}
        public Catagory cat{get; set;}
        public string theQuestion{get; set;}
        public string responseA{get; set;}
        public string responseB{get; set;}
        public string responseC{get; set;}
        public bool responseCEnabled{get; set;}
        public string responseD{get; set;}
        public bool responseDEnabled{get; set;}
        public string answer{get; set;}
        public int timeToAnswer{get; set;}
        public string explanationOfAnswer{get; set;}
        public Map m{get; set;}
        public Picture p{get; set;}
        public Question(){
            cat = new Catagory();
            m = new Map();
            p = new Picture();
        }
        public override string ToString()
        {
            return title + " - " + theQuestion;
        }
    }
}

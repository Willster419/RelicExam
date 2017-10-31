using System.Collections.Generic;


namespace WoTExam
{
    public class Question
    {
        //all parameters we will need in this class
        public string Version { get; set; }
        public string LastUpdated { get; set; }
        public string UpdateNote { get; set; }
        public string Title{get; set;}
        public string Category{get; set;}
        public string Text{get; set;}
        public int TimeToAnswer{get; set;}
        public string Picture{get; set;}
        public string Explanation { get; set; }
        public List<Answer> Answers = new List<Answer>();
        public Question()
        {
            Picture = "";
        }
        public override string ToString()
        {
            return Title + " - " + Text;
        }
    }
}

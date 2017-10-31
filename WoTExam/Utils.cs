using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;

namespace WoTExam
{
    public static class Utils
    {

        private static object _locker = new object();
        private static Random rng = new Random();

        public static void AppendToLog(string message)
        {
            //avoid that 2 or more threads calling the Log function
            lock (_locker)              
            {
                string filePath = Path.Combine(Application.StartupPath, "WoTExam.log");
                string text = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}   {1}\n", DateTime.Now, message);
                File.AppendAllText(filePath, text);
            }
        }
        public static void LogException(Exception e)
        {
            lock (_locker)              // avoid that 2 or more threads calling the Log function and writing lines in a mess
            {
                string msg = "EXCEPTION\n";
                msg = msg + "type: " + e.GetType().ToString() + "\n";
                msg = msg + "inner message: " + e.Message + "\n";
                msg = msg + "inner exception: " + e.InnerException + "\n";
                msg = msg + "source: " + e.Source + "\n";
                msg = msg + "stack trace:\n" + e.StackTrace + "\n";
                Utils.AppendToLog(msg);
            }
        }
        public static bool SaveQuestions(List<Question> questionList, string saveLocation)
        {

            return true;
        }
        public static bool LoadQuestions(List<Question> questionList, string loadLocation)
        {
            if (!File.Exists(loadLocation))
                return false;
            XDocument doc = new XDocument();
            try
            {
                doc = XDocument.Load(loadLocation);
                foreach (var question in doc.XPathSelectElements("/Questions/Question"))
                {
                    Question q = new Question()
                    {
                        Version = question.Attribute("Version").Value,
                        LastUpdated = question.Attribute("LastUpdated").Value,
                        UpdateNote = question.Attribute("UpdateNote").Value,
                        Category = question.Attribute("Category").Value,
                        Title = question.Element("Title").Value,
                        Text = question.Element("Text").Value,
                        TimeToAnswer = int.Parse(question.Element("TimeToAnswer").Value),
                        Picture = question.Element("Picture").Value,
                        Explanation = question.Element("Explanation").Value
                    };
                    q.Answers.Clear();
                    foreach (var answer in question.XPathSelectElements("Answers/Answer"))
                    {
                        q.Answers.Add(new Answer()
                        {
                            IsCorrect = bool.Parse(answer.Attribute("IsCorrect").Value),
                            Text = answer.Attribute("Text").Value
                        });
                    }
                    questionList.Add(q);
                }
            }
            catch (Exception e)
            {
                Utils.LogException(e);
                MessageBox.Show("Failed to parse questions. Check the log file for more info.");
                Application.Exit();
            }
            return true;
        }
        //https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

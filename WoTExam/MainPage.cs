//Declare all libraries we will be using
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;

namespace WoTExam
{
    public partial class MainPage : Form
    {
        //All variables that will exist throughout the lifetime of the program
        private Clan SelectedClan = null;
        private List<Question> Questions;
        XDocument InfoXML = new XDocument();

        public MainPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// gets now the "Release version" from RelhaxModpack-properties
        /// https://stackoverflow.com/questions/2959330/remove-characters-before-character
        /// https://www.mikrocontroller.net/topic/140764
        /// </summary>
        /// <returns></returns>
        public string ManagerVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().IndexOf('.') + 1);
        }
        //called when the application has finished system loading
        private void MainPage_Load(object sender, EventArgs e)
        {
            //start log entries
            Utils.AppendToLog("|--------------------------------------------------------------------------------------------------------------------------|");
            Utils.AppendToLog("|WotExam " + ManagerVersion());
            Utils.AppendToLog(string.Format("|Built on {0}", CiInfo.BuildTag));
            Utils.AppendToLog("|Running on " + System.Environment.OSVersion.ToString());
            //check for updates
            InfoXML = XDocument.Load(Path.Combine(Application.StartupPath, "info.xml"));
            string onlineVersion = InfoXML.XPathSelectElement("/info.xml/version").Value;
            if(!onlineVersion.Equals(ManagerVersion()))
            {
                using
                (
                    Updater u = new Updater()
                    {
                        UpdateMessage = InfoXML.XPathSelectElement("/info.xml/versionNotes").Value
                    }
                )
                {
                    u.ShowDialog();
                    Application.Exit();
                }
            }
            //load clan list
            NumQuestions.Items.Clear();
            ClanList.Items.Clear();
            foreach (var clan in InfoXML.XPathSelectElements("info.xml/clans/clan"))
            {
                ClanList.Items.Add(new Clan()
                {
                    ClanName = clan.Value,
                    Filename = clan.Attribute("filename").Value,
                    Message = clan.Attribute("message").Value,
                    EditPassword = clan.Attribute("editPassword").Value
                });
            }
        }

        private void BeginTestButton_Click(object sender, EventArgs e)
        {
            if (SelectedClan == null)
                return;
            if (NumQuestions.SelectedIndex == -1)
                return;
            List<Question> possibleQuestions = new List<Question>();
            //parse the questions into a new list to give to the viewer
            //first make a list of all questions based on selected cateogry
            foreach (Object o in Categories.SelectedItems)
            {
                string cat = (string)o;
                foreach (Question q in Questions)
                {
                    if (cat.Equals(q.Category) && !possibleQuestions.Contains(q))
                        possibleQuestions.Add(q);
                }
            }
            Utils.Shuffle(possibleQuestions);
            using (QuestionViewer qv = new QuestionViewer() { Questions = this.Questions})
            {
                qv.ShowDialog();
            }
        }
        //the most secure password checker ever
        private void EditQuestions_Click(object sender, EventArgs e)
        {
            if (SelectedClan == null)
                return;
            using (EnterPassword enterPassword = new EnterPassword()
            {
                password = SelectedClan.EditPassword
            })
            {
                enterPassword.ShowDialog();
                if (enterPassword.DialogResult == DialogResult.OK)
                {
                    //show the editor form
                }
                else
                {
                    //denied
                    MessageBox.Show("No");
                }
            }
        }

        private void VerifyCode_Click(object sender, EventArgs e)
        {
            using (CodeVerify cv = new CodeVerify())
            {
                cv.ShowDialog();
            }
        }

        private void ClanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedClan = (Clan)ClanList.SelectedItem;
            ClanMessage.Text = SelectedClan.Message;
            Categories.Items.Clear();
            Categories.Items.Add("Loading...");
            NumQuestions.Items.Clear();
            if (Questions == null)
                Questions = new List<Question>();
            Questions.Clear();
            NumQuestions.Items.Clear();
            if(Utils.LoadQuestions(Questions, Path.Combine(Application.StartupPath, SelectedClan.Filename)))
            {
                for(int i = 1; i < Questions.Count+1; i++)
                {
                    NumQuestions.Items.Add(i);
                }
                Categories.Items.Clear();
                foreach(Question q in Questions)
                {
                    if (!Categories.Items.Contains(q.Category))
                        Categories.Items.Add(q.Category);
                }
            }
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utils.AppendToLog("|--------------------------------------------------------------------------------------------------------------------------|");
        }
    }
}

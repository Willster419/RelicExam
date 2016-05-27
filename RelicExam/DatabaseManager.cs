using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Microsoft.Win32;
using System.Threading;
using System.Xml;

namespace RelicExam
{
    public partial class DatabaseManager : Form
    {
        public XmlTextWriter questionBaseWriter;
        public XmlWriter playerBaseWriter;
        public XmlWriter playerWriter;
        public XmlWriter questionWriter;
        private Question tempQuestion;
        private Player tempPlayer;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionPath;
        public string playerPath;
        public string questionBase;
        public string playerBase;
        private PleaseWait wait;
        private string[] sampleCatagories;
        private string[] sampleMaps;
        private string questionPrefix = "question";
        private int questionNumber = 0;

        public DatabaseManager()
        {
            InitializeComponent();
        }

        private void DatabaseManager_Load(object sender, EventArgs e)
        {
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            playerPath = dataBasePath + "\\players";
            questionBase = "questionBase.xml";
            playerBase = "playerBase.xml";
            
            //declare all private classes
            tempQuestion = new Question();
            tempPlayer = new Player();

            //display a loading window and load the database
            wait = new PleaseWait();
            Application.DoEvents();
            wait.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            //this.loadDataBase();
            wait.Close();
        }

        private void loadDataBase()
        {
            if (!Directory.Exists(dataBasePath))
            {
                this.createDataBase();
                this.loadDataBase();
                return;
            }
            //more cool code here
        }

        private void createDataBase()
        {
            Directory.CreateDirectory(dataBasePath);
            Directory.CreateDirectory(questionPath);
            Directory.CreateDirectory(playerPath);
            this.setupSampleXmlFile();
        }

        private void setupSampleXmlFile()
        {
            //setup sample arrayLists
            sampleCatagories = new string[3];
            sampleCatagories[0] = "How2Pen";
            sampleCatagories[1] = "Command";
            sampleCatagories[2] = "Calling";
            sampleMaps = new string[3];
            sampleMaps[0] = "Campinovka";
            sampleMaps[1] = "Derpinberg";
            sampleMaps[2] = "Kittenguard";

            questionBaseWriter = new XmlTextWriter(questionPath + "\\" + questionBase, Encoding.UTF8);
            questionBaseWriter.Formatting = Formatting.Indented;

            questionBaseWriter.WriteWhitespace("");
            questionBaseWriter.WriteComment("Catagories List");
            questionBaseWriter.WriteStartElement("catagories");
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteElementString("catagory", sampleCatagories[0]);
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteElementString("catagory", sampleCatagories[1]);
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteElementString("catagory", sampleCatagories[2]);
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteEndElement();
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteWhitespace("\n");

            questionBaseWriter.WriteComment("Maps List");
            questionBaseWriter.WriteStartElement("maps");
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteElementString("map", sampleMaps[0]);
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteElementString("map", sampleMaps[1]);
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteElementString("map", sampleMaps[2]);
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.WriteEndElement();
            questionBaseWriter.WriteWhitespace("\n");
            questionBaseWriter.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(dataBasePath)) Directory.Delete(dataBasePath, true);
            this.createDataBase();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}

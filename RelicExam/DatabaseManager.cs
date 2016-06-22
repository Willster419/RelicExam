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
using System.Collections;
using System.Diagnostics;
using AppLimit.CloudComputing.SharpBox;
using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;
using System.Web;
using System.Security.Cryptography;

namespace RelicExam
{
    public partial class DatabaseManager : Form
    {
        public XmlTextWriter questionWriter;
        private Question tempQuestion;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        public string questionPath;
        public string playerPath;
        public string questionBase;
        private PleaseWait wait;
        private List<Question> questionList;
        private List<Map> mapList;
        private List<Catagory> catagoryList;
        private int lastNumber;
        private bool hasMadeChanges;
        private CloudStorage dropBoxStorage;
        private WebClient client = new WebClient();
        public bool close;
        private string pictureName;
        private PhotoViewer chooser;
        private List<Picture> pictureList;
        private Point pictureSpawnPoint;
        private string picturePath;
        private bool actuallyLoad;
        //basic constructor
        public DatabaseManager()
        {
            InitializeComponent();
        }
        //constructor that passes in the loaded question, map, and cataogry lists
        public DatabaseManager(List<Question> QList, List<Map> MList, List<Catagory> CList)
        {
            InitializeComponent();
            questionList = QList;
            mapList = MList;
            catagoryList = CList;
        }
        //called when the form is ready to be shown
        private void DatabaseManager_Load(object sender, EventArgs e)
        {
            //some boolean logic hack i don't know
            actuallyLoad = false;
            hasMadeChanges = false;
            //display a loading window and load the database
            wait = new PleaseWait();
            wait.Show();
            Application.DoEvents();
            //parse all file paths
            this.parseFilePaths();
            //declare all temp objects
            tempQuestion = new Question();
            //check for another user on the system
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/RelicExam/inUse.txt", tempPath + "\\inUse.txt");
                MessageBox.Show("Database is currently being edited, try again later");
                //this.Close();
                wait.Close();
                close = true;
            }
            catch (WebException)
            {
                //if none, you are the user now
                //make a random file to use a single instance file lock
                File.WriteAllText(tempPath + "\\inUse.txt", "the service is in use");
                dropBoxStorage = new CloudStorage();
                // get the configuration for dropbox
                var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
                // declare an access token
                ICloudStorageAccessToken accessToken = null;
                // load a valid security token from file
                using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
                }
                // open the connection 
                var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
                // get a specific directory in the cloud storage, e.g. /Public 
                var questionsFolder = dropBoxStorage.GetFolder("/Public/RelicExam");
                //get the file to upload
                String srcFile = Environment.ExpandEnvironmentVariables(tempPath + "\\inUse.txt");
                //upload the file and close the connection
                dropBoxStorage.UploadFile(srcFile, questionsFolder);
                dropBoxStorage.Close();
            }
            if (close)
            {
                this.Close();
                return;
            }
            //oh boy, more hacks i put in
            currentModeLabel.Visible = false;
            removeButton.Enabled = false;
            //close this first loading window
            wait.Close();
            //(OLD) load the database
            //don't need to now because the lists are passed in
            //this.loadDataBase(true);
            //create a list of pictures for the combobox
            //this way of doing it assumes that the database is in tact
            pictureList = new List<Picture>();
            foreach (string s in Directory.GetFiles(picturePath))
            {
                pictureList.Add(new Picture(s));
            }
            //reset the GUI
            this.resetGUI();
        }

        private void resetGUI()
        {
            questionComboBox.SelectedIndex = -1;
            mapComboBox.SelectedIndex = -1;
            catagoryComboBox.SelectedIndex = -1;
            while (questionComboBox.Items.Count > 1)
            {
                questionComboBox.Items.RemoveAt(1);
            }
            while (mapComboBox.Items.Count > 1)
            {
                mapComboBox.Items.RemoveAt(1);
            }
            while (catagoryComboBox.Items.Count != 0)
            {
                catagoryComboBox.Items.RemoveAt(0);
            }
            foreach (Question q in questionList)
            {
                questionComboBox.Items.Add(q);
            }
            foreach (Map m in mapList)
            {
                mapComboBox.Items.Add(m);
            }
            foreach (Catagory c in catagoryList)
            {
                catagoryComboBox.Items.Add(c);
            }
            while (photoComboBox.Items.Count > 1)
            {
                photoComboBox.Items.RemoveAt(1);
            }
            foreach (Picture pp in pictureList)
            {
                photoComboBox.Items.Add(pp);
            }
            questionTextBox.Text = "";
            responseATextBox.Text = "";
            responseBTextBox.Text = "";
            answerCEnable.Checked = false;
            responseCTextBox.Text = "";
            answerDEnable.Checked = false;
            responseDTextBox.Text = "";
            answerMarkA.Checked = false;
            answerMarkB.Checked = false;
            answerMarkC.Checked = false;
            answerMarkD.Checked = false;
            expTextBox.Text = "";
            theQuestionTitle.Text = "";
            timeToAnswerTextBox.Text = "" + "";
            currentModeLabel.Visible = false;
            this.photoComboBox_SelectedIndexChanged(null, null);
        }
        //event raised when the checkbox for answer d is changed
        private void answerDEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (answerMarkD.Checked)
            {
                MessageBox.Show("Cannot Disable the Answer Question!!");
                answerDEnable.Checked = true;
                return;
            }
            if (answerDEnable.Checked)
            {
                responseDTextBox.ReadOnly = false;
                answerMarkD.Enabled = true;
            }
            else
            {
                responseDTextBox.ReadOnly = true;
                answerMarkD.Enabled = false;
            }
        }
        //event raised when the question combo box sleection is changed
        private void questionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chooser != null)chooser.Close();
            actuallyLoad = true;
            addPictureButton.Enabled = true;
            pictureName = "";
            saveButton.Enabled = true;
            if (questionComboBox.SelectedIndex == -1) return;
            //determine if a new question is being made or if one is being updated
            if (questionComboBox.SelectedIndex == 0)
            {
                //warn the user this is "new question mode"
                currentModeLabel.Text = "CREATE";
                currentModeLabel.Visible = true;
                saveButton.Text = "create";
                removeButton.Enabled = false;
                photoComboBox.SelectedIndex = -1;
            }
            else
            {
                //warn the user this is "update question mode"
                mapComboBox.SelectedIndex = 0;
                currentModeLabel.Text = "UPDATE";
                currentModeLabel.Visible = true;
                saveButton.Text = "update";
                removeButton.Enabled = true;
                //load the selected question
                int questionIndex = questionComboBox.SelectedIndex;
                questionIndex--;
                Question q2Load = questionList[questionIndex];
                questionTextBox.Text = q2Load.theQuestion;
                responseATextBox.Text = q2Load.responseA;
                responseBTextBox.Text = q2Load.responseB;
                if (q2Load.responseCEnabled)
                {
                    answerCEnable.Checked = q2Load.responseCEnabled;
                    responseCTextBox.Text = q2Load.responseC;
                }
                if (q2Load.responseDEnabled)
                {
                    answerDEnable.Checked = q2Load.responseDEnabled;
                    responseDTextBox.Text = q2Load.responseD;
                }
                if (q2Load.answer.Equals("a"))
                {
                    answerMarkA.Checked = true;
                }
                if (q2Load.answer.Equals("b"))
                {
                    answerMarkB.Checked = true;
                }
                if (q2Load.answer.Equals("c"))
                {
                    answerMarkC.Checked = true;
                }
                if (q2Load.answer.Equals("d"))
                {
                    answerMarkD.Checked = true;
                }
                expTextBox.Text = q2Load.explanationOfAnswer;
                theQuestionTitle.Text = q2Load.title;
                timeToAnswerTextBox.Text = "" + q2Load.timeToAnswer;
                string questionCatagoryTemp = q2Load.cat.getCatagory();
                string questionMapTemp = q2Load.m.getMap();
                //for catagory and map, find the match in the List using for loop
                //use that int as the index set for the comboBox
                for (int i = 0; i < catagoryList.Count; i++)
                {
                    if (questionCatagoryTemp.Equals(catagoryList[i].getCatagory()))
                    {
                        catagoryComboBox.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < mapList.Count; i++)
                {
                    if (questionMapTemp.Equals(mapList[i].getMap()))
                    {
                        mapComboBox.SelectedIndex = ++i;
                        break;
                    }
                    //if it gets to here it means that the map is NONE
                    mapComboBox.SelectedIndex = 0;
                }
                //get the picture selected
                string tempPictureName = null;// q2Load.p.photoTitle;
                if (tempPictureName == null)
                {
                    photoComboBox.SelectedIndex = 0;
                    return;
                }
                if (tempPictureName.Equals("NONE") && tempPictureName !=null )
                {
                    photoComboBox.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0; i < pictureList.Count; i++)
                    {
                        if (true)//tempPictureName.Equals(pictureList[i].photoTitle))
                        {
                            photoComboBox.SelectedIndex = ++i;
                            break;
                        }
                    }
                }
                
            }
            photoComboBox_SelectedIndexChanged(null, null);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////
            //(seperate)
            //determine if the user is making a new question or updating one.
            //ask if they are sure
            //process the add/update request in memory
            //(same)
            //reflect the update in disk database (delete and re-create)
            //reload the entire database to memory
            //upload to dropbox
            //update the gui
            //////////////////////////////////////////////////////////////////
            //but first make sure everything is selected
            if (theQuestionTitle.Text.Equals("") || questionTextBox.Text.Equals("") || responseATextBox.Text.Equals("") || responseBTextBox.Text.Equals("") || timeToAnswerTextBox.Text.Equals("0") || (answerCEnable.Checked && responseCTextBox.Text.Equals("")) || (answerDEnable.Checked && responseDTextBox.Text.Equals("")) || (catagoryComboBox.Text.Equals("") && catagoryComboBox.SelectedIndex == -1) || (mapComboBox.Text.Equals("") && mapComboBox.SelectedIndex == -1) || (!answerMarkA.Checked && !answerMarkB.Checked && !answerMarkC.Checked && !answerMarkD.Checked) || photoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("please make sure everything is filled out properly before continuing!");
                return;
            }
            if (questionComboBox.SelectedIndex == 0)
            {
                //ask if the user is sure they would like to create this question
                DialogResult result = MessageBox.Show("Are you sure you would like to create this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    chooser.Close();
                    hasMadeChanges = true;
                    //procede...
                    //create a new question class with everything from the GUI
                    Question newQ = new Question();
                    newQ.theQuestion = questionTextBox.Text;
                    newQ.responseA = responseATextBox.Text;
                    newQ.responseB = responseBTextBox.Text;
                    if (answerCEnable.Checked)
                    {
                        newQ.responseCEnabled = answerCEnable.Checked;
                        newQ.responseC = responseCTextBox.Text;
                    }
                    if (answerDEnable.Checked)
                    {
                        newQ.responseDEnabled = answerDEnable.Checked;
                        newQ.responseD = responseDTextBox.Text;
                    }
                    if (answerMarkA.Checked)
                    {
                        newQ.answer = "a";
                    }
                    if (answerMarkB.Checked)
                    {
                        newQ.answer = "b";
                    }
                    if (answerMarkC.Checked)
                    {
                        newQ.answer = "c";
                    }
                    if (answerMarkD.Checked)
                    {
                        newQ.answer = "d";
                    }
                    newQ.explanationOfAnswer = expTextBox.Text;
                    newQ.title = theQuestionTitle.Text;
                    newQ.timeToAnswer = int.Parse(timeToAnswerTextBox.Text);
                    int catagoryInt = catagoryComboBox.SelectedIndex;
                    //newQ.cat.setCatagory(catagoryComboBox.SelectedText);
                    if (catagoryInt == -1)
                    {
                        //NEW CATAGORY
                        //catagoryList.Add(new Catagory(catagoryComboBox.Text));
                        newQ.cat.setCatagory(catagoryComboBox.Text);
                        catagoryComboBox.Text = "";
                    }
                    else
                    {
                        newQ.cat.setCatagory(catagoryList[catagoryInt].getCatagory());
                    }
                    //newQ.cat.setCatagory(catagoryList[catagoryInt].getCatagory());
                    int mapInt = mapComboBox.SelectedIndex-1;
                    //newQ.m.setMap(mapComboBox.SelectedText);
                    if (mapInt == -1)
                    {
                        newQ.m.setMap("NONE");
                    }
                    else if (mapInt == -2)
                    {
                        //NEW MAP
                        // mapList.Add(new Map(mapComboBox.Text));
                        newQ.m.setMap(mapComboBox.Text);
                        mapComboBox.Text = "";
                    }
                    else
                    {
                        newQ.m.setMap(mapList[mapInt].getMap());
                    }
                    //Determine if first entry in the database
                    if (catagoryList == null)
                    {
                        //First Entry of catagory
                        catagoryList = new List<Catagory>();
                    }
                    if (mapList == null)
                    {
                        //First Entry of catagory
                        mapList = new List<Map>();
                    }
                    //CATAGORY
                    //determine if it needs to create another entry for a new catagory
                    int numHits = 0;
                    //run through the catagory list and count number of times the catagory to add shows up
                    foreach (Catagory catTT in catagoryList)
                    {
                        if (newQ.cat.getCatagory().Equals(catTT.getCatagory())) numHits++;
                    }
                    //if it's 0 hits then add it
                    if (numHits == 0)
                    {
                        catagoryList.Add(new Catagory(newQ.cat.getCatagory()));
                    }
                    //MAP
                    //determine if it needs to create another entry for a new map
                    int numMapHits = 0;
                    //run through the map list and count number of times the map to add shows up
                    foreach (Map mapPP in mapList)
                    {
                        if (newQ.m.getMap().Equals(mapPP.getMap())) numMapHits++;
                    }
                    //if it's 0 hits then add it
                    if (numHits == 0)
                    {
                        if(!newQ.m.getMap().Equals("NONE"))
                        {
                            mapList.Add(new Map(newQ.m.getMap()));
                        }
                    }
                    //and add the question
                    if (questionList == null)
                    {
                        //FIRST ENTRY
                        questionList = new List<Question>();
                    }
                    //parse new picture
                    if (photoComboBox.SelectedIndex != 0)
                    {
                        newQ.p = pictureList[photoComboBox.SelectedIndex - 1];
                    }
                    else
                    {
                       //newQ.p = new Picture("NONE","null.jpg");
                    }

                    questionList.Add(newQ);
                    this.cleanupCatagories();
                    this.cleanUpPictures();
                    this.uploadButton_Click(null, null);
                    this.resetGUI();
                }
            }
            else
            {
                //ask if the user is sure they would like to update the selected question
                DialogResult result = MessageBox.Show("Are you sure you would like to update this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    chooser.Close();
                    hasMadeChanges = true;
                    //procede...
                    //load the question from the question index
                    Question q2Edit = questionList[questionComboBox.SelectedIndex-1];
                    q2Edit.theQuestion = questionTextBox.Text;
                    q2Edit.responseA = responseATextBox.Text;
                    q2Edit.responseB = responseBTextBox.Text;

                    q2Edit.responseCEnabled = answerCEnable.Checked;
                    if (answerCEnable.Checked)
                    {
                        q2Edit.responseC = responseCTextBox.Text;
                    }
                    else
                    {
                        q2Edit.responseC = "";
                    }

                    q2Edit.responseDEnabled = answerDEnable.Checked;
                    if (answerDEnable.Checked)
                    {
                        q2Edit.responseD = responseDTextBox.Text;
                    }
                    else
                    {
                        q2Edit.responseD = "";
                    }

                    if (answerMarkA.Checked)
                    {
                        q2Edit.answer = "a";
                    }
                    if (answerMarkB.Checked)
                    {
                        q2Edit.answer = "b";
                    }
                    if (answerMarkC.Checked)
                    {
                        q2Edit.answer = "c";
                    }
                    if (answerMarkD.Checked)
                    {
                        q2Edit.answer = "d";
                    }
                    q2Edit.explanationOfAnswer = expTextBox.Text;
                    q2Edit.title = theQuestionTitle.Text;
                    q2Edit.timeToAnswer = int.Parse(timeToAnswerTextBox.Text);
                    string oldCatagory = q2Edit.cat.getCatagory();
                    string oldMap = q2Edit.m.getMap();
                    int catagoryInt = catagoryComboBox.SelectedIndex;
                    //newQ.cat.setCatagory(catagoryComboBox.SelectedText);
                    if (catagoryInt == -1)
                    {
                        //NEW CATAGORY
                        //catagoryList.Add(new Catagory(catagoryComboBox.Text));
                        q2Edit.cat.setCatagory(catagoryComboBox.Text);
                        catagoryComboBox.Text = "";
                    }
                    else
                    {
                        q2Edit.cat.setCatagory(catagoryList[catagoryInt].getCatagory());
                    }
                    int mapInt = mapComboBox.SelectedIndex - 1;
                    //newQ.m.setMap(mapComboBox.SelectedText);
                    if (mapInt == -1)
                    {
                        q2Edit.m.setMap("NONE");
                    }
                    else if (mapInt == -2)
                    {
                        //NEW MAP
                       // mapList.Add(new Map(mapComboBox.Text));
                        q2Edit.m.setMap(mapComboBox.Text);
                        mapComboBox.Text = "";
                    }
                    else
                    {
                        q2Edit.m.setMap(mapList[mapInt].getMap());
                    }
                    //CATAGORY
                    //determine if the catagory has changed
                    if (catagoryComboBox.SelectedItem!=null)
                    {
                        //nothing needs to be done
                    }
                    else
                    {
                        //if so, determine if old one was the last one of a catagory
                        int numHitsRemove = 0;
                        //run through the catagory list and count number of times the catagory to remove shows up
                        foreach (Catagory cat in catagoryList)
                        {
                            if (cat.Equals(oldCatagory)) numHitsRemove++;
                        }
                        //and determine if new one needs a new entry for the new catagory
                        int numHitsAdd = 0;
                        foreach (Catagory cat in catagoryList)
                        {
                            if (cat.Equals(q2Edit.cat.getCatagory())) numHitsAdd++;
                        }
                        //if remove is only 1 then remove it
                        if (numHitsRemove == 1)
                        {
                            for (int i = 0; i < catagoryList.Count; i++)
                            {
                                string temp = catagoryList[i].getCatagory();
                                if (oldCatagory.Equals(temp))
                                {
                                    catagoryList.RemoveAt(i);
                                }
                            }
                        }
                        //if add is 0 then add it
                        if (numHitsAdd == 0)
                        {
                            catagoryList.Add(new Catagory(q2Edit.cat.getCatagory()));
                        }
                    }
                    //MAP
                    //determine if the map has changed
                    if (mapComboBox.SelectedItem!=null)
                    {
                        //nothing needs to be done
                    }
                    else
                    {
                        //if so, determine if old one was the last one of a map
                        int numMapHitsRemove = 0;
                        //run through the map list and count number of times the map to remove shows up
                        foreach (Map cat in mapList)
                        {
                            if (cat.Equals(oldMap)) numMapHitsRemove++;
                        }
                        //and determine if new one needs a new entry for the new map
                        int numMapHitsAdd = 0;
                        foreach (Map cat in mapList)
                        {
                            if (cat.Equals(q2Edit.m.getMap())) numMapHitsAdd++;
                        }
                        //if remove is only 1 then remove it
                        if (numMapHitsRemove == 1)
                        {
                            for (int i = 0; i < mapList.Count; i++)
                            {
                                string temp = mapList[i].getMap();
                                if (oldMap.Equals(temp))
                                {
                                    mapList.RemoveAt(i);
                                }
                            }
                        }
                        //if add is 0 then add it
                        if (numMapHitsAdd == 0)
                        {
                            mapList.Add(new Map(q2Edit.m.getMap()));
                        }
                    }
                    //PARSE picture
                    if (photoComboBox.SelectedIndex != 0)
                    {
                        q2Edit.p = pictureList[photoComboBox.SelectedIndex - 1];
                    }
                    else
                    {
                       // q2Edit.p = new Picture("NONE","null.jpg");
                    }

                    this.cleanupCatagories();
                    this.cleanUpPictures();;
                    this.uploadButton_Click(null, null);
                    this.resetGUI();
                }
            }
            this.saveButton.Enabled = false;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////
            //(seperate)
            //ask if the user is sure they would like to remove this question from the database
            //process the remove request in memory
            //(same)
            //reflect the update in disk database (delete and re-create)
            //reload the entire database to memory
            //update the gui
            /////////////////////////////////////////////////////////////////////////////////////
            DialogResult result = MessageBox.Show("Are you sure you would like to remove this question?", "Are you Sure", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                hasMadeChanges = true;
                //procede...
                int indexToRemove = questionComboBox.SelectedIndex;
                Question q2Rem = questionList[indexToRemove - 1];
                //CATAGORY
                //determine if it's the last one of a catagory
                int numHits = 0;
                Catagory tempCat = q2Rem.cat;
                string catT = tempCat.getCatagory();
                //run through the catagory list and count number of times the catagory to remove shows up
                /*foreach (Catagory cat in catagoryList)
                {
                    if (catT.Equals(cat.getCatagory())) numHits++;
                }*/
                foreach (Question q in questionList)
                {
                    if (catT.Equals(q.cat.getCatagory())) numHits++;
                }
                //if it's only 1 then remove it
                if (numHits == 1)
                {
                    for (int i = 0; i < catagoryList.Count; i++)
                    {
                        string temp = catagoryList[i].getCatagory();
                        if (catT.Equals(catagoryList[i].getCatagory()))
                        {
                            catagoryList.RemoveAt(i);
                        }
                    }
                }
                //MAP
                //determine if it's the last one of a map
                int numMapHits = 0;
                Map tempMap = q2Rem.m;
                string MapP = tempMap.getMap();
                //run through the catagory list and count number of times the catagory to remove shows up
                foreach (Map mapppp in mapList)
                {
                    string mapListString = mapppp.getMap();
                    if (MapP.Equals(mapListString)) numMapHits++;
                }
                //if it's only 1 then remove it
                if (numHits == 1)
                {
                    for (int i = 0; i < mapList.Count; i++)
                    {
                        if (MapP.Equals(mapList[i].getMap()))
                        {
                            mapList.RemoveAt(i);
                        }
                    }
                }
                //and remove the question
                questionList.RemoveAt(indexToRemove-1);
                this.cleanupCatagories();
                this.cleanUpPictures();
                this.uploadButton_Click(null, null);
                this.resetGUI();
                removeButton.Enabled = false;
            }
        }

        private void timeToAnswerTextBox_Leave(object sender, EventArgs e)
        {
            int temp = 0;
            try
            {
                if (timeToAnswerTextBox.Text.Equals(""))
                {
                    timeToAnswerTextBox.Text = "0";
                    return;
                }
                temp = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This must be a positive whole number");
                timeToAnswerTextBox.Text = "" + lastNumber;
            }
            if (temp < 0)
            {
                MessageBox.Show("This must be a positive whole number");
                timeToAnswerTextBox.Text = "" + lastNumber;
            }
        }

        private void timeToAnswerTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                lastNumber = int.Parse(timeToAnswerTextBox.Text);
            }
            catch (FormatException)
            {

            }
        }

        private void resetForm_Click(object sender, EventArgs e)
        {
            questionComboBox.SelectedIndex = -1;
            mapComboBox.SelectedIndex = -1;
            catagoryComboBox.SelectedIndex = -1;
            questionTextBox.Text = "";
            responseATextBox.Text = "";
            responseBTextBox.Text = "";
            answerCEnable.Checked = false;
            responseCTextBox.Text = "";
            answerDEnable.Checked = false;
            responseDTextBox.Text = "";
            answerMarkA.Checked = false;
            answerMarkB.Checked = false;
            answerMarkC.Checked = false;
            answerMarkD.Checked = false;
            expTextBox.Text = "";
            theQuestionTitle.Text = "";
            timeToAnswerTextBox.Text = "" + "";
            currentModeLabel.Visible = false;
            photoComboBox_SelectedIndexChanged(null, null);
        }
        private void cleanupCatagories()
        {
            //for each catagory
            //for each question
            //run through the list and see if it is still in use
            //CATAGORY
            int catagoryHits;
            string catagory;
            for (int i = 0; i < catagoryList.Count; i++)
            {
                catagoryHits = 0;
                catagory = catagoryList[i].getCatagory();
                for (int j = 0; j < questionList.Count; j++)
                {
                    if (questionList[j].cat.getCatagory().Equals(catagory)) catagoryHits++;
                }
                if (catagoryHits == 0) catagoryList.RemoveAt(i);
            }
            //MAP
            //(remember to exclude index 0 (NONE)
            int mapHits;
            string map;
            for (int i = 0; i < mapList.Count; i++)
            {
                mapHits = 0;
                map = mapList[i].getMap();
                for (int j = 0; j < questionList.Count; j++)
                {
                    if (questionList[j].m.getMap().Equals(map)) mapHits++;
                }
                if (mapHits == 0) mapList.RemoveAt(i);
            }
        }

        private void verifyCode_Click(object sender, EventArgs e)
        {
            CodeVerify v = new CodeVerify();
            v.ShowDialog();
        }
        //OLD! DO NOT USE!
        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (!hasMadeChanges)
            {
                MessageBox.Show("No changes Made");
                return;
            }
            //WORKING DROPBOX CODE FOR UPLOAD FILES
            string[] fileList = Directory.GetFiles(questionPath);
            string[] pictureLizt = Directory.GetFiles(picturePath);
            wait = new PleaseWait(fileList.Count()+pictureLizt.Count(), 0);
            int prog = 0;
            wait.Show();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            dropBoxStorage = new CloudStorage();
            // get the configuration for dropbox
            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            // declare an access token
            ICloudStorageAccessToken accessToken = null;
            // load a valid security token from file
            using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }
            wait.setProgress(prog++);
            // open the connection 
            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
            // get a specific directory in the cloud storage, e.g. /Public 
            var questionsFolder = dropBoxStorage.GetFolder("/Public/RelicExam/questions");
            dropBoxStorage.DeleteFileSystemEntry(questionsFolder);
            wait.setProgress(prog++);
            
            for (int i = 0; i < fileList.Count(); i++)
            {
                // upload a testfile from temp directory into public folder of DropBox
                String srcFile = Environment.ExpandEnvironmentVariables(fileList[i]);
                dropBoxStorage.UploadFile(srcFile, questionsFolder);
                wait.setProgress(prog++);
            }

            var questionsFolder2 = dropBoxStorage.GetFolder("/Public/RelicExam/pictures");
            dropBoxStorage.DeleteFileSystemEntry(questionsFolder2);
            for (int i = 0; i < pictureLizt.Count(); i++)
            {
                String srcFile = Environment.ExpandEnvironmentVariables(pictureLizt[i]);
                dropBoxStorage.UploadFile(srcFile, questionsFolder2);
                wait.setProgress(prog++);
            }

            dropBoxStorage.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            wait.Close();
        }

        private void answerCEnable_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void answerCEnable_Click(object sender, EventArgs e)
        {
            if (answerMarkC.Checked && !answerCEnable.Checked)
            {
                MessageBox.Show("Cannot Disable the Answer Question!!");
                answerCEnable.Checked = true;
                return;
            }
            if (answerCEnable.Checked)
            {
                responseCTextBox.ReadOnly = false;
                answerMarkC.Enabled = true;
            }
            else
            {
                responseCTextBox.ReadOnly = true;
                answerMarkC.Enabled = false;
            }
        }

        private void DatabaseManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ask if you want to save and upload your chances
            //do so
            //close the form
            PleaseWait pw = new PleaseWait();
            if(close){}
            else
            {
                
                pw.Show();
                Application.DoEvents();
                if (chooser != null) chooser.Close();
                dropBoxStorage = new CloudStorage();
                // get the configuration for dropbox
                var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
                // declare an access token
                ICloudStorageAccessToken accessToken = null;
                // load a valid security token from file
                using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
                }
                // open the connection 
                var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
                // get a specific directory in the cloud storage, e.g. /Public 
                //var questionsFolder = dropBoxStorage.GetFolder("/Public/RelicExam");
                //String srcFile = Environment.ExpandEnvironmentVariables(null);
                dropBoxStorage.DeleteFileSystemEntry("/Public/RelicExam/inUse.txt");
                //dropBoxStorage.UploadFile(srcFile, questionsFolder);
                dropBoxStorage.Close();
                pw.Close();
            }
        }

        private void addPictureButton_Click(object sender, EventArgs e)
        {
            if (chooser != null) chooser.Close();
            pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
            chooser = new PhotoViewer(pictureSpawnPoint);
            chooser.passInList(pictureList);
            chooser.Location = pictureSpawnPoint;
            string pictureLocationz;
            string extension;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                pictureLocationz = openFileDialog1.FileName;
                extension = Path.GetExtension(pictureLocationz);
                chooser.setPicture(pictureLocationz);
                chooser.ShowDialog();
            }
            if (chooser.cancel) return;
            string[] list = Directory.GetFiles(picturePath);
            string newName = picturePath + "\\picture" + list.Length + extension;
            File.Copy(pictureLocationz, picturePath + "\\picture" + list.Length + extension);
            //Picture temp = new Picture(chooser.photoNamee, newName);
            //pictureList.Add(temp);
            //update the picture combo box
            //photoComboBox.Items.Add(temp);
            photoComboBox.SelectedIndex = getPicture(chooser.photoNamee);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void photoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actuallyLoad)
            {
                //close and photoviewers 
                if (chooser != null) chooser.Close();
                if (photoComboBox.SelectedIndex == 0 || photoComboBox.SelectedIndex == -1)
                {
                    //don't show the photo viewer if there is no picture to show
                    return;
                }
                Picture tempPic = pictureList[photoComboBox.SelectedIndex - 1];
                //pictureName = tempPic.photoTitle;
                pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
                chooser = new PhotoViewer(pictureSpawnPoint);
                chooser.Location = pictureSpawnPoint;
                chooser.setPicture(tempPic.photoFileName);
                chooser.Show();
            }
        }
        //gets the index from pictureList of desired picture based on title
        private int getPicture(string title)
        {
            if (title.Equals("NONE")) return 0;
            for (int i = 0; i < pictureList.Count; i++)
            {
                if (true)//title.Equals(pictureList[i].photoTitle))
                {
                    return i+1;
                }
            }
            return 0;
        }
        //load every picture in the picture database(done?)
        //load every picture in the folder
        //for each picture in folder, run through the list and 
        //count the number of hits it is used using the picture's filePath
        private void cleanUpPictures()
        {
            for (int i = 0; i < pictureList.Count; i++)
            {
                int numHits = 0;
                for (int j = 0; j < questionList.Count; j++)
                {
                    if (pictureList[i].photoFileName.Equals(questionList[j].p.photoFileName)) numHits++;
                }
                if (numHits == 0)
                {
                    File.Delete(pictureList[i].photoFileName);
                    pictureList.RemoveAt(i);
                }
            }
        }
        //parses all file paths required upon application load
        public void parseFilePaths()
        {
            //parse all file paths
            appPath = Application.StartupPath;
            tempPath = Path.GetTempPath();
            dataBasePath = tempPath + "\\relicExamDatabase";
            questionPath = dataBasePath + "\\questions";
            playerPath = dataBasePath + "\\players";
            questionBase = "questionBase.xml";
            picturePath = dataBasePath + "\\pictures";
        }
        //gets a string md5 hash checksum of the input string. in this case, the input string is the file
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void saveUploadChangesButton_Click(object sender, EventArgs e)
        {
            //save and upload the changes
            //requires loading dialog
            //don't close
        }
    }
}

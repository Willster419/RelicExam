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

namespace WoTExam
{
    public partial class DatabaseManager : Form
    {
        //all the stuff we'll need
        private XmlTextWriter questionWriter;
        private Question tempQuestion;
        private PleaseWait wait;
        public List<Question> questionList;
        public List<Map> mapList;
        public List<Catagory> catagoryList;
        public List<Picture> pictureList;
        private CloudStorage dropBoxStorage;
        private WebClient client = new WebClient();
        private PhotoViewer chooser;
        private Point pictureSpawnPoint;
        private string tempPath;
        private string appPath;
        public string dataBasePath;
        private string picturePath;
        private string oldPicName;
        private string databaseFileName;
        private bool actuallyLoad;
        private bool hasMadeChanges;
        public bool close;
        public bool discardedChanges;
        private int lastNumber;
        private ArrayList completePictureList;
        private ArrayList localPictureList;
        private ArrayList remotePictureList;
        private ArrayList listToUpload;
        private ArrayList listToDownload;
        private List<Picture> removedPictures;
        //basic constructor
        public DatabaseManager()
        {
            InitializeComponent();
        }
        //constructor that passes in the loaded question, map, and cataogry lists
        public DatabaseManager(List<Question> QList, List<Map> MList, List<Catagory> CList, List<Picture>PList)
        {
            InitializeComponent();
            questionList = QList;
            mapList = MList;
            catagoryList = CList;
            pictureList = PList;
        }
        //called when the form is ready to be shown
        private void DatabaseManager_Load(object sender, EventArgs e)
        {
            //get this all to a sperate thread to be done in a worker
            wait = new PleaseWait(100, 0);
            this.Hide();
            Application.DoEvents();
            loadingWorker.RunWorkerAsync();
            wait.ShowDialog();
        }

        private void resetGUI()
        {
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
            questionComboBox.SelectedIndex = -1;
            mapComboBox.SelectedIndex = -1;
            catagoryComboBox.SelectedIndex = -1;
            mapComboBox.Text = "";
            catagoryComboBox.Text = "";
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
            timer1.Enabled = false;
            timer1.Stop();
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
        //event raised when the question combo box selection is changed
        private void questionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chooser != null)chooser.Close();
            actuallyLoad = true;
            addPictureButton.Enabled = true;
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
                string tempPictureName = q2Load.p.photoFileName;
                if (tempPictureName == null)
                {
                    photoComboBox.SelectedIndex = 0;
                }
                else if (tempPictureName.Equals("NONE") && tempPictureName !=null )
                {
                    photoComboBox.SelectedIndex = 0;
                }
                else if (tempPictureName.Equals("null.jpg") && tempPictureName != null)
                {
                    photoComboBox.SelectedIndex = 0;
                }
                else
                {
                    //find the picture in picturelist that matches this question's picture
                    for (int i = 0; i < pictureList.Count; i++)
                    {
                        if (tempPictureName.Equals(pictureList[i].photoFileName))
                        {
                            photoComboBox.SelectedIndex = 0;
                            photoComboBox.SelectedIndex = ++i;
                            break;
                        }
                    }
                }
                
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //determine if the user is making a new question or updating one.
            //ask if they are sure
            //process the add/update request in memory
            //reset the gui
            //but first make sure everything is selected
            if (theQuestionTitle.Text.Equals("") || questionTextBox.Text.Equals("") || responseATextBox.Text.Equals("") || responseBTextBox.Text.Equals("") || timeToAnswerTextBox.Text.Equals("0") || (answerCEnable.Checked && responseCTextBox.Text.Equals("")) || (answerDEnable.Checked && responseDTextBox.Text.Equals("")) || (catagoryComboBox.Text.Equals("") && catagoryComboBox.SelectedIndex == -1) || (mapComboBox.Text.Equals("") && mapComboBox.SelectedIndex == -1) || (!answerMarkA.Checked && !answerMarkB.Checked && !answerMarkC.Checked && !answerMarkD.Checked) || photoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("please make sure everything is filled out properly before continuing!");
                return;
            }
            if (questionComboBox.SelectedIndex == 0)

            {
                //ask if the user is sure they would like to create this question
                //stop the timer so it doesn't screw up the gui
                timer1.Stop();
                timer1.Enabled = false;
                DialogResult result = MessageBox.Show("Are you sure you would like to create this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    //delete the picture
                    File.Delete(chooser.photoNamee);
                    //remove from memory as well
                    pictureList.RemoveAt(pictureList.Count - 1);
                    photoComboBox.Items.RemoveAt(photoComboBox.Items.Count - 1);
                    this.resetGUI();
                }
                else
                {
                    if(chooser != null) chooser.Close();
                    hasMadeChanges = true;
                    unsavedChangesLabel.Visible = true;
                    saveUploadChangesButton.Enabled = true;
                    //create a new question and parse it from the gui
                    Question newQ = this.parseQuestion(new Question());
                    //add it to the list
                    questionList.Add(newQ);
                    //update the lists and reset the gui
                    this.updateCatagoryList();
                    this.updateMapList();
                    this.updatePictureList();
                    this.resetGUI();
                }
            }
            else
            {
                //ask if the user is sure they would like to update the selected question
                //stop the timer so it doesn't screw up the gui
                timer1.Stop();
                timer1.Enabled = false;
                DialogResult result = MessageBox.Show("Are you sure you would like to update this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    //delete the picture
                    File.Delete(chooser.photoNamee);
                    //remove from memory as well
                    pictureList.RemoveAt(pictureList.Count - 1);
                    photoComboBox.Items.RemoveAt(photoComboBox.Items.Count - 1);
                    this.resetGUI();
                }
                else
                {
                    if (chooser!=null)chooser.Close();
                    hasMadeChanges = true;
                    unsavedChangesLabel.Visible = true;
                    saveUploadChangesButton.Enabled = true;
                    Question whatTheActualFuck = questionList[questionComboBox.SelectedIndex - 1];
                    Picture fuckThis = whatTheActualFuck.p;
                    //load the question from the question index and parse it from the gui
                    Question test = this.parseQuestion(questionList[questionComboBox.SelectedIndex - 1]);
                    //replace the old question with the new one
                    questionList[questionComboBox.SelectedIndex - 1] = test;
                    //update the lists
                    this.updateCatagoryList();
                    this.updateMapList();
                    this.updatePictureList();
                    this.checkForPictureFileDeletion(fuckThis);
                    //reset the GUI
                    this.resetGUI();
                }
            }
        }
        //parses all gui values into a question
        private Question parseQuestion(Question q)
        {
            q.theQuestion = questionTextBox.Text;
            q.responseA = responseATextBox.Text;
            q.responseB = responseBTextBox.Text;
            if (answerCEnable.Checked)
            {
                q.responseCEnabled = answerCEnable.Checked;
                q.responseC = responseCTextBox.Text;
            }
            if (answerDEnable.Checked)
            {
                q.responseDEnabled = answerDEnable.Checked;
                q.responseD = responseDTextBox.Text;
            }
            if (answerMarkA.Checked)
            {
                q.answer = "a";
            }
            if (answerMarkB.Checked)
            {
                q.answer = "b";
            }
            if (answerMarkC.Checked)
            {
                q.answer = "c";
            }
            if (answerMarkD.Checked)
            {
                q.answer = "d";
            }
            q.explanationOfAnswer = expTextBox.Text;
            q.title = theQuestionTitle.Text;
            q.timeToAnswer = int.Parse(timeToAnswerTextBox.Text);
            q.cat.setCatagory(catagoryComboBox.Text);
            q.m.setMap(mapComboBox.Text);
            //go through the picture list to find where
            //this question's picture is located
            string trimmed = photoComboBox.Text;
            trimmed = trimmed.Trim();
            string[] nameToFind = trimmed.Split(new Char[]{'-'});
            for (int i = 0; i < nameToFind.Length; i++)
            {
                nameToFind[i] = nameToFind[i].Trim();
            }
            string photoTitle = nameToFind[0];
            if (photoTitle.Equals("NONE"))
            {
                //no picture with the question
                q.p = new Picture();
                q.p.photoTitle = "NONE";
                q.p.photoFileName = "NONE";
                return q;
            }
            else
            {
                string photoFileName = nameToFind[1];
                foreach (Picture p in pictureList)
                {
                    if (p.photoTitle.Equals(photoTitle) && p.photoFileName.Equals(photoFileName))
                    {
                        q.p = p;
                    }
                }
                return q;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            //ask if the user is sure they would like to remove this question from the database
            //process the remove request in memory
            //update the gui
            DialogResult result = MessageBox.Show("Are you sure you would like to remove this question?", "Are you Sure", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                hasMadeChanges = true;
                unsavedChangesLabel.Visible = true;
                saveUploadChangesButton.Enabled = true;
                //just remove the question at it's index
                Question test = questionList[questionComboBox.SelectedIndex - 1];
                //removedPictures.Add(test.p);
                questionList.RemoveAt(questionComboBox.SelectedIndex -1);
                //update the lists and reset the gui
                this.updateCatagoryList();
                this.updateMapList();
                this.updatePictureList();
                this.checkForPictureFileDeletion(test.p);
                this.resetGUI();
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
        //button to reset the form
        private void resetForm_Click(object sender, EventArgs e)
        {
            this.resetGUI();
        }

        private void verifyCode_Click(object sender, EventArgs e)
        {
            CodeVerify v = new CodeVerify();
            v.ShowDialog();
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
        //hook into when the form closes
        private void DatabaseManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close) return;
            //ask if you want to save and upload your chances
            //do so if so
            //close the form
            
            if (chooser != null) chooser.Close();
            if (hasMadeChanges)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes, would you like to save them?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    //close
                    discardedChanges = true;
                    this.Hide();
                }
                else
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    discardedChanges = false;
                    //save changes
                    wait = new PleaseWait(100, 0);
                    wait.databaseLoading.Text = "please wait, database saving...";
                    updateWorker.RunWorkerAsync();
                    wait.ShowDialog();
                    //close
                    this.Hide();
                }
            }
            //still need to remove the lock...
            wait = new PleaseWait(100, 0);
            wait.databaseLoading.Text = "please wait, removing single instance lock...";
            closeWorker.RunWorkerAsync();
            wait.ShowDialog();
        }

        private void addPictureButton_Click(object sender, EventArgs e)
        {
            if (chooser != null) chooser.Close();
            pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
            chooser = new PhotoViewer(pictureSpawnPoint,1);
            chooser.passInPhotoList(pictureList);
            chooser.Location = pictureSpawnPoint;
            string extension;
            string newName;
            Picture p;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                int i = 0;
                extension = Path.GetExtension(openFileDialog1.FileName);
                while (File.Exists(picturePath + "\\picture" + i + extension))
                {
                    i++;
                }
                newName = picturePath + "\\picture" + i + extension;
                File.Copy(openFileDialog1.FileName, newName);
                p = new Picture();
                p.photoFileName = Path.GetFileName(newName);
                chooser.parsePicture(p);
                chooser.ShowDialog();
            }
            if (chooser.cancel)
            {
                //delete the file and leave
                File.Delete(newName);
            }
            else
            {
                //add it to the list
                p.photoTitle = chooser.photoTitle;
                pictureList.Add(p);
                //determine if the user is making a new question
                //or modifying one
                //1=add, 2=edit
                if (chooser.getMode() == 1)
                {
                    bool temp = actuallyLoad;
                    actuallyLoad = false;
                    //set the selected index of the photoCOmboBox to the new picture
                    photoComboBox.Items.Add(p);
                    photoComboBox.SelectedIndex = photoComboBox.Items.Count -1;
                    actuallyLoad = temp;
                    //add
                    //force create the question
                    this.saveButton_Click(null, null);
                }
                else
                {
                    //edit
                    //attach it to the selected question
                    questionList[questionComboBox.SelectedIndex - 1].p = p;
                    //reset gui
                    this.resetGUI();
                }
            }
        }

        private void photoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actuallyLoad)
            {
                //close old pictureviewers if they exist 
                if (chooser != null) chooser.Close();
                if (photoComboBox.SelectedIndex == 0 || photoComboBox.SelectedIndex == -1)
                {
                    //don't show the photo viewer if there is no picture to show
                    return;
                }
                //get the picture to set
                Picture tempPic = pictureList[photoComboBox.SelectedIndex - 1];
                //do some spawn point stuff
                pictureSpawnPoint = new Point(this.Location.X + this.Width + 5, this.Location.Y);
                chooser = new PhotoViewer(pictureSpawnPoint,2);
                chooser.passInPhotoList(pictureList);
                chooser.Location = pictureSpawnPoint;
                //set the picture into the photoviewer
                chooser.parsePicture(tempPic);
                //backup the old picture name
                oldPicName = tempPic.photoFileName;
                chooser.Show();
                timer1.Enabled = true;
                timer1.Start();
            }
        }
        //parses all file paths required upon application load
        public void parseFilePaths()
        {
            //parse all file paths
            tempPath = Path.GetTempPath();
            appPath = Application.StartupPath;
            dataBasePath = tempPath + "\\relicExamDatabase";
            picturePath = dataBasePath + "\\pictures";
            databaseFileName = "\\questions.xml";
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
        //dumps changes to disk and uploads to dropbox
        private void saveUploadChangesButton_Click(object sender, EventArgs e)
        {
            //save and upload the changes
            //requires loading dialog
            //don't close
            timer1.Stop();
            timer1.Enabled = false;
            unsavedChangesLabel.Visible = false;
            hasMadeChanges = false;
            //save changes
            wait = new PleaseWait(100, 0);
            wait.databaseLoading.Text = "saving database...";
            updateWorker.RunWorkerAsync();
            wait.ShowDialog();
        }
        //updates the list of maps to reflect current questin list
        private void updateMapList()
        {
            bool duplicate = false;
            mapList = new List<Map>();
            foreach (Question q in questionList)
            {
                duplicate = false;
                foreach (Map c in mapList)
                {
                    if (q.m.getMap().Equals(c.getMap()))
                    {
                        duplicate = true;
                    }
                }
                if (!duplicate) mapList.Add(q.m);
            }
        }
        //updates the list of catagories to reflect current questin list
        private void updateCatagoryList()
        {
            //reset the catagory list
            //for each catagory in questionlist
            //traverse catagory list
            //if the catagory does not already exist
            //add it
            bool duplicate = false;
            catagoryList = new List<Catagory>();
            foreach (Question q in questionList)
            {
                duplicate = false;
                //going through each question
                foreach (Catagory c in catagoryList)
                {
                    //take the above question and traverse the catagoryList
                    if (q.cat.getCatagory().Equals(c.getCatagory()))
                    {
                        //break the inner for loop because it's a duplicate
                        duplicate = true;
                    }
                }
                //if we get here it means that it's a new catagory
                if(!duplicate) catagoryList.Add(q.cat);
            }
        }
        //updates the list of pictures to reflect current questin list
        private void updatePictureList()
        {
            bool duplicate = false;
            pictureList = new List<Picture>();
            foreach (Question q in questionList)
            {
                duplicate = false;
                foreach (Picture c in pictureList)
                {
                    if (q.p.photoFileName.Equals(c.photoFileName))
                    {
                        duplicate = true;
                    }
                }
                if (!duplicate && !q.p.photoFileName.Equals("NONE") && !q.p.photoFileName.Equals("null.jpg") && !q.p.photoTitle.Equals("NONE"))
                {
                    pictureList.Add(q.p);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chooser.nameChange)
            {
                //parse it in other questions
                for (int i = 0; i < questionList.Count; i++)
                {
                    if (questionList[i].p.photoFileName.Equals(oldPicName))
                    {
                        questionList[i].p.photoTitle = chooser.photoTitle;
                    }
                }
                //mark unsaved changes
                unsavedChangesLabel.Visible = true;
                saveUploadChangesButton.Enabled = true;
                hasMadeChanges = true;
                chooser.nameChange = false;
                this.resetGUI();
            }
        }

        private void checkForPictureFileDeletion(Picture pp)
        {
            //let go of the picture!!
            if (chooser != null) chooser.Close();
            if (pp.photoFileName.Equals("NONE") || pp.photoFileName.Equals("null.jpg")) return;
            //run through the pictureList
            //if no hits then delete the file
            int numHitz = 0;
            foreach (Picture p in pictureList)
            {
                if (p.photoFileName.Equals(pp.photoFileName))
                {
                    numHitz++;
                }
            }
            if (numHitz == 0)
            {
                //picture is not in use, delete it
                removedPictures.Add(pp);
            }
        }
        //dumps changes to disk and uploads to dropbox
        private void saveDatabase()
        {
            updateWorker.ReportProgress(1);
            //save the database xml
            string actualSaveName = databaseFileName.Substring(1);
            questionWriter = new XmlTextWriter(dataBasePath + databaseFileName, Encoding.UTF8);
            questionWriter.Formatting = Formatting.Indented;
            questionWriter.WriteStartDocument();
            questionWriter.WriteStartElement(actualSaveName);
            questionWriter.WriteStartElement("questions");
            for (int i = 0; i < questionList.Count; i++)
            {
                questionWriter.WriteStartElement("question");
                questionWriter.WriteElementString("title", questionList[i].title);
                questionWriter.WriteElementString("catagory", questionList[i].cat.getCatagory());
                questionWriter.WriteElementString("theQuestion", questionList[i].theQuestion);
                questionWriter.WriteElementString("responseA", questionList[i].responseA);
                questionWriter.WriteElementString("responseB", questionList[i].responseB);
                questionWriter.WriteElementString("responseC", questionList[i].responseC);
                questionWriter.WriteElementString("responseCEnabled", "" + questionList[i].responseCEnabled);
                questionWriter.WriteElementString("responseD", questionList[i].responseD);
                questionWriter.WriteElementString("responseDEnabled", "" + questionList[i].responseDEnabled);
                questionWriter.WriteElementString("answer", questionList[i].answer);
                questionWriter.WriteElementString("timeToAnswer", "" + questionList[i].timeToAnswer);
                questionWriter.WriteElementString("explanationOfAnswer", questionList[i].explanationOfAnswer);
                questionWriter.WriteElementString("map", questionList[i].m.getMap());
                questionWriter.WriteElementString("picture", questionList[i].p.photoFileName);
                questionWriter.WriteElementString("pictureName", questionList[i].p.photoTitle);
                questionWriter.WriteEndElement();
            }
            questionWriter.WriteEndElement();
            questionWriter.WriteEndElement();
            questionWriter.Close();
            updateWorker.ReportProgress(20);
            //upload questions.xml
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
            updateWorker.ReportProgress(30);
            // open the connection 
            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
            updateWorker.ReportProgress(50);
            // get a specific directory in the cloud storage, e.g. /Public 
            var questionsFolder = dropBoxStorage.GetFolder("/Public/WoTExam/questions");
            updateWorker.ReportProgress(60);
            // upload xml database
            String srcFile = Environment.ExpandEnvironmentVariables(dataBasePath + databaseFileName);
            dropBoxStorage.UploadFile(srcFile, questionsFolder);
            updateWorker.ReportProgress(70);
            //delete unused pictures
            this.deleteUnusedPictures();
            //upload all the pictures
            //use cool algorithims here to determine if pictures need to be up or downloaded
            this.syncPictures();
            updateWorker.ReportProgress(75);
            //close the connection
            dropBoxStorage.Close();
            updateWorker.ReportProgress(95);
            System.Threading.Thread.Sleep(100);
            updateWorker.ReportProgress(100);
        }
        //where all the intensive work is done
        private void loadingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.loadForm();
        }
        //updates the waiting window
        private void loadingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            wait.setProgress(e.ProgressPercentage);
        }
        //called when the work is complete to close the waiting form
        private void loadingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //oh boy, more hacks i put in
            currentModeLabel.Visible = false;
            removeButton.Enabled = false;
            saveUploadChangesButton.Enabled = false;
            //close the loading window and reset the gui
            wait.Close();
            this.resetGUI();
            if (close) this.Close();
        }

        private void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.saveDatabase();
        }

        private void updateWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            wait.setProgress(e.ProgressPercentage);
        }

        private void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            wait.Close();
            this.resetGUI();
        }

        private void closeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.removeInstanceLock();
        }

        private void closeWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            wait.setProgress(e.ProgressPercentage);
        }

        private void closeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            wait.Close();
            this.Hide();
        }
        //method for loading the form
        private void loadForm()
        {
            removedPictures = new List<Picture>();
            //some boolean logic hack i don't know
            actuallyLoad = false;
            hasMadeChanges = false;
            discardedChanges = false;
            //parse all file paths
            this.parseFilePaths();
            //declare all temp objects
            tempQuestion = new Question();
            loadingWorker.ReportProgress(5);
            //check for another user on the system
            try
            {
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/WoTExam/inUse.txt", tempPath + "\\inUse.txt");
                MessageBox.Show("Database is currently being edited, try again later");
                close = true;
                loadingWorker.ReportProgress(15);
                return;
            }
            catch (WebException)
            {
                //if none, you are the user now
                //make a random file to use a single instance file lock
                File.WriteAllText(tempPath + "\\inUse.txt", "the service is in use");
                dropBoxStorage = new CloudStorage();
                loadingWorker.ReportProgress(20);
                // get the configuration for dropbox
                var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
                // declare an access token
                ICloudStorageAccessToken accessToken = null;
                // load a valid security token from file
                using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
                }
                loadingWorker.ReportProgress(25);
                // open the connection 
                var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
                loadingWorker.ReportProgress(50);
                // get a specific directory in the cloud storage, e.g. /Public 
                var questionsFolder = dropBoxStorage.GetFolder("/Public/WoTExam");
                loadingWorker.ReportProgress(75);
                //get the file to upload
                String srcFile = Environment.ExpandEnvironmentVariables(tempPath + "\\inUse.txt");
                //upload the file and close the connection
                dropBoxStorage.UploadFile(srcFile, questionsFolder);
                loadingWorker.ReportProgress(90);
                dropBoxStorage.Close();
                loadingWorker.ReportProgress(95);
            }
            if (close)
            {
                this.Close();
                return;
            }
            loadingWorker.ReportProgress(99);
            System.Threading.Thread.Sleep(100);
            loadingWorker.ReportProgress(100);
        }
        //removes the lock for the single instance of the databaseManger
        private void removeInstanceLock()
        {
            CloudStorage dropBoxStorage = new CloudStorage();
            closeWorker.ReportProgress(10);
            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            closeWorker.ReportProgress(20);
            ICloudStorageAccessToken accessToken = null;
            closeWorker.ReportProgress(25);
            using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
                closeWorker.ReportProgress(35);
            }
            closeWorker.ReportProgress(40);
            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
            closeWorker.ReportProgress(60);
            dropBoxStorage.DeleteFileSystemEntry("/Public/WoTExam/inUse.txt");
            closeWorker.ReportProgress(80);
            dropBoxStorage.Close();
            closeWorker.ReportProgress(99);
            System.Threading.Thread.Sleep(100);
            closeWorker.ReportProgress(100);
        }
        //creates a non-duplicate list of all pictures in use in the application
        //then uploads or downloads to make both sides have the latest changes
        private void syncPictures()
        {
            //create all lists 
            completePictureList = new ArrayList();
            localPictureList = new ArrayList();
            remotePictureList = new ArrayList();
            listToDownload = new ArrayList();
            listToUpload = new ArrayList();

            //add to list of remote pictures
            dropBoxStorage = new CloudStorage();
            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            ICloudStorageAccessToken accessToken = null;
            using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }
            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken); 
            ICloudDirectoryEntry pictureFolder = dropBoxStorage.GetFolder("/Public/WoTExam/pictures");
            foreach (ICloudFileSystemEntry entry in pictureFolder)
            {
                if (entry is ICloudDirectoryEntry)
                {
                    //directory
                }
                else
                {
                    //file
                    string theEntry = entry.Name;
                    remotePictureList.Add(theEntry);
                }
            }
            dropBoxStorage.Close();
            //add to list of local pictures
            foreach (Picture p in pictureList)
            {
                localPictureList.Add(p.photoFileName);
            }
            //merge the lists
            foreach (string s in localPictureList)
            {
                this.addPictureNameIfNotDuplicate(s);
            }
            foreach (string s in remotePictureList)
            {
                this.addPictureNameIfNotDuplicate(s);
            }
            //we now have a complete list of all pictures
            //check for uploads and downloads
            foreach (string s in completePictureList)
            {
                this.createDownloadQueue(s);
                this.createUploadQueue(s);
            }
            //then eithor upload of download
            foreach (string s in listToDownload)
            {
                //download
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/WoTExam/pictures/" + s,picturePath + "\\" + s);
            }
            //prepare upload first - save time
            CloudStorage dropBoxStorage2 = new CloudStorage();
            var dropBoxConfig2 = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            ICloudStorageAccessToken accessToken2 = null;
            using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken2 = dropBoxStorage2.DeserializeSecurityToken(fs);
            }
            var storageToken2 = dropBoxStorage2.Open(dropBoxConfig2, accessToken2);
            var picturesFolder2 = dropBoxStorage2.GetFolder("/Public/WoTExam/pictures");
            foreach (string s in listToUpload)
            {
                //upload
                String srcFile2 = Environment.ExpandEnvironmentVariables(picturePath + "\\" + s);
                dropBoxStorage2.UploadFile(srcFile2, picturesFolder2);
            }
            dropBoxStorage.Close();
        }

        private void addPictureNameIfNotDuplicate(string fileName)
        {
            bool duplicate = false;
            foreach (string s in completePictureList)
            {
                if (s.Equals(fileName)) duplicate = true;
            }
            if (!duplicate) completePictureList.Add(fileName);
        }

        private void createUploadQueue(string s)
        {
            bool located = false;
            foreach (string ss in remotePictureList)
            {
                if (ss.Equals(s)) located = true;
            }
            //can't find it on server means it needs to be uploaded
            if (!located) listToUpload.Add(s);
        }

        private void createDownloadQueue(string s)
        {
            bool located = false;
            foreach (string ss in localPictureList)
            {
                if (ss.Equals(s)) located = true;
            }
            //can't find it localy means it needs to be downloaded
            if (!located) listToDownload.Add(s);
        }

        private void testSync_Click(object sender, EventArgs e)
        {
            this.syncPictures();
        }

        private void deleteUnusedPictures()
        {
            //delete locally and remotely
            CloudStorage dropBoxStorage = new CloudStorage();
            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            ICloudStorageAccessToken accessToken = null;
            using (FileStream fs = File.Open(appPath + "\\key.txt", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }
            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
            foreach (Picture p in removedPictures)
            {
                if (File.Exists(picturePath + "\\" + p.photoFileName)) File.Delete(picturePath + "\\" + p.photoFileName);
                dropBoxStorage.DeleteFileSystemEntry("/Public/WoTExam/pictures/" + p.photoFileName);
            }
            dropBoxStorage.Close();
            removedPictures = new List<Picture>();
        }
    }
}

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
        private PhotoViewer chooser;
        private List<Picture> pictureList;
        private Point pictureSpawnPoint;
        private string picturePath;
        private bool actuallyLoad;
        private List<MoveQueue> mq;
        private string oldPicName;
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
            mq = new List<MoveQueue>();
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
            //reset the GUI
            this.resetGUI();
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
                    return;
                }
                if (tempPictureName.Equals("NONE") && tempPictureName !=null )
                {
                    photoComboBox.SelectedIndex = 0;
                }
                if (tempPictureName.Equals("null.jpg") && tempPictureName != null)
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
                DialogResult result = MessageBox.Show("Are you sure you would like to create this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    if(chooser != null) chooser.Close();
                    hasMadeChanges = true;
                    unsavedChangesLabel.Visible = true;
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
                DialogResult result = MessageBox.Show("Are you sure you would like to update this question?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (chooser!=null)chooser.Close();
                    hasMadeChanges = true;
                    unsavedChangesLabel.Visible = true;
                    //load the question from the question index and parse it from the gui
                    Question test = this.parseQuestion(questionList[questionComboBox.SelectedIndex - 1]);
                    //replace the old question with the new one
                    questionList[questionComboBox.SelectedIndex - 1] = test;
                    //update the lists
                    this.updateCatagoryList();
                    this.updateMapList();
                    this.updatePictureList();
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
            q.p.photoFileName = photoComboBox.Text;
            return q;
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
                //just remove the question at it's index
                Question test = questionList[questionComboBox.SelectedIndex - 1];
                questionList.RemoveAt(questionComboBox.SelectedIndex -1);
                //update the lists and reset the gui
                this.updateCatagoryList();
                this.updateMapList();
                this.updatePictureList();
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
            if (hasMadeChanges)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes, would you like to save them?", "Are you Sure", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    //save the changes

                }
            }










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
            chooser = new PhotoViewer(pictureSpawnPoint,1);
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
                string[] list = Directory.GetFiles(picturePath);
                extension = Path.GetExtension(openFileDialog1.FileName);
                newName = picturePath + "\\picture" + list.Length + extension;
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
                //attach it to the selected question
                questionList[questionComboBox.SelectedIndex - 1].p = p;
                this.resetGUI();
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
        //gets the index from pictureList of desired picture based on title
        private int getPicture(string title)
        {
            if (title.Equals("NONE")) return 0;
            for (int i = 0; i < pictureList.Count; i++)
            {
                if (title.Equals(pictureList[i].photoFileName))
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
            unsavedChangesLabel.Visible = false;
            hasMadeChanges = false;
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
                if (!duplicate) pictureList.Add(q.p);
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
                hasMadeChanges = true;
                chooser.nameChange = false;
                this.resetGUI();
            }
        }

    }
}

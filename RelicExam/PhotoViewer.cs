using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace RelicExam
{
    public partial class PhotoViewer : Form
    {
        public bool cancel;
        public string photoNamee;
        private Point startPoint;
        private List<Picture> thePictureList;
        private string photoPath;
        public PhotoViewer()
        {
            InitializeComponent();
            cancel = true;
        }
        public PhotoViewer(Point start)
        {
            InitializeComponent();
            cancel = true;
            startPoint = start;
        }
        public void setPicture(string pictureFile)
        {
            pictureBox1.Image = Image.FromFile(pictureFile);
            photoNamee = pictureFile;
            photoNamee = System.IO.Path.GetFileName(photoNamee);
            cancel = true;
        }

        private void PhotoViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pictureBox1.Image != null) pictureBox1.Image.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cancel
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //accept
            //take the save name and use that as the fileName
            //unless it is already taken
            //check for a valid name
            photoPath = System.IO.Path.GetTempPath() + "\\relicExamDatabase\\pictures";
            if(this.photoName.Text == null || this.photoName.Text.Equals(""))
            {
                //invalid name
                MessageBox.Show("invalid name");
            }
                //TODO:check for possible problems with the extension
            else if(System.IO.File.Exists(photoPath + "\\" + photoNamee))
            {
                //picture with that name already exists
                MessageBox.Show("picture with that name already exists");
            }
            else
            {
                //good to save the picture
                cancel = false;
                this.Close();
            }
        }

        private void PhotoViewer_Load(object sender, EventArgs e)
        {
            this.Location = startPoint;
        }

        public void passInList(List<Picture> theList)
        {
            thePictureList = theList;
        }
    }
}

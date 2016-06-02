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
        private string oldName;
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
            //checks for invalid photo names
            if (photoName.Text.Equals(""))
            {
                MessageBox.Show("Invalid name");
                return;
            }
            for (int i = 0; i < thePictureList.Count; i++)
            {
                if (thePictureList[i].photoTitle.Equals(photoName.Text))
                {
                    MessageBox.Show("Name is already taken");
                    photoName.Text = oldName;
                    return;
                }
            }
            photoNamee = photoName.Text;
            cancel = false;
            this.Close();
        }

        private void PhotoViewer_Load(object sender, EventArgs e)
        {
            this.Location = startPoint;
        }

        public void passInList(List<Picture> theList)
        {
            thePictureList = theList;
        }

        private void photoName_Leave(object sender, EventArgs e)
        {
            /*
            //checks for invalid photo names
            if(photoName.Text.Equals(""))
            {
                MessageBox.Show("Invalid name");
                return;
            }
            for (int i = 0; i < thePictureList.Count; i++)
            {
                if (thePictureList[i].Equals(photoName.Text))
                {
                    MessageBox.Show("Name is already taken");
                    photoName.Text = oldName;
                }
            }*/
        }

        private void photoName_Enter(object sender, EventArgs e)
        {
            oldName = photoName.Text;
        }
    }
}

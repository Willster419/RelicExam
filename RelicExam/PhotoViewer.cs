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
        public bool nameChange;
        public string photoPath;
        public string photoExtension;
        public string photoNamee;
        public string photoNameNoExt;
        public string photoTitle;
        private Point startPoint;

        public PhotoViewer()
        {
            InitializeComponent();
            cancel = true;
            nameChange = false;
        }

        public PhotoViewer(Point start, int mode)
        {
            InitializeComponent();
            cancel = true;
            nameChange = false;
            startPoint = start;
            this.setMode(mode);
        }
        //sets the pictureViewer to the picture on disk
        //also parses all string paths
        public void parsePicture(Picture pic)
        {
            photoPath = System.IO.Path.GetTempPath() + "\\relicExamDatabase\\pictures";
            photoNamee = photoPath + "\\" + pic.photoFileName;
            photoExtension = System.IO.Path.GetExtension(photoNamee);
            photoNameNoExt = System.IO.Path.GetFileNameWithoutExtension(photoNamee);
            pictureBox1.Image = Image.FromFile(photoNamee);
            photoTitle = pic.photoTitle;
            photoName.Text = photoTitle;
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
            //check for a valid name
            //update it
            if(this.photoName.Text == null || this.photoName.Text.Equals(""))
            {
                //invalid name
                MessageBox.Show("invalid name");
            }
                //TODO:check for possible problems with the extension
            else if (this.photoName.Text.Equals(photoTitle))
            {
                //same name as before, also prevents errors with renameing files
                MessageBox.Show("The name didn't change!!!");
            }
            else
            {
                //good to save the picture
                cancel = false;
                nameChange = true;
                photoTitle = photoName.Text;
                this.Close();
            }
        }

        private void PhotoViewer_Load(object sender, EventArgs e)
        {
            this.Location = startPoint;
        }
        //sets the "mode" of the photo viewer
        public void setMode(int m)
        {
            if (m == 1)
            {
                //add mode
                photoName.ReadOnly = false;
                button1.Enabled = true;
                button1.Text = "add picture";
                button2.Enabled = true;
                preview.Text = "ADD";
            }
            else if (m == 2)
            {
                //edit mode
                photoName.ReadOnly = false;
                button1.Enabled = true;
                button1.Text = "change name";
                button2.Enabled = false;
                preview.Text = "EDIT";
            }
            else if (m == 3)
            {
                //viewer mode
                photoName.ReadOnly = true;
                button1.Enabled = false;
                button2.Enabled = false;
                preview.Text = "VIEWER";
            }
            else
            {

            }
        }
    }
}

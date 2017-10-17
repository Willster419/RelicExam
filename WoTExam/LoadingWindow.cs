using System;
using System.Drawing;
using System.Windows.Forms;

namespace WoTExam
{
    public partial class LoadingWindow : Form
    {
        private float oldWidth;
        public LoadingWindow()
        {
            InitializeComponent();
        }
        
        public void setProgress(float prog)
        {
            float scaledValue = prog / 100;
            float newWidth = scaledValue * oldWidth;
            int newWidthInt = (int)newWidth;
            LoadingInnerPanel.Size = new Size(newWidthInt, LoadingInnerPanel.Size.Height);
            Application.DoEvents();
        }

        private void LoadingWindow_Load(object sender, EventArgs e)
        {

            oldWidth = LoadingInnerPanel.Size.Width;
            LoadingInnerPanel.Size = new Size(0, LoadingInnerPanel.Size.Height);
        }
    }
}

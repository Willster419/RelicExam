using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RelicExam
{
    public partial class PleaseWait : Form
    {
        public PleaseWait()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }
        public PleaseWait(int maxValue, int minValue)
        {
            InitializeComponent();
            progressBar1.Maximum = maxValue;
            progressBar1.Minimum = minValue;
        }
        public void setProgress(int prog)
        {
            if (progressBar1.Maximum < prog)
            {
                progressBar1.Value = progressBar1.Maximum;
            }
            else
            {
                progressBar1.Value = prog;
            }
            Application.DoEvents();
        }
    }
}

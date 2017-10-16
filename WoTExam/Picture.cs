using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoTExam
{
    public class Picture
    {
        public string photoFileName;
        public string photoTitle;
        public Picture()
        {

        }
        public Picture(string fotoFilename)
        {
            photoFileName = fotoFilename;
        }
        public override string ToString() 
        {
            return photoTitle + " - " + System.IO.Path.GetFileName(photoFileName);
        }
    }
}

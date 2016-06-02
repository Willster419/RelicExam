using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelicExam
{
    public class Picture
    {
        public string photoFileName;
        public string photoTitle;
        public Picture()
        {

        }
        public Picture(string title, string fotoFilename)
        {
            photoFileName = fotoFilename;
            photoTitle = title;
        }
        public Picture(string title)
        {
            //photoFileName = fotoname;
            photoTitle = title;
        }
        public override string ToString() 
        {
            return photoTitle;
        }
    }
}

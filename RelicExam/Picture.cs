using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelicExam
{
    public class Picture
    {
        public string photoFileName;
        public Picture()
        {

        }
        public Picture(string fotoFilename)
        {
            photoFileName = fotoFilename;
        }
        public override string ToString() 
        {
            return System.IO.Path.GetFileName(photoFileName);
        }
    }
}

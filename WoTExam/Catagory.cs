using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoTExam
{
    public class Catagory
    {
        private string theCatagory;
        public Catagory()
        {

        }
        public Catagory(string cat)
        {
            theCatagory = cat;
        }
        public string getCatagory()
        {
            return theCatagory;
        }
        public void setCatagory(string cat)
        {
            theCatagory = cat;
        }
        public override string ToString()
        {
            return theCatagory;
        }
    }
}

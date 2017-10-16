using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoTExam
{
    public class Map
    {
        private string theMap;
        public Map() { }
        public Map(string m) { theMap = m; }
        public void setMap(string m) { theMap = m; }
        public string getMap() { return theMap; }
        public override string ToString() {return theMap;}
    }
}

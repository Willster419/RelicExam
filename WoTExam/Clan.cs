using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoTExam
{
    public class Clan
    {
        public string Filename { get; set; }
        public string Message { get; set; }
        public string EditPassword { get; set; }
        public string ClanName { get; set; }
        public override string ToString()
        {
            return ClanName;
        }
    }
}

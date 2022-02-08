using System;
using System.Collections.Generic;
using System.Text;

namespace ASP.NET_Core_lesson1
{
    public class ThePost
    {
        public uint userId { get; set; }
        public uint id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public override string ToString()
        {
            string res = "";
            res += userId.ToString();
            res += "\n";
            res += id.ToString();
            res += "\n";
            res += title;
            res += "\n";
            res += body;
            return res;
        }
    }
}

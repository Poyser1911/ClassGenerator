using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaClassGenerator
{
    static class Helper
    {
        public static string GetAccessorSymbol(string a)
        {
            switch (a)
            {
                case "public": return "+"; 
                case "private": return "-";
                case "protected": return "#";

                default: return "?";
            }
        }
    }
}

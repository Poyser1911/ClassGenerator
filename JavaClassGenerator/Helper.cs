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
        public static string RemoveLast(string s,int count)
        {
            if (count >= 1)
                return s.Substring(0, s.Length - 1);
            else
                return s;
        }
        public static string GetDefaultVal(string type)
        {
            switch (type)
            {
                case"String": return "\"\"";
                case "int": return "0";
                case "double": return "0.0";
                case "boolean": return "false";
                case "char": return "' '";
                default: return "new "+type+"()";
            }

        }

        public static int IndexOfNth(this string str, string value, int nth = 1)
        {
            if (nth <= 0)
                throw new ArgumentException("Can not find the zeroth index of substring in string. Must start with 1");
            int offset = str.IndexOf(value);
            for (int i = 1; i < nth; i++)
            {
                if (offset == -1) return -1;
                offset = str.IndexOf(value, offset + 1);
            }
            return offset;
        }
    }
}

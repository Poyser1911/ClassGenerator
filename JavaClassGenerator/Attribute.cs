using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaClassGenerator
{
     class Attribute
    {
        public string name { get; set; }
        public string accessor { get; set; }
        public string type { get; set; }
        public string defaultval { get; set; }
        public bool isStatic = false;

        public Attribute Parse(string a)
        {
            Attribute attr = new Attribute();
            string[] parts = a.Split(' '); //spit it by spaces and ohh default val should be last cuz its gonna mess it up 
            attr.name = parts[0];
            attr.accessor = parts[1];
            attr.type = parts[2];
            attr.isStatic = (parts[3] == "True");
            int startindex = a.IndexOfNth(" ", 4);
            string rest = a.Substring(startindex, a.Length - (startindex)).Trim();
            attr.defaultval = (attr.type != "String") ? (rest == "") ? Helper.GetDefaultVal(attr.type) : rest : "\"" + rest + "\"";
            return attr;
        }
        public override string ToString()
        {
            return ((isStatic)?"static ":"")+this.type + " " + this.name + ";\n";
        }
        public string AsArg()
        {
            return this.type + " " + this.name + ",";
        }
        public string DefaultInit()
        {
            return "\t\t" + this.name + " = " + this.defaultval + ";\n";
        }
    }
}

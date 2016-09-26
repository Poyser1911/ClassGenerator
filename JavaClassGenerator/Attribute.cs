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

        public Attribute Parse(string a)
        {
            Attribute attr = new Attribute();
            string[] parts = a.Split(' ');
            attr.name = parts[0];
            attr.accessor = parts[1];
            attr.type = parts[2];
            attr.defaultval = (attr.type != "String") ? parts[3] : "\"" + parts[3] + "\"";
            return attr;
        }
        public override string ToString()
        {
            return this.type + " " + this.name + ";\n";
        }
    }
}

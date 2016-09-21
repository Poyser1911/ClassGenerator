using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaClassGenerator
{
    static class Parser
    {

        public static string Parse(string classname, List<Attribute> attributelist = null)
        {
            string result = String.Empty;
            //Class Header 
            result = "public class " + classname + "{\n";

            //Attributes
                foreach (Attribute Attribute in attributelist)
                    result += Attribute;

            //Default Constructor
            result += "\n\tpublic " + classname + "(){\n";
                foreach (Attribute Attribute in attributelist)
                    result += "\t\t" + Attribute.name + " = " + Attribute.defaultval + ";\n";

            result += "\t}\n";

            //Primary Constructor
            result += "\n\tpublic " + classname + "(";


            foreach (Attribute Attribute in attributelist)
                result += Attribute.type + " " + Attribute.name + ",";

            if (attributelist.Count != 0)
            result = result.Substring(0, result.Length - 1);

            result += "){\n";
            foreach (Attribute Attribute in attributelist)
                result += "\t\tthis." + Attribute.name + " = " + Attribute.name + ";\n";
            result += "\t}\n";

            //Copy Constructor
            result += "\n\tpublic " + classname + "(" + classname + " " + classname.ToLower() + "){\n";

            if (attributelist.Count != 0)
                foreach (Attribute Attribute in attributelist)
                    result += "\t\tthis." + Attribute.name + " = " + classname.ToLower() + ".get" + Attribute.name + "();\n";

            result += "\n\t}";
            //Getters
            foreach (Attribute Attribute in attributelist)
                result += "\n\tpublic " + Attribute.type + " get" + Attribute.name + "(){\n\t\treturn this." + Attribute.name + ";\n\t}\n";

            //Setters
            foreach (Attribute Attribute in attributelist)
                result += "\n\tpublic void set" + Attribute.name + "(" + Attribute.type+" "+Attribute.name+"){\n\t\tthis."+Attribute.name+" = "+Attribute.name+";\n\t}\n";

            //Display
            result += "\n\tpublic void display(){\n";
            foreach (Attribute Attribute in attributelist)
                result += "\t\tSystem.out.println(\""+Attribute.name+": \"+"+Attribute.name+");\n";

            return result += "\t}\n}";

        }
    }
}

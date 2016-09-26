using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaClassGenerator
{
    static class Parser
    {
        #region Java
        public static class Java
        {
            public static string Parse(string classname, List<Attribute> attributelist = null)
            {
                //First Character should be upper
                classname = classname == "" ? "MyClass" : classname.Substring(0, 1).ToUpper() + classname.Substring(1);
                string result = String.Empty;
                //Class Header 
                result = "public class " + classname + "{\n";

                //Attributes
                foreach (Attribute Attribute in attributelist)
                    result += "\t" + Attribute.accessor + " " + Attribute;

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
                List<string> getters = new List<string>();
                List<string> setters = new List<string>();
                //Getters
                foreach (Attribute Attribute in attributelist)
                    getters.Add("\n\tpublic " + Attribute.type + " get" + Attribute.name + "(){\n\t\treturn this." + Attribute.name + ";\n\t}\n");

                //Setters
                foreach (Attribute Attribute in attributelist)
                    setters.Add("\n\tpublic void set" + Attribute.name + "(" + Attribute.type + " " + Attribute.name + "){\n\t\tthis." + Attribute.name + " = " + Attribute.name + ";\n\t}\n");

                //Store them Alternating(getter below setter)
                for (int i = 0; i < attributelist.Count; i++)
                    result += getters[i] + setters[i];


                //Display
                result += "\n\tpublic void display(){\n";
                foreach (Attribute Attribute in attributelist)
                    result += "\t\tSystem.out.println(\"" + Attribute.name + ": \"+" + Attribute.name + ");\n";

                return result += "\t}\n}";

            }
        }
        #endregion

        #region Cpp
        public static class Cpp
        {
            public static string Parse(string classname, List<Attribute> attributelist = null)
            {
                //Change of abstract type String to string for C++
                for (int i = 0; i < attributelist.Count; i++)
                {
                    if (attributelist[i].type == "String")
                        attributelist[i].type = "string";

                    if (attributelist[i].type == "boolean")
                        attributelist[i].type = "bool";
                }

                //First Character should be upper
                classname = classname == "" ? "MyClass" : classname.Substring(0, 1).ToUpper() + classname.Substring(1);
                string result = String.Empty;
                //Class Header 
                result = "class " + classname + "\n\t{\n";

                //Private vars
                result += "\n\tprivate:\n";

                //Attributes
                foreach (Attribute Attribute in attributelist)
                    result += "\t" + Attribute;

                //Public vars
                result += "\n\tpublic:\n";

                //Default Constructor
                result += "\t" + classname + "()\n\t{\n";
                foreach (Attribute Attribute in attributelist)
                    result += "\t\t" + Attribute.name + " = " + Attribute.defaultval + ";\n";

                if (attributelist.Count != 0)
                    result = result.Substring(0, result.Length - 1);
                result += "\n\t}\n";

                //Primary Constructor
                result += "\n\t" + classname + "(";


                foreach (Attribute Attribute in attributelist)
                    result += Attribute.type + " " + Attribute.name + ",";

                if (attributelist.Count != 0)
                    result = result.Substring(0, result.Length - 1);

                result += ")\n\t{\n";
                foreach (Attribute Attribute in attributelist)
                    result += "\t\tthis->" + Attribute.name + " = " + Attribute.name + ";\n";

                if (attributelist.Count != 0)
                    result = result.Substring(0, result.Length - 1);
                result += "\n\t}\n";

                
                //Copy Constructor
                result += "\n\t" + classname + "(" + classname + "  &" + classname.ToLower() + ")\n\t{\n";

                if (attributelist.Count != 0)
                    foreach (Attribute Attribute in attributelist)
                        result += "\t\tthis->" + Attribute.name + " = " + classname.ToLower() + ".Get" + Attribute.name + "();\n";

                if (attributelist.Count != 0)
                    result = result.Substring(0, result.Length - 1);

                result += "\n\t}";
                List<string> getters = new List<string>();
                List<string> setters = new List<string>();
                //Getters
                foreach (Attribute Attribute in attributelist)
                    getters.Add("\n\t" + Attribute.type + " Get" + Attribute.name + "()\n\t{\n\t\treturn this->" + Attribute.name + ";\n\t}\n");

                //Setters
                foreach (Attribute Attribute in attributelist)
                    setters.Add("\n\tvoid Set" + Attribute.name + "(" + Attribute.type + " " + Attribute.name + ")\n\t{\n\t\tthis->" + Attribute.name + " = " + Attribute.name + ";\n\t}\n");

                //Store them Alternating(getter below setter)
                for (int i = 0; i < attributelist.Count; i++)
                    result += getters[i] + setters[i];


                //Display
                result += "\n\tvoid Display()\n\t{\n";
                foreach (Attribute Attribute in attributelist)
                    result += "\t\tcout << \"" + Attribute.name + ": \" << " + Attribute.name + " << endl;\n";

                if (attributelist.Count != 0)
                    result = result.Substring(0, result.Length - 1);

                return result += "\n\t}\n};";

            }
        }
        #endregion
    }
}

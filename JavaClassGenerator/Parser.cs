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
                string publicvars = string.Empty;
                string privatevars = string.Empty;
                string defaultinits = string.Empty;
                string primaryconsargs = string.Empty;
                string primaryinits = string.Empty;
                string copyconsinits = string.Empty;
                string printstrings = string.Empty;

                List<string> getters = new List<string>();
                List<string> setters = new List<string>();

                string result = String.Empty;

                //First Character should be upper
                classname = classname == "" ? "MyClass" : classname.Substring(0, 1).ToUpper() + classname.Substring(1);
                //Get Parts
                if (attributelist.Count >= 0)
                    for (int i = 0; i < attributelist.Count; i++)
                    {
                        if (attributelist[i].accessor == "private")
                            privatevars += "\tprivate " + attributelist[i];
                        else
                            publicvars += "\tpublic " + attributelist[i];

                        defaultinits += attributelist[i].DefaultInit();

                        primaryconsargs += attributelist[i].AsArg();

                        copyconsinits += "\t\tthis." + attributelist[i].name + " = " + classname.ToLower() + ".get" + attributelist[i].name + "();\n";

                        if (attributelist[i].isStatic)
                        {
                            setters.Add("\n\tpublic static void set" + attributelist[i].name + "(" + attributelist[i].type + " " + attributelist[i].name + "){\n\t\tthis." + attributelist[i].name + " = " + attributelist[i].name + ";\n\t}\n");
                            getters.Add("\n\tpublic static " + attributelist[i].type + " get" + attributelist[i].name + "(){\n\t\treturn this." + attributelist[i].name + ";\n\t}\n");
                        }
                        else
                        {
                            getters.Add("\n\tpublic " + attributelist[i].type + " get" + attributelist[i].name + "(){\n\t\treturn this." + attributelist[i].name + ";\n\t}\n");
                            setters.Add("\n\tpublic void set" + attributelist[i].name + "(" + attributelist[i].type + " " + attributelist[i].name + "){\n\t\tthis." + attributelist[i].name + " = " + attributelist[i].name + ";\n\t}\n");
                        }

                        if (attributelist[i].defaultval.Contains("new"))
                            printstrings += "\t\tSystem.out.println(\"" + attributelist[i].name + ": \"+" + attributelist[i].name + ".toString());\n";
                        else
                        printstrings += "\t\tSystem.out.println(\"" + attributelist[i].name + ": \"+" + attributelist[i].name + ");\n";

                        primaryinits += "\t\tthis." + attributelist[i].name + " = " + attributelist[i].name + ";\n";

                    }

                //Class Header 
                result = "public class " + classname + "{\n";

                //Attributes
                result += privatevars + publicvars;

                //Default Constructor
                result += "\n\tpublic " + classname + "(){\n";
                result += Helper.RemoveLast(defaultinits, attributelist.Count);
                result += "\n\t}\n";

                //Primary Constructor
                result += "\n\tpublic " + classname + "(";
                result += Helper.RemoveLast(primaryconsargs, attributelist.Count);
                result += "){\n";
                result += primaryinits;
                result += "\t}\n";

                //Copy Constructor
                result += "\n\tpublic " + classname + "(" + classname + " " + classname.ToLower() + "){\n";
                result += Helper.RemoveLast(copyconsinits, attributelist.Count);
                result += "\n\t}";

                //Store them Alternating(getter below setter)
                for (int i = 0; i < attributelist.Count; i++)
                    result += getters[i] + setters[i];


                //Display
                result += "\n\tpublic void display(){\n";
                result += Helper.RemoveLast(printstrings, attributelist.Count);
                return result += "\n\t}\n}";

            }
        }
        #endregion

        #region Cpp
        public static class Cpp
        {
            public static string Parse(string classname, List<Attribute> attributelist = null)
            {
                string publicvars = string.Empty;
                string privatevars = string.Empty;
                string protectedvars = string.Empty;
                string defaultinit = string.Empty;
                string primaryconsargs = string.Empty;
                string primaryinits = string.Empty;
                string copyconsinits = string.Empty;
                string printstrings = string.Empty;

                List<string> getters = new List<string>();
                List<string> setters = new List<string>();

                string result = String.Empty;


                //First Character should be upper
                classname = classname == "" ? "MyClass" : classname.Substring(0, 1).ToUpper() + classname.Substring(1);

                //Get Parts
                if (attributelist.Count >= 0)
                    for (int i = 0; i < attributelist.Count; i++)
                    {
                        if (attributelist[i].type == "String")
                            attributelist[i].type = "string";

                        if (attributelist[i].type == "boolean")
                            attributelist[i].type = "bool";
                        if (attributelist[i].defaultval.Contains("new"))
                            defaultinit += "\t\t"+attributelist[i].type + " " + attributelist[i].name + "();\n";
                        else
                            defaultinit += attributelist[i].DefaultInit();

                        if (attributelist[i].accessor == "private")
                            privatevars += "\t" + attributelist[i];
                        else if (attributelist[i].accessor == "public")
                            publicvars += "\t" + attributelist[i];
                        else
                            protectedvars += "\t" + attributelist[i];


                        primaryconsargs += attributelist[i].AsArg();

                        copyconsinits += "\t\tthis->" + attributelist[i].name + " = " + classname.ToLower() + "->Get" + attributelist[i].name + "();\n";
                        if (attributelist[i].isStatic)
                        {
                            getters.Add("\n\tstatic " + attributelist[i].type + " Get" + attributelist[i].name + "()\n\t{\n\t\treturn this->" + attributelist[i].name + ";\n\t}\n");
                            setters.Add("\n\tstatic void Set" + attributelist[i].name + "(" + attributelist[i].type + " " + attributelist[i].name + ")\n\t{\n\t\tthis->" + attributelist[i].name + " = " + attributelist[i].name + ";\n\t}\n");
                        }
                        else
                        {
                            getters.Add("\n\t" + attributelist[i].type + " Get" + attributelist[i].name + "()\n\t{\n\t\treturn this->" + attributelist[i].name + ";\n\t}\n");
                            setters.Add("\n\tvoid Set" + attributelist[i].name + "(" + attributelist[i].type + " " + attributelist[i].name + ")\n\t{\n\t\tthis->" + attributelist[i].name + " = " + attributelist[i].name + ";\n\t}\n");
                        }
                        if (attributelist[i].defaultval.Contains("new"))
                            printstrings += "\t\tcout << \"" + attributelist[i].name + ": \" << " + attributelist[i].name + ".ToString() << endl;\n";
                        else
                        printstrings += "\t\tcout << \"" + attributelist[i].name + ": \" << " + attributelist[i].name + " << endl;\n";

                        primaryinits += "\t\tthis->" + attributelist[i].name + " = " + attributelist[i].name + ";\n";

                    }


                //Class Header 
                result = "class " + classname + "\n{";

                //Private vars
                result += "\n\tprivate:\n";
                result += privatevars;

                //Protected vars
                result += "\n\tprotected:\n";
                result += protectedvars;

                //Public vars
                result += "\n\tpublic:\n";
                result += publicvars;


                //Default Constructor
                result += "\t" + classname + "()\n\t{\n";
                result += Helper.RemoveLast(defaultinit, attributelist.Count);
                result += "\n\t}\n";

                //Primary Constructor
                result += "\n\t" + classname + "(";
                result += Helper.RemoveLast(primaryconsargs, attributelist.Count);
                result += ")\n\t{\n";
                result += Helper.RemoveLast(primaryinits, attributelist.Count);
                result += "\n\t}\n";


                //Copy Constructor
                result += "\n\t" + classname + "(" + classname + "  &" + classname.ToLower() + ")\n\t{\n";
                result += Helper.RemoveLast(copyconsinits, attributelist.Count);
                result += "\n\t}";

                //Store them Alternating(getter below setter)
                for (int i = 0; i < attributelist.Count; i++)
                    result += getters[i] + setters[i];


                //Display
                result += "\n\tvoid Display()\n\t{\n";
                result += Helper.RemoveLast(printstrings, attributelist.Count);
                return result += "\n\t}\n};";

            }
        }
        #endregion
    }
}

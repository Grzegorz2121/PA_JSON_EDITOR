using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PA_JSON_EDITOR;
using System.IO;
using static Pa_Looker_2.Folder_tools;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;

using static PA_JSON_EDITOR.ModStructureManager;
using static PA_JSON_EDITOR.DataBaseControler;

namespace PA_JSON_EDITOR
{
    public partial class ScannerForm : Form
    {
        public class Property
        {
            public Property(string input_name, string input_type)
            {
                name = input_name;
                type = input_type;
                if ((type == "Object") || (type == "Array"))
                {
                    complex = "true";
                }
                else
                {
                    complex = "false";
                }
            }
            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                Property temp = obj as Property;
                if(temp.name==this.name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public string complex;
            public string name;
            public string type;
        } 


        public static Form main_form;
        protected static string main_path;

        public static HashSet<Property> featurelist = new HashSet<Property>();

        public ModStructureManager modStructure;

        public HashContainer hashContainer;

        public ScannerForm(Form parent, string path)
        {
            main_form = parent;
            main_path = path;
            InitializeComponent();
           // modStructure = new ModStructureManager(main_path);
        }

        private void Scan_button_Click(object sender, EventArgs e)
        {
            
            List<string> classes = new List<string>();
            List<string> units = new List<string>();
            List<string> jsons = new List<string>();
            
            List<string> temp = Directory.GetDirectories(main_path, "units", SearchOption.AllDirectories).ToList<string>();

            foreach (string u in temp)
            {
                classes.AddRange(Directory.GetDirectories(u).ToList<string>());
            }

            foreach(string c in classes)
            {
                units.AddRange(Directory.GetDirectories(c));
            }

            foreach(string u in units)
            {
                var t2 = u.Remove(0, Directory.GetParent(u).FullName.ToString().Length + 1);
                //List<string> t = Directory.GetFiles(u, t2+"_tool_weapon.json").ToList<string>();
                List<string> t = Directory.GetFiles(u, "*.json").ToList<string>();
                jsons.AddRange(t);
            }

            hashContainer = new HashContainer(jsons.ToArray<string>(), main_path);
            /*
            foreach(string s in jsons)
            {
                listBox1.Items.Add(Shorten_path(main_path,s));

                List<string> temp3 = richTextBox1.Lines.ToList<string>();
                temp3.Add(s);
                richTextBox1.Lines = temp3.ToArray<string>();

                
                StreamReader sr = new StreamReader(s);

                JToken jobject = JsonConvert.DeserializeObject(sr.ReadToEnd()) as JToken;
                foreach(JProperty p in jobject)
                {
                    featurelist.Add(new Property(p.Name, p.Value.Type.ToString()));
                }  
            }
        */
            foreach(Property p in featurelist)
            {
                listBox2.Items.Add(p.name);

                List<string> temp3 = richTextBox2.Lines.ToList<string>();
                temp3.Add(p.name);
                richTextBox2.Lines = temp3.ToArray<string>();
            }
        }

        private void Fill_Database()
        {
            
        }   
        
        private void Save_Properties_button_Click(object sender, EventArgs e)
        {
            hashContainer.CreateTheDump(main_path);
            /*
            DataBaseControler.SetDataBasePath(Application.StartupPath);
            DataBaseControler.CreateDataBase();
            // "name":"health_fraction", "type":"float", "description":"The amount of health remaining after being converted to wreckage.", "parent":"wreckage", "advanced":false, "context":"UINTTYPE_Air & UNITTYPE_Fabber"
            foreach (Property p in featurelist)
            {
                DataBaseControler.AddItem(p.name, p.type, complex:p.complex);
            }
                */
        }
    }
}

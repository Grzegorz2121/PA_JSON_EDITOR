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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;

namespace PA_JSON_EDITOR
{
    public partial class ScannerForm : Form
    {
        public HashContainer[] hashContainer = new HashContainer[4];
        public List<DataContainer> ScannedFilesData = new List<DataContainer>();

        public ScannerForm()
        {
            InitializeComponent();
        }

        private void Scan_button_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        
        private void Save_Properties_button_Click(object sender, EventArgs e)
        {
            foreach(HashContainer container in hashContainer)
            {
                container.CreateTheDump("");
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string inputPath = saveFileDialog1.FileName.Remove(saveFileDialog1.FileName.LastIndexOf(@"\"));

            List<string> keys = new List<string>()
            {
                "air",
                "land",
                "orbital",
                "sea"
            };

            foreach(string key in keys)
            {
                foreach(string directory in Directory.GetDirectories(inputPath, key, SearchOption.AllDirectories))
                {
                   foreach(string file in Directory.GetFiles(directory, "*.json", SearchOption.AllDirectories))
                   {
                        string[] temp = file.Split('\\');
                        if (temp[temp.Length-1].Remove(temp[temp.Length-1].LastIndexOf('.'))==temp[temp.Length-2])
                        {
                            listBox1.Items.Add(temp[temp.Length-1]);
                            ScannedFilesData.Add(new DataContainer(file));
                        }
                   }
                }
                
            }

            Console.WriteLine();
            /*
            hashContainer[0] = new HashContainer("", "Unit", "*.json", "*ammo.json|*tool_weapon.json|*build_arm.json");
            hashContainer[1] = new HashContainer("", "Ammo", "*ammo.json", "");
            hashContainer[2] = new HashContainer("", "ToolWeapon", "*tool_weapon.json", "");
            hashContainer[3] = new HashContainer("", "BuildArm", "*build_arm.json", "");*/
        }

    }
}

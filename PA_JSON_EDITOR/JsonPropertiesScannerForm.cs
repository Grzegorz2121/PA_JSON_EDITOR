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

    public partial class JsonPropertiesScannerForm : Form
    {

        /////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION

        public HashContainer[] hash_container = new HashContainer[4];
        public List<DataContainerMain> scanned_files_data = new List<DataContainerMain>();

        public JsonPropertiesScannerForm()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////

        // BUTTON EVENTS

        private void scan_properties_button_Click(object sender, EventArgs e)
        {
            save_file_dialog.ShowDialog();
        }

        private void save_properties_button_Click_1(object sender, EventArgs e)
        {
            foreach (HashContainer container in hash_container)
            {
                container.CreateTheDump("");
            }
        }

        private void extract_button_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            list.AddRange(scanned_files_data[0].GetNamesOfMainProperties());
            foreach (string s in list)
            {
                list1.Add(s);
            }
            for (int i = 1; i < scanned_files_data.Count - 1; i++)
            {
                foreach (string s in list)
                {
                    if (!scanned_files_data[i].GetNamesOfMainProperties().Contains<string>(s))
                    {
                        list1.Remove(s);
                    }
                }
            }

            list_box_properties.Items.Clear();

            foreach (string s in list1)
            {
                list_box_properties.Items.Add(s);
            }

        }

        /////////////////////////////////////////////////////////////////////////////////

        // OTHER EVENTS

        private void save_file_dialog1_FileOk(object sender, CancelEventArgs e)
        {
            string inputPath = save_file_dialog.FileName.Remove(save_file_dialog.FileName.LastIndexOf(@"\"));

            ExtractUnits(inputPath);

            /*
            hashContainer[0] = new HashContainer("", "Unit", "*.json", "*ammo.json|*tool_weapon.json|*build_arm.json");
            hashContainer[1] = new HashContainer("", "Ammo", "*ammo.json", "");
            hashContainer[2] = new HashContainer("", "ToolWeapon", "*tool_weapon.json", "");
            hashContainer[3] = new HashContainer("", "BuildArm", "*build_arm.json", "");
            */
        }

        /////////////////////////////////////////////////////////////////////////////////

        // FUNCTIONS

        private void ExtractUnits(string path)
        {
            List<string> keys = new List<string>()
            {
                "air",
                "land",
                "orbital",
                "sea"
            };

            foreach (string key in keys)
            {
                foreach (string directory in Directory.GetDirectories(path, key, SearchOption.AllDirectories))
                {
                    foreach (string file in Directory.GetFiles(directory, "*.json", SearchOption.AllDirectories))
                    {
                        string[] temp = file.Split('\\');
                        if (temp[temp.Length - 1].Remove(temp[temp.Length - 1].LastIndexOf('.')) == temp[temp.Length - 2])
                        {
                            list_box_properties.Items.Add(temp[temp.Length - 1]);
                            scanned_files_data.Add(new DataContainerMain(file));
                        }
                    }
                }

            }
        }

        /////////////////////////////////////////////////////////////////////////////////
    }
}

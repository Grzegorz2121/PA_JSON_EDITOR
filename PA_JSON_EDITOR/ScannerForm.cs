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

        public ScannerForm()
        {
            InitializeComponent();
        }

        private void Scan_button_Click(object sender, EventArgs e)
        {
            hashContainer[0] = new HashContainer("", "Unit", "*.json", "*ammo.json|*tool_weapon.json|*build_arm.json");
            hashContainer[1] = new HashContainer("", "Ammo", "*ammo.json", "");
            hashContainer[2] = new HashContainer("", "ToolWeapon", "*tool_weapon.json", "");
            hashContainer[3] = new HashContainer("", "BuildArm", "*build_arm.json", "");
        }
        
        private void Save_Properties_button_Click(object sender, EventArgs e)
        {
            foreach(HashContainer container in hashContainer)
            {
                container.CreateTheDump("");
            }
        }

    }
}

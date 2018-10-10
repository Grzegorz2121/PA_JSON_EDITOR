using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA_JSON_EDITOR
{
    public partial class JsonEditorForm : Form
    {
        public GraphicalContainerMain con;
        public DataContainerMain dataContainer;
        public string JsonPath;

        public JsonEditorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            JsonPath = openFileDialog1.FileName;
            dataContainer = new DataContainerMain(JsonPath);
            con = new GraphicalContainerMain(dataContainer, this, new Point(3, 3));

            Console.WriteLine(this.Controls.Count);
            // dataContainer = new DataContainer(JsonPath, this);


            Console.WriteLine();
        }

        private void Save_Json_button_Click(object sender, EventArgs e)
        {
            if(dataContainer != null)
            {
               // dataContainer.SaveTheJson(JsonPath);
            }
        }
    }
}

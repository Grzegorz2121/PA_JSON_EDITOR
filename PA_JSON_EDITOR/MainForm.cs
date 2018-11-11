using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PA_JSON_EDITOR;
using PA_JSON_EDITOR.PapaEditor;

namespace Pa_Looker_2
{
    public partial class Main_Form : Form
    {
        public static Form main_form;
        public static JsonPropertiesScannerForm json_scanner_form;
        public static JsonEditorForm json_editor_form;
        public static PapaEditorForm papa_editor_form;

        public Main_Form()
        {
            InitializeComponent();
            main_form = this;
        }

        private void Show_Scanner_button_Click(object sender, EventArgs e)
        {
            if(json_scanner_form == null)
            {
                json_scanner_form = new JsonPropertiesScannerForm();
                json_scanner_form.Show();
            }  
        }

        private void Show_Json_editor_button_Click(object sender, EventArgs e)
        {
            if(json_editor_form == null)
            {
                json_editor_form = new JsonEditorForm();
                json_editor_form.Show();
            }
        }

        private void Show_Papa_button_Click(object sender, EventArgs e)
        {
            if (papa_editor_form == null)
            {
                papa_editor_form = new PapaEditorForm();
                papa_editor_form.Show();
            }

        }
    }
}

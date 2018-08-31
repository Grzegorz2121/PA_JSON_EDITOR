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

namespace Pa_Looker_2
{
    public partial class Main_Form : Form
    {
        public static Form main_form;
        public static ScannerForm scannerForm;
        public static JsonEditorForm jsonEditorForm;
        public static VisualAdjustment visualAdjustment;

        public Main_Form()
        {
            InitializeComponent();
            main_form = this;
        }

        private void Show_Scanner_button_Click(object sender, EventArgs e)
        {
            if(scannerForm == null)
            scannerForm = new ScannerForm();
            scannerForm.Show();
        }

        private void Show_Json_editor_button_Click(object sender, EventArgs e)
        {
            if(jsonEditorForm == null)
            jsonEditorForm = new JsonEditorForm();
            jsonEditorForm.Show();
        }

        private void Show_GUI_Adj_button_Click(object sender, EventArgs e)
        {
            if (visualAdjustment == null)
                visualAdjustment = new VisualAdjustment();
            visualAdjustment.Show();
        }
    }
}

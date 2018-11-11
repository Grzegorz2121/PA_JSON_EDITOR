using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PA_JSON_EDITOR.PapaEditor;

namespace PA_JSON_EDITOR.PapaEditor
{
    public partial class PapaEditorForm : Form
    {
        PapaFile papa_file;

        public PapaEditorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open_file_dialog.ShowDialog();
        }

        private void open_file_dialog_FileOk(object sender, CancelEventArgs e)
        {
            papa_file = new PapaFile(open_file_dialog.FileName);
        }
    }
}

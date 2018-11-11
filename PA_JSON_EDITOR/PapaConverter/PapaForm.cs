using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PA_JSON_EDITOR.PapaConverter;

namespace PA_JSON_EDITOR.PapaConverter
{
    public partial class PapaForm : Form
    {
        PapaConverter papaConverter;

        public PapaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            papaConverter = new PapaConverter(openFileDialog1.FileName);
        }
    }
}

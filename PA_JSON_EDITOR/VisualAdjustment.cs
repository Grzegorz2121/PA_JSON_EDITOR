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
    public partial class VisualAdjustment : Form
    {
        public VisualAdjustment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(button1);
            panel1.Location = new Point(panel1.Location.X + 10, panel1.Location.Y + 10);
        }
    }
}

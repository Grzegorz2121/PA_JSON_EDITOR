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
using static Pa_Looker_2.Folder_tools;
using PA_JSON_EDITOR;

namespace Pa_Looker_2
{
    public partial class Main_Form : Form
    {
        public static Form main_form;
        public static ScannerForm scannerForm;

        public static PA_JSON_EDITOR.Container main_container;
        GUI_node_folder node;
        string main_path;

        public Main_Form()
        {
            InitializeComponent();
            main_form = this;
        }

        private void Pick_folder_button_Click(object sender, EventArgs e)
        {
            if (node != null)
            {
                node.Dispose();
                node = null;
            }

            //folderBrowserDialog1.ShowDialog();
            pick_folder_dialog.ShowDialog();
            main_path = pick_folder_dialog.FileName.Remove(pick_folder_dialog.FileName.Length - "Folder selection".Length);

             node = new GUI_node_folder(this, main_path, new Point(30,60));

            //Console.WriteLine(Folder_tools.Is_units_folder(main_path));
            Console.WriteLine(main_path);

            //string[] folders = Folder_tools.Shorten_path(main_path, Directory.GetDirectories(main_path));
            //list = new GUI_components.FolderPickList(new Point(10, 50), "main", main_path , folders, this);
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            main_container.Show();
        }

        private void hideAllButton_Click(object sender, EventArgs e)
        {
            main_container.Hide();
        }

        private void Show_Scanner_button_Click(object sender, EventArgs e)
        {
            scannerForm = new ScannerForm(this, main_path);
            scannerForm.Show();
        }
    }
}

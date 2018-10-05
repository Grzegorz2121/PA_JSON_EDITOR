using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;

namespace PA_JSON_EDITOR
{
    class GraphicalContainerComplex : GraphicalContainer
    {
        //For complex containers

        DataContainerComplex slave;

        Panel panel;
        Button editButton;
        Button addButton;
        Button deleteButton;
        ListBox listBox;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public GraphicalContainerComplex(IDataContainer dataContainer) : base()
        {
            slave = dataContainer as DataContainerComplex;

            panel = CreatePanel(new Point(), new Size(),
                new Control[]
                {
                    addButton = CreateButton("Add", new Point(), new Size()),
                    deleteButton = CreateButton("Delete", new Point(), new Size()),
                    editButton = CreateButton("Edit", new Point(), new Size()),
                    listBox = CreateListBox(new Point(), new Size())
                },
                new Form()
                );
        }

        public override void Hide()
        {
            addButton.Hide();
            editButton.Hide();
            deleteButton.Hide();
            listBox.Hide();
            panel.Hide();
        }

        public override void Show()
        {
            addButton.Show();
            editButton.Show();
            deleteButton.Show();
            listBox.Show();
            panel.Show();
        }

        public override void Dispose()
        {
            addButton.Dispose();
            editButton.Dispose();
            deleteButton.Dispose();
            listBox.Dispose();
            panel.Dispose();

            addButton = null;
            editButton = null;
            deleteButton = null;
            listBox = null;
            panel = null;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}

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
    class GraphicalContainerArray : GraphicalContainer
    {
        //For arrays

        public Dictionary<int, IGraphicalContainer> ArrayGraphicalElements = new Dictionary<int, IGraphicalContainer>();

        //DataContainerArray slave;

        Panel panel;
        Button editButton;
        Button addButton;
        Button deleteButton;
        ListBox listBox;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public GraphicalContainerArray(DataContainerArray dataContainer, Point inLocation, Size inSize, Form parentForm) : base(dataContainer, parentForm, inLocation, inSize)
        {
            // slave = dataContainer as DataContainerArray;


            /*
            foreach (IDataContainer children in dataContainer.GetChilden())
            {
                ArrayElements.Add(children.GetTheName(), CreateNewGraphicalContainer(children, parentForm, inLocation, new Size()));
            }*/


            panel = CreatePanel(new Point(inLocation.X, inLocation.Y + 100), new Size(100, 100),
                new Control[]
                {
                    addButton = CreateButton("Add", new Point(3,3), new Size(30,20)),
                    deleteButton = CreateButton("Delete", new Point(36,3), new Size(30,20)),
                    editButton = CreateButton("Edit", new Point(69,3), new Size(30,20)),
                    listBox = CreateListBox(new Point(3,26), new Size(94,74), new List<int>(dataContainer.GetTheList().Keys))
                },
                parentForm
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

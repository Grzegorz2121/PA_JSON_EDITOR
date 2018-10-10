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

        public Dictionary<string, IGraphicalContainer> ComplexGraphicalElements = new Dictionary<string, IGraphicalContainer>();

        // DataContainerComplex slave;

        DataContainerComplex slave;
        Point location;
        Form parent;

        Panel panel;
        Button editButton;
        Button addButton;
        Button deleteButton;
        ListBox listBox;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public GraphicalContainerComplex(DataContainerComplex dataContainer, Point inLocation, Size inSize, Form parentForm) : base(dataContainer, parentForm, inLocation, inSize)
        {
            slave = dataContainer;
            parent = parentForm;
            location = inLocation;

            /*
            foreach (IDataContainer children in dataContainer.GetChilden())
            {
                GraphicalElements.Add(children.GetTheName(), CreateNewGraphicalContainer(children, parentForm, inLocation, new Size()));
            }*/


            panel = CreatePanel(new Point(inLocation.X, inLocation.Y + 100), new Size(100, 100),
                new Control[]
                {
                    addButton = CreateButton("Add", new Point(3,3), new Size(30,20), AddButtonClick),
                    deleteButton = CreateButton("Delete", new Point(36,3), new Size(30,20), DeleteButtonClick),
                    editButton = CreateButton("Edit", new Point(69,3), new Size(30,20), EditButtonClick),
                    listBox = CreateListBox(new Point(3,26), new Size(94,74), new List<string>(dataContainer.GetTheList().Keys), ListBoxChange)
                    /*
                    addButton = CreateButton("Add", new Point(), new Size()),
                    deleteButton = CreateButton("Delete", new Point(), new Size()),
                    editButton = CreateButton("Edit", new Point(), new Size()),
                    listBox = CreateListBox(new Point(), new Size(), new List<string>(dataContainer.GetTheList().Keys))*/
                },
                parentForm
                );
    
        }

        private void AddButtonClick(object sender, EventArgs e)
        {

        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {

        }

        private void EditButtonClick(object sender, EventArgs e)
        {

        }

        private void ListBoxChange(object sender, EventArgs e)
        {

            List<string> newToCreate = new List<string>();
            List<string> oldToDelete = new List<string>();
            List<string> selected = new List<string>();

            foreach (string s in listBox.SelectedItems)
            {
                selected.Add(s);
            }

            foreach (string s in selected)
            {
                if (!ComplexGraphicalElements.ContainsKey(s))
                {
                    newToCreate.Add(s);
                }
            }
            foreach (string s in ComplexGraphicalElements.Keys)
            {
                if (!selected.Contains(s))
                {
                    oldToDelete.Add(s);
                }
            }

            foreach (string s in oldToDelete)
            {
                ComplexGraphicalElements[s].Dispose();
                ComplexGraphicalElements.Remove(s);
            }

            Point temp = new Point(location.X+103*ComplexGraphicalElements.Count, location.Y+103);
            foreach (string s in newToCreate)
            {
                ComplexGraphicalElements.Add(s, CreateNewGraphicalContainer(slave.GetChild(s), parent, temp, new Size()));
                temp = new Point(temp.X + 103, temp.Y);
            }
            temp = location;
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

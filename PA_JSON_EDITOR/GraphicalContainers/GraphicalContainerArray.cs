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

        DataContainerArray slave;
        Point location;
        Form parent;

        Panel panel;
        Button editButton;
        Button addButton;
        Button deleteButton;
        ListBox listBox;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public GraphicalContainerArray(DataContainerArray dataContainer, Point inLocation, Size inSize, Form parentForm) : base(dataContainer, parentForm, inLocation, inSize)
        {
            slave = dataContainer;
            parent = parentForm;
            location = inLocation;
            /*
            foreach (IDataContainer children in dataContainer.GetChilden())
            {
                ArrayElements.Add(children.GetTheName(), CreateNewGraphicalContainer(children, parentForm, inLocation, new Size()));
            }*/


            panel = CreatePanel(new Point(inLocation.X, inLocation.Y + 100), new Size(100, 100),
                new Control[]
                {
                    addButton = CreateButton("Add", new Point(3,3), new Size(30,20), AddButtonClick),
                    deleteButton = CreateButton("Delete", new Point(36,3), new Size(30,20), DeleteButtonClick),
                    editButton = CreateButton("Edit", new Point(69,3), new Size(30,20), EditButtonClick),
                    listBox = CreateListBox(new Point(3,26), new Size(94,74), new List<int>(dataContainer.GetTheList().Keys), ListBoxChange)
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

            List<int> newToCreate = new List<int>();
            List<int> oldToDelete = new List<int>();
            List<int> selected = new List<int>();

            foreach (int i in listBox.SelectedItems)
            {
                selected.Add(i);
            }
            
            foreach (int i in selected)
            {
                if (!ArrayGraphicalElements.ContainsKey(i))
                {
                    newToCreate.Add(i);
                }
            }
            foreach (int i in ArrayGraphicalElements.Keys)
            {
                if (!selected.Contains(i))
                {
                    oldToDelete.Add(i);
                }
            }

            foreach(int i in oldToDelete)
            {
                ArrayGraphicalElements[i].Dispose();
                ArrayGraphicalElements.Remove(i);
            }

            Point temp = new Point(location.X+103*ArrayGraphicalElements.Count, location.Y + 103);
            foreach (int i in newToCreate)
            {
                ArrayGraphicalElements.Add(i, CreateNewGraphicalContainer(slave.GetChild(i), parent, temp, new Size()));
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

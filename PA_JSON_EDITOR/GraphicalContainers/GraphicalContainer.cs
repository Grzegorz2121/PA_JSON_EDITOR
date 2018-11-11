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

    public abstract class GraphicalContainer : IGraphicalContainer
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //TREE CREATION AND POPULATION

        /// <summary>
        /// Constructor used for recursive tree creation. Forces all types of containers to be able to be created from jtokens
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public GraphicalContainer(IDataContainer container, Form parentForm, Point inLoc, Size inSize)
        {

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //GENERAL METHODS (CAN BE USED IN EVERY OBJECT TYPE [primitive, array, complex])

        public virtual IGraphicalContainer CreateNewGraphicalContainer(IDataContainer dataContainer, Form parentForm, Point inLoc, Size inSize)
        {
            switch (dataContainer.GetTheType())
            {
                case DataContainer.DataContainerType.Array:
                    return new GraphicalContainerArray(dataContainer as DataContainerArray, inLoc, inSize, parentForm);

                case DataContainer.DataContainerType.Complex:
                    return new GraphicalContainerComplex(dataContainer as DataContainerComplex, inLoc, inSize, parentForm);

                case DataContainer.DataContainerType.Null:
                    return null;

                default:
                    return new GraphicalContainerPrimitive(dataContainer as DataContainerPrimitive, inLoc, inSize, parentForm);

            }

        }

        public Button CreateButton(string name, Point offset, Size size, MouseEventHandler handle)
        {
            Button temp = new Button
            {
                Text = name,
                Location = offset,
                Size = size
            };
            temp.MouseClick += handle;
            return temp;
        }

        public TextBox CreateTextBox(Point offset, Size size, string text)
        {
            TextBox temp = new TextBox
            {
                Location = offset,
                Size = size
            };
            temp.Text = text;
            return temp;
        }


        public ListBox CreateListBox(Point offset, Size size, List<string> items, EventHandler handler)
        {
            ListBox temp = new ListBox
            {
                Location = offset,
                Size = size
            };
            temp.SelectedIndexChanged += handler;
            foreach(string s in items)
            temp.Items.Add(s);
            return temp;
        }

        public ListBox CreateListBox(Point offset, Size size, List<int> items, EventHandler handler)
        {
            ListBox temp = new ListBox
            {
                Location = offset,
                Size = size
            };
            temp.SelectionMode = SelectionMode.MultiSimple;
            temp.SelectedIndexChanged += handler;
            foreach (int i in items)
            temp.Items.Add(i);
            return temp;
        }


        public Panel CreatePanel(Point offset, Size size, Control[] childElements, Form parentForm)
        {
            
            Panel temp = new Panel
            {
                Location = offset,
                Size = size
            };
            temp.BackColor = Color.Aqua;
            parentForm.Controls.Add(temp);
            foreach (Control c in childElements)
            {
                temp.Controls.Add(c);
            }
            
            return temp;
        }

        public abstract void Show();

        public abstract void Hide();

        public abstract void Dispose();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

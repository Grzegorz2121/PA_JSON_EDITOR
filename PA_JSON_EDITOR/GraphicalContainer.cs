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
using static PA_JSON_EDITOR.DataContainer;

namespace PA_JSON_EDITOR
{
    class GraphicalContainer
    {
        public enum GraphicalContainerType
        {
            Primitive,
            Complex,
            Array
        }

        ListBox ComplexListBox;
        Button ComplexDeleteButton;
        Button ComplexAddButton;

        ListBox ArrayListBox;
        Button ArrayDeleteButton;
        Button ArrayAddButton;

        TextBox PrimitiveTextBox;
        Button PrimitiveSaveButton;

        public Form ParentForm;
        public DataContainer DataProvider;
        public Point GUILocation;

        public GraphicalContainer(Form InParentForm, DataContainer InDataProvider, Point InLocation)
        {
            ParentForm = InParentForm;
            DataProvider = InDataProvider;
            GUILocation = InLocation;

            switch(DataProvider.ContainerType)
            {
                case DataContainerType.Array:
                    CreateArray();
                    break;
                case DataContainerType.Complex:
                    CreateComplex();
                    break;
                case DataContainerType.Primitive:
                    CreatePrimitive();
                    break;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void CreatePrimitive()
        {
            CreateButton("Save", ParentForm, PrimitiveSaveButton, new Point(GUILocation.X + 3, GUILocation.Y + 174), Save_Primitive_button_click);
            CreateTextBox(DataProvider.PrimitiveElement.ToString(), ParentForm ,PrimitiveTextBox, new Point(GUILocation.X + 3, GUILocation.Y + 3));
        }

        void CreateArray()
        {
            string[] Helper()
            {
                List<string> list = new List<string>();
                foreach (int i in DataProvider.ArrayElements.Keys)
                {
                    list.Add(i.ToString());
                }
                return list.ToArray<string>();
            }
           
            CreateButton("Add", ParentForm, ArrayAddButton, new Point(GUILocation.X + 3, GUILocation.Y + 174), Add_Complex_button_click);
            CreateButton("Delete", ParentForm, ArrayAddButton, new Point(GUILocation.X + 84, GUILocation.Y + 174), Delete_Complex_button_click);
            CreateListBox(Helper() ,ParentForm , ArrayListBox, new Point(GUILocation.X + 3, GUILocation.Y + 3), ListBox_Complex_index_change);
        }

        void CreateComplex()
        {
            CreateButton("Add", ParentForm, ComplexAddButton, new Point(GUILocation.X + 3, GUILocation.Y + 174), Add_Complex_button_click);
            CreateButton("Delete", ParentForm, ComplexAddButton, new Point(GUILocation.X + 84, GUILocation.Y + 174), Delete_Complex_button_click);
            CreateListBox(DataProvider.ComplexElements.Keys.ToArray<string>() ,ParentForm , ComplexListBox, new Point(GUILocation.X + 3, GUILocation.Y + 3), ListBox_Complex_index_change);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Add_Complex_button_click(object sender, EventArgs e)
        {

        }

        private void Delete_Complex_button_click(object sender, EventArgs e)
        {

        }

        private void Save_Primitive_button_click(object sender, EventArgs e)
        {

        }

        private void ListBox_Complex_index_change(object sender, EventArgs e)
        {

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //HELPER FUNCTIONS

        public void OffsetThePoint(ref Point in_point)
        {
            in_point = new Point(in_point.X + DataProvider.Location.X, in_point.Y + DataProvider.Location.Y);
        }

        void CreateTextBox(string item, Form parent, TextBox textbox, Point InLocation)
        {
            
            textbox = new TextBox();
            textbox.Text = item;

            textbox.Height = 20;
            textbox.Width = 194;

            textbox.Location = InLocation;

            parent.Controls.Add(textbox);
        }

        void CreateListBox(string[] items, Form parent, ListBox listbox, Point InLocation, EventHandler callback)
        {
            listbox = new ListBox();

            listbox.Height = 160;
            listbox.Width = 194;

            listbox.Location = InLocation;

            foreach(string item in items)
            {
                listbox.Items.Add(item);
            }
            listbox.SelectedIndexChanged += callback;
            parent.Controls.Add(listbox);
        }

        void CreateButton(string name, Form parent, Button button, Point InLocation, EventHandler callback)
        {
            button = new Button();

            button.Height = 23;
            button.Width = 75;

            button.Location = InLocation;

            button.Text = name;
            button.Click += callback;
            parent.Controls.Add(button);
        }
    }
}

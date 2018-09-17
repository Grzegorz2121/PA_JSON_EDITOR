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
        public bool IsHidden;

        public GraphicalContainer(Form InParentForm, DataContainer InDataProvider, Point InLocation, bool Hidden)
        {
            IsHidden = Hidden;

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
            CreateButton("Save", ParentForm, PrimitiveSaveButton, new Point(GUILocation.X + 3, GUILocation.Y + 174), Save_Primitive_button_click, IsHidden);
            CreateTextBox(DataProvider.PrimitiveElement.ToString(), ParentForm ,PrimitiveTextBox, new Point(GUILocation.X + 3, GUILocation.Y + 3), IsHidden);
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
           
            CreateButton("Add", ParentForm, ArrayAddButton, new Point(GUILocation.X + 3, GUILocation.Y + 174), Add_Complex_button_click, IsHidden);
            CreateButton("Delete", ParentForm, ArrayAddButton, new Point(GUILocation.X + 84, GUILocation.Y + 174), Delete_Complex_button_click, IsHidden);
            CreateListBox(Helper() ,ParentForm , ArrayListBox, new Point(GUILocation.X + 3, GUILocation.Y + 3), ListBox_Complex_index_change, IsHidden);
        }

        void CreateComplex()
        {
            CreateButton("Add", ParentForm, ComplexAddButton, new Point(GUILocation.X + 3, GUILocation.Y + 174), Add_Complex_button_click, IsHidden);
            CreateButton("Delete", ParentForm, ComplexAddButton, new Point(GUILocation.X + 84, GUILocation.Y + 174), Delete_Complex_button_click, IsHidden);
            CreateListBox(DataProvider.ComplexElements.Keys.ToArray<string>() ,ParentForm , ComplexListBox, new Point(GUILocation.X + 3, GUILocation.Y + 3), ListBox_Complex_index_change, IsHidden);
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

        public void Show()
        {
            switch (DataProvider.ContainerType)
            {
                case DataContainerType.Array:
                    ArrayListBox.Show();
                    ArrayDeleteButton.Show();
                    ArrayAddButton.Show();
                    break;
                case DataContainerType.Complex:
                    ComplexListBox.Show();
                    ComplexDeleteButton.Show();
                    ComplexAddButton.Show();
                    break;
                case DataContainerType.Primitive:
                    PrimitiveTextBox.Show();
                    PrimitiveSaveButton.Show();
                    break;
            }
        }

        public void Hide()
        {
            switch (DataProvider.ContainerType)
            {
                case DataContainerType.Array:
                    ArrayListBox.Hide();
                    ArrayDeleteButton.Hide();
                    ArrayAddButton.Hide();
                    break;
                case DataContainerType.Complex:
                    ComplexListBox.Hide();
                    ComplexDeleteButton.Hide();
                    ComplexAddButton.Hide();
                    break;
                case DataContainerType.Primitive:
                    PrimitiveTextBox.Hide();
                    PrimitiveSaveButton.Hide();
                    break;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //HELPER FUNCTIONS

        public void OffsetThePoint(ref Point in_point)
        {
            in_point = new Point(in_point.X + DataProvider.Location.X, in_point.Y + DataProvider.Location.Y);
        }

        void CreateTextBox(string item, Form parent, TextBox textbox, Point InLocation, bool hidden)
        {
            
            textbox = new TextBox();
            textbox.Text = item;

            textbox.Height = 20;
            textbox.Width = 194;

            textbox.Location = InLocation;

            parent.Controls.Add(textbox);

            if(hidden)
            textbox.Hide();
        }

        void CreateListBox(string[] items, Form parent, ListBox listbox, Point InLocation, EventHandler callback, bool hidden)
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

            if (hidden)
            listbox.Hide();
        }

        void CreateButton(string name, Form parent, Button button, Point InLocation, EventHandler callback, bool hidden)
        {
            button = new Button();

            button.Height = 23;
            button.Width = 75;

            button.Location = InLocation;

            button.Text = name;
            button.Click += callback;
            parent.Controls.Add(button);

            if (hidden)
            button.Hide();
        }
    }
}

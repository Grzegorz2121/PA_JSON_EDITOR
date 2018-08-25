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
using static PA_JSON_EDITOR.GraphicalContainerSizes;

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

        public GraphicalContainer(Form InParentForm, DataContainer InDataProvider)
        {
            ParentForm = InParentForm;
            DataProvider = InDataProvider;

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
            CreateButton("Save", ParentForm, PrimitiveSaveButton, PrimitiveSize.SaveButtonLoc, PrimitiveSize.SaveButtonSize ,Save_Primitive_button_click);
            CreateTextBox(DataProvider.PrimitiveElement.ToString(), ParentForm ,PrimitiveTextBox, PrimitiveSize.TextBoxLoc, PrimitiveSize.TextBoxSeize);
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
           
            CreateButton("Add", ParentForm, ArrayAddButton, ArraySize.AddButtonLoc, ArraySize.AddButtonSize ,Add_Complex_button_click);
            CreateButton("Delete", ParentForm, ArrayAddButton, ArraySize.DeleteButtonLoc, ArraySize.DeleteButtonSize, Delete_Complex_button_click);
            CreateListBox(Helper() ,ParentForm , ArrayListBox, ArraySize.ListBoxLoc, ArraySize.ListBoxSeize, ListBox_Complex_index_change);
        }

        void CreateComplex()
        {
            CreateButton("Add", ParentForm, ComplexAddButton, ComplexSize.AddButtonLoc, ComplexSize.AddButtonSize, Add_Complex_button_click);
            CreateButton("Delete", ParentForm, ComplexAddButton, ComplexSize.DeleteButtonLoc, ComplexSize.DeleteButtonSize, Delete_Complex_button_click);
            CreateListBox(DataProvider.ComplexElements.Keys.ToArray<string>() ,ParentForm , ComplexListBox, ComplexSize.ListBoxLoc, ComplexSize.ListBoxSeize, ListBox_Complex_index_change);
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

        void CreateTextBox(string item, Form parent, TextBox textbox, Point Marigin, Point Size)
        {
            //OffsetThePoint(ref Size);
            OffsetThePoint(ref Marigin);

            textbox = new TextBox();
            textbox.Text = item;

            textbox.Height = Size.Y;
            textbox.Width = Size.X;

            textbox.Location = Marigin;

            parent.Controls.Add(textbox);
        }

        void CreateListBox(string[] items, Form parent, ListBox listbox, Point Marigin, Point Size, EventHandler callback)
        {
            //OffsetThePoint(ref Size);
            OffsetThePoint(ref Marigin);

            listbox = new ListBox();

            listbox.Height = Size.Y;
            listbox.Width = Size.X;

            listbox.Location = Marigin;

            foreach(string item in items)
            {
                listbox.Items.Add(item);
            }
            listbox.SelectedIndexChanged += callback;
            parent.Controls.Add(listbox);
        }

        void CreateButton(string name, Form parent, Button button, Point Marigin, Point Size, EventHandler callback)
        {
            //OffsetThePoint(ref Size);
            OffsetThePoint(ref Marigin);

            button = new Button();

            button.Height = Size.Y;
            button.Width = Size.X;

            button.Location = Marigin;

            button.Text = name;
            button.Click += callback;
            parent.Controls.Add(button);
        }
    }
}

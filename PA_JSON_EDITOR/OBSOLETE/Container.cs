using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
//using static Pa_Looker_2.Folder_tools;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using static Pa_Looker_2.ICallback;

namespace PA_JSON_EDITOR
{
    public class Constants
    {
        public const int PanelHeight = 140;
        public const int PanelWidth = 90;

        public static readonly Point ListBoxPosition = new Point(10, 10);
        public const int ListBoxHeight = 90;
        public const int ListBoxWidth = 70;

        public static readonly Point TextBoxPosition = new Point(10, 10);
        public const int TextBoxHeight = 20;
        public const int TextBoxWidth = 70;

        public const string SaveButtonText = "Save";
        public static readonly Point SaveButtonPosition = new Point(10, 110);
        public const int SaveButtonHeight = 20;
        public const int SaveBoxWidth = 20;

        public const string DeleteButtonText = "Delete";
        public static readonly Point DeleteButtonPosition = new Point(30, 110);
        public const int DeleteButtonHeight = 20;
        public const int DeleteBoxWidth = 20;

        public const string AddButtonText = "Add";
        public static readonly Point AddButtonPosition = new Point(50, 110);
        public const int AddButtonHeight = 20;
        public const int AddBoxWidth = 20;
    }

    /// <summary>
    /// Container class allows for easy data managing.
    /// </summary>
    public class Container
    {
        protected GraphicalBlock graphicalBlock;
        //Universal usage
        protected bool IsOriginObject;
        protected bool IsComplex;

        //For primitive containers
        protected object PrimitiveContent;

        //For complex containers
        protected Container child_container;
        protected Dictionary<string, Container> child_elements = new Dictionary<string, Container>();

        public delegate void ListCallback(object sender, EventArgs e);
        public ListCallback ListBoxChange;

        /// <summary>
        /// Constructor when provided with raw jobject will create a tree that allows easy data managment.
        /// Constructor is only called once, after that the whole tree will be created recusivly
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public Container(JToken input_jobject, Point loc, Form main_form_ref, bool Is_orig_obj=true, string name="")
        {
            IsOriginObject = Is_orig_obj;

            //Check for complex vs primitive
            if(input_jobject.HasValues)
            {
                IsComplex = true;
                //Populate children list with next level/tier tokens
                //Every token will execute the same procedure eventualy creating a tree
                //The main container is the origin of the tree, primitive containers are the end of the branches
                int i = 0;
                JObject job = input_jobject as JObject;
                foreach (KeyValuePair<string, JToken> pair in job)
                {
                    if(pair.Value.Type != JTokenType.Array)
                    {
                        child_elements.Add(pair.Key, new Container(pair.Value, new Point(loc.X + i, loc.Y + 150), main_form_ref, false, pair.Key));
                        i += 100;
                    }  
                }

                List<string> names = new List<string>();
                foreach(string n in child_elements.Keys)
                {
                    names.Add(n);
                }

                ListBoxChange = new ListCallback(ListBoxIndexChange);

                graphicalBlock = new GraphicalBlock(loc, names, main_form_ref, this, ListBoxChange);
            }
            else
            {
                IsComplex = false;
                //If token is primitive we can extract value from it
                PrimitiveContent = input_jobject.ToObject<object>();
                graphicalBlock = new GraphicalBlock(loc, name, main_form_ref, this);
            }
        }

        public void ListBoxIndexChange(object sender, EventArgs e)
        {
            foreach(KeyValuePair<string, Container> k in child_elements)
            {
                k.Value.Hide();
            }

            ListBox lbox = (ListBox)sender;
            child_elements[(string)lbox.SelectedItem].Show();
        }

        public void Show()
        {
            graphicalBlock.Show();
        }

        public void Hide()
        {
            if(IsComplex)
            {
                foreach(Container c in child_elements.Values)
                {
                    c.Hide();
                }
            }

            graphicalBlock.Hide();
        }

        public void Dispose()
        {
            if (IsComplex)
            {
                foreach (Container c in child_elements.Values)
                {
                    c.Dispose();
                    
                }
                child_elements = null;
            }

            graphicalBlock.Dispose();
        }
    }


    public class GraphicalBlock
    {
        object ParentContainer;
        Form MainFormReference;
        Panel GraphicalPanel;

        TextBox PrimitiveEditBox;
        ListBox ComplexChoiceBox;

        Button SaveButton;
        Button DeleteButton;
        Button AddButton;

        //public delegate void ListCallback(object sender, EventArgs e);

        public GraphicalBlock(Point loc,string text, Form form_ref, object par)
        {
            MainFormReference = form_ref;
            ParentContainer = par;

            GraphicalPanel = new Panel();
            GraphicalPanel.BackColor = Color.Aqua;
            MainFormReference.Controls.Add(GraphicalPanel);
            GraphicalPanel.Location = loc;
            GraphicalPanel.Height = Constants.PanelHeight;
            GraphicalPanel.Width = Constants.PanelWidth;

            PrimitiveEditBox = new TextBox();
            GraphicalPanel.Controls.Add(PrimitiveEditBox);
            PrimitiveEditBox.Location = Constants.TextBoxPosition;
            PrimitiveEditBox.Text = text;
            PrimitiveEditBox.Width = Constants.TextBoxWidth;

            SaveButton = new Button();
            SaveButton.Location = Constants.SaveButtonPosition;
            SaveButton.Height = Constants.SaveButtonHeight;
            SaveButton.Width = Constants.SaveBoxWidth;
            SaveButton.Text = Constants.SaveButtonText;
            GraphicalPanel.Controls.Add(SaveButton);
        }

        public GraphicalBlock(Point loc, List<string> texts, Form form_ref, object par, Container.ListCallback list_callback)
        {
            MainFormReference = form_ref;
            ParentContainer = par;

            GraphicalPanel = new Panel();
            GraphicalPanel.BackColor = Color.Aqua;
            MainFormReference.Controls.Add(GraphicalPanel);
            GraphicalPanel.Location = loc;
            GraphicalPanel.Height = Constants.PanelHeight;
            GraphicalPanel.Width = Constants.PanelWidth;

            ComplexChoiceBox = new ListBox();
            GraphicalPanel.Controls.Add(ComplexChoiceBox);
            ComplexChoiceBox.Location = Constants.ListBoxPosition;
            ComplexChoiceBox.Width = Constants.ListBoxWidth;
            foreach(string s in texts)
            {
                ComplexChoiceBox.Items.Add(s);
            }

            ComplexChoiceBox.SelectedIndexChanged += new EventHandler(list_callback);

            SaveButton = new Button();
            SaveButton.Location = Constants.SaveButtonPosition;
            SaveButton.Height = Constants.SaveButtonHeight;
            SaveButton.Width = Constants.SaveBoxWidth;
            SaveButton.Text = Constants.SaveButtonText;
            GraphicalPanel.Controls.Add(SaveButton);
        }

        public void Hide()
        {
            GraphicalPanel.Visible = false;
        }

        public void Show()
        {
            GraphicalPanel.Visible = true;
        }

        public void Dispose()
        {
            GraphicalPanel.Dispose();
        }
    }
}

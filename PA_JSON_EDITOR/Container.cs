using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static Pa_Looker_2.Folder_tools;
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
        public const int PanelHeight = 120;
        public const int PanelWidth = 90;

        public static readonly Point ListBoxPosition = new Point(10, 10);
        public const int ListBoxHeight = 100;
        public const int ListBoxWidth = 60;

        public static readonly Point TextBoxPosition = new Point(10, 10);
        public const int TextBoxHeight = 20;
        public const int TextBoxWidth = 100;

        public const string SaveButtonText = "Save";
        public static readonly Point SaveButtonPosition = new Point(10, 120);
        public const int SaveButtonHeight = 20;
        public const int SaveBoxWidth = 20;

        public const string DeleteButtonText = "Delete";
        public static readonly Point DeleteButtonPosition = new Point(30, 120);
        public const int DeleteButtonHeight = 20;
        public const int DeleteBoxWidth = 20;

        public const string AddButtonText = "Add";
        public static readonly Point AddButtonPosition = new Point(50, 120);
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

        //For primitive containers
        protected object PrimitiveContent;

        //For complex containers
        protected Container child_container;
        protected List<Container> child_elements = new List<Container>();

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
                //Populate children list with next level/tier tokens
                //Every token will execute the same procedure eventualy creating a tree
                //The main container is the origin of the tree, primitive containers are the end of the branches
                int i = 0;
                JObject job = input_jobject as JObject;
                foreach (KeyValuePair<string, JToken> pair in job)
                {
                    if(pair.Value.Type != JTokenType.Array)
                    {
                        child_elements.Add(new Container(pair.Value, new Point(loc.X + i, loc.Y + 150), main_form_ref, false, pair.Key));
                        i += 100;
                    }  
                }

                graphicalBlock = new GraphicalBlock(loc, name, true, main_form_ref, this);
            }
            else
            {
                //If token is primitive we can extract value from it
                PrimitiveContent = input_jobject.ToObject<object>();
                graphicalBlock = new GraphicalBlock(loc, name, false, main_form_ref, this);
            }
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

        public GraphicalBlock(Point loc,string text ,bool IsComplex, Form form_ref, object par)
        {
            MainFormReference = form_ref;
            ParentContainer = par;

            GraphicalPanel = new Panel();
            GraphicalPanel.BackColor = Color.Aqua;
            MainFormReference.Controls.Add(GraphicalPanel);
            GraphicalPanel.Location = loc;
            GraphicalPanel.Height = Constants.PanelHeight;
            GraphicalPanel.Width = Constants.PanelWidth;

            if(IsComplex)
            {
                ComplexChoiceBox = new ListBox();
                GraphicalPanel.Controls.Add(ComplexChoiceBox);
                ComplexChoiceBox.Location = Constants.ListBoxPosition;
                ComplexChoiceBox.Items.Add(text);
            }
            else
            {
                PrimitiveEditBox = new TextBox();
                GraphicalPanel.Controls.Add(PrimitiveEditBox);
                PrimitiveEditBox.Location = Constants.TextBoxPosition;
                PrimitiveEditBox.Text = text;
            }
        }
    }
}

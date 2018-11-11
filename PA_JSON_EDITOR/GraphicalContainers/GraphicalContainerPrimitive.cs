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
    class GraphicalContainerPrimitive : GraphicalContainer
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (complex)

        Panel panel;
        Button editButton;
        TextBox textBox;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PARENT FUNCTIONALITY OVERRIDE

        public GraphicalContainerPrimitive(DataContainerPrimitive dataContainer, Point inLocation, Size inSize, Form parentForm) : base(dataContainer, parentForm, inLocation, inSize)
        {
            panel = CreatePanel(new Point(inLocation.X, inLocation.Y + 100), new Size(100, 100),
                new Control[]
                {
                    editButton = CreateButton("Edit", new Point(3,3), new Size(80,20), EditButtonClick),
                    textBox = CreateTextBox(new Point(3,26), new Size(80,20), dataContainer.GetValue().ToString())
                },
                parentForm
                );
            
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //CONTAINER SPECYFIC FUNCTIONS

        private void EditButtonClick(object sender, EventArgs e)
        {

        }


        public override void Hide()
        {
            textBox.Hide();
            editButton.Hide();
            panel.Hide();
        }

        public override void Show()
        {
            textBox.Show();
            editButton.Show();
            panel.Show();
        }

        public override void Dispose()
        {
            textBox.Dispose();
            editButton.Dispose();
            panel.Dispose();
            textBox = null;
            editButton = null;
            panel = null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

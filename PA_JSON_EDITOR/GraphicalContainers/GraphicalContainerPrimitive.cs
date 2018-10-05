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
        //For primitive containers

        DataContainerPrimitive slave;

        Panel panel;
        Button editButton;
        TextBox textBox;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public GraphicalContainerPrimitive(IDataContainer dataContainer) : base()
        {
            slave = dataContainer as DataContainerPrimitive;

            panel = CreatePanel(new Point(), new Size(),
                new Control[]
                {
                    editButton = CreateButton("Edit", new Point(), new Size()),
                    textBox = CreateTextBox(new Point(), new Size())
                },
                new Form()
                );
            
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

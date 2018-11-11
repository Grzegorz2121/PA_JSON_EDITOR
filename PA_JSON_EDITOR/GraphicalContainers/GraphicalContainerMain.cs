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
    public class GraphicalContainerMain
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (complex)

        public Dictionary<string, IGraphicalContainer> GraphicalElements = new Dictionary<string, IGraphicalContainer>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // FILE READING FUNCTIONALITY + CHILD CREATION

        public GraphicalContainerMain(DataContainerMain mainDataContainer, Form mainForm , Point startLocation)
        {
            Point temp = startLocation;
            foreach (IDataContainer children in mainDataContainer.GetChilden())
            {
                GraphicalElements.Add(children.GetTheName(), CreateNewGraphicalContainer(children, mainForm, startLocation, new Size()));
                startLocation = new Point(startLocation.X + 103, startLocation.Y);
            }
            startLocation = temp;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //CONTAINER SPECYFIC FUNCTIONS

        private IGraphicalContainer[] CreateNewGraphicalContainer(IDataContainer[] dataContainers, Form parentForm, Point inLoc, Size inSize)
        {
            List<IGraphicalContainer> temp = new List<IGraphicalContainer>();

            foreach(IDataContainer c in dataContainers)
            {
                temp.Add(CreateNewGraphicalContainer(c, parentForm, inLoc, inSize));
            }

            return temp.ToArray<IGraphicalContainer>();
        }

            private IGraphicalContainer CreateNewGraphicalContainer(IDataContainer dataContainer, Form parentForm, Point inLoc, Size inSize)
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

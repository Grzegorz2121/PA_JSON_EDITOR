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
        //For complex containers
        //For complex containers
        public Dictionary<string, IGraphicalContainer> GraphicalElements = new Dictionary<string, IGraphicalContainer>();



        public GraphicalContainerMain(DataContainerMain mainDataContainer, Form mainForm)
        {
           foreach(IDataContainer children in mainDataContainer.GetChilden())
           {
                GraphicalElements.Add(children.GetTheName(), CreateNewGraphicalContainer(children));
           }
        }



        private IGraphicalContainer CreateNewGraphicalContainer(IDataContainer dataContainer)
        {
            switch (dataContainer.GetTheType())
            {
                case DataContainer.DataContainerType.Array:
                    return new GraphicalContainerArray(dataContainer);

                case DataContainer.DataContainerType.Complex:
                    return new GraphicalContainerComplex(dataContainer);

                case DataContainer.DataContainerType.Null:
                    return null;

                default:
                    return new GraphicalContainerPrimitive(dataContainer);

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        
    }
}

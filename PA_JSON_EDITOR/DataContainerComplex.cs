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
using static PA_JSON_EDITOR.GraphicalContainer;

namespace PA_JSON_EDITOR
{
    class DataContainerComplex : DataContainer, IDataComplex
    {
        //For complex containers
        public int ComplexAmount = 0;
        public Dictionary<string, IDataContainer> ComplexElements = new Dictionary<string, IDataContainer>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataContainerComplex(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Complex;

            int i = 0;
            //If the Token is complex it has to be a JObject with dictonary of next tokens
            foreach (KeyValuePair<string, JToken> Pair in (JObject)InTokenPair.Value)
            {
                //Creates a new token if token wasnt found on the list already, updates the token when it exists
                if (ComplexElements.ContainsKey(Pair.Key))
                {
                    ComplexElements[Pair.Key].Update(Pair, Name);
                }
                else
                {
                    ComplexElements.Add(Pair.Key, new DataContainer(Pair, Tier, Name, Parent, new Point(Location.X + i, Location.Y + 206), DataOnly));
                }
                i += 103;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public override JToken GetTheData()
        {

        }
    }
}

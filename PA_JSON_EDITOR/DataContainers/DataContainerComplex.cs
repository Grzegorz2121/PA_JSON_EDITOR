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
    class DataContainerComplex : DataContainer, IDataComplex
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (complex)

        public int complex_amount = 0;
        public Dictionary<string, IDataContainer> ComplexElements = new Dictionary<string, IDataContainer>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PARENT FUNCTIONALITY OVERRIDE

        public DataContainerComplex(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Complex;

            foreach (KeyValuePair<string, JToken> Pair in (JObject)InTokenPair.Value)
            {
                ComplexElements.Add(Pair.Key, CreateNewDataContainer(Pair, Tier, Name));
            }
        }

        public override JToken GetTheData()
        {
            JObject OutputArray = new JObject();
            foreach (KeyValuePair<string, IDataContainer> ChildContainer in ComplexElements)
            {
                OutputArray.Add(ChildContainer.Key, ChildContainer.Value.GetTheData());
            }
            return (JToken)OutputArray;
        }

        public override List<string> FindValueInData(string key)
        {
           List<string> results = new List<string>();
            foreach (DataContainer d in ComplexElements.Values)
            {
                List<string> temp = d.FindValueInData(key);
                if (temp != null)
                {
                    foreach (string s in temp)
                    {
                        if (s != null)
                        {
                            results.Add(s);
                        }
                    }
                }

            }
            return results;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //CONTAINER SPECYFIC FUNCTIONS

        public Dictionary<string, IDataContainer> GetTheList()
        {
            return ComplexElements;
        }

        public IDataContainer GetChild(string name)
        {
            return ComplexElements[name];
        }

        public void AddItem(string name, IDataContainer newItem)
        {
            ComplexElements.Add(name, newItem);
        }

        public void EditItem(string name, IDataContainer newItem)
        {
            ComplexElements[name] = newItem;
        }

        public void DeleteItem(string name)
        {
            ComplexElements.Remove(name);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

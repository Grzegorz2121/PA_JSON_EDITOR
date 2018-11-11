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
    class DataContainerArray : DataContainer, IDataArray
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (array)

        public int array_amount = 0;
        public Dictionary<int, IDataContainer> ArrayElements = new Dictionary<int, IDataContainer>();

        protected DataContainer ArraysTemplate;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PARENT FUNCTIONALITY OVERRIDE

        public DataContainerArray(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Array;

            //Array will create template and redirect all data from other array members to it.
            foreach (JToken ArraysToken in (JArray)InTokenPair.Value)
            {
                ArrayElements.Add(array_amount, CreateNewDataContainer(new KeyValuePair<string, JToken>(array_amount.ToString(), ArraysToken), Tier, Name));
                array_amount++;
            }
        }

        public override JToken GetTheData()
        {
            JArray OutputArray = new JArray();
            foreach(IDataContainer ChildContainer in ArrayElements.Values)
            {
                OutputArray.Add(ChildContainer.GetTheData());
            }
            return (JToken)OutputArray;
        }

        public override List<string> FindValueInData(string key)
        {
            List<string> results = new List<string>();
            foreach (DataContainer d in ArrayElements.Values)
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

        public IDataContainer GetChild(int position)
        {
            return ArrayElements[position];
        }

        public Dictionary<int, IDataContainer> GetTheList()
        {
            return ArrayElements;
        }

        public void AddItem(IDataContainer newValue)
        {
            ArrayElements.Add(ArrayElements.Count, newValue);
        }

        public void EditItem(int position, IDataContainer newValue)
        {
            ArrayElements[position] = newValue;
        }

        public void DeleteItem(int position)
        {
            ArrayElements.Remove(position);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

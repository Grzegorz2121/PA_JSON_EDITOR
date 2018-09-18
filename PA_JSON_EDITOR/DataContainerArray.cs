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
    class DataContainerArray : DataContainer, IDataArray
    {
        //For arrays
        public int ArrayAmount = 0;
        public Dictionary<int, IDataContainer> ArrayElements = new Dictionary<int, IDataContainer>();

        protected DataContainer ArraysTemplate;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataContainerArray(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Array;

            //Array will create template and redirect all data from other array members to it.
            foreach (JToken ArraysToken in (JArray)InTokenPair.Value)
            {
                ArrayElements.Add(ArrayAmount, CreateNewDataContainer(new KeyValuePair<string, JToken>(ArrayAmount.ToString(), ArraysToken), Tier, Name));
                ArrayAmount++;
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int GetAmountOfItems()
        {
            return ArrayElements.Count;
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
    }
}

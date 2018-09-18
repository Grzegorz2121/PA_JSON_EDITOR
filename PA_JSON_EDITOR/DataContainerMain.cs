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
    class DataContainerMain : IDataComplex
    {
        //For complex containers
        public int ComplexAmount = 0;
        public Dictionary<string, IDataContainer> ComplexElements = new Dictionary<string, IDataContainer>();

        public DataContainerMain(string path)
        {
            string[] temp = path.Split('\\');
            
            using (StreamReader sr = new StreamReader(path))
            {
                foreach(KeyValuePair<string, JToken> Pair in (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd()))
                {
                    ComplexElements.Add(Pair.Key, CreateNewDataContainer(Pair, 0, temp[temp.Length - 1]));
                }
            }
        }

        private IDataContainer CreateNewDataContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string ParentName)
        {
            switch (InputToken.Value.Type)
            {
                case JTokenType.Array:
                    return new DataContainerArray(InputToken, ParentTier, ParentName);

                case JTokenType.Object:
                    return new DataContainerComplex(InputToken, ParentTier, ParentName);

                case JTokenType.Null:
                    return null;

                default:
                    return new DataContainerPrimitive(InputToken, ParentTier, ParentName);

            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string[] GetItemNames()
        {
            return ComplexElements.Keys.ToArray<string>();
        }

        public int GetAmountOfItems()
        {
            return ComplexElements.Count;
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

        public void SaveTheJson(string path)
        {
            JObject job = new JObject();

            foreach (DataContainer children in ComplexElements.Values)
            {
                job.Add(children.Name, children.GetTheData());
            }

            using (StreamWriter file = File.CreateText(path))
            {
                file.Write(JsonConvert.SerializeObject(job, Formatting.Indented));
            }
        }
    }
}

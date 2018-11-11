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
    public class DataContainerMain
    {

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (main)

        public int ComplexAmount = 0;
        public Dictionary<string, IDataContainer> ComplexElements = new Dictionary<string, IDataContainer>();
        public string path1;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // FILE READING FUNCTIONALITY + CHILD CREATION

        public DataContainerMain(string path)
        {
            string[] temp = path.Split('\\');

            path1 = temp[temp.Length - 1];

            using (StreamReader sr = new StreamReader(path))
            {
                foreach (KeyValuePair<string, JToken> Pair in (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd()))
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
                    return new DataContainerNull(InputToken, ParentTier, ParentName);

                default:
                    return new DataContainerPrimitive(InputToken, ParentTier, ParentName);

            }

        }

        public List<string> FindValueInData(string key)
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

            for (int i = 0; i < results.Count; i++)
            {
                results[i] = path1 + @":" + results[i];
            }
            return results;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //CONTAINER SPECYFIC FUNCTIONS

        public string[] GetNamesOfMainProperties()
        {
            return ComplexElements.Keys.ToArray<string>();
        }

        public string[] GetItemNames()
        {
            return ComplexElements.Keys.ToArray<string>();
        }

        public int GetAmountOfItems()
        {
            return ComplexElements.Count;
        }

        public IDataContainer GetChild(string name)
        {
            return ComplexElements[name];
        }

        public IDataContainer[] GetChilden()
        {
            return ComplexElements.Values.ToArray<IDataContainer>();
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

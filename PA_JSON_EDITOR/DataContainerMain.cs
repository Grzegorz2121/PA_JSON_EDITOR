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
    class DataContainerMain : DataContainer, IDataComplex
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="path"></param>
        public DataContainer(string path, Form InParent)
        {
            Location = new Point(10, 50);

            Name = "";

            ContainerType = DataContainerType.Complex;

            IsMain = true;

            Parent = InParent;

            ReadTheJson(path);
        }

        public DataContainer(string path)
        {
            Name = "";

            ContainerType = DataContainerType.Complex;

            IsMain = true;

            DataOnly = true;

            ReadTheJson(path);
        }

        public void ReadTheJson(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                Update(new KeyValuePair<string, JToken>(Name, JsonConvert.DeserializeObject(sr.ReadToEnd()) as JObject), Name);
            }
        }

        //For complex containers
        public int ComplexAmount = 0;
        public Dictionary<string, DataContainer> ComplexElements = new Dictionary<string, DataContainer>();

        public void UpdateComplex(KeyValuePair<string, JToken> InputToken)
        {
            int i = 0;
            //If the Token is complex it has to be a JObject with dictonary of next tokens
            foreach (KeyValuePair<string, JToken> Pair in (JObject)InputToken.Value)
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

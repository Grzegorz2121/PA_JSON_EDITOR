using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
//using static Pa_Looker_2.Folder_tools;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;

namespace PA_JSON_EDITOR
{

    public class DataContainer
    {

        public enum DataContainerType
        {
            Primitive,
            Complex,
            Array,
            Null
        }

        //Universal usage
        protected DataContainerType ContainerType;
        public bool IsMain = false;
        public int Tier = 0;
        public string Name = "";
        public string filename;

        //For primitive containers
        protected object PrimitiveElement = new object();
        protected Type PrimitiveType;

        //For complex containers
        protected int ComplexAmount = 0;
        protected Dictionary<string, DataContainer> ComplexElements = new Dictionary<string, DataContainer>();

        //For arrays
        protected int ArrayAmount = 0;
        protected Dictionary<int, DataContainer> ArrayElements = new Dictionary<int, DataContainer>();

        protected DataContainer ArraysTemplate;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Creates new DataContainer class used for json loading, editing, saving, modyfying
        /// </summary>
        public DataContainer()
        {
            Name = "";

            ContainerType = DataContainerType.Complex;

            IsMain = true;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="path"></param>
        public DataContainer(string path)
        {
            Name = "";

            ContainerType = DataContainerType.Complex;

            IsMain = true;

            ReadTheJson(path);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ReadTheJson(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                Update(new KeyValuePair<string, JToken>(Name, JsonConvert.DeserializeObject(sr.ReadToEnd()) as JObject), Name);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //TREE CREATION AND POPULATION

        /// <summary>
        /// Constructor used for recursive tree creation
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public DataContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string parent)
        {
            Name = InputToken.Key;

            Tier = ++ParentTier;

            ContainerType = GetTheTokenType(InputToken.Value);

            Update(InputToken, parent);
        }
       
        public void Update(KeyValuePair<string, JToken> InputToken, string parent_name)
        {
           switch(GetTheTokenType(InputToken.Value))
           {
                case DataContainerType.Array:
                    UpdateArray(InputToken);
                    break;

                case DataContainerType.Complex:
                    UpdateComplex(InputToken);
                    break;

                case DataContainerType.Primitive:
                    UpdatePrimitive(InputToken);
                    break;
           }
        }

        public void UpdateComplex(KeyValuePair<string, JToken> InputToken)
        {
            //If the Token is complex it has to be a JObject with dictonary of next tokens
            foreach(KeyValuePair<string, JToken> Pair in (JObject)InputToken.Value)
            {
                //Creates a new token if token wasnt found on the list already, updates the token when it exists
                if(ComplexElements.ContainsKey(Pair.Key))
                {
                    ComplexElements[Pair.Key].Update(Pair, Name);
                }
                else
                {
                    ComplexElements.Add(Pair.Key, new DataContainer(Pair, Tier, Name));
                }
            }
        }

        public void UpdateArray(KeyValuePair<string, JToken> InputToken)
        {
            //Array will create template and redirect all data from other array members to it.
            
            foreach (JToken ArraysToken in (JArray)InputToken.Value)
            {
                ArrayElements.Add(ArrayAmount, new DataContainer(new KeyValuePair<string, JToken>(ArrayAmount.ToString(), ArraysToken), Tier, Name));
                ArrayAmount++;
            }
        }

        public void UpdatePrimitive(KeyValuePair<string, JToken> InputToken)
        {
            PrimitiveType = InputToken.Value.ToObject<object>().GetType();

            PrimitiveElement = InputToken.Value.ToObject<object>();
        }

        public DataContainerType GetTheTokenType(JToken token)
        {
            if (token == null)
            {
                return DataContainerType.Null;
            }

            switch(token.Type)
            {
                case JTokenType.Array:
                    return DataContainerType.Array;

                case JTokenType.Object:
                    return DataContainerType.Complex;

                case JTokenType.Null:
                    return DataContainerType.Null;

                default:
                    return DataContainerType.Primitive;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public JToken GetTheData()
        {

            switch (ContainerType)
            {
                case DataContainerType.Complex:

                    JObject job = new JObject();

                    foreach (DataContainer children in ComplexElements.Values)
                    {
                       job.Add(children.Name, children.GetTheData());
                    }

                    return job;
                    break;

                case DataContainerType.Primitive:

                    JToken token = JToken.FromObject(PrimitiveElement);

                    return token;
                    break;

                case DataContainerType.Array:

                    JArray job2 = new JArray();

                    foreach (DataContainer children in ArrayElements.Values)
                    {
                        job2.Add(children.GetTheData());
                    }

                    return job2 as JToken;
                    break;

                default:

                    return new JObject("REEEEEEEEE");

                    break;
            }
            /*
            switch (ContainerType)
            {
                case DataContainerType.Complex:
                    foreach (DataContainer Children in ComplexElements.Values)
                    {
                        Children.GetTheData();
                    }
                    break;

                case DataContainerType.Primitive:

                    break;

                case DataContainerType.Array:
                    ArraysTemplate.GetTheData();
                    break;
            }*/

        }

        public void SaveTheJson(string path)
        {
            JObject job = new JObject();

            foreach(DataContainer children in ComplexElements.Values)
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

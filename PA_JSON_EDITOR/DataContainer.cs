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
        GraphicalContainer graphicalContainer;
        public Form Parent;
        public Point Location;

        public DataContainerType ContainerType
        { get; private set; }

        public bool DataOnly = false;
        public bool IsMain { get; private set; } = false;
        public int Tier { get; private set; } = 0;
        public string Name { get; private set; } = "";
        public string filename { get; private set; }

        //For primitive containers
        public object PrimitiveElement = new object();
        protected Type PrimitiveType;

        //For complex containers
        public int ComplexAmount = 0;
        public Dictionary<string, DataContainer> ComplexElements = new Dictionary<string, DataContainer>();

        //For arrays
        public int ArrayAmount = 0;
        public Dictionary<int, DataContainer> ArrayElements = new Dictionary<int, DataContainer>();

        protected DataContainer ArraysTemplate;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public DataContainer(string path)
        {
            Name = "";

            ContainerType = DataContainerType.Complex;

            IsMain = true;

            DataOnly = true;

            ReadTheJson(path);
        }

        /// <summary>
        /// Creates new DataContainer class used for json loading, editing, saving, modyfying
        /// </summary>
        public DataContainer(Form InParent)
        {
            Name = "";

            ContainerType = DataContainerType.Complex;

            IsMain = true;

            Parent = InParent;
        }

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
        public DataContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string parent, Form InParent, Point InLocation, bool inDataOnly)
        {
            Name = InputToken.Key;

            Tier = ++ParentTier;

            ContainerType = GetTheTokenType(InputToken.Value);

            Parent = InParent;

            Location = new Point(InLocation.X, InLocation.Y);

            DataOnly = inDataOnly;

            Update(InputToken, parent);
        }
       
        public void Update(KeyValuePair<string, JToken> InputToken, string parent_name)
        {
           switch(GetTheTokenType(InputToken.Value))
           {
                case DataContainerType.Array:
                    UpdateArray(InputToken);

                    if(!DataOnly)
                    {
                        graphicalContainer = new GraphicalContainer(Parent, this, Location, !IsMain);
                    }

                    break;

                case DataContainerType.Complex:
                    UpdateComplex(InputToken);

                    if (!DataOnly)
                    {
                        graphicalContainer = new GraphicalContainer(Parent, this, Location, !IsMain);
                    }       

                    break;

                case DataContainerType.Primitive:
                    UpdatePrimitive(InputToken);

                    if (!DataOnly)
                    {
                        graphicalContainer = new GraphicalContainer(Parent, this, Location, !IsMain);
                    }
                        

                    break;
           }
        }


        public void UpdateComplex(KeyValuePair<string, JToken> InputToken)
        {
            int i = 0; 
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
                    ComplexElements.Add(Pair.Key, new DataContainer(Pair, Tier, Name, Parent, new Point(Location.X+i, Location.Y+206), DataOnly));
                }
                i += 103;
            }
        }

        public void UpdateArray(KeyValuePair<string, JToken> InputToken)
        {
            //Array will create template and redirect all data from other array members to it.
            int i = 0;
            foreach (JToken ArraysToken in (JArray)InputToken.Value)
            {
                ArrayElements.Add(ArrayAmount, new DataContainer(new KeyValuePair<string, JToken>(ArrayAmount.ToString(), ArraysToken), Tier, Name, Parent, new Point(Location.X + i, Location.Y + 206), DataOnly));
                ArrayAmount++;
                i += 103;
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

        public void Delete(string item)
        {

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

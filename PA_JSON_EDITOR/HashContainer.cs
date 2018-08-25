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

    public class HashContainer
    {

        public enum HashContainerType
        {
            Primitive,
            Complex,
            Array,
            Null
        }

        //Universal usage
        protected HashContainerType ContainerType;
        public bool IsMain = false;
        public int Tier = 0;
        public string Name = "";
        public HashSet<string> Parents = new HashSet<string>();
        public string filename;

        //For primitive containers
        protected int PrimitiveAmount = 0;
        protected List<object> PrimitiveElements = new List<object>();
        protected Type PrimitiveType;

        //For complex containers
        protected int ComplexAmount = 0;
        protected Dictionary<string, HashContainer> ComplexElements = new Dictionary<string, HashContainer>();

        //For arrays
        protected int ArrayAmount = 0;
        protected Dictionary<int, HashContainer> ArrayElements = new Dictionary<int, HashContainer>();

        protected HashContainer ArraysTemplate;

        protected HashContainer ArrayComplexTemplate;

        protected object ArrayPrimitiveTemplate;
        protected Type child_primive_type;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Takes paths to all jsons, relative path and the name of the tree, gives the root of the HashContainerTree
        /// </summary>
        /// <param name="path"></param>
        /// 
        public HashContainer(string path, string treename, string in_searchword, string in_negative_searchword)
        {
            
            Name = "";

            filename = treename;

            ContainerType = HashContainerType.Complex;

            IsMain = true;

            Parents.Add("");

            UpdateMain(path, in_searchword, in_negative_searchword);
        }

        public void UpdateMain(string path, string searchword, string negative_searchword)
        {
            List<string> temp = new List<string>();
            foreach(string s in negative_searchword.Split('|'))
            {
                temp.AddRange(Directory.GetFiles(path, s, SearchOption.AllDirectories));
            }
            string[] NegativeJsonFiles = temp.ToArray();
            string[] JsonFiles = Directory.GetFiles(path, searchword, SearchOption.AllDirectories);

            List<JObject> JsonFilesJObjects = new List<JObject>();
            foreach(string JsonFile in JsonFiles.Except(NegativeJsonFiles))
            {
                using (StreamReader sr = new StreamReader(JsonFile))
                {

                    Update(new KeyValuePair<string, JToken>(Name, JsonConvert.DeserializeObject(sr.ReadToEnd()) as JObject), Name);
                }
            }

            //return JsonFilesJObjects.ToArray<JObject>();
        }
        

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Constructor used for recursive tree creation
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public HashContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string parent)
        {
            Name = InputToken.Key;

            Tier = ++ParentTier;

            ContainerType = GetTheTokenType(InputToken.Value);

            Update(InputToken, parent);
        }

        public void Update(KeyValuePair<string, JToken> InputToken, string parent_name)
        {
            Parents.Add(parent_name);
            if(GetTheTokenType(InputToken.Value)!=ContainerType)
            {
                return;
            }
           // switch (ContainerType)
           switch(GetTheTokenType(InputToken.Value))
           {
                case HashContainerType.Array:
                    UpdateArray(InputToken);
                    break;

                case HashContainerType.Complex:
                    UpdateComplex(InputToken);
                    break;

                case HashContainerType.Primitive:
                    UpdatePrimitive(InputToken);
                    break;
           }

        }

        public void UpdateComplex(KeyValuePair<string, JToken> InputToken)
        {

            //TODO: Fix cases where a token can contain different types
            /*if (GetTheTokenType(InputToken.Value, this) != ContainerType)
            {
                return;
            }*/

            //If the Token is complex it has to be a JObject with dictonary of next tokens
            foreach(KeyValuePair<string, JToken> Pair in (JObject)InputToken.Value)
            {
                PrimitiveAmount++;
                //Creates a new token if token wasnt found on the list already, updates the token when it exists
                if(ComplexElements.ContainsKey(Pair.Key))
                {
                    ComplexElements[Pair.Key].Update(Pair, Name);
                }
                else
                {
                    ComplexElements.Add(Pair.Key, new HashContainer(Pair, Tier, Name));
                }
            }
        }

        public void UpdateArray(KeyValuePair<string, JToken> InputToken)
        {

            //Array will create template and redirect all data from other array members to it.
            if (ArraysTemplate != null)
            {
                foreach (JToken ArraysToken in (JArray)InputToken.Value)
                {
                    ArrayAmount++;
                    ArraysTemplate.Update(new KeyValuePair<string, JToken>(Name+@"_Template", ArraysToken), Name);
                }
            }
            else
            {
                ArraysTemplate = new HashContainer(new KeyValuePair<string, JToken>(Name+@"_Template", InputToken.Value.First), --Tier, Name);
            }

        }

        public void UpdatePrimitive(KeyValuePair<string, JToken> InputToken)
        {

            PrimitiveType = InputToken.Value.ToObject<object>().GetType();
            PrimitiveAmount++;

            if (Name == "ammo_id")
            {
                Console.WriteLine();
                if (PrimitiveType != typeof(String))
                {
                    Console.WriteLine();
                }
            }

            if (!PrimitiveElements.Contains(InputToken.Value.Value<object>()))
            {
                PrimitiveElements.Add(InputToken.Value.Value<object>());
            }
        }

       public HashContainerType GetTheTokenType(JToken token)
       {
            if (token == null)
            {
                return HashContainerType.Null;
            }

            switch(token.Type)
            {
                case JTokenType.Array:
                    return HashContainerType.Array;

                case JTokenType.Object:
                    return HashContainerType.Complex;

                case JTokenType.Null:
                    return HashContainerType.Null;

                default:
                    return HashContainerType.Primitive;
            }
       }

        public class Content
        {
            public string Type;
            public string PrimitiveType;
            public string ArrayTemplate;
            public string[] ComplexChildren;
            public int Tier;
            public string[] Parent;

            public Content(HashContainerType in_type, object in_elements, int in_tier, HashSet<string> parents)
            {
                Type = in_type.ToString();
                switch(in_type)
                {
                    case HashContainerType.Array:
                        ArrayTemplate = (string)in_elements;
                        break;
                    case HashContainerType.Primitive:
                        PrimitiveType = (string)in_elements;
                        break;
                    case HashContainerType.Complex:
                        var temp = in_elements as List<string>;
                        ComplexChildren = temp.ToArray();
                        break;
                }
                Parent = parents.ToArray();
                Tier = in_tier;
            }
        }

        public void GetTheData(Dictionary<string, Content> ListOfProperties)
        {
            Content ContainerHashedInfo;

            switch (ContainerType)
            {
                case HashContainerType.Complex:
                    List<string> Temp = new List<string>();
                    foreach(string ChildrenName in ComplexElements.Keys)
                    {
                        Temp.Add(ChildrenName);
                    }
                    ContainerHashedInfo = new Content(ContainerType, Temp, Tier, Parents);
                    break;

                case HashContainerType.Primitive:
                    ContainerHashedInfo = new Content(ContainerType, PrimitiveType.ToString(), Tier, Parents);
                    break;

                case HashContainerType.Array:
                    ContainerHashedInfo = new Content(ContainerType, ArraysTemplate.Name, Tier, Parents);
                    break;

                default:
                    ContainerHashedInfo = new Content(ContainerType, "ERROR", Tier, Parents);
                    break;
            }

            //Sometimes few tokens can contain the same children with the same type, that check prevents putting them twice in a dictonary
            if(!ListOfProperties.ContainsKey(Name))
            {
                ListOfProperties.Add(Name, ContainerHashedInfo);
            }


            switch (ContainerType)
            {
                case HashContainerType.Complex:
                    foreach(HashContainer Children in ComplexElements.Values)
                    {
                        Children.GetTheData(ListOfProperties);
                    }
                    break;

                case HashContainerType.Primitive:

                    break;

                case HashContainerType.Array:
                    ArraysTemplate.GetTheData(ListOfProperties);
                    break;
            }
            
        }

        public void CreateTheDump(string path)
        {

            Dictionary<string, Content> ListOfProperties = new Dictionary<string, Content>();

            foreach (HashContainer Children in ComplexElements.Values)
            {
                Children.GetTheData(ListOfProperties);
            }

            JObject job = JObject.FromObject(ListOfProperties);

            using (StreamWriter file = File.CreateText(path + filename + @"PropertiesList.json"))
            {
                file.Write(JsonConvert.SerializeObject(job, Formatting.Indented));
            }

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}

/*
string json = JsonConvert.SerializeObject(account, Formatting.Indented);
// {
//   "Email": "james@example.com",
//   "Active": true,
//   "CreatedDate": "2013-01-20T00:00:00Z",
//   "Roles": [
//     "User",
//     "Admin"
//   ]
// }*/





/*
 *         public void CreateTheDump(string path)
    {
        Dictionary<string, object> content = new Dictionary<string, object>();

        switch (ContainerType)
        {
            case HashContainerType.Complex:
                content.Add("Name", Name);
                content.Add("Tier", Tier);
                content.Add("Type", ContainerType.ToString());
                content.Add("Children", ComplexElements.Keys);
                using (StreamWriter file = File.CreateText(path + @"\dump" + Tier + @".json"))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.Serialize(file, content);
                }

                foreach(HashContainer Children in ComplexElements.Values)
                {
                    Children.CreateTheDump(path);
                }
                break;

            case HashContainerType.Primitive:
                content.Add("Name", Name);
                content.Add("Tier", Tier);
                content.Add("Type", ContainerType.ToString());
                content.Add("Values", PrimitiveElements);
                using (StreamWriter file = File.CreateText(path + @"dump" + Tier + Name + @".json"))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.Serialize(file, content);
                }
                break;

            case HashContainerType.Array:
                content.Add("Name", Name);
                content.Add("Tier", Tier);
                content.Add("Type", ContainerType.ToString());
                content.Add("Template", ArraysTemplate.Name);
                using (StreamWriter file = File.CreateText(path + @"\dump" + Tier + Name + @".json"))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.Serialize(file, content);
                }

                ArraysTemplate.CreateTheDump(path);
                break;
        }


    }
     */

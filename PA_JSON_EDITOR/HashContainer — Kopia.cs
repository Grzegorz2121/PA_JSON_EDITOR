using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static Pa_Looker_2.Folder_tools;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using static Pa_Looker_2.ICallback;

namespace PA_JSON_EDITOR
{
    
    public class HashContainer
    {

       

        //Object info
        //Shows type of the object
        protected string ObjectType;
        //Shows is that object is nessesary
        protected bool ObjectIsNecessary;
        //For all types
        protected List<object> ObjectExamples = new List<object>();

        //Shows values of the object
        protected object ObjectValue = new object();
        //For arrays (complex)
        protected KeyValuePair<string, string> ObjectTemplateObject = new KeyValuePair<string, string>();

        protected JObject jObject = new JObject();




        //For primitive containers
        protected List<object> child_primitive_elements = new List<object>();

        //For complex containers
        protected Dictionary<string, HashContainer> child_elements_complex = new Dictionary<string, HashContainer>();

        //For arrays
        protected Dictionary<int, HashContainer> child_elements_array = new Dictionary<int, HashContainer>();
        protected HashContainer child_template;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor when provided with a path to json file will create a tree of containers. 
        /// That method automaticly creates origin container
        /// </summary>
        /// <param name="path"></param>
        public HashContainer(string[] paths, string mainpath)
        {
            IsOriginObject = true;

            HashContainerType = JTokenType.Object;

            //Iterates throught the jsons
            foreach (string path in paths)
            {
                string filename = path.Remove(0, mainpath.Length);
                using (StreamReader sr = new StreamReader(path))
                {
                    JObject json_jobject = JsonConvert.DeserializeObject(sr.ReadToEnd()) as JObject;

                    Update(new KeyValuePair<string, JToken>("", json_jobject));

                }
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor when provided with raw jobject will create a tree that allows easy data managment.
        /// Constructor is only called once, after that the whole tree will be created recusivly
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public HashContainer(KeyValuePair<string, JToken> named_token)
        {
           
            jObject.Add("ObjectType", ObjectType);
            jObject.Add("ObjectValue", ObjectType);
            jObject.Add("ObjectIsNecesarry", ObjectType);
           // jObject.Add

            IsOriginObject = false;

            name = named_token.Key;

            HashContainerType = named_token.Value.Type;

            Update(named_token); 
        }

        public void Update(KeyValuePair<string, JToken> named_token)
        {

            JTokenType ContainerType = named_token.Value.Type;

            switch (ContainerType)
            {
                case JTokenType.Array:
                    bool first_time = true;
                    foreach(JToken token in named_token.Value)
                    {

                        if(first_time)
                        {
                            child_template = new HashContainer(new KeyValuePair<string, JToken>("TEMPLATE", token));
                            first_time = false;
                        }
                        else
                        {
                            child_template.Update(new KeyValuePair<string, JToken>("TEMPLATE", token));
                        }
                        /*
                        if (child_elements_array.ContainsKey(i))
                        {
                            child_elements_array[i].Update(new KeyValuePair<string, JToken>(i.ToString(), token));
                        }
                        else
                        {
                            child_elements_array.Add(i, new HashContainer(new KeyValuePair<string, JToken>(i.ToString(), token)));
                        }
                        
                        i++;*/
                    }

                    break;

                case JTokenType.Object:

                    foreach (KeyValuePair<string, JToken> children_token in (JObject)named_token.Value)
                    {
                        if(child_elements_complex.ContainsKey(children_token.Key))
                        {
                            child_elements_complex[children_token.Key].Update(children_token);
                        }
                        else
                        {
                            child_elements_complex.Add(children_token.Key, new HashContainer(children_token));
                        }
                        
                    }

                    break;

                default:

                    if(!child_primitive_elements.Contains(named_token.Value.ToObject(typeof(String))))
                    {
                        child_primitive_elements.Add(named_token.Value.ToObject(typeof(String)));
                    }
                    

                    break;
            }
            
            
        }

        public JToken GetTheData()
        {
            switch(HashContainerType)
            {
                case JTokenType.Array:
                    
                   // Dictionary<string, JToken> children_names_and_valuesa = new Dictionary<string, JToken>();
                   /*
                    foreach (KeyValuePair<int, HashContainer> child_container in child_elements_array)
                    {
                        children_names_and_valuesa.Add(child_container.Key.ToString(), child_container.Value.GetTheData());
                    }

                    JObject return_jobjecta = new JObject(children_names_and_valuesa);*/
                   // children_names_and_valuesa.Add("TEMPLATE", child_template.GetTheData());
                   // List<JToken> Jtokenlist = new List<JToken>();
                   // Jtokenlist.Add(child_template.GetTheData());
                   // JObject return_jobjecta = new JObject();
                    return JToken.FromObject(child_template.GetTheData());
                   // return return_jobjecta as JToken;

                    break;

                case JTokenType.Object:

                    Dictionary<string, JToken> children_names_and_values = new Dictionary<string, JToken>();
                    foreach (KeyValuePair<string, HashContainer> child_container in child_elements_complex)
                    {
                        children_names_and_values.Add(child_container.Key, child_container.Value.GetTheData());
                    }
                    return JToken.FromObject(children_names_and_values);
                    //JObject return_jobject = new JObject(children_names_and_values);
                   // return return_jobject as JToken;

                    break;

                default:

                    return JToken.FromObject(child_primitive_elements);
                    

                break;
            }
               
        }

        public void CreateTheDump(string path)
        {
            using (StreamWriter file = File.CreateText(path+@"\dump.json"))
            {
                JsonSerializer js = new JsonSerializer();
                js.Serialize(file, GetTheData());
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
}















/*
        public void CreateTheDump(string path)
        {
            JObject ob = new JObject();
            foreach(KeyValuePair<string, HashContainer> hc in child_elements)
            {
               ob.Add(hc.Key, hc.Value.GetTheData());
            }
            using (StreamWriter sw = new StreamWriter(path + "\\dump.json"))
            {
                JsonSerializer js = new JsonSerializer();
                js.Serialize(sw, ob);
            }
        }
        
        public JToken GetTheData()
        {
            if(ContainerType == HashContainerType.complex)
            {
                JObject ob = new JObject();
                foreach (KeyValuePair<string, HashContainer> hc in child_elements)
                {
                    ob.Add(hc.Key, hc.Value.GetTheData());
                }
                return ob as JToken;
            }
            else
            {
               return JToken.FromObject(PrimitiveContents);
            }
        }

        public void Update(JToken jobject, string filename)
        {

            //Check for complex vs primitive
            if (jobject.HasValues)
            {
                ContainerType = HashContainerType.complex;
                //Populate children list with next level/tier tokens
                //Every token will execute the same procedure eventualy creating a tree
                //The main container is the origin of the tree, primitive containers are the end of the branches

                JObject job = jobject as JObject;

                foreach (KeyValuePair<string, JToken> pair in job)
                {
                    if (!child_elements.ContainsKey(pair.Key))
                    {
                        if (pair.Value.Type != JTokenType.Array)
                        {
                            child_elements.Add(pair.Key, new HashContainer(pair.Value, pair.Key, filename, GetContainerType(pair.Value) ,false));
                        }
                    }
                    else
                    {
                        if (pair.Value.Type != JTokenType.Array)
                        {
                            child_elements[pair.Key].Update(pair.Value, filename);
                        }
                    }

                }

            }
            else
            {
                ContainerType = HashContainerType.primitive;
                //If token is primitive we can extract value from it
                PrimitiveContents.Add(jobject.ToObject<object>());
            }
        }   

        public void Dispose()
        {
            if (ContainerType == HashContainerType.complex)
            {
                foreach (HashContainer c in child_elements.Values)
                {
                    c.Dispose();

                }
                child_elements = null;
            }

        }*/

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
    public class DataContainer
    {
        //Universal usage
        protected JTokenType DataContainerType;

        //For primitive containers
        protected object child_primitive_element = new object();

        //For complex containers
        protected Dictionary<string, DataContainer> child_elements_complex = new Dictionary<string, DataContainer>();

        //For arrays
        protected Dictionary<int, DataContainer> child_elements_array = new Dictionary<int, DataContainer>();
        protected DataContainer child_template;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor when provided with a path to json file will create a tree of containers. 
        /// That method automaticly creates origin container
        /// </summary>
        /// <param name="path"></param>
        public DataContainer(string path, string mainpath)
        {
            DataContainerType = JTokenType.Object;
            
            string filename = path.Remove(0, mainpath.Length);
            using (StreamReader sr = new StreamReader(path))
            {
                JObject json_jobject = JsonConvert.DeserializeObject(sr.ReadToEnd()) as JObject;

                Update(json_jobject);
            }
            
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor when provided with raw jobject will create a tree that allows easy data managment.
        /// Constructor is only called once, after that the whole tree will be created recusivly
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public DataContainer(JToken Main_token)
        {
            DataContainerType = Main_token.Type;

            Update(Main_token);
        }

        public void Update(JToken input_token)
        {

            JTokenType ContainerType = input_token.Type;

            switch (ContainerType)
            {
                case JTokenType.Array:

                    int i = 0;
                    foreach (JToken token in input_token)
                    {
                        child_template = new DataContainer(token);

                        child_elements_array.Add(i, new DataContainer(token));
                        i++;
                    }

                    break;

                case JTokenType.Object:

                    foreach (KeyValuePair<string, JToken> children_token in (JObject)input_token)
                    {
                          child_elements_complex.Add(children_token.Key, new DataContainer(children_token.Value));
                    }

                    break;

                default:

                    child_primitive_element = input_token.Value<object>();

                    break;
            }


        }
        
    }
}





/*
//protected GraphicalBlock graphicalBlock;
//Universal usage
protected bool IsOriginObject;
protected bool IsComplex;

//For primitive containers
protected object PrimitiveContent;

//For complex containers
protected DataContainer child_container;
protected Dictionary<string, DataContainer> child_elements = new Dictionary<string, DataContainer>();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/// <summary>
/// Constructor when provided with a path to json file will create a tree of containers. 
/// That method automaticly creates origin container
/// </summary>
/// <param name="path"></param>
public DataContainer(string path)
{
    IsOriginObject = true;

    using (StreamReader sr = new StreamReader(path))
    {
        JToken jobject = JsonConvert.DeserializeObject(sr.ReadToEnd()) as JToken;

        //Check for complex vs primitive
        if (jobject.HasValues)
        {
            IsComplex = true;
            //Populate children list with next level/tier tokens
            //Every token will execute the same procedure eventualy creating a tree
            //The main container is the origin of the tree, primitive containers are the end of the branches

            JObject job = jobject as JObject;

            foreach (KeyValuePair<string, JToken> pair in job)
            {
                if (pair.Value.Type != JTokenType.Array)
                {
                    child_elements.Add(pair.Key, new DataContainer(pair.Value, pair.Key, false));
                }
            }

        }
        else
        {
            IsComplex = false;
            //If token is primitive we can extract value from it
            PrimitiveContent = jobject.ToObject<object>();
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
public DataContainer(JToken input_jobject, string name, bool Is_orig_obj = true)
{
    IsOriginObject = Is_orig_obj;

    //Check for complex vs primitive
    if (input_jobject.HasValues)
    {
        IsComplex = true;
        //Populate children list with next level/tier tokens
        //Every token will execute the same procedure eventualy creating a tree
        //The main container is the origin of the tree, primitive containers are the end of the branches

        JObject job = input_jobject as JObject;
        foreach (KeyValuePair<string, JToken> pair in job)
        {
            if (pair.Value.Type != JTokenType.Array)
            {
                child_elements.Add(pair.Key, new DataContainer(pair.Value, pair.Key, false));
            }
        }
    }
    else
    {
        IsComplex = false;
        //If token is primitive we can extract value from it
        PrimitiveContent = input_jobject.ToObject<object>();
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public void Dispose()
{
    if (IsComplex)
    {
        foreach (DataContainer c in child_elements.Values)
        {
            c.Dispose();

        }
        child_elements = null;
    }

}*/
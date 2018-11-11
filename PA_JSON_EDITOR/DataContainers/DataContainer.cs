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

    public abstract class DataContainer : IDataContainer
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (universal)

        //Define all possible Types of tokens.
        public enum DataContainerType
        {
            Primitive,
            Complex,
            Array,
            Null
        }

        public string Name  {get; protected set; } = "";
        public int Tier {get; protected set; } = 0;

        public DataContainerType ContainerType
        { get; protected set; }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //TREE CREATION AND POPULATION

        /// <summary>
        /// Constructor used for recursive tree creation.
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public DataContainer(KeyValuePair<string, JToken> inToken, int parentTier, string parentName)
        {
            Name = inToken.Key;
            Tier = ++parentTier;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //GENERAL METHODS (CAN BE USED IN EVERY OBJECT TYPE [primitive, array, complex])

        public DataContainer.DataContainerType GetTheType()
        {
            return ContainerType;
        }

        public string GetTheName()
        {
            return Name;
        }

        public abstract List<string> FindValueInData(string key);

        public abstract JToken GetTheData();

        public virtual IDataContainer CreateNewDataContainer(KeyValuePair<string, JToken> inToken, int parentTier, string parentName)
        {
            switch (inToken.Value.Type)
            {
                case JTokenType.Array:
                    return new DataContainerArray(inToken, parentTier, parentName);

                case JTokenType.Object:
                    return new DataContainerComplex(inToken, parentTier, parentName);

                case JTokenType.Null:
                    return new DataContainerNull(inToken, parentTier, parentName);

                default:
                    return new DataContainerPrimitive(inToken, parentTier, parentName);

            }
            
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

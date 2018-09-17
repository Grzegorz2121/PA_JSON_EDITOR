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

    public abstract class DataContainer : IDataContainer
    {
        //Define all possible Types of tokens.
        public enum DataContainerType
        {
            Primitive,
            Complex,
            Array,
            Null
        }

        //Data - All DataContainers have to contain their name, tier and type. (All propierties are read only outside)
        public string Name { get; protected set; } = "";
        public int Tier { get; protected set; } = 0;

        public DataContainerType ContainerType
        { get; protected set; }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //TREE CREATION AND POPULATION

        /// <summary>
        /// Constructor used for recursive tree creation. Forces all types of containers to be able to be created from jtokens
        /// </summary>
        /// <param name="input_jobject"></param>
        /// <param name="Is_orig_obj"></param>
        public DataContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string ParentName)
        {
            Name = InputToken.Key;
            Tier = ++ParentTier;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //GENERAL METHODS (CAN BE USED IN EVERY OBJECT TYPE [primitive, array, complex])

        public abstract JToken GetTheData();

        public virtual IDataContainer CreateNewDataContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string ParentName)
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
    }
}

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
    class DataContainerPrimitive : DataContainer, IDataPrimitive
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (primitive)

        public object PrimitiveElement = new object();
        protected Type PrimitiveType;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PARENT FUNCTIONALITY OVERRIDE

        public DataContainerPrimitive(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Primitive;

            PrimitiveType = InTokenPair.Value.ToObject<object>().GetType();

            PrimitiveElement = InTokenPair.Value.ToObject<object>();
 
        }

        public override JToken GetTheData()
        {
            return JToken.FromObject(PrimitiveElement);
        }

        public override List<string> FindValueInData(string key)
        {
            if(PrimitiveType == typeof(string))
            {
                List<string> result = new List<string>();
                string s = PrimitiveElement as string;

                if(s.Contains(key))
                {
                    result.Add(Name + @":" + s);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //CONTAINER SPECYFIC FUNCTIONS

        public object GetValue()
        {
            return PrimitiveElement;
        }

        public void EditValue(object newObject)
        {
            PrimitiveElement = newObject;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

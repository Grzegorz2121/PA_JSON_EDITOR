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
    class DataContainerNull : DataContainer, IDataNull
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // VARIABLES INITIALISATION (null)

        public const object Null = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PARENT FUNCTIONALITY OVERRIDE

        public DataContainerNull(KeyValuePair<string, JToken> InTokenPair, int InParentTier, string InParentName) : base(InTokenPair, InParentTier, InParentName)
        {
            ContainerType = DataContainerType.Null;
        }

        public override JToken GetTheData()
        {
            return JToken.FromObject(Null);
        }

        public override List<string> FindValueInData(string key)
        {
            List<string> result = new List<string>();
            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //CONTAINER SPECYFIC FUNCTIONS

        public object GetValue()
        {
            return Null;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

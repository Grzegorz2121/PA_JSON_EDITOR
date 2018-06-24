using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Pa_Looker_2
{
    interface ICallback
    {
       void Callback(string key, JToken token);
    }
}

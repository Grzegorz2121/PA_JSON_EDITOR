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
    public interface IDataContainer
    {
        JToken GetTheData();
        IDataContainer CreateNewDataContainer(KeyValuePair<string, JToken> InputToken, int ParentTier, string ParentName);
    }

    public interface IDataPrimitive
    {
        object GetValue();
        void EditValue(object newObject);
    }

    public interface IDataArray
    {
        int GetAmountOfItems();

        void AddValue(DataContainer newObject);
        void EditValue(int position, DataContainer newObject);
        void DeleteValue(int position);
    }

    public interface IDataComplex
    {
        string[] GetItemNames();
        int GetAmountOfItems();

        void AddValue(string newName, DataContainer newObject);
        void EditValue(string name, DataContainer newObject);
        void DeleteValue(string name);
    }
}

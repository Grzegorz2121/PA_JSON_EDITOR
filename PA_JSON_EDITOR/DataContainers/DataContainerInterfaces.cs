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
    //TODO: DataContainers should be able to create new childrens. Implement universal function for that
    //Put wrapper functions in array and the complex containers to create new children.
    //Also new empty object constructors are needed (without data but with names, tier ect...) (default data?)
    // primitive -> create new <obj type>
    // array -> create new <obj type> (amount)
    // complex -> create new

    // A way to store constructs? Like a new gun ect... (Of course JSON, implementation matters)

    // Easy: get the items from array and the complex to attach graphical layer elements to them

    public interface IDataContainer
    {
        //Identyfication of the data container (used in graphical layer tree building)
        DataContainer.DataContainerType GetTheType();
        string GetTheName();

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
        //int GetAmountOfItems();
        IDataContainer GetChild(int position);

        Dictionary<int, IDataContainer> GetTheList();

      /*  int[] GetItemNames();
        IDataContainer[] GetChilden();*/

        void AddItem(IDataContainer newObject);
        void EditItem(int position, IDataContainer newObject);
        void DeleteItem(int position);
    }

    public interface IDataComplex
    {
        //int GetAmountOfItems();
        IDataContainer GetChild(string name);
    
        Dictionary<string, IDataContainer> GetTheList();
        /*
        string[] GetItemNames();
        IDataContainer[] GetChilden();*/

        void AddItem(string newName, IDataContainer newObject);
        void EditItem(string name, IDataContainer newObject);
        void DeleteItem(string name);
    }
}

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
    
    public interface IGraphicalContainer
    {
        //TODO: Graphical elements show() hide() dispose() methods

      //  JToken GetTheData();
        IGraphicalContainer CreateNewGraphicalContainer(IDataContainer dataContainer);
        void Show();
        void Hide();
        void Dispose();
    }
    /*
    public interface IGraphicalPrimitive
    {
        object GetValue();
        void EditValue(object newObject);
    }

    public interface IGraphicalArray
    {
        int GetAmountOfItems();

        void AddItem(IDataContainer newObject);
        void EditItem(int position, IDataContainer newObject);
        void DeleteItem(int position);
    }

    public interface IGraphicalComplex
    {
        string[] GetItemNames();
        int GetAmountOfItems();

        void AddItem(string newName, IDataContainer newObject);
        void EditItem(string name, IDataContainer newObject);
        void DeleteItem(string name);
    }*/
}

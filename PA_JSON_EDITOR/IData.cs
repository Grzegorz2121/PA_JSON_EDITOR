using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA_JSON_EDITOR
{
    interface IDataPrimitive
    {
        object GetValue();
        void EditValue(object newObject);
    }

    interface IDataArray
    {
        int GetAmountOfItems();

        void AddValue(DataContainer newObject);
        void EditValue(int position, DataContainer newObject);
        void DeleteValue(int position);
    }

    interface IDataComplex
    {
        string[] GetItemNames();
        int GetAmountOfItems();

        void AddValue(string newName, DataContainer newObject);
        void EditValue(string name, DataContainer newObject);
        void DeleteValue(string name);
    }
}

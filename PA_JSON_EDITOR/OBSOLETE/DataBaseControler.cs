using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;

namespace PA_JSON_EDITOR
{

    static class DataBaseControler
    {
        private class Item
        {
            public string name;
            public string type;
            public string description;
            public string parent;
            public string complex;
        }

        public static string database_path;

        public static void SetDataBasePath(string path)
        {
            database_path = path + "//" + "database";
        }

        public static bool CreateDataBase()
        {
            if(database_path==null)
            {
                return false;
            }

            if(Directory.Exists(database_path))
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(database_path);
                return true;
            }
        }
        //"name":"health_fraction", "type":"float", "description":"The amount of health remaining after being converted to wreckage.", "parent":"wreckage", "advanced":false, "context":"UINTTYPE_Air & UNITTYPE_Fabber"
        public static bool AddItem(string name, string type, string origin = "", string description="", string parent="", string complex="false")
        {
            if (database_path == null)
            {
                return false;
            }

            Item item = new Item();

            item.name = name;
            item.type = type;
            item.description = description;
            item.parent = parent;
            item.complex = complex;

            if(origin==null)
            {
                if (File.Exists(database_path + "\\" + name + ".json"))
                {
                    return false;
                }
                else
                {
                    StreamWriter file = File.CreateText(database_path + "\\" + name + ".json");

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, item);

                    file.Dispose();
                    return true;
                }
            }
            else
            {
                if (File.Exists(database_path + "\\" + name + ".json"))
                {
                    return false;
                }
                else
                {
                    StreamWriter file = File.CreateText(database_path + "\\" + name + ".json");

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, item);

                    file.Dispose();
                    return true;
                }
            }
  
        }
    }
}

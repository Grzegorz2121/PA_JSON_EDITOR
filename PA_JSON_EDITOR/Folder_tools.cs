using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pa_Looker_2
{
    static class Folder_tools
    {
        /*
        public static bool Is_units_folder(string path)
        {
            foreach(string c in Pa_types.Classes)
            {
                if(Directory.Exists(path + c))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool Is_primitive_type(object input)
        {
            Type t = input.GetType();
            if(t.IsPrimitive || t == typeof(String))
            {
                return true;
            }
            return false;
        }
        */

        public static bool Is_json_there(string path)
        {
            string[] files = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
            if(files.Length >0)
            {
                return true;
            }
            return false;
        }

        public static bool Is_folder_there(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            if (directories.Length > 0)
            {
                return true;
            }
            return false;
        }

        public static string Lenghten_path(string main_path, string short_path)
        {
            return main_path + short_path;
        }

        public static string Shorten_path(string main_path, string full_path)
        {
            return full_path.Remove(0, main_path.Length);
        }

        public static string[] Shorten_path(string main_path, string[] full_paths)
        {
            return Array.ConvertAll<string, string>(full_paths, s => s.Remove(0, main_path.Length));
        }
    }
}

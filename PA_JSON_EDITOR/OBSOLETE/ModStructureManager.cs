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
using static PA_JSON_EDITOR.Paths;

namespace PA_JSON_EDITOR
{
    public class UnitFolder
    {
        public static DataContainer unit_main_json;
        public static string unit_main_json_path;
        public static string unit_folder_path;

        public UnitFolder(string path)
        {
            unit_folder_path = path;
            foreach (string file in Directory.GetFiles(unit_folder_path))
            {
               /* if (file.Remove(0, unit_folder_path.Length) == Directory.GetParent(unit_folder_path).Name)
                {
                    unit_main_json = new DataContainer(file);
                    unit_main_json_path = file;
                }*/
            }
        }
    }
    public class ModStructureManager
    {
        private string input_path;

        public static List<UnitFolder> unit_list;
        public static List<string> ammo_list;
        public static List<string> tool_list;

        /// <summary>
        /// Dissasembles the mod
        /// </summary>
        /// <param name="path"></param>
        public ModStructureManager(string path)
        {
            input_path = path;

            ammo_list.AddRange(Directory.GetDirectories(input_path + Paths.Mod.AmmoFolder));
            tool_list.AddRange(Directory.GetDirectories(input_path + Paths.Mod.ToolsFolder));

            foreach(string directory in Directory.GetDirectories(input_path + Paths.Mod.UnitsFolder))
            {
                unit_list.Add(new UnitFolder(directory));
            }
            
        }



        /*
        private string ShortenThePath(string full_path, string parent_path)
        {
            return full_path.Remove(0, parent_path.Length+1);
        }*/
        /*
        private List<string> ShortenThePath(string[] full_paths, string parent_path)
        {
            List<string> output = new List<string>();
            foreach(string path in full_paths)
            {
                output.Add(ShortenThePath(path, parent_path));
            }
            return output;
        }*/
    }
}

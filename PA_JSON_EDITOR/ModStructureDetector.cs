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
    public struct Ammo
    {
        public string name;
        public string path;
        public Container container;
    }
    public struct Tool
    {
        public string name;
        public string path;
        public Container container;
    }
    public struct Unit
    {
        public string name;
        public string path;
        public Container container;
    }

    public class ModStructureDetector
    {
        private string input_path;

        public bool ammo_exists = false;
        public bool tools_exists = false;
        public bool unit_exists = false;

        public string ammo_dir;
        public string tools_dir;
        public string units_dir;

        public Dictionary<string, string> unit_classes;

        public List<Unit> all_units;
        public List<Tool> all_tools;
        public List<Ammo> all_ammos;

        /// <summary>
        /// Dissasembles the mod
        /// </summary>
        /// <param name="path"></param>
        public ModStructureDetector(string path)
        {
            this.input_path = path.Remove(path.Length-1);
            string[] directories = Directory.GetDirectories(path);
            foreach(string directory in directories)
            {
                string dir_name = ShortenThePath(directory, input_path);
                if (dir_name=="ammo")
                {
                    ammo_dir = directory;
                    ammo_exists = true;

                }

                if (dir_name == "tools")
                {
                    tools_dir = directory;
                    tools_exists = true;
                }

                if (dir_name == "units")
                {
                    units_dir = directory;
                    unit_exists = true;
                }
                
            }

            if(unit_exists)
            {
                foreach (string unit_path in Directory.GetDirectories(units_dir))
                    unit_classes.Add(ShortenThePath(unit_path, units_dir), unit_path);

                foreach(string class_path in unit_classes.Values)
                {
                    Directory.GetDirectories(class_path);
                }
                
            }
        }

        private string ShortenThePath(string full_path, string parent_path)
        {
            return full_path.Remove(0, parent_path.Length+1);
        }

        private List<string> ShortenThePath(string[] full_paths, string parent_path)
        {
            List<string> output = new List<string>();
            foreach(string path in full_paths)
            {
                output.Add(ShortenThePath(path, parent_path));
            }
            return output;
        }
    }
}

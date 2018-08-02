using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA_JSON_EDITOR
{
    public static class Paths
    {
        public static class Mod
        {
            public const string PaFolder = @"\pa";
            public const string UnitsFolder = @"\pa\units";
            public const string AmmoFolder = @"\pa\ammo";
            public const string ToolsFolder = @"\pa\tools";

            public const string unit_list = @"\pa\units\unit_list.json";

            public const string mod_info = @"\mod_info.json";
        }

        public static List<string> unit_classes = new List<string>(
            new string[]
            {
                "air",
                "land",
                "sea",
                "orbital",
                "commanders",
            });

        public static List<string> unit_classes_paths = new List<string>(
            new string[]
            {
                @"\pa\units\air",
                @"\pa\units\land",
                @"\pa\units\sea",
                @"\pa\units\orbital",
                @"\pa\units\commanders",
            }
            );

    }
}

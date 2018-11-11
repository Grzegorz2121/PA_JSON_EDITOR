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

namespace PA_JSON_EDITOR.PapaEditor
{
    /// <summary>
    /// Class represents a C struct, allows for easy data manipulation
    /// </summary>
    public class PapaFile
    {
        private static StreamReader stream_reader;
        private List<string> papa_contents = new List<string>();

        UInt32 papa_magic;
        UInt32 version;

        UInt16 num_strings;
        UInt16 num_textures;
        UInt16 num_vertex_buffers;
        UInt16 num_index_buffers;

        UInt16 num_materials;
        UInt16 num_meshes;
        UInt16 num_skeletons;
        UInt16 num_models;

        UInt16 num_animations;
        UInt16[] padding = new UInt16[3];

        UInt64 string_table_offset;
        UInt64 texture_table_offset;
        UInt64 vertex_buffer_table_offset;
        UInt64 index_buffer_table_offset;
        UInt64 material_table_offset;
        UInt64 mesh_table_offset;
        UInt64 skeleton_table_offset;
        UInt64 model_table_offset;
        UInt64 animation_table_offset;

        /// <summary>
        /// Reads raw byte data from a file and converts it to a object form
        /// </summary>
        /// <param name="papa_path">path where .papa file is situated</param>
        public PapaFile(string papa_path)
        {
            stream_reader = new StreamReader(papa_path);
            for(int I=0;I<10;I++)
            {
                char[] char_buffer = new char[4];
                stream_reader.ReadBlock(char_buffer, 0, 4);
                Console.WriteLine(char_buffer);
            }
          
        }
    }
}

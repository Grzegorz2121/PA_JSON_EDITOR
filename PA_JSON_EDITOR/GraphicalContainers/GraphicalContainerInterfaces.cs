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
        IGraphicalContainer CreateNewGraphicalContainer(IDataContainer dataContainer, Form parentForm, Point inLoc, Size inSize);
        void Show();
        void Hide();
        void Dispose();
    }
    
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PA_JSON_EDITOR;

namespace PA_JSON_EDITOR
{

    public static class GraphicalContainerSizes
    {

        public static class UniversalSize
        {
            public static Point Marigin = new Point(10, 10);
            public static Point Size = new Point(100, 100);
        }

        public static class ArraySize
        {
            public static Point BoxSeize = UniversalSize.Size;
            public static Point BoxMarigins = UniversalSize.Marigin;

            public static Point ListBoxSeize = new Point(BoxSeize.X - 2*BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y );
            public static Point ListBoxLoc = new Point(BoxMarigins.X, BoxMarigins.Y);

            public static Point AddButtonSize = new Point((BoxSeize.X / 2) - BoxMarigins.X * 2, 20);
            public static Point AddButtonLoc = new Point(BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);  
            
            public static Point DeleteButtonSize = new Point((BoxSeize.X / 2) - BoxMarigins.X * 2, 20);
            public static Point DeleteButtonLoc = new Point(BoxSeize.X - BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);
        }
        public static class ComplexSize
        {
            public static Point BoxSeize = UniversalSize.Size;
            public static Point BoxMarigins = UniversalSize.Marigin;

            public static Point ListBoxSeize = new Point(BoxSeize.X - 2 * BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);
            public static Point ListBoxLoc = new Point(BoxMarigins.X, BoxMarigins.Y);

            public static Point AddButtonSize = new Point((BoxSeize.X / 2) - BoxMarigins.X * 2, 20);
            public static Point AddButtonLoc = new Point(BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);
           
            public static Point DeleteButtonSize = new Point((BoxSeize.X / 2) - BoxMarigins.X * 2, 20);
            public static Point DeleteButtonLoc = new Point(BoxSeize.X - BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);
        }
        public static class PrimitiveSize
        {
            public static Point BoxSeize = UniversalSize.Size;
            public static Point BoxMarigins = UniversalSize.Marigin;

            public static Point TextBoxSeize = new Point(BoxSeize.X - 2 * BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);
            public static Point TextBoxLoc = new Point(BoxMarigins.X, BoxMarigins.Y);

            public static Point SaveButtonSize = new Point((BoxSeize.X / 2) - BoxMarigins.X * 2, 20);
            public static Point SaveButtonLoc = new Point(BoxMarigins.X, BoxSeize.Y - BoxMarigins.Y);
        }
    }
}

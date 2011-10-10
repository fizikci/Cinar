using System.Drawing;

namespace Cinar.Drawing
{
    public class Rectangle : Element
    {
        public Rectangle()
        {
            Border.All.Color = KnownColor.Black;
            Border.All.Style = BorderStyles.Solid;
            Border.All.Width = 2;
        }
    }
}

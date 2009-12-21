using System.Drawing;
using System.ComponentModel;

namespace Cinar.TemplateDesign
{
    public class Line : Element
    {
        public Line()
        {
            this.LineType = new Border() { 
                Color = KnownColor.Black,
                Style = BorderStyles.Solid,
                Width = 2
            };
        }

        [Browsable(false)]
        public bool Up { get; set; }

        [Category("Line")]
        public Border LineType { get; set; }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            if (!Up)
                g.DrawLine(
                    this.LineType.GetPen(),
                    this.Location.X,
                    this.Location.Y,
                    this.Location.X + this.Width,
                    this.Location.Y + this.Height);
            else
                g.DrawLine(
                    this.LineType.GetPen(),
                    this.Location.X,
                    this.Location.Y + this.Height,
                    this.Location.X + this.Width,
                    this.Location.Y);

        }
    }
}

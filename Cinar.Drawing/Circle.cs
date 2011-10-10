using System.Drawing;
using System.ComponentModel;

namespace Cinar.Drawing
{
    public class Circle : Element
    {
        public Circle()
        {
            this.LineType = new Border()
            {
                Color = KnownColor.Black,
                Style = BorderStyles.Solid,
                Width = 2
            };
        }

        [Category("Circle")]
        public Border LineType { get; set; }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            g.DrawEllipse(
                LineType.GetPen(),
                this.Location.X,
                this.Location.Y,
                this.Width,
                this.Height);
        }

        public override void PaintBackground(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.FromKnownColor(this.Background.Color)),
                this.Location.X,
                this.Location.Y,
                this.Width,
                this.Height);
        }
    }
}

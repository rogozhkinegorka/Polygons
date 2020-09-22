using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Polygons
{
    public class Square : Shape
    {
        public Square(int x, int y) : base(x, y)
        { }
        public override void Draw(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Shape.Color), x - R, y - R, (int)(2 * R / Math.Sqrt(2)), (int)(2 * R / Math.Sqrt(2)));
        }
        public override bool IsInside(Point pointClick)
        {
            double a = R / Math.Sqrt(2);
            return (pointClick.X <= x + a && pointClick.X >= x - a && pointClick.Y >= y - a && pointClick.Y <= y + a);
        }
    }
}

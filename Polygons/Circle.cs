using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Polygons
{
    public class Circle : Shape
    {
        public Circle(int x, int y) : base(x, y)
        { }
        public override void Draw(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Shape.Color), x, y, R, R);
        }
        public override bool IsInside(Point pointClick)
        {
            return R >= Math.Sqrt(Math.Pow(x - pointClick.X, 2) + Math.Pow(y - pointClick.Y, 2));
        }
    }
}

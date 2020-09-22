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
            graphics.FillEllipse(new SolidBrush(Shape.Color), x-R, y-R, 2*R, 2*R);
        }
        public override bool IsInside(int x1, int y1)
        {
            return R >= Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y - y1, 2));
        }
    }
}

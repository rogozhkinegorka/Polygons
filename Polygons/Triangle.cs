using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Polygons
{
    public class Triangle : Shape
    {
        public Triangle(int x, int y) : base(x, y)
        { }
        public override void Draw(Graphics graphics)
        {
            Point point1 = new Point(x, y - R);
            Point point2 = new Point(x - (int)(R * Math.Sqrt(3) / 2), y + R / 2);
            Point point3 = new Point(x + (int)(R * Math.Sqrt(3) / 2), y + R / 2);
            Point[] points = { point1, point2, point3 };

            graphics.FillPolygon(new SolidBrush(Shape.Color), points);
        }
        public override bool IsInside(Point pointClick)
        {
            int x1 = x - (int)(R * Math.Sqrt(3) / 2);
            int y1 = y + R / 2;
            int x2 = x;
            int y2 = y - R;
            int x3 = x + (int)(R * Math.Sqrt(3) / 2);
            int y3 = y + R / 2;


            if (pointClick.Y >= y + R / 2) return false;
            else
            {
                return pointClick.Y <= y + R / 2 && pointClick.Y >= ((pointClick.X - x1) * (y2 - y1) / (x2 - x1) + y1) && pointClick.Y >= ((pointClick.X - x3) * (y2 - y3) / (x2 - x3) + y3);
            }
        }
    }
}

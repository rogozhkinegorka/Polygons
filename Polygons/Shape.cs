using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Polygons
{
    abstract public class Shape
    {
        protected static int R;
        protected static Color Color;
        protected int x, y;
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        static Shape()
        {
            R = 10;
            Color = Color.Blue;
        }
        public abstract void Draw(Graphics graphics);
        public abstract bool IsInside(Point pointClick);
    }
}

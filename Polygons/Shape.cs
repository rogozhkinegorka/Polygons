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
        protected int x, y, dx, dy;
        protected bool isMoving;
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            dx = 0;
            dy = 0;
            isMoving = false;
        }
        static Shape()
        {
            R = 50;
            Color = Color.Blue;
        }
        public abstract void Draw(Graphics graphics);
        public abstract bool IsInside(int x1, int y1);
    }
}

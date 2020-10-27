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
        protected bool isMoving, isNotInside;
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            dx = 0;
            dy = 0;
            isMoving = false;
            isNotInside = true;
        }
        static Shape()
        {
            R = 20;
            Color = Color.Blue;
        }
        public bool IsNotInside { get { return isNotInside; } set { isNotInside = value; } }
        public bool IsMoving { get { return isMoving; }  set { isMoving = value; } }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Dx { get { return dx; } set { dx = value; } }
        public int Dy { get { return dy; } set { dy = value; } }



        public abstract void Draw(Graphics graphics);
        public abstract bool IsInside(int x1, int y1);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Polygons
{
    [Serializable] abstract public class Shape
    {
        protected static int R;
        protected static Color color;
        [NonSerialized] protected int x, y, dx, dy;
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
            color = Color.Black;
        }
        public static int Radius
        {
            set { R = value; }
            get { return R; }
        }
        public static Color Colour
        {
            set { color = value; }
            get { return color; }
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polygons
{
    public partial class Form1 : Form
    {
        bool isClick = true;
        List<Shape> Points = new List<Shape>();
        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            Shape[] tops;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isClick)
            {
                if ((e.Button & MouseButtons.Right) == 0)
                    Points.Add(new Triangle(e.X, e.Y));
                else
                {
                    for (int i = Points.Count - 1; i >= 0; i--)
                    {
                        if (Points[i].IsInside(e.X, e.Y))
                        {
                            Points.RemoveAt(i);
                            break;
                        }
                    }
                }
                this.Invalidate();
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var i in Points)
            {
                i.Draw(e.Graphics);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].IsInside(e.X, e.Y))
                {
                    if(e.Button == MouseButtons.Left) isClick = false;
                    Points[i].IsMoving = true;
                    Points[i].Dx = Points[i].X - e.X;
                    Points[i].Dy = Points[i].Y - e.Y;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].IsMoving)
                {
                    Points[i].X = e.X + Points[i].Dx;
                    Points[i].Y = e.Y + Points[i].Dy;
                }
            }
            this.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isClick = true;
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].IsMoving = false;
                Points[i].Dx = 0;
                Points[i].Dy = 0;
            }
        }
    }
}

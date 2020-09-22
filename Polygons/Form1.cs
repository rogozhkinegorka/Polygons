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
        List<Shape> Points = new List<Shape>();
        public Form1()
        {
            InitializeComponent();
            Shape[] tops;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if((e.Button & MouseButtons.Right) == 0)
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var i in Points)
            {
                i.Draw(e.Graphics);
            }
        }
    }
}

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
        enum PShape { C, S, T }
        enum Algoritm { byDefinition, Jarvis }
        PShape pointShape = PShape.C;
        bool isNotInside;
        Algoritm convexHull = Algoritm.Jarvis;
        List<Shape> Points = new List<Shape>();
        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void ByDefinition(List<Shape> Points, Graphics graphics)
        {
            //Алгоритм по определению
            double k;
            double b;
            int upPoints;
            foreach (var i in Points)
            {
                i.IsNotInside = false;
            }
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = i + 1; j < Points.Count; j++)
                {
                    upPoints = 0;
                    if (Points[i].X == Points[j].X)
                    {
                        for (int z = 0; z < Points.Count; z++)
                        {
                            if (z != i && z != j)
                            {
                                if (Points[z].X > Points[i].X) upPoints++;
                            }
                        }
                    }
                    else
                    {

                        k = ((double)Points[i].Y - Points[j].Y) / (Points[i].X - Points[j].X);
                        b = Points[i].Y - k * Points[i].X;
                        for (int z = 0; z < Points.Count; z++)
                        {
                            if (z != i && z != j)
                            {
                                if (Points[z].Y > k * Points[z].X + b) upPoints++;
                            }
                        }
                    }
                    if (upPoints == 0 || upPoints == Points.Count - 2)
                    {
                        graphics.DrawLine(new Pen(Color.Red), Points[i].X, Points[i].Y, Points[j].X, Points[j].Y);
                        Points[i].IsNotInside = true;
                        Points[j].IsNotInside = true;
                    }
                }
            }
        } 
        void Jarvis (List<Shape> Points, Graphics graphics, bool draw)
        {
            //Алгоритм Джарвиса
            foreach (var i in Points)
            {
                i.IsNotInside = false;
            }
            Shape A = Points[0];
            foreach (var i in Points)
            {
                if (i.Y > A.Y)
                    A = i;
                if (i.Y == A.Y)
                    if (i.X < A.X)
                        A = i;
            }
            //e.Graphics.FillEllipse(new SolidBrush(Color.Red), A.X - 20, A.Y - 20, 2 * 25, 2 * 25);
            Shape F = A;
            Shape M = new Circle(A.X - 10, A.Y);
            double minCos = 1;
            Shape P;
            if (Points[0] == A)
                P = Points[1];
            else
                P = Points[0];
            double cos;
            foreach (var i in Points)
            {
                if (i != A && i != M)
                {
                    double aX = M.X - A.X;
                    double bX = i.X - A.X;
                    double aY = M.Y - A.Y;
                    double bY = i.Y - A.Y;
                    cos = (aX * bX + aY * bY) / (Math.Sqrt(aX * aX + aY * aY) * Math.Sqrt(bX * bX + bY * bY));
                    if (cos < minCos)
                    {
                        minCos = cos;
                        P = i;
                    }
                }
            }
            if(draw) graphics.DrawLine(new Pen(Color.Red), A.X, A.Y, P.X, P.Y);
            M = A;
            A = P;
            while (P != F)
            {
                minCos = 1;
                if (Points[0] == A)
                    P = Points[1];
                else
                    P = Points[0];
                foreach (var i in Points)
                {
                    if (i != A && i != M)
                    {
                        double aX = M.X - A.X;
                        double bX = i.X - A.X;
                        double aY = M.Y - A.Y;
                        double bY = i.Y - A.Y;
                        cos = (aX * bX + aY * bY) / (Math.Sqrt(aX * aX + aY * aY) * Math.Sqrt(bX * bX + bY * bY));
                        if (cos < minCos)
                        {
                            minCos = cos;
                            P = i;
                        }
                    }
                }
                if(draw) graphics.DrawLine(new Pen(Color.Red), A.X, A.Y, P.X, P.Y);
                A.IsNotInside = true;
                P.IsNotInside = true;
                M = A;
                A = P;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (Points.Count >= 3)
            {
                if (convexHull == Algoritm.byDefinition)
                {
                    ByDefinition(Points, e.Graphics);
                }
                else
                {
                    Jarvis(Points, e.Graphics, true);
                }
            }
            foreach (var i in Points)
            {
                i.Draw(e.Graphics);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isNotInside = true;
            for (int j = Points.Count - 1; j >= 0; j--)
            {
                if (Points[j].IsInside(e.X, e.Y))
                {
                    isNotInside = false;
                    if ((e.Button & MouseButtons.Right) == 0)
                    {
                        Points[j].IsMoving = true;
                        Points[j].Dx = Points[j].X - e.X;
                        Points[j].Dy = Points[j].Y - e.Y;
                    }
                    else
                    {
                        Points.RemoveAt(j);
                        break;
                    }
                }
            }
            if ((e.Button & MouseButtons.Right) == 0 && isNotInside)
            {
                bool move = false;
                if (Points.Count >= 3)
                {
                    List<Shape> Points2 = new List<Shape>(Points);
                    Points2.Add(new Circle(e.X, e.Y));

                    //Алгоритм Джарвиса
                    foreach (var i in Points2)
                    {
                        i.IsNotInside = false;
                    }
                    Shape A = Points2[0];
                    foreach (var i in Points2)
                    {
                        if (i.Y > A.Y)
                            A = i;
                        if (i.Y == A.Y)
                            if (i.X < A.X)
                                A = i;
                    }
                    Shape F = A;
                    Shape M = new Circle(A.X - 10, A.Y);
                    double minCos = 1;
                    Shape P;
                    if (Points2[0] == A)
                        P = Points2[1];
                    else
                        P = Points2[0];
                    double cos;
                    foreach (var i in Points2)
                    {
                        if (i != A && i != M)
                        {
                            double aX = M.X - A.X;
                            double bX = i.X - A.X;
                            double aY = M.Y - A.Y;
                            double bY = i.Y - A.Y;
                            cos = (aX * bX + aY * bY) / (Math.Sqrt(aX * aX + aY * aY) * Math.Sqrt(bX * bX + bY * bY));
                            if (cos < minCos)
                            {
                                minCos = cos;
                                P = i;
                            }
                        }
                    }
                    M = A;
                    A = P;
                    while (P != F)
                    {
                        minCos = 1;
                        if (Points2[0] == A)
                            P = Points2[1];
                        else
                            P = Points2[0];
                        foreach (var i in Points2)
                        {
                            if (i != A && i != M)
                            {
                                double aX = M.X - A.X;
                                double bX = i.X - A.X;
                                double aY = M.Y - A.Y;
                                double bY = i.Y - A.Y;
                                cos = (aX * bX + aY * bY) / (Math.Sqrt(aX * aX + aY * aY) * Math.Sqrt(bX * bX + bY * bY));
                                if (cos < minCos)
                                {
                                    minCos = cos;
                                    P = i;
                                }
                            }
                        }
                        A.IsNotInside = true;
                        P.IsNotInside = true;
                        M = A;
                        A = P;
                    }
                    for (int i = 0; i < Points2.Count; ++i)
                    {
                        if (!Points2[i].IsNotInside)
                        {
                            Points2.RemoveAt(i);
                            --i;
                        }
                    }
                    move = Points.SequenceEqual(Points2);
                    if (move)
                    {
                        for (int j = 0; j < Points.Count(); ++j)
                        {
                            Points[j].IsMoving = true;
                            Points[j].Dx = Points[j].X - e.X;
                            Points[j].Dy = Points[j].Y - e.Y;
                        }
                    }
                }
                if(!move)
                {
                    if (pointShape == PShape.C) Points.Add(new Circle(e.X, e.Y));
                    if (pointShape == PShape.S) Points.Add(new Square(e.X, e.Y));
                    if (pointShape == PShape.T) Points.Add(new Triangle(e.X, e.Y));
                }
            }
            this.Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var i in Points)
            {
                if (i.IsMoving)
                {
                    i.X = e.X + i.Dx;
                    i.Y = e.Y + i.Dy;
                }
            }
            this.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (var i in Points)
            {
                i.IsMoving = false;
                i.Dx = 0;
                i.Dy = 0;
            }
            for (int i = 0; i < Points.Count; ++i)
            {
                if (!Points[i].IsNotInside)
                {
                    Points.RemoveAt(i);
                    --i;
                }
            }
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pointShape = PShape.C;
            circleToolStripMenuItem.Checked = true;
            squareToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = false;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pointShape = PShape.S;
            circleToolStripMenuItem.Checked = false;
            squareToolStripMenuItem.Checked = true;
            triangleToolStripMenuItem.Checked = false;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pointShape = PShape.T;
            circleToolStripMenuItem.Checked = false;
            squareToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = true;
        }

        private void comparison_Click(object sender, EventArgs e)
        {
            if (Points.Count >= 3)
            {
                //Алгоритм по определению
                double k;
                double b;
                int upPoints;
                foreach (var i in Points)
                {
                    i.IsNotInside = false;
                }
                for (int i = 0; i < Points.Count; i++)
                {
                    for (int j = i + 1; j < Points.Count; j++)
                    {
                        upPoints = 0;
                        if (Points[i].X == Points[j].X)
                        {
                            for (int z = 0; z < Points.Count; z++)
                            {
                                if (z != i && z != j)
                                {
                                    if (Points[z].X > Points[i].X) upPoints++;
                                }
                            }
                        }
                        else
                        {

                            k = ((double)Points[i].Y - Points[j].Y) / (Points[i].X - Points[j].X);
                            b = Points[i].Y - k * Points[i].X;
                            for (int z = 0; z < Points.Count; z++)
                            {
                                if (z != i && z != j)
                                {
                                    if (Points[z].Y > k * Points[z].X + b) upPoints++;
                                }
                            }
                        }
                        if (upPoints == 0 || upPoints == Points.Count - 2)
                        {
                            Points[i].IsNotInside = true;
                            Points[j].IsNotInside = true;
                        }
                    }
                }
                
              
                //Алгоритм Джарвиса
                foreach (var i in Points)
                {
                    i.IsNotInside = false;
                }
                Shape A = Points[0];
                foreach (var i in Points)
                {
                    if (i.Y > A.Y)
                        A = i;
                    if (i.Y == A.Y)
                        if (i.X < A.X)
                            A = i;
                }
                Shape F = A;
                Shape M = new Circle(A.X - 10, A.Y);
                double minCos = 1;
                Shape P;
                if (Points[0] == A)
                    P = Points[1];
                else
                    P = Points[0];
                double cos;
                foreach (var i in Points)
                {
                    if (i != A && i != M)
                    {
                        double aX = M.X - A.X;
                        double bX = i.X - A.X;
                        double aY = M.Y - A.Y;
                        double bY = i.Y - A.Y;
                        cos = (aX * bX + aY * bY) / (Math.Sqrt(aX * aX + aY * aY) * Math.Sqrt(bX * bX + bY * bY));
                        if (cos < minCos)
                        {
                            minCos = cos;
                            P = i;
                        }
                    }
                }
                M = A;
                A = P;
                while (P != F)
                {
                    minCos = 1;
                    if (Points[0] == A)
                        P = Points[1];
                    else
                        P = Points[0];
                    foreach (var i in Points)
                    {
                        if (i != A && i != M)
                        {
                            double aX = M.X - A.X;
                            double bX = i.X - A.X;
                            double aY = M.Y - A.Y;
                            double bY = i.Y - A.Y;
                            cos = (aX * bX + aY * bY) / (Math.Sqrt(aX * aX + aY * aY) * Math.Sqrt(bX * bX + bY * bY));
                            if (cos < minCos)
                            {
                                minCos = cos;
                                P = i;
                            }
                        }
                    }
                    A.IsNotInside = true;
                    P.IsNotInside = true;
                    M = A;
                    A = P;
                }
            }
        }
    }
}

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
    public partial class RadiusChangeForm : Form
    {
        Del del1;
        public RadiusChangeForm(Del del2)
        {
            del1 = del2;
            InitializeComponent();
            trackBar1.Value = Shape.Radius;
        }

        public void OpenRadiusChange()
        {
            trackBar1.Value = Shape.Radius;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            del1(trackBar1.Value);
        }

        private void RadiusChangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.RadiusForm = null;
        }
    }
}

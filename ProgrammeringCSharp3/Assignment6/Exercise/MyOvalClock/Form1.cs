using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyOvalClock
{
    public partial class Form1 : Form
    {
        private string bg = Path.Combine(Application.StartupPath, "9F11BD6A-8BFC-4E04-A706-E4E485B07E13.jpg");
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            MyOvalClock clockObj = new MyOvalClock();

            if(!File.Exists(bg))
            {
                timer1.Stop();
                MessageBox.Show("Please copy an background to the bin debug folder");
            }
            if(!clockObj.DrawGraphicPathClipDrawingArea(e.Graphics, ClientRectangle, bg))
            {
                Application.Exit();
            }

            clockObj.DrawSystemTime(e.Graphics, ClientRectangle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            Invalidate(rect);
        }
    }
}

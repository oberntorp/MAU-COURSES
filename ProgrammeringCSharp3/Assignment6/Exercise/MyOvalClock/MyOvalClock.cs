using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOvalClock
{
    public class MyOvalClock
    {
        public bool DrawGraphicPathClipDrawingArea(Graphics graphics, Rectangle clientAreaOfForm, string pictureToUseBg)
        {
            int rectangleX = clientAreaOfForm.Width;
            int rectangleY = clientAreaOfForm.Height;

            GraphicsPath graphicsPath = new GraphicsPath();

            Rectangle boundaryOfForm = new Rectangle(0, 0, rectangleX, rectangleY);
            graphicsPath.AddPie(boundaryOfForm, 40, 370);
            graphics.SetClip(graphicsPath);

            Pen pen = new Pen(Color.Blue, 10);

            try
            {
                graphics.DrawImage(Image.FromFile(pictureToUseBg), boundaryOfForm);
            }
            catch
            {
                return false;
            }

            graphics.DrawPath(pen, graphicsPath);

            return true;
        }

        public void DrawSystemTime(Graphics graphics, Rectangle clientAreaOfForm)
        {
            int fontSize = 70;

            Font fontUsed = new Font("Times New Roman", fontSize, FontStyle.Regular);

            SolidBrush brush = new SolidBrush(Color.Green);

            string stringToDraw = DateTime.Now.ToLongTimeString();

            SizeF stringSizeBeingDrawn = graphics.MeasureString(stringToDraw, fontUsed);

            float xAxisPos = clientAreaOfForm.Width / 2.0F - stringSizeBeingDrawn.Width / 2.0F;
            float yAxisPos = clientAreaOfForm.Height / 3.5F - fontSize;


            graphics.DrawString(stringToDraw, fontUsed, brush, xAxisPos, yAxisPos);

            fontUsed.Dispose();
            brush.Dispose();
        }
    }
}

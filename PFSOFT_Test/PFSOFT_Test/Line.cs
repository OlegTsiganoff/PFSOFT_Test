using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace PFSOFT_Test
{
    [Serializable]
    class Line : Shape
    {
        Point startPoint;
        Point endPoint;

        public Line(Point startP, Point endP)
        {
            startPoint = startP;
            endPoint = endP;
            Color = Color.Black;
            Thickness = 2;
        }


        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(pen, startPoint, endPoint);
            pen.Dispose();
        }

        public void ChangeEndPoint(Point endP)
        {
            endPoint = endP;
        }
    }
}

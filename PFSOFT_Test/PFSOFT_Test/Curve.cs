using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PFSOFT_Test
{
    [Serializable]
    public class Curve : Shape
    {

        List<Point> points;

        public Curve()
        {
            points = new List<Point>();
            Color = Color.Black;
            Thickness = 2;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, Thickness);
            pen.StartCap = pen.EndCap = LineCap.Round;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if(points.Count > 1)
                g.DrawCurve(pen, points.ToArray());
            pen.Dispose();
        }

        public void AddPoint(Point point)
        {
            points.Add(point);
        }
    }
}

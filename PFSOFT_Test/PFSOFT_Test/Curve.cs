using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using PaintInterface;

namespace PFSOFT_Test
{
    [Serializable]
    public class Curve : IShape
    {        
        List<Point> points;

        public bool IsSelected { get; set; }
        public DrawSettings DrawSettings { get; set; }

        public Point[] KeyPoints
        {
            get { return points.ToArray(); }
            set { points = new List<Point>(value); }
        }

        public Curve()
        {
            points = new List<Point>();            
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            pen.StartCap = pen.EndCap = LineCap.Round;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if(points.Count > 1)
                g.DrawCurve(pen, points.ToArray());
            pen.Dispose();
        }

        public void DrawSelection(Graphics g)
        {
            PaintHelper.DrawSelection(g, KeyPoints);
        }

        public void AddPoint(Point point)
        {
            points.Add(point);
        }

        /// <summary>
        /// Проверяет попадание точки в фигуру
        /// </summary>
        /// <param name="p"></param>
        /// <returns>-1 - нет попадания, 0 - есть попадание, 1 и более - номер опорной точки в которую попал курсор</returns>
        public int ContainsPoint(Point p)
        {
            if (this.IsSelected)
            {
                for (int i = 1; i <= KeyPoints.Length; i++)
                {
                    if (PaintHelper.GetKeyPointWhiteRect(KeyPoints[i - 1]).Contains(p))
                        return i;
                }
            }

            var path = new GraphicsPath();
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            path.AddCurve(points.ToArray());
            path.Widen(pen);
            Region region = new Region(path);
            pen.Dispose();
            if (region.IsVisible(p))
                return 0;
            return -1;
        }


        public void Move(int deltaX, int deltaY)
        {
            Point point;
            for (int i = 0; i < points.Count; i++ )
            {
                point = new Point(points[i].X + deltaX, points[i].Y + deltaY);
                points[i] = point;
            }
        }

        public void MoveKeyPoint(int number, Point destPoint)
        {
            if (number > points.Count || number < 0)
                return;
            points[number - 1] = destPoint;            
        }

        
    }
}

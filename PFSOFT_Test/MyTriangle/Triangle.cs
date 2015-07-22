using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using PaintInterface;

namespace MyTriangle
{

    public class Triangle: IShape
    {
        Point startPoint;
        Point endPoint;

        Point[] keyPoints;

        public Point StartPoint
        {
            get { return startPoint; }
            set 
            { 
                startPoint = value;
                UpdateKeyPoints();
            }
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set 
            { 
                endPoint = value;
                UpdateKeyPoints();
            }
        }

        public bool IsSelected { get; set; }

        public Point[] KeyPoints
        {
            get { return keyPoints; }
            set { keyPoints = value; }
        }

        public DrawSettings DrawSettings { get; set; }

        public Triangle(Point startP, Point endP)
        {
            startPoint = startP;
            endPoint = endP;
            this.DrawSettings = new DrawSettings(1, Color.Black, Color.Transparent);
            keyPoints = PaintHelper.PointsFromRect(PaintHelper.NormalizeRect(startP, endP));
        }

        public void Draw(System.Drawing.Graphics g)
        {
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = PaintHelper.NormalizeRect(startPoint, endPoint);
            Point p1 = new Point(rect.X, rect.Y + rect.Height);
            Point p2 = new Point(rect.X + rect.Width / 2, rect.Y);
            Point p3 = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            g.DrawLine(pen, p1, p2);
            g.DrawLine(pen, p2, p3);
            g.DrawLine(pen, p3, p1);
            if (DrawSettings.BackColor != System.Drawing.Color.Transparent)
            {
                SolidBrush brush = new SolidBrush(DrawSettings.BackColor);
                g.FillPolygon(brush, new Point[]{p1,p2,p3}, FillMode.Alternate);
                brush.Dispose();
            }
            pen.Dispose();
        }

        public void DrawSelection(Graphics g)
        {
            PaintHelper.DrawSelection(g, KeyPoints);
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
            Rectangle rect = PaintHelper.NormalizeRect(startPoint, endPoint);
            Point p1 = new Point(rect.X, rect.Y + rect.Height);
            Point p2 = new Point(rect.X + rect.Width / 2, rect.Y);
            Point p3 = new Point(rect.X + rect.Width, rect.Y + rect.Height);
            path.AddPolygon(new Point[] { p1, p2, p3 });
            path.Widen(pen);
            Region region = new Region(path);
            pen.Dispose();
            if (region.IsVisible(p))
                return 0;


            if (IsPointInTriangle(p, p1, p2, p3))
                return 0;
            return -1;
        }

        /// <summary>
        /// Определяет попадание точки в треугольник
        /// </summary>
        /// <param name="p">искомая точка</param>
        /// <param name="p0">точка треугольника</param>
        /// <param name="p1">точка треугольника</param>
        /// <param name="p2">точка треугольника</param>
        /// <returns></returns>
        public static bool IsPointInTriangle(Point p, Point p0, Point p1, Point p2)
        {
            var s = (p0.Y * p2.X - p0.X * p2.Y + (p2.Y - p0.Y) * p.X + (p0.X - p2.X) * p.Y);
            var t = (p0.X * p1.Y - p0.Y * p1.X + (p0.Y - p1.Y) * p.X + (p1.X - p0.X) * p.Y);

            if (s <= 0 || t <= 0)
                return false;

            var A = (-p1.Y * p2.X + p0.Y * (-p1.X + p2.X) + p0.X * (p1.Y - p2.Y) + p1.X * p2.Y);

            return (s + t) < A;
        }

        public void Move(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;
            UpdateKeyPoints();
        }

        public void MoveKeyPoint(int number, Point destPoint)
        {
            switch (number)
            {
                case 1:
                    startPoint = destPoint;
                    break;
                case 2:
                    startPoint.Y = destPoint.Y;
                    endPoint.X = destPoint.X;
                    break;
                case 3:
                    endPoint = destPoint;
                    break;
                case 4:
                    startPoint.X = destPoint.X;
                    endPoint.Y = destPoint.Y;
                    break;
                default:
                    break;
            }
            UpdateKeyPoints();
        }

        private void UpdateKeyPoints()
        {
            keyPoints = PaintHelper.PointsFromRect(PaintHelper.NormalizeRect(startPoint, endPoint));
        }


        
    }
}

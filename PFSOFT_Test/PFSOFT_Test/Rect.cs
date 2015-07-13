using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PFSOFT_Test
{
    [Serializable]
    public class Rect : Shape
    {
        Point startPoint;
        Point endpoint;
        Color backColor = Color.Transparent;


        public Point EndPoint
        {
            get { return endpoint; }
            set { endpoint = value; }
        }

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public Rect() { }
        public Rect(Point startP, Point endP)
        {
            startPoint = startP;
            endpoint = endP;
            Color = System.Drawing.Color.DarkBlue;
            Thickness = 1;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawRectangle(pen, NormalizeRect(startPoint, endpoint));
            if(backColor != System.Drawing.Color.Transparent)
            {
                SolidBrush brush = new SolidBrush(backColor);
                g.FillRectangle(brush, NormalizeRect(startPoint, endpoint));
                brush.Dispose();
            }
            pen.Dispose();
        }

       
 
        
        /// <summary>
        /// функции преобразования входящих координат двух точек в корректный прямоугольник
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Rectangle NormalizeRect(Point p1, Point p2)
        {
            return NormalizeRect(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static Rectangle NormalizeRect(Rectangle rect)
        {
            return NormalizeRect(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
        }
        
        static Rectangle NormalizeRect(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
    }
}

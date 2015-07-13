using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PFSOFT_Test
{
    [Serializable]
    class Circle : Shape
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

        public Circle(Point startP, Point endP)
        {
            startPoint = startP;
            endpoint = endP;
            Color = System.Drawing.Color.Red;
            Thickness = 1;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(pen, NormalRectToSquare(Rect.NormalizeRect(startPoint, endpoint)));
            if(backColor != System.Drawing.Color.Transparent)
            {
                SolidBrush brush = new SolidBrush(backColor);
                g.FillEllipse(brush, NormalRectToSquare(Rect.NormalizeRect(startPoint, endpoint)));
                brush.Dispose();
            }
            pen.Dispose();
        }

        /// <summary>
        /// преобразует прямоугольник в квадрат
        /// </summary>
        /// <param name="normalRect"></param>
        /// <returns></returns>
 
        Rectangle NormalRectToSquare(Rectangle normalRect)
        {
            Rectangle square = normalRect;
            if(normalRect.Height > normalRect.Width)
            {
                square.Height = normalRect.Width;
            }
            else
            {
                square.Width = normalRect.Height;
            }

            return square;
        }

    }
}

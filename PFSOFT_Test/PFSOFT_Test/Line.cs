using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using PaintInterface;

namespace PFSOFT_Test
{
    [Serializable]
    class Line : IShape
    {
        Point startPoint;
        Point endPoint;

        public bool IsSelected { get; set; }

        public Point[] KeyPoints
        {
            get { return new Point[2] { startPoint, endPoint }; }
            set
            {
                if(value != null)
                {
                    startPoint = value[0];
                    endPoint = value[1];
                }
            }
        }

        public DrawSettings DrawSettings { get; set; }

        public Line(Point startP, Point endP)
        {
            startPoint = startP;
            endPoint = endP;
            DrawSettings = new DrawSettings(2, Color.Black, Color.Transparent);            
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(pen, startPoint, endPoint);
            pen.Dispose();
        }

        /// <summary>
        ///  отрисовывает квадратики в опорных точках фигуры при выделении
        /// </summary>
        /// <param name="g"></param>
        public void DrawSelection(Graphics g)
        {
            PaintHelper.DrawSelection(g, KeyPoints);
        }

        public void ChangeEndPoint(Point endP)
        {
            endPoint = endP;
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
            path.AddLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            path.Widen(pen);
            Region region = new Region(path);
            pen.Dispose();
            if(region.IsVisible(p))
                return 0;

            return -1;
        }

        public void Move(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;
        }

        public void MoveKeyPoint(int number, Point destPoint)
        {
            switch(number)
            {
                case 1:
                    startPoint = destPoint;
                    break;
                case 2:
                    endPoint = destPoint;
                    break;
            }
        }
    }
}

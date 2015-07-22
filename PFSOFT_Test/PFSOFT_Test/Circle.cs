using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using PaintInterface;

namespace PFSOFT_Test
{
    [Serializable]
    public class Circle : Rect
    {        

        public Circle(Point startP, Point endP):base(startP, endP) 
        {            
            DrawSettings = new DrawSettings(1, Color.Black, Color.Transparent);
            KeyPoints = PaintHelper.PointsFromRect(NormalRectToSquare(PaintHelper.NormalizeRect(startP, endP)));
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(pen, NormalRectToSquare(PaintHelper.NormalizeRect(StartPoint, EndPoint)));
            if (DrawSettings.BackColor != System.Drawing.Color.Transparent)
            {
                SolidBrush brush = new SolidBrush(DrawSettings.BackColor);
                g.FillEllipse(brush, NormalRectToSquare(PaintHelper.NormalizeRect(StartPoint, EndPoint)));
                brush.Dispose();
            }
            pen.Dispose();
        }

        public override void Move(int deltaX, int deltaY)
        {
            Point pStart = new Point(StartPoint.X + deltaX, StartPoint.Y + deltaY);
            Point pEnd = new Point(EndPoint.X + deltaX, EndPoint.Y + deltaY);
            StartPoint = pStart;
            EndPoint = pEnd;
            UpdateKeyPoints();
        }

        protected override void UpdateKeyPoints()
        {
            KeyPoints = PaintHelper.PointsFromRect(NormalRectToSquare(PaintHelper.NormalizeRect(StartPoint, EndPoint)));
        }


        /// <summary>
        /// Проверяет попадание точки в фигуру
        /// </summary>
        /// <param name="p"></param>
        /// <returns>-1 - нет попадания, 0 - есть попадание, 1 и более - номер опорной точки в которую попал курсор</returns>
        public override int ContainsPoint(Point p)
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

            Rectangle rect = NormalRectToSquare(PaintHelper.NormalizeRect(StartPoint, EndPoint));
            path.AddEllipse(rect);
            path.Widen(pen);          
            
            Region region = new Region(path);            
            pen.Dispose();
            if(region.IsVisible(p))
                return 0;

            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            double radius = rect.Width / 2;
            float dx = p.X - center.X;
            float dy = p.Y - center.Y;
            if (Math.Sqrt(dx * dx + dy * dy) <= radius)
                return 0;
            return -1;
        }

        /// <summary>
        /// преобразует прямоугольник в квадрат
        /// </summary>
        /// <param name="normalRect"></param>
        /// <returns></returns>
        Rectangle NormalRectToSquare(Rectangle normalRect)
        {
            Rectangle square = normalRect;
            if (normalRect.Height > normalRect.Width)
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

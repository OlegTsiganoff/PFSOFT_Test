using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using PaintInterface;


namespace PFSOFT_Test
{
    [Serializable]
    public class Rect : IShape
    {
        Point startPoint;
        Point endPoint;
        Point[] keyPoints;


        public Point EndPoint
        {
            get { return endPoint; }
            set 
            { 
                endPoint = value;
                UpdateKeyPoints();
            }
        }

        public Point StartPoint
        {
            get { return startPoint; }
            set 
            { 
                startPoint = value;
                UpdateKeyPoints();
            }
        }
        public Point[] KeyPoints
        {
            get { return keyPoints; }
            set { keyPoints = value; }
        }


        public bool IsSelected { get; set; }

        public DrawSettings DrawSettings { get; set; }
        
        public Rect(Point startP, Point endP)
        {
            startPoint = startP;
            endPoint = endP;
            DrawSettings = new DrawSettings(1, Color.Black, Color.Transparent);
            keyPoints = PaintHelper.PointsFromRect(PaintHelper.NormalizeRect(startPoint, endPoint));            
        }


        public virtual void Draw(Graphics g)
        {
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawRectangle(pen, PaintHelper.NormalizeRect(startPoint, endPoint));
            if (DrawSettings.BackColor != System.Drawing.Color.Transparent)
            {
                SolidBrush brush = new SolidBrush(DrawSettings.BackColor);
                g.FillRectangle(brush, PaintHelper.NormalizeRect(startPoint, endPoint));
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
        public virtual int ContainsPoint(Point p)
        {
            if(this.IsSelected)
            {
                for (int i = 1; i <= KeyPoints.Length; i++)
                {
                    if (PaintHelper.GetKeyPointWhiteRect(KeyPoints[i - 1]).Contains(p))
                        return i;
                }
            }

            var path = new GraphicsPath();
            Pen pen = new Pen(DrawSettings.Color, DrawSettings.Thickness);
            Rectangle rect = PaintHelper.NormalizeRect(PaintHelper.NormalizeRect(startPoint, endPoint));
            path.AddRectangle(rect);
            path.Widen(pen);
            Region region = new Region(path);
            pen.Dispose();

            if (region.IsVisible(p))
                return 0;

            if (rect.Contains(p))
                return 0;

            return -1;
        }

        public virtual void Move(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;
            UpdateKeyPoints();
        }

        public virtual void MoveKeyPoint(int number, Point destPoint)
        {
            switch(number)
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

        protected virtual void UpdateKeyPoints()
        {
            keyPoints = PaintHelper.PointsFromRect(PaintHelper.NormalizeRect(startPoint, endPoint));
        }





        

        
    }
}

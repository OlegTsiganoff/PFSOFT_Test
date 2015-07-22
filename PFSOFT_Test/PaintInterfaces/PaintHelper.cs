using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintInterface
{
    public class PaintHelper
    {
        /// <summary>
        /// функция отрисовывает квадратики во всех опорных точках фигуры
        /// </summary>
        /// <param name="g"></param>
        /// <param name="keyPoints">массив опорных точек фигуры</param>
        public static void DrawSelection(Graphics g, Point[] keyPoints)
        {
            Pen pen = new Pen(Color.Black, 1);
            Pen pen2 = new Pen(Color.White, 1);
            for (int i = 0; i < keyPoints.Length; i++)
            {
                g.DrawRectangle(pen, GetKeyPointBlackRect(keyPoints[i]));
                g.DrawRectangle(pen2, GetKeyPointWhiteRect(keyPoints[i]));
            }
            pen.Dispose();
            pen2.Dispose();
        }

        /// <summary>
        /// определяет черный квадратик для данной опорной точки
        /// </summary>
        /// <param name="keyPoint">опорная точка</param>
        /// <returns></returns>
        private static Rectangle GetKeyPointBlackRect(Point keyPoint)
        {
            return new Rectangle(keyPoint.X - 4, keyPoint.Y - 4, 8, 8);
        }

        /// <summary>
        /// определяет белый квадратик для данной опорной точки
        /// </summary>
        /// <param name="keyPoint">опорная точка</param>
        /// <returns></returns>
        public static Rectangle GetKeyPointWhiteRect(Point keyPoint)
        {
            return new Rectangle(keyPoint.X - 5, keyPoint.Y - 5, 10, 10);
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
        /// <summary>
        /// Определяет точки вершин прямоугольника
        /// </summary>
        /// <param name="r"></param>
        /// <returns>массив из 4-х точек, вершин прямоугольника начиная с левой верхней вершины</returns>
        public static Point[] PointsFromRect(Rectangle r)
        {
            Point[] points = new Point[4];
            points[0] = new Point(r.X, r.Y); // левая верхняя точка
            points[1] = new Point(r.X + r.Width, r.Y); // и далее по часовой стрелке
            points[2] = new Point(r.X + r.Width, r.Y + r.Height);
            points[3] = new Point(r.X, r.Y + r.Height);

            return points;
        }
    }
}

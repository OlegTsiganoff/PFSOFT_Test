using System.Drawing;
using System.Collections.Generic;

namespace PaintInterface
{
    public interface IShape
    {
        /// <summary>
        /// Настраиваемые свойства фигуры
        /// толщина линии, цвет линии и цвет фона
        /// </summary>
        DrawSettings DrawSettings { get; set; }
        /// <summary>
        /// Опорные точки фигуры при выделении, перемещении, изменении формы и размера
        /// </summary>
        Point[] KeyPoints { get; set; }        
        bool IsSelected { get; set; }
        /// <summary>
        /// ункция отрисовки фигуры
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);
        /// <summary>
        /// функция отрисовки опорных точек при выделении
        /// </summary>
        /// <param name="g"></param>
        void DrawSelection(Graphics g);
        /// <summary>
        /// функция перемещения фигуры
        /// </summary>
        /// <param name="deltaX"> изменения по оси X</param>
        /// <param name="deltaY"> изменения по оси Y</param>
        void Move(int deltaX, int deltaY);
        /// <summary>
        /// функция изменения координат одной из опорных точек фигуры
        /// </summary>
        /// <param name="number"></param>
        /// <param name="destPoint"></param>
        void MoveKeyPoint(int number, Point destPoint);
        /// <summary>
        /// функция проверяет попадание точки в опорную точку если фигура выделена
        /// или если не выделена, то проверяет попадание точки в фигуру
        /// </summary>
        /// <param name="p">координаты клика</param>
        /// <returns>-1 - нет попадания, 0 - попадание в фигуру, 1 и более - номер опорной точки, в которую есть попадание</returns>
        int ContainsPoint(Point p);
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;

namespace PaintInterface
{
    public interface ITool
    {
        /// <summary>
        /// Название инструмента
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Иконка для кнопки инструмента
        /// </summary>
        Image Image { get; }
        /// <summary>
        /// Настраиваемые свойства фигуры
        /// толщина линии, цвет линии и цвет фона
        /// </summary>
        DrawSettings DrawSettings { get; set; }
        void OnMouseDown(UserControl canvas, MouseEventArgs e);
        void OnMouseMove(UserControl canvas, MouseEventArgs e);
        ITool SetSettings(int thickness, Color color, Color backColor);
    }
}

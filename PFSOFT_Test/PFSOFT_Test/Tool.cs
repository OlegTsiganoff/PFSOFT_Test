using System;
using System.Windows.Forms;
using System.Drawing;

namespace PFSOFT_Test
{
    /// <summary>
    /// базовый класс для инструментов, рисующих фигуры
    /// </summary>
 
    public abstract class Tool
    {
        public int Thickness { get; set; }
        public Color Color { get; set; }
        public Color BackColor { get; set; }

        protected Tool()
        {
            Thickness = 1;
            Color = System.Drawing.Color.Black;
            BackColor = System.Drawing.Color.Transparent;
        }
        public abstract void OnMouseDown(MyCanvas canvas, MouseEventArgs e);

        public abstract void OnMouseMove(MyCanvas canvas, MouseEventArgs e);

        public abstract void OnMouseUp(MyCanvas canvas, MouseEventArgs e);        

        public Tool SetSettings(int thickness, Color color, Color backColor)
        {
            Thickness = thickness;
            Color = color;
            BackColor = backColor;
            return this;
        }
    }
}

using System;
using System.Drawing;

namespace PFSOFT_Test
{
    // базовый класс для фигур
    [Serializable]
    public abstract class Shape
    {
        public Color Color { get; set; }
        public int Thickness { get; set; }

        public virtual void Draw(Graphics g)
        {

        }

    }
}

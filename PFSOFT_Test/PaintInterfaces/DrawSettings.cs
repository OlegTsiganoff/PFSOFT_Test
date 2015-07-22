using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaintInterface
{
    [Serializable]
    public struct DrawSettings
    {
        public int Thickness;
        public Color Color;
        public Color BackColor;

        public DrawSettings(int thickness, Color color, Color backColor)
        {
            Thickness = thickness;
            Color = color;
            BackColor = backColor;
        }
    }
}

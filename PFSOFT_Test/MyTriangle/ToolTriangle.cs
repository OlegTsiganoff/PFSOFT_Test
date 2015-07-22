using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using MyTriangle.Properties;
using PaintInterface;

namespace MyTriangle
{
    class ToolTriangle : ITool
    {
        Triangle triangle;

        public string Name { get { return "Triangle"; } }
        public System.Drawing.Image Image { get { return Resources.triangle; } }
        public DrawSettings DrawSettings { get; set; }


        public void OnMouseDown(UserControl canvas, MouseEventArgs e)
        {
            triangle = new Triangle(e.Location, new Point(e.X + 1, e.Y + 1));
            ApplySettings();
            var iShapeList = canvas as IAddShape;
            if (iShapeList != null)
                iShapeList.AddShape(triangle);            
        }

        public void OnMouseMove(UserControl canvas, MouseEventArgs e)
        {
            if (triangle == null || e.Button != MouseButtons.Left)
                return;
            triangle.EndPoint = e.Location;
            canvas.Refresh();
        }

        /// <summary>
        /// переносим сохраненные свойства фигуры из этого класса в объект фигуры
        /// </summary>
        void ApplySettings()
        {
            if (triangle != null)
            {
                triangle.DrawSettings = this.DrawSettings;
            }
        } 

        public ITool SetSettings(int thickness, Color color, Color backColor)
        {
            this.DrawSettings = new DrawSettings(thickness, color, backColor);
            return this;
        }

      
    }
}

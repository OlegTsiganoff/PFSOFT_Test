using System.Drawing;
using System.Windows.Forms;
using PFSOFT_Test.Properties;
using PaintInterface;

namespace PFSOFT_Test
{
    class ToolRect : ITool
    {
        Rect rect;
        private string name = "Rectangle";

        /// <summary>
        /// Название инструмента
        /// </summary>
        public string Name { get { return name; } }
        /// <summary>
        /// Изображение на кнопку с инструментом
        /// </summary>
        public Image Image { get { return Resources.rect; }  }

        public DrawSettings DrawSettings { get; set; }

        public ToolRect() {  }

        public void OnMouseDown(UserControl canvas, MouseEventArgs e)
        {
            rect = new Rect(e.Location, new Point(e.X + 1, e.Y + 1));
            ApplySettings();
            var iShapeList = canvas as IAddShape;
            if (iShapeList != null)
                iShapeList.AddShape(rect);
        }

        public void OnMouseMove(UserControl canvas, MouseEventArgs e)
        {
            if (rect == null || e.Button != MouseButtons.Left)
                return;

            rect.EndPoint = e.Location;
            canvas.Refresh();
        }

        /// <summary>
        /// переносим сохраненные свойства фигуры из этого класса в объект фигуры
        /// </summary>
        void ApplySettings()
        {
            if (rect != null)
            {
                rect.DrawSettings = this.DrawSettings;
            }
        } 

        public ITool SetSettings(int thickness, Color color, Color backColor)
        {
            this.DrawSettings = new DrawSettings(thickness, color, backColor);
            return this;
        }


        

        
    }
}

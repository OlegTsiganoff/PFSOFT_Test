using System.Drawing;
using System.Windows.Forms;
using PFSOFT_Test.Properties;
using PaintInterface;

namespace PFSOFT_Test
{
    class ToolCircle : ITool
    {
        Circle circle;
        private string name = "Circle";

        /// <summary>
        /// Название инструмента
        /// </summary>
        public string Name { get { return name; } }
        /// <summary>
        /// Изображение на кнопку с инструментом
        /// </summary>
        public Image Image { get { return Resources.circle; } }

        public DrawSettings DrawSettings { get; set; }

        public ToolCircle() { }

        public void OnMouseDown(UserControl canvas, MouseEventArgs e)
        {
            circle = new Circle(e.Location, new Point(e.X + 1, e.Y + 1));
            ApplySettings();
            var iShapeList = canvas as IAddShape;
            if (iShapeList != null)
                iShapeList.AddShape(circle);
        }

        public void OnMouseMove(UserControl canvas, MouseEventArgs e)
        {
            if (circle == null || e.Button != MouseButtons.Left)
                return;

            circle.EndPoint = e.Location;
            canvas.Refresh();
        }



        /// <summary>
        /// переносим сохраненные свойства фигуры из этого класса в объект фигуры
        /// </summary>
        void ApplySettings()
        {
            if (circle != null)
            {
                circle.DrawSettings = this.DrawSettings;
            }
        } 


       

        

        public ITool SetSettings(int thickness, Color color, Color backColor)
        {
            this.DrawSettings = new DrawSettings(thickness, color, backColor);
            return this;
        }
    }
}

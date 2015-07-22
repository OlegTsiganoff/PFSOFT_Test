using System.Drawing;
using System.Windows.Forms;
using PFSOFT_Test.Properties;
using PaintInterface;

namespace PFSOFT_Test
{
    class ToolLine : ITool
    {
        Line line;
        private string name = "Line";

        /// <summary>
        /// Название инструмента
        /// </summary>
        public string Name { get { return name; } }
        /// <summary>
        /// Изображение на кнопку с инструментом
        /// </summary>
        public Image Image { get { return Resources.line; } }

        public DrawSettings DrawSettings { get; set; }

        public ToolLine() { }

        public void OnMouseDown(UserControl canvas, MouseEventArgs e)
        {
            line = new Line(e.Location, new Point(e.X + 1, e.Y + 1));
            ApplySettings();
            var iShapeList = canvas as IAddShape;
            if(iShapeList != null)
                iShapeList.AddShape(line);
        }

        public void OnMouseMove(UserControl canvas, MouseEventArgs e)
        {
            if (line == null || e.Button != MouseButtons.Left)
                return;

            line.ChangeEndPoint(e.Location);
            canvas.Refresh();
        }

        /// <summary>
        /// переносим сохраненные свойства фигуры из этого класса в объект фигуры
        /// </summary>
        void ApplySettings()
        {
            if(line != null)
            {
                line.DrawSettings = this.DrawSettings;
            }
        }
        public ITool SetSettings(int thickness, Color color, Color backColor)
        {
            this.DrawSettings = new DrawSettings(thickness, color, backColor);
            return this;
        }
    }
}

using System.Drawing;
using System.Windows.Forms;
using PFSOFT_Test.Properties;
using PaintInterface;

namespace PFSOFT_Test
{
    class ToolCurve : ITool
    {        
        Curve curve;
        private string name = "Curve";

        /// <summary>
        /// Название инструмента
        /// </summary>
        public string Name { get { return name; } }
        /// <summary>
        /// Изображение на кнопку с инструментом
        /// </summary>
        public Image Image { get { return Resources.curve; } }

        public DrawSettings DrawSettings { get;  set; }

        public ToolCurve(){ }

        public void OnMouseDown(UserControl canvas, MouseEventArgs e)
        {            
            curve = new Curve();
            ApplySettings();
            curve.AddPoint(e.Location);
            var iShapeList = canvas as IAddShape;
            if(iShapeList != null)
                iShapeList.AddShape(curve);
        }

        public void OnMouseMove(UserControl canvas, MouseEventArgs e)
        {
            if (curve == null || e.Button != MouseButtons.Left)
                return;
            
            curve.AddPoint(e.Location);
            canvas.Refresh();
        }

        /// <summary>
        /// переносим сохраненные свойства фигуры из этого класса в объект фигуры
        /// </summary>
        void ApplySettings()
        {
            if(curve != null)
            {
                curve.DrawSettings = this.DrawSettings;
            }
        }

        public ITool SetSettings(int thickness, Color color, Color backColor)
        {
            this.DrawSettings = new DrawSettings(thickness, color, backColor);           
            return this;
        }
    }
}

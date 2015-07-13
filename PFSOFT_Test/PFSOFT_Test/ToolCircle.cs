using System.Drawing;
using System.Windows.Forms;

namespace PFSOFT_Test
{
    class ToolCircle :Tool
    {
        Circle circle;

        public ToolCircle() { }

        public override void OnMouseDown(MyCanvas canvas, MouseEventArgs e)
        {
            circle = new Circle(e.Location, e.Location);
            SetSettings();
            canvas.ShapeList.Add(circle);
        }

        public override void OnMouseMove(MyCanvas canvas, MouseEventArgs e)
        {
            if (circle == null || e.Button != MouseButtons.Left)
                return;

            circle.EndPoint = e.Location;
            canvas.Refresh();
        }

        public override void OnMouseUp(MyCanvas canvas, MouseEventArgs e)
        {

        }

        void SetSettings()
        {
            if(circle != null)
            {
                circle.Thickness = Thickness;
                circle.Color = Color;
                circle.BackColor = BackColor;
            }
        }

    }
}

using System.Drawing;
using System.Windows.Forms;

namespace PFSOFT_Test
{
    class ToolLine : Tool
    {
        Line line;

        public ToolLine() { }

        public override void OnMouseDown(MyCanvas canvas, MouseEventArgs e)
        {
            line = new Line(e.Location, e.Location);
            SetSettings();
            canvas.ShapeList.Add(line);
        }

        public override void OnMouseMove(MyCanvas canvas, MouseEventArgs e)
        {
            if (line == null || e.Button != MouseButtons.Left)
                return;

            line.ChangeEndPoint(e.Location);
            canvas.Refresh();
        }

        public override void OnMouseUp(MyCanvas canvas, MouseEventArgs e)
        {
            
        }

        void SetSettings()
        {
            if(line != null)
            {
                line.Thickness = Thickness;
                line.Color = Color;
            }
        }
    }
}

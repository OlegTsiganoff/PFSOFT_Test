using System.Drawing;
using System.Windows.Forms;

namespace PFSOFT_Test
{
    class ToolRect : Tool
    {
        Rect rect;
        

        public ToolRect() { }

        public override void OnMouseDown(MyCanvas canvas, MouseEventArgs e)
        {
            rect = new Rect(e.Location, e.Location);
            SetSettings();
            canvas.ShapeList.Add(rect);
        }

        public override void OnMouseMove(MyCanvas canvas, MouseEventArgs e)
        {
            if (rect == null || e.Button != MouseButtons.Left)
                return;

            rect.EndPoint = e.Location;
            canvas.Refresh();
        }

        public override void OnMouseUp(MyCanvas canvas, MouseEventArgs e)
        {

        }

        void SetSettings()
        {
            if(rect != null)
            {
                rect.Thickness = Thickness;
                rect.Color = Color;
                rect.BackColor = BackColor;
            }
        }
       
    }
}

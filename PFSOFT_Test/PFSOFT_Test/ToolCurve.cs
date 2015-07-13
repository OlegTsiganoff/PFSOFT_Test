using System.Drawing;
using System.Windows.Forms;


namespace PFSOFT_Test
{
    class ToolCurve : Tool
    {        
        Curve curve;

        public ToolCurve(){ }

        public override void OnMouseDown(MyCanvas canvas, MouseEventArgs e)
        {            
            curve = new Curve();
            SetSettings();
            curve.AddPoint(e.Location);
            canvas.ShapeList.Add(curve);
        }

        public override void OnMouseMove(MyCanvas canvas, MouseEventArgs e)
        {
            if (curve == null || e.Button != MouseButtons.Left)
                return;
            
            curve.AddPoint(e.Location);
            canvas.Refresh();
        }

        public override void OnMouseUp(MyCanvas canvas, MouseEventArgs e)
        {
            
        }

        void SetSettings()
        {
            if(curve != null)
            {
                curve.Thickness = Thickness;
                curve.Color = Color;
            }
        }

    }
}

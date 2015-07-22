using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using PaintInterface;

namespace PFSOFT_Test
{

    // класс содержащий список нарисованных фигур
    // объект этого класса мы сохраняем и достаем из файла
    [Serializable]
    class ShapeList 
    {
        Color canvasBackground = Color.White;
        List<IShape> shapes;        
        
        public Color CanvasBackground
        {
            get { return canvasBackground; }
            set { canvasBackground = value; }
        }

        public List<IShape> Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }

        public ShapeList() { shapes = new List<IShape>(); }

        public void Draw(Graphics g)
        {
            foreach (IShape shape in shapes)
            {
                shape.Draw(g);
                if(shape.IsSelected)
                {
                    shape.DrawSelection(g);
                }
            }
        }


        public void Clear()
        {
            if (shapes != null)
                shapes.Clear();
        }

        public void Add(IShape shape)
        {
            shapes.Add(shape);
        }

        public void DeselectAll()
        {
            foreach (var item in shapes)
                item.IsSelected = false;
        }

        /// <summary>
        /// асинхронная функция инвертирования цветов всех нарисованных объектов
        /// </summary>
        /// <param name="progress"> переменная для обратной связи (отслеживания состояния)</param>
        /// <returns></returns>
 
        public async Task InvertColors(IProgress<Int32> progress)
        {            
            int count = 0;
            foreach(IShape shape in shapes)
            {
                Color color = Color.FromArgb(shape.DrawSettings.Color.ToArgb() ^ 0xffffff);
                Color backColor = Color.FromArgb(shape.DrawSettings.BackColor.ToArgb() ^ 0xffffff);

                shape.DrawSettings = new DrawSettings(shape.DrawSettings.Thickness, color, backColor);

                progress.Report(count++ * 100 / shapes.Count);
                await Task.Delay(100);
            }
            canvasBackground = Color.FromArgb(canvasBackground.ToArgb() ^ 0xffffff);
        }
        
    }
}

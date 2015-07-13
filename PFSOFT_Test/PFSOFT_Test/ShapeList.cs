using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;


namespace PFSOFT_Test
{

    // класс содержащий список нарисованных фигур
    // объект этого класса мы сохраняем и достаем из файла
    [Serializable]
    class ShapeList 
    {
        Color canvasBackground = Color.White;
        List<Shape> shapes;        
        
        public Color CanvasBackground
        {
            get { return canvasBackground; }
            set { canvasBackground = value; }
        }

        public List<Shape> Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }

        public ShapeList() { shapes = new List<Shape>(); }

        public void Draw(Graphics g)
        {
            foreach(Shape shape in shapes)
            {
                shape.Draw(g);
            }
        }

        public void Clear()
        {
            if (shapes != null)
                shapes.Clear();
        }

        public void Add(Shape shape)
        {
            shapes.Add(shape);
        }

        public void RemoveLast()
        {
            shapes.RemoveAt(shapes.Count - 1);
        }

        /// <summary>
        /// асинхронная функция инвертирования цветов всех нарисованных объектов
        /// </summary>
        /// <param name="progress"> переменная для обратной связи (отслеживания состояния)</param>
        /// <returns></returns>
 
        public async Task InvertColors(IProgress<Int32> progress)
        {            
            int count = 0;
            foreach(Shape shape in shapes)
            {
                shape.Color = Color.FromArgb(shape.Color.ToArgb() ^ 0xffffff);
                
                var rect = shape as Rect;
                if(rect != null)
                    rect.BackColor = Color.FromArgb(rect.BackColor.ToArgb() ^ 0xffffff);
                var circle = shape as Circle;
                if(circle != null)
                    circle.BackColor = Color.FromArgb(circle.BackColor.ToArgb() ^ 0xffffff);

                progress.Report(count++ * 100 / shapes.Count);
                await Task.Delay(100);
            }
            canvasBackground = Color.FromArgb(canvasBackground.ToArgb() ^ 0xffffff);
        }
        
    }
}

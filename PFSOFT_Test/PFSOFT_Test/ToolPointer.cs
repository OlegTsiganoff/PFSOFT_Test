using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using PFSOFT_Test.Properties;
using PaintInterface;

namespace PFSOFT_Test
{
    internal enum PointerMode
    {
        None,
        Move,
        Resize
    }

    class ToolPointer: ITool
    {
        Point startPoint;
        IShape selectedShape; // выделенная фигура
        PointerMode pointerMode = PointerMode.None; // режим выделения
        int keyPointNumber; // номер выбранной опорной точки фигуры
        public string Name { get { return "Pointer"; } }    

        public Image Image { get { return Resources.pointer; } }

        public DrawSettings DrawSettings { get; set; }

        public void OnMouseDown(UserControl canvas, MouseEventArgs e)
        {
            MyCanvas myCanvas = canvas as MyCanvas;
            if (myCanvas != null)
            {
                int count = myCanvas.ShapeList.Shapes.Count;
                bool found = false;                
                for (int i = count - 1; i >= 0; i--) // перебираем все отрисованные фигуры
                {
                    int number = myCanvas.ShapeList.Shapes[i].ContainsPoint(e.Location);
                    if (number == 0)    // если есть попадание клика в фигуру
                    {                   // выделяем  фигуру
                        myCanvas.ShapeList.DeselectAll();
                        startPoint = e.Location; // запоминаем координаты клика
                        selectedShape = myCanvas.ShapeList.Shapes[i]; // запоминаем выделенную фигуру
                        selectedShape.IsSelected = true; // помечаем фигуру как выделенную
                        found = true;
                        pointerMode = PointerMode.Move; // переключаем режим выделения на перемещение
                        break;
                    }

                    if(number > 0)
                    {
                        myCanvas.ShapeList.DeselectAll();
                        startPoint = e.Location;
                        keyPointNumber = number; // запоминаем номер опорной точки выделенной фигуры
                        selectedShape = myCanvas.ShapeList.Shapes[i];
                        selectedShape.IsSelected = true;
                        found = true;
                        pointerMode = PointerMode.Resize; // переключаем режим выделения на изменение размера 
                        break;
                    }
                }

                if (found == false) // если клик не попал ни в одну фигуру
                {
                    myCanvas.ShapeList.DeselectAll(); // снимаем выделение со всех фигур
                    selectedShape = null;
                    pointerMode = PointerMode.None; // обнуляем режим выделения
                }
                
            }
            myCanvas.Refresh();
        }

        public void OnMouseMove(UserControl canvas, MouseEventArgs e)
        {
            if (selectedShape == null || e.Button != MouseButtons.Left)
                return;
            if (pointerMode == PointerMode.Move) // режим перемещения
            {
                int deltaX = e.X - startPoint.X;
                int deltaY = e.Y - startPoint.Y;
                startPoint = e.Location;
                selectedShape.Move(deltaX, deltaY); // перемещаем фигуру
            }

            if(pointerMode == PointerMode.Resize)
            {
                selectedShape.MoveKeyPoint(keyPointNumber, e.Location); // перемещаем опорную точку
            }
            canvas.Refresh();
        }


        public ITool SetSettings(int thickness, Color color, Color backColor) 
        { 
            if(selectedShape != null)
            {
                selectedShape.DrawSettings = new DrawSettings(thickness, color, backColor);
            }
            return this; 
        }

        private void DrawSelection()
        {

        }
    }
}

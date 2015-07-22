using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintInterface;

namespace PFSOFT_Test
{
    // пользовательский контрол для рисования
    public partial class MyCanvas : UserControl, IAddShape
    {
        MainForm parent;        // ссылка на родительское окно
        ShapeList shapeList;    // список нарисованных фигур
        List<IShape> redoList;  // список отмененных фигур
        ITool activeTool;       // активный инструмент        


        internal ShapeList ShapeList
        {
            get { return shapeList; }
            set { shapeList = value; }
        }

        public List<IShape> RedoList
        {
            get { return redoList; }
            set { redoList = value; }
        }

        public ITool ActiveTool
        {
            get { return activeTool; }
            set { activeTool = value; }
        }

        public MyCanvas(MainForm form)
        {
            InitializeComponent();
            parent = form;
            // подписываемся на событие изменения настроек в родительском окне
            parent.SettingsChanged += parent_SettingsChanged;            
            MyInit();
        }

        // инициализация
        void MyInit()
        {
            shapeList = new ShapeList();
            redoList = new List<IShape>();
            this.Paint += MyCanvas_Paint;
            this.MouseDown += MyCanvas_MouseDown;
            this.MouseMove += MyCanvas_MouseMove;
            this.DoubleBuffered = true;        
        }

        public void AddShape(IShape shape)
        {
            ShapeList.Add(shape);
            parent.EnableUndoButton(true);
        }

        void parent_SettingsChanged(int thickness, Color color, Color backColor)
        {
            if(activeTool != null)
            {
                activeTool.SetSettings(thickness, color, backColor);
                this.Refresh();
            }
        }

        
        // обработчик события перерисовки контрола
        void MyCanvas_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(shapeList.CanvasBackground);
            e.Graphics.FillRectangle(brush, this.ClientRectangle); // заливаем выбранным цветом клиентскую область
            if (shapeList != null)
                shapeList.Draw(e.Graphics); // отрисовываем все сохраненные фигуры

            brush.Dispose();
        }

        void MyCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (activeTool != null)
                activeTool.OnMouseDown(this, e); // перебрасываем событие в соответствующий класс
        }

        void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeTool != null)
                activeTool.OnMouseMove(this, e); // перебрасываем событие в соответствующий класс
        }

        


       
    }
}

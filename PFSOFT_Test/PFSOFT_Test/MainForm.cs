using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PaintInterface;

namespace PFSOFT_Test
{
    // делегат для события SettingsChanged
    delegate void ChangeSettings(int thickness, Color color, Color backColor);
    public partial class MainForm : Form
    {
        MyCanvas myCanvas;      // поле для рисования
        int thickness = 1;      // толщина линии
        Color color = Color.Black; // цвет линии
        Color backColor = Color.Transparent;    // цвет фона фигуры
        Color oldBackColor = Color.Transparent; // цвет выбранный последний раз в ColorDialog
        ITool lastTool;      // последний инструмент, который использовался
        bool isAsyncRuning = false; // выполняется ли сейчас асинхронная операция (Инвертирование)
        

        internal event ChangeSettings SettingsChanged;        

        public MainForm()
        {
            InitializeComponent();
            MyInit();
        }

        // инициализация
        void MyInit()
        {
            if(comboBoxThickness.Items.Count > 0)
                comboBoxThickness.SelectedIndex = 0;
            comboBoxThickness.SelectedIndexChanged += comboBoxThickness_SelectedIndexChanged;

            butColor.BackColor = color;
            butColor.Click += butColor_Click;

            butBackColor.BackColor = SystemColors.Control;
            butBackColor.Click += butBackColor_Click;

            checkBoxTransparent.CheckedChanged += checkBoxTransparent_CheckedChanged;

            progressBar.Visible = false;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;

            myCanvas = new MyCanvas(this);
            myCanvas.ActiveTool = null;        
            this.Controls.Add(myCanvas);
            myCanvas.Left = this.ClientRectangle.Left + 10;
            myCanvas.Top = this.ClientRectangle.Top + toolStripMain.Height + menuStripMain.Height + 10;
            myCanvas.Width = this.ClientRectangle.Width - 20;
            myCanvas.Height = this.ClientRectangle.Height - toolStripMain.Height - menuStripMain.Height - 20;
            myCanvas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                        
            FillMainToolStrip();
        }

        /// <summary>
        ///  Заполняет кнопками панель инструментов
        /// </summary>
        void FillMainToolStrip()
        {
            // ищем сборки в корневой папке приложения
            Program.toolService.FindToolLibrary(Environment.CurrentDirectory);
            foreach(var tool in Program.toolService.avalibleTools)
            {
                ToolStripButton button = new ToolStripButton("", tool.Image, toolStripButton_Click);
                button.Tag = tool.GetType();
                button.CheckOnClick = true;
                button.ToolTipText = tool.Name;

                toolStripMain.Items.Add(button);
                ToolStripSeparator separator = new ToolStripSeparator();
                toolStripMain.Items.Add(separator);
            }
        }

        /// <summary>
        /// обработчик checkBox Transparent 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        void checkBoxTransparent_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxTransparent.Checked)
            {
                butBackColor.BackColor = SystemColors.Control;
                backColor = Color.Transparent;
                SettingsChanged(thickness, color, backColor); // обновляем настройки
            }
            else
            {
                butBackColor.BackColor = oldBackColor;
                backColor = oldBackColor;
                SettingsChanged(thickness, color, backColor); // обновляем настройки
            }
        }

        /// <summary>
        /// вызываем диалог выбора фона фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        void butBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = backColor;
            dialog.CustomColors = new int[] { Color.Transparent.ToArgb() };
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                backColor = oldBackColor = dialog.Color;
                SettingsChanged(thickness, color, backColor); // обновляем настройки
                checkBoxTransparent.Checked = false;
            }
            butBackColor.BackColor = backColor;
        }

        /// <summary>
        /// вызываем диалог выбора цвета линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        void butColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = color;
            if(dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                color = dialog.Color;
                SettingsChanged(thickness, color, backColor); // обновляем настройки
            }
            butColor.BackColor = color;
        }

        /// <summary>
        /// обработчик comboBox (выбор толщины линии)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        void comboBoxThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32.TryParse(comboBoxThickness.SelectedItem.ToString(), out thickness);
            SettingsChanged(thickness, color, backColor); // обновляем настройки
        }

        /// <summary>
        /// Обработчик меню New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isAsyncRuning)
            {
                MessageBox.Show("Подождите окончания операции.");
                return;
            }
            EnableUndoButton(false);
            EnableRedoButon(false);
            myCanvas.RedoList.Clear();
            // очищаем список фигур
            myCanvas.ShapeList.Clear();
            myCanvas.Refresh();
        }

        /// <summary>
        /// Обработчик меню Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isAsyncRuning)
            {
                MessageBox.Show("Подождите окончания операции.");
                return;
            }
            string fileName = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Super Paint files (*.spt)|*.spt|All Files (*.*)|*.*";
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            fileDialog.Multiselect = false;
            if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EnableUndoButton(false);
                EnableRedoButon(false);
                myCanvas.RedoList.Clear();

                fileName = fileDialog.FileName;
                try
                {
                    using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {                        
                        IFormatter formatter = new BinaryFormatter();
                        myCanvas.ShapeList = (ShapeList)formatter.Deserialize(stream);
                        myCanvas.Refresh();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Open file error");
                }
            }
        }

        /// <summary>
        /// Обработчик меню Сохранить (Save)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newFileName = "Untitled.spt";
            string filter = "Super Paint files (*.spt)|*.spt|All Files (*.*)|*.*";
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = filter;
            fileDialog.FileName = newFileName;
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                newFileName = fileDialog.FileName;
                try
                {
                    using (Stream stream = new FileStream(newFileName, FileMode.Create, FileAccess.Write))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, myCanvas.ShapeList);                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save error!");
                }
            }                       
        }

        /// <summary>
        /// Обработчик меню Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработчик кнопок инструментов
        /// </summary>
        /// <param name="sender">кнопка, которую нажали</param>
        /// <param name="e">параметры</param>
        private void toolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            if(button != null)
            {
                Type toolType = button.Tag as Type;
                if(toolType != null)
                {
                    ITool tool = Activator.CreateInstance(toolType) as ITool;
                    if (tool != null)                    
                        myCanvas.ActiveTool = tool.SetSettings(thickness, color, backColor);
                    myCanvas.ShapeList.DeselectAll();
                    myCanvas.Refresh();
                }
            }

            // снимаем выделение с остальных кнопок
            foreach(var butt in toolStripMain.Items)
            {
                if(butt != sender)
                {
                    ToolStripButton b = butt as ToolStripButton;
                    if (b != null)
                        b.Checked = false;
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки Invert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butInvert_Click(object sender, EventArgs e)
        {
            isAsyncRuning = true;
            lastTool = myCanvas.ActiveTool; // сохраняем текущий инструмент
            EnableToolButtons(false);       // отключаем кнопки включения инструментов
            myCanvas.ActiveTool = null;     // обнуляем текущий инструмент
            progressBar.Visible = true;  
            progressBar.Value = 0;

            Progress<int> progressIndicator = new Progress<int>(ReportProgress); 
            await myCanvas.ShapeList.InvertColors(progressIndicator); // запускаем асинхронно функцию инвертирования
            progressBar.Visible = false;
            myCanvas.Refresh();
            EnableToolButtons(true);            // по завершении операции включаем кнопки инструментов
            myCanvas.ActiveTool = lastTool;     // активизируем сохраненный инструмент
            isAsyncRuning = false;
        }

        /// <summary>
        /// функция обновления состояния прогресс бара
        /// </summary>
        /// <param name="amount">состояние прогресса от 1 до 100</param> 
        void ReportProgress(int amount)
        {
            progressBar.Value = amount;
            myCanvas.Refresh();
        }

        
        
        /// <summary>
        /// Обработчик кнопки Undo (Назад, отмена)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            if (myCanvas.ShapeList.Shapes.Count > 0)
            {
                // запоминаем последнюю фигуру в списке
                IShape shape = myCanvas.ShapeList.Shapes.ElementAt(myCanvas.ShapeList.Shapes.Count - 1);
                myCanvas.ShapeList.Shapes.RemoveAt(myCanvas.ShapeList.Shapes.Count - 1); // удаляем из списка последнюю фигуру
                myCanvas.RedoList.Add(shape);   // добавляем эту фигуру в список отмененных
                EnableRedoButon(true);
                myCanvas.Refresh(); // перерисовываем поляну
            }
            if (myCanvas.ShapeList.Shapes.Count == 0)
                EnableUndoButton(false);
        }

        /// <summary>
        /// Обработчик кнопки Redo (вперед, вернуть отмененную операцию)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRedo_Click(object sender, EventArgs e)
        {
            if (myCanvas.RedoList.Count > 0)
            {
                // запоминаем последнюю фигуру из списка отмененных
                IShape shape = myCanvas.RedoList.ElementAt(myCanvas.RedoList.Count - 1); 
                myCanvas.RedoList.RemoveAt(myCanvas.RedoList.Count - 1); // удаляем последнюю в списке отмененных
                myCanvas.ShapeList.Add(shape); // добавляем фигуру в список отрисованных
                EnableUndoButton(true);
                myCanvas.Refresh(); // перерисовываем поляну
            }
            if (myCanvas.RedoList.Count == 0)
                EnableRedoButon(false);
        }

        /// <summary>
        /// активация и деактивация кнопок инструментов
        /// </summary>
        /// <param name="enable"></param> 
        void EnableToolButtons(bool enable)
        {
            foreach (var item in toolStripMain.Items)
            {
                ToolStripButton button = item as ToolStripButton;
                if (button != null)
                    button.Enabled = enable;
            }
        }

        /// <summary>
        /// активация и деактивация кнопки Вперед
        /// </summary>
        /// <param name="enable"></param>
        public void EnableRedoButon(bool enable)
        {
            buttonRedo.Enabled = enable;
        }

        /// <summary>
        /// активация и деактивация кнопки Назад
        /// </summary>
        /// <param name="enable"></param>
        public void EnableUndoButton(bool enable)
        {
            buttonUndo.Enabled = enable;
        }
       
    }
}

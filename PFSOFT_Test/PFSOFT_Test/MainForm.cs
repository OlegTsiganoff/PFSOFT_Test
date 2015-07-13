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
        Tool lastTool;      // последний инструмент, который использовался
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
        }

        // обработчик checkBox Transparent        
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

        // вызываем диалог выбора фона фигуры
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

        // вызываем диалог выбора цвета линии
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

        // обработчик comboBox (выбор толщины линии)
        void comboBoxThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32.TryParse(comboBoxThickness.SelectedItem.ToString(), out thickness);
            SettingsChanged(thickness, color, backColor); // обновляем настройки
        }

        // обработчик Новый документ
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isAsyncRuning)
            {
                MessageBox.Show("Подождите окончания операции.");
                return;
            }
            // очищаем список фигур
            myCanvas.ShapeList.Clear();
            myCanvas.Refresh();
        }

        // открываем диалог открытия файла
        // открываем файл и восстанавливаем объект ShapeList
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

        // открываем диалог сохранения файла
        // сохраняем объект ShapeList в файл
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

        // делаем активным инструмент Кривая линия
        private void toolStripCurve_Click(object sender, EventArgs e)
        {
            if (toolStripCurve.Checked == false)
            {
                toolStripCurve.Checked = true;
                toolStripRectangle.Checked = false;
                toolStripCircle.Checked = false;
                toolStripLine.Checked = false;                
                myCanvas.ActiveTool = new ToolCurve().SetSettings(thickness, color, backColor);
            }
            else
            {
                toolStripCurve.Checked = false;               
                myCanvas.ActiveTool = null;
            }
            
        }

        // делаем активным инструмент Прямая линия
        private void toolStripLine_Click(object sender, EventArgs e)
        {
            if (toolStripLine.Checked == false)
            {
                toolStripLine.Checked = true;
                toolStripRectangle.Checked = false;
                toolStripCircle.Checked = false;
                toolStripCurve.Checked  = false;
                myCanvas.ActiveTool = new ToolLine().SetSettings(thickness, color, backColor);;
                
            }
            else
            {
                toolStripLine.Checked = false;
                myCanvas.ActiveTool = null;
            }
        }

        // делаем активным инструмент Прямоугольник
        private void toolStripRectangle_Click(object sender, EventArgs e)
        {
            if ( toolStripRectangle.Checked == false)
            {
                toolStripRectangle.Checked = true;
                toolStripCurve.Checked = false;
                toolStripCircle.Checked = false;
                toolStripLine.Checked = false;
                myCanvas.ActiveTool = new ToolRect().SetSettings(thickness, color, backColor);;
            }
            else
            {
                toolStripCurve.Checked = false;
                myCanvas.ActiveTool = null;
            }
        }

        // делаем активным инструмент Круг
        private void toolStripCircle_Click(object sender, EventArgs e)
        {
            if (toolStripCircle.Checked == false)
            {
                toolStripCircle.Checked = true;
                toolStripRectangle.Checked = false;
                toolStripCurve.Checked = false;
                toolStripLine.Checked = false;
                myCanvas.ActiveTool = new ToolCircle().SetSettings(thickness, color, backColor);;
            }
            else
            {
                toolStripCurve.Checked = false;
                myCanvas.ActiveTool = null;
            }
        }

        // запускаем процесс инвертирования рисунка
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

        // функция обновления состояния прогресс бара
        void ReportProgress(int amount)
        {
            progressBar.Value = amount;
            myCanvas.Refresh();
        }

        // активация и деактивация кнопок инструментов
        void EnableToolButtons(bool enable)
        {
            toolStripCurve.Enabled = enable;
            toolStripLine.Enabled = enable;
            toolStripRectangle.Enabled = enable;
            toolStripCircle.Enabled = enable;
        }

        // закрываем окно
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}

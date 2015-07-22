using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintInterface;

namespace PFSOFT_Test
{
    static class Program
    {
        /// <summary>
        /// Содержит список доступных инструментов и методы поиска инструментах в сборках
        /// </summary>
        public static ToolsService toolService;

        static Program()
        {
            toolService = new ToolsService();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

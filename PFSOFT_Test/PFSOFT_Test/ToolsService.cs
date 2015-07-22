using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using PaintInterface;

namespace PFSOFT_Test
{
    class ToolsService
    {
        /// <summary>
        /// список доступных инструментов рисования
        /// </summary>
        public List<ITool> avalibleTools;

        public ToolsService()
        {
            avalibleTools = new List<ITool>();
        }

        /// <summary>
        /// Ищет все подходящие сборки в данной директории
        /// </summary>
        /// <param name="path">Путь к папке со сборками, которые содержат инструменты для рисования</param>
        public void FindToolLibrary(string path)
        {
            avalibleTools.Clear();

            foreach (string fileName in Directory.GetFiles(path))
            {
                FileInfo file = new FileInfo(fileName);
                if (file.Extension.Equals(".dll") || file.Extension.Equals(".exe"))
                {
                    this.AddToolLibrary(fileName);
                }
            }
        }

        /// <summary>
        /// Ищет и добавляет в список инструменты найденные в сборке
        /// </summary>
        /// <param name="fileName">имя файла сборки</param>
        private void AddToolLibrary(string fileName)
        {
            Assembly toolAssembly = Assembly.LoadFrom(fileName);

            foreach (Type toolType in toolAssembly.GetTypes())
            {
                Type toolInterface = toolType.GetInterface("PaintInterface.ITool");
                if (toolInterface != null)
                {
                    var tool = Activator.CreateInstance(toolType);
                    avalibleTools.Add((ITool)tool);
                }
            }
        }
    }
}

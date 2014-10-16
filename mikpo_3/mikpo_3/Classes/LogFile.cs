using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikpo_3.Classes
{
    class LogFile
    {
        private System.IO.StreamWriter sw;

        public LogFile(string path)
        {
            sw = new System.IO.StreamWriter(path, true, Encoding.UTF8);
        }

        /// <summary>
        /// Запись в лог файл
        /// </summary>
        /// <param name="line">Сообщение</param>
        public void Add(string line)
        {
            DateTime presently = DateTime.Now;
            line = String.Format("[{0}] - {1}", presently.ToString(), line);
            sw.WriteLine(line);
            sw.Flush();
        }
    }
}

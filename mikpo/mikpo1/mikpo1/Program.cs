using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mikpo1
{
    /// <summary>
    /// Браун Д.В.
    /// </summary>
    class Program
    {
       

        /// <summary>
        /// Тест 1: Удаление файла входных данных
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void deleteFile(string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);
        }

        /// <summary>
        /// Тест 2: Создание пустого файла
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void emptyFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.Close();
        }

        /// <summary>
        /// Тест 3: 1 строка, количество входных значений превышает 3
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void manyArgsFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            Random r = new Random();
            int N = r.Next(200);
            string temp = "";
            for (int i = 0; i < N; i++)
                temp += (r.NextDouble() * 180).ToString()+";";
            sw.WriteLine(temp.Substring(0, temp.Length - 1));
            sw.Close();
        }

        /// <summary>
        /// Тест 4: 1 строка, количество входных значений меньше 3
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void fewArgsFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            Random r = new Random();
            int N = r.Next(2)+1;
            string temp = "";
            for (int i = 0; i < N; i++)
                temp += (r.NextDouble() * 180).ToString() + ";";
            sw.WriteLine(temp.Substring(0, temp.Length - 1));
            sw.Close();
        }

        /// <summary>
        /// Тест 5: Многострочный файл, каждая строка содержит корректные данные
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void manyStringFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            Random r = new Random();
            int N = 3;
            string temp = "";
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < N; j++)
                    temp += (r.NextDouble() * 180).ToString() + ";";
                sw.WriteLine(temp.Substring(0, temp.Length - 1));
                temp = "";
            }
            sw.Close();
        }

        /// <summary>
        /// Тест 6: Многострочный файл, каждая строка содержит случайные данные
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void randomStringFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            Random r = new Random();
            int N = 3;
            string temp = "";
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < N; j++)
                    temp += ((char)r.Next(255)).ToString() + ";";
                sw.WriteLine(temp.Substring(0, temp.Length - 1));
                temp = "";
            }
            sw.Close();
        }

        /// <summary>
        /// Тест 7: 1 строка, входные данные отрицательные
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void negativeArgsFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            Random r = new Random();
            int N = 3;
            string temp = "";
            for (int i = 0; i < N; i++)
                temp += (-r.NextDouble() * 180).ToString() + ";";
            sw.WriteLine(temp.Substring(0, temp.Length - 1));
            sw.Close();
        }

        /// <summary>
        /// Тест 8: 1 строка, входные данные корректные
        /// </summary>
        /// <param name="filename">Имя файла входных данных</param>
        public static void rightArgsFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            Random r = new Random();
            int N = 3;
            string temp = "";
            for (int i = 0; i < N; i++)
                temp += (r.NextDouble() * 180).ToString() + ";";
            sw.WriteLine(temp.Substring(0, temp.Length - 1));
            sw.Close();
        }

        /// <summary>
        /// Запуск теста
        /// </summary>
        /// <param name="proc">Выполняемый процесс</param>
        /// <param name="time">Время ожидания выполнения процесса</param>
        /// <param name="N">Порядковый номер теста</param>
        public static void beginTest(ref System.Diagnostics.Process proc, int time, int N)
        {
            if (proc.WaitForExit(time))
                Console.WriteLine("Тест " + N.ToString() + " пройден");
            else
            {
                Console.WriteLine("Тест " + N.ToString() + " не  пройден");
                proc.Kill();
            }
        }

        /// <summary>
        /// Тест программы 
        /// </summary>
        /// <param name="args[0]">Путь к файлу тестируемой программы</param>
        /// <param name="args[1]"аргументы</param>
        static void Main(string[] args)
        {
            try
            {
                string programname = args[0];
                string filename = args[1];

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = programname;
                proc.StartInfo.Arguments = filename;


                //-------------Тест 1-----------------
                deleteFile(filename);
                proc.Start();
                beginTest(ref proc, 1000, 1);

                //-------------Тест 2-----------------
                emptyFile(filename);
                proc.Start();
                beginTest(ref proc, 1000, 2);

                //-------------Тест 3-----------------
                manyArgsFile(filename);
                proc.Start();
                beginTest(ref proc, 1000, 3);

                //-------------Тест 4-----------------
                fewArgsFile(filename);
                proc.Start();
                beginTest(ref proc, 1000, 4);

                //-------------Тест 5-----------------
                manyStringFile(filename);
                proc.Start();
                beginTest(ref proc, 2000, 5);

                //-------------Тест 6-----------------
                randomStringFile(filename);
                proc.Start();
                beginTest(ref proc, 2000, 6);

                //-------------Тест 7-----------------
                negativeArgsFile(filename);
                proc.Start();
                beginTest(ref proc, 1000, 7);

                //-------------Тест 8-----------------
                rightArgsFile(filename);
                proc.Start();
                beginTest(ref proc, 1000, 8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

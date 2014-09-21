using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikpo.Classes
{
    /// <summary>
    /// 
    /// Муравьев Игорь Александрович
    /// 21.09.2014
    /// 
    /// </summary>
    public class TriangleInfo
    {
        /// <summary>
        ///  Функция, которая распарсивает входную строку
        /// </summary>
        /// <param name="s"> Входная строка. Должна иметь формат a;b;alpha</param>
        /// <returns>Массив значений типа double </returns>
        public static double[] parseString(string s)
        {
            double[] res = null;
            string[] buf1 = s.Split(';');
            res = new double[3];
            for (int i = 0; i < buf1.Length; i++)
            {
                res[i] = Convert.ToDouble(buf1[i]);
                if (res[i] <= 0) throw new Exception("Число меньше либо равно 0");
            }
            if (res[2] > 180) throw new Exception("Неверный угол");
            return res;
        }


        /// <summary>
        ///  чтение данных с файла
        /// </summary>
        /// <param name="filename"> Путь к файлу</param>
        /// <returns>Массив из 3х элементов , сторона сторона угол </returns>
        public static void Processing(string infile, string outfile)
        {
            string currentLine = "";
            double[] res = null;
            int currentRowNum = 0;
            using (StreamWriter sw = new StreamWriter(outfile))
            {
                using (StreamReader sr = new StreamReader(infile))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            currentRowNum++;
                            currentLine = sr.ReadLine();
                            res = parseString(currentLine);

                            double c = TriangleInfo.getC(res[0], res[1], res[2]);
                            double betta = TriangleInfo.getAngle(res[0], c, res[1]);
                            double alpha = TriangleInfo.getAngle(res[1], c, res[0]);

                            sw.WriteLine(String.Format("{0};{1};{2}", res[0], res[1], c));
                            Console.WriteLine("**********Строка " + currentRowNum.ToString() + "************");
                            Console.WriteLine(String.Format("{0};{1} // Угол между сторонами a и с ; b и с", betta, alpha));
                        }
                        catch(Exception ex)
                        {
                            // Выводит ERROR ROW , если входная строка была ошибочной
                            sw.WriteLine(";ERROR ROW;"+ ex.Message);
                            Console.WriteLine("**********Строка "+currentRowNum.ToString()+"************");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.Source);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  Переводит градусы в радианы
        /// </summary>
        /// <param name="angle">Угол в градусах</param>
        /// <returns></returns>
        public static double toRad(double angle)
        {
            return angle * Math.PI / 180;
        }



        /// <summary>
        /// Перевод радиан в градусы
        /// </summary>
        /// <param name="rad">Радианы</param>
        /// <returns></returns>
        public static double toAngle(double rad)
        {
            return rad * 180 / Math.PI;
        }

        /// <summary>
        ///  Возвращает сторону по теореме косинусов
        /// </summary>
        /// <param name="a"> Сторона 1</param>
        /// <param name="b">Сторона 2 </param>
        /// <param name="alpha">Угол между стороной 1 и 2</param>
        /// <returns>Возвращает сторону по теореме косинусов</returns>
        public static double getC(double a, double b, double alpha)
        {
            return Math.Sqrt(a * a + b * b - 2 * a * b * Math.Cos(toRad(alpha)));
        }

        /// <summary>
        ///  Уго между сторонами a и b
        /// </summary>
        /// <param name="a"> Длина стороны а</param>
        /// <param name="b">Длина стороны  b</param>
        /// <param name="c">Длина стороны с</param>
        /// <returns>Угол между сторонами a и b</returns>
        public static double getAngle(double a, double b, double c)
        {
            return toAngle(Math.Acos((a * a + b * b - c * c) / 2 / a / b));
        }
    }
}

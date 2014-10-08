using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace MiKPO_2.Classes
{

    /// <summary>
    ///  Делегат, описывающий функцию вычисления нового цвета
    /// </summary>
    /// <param name="list"> Список цветов, окружающих пикселей</param>
    /// <returns></returns>
    public delegate Color CalcColor(List<Color> list);


    class MedianFilter
    {
        public static int bmp_info = 54;   //Байты описания файла

        private int width, height;   //Ширина/высота изображения
        public int length;          //Длина массива байтов
        private int windowSize;    //Размер окна
        public IComparer<Color> comp;     //компоратор
        public CalcColor calcColor; // Выбор нового цвета 


        public byte[] input, output;    //Исходный и итоговый массивы байтов

        #region Конструктор
        public MedianFilter(int windowSize)
        {
            this.comp = new Comparer();
            this.calcColor = this.avgColor;
            this.windowSize = windowSize;
        }
        #endregion

        #region Загрузка/Сохранение
        public void Load(string filename)
        {
                using (Image img = Image.FromFile(filename))
                {
                    this.width = img.Width * 3;
                    this.height = img.Height * 3;
                    this.input = imageToArray(img);
                    this.length = this.input.Length;
                    this.output = new byte[this.input.Length];
                    this.input.CopyTo(this.output, 0);
                }
        }

        public void Save(string filename)
        {
           using (Image img = arrayToImage(this.output))
                img.Save(filename);
        }
        #endregion

        #region Конвертация в массив/в изображение
        public byte[] imageToArray(Image img)
        {
            MemoryStream memoryStream = new MemoryStream();
            img.Save(memoryStream, ImageFormat.Bmp);
            return memoryStream.ToArray();
        }

        private Image arrayToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            return Image.FromStream(ms);
        }
        #endregion


        //Сумма компонентов цвета
        private static int sumColor(Color cl)
        {
            return cl.R + cl.G + cl.B;
        }

        //Сравнение цветов 
        /// <summary>
        /// КОмпаратор по умолчанию
        /// </summary>
        private class Comparer : IComparer<Color>
        {
            int IComparer<Color>.Compare(Color x, Color y)
            {
                int SumX = sumColor(x);
                int SumY = sumColor(y);

                if (SumX < SumY) return -1;
                if (SumX > SumY) return 1;

                return 0;
            }
        }

        //Среднее значение пикселей 
        private Color avgColor(List<Color> buf)
        {
            buf.Sort(this.comp);
            return buf[buf.Count / 2];
        }

        private byte[] getMass(byte[] buf, int start)
        {
            return new byte[3] { buf[start], buf[start - 1], buf[start - 2] };
        }

        //Получение итогового значения пикселя
        private Color getColor(int x, byte[] img)
        {
            int step = this.windowSize / 2;
            int value = 0;

            byte[] bytebuf;
            List<Color> buf = new List<Color>();

            for (int i = 0; i < this.windowSize; i++)
                for (int j = 0; j < this.windowSize; j++)
                {
                    value = x + this.width * (step - i) + (step - j) * 3;

                    if (value >= bmp_info && value < img.Length)
                    {
                        bytebuf = getMass(img, value);
                        buf.Add(Color.FromArgb(bytebuf[0], bytebuf[1], bytebuf[2]));
                    }
                }

            if (buf.Count > 0) return calcColor(buf);
            return Color.FromArgb(0, 0, 0);
        }

        //Процедура фильтрации
        public void Filter(int start, int end, byte[] img)
        {
            Color cl;

            for (int i = end; i >= start; i -= 3)
            {
                cl = getColor(i, img);
                this.output[i] = cl.R;
                this.output[i - 1] = cl.G;
                this.output[i - 2] = cl.B;
               // Thread.Sleep(1);
            }
        }


        private void ActionFilter(object interval)
        {
            Point _int = (Point)interval;
            Filter(_int.X, _int.Y, this.input);
        }

        public void PFilter(List<Point> intervals)
        {
            Thread[] threads = new Thread[intervals.Count];
            for(int i = 0 ; i<threads.Length; i++)
            {
                threads[i] = new Thread(this.ActionFilter);
                threads[i].Start(intervals[i]);
            
            }
            for(int i = 0; i<threads.Length ; i++)
                threads[i].Join();

        }

    }
}

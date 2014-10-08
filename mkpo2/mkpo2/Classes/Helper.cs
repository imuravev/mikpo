using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MiKPO_2.Classes
{
    public static class Helper
    {
        /// <summary>
        /// Разбивает линейный массив на интервалы
        /// </summary>
        /// <param name="countIntervals"> Число интервалов</param>
        /// <param name="byteOnPixel"> Байт на пиксель</param>
        /// <param name="len">Длина массива</param>
        /// <param name="offset"> Смещение относительно начала</param>
        /// <returns></returns>
        public static List<Point> getIntervals(int countIntervals,int byteOnPixel, int len, int offset = 0)
        {
            List<Point> intervals = new List<Point>();

            int pixels = (len - offset) / byteOnPixel;

            int intervalSize = pixels / countIntervals;
            int addTolastInterval = pixels - (intervalSize * countIntervals);
            for (int i = 0; i < countIntervals - 1; i++)
            {
                intervals.Add(new Point() { X = MedianFilter.bmp_info + i * byteOnPixel * intervalSize, Y = MedianFilter.bmp_info + (i + 1) * byteOnPixel * intervalSize - 1 });
            }

            intervals.Add(new Point() { X = MedianFilter.bmp_info + (countIntervals - 1) * byteOnPixel * intervalSize, Y = MedianFilter.bmp_info + (countIntervals) * byteOnPixel * (intervalSize) + byteOnPixel * addTolastInterval - 1 });

            return intervals;
        }
    }
}

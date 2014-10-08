using MiKPO_2.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiKPO_2
{
    class Program
    {

        static int ThreadCount = 8;
        static int byteOnPixel = 3;
        static int windowSize = 10;

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2) throw new Exception("Недостаточно аргументов!");
                MedianFilter M = new MedianFilter(windowSize);
                M.Load(args[0]);
                List<Point> intervals = Helper.getIntervals(ThreadCount, byteOnPixel, M.length, MedianFilter.bmp_info);
                M.PFilter(intervals);
                M.Save(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("************APP EXCEPTION**********");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

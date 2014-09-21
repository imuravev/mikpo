using mikpo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikpo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2) throw new Exception("Недостаточно аргументов!");
                TriangleInfo.Processing(args[0], args[1]);
               
            }
            catch(Exception ex)
            {
                Console.WriteLine("************APP EXCEPTION**********");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

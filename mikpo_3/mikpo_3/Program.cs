using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mikpo_3.Classes;

namespace mikpo_3
{
    class Program
    {
        static void Main(string[] args)
        {
                QueuingSystem qs = new QueuingSystem(@"C:\Users\User\Documents\Visual Studio 2013\Projects\mikpo_3\mikpo_3"+@"\settings.ini");
                qs.Start();
                Console.WriteLine(qs.CountNonServedObjects);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filestr = File.ReadAllText(@"input.txt");
            decimal r = filestr.Sum( ch => (ch == '(' ? 1 : -1));
            Console.WriteLine("result day1.1 = {0}.", r);
            
            r = 0;
            int i=0;
            for (i = 0; i < filestr.Length; i++)
			{
                
			    r += (filestr[i]=='(' ? 1 : -1 );
                if (r == -1)
                    break;
			}
            Console.WriteLine( "result day1.2 = {0}.\n", i+1);
            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }
    }
}

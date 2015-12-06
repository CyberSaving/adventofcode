using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"(\d+)x(\d+)x(\d+)");
                
            long areatot =0;
            long arearibbon = 0;

            foreach (string l in filelns)
            {
                var dims = r.Match(l);
                int[] dimint = new int[] {int.Parse(dims.Groups[1].Value) , int.Parse(dims.Groups[2].Value),int.Parse(dims.Groups[3].Value)};
                int[] areas = new int[] {dimint[0] *dimint[1],dimint[1] *dimint[2],dimint[0] *dimint[2]};
                areatot += (areas.Sum() * 2 + areas.Min());
                arearibbon += (dimint.Sum() - dimint.Max()) * 2 + (dimint[0] * dimint[1] * dimint[2]);
            }
            Console.WriteLine("result day2.1 = {0}.\n", areatot);
            Console.WriteLine("result day2.2 = {0}.\n", arearibbon);

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }
    }
}

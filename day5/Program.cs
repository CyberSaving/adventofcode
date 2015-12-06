using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex reg1 = new Regex(@"(\w{2})\w*\1");
            Regex reg2 = new Regex(@"(\w)\w\1");

            int goodstring =0;
            int goodstring2=0;
            
            foreach (string l in filelns)
            {
                int hmvowels = 0;
                bool hasdouble = false;
                bool hasfalsestr = false;
                char cprev = '\0';
                for (int i = 0; i < l.Length; i++)
                {
                    char ccur = l[i];
                    if ("aeiou".IndexOf(ccur) >= 0)
                        hmvowels++;

                    if (cprev == ccur)
                        hasdouble = true;
                    if ((cprev == (char)(ccur - 1)) && "acpx".IndexOf(cprev) >= 0)
                        hasfalsestr = true;

                    cprev = ccur;
                }
                if (!hasfalsestr && hasdouble && hmvowels >= 3)
                    goodstring++;
                if (reg1.IsMatch(l) && reg2.IsMatch(l))
                    goodstring2++;
            }
            Console.WriteLine("result day5.1 = {0}.\n", goodstring);
            Console.WriteLine("result day5.2 = {0}.\n", goodstring2);

            Console.Write("Presse eny key ...");
            Console.ReadKey();

        }
    }
}

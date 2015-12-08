using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filelns = File.ReadAllLines(@"input.txt");

            long allCodeChars = 0;
            long allExpancedChars = 0;

            foreach (string line in filelns)
            {
                short codeChars = 2;
                short memoryChars = 0;
                short expancedChars = 4;


                for (int i = 1; i < line.Length-1; i++)
                {
                    char c = line[i];
                    if (c == '\\')
                    {
                        expancedChars++;
                        codeChars++;
                        expancedChars += (short)((line[i + 1] == 'x') ? 0 : 1);
                        i += (line[i + 1] == 'x') ? 3 : 1;
                    }
                    memoryChars++;
                }
                allCodeChars += (line.Length - memoryChars);
                allExpancedChars += expancedChars; 
            }

            Console.WriteLine("result day8.1 = {0}.\n", allCodeChars);
            Console.WriteLine("result day8.2 = {0}.\n", allExpancedChars);

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }
    }
}

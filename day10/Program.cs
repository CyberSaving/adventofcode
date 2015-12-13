using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            const string input = "3113322113";
            string result=input;
            for (int i = 0; i < 40; i++)
                result = lookAndSay(result);
            string result1 = result;

            for (int i = 0; i < 10; i++)
                result = lookAndSay(result);



            Console.WriteLine("result day10.1 = {0}.\n", result1.Length);
            Console.WriteLine("result day10.2 = {0}.\n", result.Length);

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }

        static string lookAndSay(string input)
        {
            StringBuilder sb = new StringBuilder();
            char c_prev = input[0];
            char c;
            short count=0;
            for (int i = 1; i < input.Length; i++)
            {
                c = input[i];
                if (c != c_prev)
                {
                    sb.Append(count + 1).Append(c_prev);
                    count = 0;
                    c_prev = c;
                }
                else
                    count++;
            }
            return sb.Append(count + 1).Append(c_prev).ToString();
        }
    }
}

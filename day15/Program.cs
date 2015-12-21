using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filelns = File.ReadAllLines(@"input.txt");
            //Frosting: capacity 4, durability -2, flavor 0, texture 0, calories 5
            Regex r = new Regex(@"(\w+): capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)");

            List<int[]> comp = new List<int[]>(filelns.Length);
            foreach (string line in filelns)
            {
                var mc = r.Match(line);
                int capacity = int.Parse(mc.Groups[2].Value);
                int durability = int.Parse(mc.Groups[3].Value);
                int flavor = int.Parse(mc.Groups[4].Value);
                int texture = int.Parse(mc.Groups[5].Value);
                int calories = int.Parse(mc.Groups[6].Value);
                comp.Add(new int[] { capacity, durability, flavor, texture, calories });
            }

            //foreach (var per in getValues(3))
            //    Console.WriteLine("{0} {1} {2}", per.Cast<Object>().ToArray()); 

            long maxvalue = 0;
            long maxvalueless500 = 0;

            foreach (int[] k in getValues(4))
            {
                long[] sum = new long[5];
                for (int i = 0; i < comp.Count; i++)
                    for (int j = 0; j < 5; j++)
                        sum[j] += comp[i][j] * k[i];
                
                    
                long total = sum.Take(4).Aggregate(1, (a, b) => (int)(a * ((b < 0) ? 0 : b)));
                maxvalue = Math.Max(total, maxvalue);
                if(sum[4]==500)
                    maxvalueless500 = Math.Max(total, maxvalueless500);
            }


            Console.WriteLine("result day15.1 = {0}.", maxvalue);
            Console.WriteLine("result day15.2 = {0}.", maxvalueless500);
            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }


        static IEnumerable<int[]>
            getValues(int length)
        {
            int[] leng = new int[length];
            leng[0] = 1;
            int p = 0;
            while (p >= 0)
            {
                for (int t=p+1; t < length - 1; t++) leng[t] = 1;

                leng[length-1]=0; 
                int s = leng.Sum();
                if (s < 100)
                {
                    leng[length - 1] = 100 - s;
                    yield return leng;
                    p = length - 2;
                }
                else
                    p--;

                while (p >= 0 && ++leng[p] > 100 - length + 1) p--;
                
            }

        }
    }
}

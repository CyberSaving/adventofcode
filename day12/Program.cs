using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            string filelns = File.ReadAllText(@"input.txt");
            Regex r = new Regex(@"([-\d]+)(?!"")");
            Regex r_red = new Regex(@":""red""");
            long l1 =0;
            long l2 = 0;


            foreach (Match ms in r.Matches(filelns))
                l1 += long.Parse(ms.Groups[1].Value);

            int t= filelns.Length;
            
            while( (t = filelns.LastIndexOf(@":""red""",t)) >=0){
                
                int bracepre  = findBrance(filelns, t, false);
                int bracepost = findBrance(filelns, t, true);
                if (bracepre >= 0)
                {
                    filelns = filelns.Remove(bracepre, bracepost - bracepre + 1);
                    t = bracepre - 1;
                }
                else
                    break;
            }
            
            foreach (Match ms in r.Matches(filelns))
                l2 += long.Parse(ms.Groups[1].Value);

            Console.WriteLine("result day12.1 = {0}.\n", l1);
            Console.WriteLine("result day12.2 = {0}.\n", l2);

            Console.Write("Presse eny key ...");
            Console.ReadKey();

            
        }

        static int findBrance(string where,int from,bool lookahead)
        {
            int bracecount = 0, p = from;
            while (bracecount != 1)
            {
                if (lookahead) { 
                   p++;
                   if(p>where.Length) return -1;
                }
                else
                {
                    p--;
                    if (p < 0) return -1;
                }
                
                switch (where[p])
                {
                    case '}': bracecount+= (lookahead) ? 1 :-1; break;
                    case '{': bracecount+= (lookahead) ? -1 :1; break;
                }
            }
            return p;
        }
    }
}

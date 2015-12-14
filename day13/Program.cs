using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day13
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"([A-Za-z]+) would (gain|lose) (\d+) happiness units by sitting next to ([A-Za-z]+)\.");

            int last = 0;
            Dictionary<String, int> nameMapper = new Dictionary<string, int>();
            Dictionary<int, Dictionary<int, int>> prevertex
                = new Dictionary<int, Dictionary<int, int>>();
            foreach (string line in filelns)
            {
                var mct = r.Match(line);
                string from = mct.Groups[1].Value;
                bool gain = mct.Groups[2].Value == "gain";
                int value = int.Parse(mct.Groups[3].Value);
                if(!gain) value =-value;
                string to = mct.Groups[4].Value;
                
                if (!nameMapper.ContainsKey(from))
                    nameMapper[from] = (int)last++;
                if (!nameMapper.ContainsKey(to))
                    nameMapper[to] = (int)last++;


                int i_from = nameMapper[from];
                int i_to = nameMapper[to];

                Dictionary<int, int> theVertex = null;
                if (!prevertex.TryGetValue(i_from, out theVertex))
                {
                    theVertex = new Dictionary<int, int>();
                    prevertex[i_from] = theVertex;
                }
                theVertex[i_to] = value;

                //if (!prevertex.TryGetValue(i_to, out theVertex))
                //{
                //    theVertex = new Dictionary<int, int>();
                //    prevertex[i_to] = theVertex;
                //}
                //theVertex[i_from] = value;

            }
            
            int t =0;
            long c = 0;
            foreach (var item in Mat.CircularPermutation(prevertex.Count))
            {
                t++;
                c = Math.Max(c, letHappiness(item.ToArray(),prevertex));
            }
            Console.WriteLine("result day13.1 = {0}. [{1}]\n", c,t);

            int vc = prevertex.Count;
            prevertex[vc] = new Dictionary<int, int>(vc);
            for (int i = 0; i <= vc; i++)
            {
                prevertex[vc][i] = 0;
                prevertex[i][vc] = 0;
            }

            t = 0;
            c = 0;
            foreach (var item in Mat.CircularPermutation(prevertex.Count))
            {
                t++;
                c = Math.Max(c, letHappiness(item.ToArray(), prevertex));
            }
            Console.WriteLine("result day13.2 = {0}. [{1}]\n", c, t);

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }

        private static long letHappiness(int[] ar, Dictionary<int, Dictionary<int, int>> prevertex)
        {
            long happiness = prevertex[ar[0]][ar[ar.Length - 1]] + prevertex[ar[ar.Length - 1]][ar[0]];
            for (int i = 1; i < ar.Length; i++)
            {
                happiness += prevertex[ar[i - 1]][ar[i]] + prevertex[ar[i]][ar[i - 1]];
            }
            return happiness;
        }
        
    }

    public static class Mat
    {
        public static IEnumerable<int[]> CircularPermutation(int max)
        {
            bool[] retval = new bool[max];
            int[] mat = new int[max];

            for (int i = 0; i < retval.Length; i++)
            {
                retval[i] = false;
                mat[i] = i;
            }
            yield return mat;
            int[] stringval = new int[max];
            foreach (var item in movegen(retval, mat, mat.Length-1))
                yield return item;
             
        }

        static IEnumerable<int[]> movegen(bool[] rest, int[] mat, int level)
        {
            long c = 1;
            do
            {

                int offset = level;
                if (mat[level] == (mat.Length - 1))
                {
                    offset = level - 1;
                    for (int i_rest = offset + 1; i_rest < mat.Length; i_rest++)
                    {
                        rest[mat[i_rest]] = true;
                    }
                }

                int newval = markOne(rest, mat[offset]);
                rest[mat[offset]] = true;
                mat[offset] = newval;
                rest[newval] = false;

                for (int i_rest = offset + 1; i_rest < mat.Length; i_rest++)
                {
                    newval = markOne(rest);
                    if (newval >= 0)
                        mat[i_rest] = newval;
                    else
                        break;
                }

                if (mat[offset] != (mat.Length - 1))
                    level = mat.Length;
                

                //if (++c % mat.Length != 0)
                    yield return mat;
                level--;
            } while (level > 0);
        }

        public static int markOne(bool[] rest, int min)
        {
            for (int  i = min; i < rest.Length; i++)
            {
                if (rest[i])
                {
                    rest[i] = false;
                    return i;
                }
            }
            return -1;
        }
        public static int markOne(bool[] rest)
        {
            return markOne(rest, 0);
        }
    }
}

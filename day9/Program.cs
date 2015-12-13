using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"(\w+)\sto\s(\w+)\s\=\s(\d+)");

            char last = 'a';

            Dictionary<String, char> nameMapper = new Dictionary<string, char>();
            Dictionary<char, Dictionary<char, int>> prevertex
                = new Dictionary<char, Dictionary<char, int>>();  
            foreach (string line in filelns)
            {
                var mct = r.Match(line);
                string from = mct.Groups[1].Value;
                string to = mct.Groups[2].Value;
                int distance = int.Parse(mct.Groups[3].Value);

                if (!nameMapper.ContainsKey(from))
                    nameMapper[from] = (char)((int)last++);
                if (!nameMapper.ContainsKey(to))
                    nameMapper[to] = (char)((int)last++);

                char c_from = nameMapper[from];
                char c_to = nameMapper[to];

                Dictionary<char, int> theVertex=null;
                if (!prevertex.TryGetValue(c_from, out theVertex))
                {
                    theVertex = new Dictionary<char, int>();
                    prevertex[c_from] = theVertex;
                }
                theVertex[c_to] = distance;

                if (!prevertex.TryGetValue(c_to, out theVertex))
                {
                    theVertex = new Dictionary<char, int>();
                    prevertex[c_to] = theVertex;
                }
                theVertex[c_from] = distance;

            }

            long minpath = long.MaxValue;
            long maxpath = long.MinValue;

            char[] bestminpath = null;
            char[] bestmaxpath = null;
            
            foreach (char[] vtx in generator(prevertex.Count))
            {
                long path =0;
                for (int i = 1; i < vtx.Length; i++)
			    {
                    path += prevertex[vtx[i - 1]][vtx[i]];
			    }
                if (path < minpath)
                {
                    bestminpath = (char[])vtx.Clone();
                    minpath = path;  
                }
                if (path > maxpath){
                    bestmaxpath = (char[])vtx.Clone();
                    maxpath = path;  
                }
            }

            Func<char[], string> _tostring = new Func<char[], string>(pathc =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(nameMapper.First(v => v.Value == pathc[0]).Key);
                for (int i = 1; i < pathc.Length; i++)
                {
                    char char_city = pathc[i];

                    sb.Append(" -").Append(prevertex[pathc[i - 1]][char_city]).Append("-> ").
                    Append(nameMapper.First(v => v.Value == char_city).Key);
                }
                return sb.ToString();
            });

            Console.WriteLine("result day9.1 = {0}. [{1}]\n", minpath, _tostring(bestminpath));
            Console.WriteLine("result day9.2 = {0}. [{1}]\n", maxpath, _tostring(bestmaxpath));

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }



        static IEnumerable<char[]> generator(int max)
        {
            bool[] retval = new bool[max];
            short[] mat = new short[max];
            for (int i = 0; i < max; i++) retval[i] = true;

            char[] stringval = new char[max];
            foreach (var item in movegen(retval, mat, 0))
            {
                for (int i = 0; i < item.Length; i++)
                {
                    stringval[i] = (char)('a' + item[i]);
                }
                yield return stringval;
            } 
        }

        static IEnumerable<short[]> movegen(bool[] rest, short[] mat, int level)
        {
            if (level >= mat.Length){
                yield return mat;
            } else{
                for (short i = 0; i < rest.Length; i++)
			    {
                    if (rest[i])
                    {
                        mat[level] = i;
                        if (level < mat.Length - 1)
                        {
                            rest[i] = false;
                            foreach (var item in movegen(rest, mat, level + 1))
                                yield return mat;
                            rest[i] = true;
                        }
                        else
                            yield return mat;
                    }
                    
			    }

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"(turn off|turn on|toggle) (\d+),(\d+) through (\d+),(\d+)");

            byte[,] lights = new byte[1000, 1000];
            short[,] lightsbr = new short[1000, 1000];



            int count = 0;
            long countbr = 0;
            foreach (string l in filelns)
            {
                var ms =r.Match(l);
                int fromx = int.Parse(ms.Groups[2].Value);
                int fromy = int.Parse(ms.Groups[3].Value);

                int tox = int.Parse(ms.Groups[4].Value);
                int toy = int.Parse(ms.Groups[5].Value);

                int action = (ms.Groups[1].Value=="turn off") ? 0 : (ms.Groups[1].Value=="toggle") ? -1 : 1;


                for (int x = fromx; x <= tox; x++)
                    for (int y = fromy; y <= toy; y++){
                        if (action < 0){
                            
                            //for test 1
                            if(lights[x, y]==1){
                                count --;
                                lights[x, y] =0;  
                            }else{
                                count ++;
                                lights[x, y] =1;
                            }
                            //for test 2

                            lightsbr[x, y] += 2;
                            countbr += 2;
                        }
                        else
                        {
                            //for test 1
                            if (lights[x, y] != action){
                                count += (action * 2) - 1;
                                lights[x, y] = (byte)action;
                            }

                            //for test 2
                            if (action==0){
                                if (lightsbr[x, y] > 0)
                                {
                                    lightsbr[x, y]--;
                                    countbr--;
                                }
                            }else{
                                lightsbr[x, y]++;
                                countbr ++;
                            }
                        }
                            
                    }

            }
            Console.WriteLine("result day6.1 = {0}.\n", count);
            Console.WriteLine("result day6.2 = {0}.\n", countbr);
            
            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }
    }
}

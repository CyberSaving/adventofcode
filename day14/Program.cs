using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            const int last_time= 2503;
            //Dancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.
            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds");

            string max_name = "";
            int max_kilometrs = 0;

            
            List<Reindeer> allReindeer = new List<Reindeer>();

            foreach (string line in filelns)
	        {
    	        var m = r.Match(line);
                string who = m.Groups[1].Value;
                int fly_v= int.Parse(m.Groups[2].Value);
                int fly_t = int.Parse(m.Groups[3].Value);
                int pause = int.Parse(m.Groups[4].Value);

                var reindeer = new Reindeer() { name = who,fly_v = fly_v, fly_t = fly_t, pause = pause };
                allReindeer.Add(reindeer);

                int kilometers = reindeer.getKilometersFromTime(last_time);
                if (kilometers > max_kilometrs)
                {
                    max_name = who;
                    max_kilometrs = kilometers;
                }
	        }

            for (int i = 1; i <= last_time; i++)
            {
                int klm=0;
                foreach (var ReindeerItem in allReindeer){
                    ReindeerItem.setKilometersFromTime(i);
                    klm = Math.Max(klm,ReindeerItem.kilometes);
                }
                allReindeer.FindAll(v => v.kilometes == klm).ForEach(v => v.points++);
            }
            
            List<Reindeer> winners = allReindeer.OrderByDescending(v => v.points).ToList();
            foreach (var ReindeerItem in winners) 
                Console.WriteLine("{0}. [{1}]", ReindeerItem.name, ReindeerItem.points);


            Console.WriteLine("result day14.1 = {0}. [{1}]", max_name, max_kilometrs);
            Console.WriteLine("result day14.2 = {0}. [{1}]", winners[0].name, winners[0].points);    
            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }
    }

    class Reindeer
    {
        public int fly_v;
        public int fly_t;
        public int pause;
        public string name;
        public int kilometes;
        public int points;
        
        public void setKilometersFromTime(int time){
           kilometes = getKilometersFromTime(time);
        }
        public int getKilometersFromTime(int time)
        {
            int t_missed = time % (fly_t + pause);
            int kilometers = (time / (fly_t + pause)) * fly_t * fly_v;
            kilometers += ((t_missed >= fly_t) ? fly_t : t_missed) * fly_v;
            return kilometers;
        }
    }
}

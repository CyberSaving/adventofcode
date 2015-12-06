using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3
{
    class Program
    {
        class house : IComparable<house>, IEqualityComparer<house>
        {
            public int x = 0;
            public int y = 0;

            public int CompareTo(house other)
            {
                if (other.x == this.x)
                    return other.y.CompareTo(this.y);
                else
                    return other.x.CompareTo(this.x);
            }

            public bool Equals(house from, house to)
            {
                return (from.x == to.x) && (from.y == to.y);
                    
            }

            public int GetHashCode(house obj)
            {
                return (x.ToString() + "." + y.ToString()).GetHashCode();
            }

            public long GetLongHashCode()
            {
                var A = (ulong)(x >= 0 ? 2 * (long)x : -2 * (long)x - 1);
                var B = (ulong)(y >= 0 ? 2 * (long)y : -2 * (long)y - 1);
                var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
                return x < 0 && y < 0 || x >= 0 && y >= 0 ? C : -C - 1;
            }
        } 

        static void Main(string[] args)
        {
            string filestr = File.ReadAllText(@"input.txt");

            
            house currentHouse = new house();
            
            house roboSantaCurrentHouse = new house();
            house santaCurrentHouse = new house();

            house currentHouse2;
            HashSet<long> _allHouses = new HashSet<long>();
            HashSet<long> _allHouses2 = new HashSet<long>();
            

            int i = 0;
            for (i = 0; i < filestr.Length; i++)
            {
                currentHouse2 = (i % 2 == 1)? roboSantaCurrentHouse : santaCurrentHouse;
                switch (filestr[i])
                {
                    case '<':
                        currentHouse.x--;
                        currentHouse2.x--;
                        break;
                    case '^':
                        currentHouse.y++;
                        currentHouse2.y++;
                        break;
                    case '>':
                        currentHouse.x++;
                        currentHouse2.x++;
                        break;
                    case 'v':
                        currentHouse.y--;
                        currentHouse2.y--;
                        break;
                }

                _allHouses.Add(currentHouse.GetLongHashCode());
                _allHouses2.Add(currentHouse2.GetLongHashCode());

            }
            Console.WriteLine("result day3.1 = {0}.\n", _allHouses.Count());
            Console.WriteLine("result day3.2 = {0}.\n", _allHouses2.Count());

            Console.Write("Presse eny key ...");
            Console.ReadKey();

        }
    }
}

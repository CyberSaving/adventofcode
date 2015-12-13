using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day11
{
    class Program
    {
        static void Main(string[] args)
        {
            const string input = "hxbxwxba";

            char[] password = input.ToCharArray();
            getNextPassword(password);
            string solution1 = new string(password);
            getNextPassword(password);
            string solution2 = new string(password);
            

            Console.WriteLine("result day11.1 = {0}.\n", solution1);
            Console.WriteLine("result day11.2 = {0}.\n", solution2);

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }

        static void getNextPassword(char[] password){
            bool cond1 = false;
            bool cond2 = false;
            int overlc = 0;
            do
            {
                short p = (short)(password.Length - 1);
                while (p >= 0 && isInc(password, p)) p--;

                cond1 = false;
                cond2 = false;
                int overl = 0; overlc = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    cond1 |= (i > 1 && password[i - 2] + 1 == password[i - 1] && password[i - 1] == password[i] - 1);
                    cond2 |= ("iol".IndexOf(password[i]) >= 0);

                    if (i > overl && password[i - 1] == password[i])
                    {
                        overlc++;
                        overl = i + 1;
                    }
                }

            } while (!(cond1 && !cond2 && overlc > 1));
        }

        static bool isInc(char[] str, short pos)
        {
            if (pos < 0)
                return true;

            if (str[pos] == 'z')
            {
                str[pos] = 'a';
                return true;
            }
            else
            {
                str[pos]++;
            }
            return false;
        }

    }

}

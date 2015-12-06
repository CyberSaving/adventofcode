using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            const string secret = "ckczppom";

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            long t=0;
            byte[] hash;

            long step1 =0;
            string hs1 = "";

            long step2 = 0;
            string hs2 = "";
            StringBuilder sb; 
            do
            {
                t++;
                string secrettotest = secret + t.ToString();
                byte[] secretbyte = System.Text.Encoding.ASCII.GetBytes(secrettotest);
                hash = md5.ComputeHash(secretbyte);

                if (step1 == 0 && hash[0] == 0 && hash[1] == 0 && hash[2] <= 0x0F)
                {
                    step1 = t;
                    sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                        sb.Append(hash[i].ToString("X2"));
                    hs1 = sb.ToString();
                }
                if (step2 == 0 && hash[0] == 0 && hash[1] == 0 && hash[2] == 0x00)
                {
                    step2 = t;
                    sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                        sb.Append(hash[i].ToString("X2"));
                    hs2 = sb.ToString();
                }

            } while (step1 == 0 || step2 ==0);



            Console.WriteLine("result day4.1 = {0}. [{1}]\n", step1, hs1);
            Console.WriteLine("result day4.2 = {0}. [{1}]\n", step2, hs2);

            
            Console.Write("Presse eny key ...");
            Console.ReadKey();

        }
    }
}

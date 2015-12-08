using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            bestForPart1();
            bestForPart1and2();

        }

        /// <summary>This is the best solution for the part one but not for the part 2!</summary>
        static void bestForPart1()
        {
            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"(([\da-z]+)\s)?(AND|LSHIFT|RSHIFT|NOT|OR)?\s?([\da-z]+) -> ([a-z]+)");


            Dictionary<string, ushort> signals = new Dictionary<string, ushort>();
            LinkedList<string> stack = new LinkedList<string>();
            int fileline = 0;
            string l = filelns[0];
            do
            {
                var mct = r.Match(l);

                string signal = mct.Groups[5].Value;
                string right = mct.Groups[4].Value;
                string operation = mct.Groups[3].Value;
                string left = mct.Groups[2].Value;

                ushort bufferl;
                ushort bufferr;


                bool reprocess = false;
                LinkedListNode<string> node = null;
                if (!evalOrDie(signals, left, right, out bufferl, out bufferr))
                {
                    stack.AddLast(l);
                }
                else
                {
                    if (string.IsNullOrEmpty(operation))
                        signals[signal] = bufferr;
                    else if (operation == "NOT")
                        signals[signal] = (ushort)~bufferr;
                    else if (operation == "AND")
                        signals[signal] = (ushort)(bufferl & bufferr);
                    else if (operation == "OR")
                        signals[signal] = (ushort)(bufferl | bufferr);
                    else if (operation == "LSHIFT")
                        signals[signal] = (ushort)(bufferl << bufferr);
                    else if (operation == "RSHIFT")
                        signals[signal] = (ushort)(bufferl >> bufferr);

                    node = stack.First;
                    while (node != null && !(reprocess = Regex.IsMatch(node.Value, @"\b" + signal + @"\b")))
                        node = node.Next;

                }

                if (reprocess)
                {
                    l = node.Value;
                    stack.Remove(node);
                }
                else if (fileline + 1 < filelns.Length)
                {
                    l = filelns[++fileline];
                }
                else if (stack.Count > 0)
                {
                    l = stack.First();
                    stack.RemoveFirst();
                }
                else
                    l = null;

            } while (l != null);
            Console.WriteLine("result day7.1 = {0}.\n", signals["a"]);

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }
        
        static void bestForPart1and2()
        {
            string[] filelns = File.ReadAllLines(@"input.txt");
            Regex r = new Regex(@"(([\da-z]+)\s)?(AND|LSHIFT|RSHIFT|NOT|OR)?\s?([\da-z]+) -> ([a-z]+)");

            Dictionary<string, LogicUnit> signals = new Dictionary<string, LogicUnit>(filelns.Length);
            
            foreach (var l in filelns)
	        {
                var mct = r.Match(l);

                string signal = mct.Groups[5].Value;
                string right = mct.Groups[4].Value;
                string operation = mct.Groups[3].Value;
                string left = mct.Groups[2].Value;

                signals[signal] = new LogicUnit() { left = left, right = right, operation = operation };
	        }
            ushort a_value = signals["a"].Set(signals);
            Console.WriteLine("result day7.1 = {0}.\n", a_value);

            foreach (LogicUnit signal in signals.Values)
                signal.Reset(signals);

            signals["b"] = new LogicUnit() { left = "", right = a_value.ToString(), operation = "" };
            Console.WriteLine("result day7.2 = {0}.\n", signals["a"].Set(signals));

            Console.Write("Presse eny key ...");
            Console.ReadKey();
        }

        static bool evalOrDie(Dictionary<string, ushort> signals, string value1, string value2, out ushort retval1, out ushort retval2)
        {
            retval1 = ushort.MinValue;
            retval2 = ushort.MinValue;
            if (!string.IsNullOrEmpty(value1))
            {
                if(!eval(signals,value1,out retval1))
                    return false;
            }

            if (!string.IsNullOrEmpty(value2))
            {
                if (!eval(signals, value2, out retval2))
                    return false;
            }
            return true;
        }

        static bool eval(Dictionary<string, ushort> signals, string value, out ushort retval)
        {
            if (!ushort.TryParse(value, out retval))
                return (signals.TryGetValue(value, out retval));
            return true;
        }
    }
}

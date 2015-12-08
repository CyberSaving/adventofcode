using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day7
{
    class LogicUnit
    {
        public string operation;
        public string left;
        public string right;
        private ushort? value=null;

        public void Reset(Dictionary<string, LogicUnit> logicMapper)
        {
            value = null;
        }
        public ushort Set(Dictionary<string, LogicUnit> logicMapper)
        {

            if (value != null)
                return value.Value;
            
            ushort lvalue, rvalue;

            lvalue = ushort.MinValue;
            rvalue = ushort.MinValue;
            if (!string.IsNullOrEmpty(left))
            {
                if (!ushort.TryParse(left, out lvalue))
                    lvalue = logicMapper[left].Set(logicMapper);
            }
            if (!string.IsNullOrEmpty(right))
            {
                if (!ushort.TryParse(right, out rvalue))
                    rvalue = logicMapper[right].Set(logicMapper);
            }


            if (string.IsNullOrEmpty(operation))
                value = (ushort)rvalue;
            else if (operation == "NOT")
                value =(ushort)~rvalue;
            else if (operation == "AND")
                value = (ushort)(lvalue & rvalue);
            else if (operation == "OR")
                value = (ushort)(lvalue | rvalue);
            else if (operation == "LSHIFT")
                value = (ushort)(lvalue << rvalue);
            else if (operation == "RSHIFT")
                value = (ushort)(lvalue >> rvalue);
            if(value==null)
                 throw new Exception("Invalid Operation");
            
            return (ushort)value;
           
        }
    }
}

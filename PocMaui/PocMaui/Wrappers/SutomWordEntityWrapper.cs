using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.Wrappers
{
    public class SutomWordEntityWrapper
    {
        public char Value { get; set; }
        public int Status { get; set; } // 0 => Grey / 1 => Yellow / 2 => Red

        public SutomWordEntityWrapper()
        {
        }

        public SutomWordEntityWrapper(char value)
        {
            Value = value;
        }
    }
}

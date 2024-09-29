using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace
{
    internal record Range(int Min, int Max)
    {
        public bool IsInRange(int value)
        {
            return value >= Min && value <= Max;
        }
    }
}

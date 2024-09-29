using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Extensions
{
    internal static class DiceRollerExtensions
    {
        public static int Roll3d(this DiceRoller roller)
        {
            return roller.Roll(3, 6);
        }

        public static int Roll3d(this DiceRoller roller, int modifier)
        {
            return roller.Roll(3, 6, modifier);
        }
    }
}

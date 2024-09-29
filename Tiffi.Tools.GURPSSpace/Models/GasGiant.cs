using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A giant world made out of gas without a surface
    /// Non-habitable
    /// </summary>
    public record GasGiant : World
    {
        /// <summary>
        /// Size of the gas giant
        /// </summary>
        public required GasGiantSize GasGiantSize { get; init; }
    }
}

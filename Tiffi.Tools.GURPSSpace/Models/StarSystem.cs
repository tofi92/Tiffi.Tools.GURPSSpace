using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A star system
    /// </summary>
    public record StarSystem
    {
        public required IEnumerable<Star> Stars { get; init; }

        public Star PrimaryStar => Stars.First(x => x.IsPrimary);

        public required IEnumerable<World> Worlds { get; init; }

        /// <summary>
        /// The seed that was used to create this system
        /// </summary>
        public required int Seed { get; init; }

        ///// <summary>
        ///// The inner limit in km where planets can be formed
        ///// </summary>
        //public double InnerLimitRadius => PrimaryStar.InnerLimitRadius;

        ///// <summary>
        ///// The outer limit in km where planets can be formed
        ///// </summary>
        //public double OuterLimitRadius => PrimaryStar.OuterLimitRadius;

        ///// <summary>
        ///// The limit in km where water ice could exist
        ///// </summary>
        //public double SnowLine => PrimaryStar.SnowLine;
    }
}

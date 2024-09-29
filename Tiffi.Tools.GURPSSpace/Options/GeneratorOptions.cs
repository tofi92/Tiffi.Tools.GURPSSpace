using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Options
{
    /// <summary>
    /// Some options for the generator
    /// </summary>
    public record GeneratorOptions
    {
        /// <summary>
        /// The seed of the generated system
        /// Using the same seed will result in the same results
        /// Defaults to zero which will result in a random seed
        /// </summary>
        /// <value>Default: 0</value>
        public int Seed { get; set; } = 0;

        /// <summary>
        /// Generate multiple stars
        /// Multiple stars are pretty uncommon, but possible
        /// </summary>
        /// <value>Default: false</value>
        public bool MultipleStars { get; set; } = false;

        /// <summary>
        /// Force a planet that is habitable, even if the generator does not generate one
        /// </summary>
        /// <remarks>
        /// Habitable does not mean the planet is friendly (e.g. could be radiated due to the lack of a magnetic field)
        /// </remarks>
        public bool ForceHabitablePlanet { get; set; } = false;

        /// <summary>
        /// The first gas giant can be generated in a forbidden zone of a multi-star system.
        /// If this is true, the gas giant will be created nevertheless. Note that this planet should be treated as unstable.
        /// </summary>
        public bool AllowFirstGasGiantInForbiddenZone { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// How a world (e.g. a planet or a moon) is roughly classified, not including gas giants
    /// </summary>
    public enum WorldClassifier
    {
        /// <summary>
        /// A world with an atmosphere and water, but so cold that all water is frozen
        /// </summary>
        Ice,

        /// <summary>
        /// A world that was formed with tremendous amount of volcanic activity (e.g. Io)
        /// </summary>
        Sulfur,

        /// <summary>
        /// A world that is too small to retain an atmosphere and is composed of mostly naked rock (e.g. Mercury, Earths moon)
        /// </summary>
        Rock,

        /// <summary>
        /// A proto-world which is large enough to retain an atmosphere but is too cold (e.g. Triton)
        /// </summary>
        Hadean,

        /// <summary>
        /// A world with a thick athmosphere of ammonia. It is too cold to contain Earthlike life.
        /// </summary>
        Ammonia,

        /// <summary>
        /// A world which consists only of a large ocean or with only small patches of land that do not support life
        /// </summary>
        Ocean,

        /// <summary>
        /// An Earthlike world with water, life and a human breathable atmosphere
        /// </summary>
        Garden,

        /// <summary>
        /// A former Earthlike world that experienced a sudden burst in greenhouse emissions and is now too warm to retain most life (e.g. Venus)
        /// </summary>
        Greenhouse,

        /// <summary>
        /// A world that would be large enough to retain an atmosphere but is too close to the primary star so all atmosphere was stripped by the stellar winds
        /// </summary>
        Chthonian,

        /// <summary>
        /// Used for asteroid belts
        /// </summary>
        AsteroidBelt
    }
}

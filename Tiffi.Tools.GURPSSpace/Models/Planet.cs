using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A planet in a star system
    /// </summary>
    public record Planet : World
    {

        /// <summary>
        /// World classification
        /// </summary>
        public WorldClassifier Classifier { get; set; }

        /// <summary>
        /// Atmospheric mass in earth atmospheric masses
        /// </summary>
        public double AtmosphericMass { get; set; }

        /// <summary>
        /// Atmospheric mass in kg
        /// </summary>
        public double AtmosphericMassInKg => AtmosphericMass * Constants.EARTH_ATMOSPHERIC_MASS;

        /// <summary>
        /// Water coverage in percent (0 = 0%, 1 = 100%)
        /// </summary>
        public double WaterCoverage { get; set; }

        /// <summary>
        /// Average surface temperature in Kelvin
        /// </summary>
        public double AverageSurfaceTemperature { get; set; }

        /// <summary>
        /// Black body temperature in Kelvin
        /// </summary>
        public double BlackBodyTemperature { get; set; }

        /// <summary>
        /// Gravity in earth gravity
        /// </summary>
        public double Gravity { get; set; }

        /// <summary>
        /// Gravity in m/s²
        /// </summary>
        public double GravityInMetersPerSecondSquared => Gravity * Constants.EARTH_GRAVITY;

        /// <summary>
        /// Pressure in atmospheres
        /// </summary>
        public double Pressure { get; set; }

        /// <summary>
        /// Pressure in millibars
        /// </summary>
        public double PressureInMillibars => Pressure * Constants.EARTH_SURF_PRES_IN_MILLIBARS;

        /// <summary>
        /// How many resources this planet contains
        /// </summary>
        public OverallResourceValue OverallResourceValue { get; set; }

    }
}

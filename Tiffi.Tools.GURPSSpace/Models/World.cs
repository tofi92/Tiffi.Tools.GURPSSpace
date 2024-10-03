using System.Text.Json.Serialization;

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A base for all worlds, e.g. terrestrial planets or gas giants
    /// </summary>
    [JsonDerivedType(typeof(Planet))]
    [JsonDerivedType(typeof(GasGiant))]
    public abstract record World
    {
        /// <summary>
        /// Size of the world
        /// </summary>
        public required WorldSize Size { get; set; }


        /// <summary>
        /// The orbital radius in AU
        /// </summary>
        public double OrbitalRadius { get; set; }

        /// <summary>
        /// The orbital radius in km
        /// </summary>
        public double OrbitalRadiusInKm => OrbitalRadius * Constants.KM_PER_AU;

        /// <summary>
        /// Density in earth density
        /// </summary>
        public required double Density { get; set; }

        /// <summary>
        /// Density in g/cm³
        /// </summary>
        public double DensityInGramsPerCubicCentimeter => Density * Constants.EARTH_DENSITY;

        /// <summary>
        /// Mass in earth masses
        /// </summary>
        public required double Mass { get; set; }

        /// <summary>
        /// Mass in kg
        /// </summary>
        public double MassInKg => Mass * Constants.EARTH_MASS_IN_GRAMS / 1000;

        /// <summary>
        /// Diameter of the world in Earth diameters
        /// </summary>
        public required double Diameter { get; set; }

        /// <summary>
        /// Diamter in km
        /// </summary>
        public double DiameterInKm => Diameter * Constants.KM_EARTH_DIAMETER;

        /// <summary>
        /// Orbital Period in hours around it's primary star
        /// </summary>
        public required double OrbitalPeriod { get; set; }

        /// <summary>
        /// The worlds orbit eccentricity
        /// </summary>
        public required double Eccentricity { get; set; }

        /// <summary>
        /// Apoapsis of the world from it's primary star in AU
        /// </summary>
        /// <remarks>
        /// This is the furthest point from it's star
        /// </remarks>
        public required double Apoapsis { get; set; }

        /// <summary>
        /// Periapsis of the world from it's primary star in AU
        /// </summary>
        /// <remarks>
        /// This is the closest point to it's star
        /// </remarks>
        public required double Periapsis { get; set; }

        /// <summary>
        /// A list of moons of this world
        /// </summary>
        public IEnumerable<Moon> Moons { get; set; } = [];

        /// <summary>
        /// Rotation period in hours
        /// </summary>
        public double RotationPeriod { get; set; }

        /// <summary>
        /// If this moon is tide locked to it's planet
        /// </summary>
        public bool IsTideLocked { get; set; }
    }
}

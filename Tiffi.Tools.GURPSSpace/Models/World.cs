namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A base for all worlds, e.g. terrestrial planets or gas giants
    /// </summary>
    public abstract record World
    {
        /// <summary>
        /// Size of the world
        /// </summary>
        public required WorldSize Size { get; set; }


        /// <summary>
        /// The orbital radius in km
        /// </summary>
        public double OrbitalRadius { get; set; }

        /// <summary>
        /// Density in g/cm³
        /// </summary>
        public required double Density { get; set; }

        /// <summary>
        /// Mass in KG
        /// </summary>
        public required double Mass { get; set; }

        /// <summary>
        /// Diameter of the world in km
        /// </summary>
        public required double Diameter { get; set; }

        /// <summary>
        /// Orbital Period in hours around it's primary star
        /// </summary>
        public required double OrbitalPeriod { get; set; }

        /// <summary>
        /// The worlds orbit eccentricity
        /// </summary>
        public required double Eccentricity { get; set; }

        /// <summary>
        /// Apoapsis of the world from it's primary star in KM
        /// </summary>
        /// <remarks>
        /// This is the furthest point from it's star
        /// </remarks>
        public required double Apoapsis { get; set; }

        /// <summary>
        /// Periapsis of the world from it's primary star in KM
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

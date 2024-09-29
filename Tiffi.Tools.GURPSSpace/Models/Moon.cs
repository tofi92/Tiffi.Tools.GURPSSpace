using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// Represents a moon around a planet
    /// </summary>
    public record Moon
    {
        /// <summary>
        /// The moon size
        /// </summary>
        public MoonSize Size { get; init; }

        [MemberNotNullWhen(true, nameof(WorldSize))]
        private bool _isMajorMoon => Size == MoonSize.Major;

        /// <summary>
        /// Optional world size if MoonSize is MajorMoon
        /// </summary>
        public WorldSize? WorldSize { get; init; }

        /// <summary>
        /// The orbital radius of the moon around it's planet in KM
        /// </summary>
        public required double Orbit { get; init; }

        /// <summary>
        /// The orbital period of the moon in hours
        /// </summary>
        public required double OrbitalPeriod { get; init; }

        /// <summary>
        /// The tidal effect of this moon on it's planet in meters per second squared
        /// </summary>
        public required double TidalEffectOnWorld { get; init; }

        /// <summary>
        /// The tidal effect of the planet on this moon in meters per second squared
        /// </summary>
        public required double TidalEffectFromPlanet { get; init; }

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
        /// Rotation period in hours
        /// </summary>
        public required double RotationPeriod { get; set; }

        /// <summary>
        /// If this moon is tide locked to it's planet
        /// </summary>
        public required bool IsTideLocked { get; set; }
    }
}

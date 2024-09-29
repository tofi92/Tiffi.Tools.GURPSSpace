namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A star in a star system
    /// </summary>
    public record Star
    {
        /// <summary>
        /// Mass in kg
        /// </summary>
        public required double Mass { get; init; }

        /// <summary>
        /// Age in earth years
        /// </summary>
        /// <remarks>
        /// The age is calculated as a fraction of billions of years, e.g. 3.2 billion of years and then multiplied by 1 billion.
        /// Because of this, the age will always be an even number with mostly zeroes
        /// </remarks>
        public required double Age { get; init; }

        /// <summary>
        /// Luminosity in Watts
        /// </summary>
        public required double Luminosity { get; init; }

        /// <summary>
        /// Radius in km
        /// </summary>
        public required double Radius { get; init; }

        /// <summary>
        /// If this star is the primary star of the star system
        /// </summary>
        public required bool IsPrimary { get; init; }

        /// <summary>
        /// The inner limit in km where planets can be formed
        /// </summary>
        public required double InnerLimitRadius { get; init; }

        /// <summary>
        /// The outer limit in km where planets can be formed
        /// </summary>
        public required double OuterLimitRadius { get; init; }

        /// <summary>
        /// The limit in km where water ice could exist
        /// </summary>
        public required double SnowLine { get; init; }

        /// <summary>
        /// Average distance in km to the primary star
        /// </summary>
        /// <remarks>
        /// Only set if this star is not the primary star of the system
        /// </remarks>
        public double? AverageDistanceToPrimaryStar { get; internal set; }

        /// <summary>
        /// Eccentricity in relation to the primary star
        /// </summary>
        /// <remarks>
        /// Only set if this star is not the primary star of the system
        /// </remarks>
        public double? EccentricityInRelationToPrimaryStar { get; internal set; }

        /// <summary>
        /// Minimum separation in km to the primary star
        /// </summary>
        /// <remarks>
        /// Only set if this star is not the primary star of the system
        /// </remarks>
        public double? MinimumSeparationToPrimaryStar { get; internal set; }

        /// <summary>
        /// Maximum separation in km to the primary star
        /// </summary>
        /// <remarks>
        /// Only set if this star is not the primary star of the system
        /// </remarks>
        public double? MaximumSeparationToPrimaryStar { get; internal set; }

        /// <summary>
        /// Inner edge of the forbidden zone (where no planets can be generated)
        /// </summary>
        /// <remarks>
        /// Only set if this star is not the primary star of the system
        /// </remarks>
        public double? ForbiddenZoneInnerEdge => MinimumSeparationToPrimaryStar * 1 / 3;

        /// <summary>
        /// Outer edge of the forbidden zone (where no planets can be generated)
        /// </summary>
        /// <remarks>
        /// Only set if this star is not the primary star of the system
        /// </remarks>
        public double? ForbiddenZoneOuterEdge => MaximumSeparationToPrimaryStar * 3;
    }
}

namespace Tiffi.Tools.GURPSSpace.Models
{
    /// <summary>
    /// A star in a star system
    /// </summary>
    public record Star
    {
        /// <summary>
        /// Mass in solar masses
        /// </summary>
        public required double Mass { get; init; }

        /// <summary>
        /// Mass in kg
        /// </summary>
        public double MassInKg => Mass * Constants.SOLAR_MASS_IN_KILOGRAMS;

        /// <summary>
        /// Age in earth years
        /// </summary>
        /// <remarks>
        /// The age is calculated as a fraction of billions of years, e.g. 3.2 billion of years and then multiplied by 1 billion.
        /// Because of this, the age will always be an even number with mostly zeroes
        /// </remarks>
        public required double Age { get; init; }

        /// <summary>
        /// Luminosity in solar luminosities
        /// </summary>
        public required double Luminosity { get; init; }

        /// <summary>
        /// Luminosity in watts
        /// </summary>
        public double LuminosityInWatts => Luminosity * Constants.SOLAR_LUMINOSITY_IN_WATT;

        /// <summary>
        /// Radius in solar radius
        /// </summary>
        public required double Radius { get; init; }

        /// <summary>
        /// Radius in km
        /// </summary>
        public double RadiusInKm => Radius * Constants.SOLAR_RADIUS_IN_KM;

        /// <summary>
        /// If this star is the primary star of the star system
        /// </summary>
        public required bool IsPrimary { get; init; }

        /// <summary>
        /// The inner limit in AU where planets can be formed
        /// </summary>
        public required double InnerLimitRadius { get; init; }

        /// <summary>
        /// Inner limit in km
        /// </summary>
        public double InnerLimitRadiusInKm => InnerLimitRadius * Constants.KM_PER_AU;

        /// <summary>
        /// The outer limit in AU where planets can be formed
        /// </summary>
        public required double OuterLimitRadius { get; init; }

        /// <summary>
        /// Outer limit in km
        /// </summary>
        public double OuterLimitRadiusInKm => OuterLimitRadius * Constants.KM_PER_AU;

        /// <summary>
        /// The limit in AU where water ice could exist
        /// </summary>
        public required double SnowLine { get; init; }

        /// <summary>
        /// Snow line in km
        /// </summary>
        public double SnowLineInKm => SnowLine * Constants.KM_PER_AU;

        /// <summary>
        /// Average distance in au to the primary star
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

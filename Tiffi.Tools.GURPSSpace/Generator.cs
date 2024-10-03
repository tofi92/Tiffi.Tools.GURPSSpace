using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Tracing;
using System.Numerics;
using System.Runtime.CompilerServices;
using Tiffi.Tools.GURPSSpace.Extensions;
using Tiffi.Tools.GURPSSpace.Models;
using Tiffi.Tools.GURPSSpace.Options;
using static Tiffi.Tools.GURPSSpace.Tables;

namespace Tiffi.Tools.GURPSSpace
{
    /// <summary>
    /// Star system generator using the rules of GURPS
    /// </summary>
    public class Generator
    {
        private readonly GeneratorOptions _generatorOptions;
        private readonly DiceRoller dice;
        private IEnumerable<Star> _stars = [];

        /// <summary>
        /// A new instance of the generator with specific options
        /// </summary>
        /// <param name="generatorOptions">Optional generator options</param>
        public Generator(GeneratorOptions? generatorOptions = null)
        {
            this._generatorOptions = generatorOptions ?? new GeneratorOptions();
            if (this._generatorOptions.Seed == 0)
            {
                this._generatorOptions.Seed = DateTime.UtcNow.Nanosecond;
            }
            dice = new DiceRoller(this._generatorOptions.Seed);
        }

        /// <summary>
        /// Generate a star system
        /// </summary>
        public StarSystem Generate(GeneratorOptions? options = null)
        {
            options ??= _generatorOptions;
            var numberOfStars = RollNumberOfStars();
            List<Star> stars = [];
            var primaryStar = GenerateStar(0, options);
            stars.Add(primaryStar);
            for (var i = 1; i < numberOfStars; i++)
            {
                stars.Add(GenerateStar(i, options, primaryStar));
            }
            _stars = stars;
            var worlds = GenerateWorlds(stars, options);

            var system = new StarSystem()
            {
                Stars = stars,
                Worlds = worlds,
                Seed = options.Seed
            };
            return system;

        }

        private Star GenerateStar(int starIndex, GeneratorOptions generatorOptions, Star? primaryStar = null)
        {
            var stellarMass = RollStellarMass();
            var stellarAge = CalculateAgeInBillionsOfYears();
            var evolution = RollStellarEvolution(stellarMass);
            var luminosity = CalculateLuminosity(evolution, stellarAge);
            var radiusInAu = CalculateRadiusInAU(luminosity, evolution.Temp);

            var innerLimitRadiusInAu = CalculateInnerLimitRadius(stellarMass, luminosity);
            var outerLimitRadiusInAu = CalculateOuterLimitRadius(stellarMass);
            var snowLineInAu = CalculateSnowLineInAu(evolution.LMin);

            var newStar = new Star()
            {
                Age = stellarAge * 1e9,
                Luminosity = luminosity,
                Mass = stellarMass,
                Radius = radiusInAu,
                IsPrimary = starIndex == 0,
                InnerLimitRadius = innerLimitRadiusInAu,
                OuterLimitRadius = outerLimitRadiusInAu,
                SnowLine = snowLineInAu
            };

            if (primaryStar != null)
            {
                var modifier = 0;
                //if we have a garden world
                if (generatorOptions.ForceHabitablePlanet)
                {
                    //base modifier for a companion star
                    modifier += 4;
                    if (starIndex == 2) //if this is the third star, also add 6 to the modifier
                    {
                        modifier += 6;
                    }
                }

                var separationTableRoll = dice.Roll3d(modifier);
                var separation = OrbitalSeparationTable.First(x => x.Key.IsInRange(separationTableRoll)).Value;
                var separationRoll = dice.Roll(2, 6);
                newStar.AverageDistanceToPrimaryStar = separation.RadiusMultiplierInAu * separationRoll;

                //if (separation.Separation == Separation.Distant)
                //{
                //    //normaly we would check if the companion star had a companian for itself
                //    //maybe in a next version
                //}

                var eccentricityModifier = separation.Separation switch
                {
                    Separation.VeryClose => -6,
                    Separation.Close => -4,
                    Separation.Moderate => -2,
                    Separation.Wide => 0,
                    Separation.Distant => 0
                };

                var eccentricityRoll = dice.Roll3d(eccentricityModifier);
                var companionEccentricity = EccentricityTable.First(x => x.Key.IsInRange(eccentricityRoll)).Value;
                newStar.MinimumSeparationToPrimaryStar = (1 - companionEccentricity) * newStar.AverageDistanceToPrimaryStar;
                newStar.MaximumSeparationToPrimaryStar = (1 + companionEccentricity) * newStar.AverageDistanceToPrimaryStar;
                newStar.EccentricityInRelationToPrimaryStar = companionEccentricity;
            }

            return newStar;
        }

        private int RollNumberOfStars()
        {
            if (!_generatorOptions.MultipleStars)
            {
                return 1;
            }

            var roll = dice.Roll3d();

            return roll switch
            {
                (>= 3) and (<= 10) => 1,
                (>= 11) and (<= 15) => 2,
                (>= 16) => 3,
                _ => 1
            };
        }

        private double RollStellarMass()
        {
            var stellarMassTableFirstRoll = dice.Roll3d();

            if (_generatorOptions.ForceHabitablePlanet)
            {
                var exceptionRoll = dice.Roll(6);
                stellarMassTableFirstRoll = exceptionRoll switch
                {
                    1 => 5,
                    2 => 6,
                    (>= 3) and (<= 4) => 7,
                    (> 4) => 8,
                    _ => 5
                };
            }

            var stellarMassTableSecondRoll = dice.Roll3d();

            return Tables.StellarMassTable[stellarMassTableFirstRoll].First(x => x.Key.IsInRange(stellarMassTableSecondRoll)).Value;
        }

        private double CalculateAgeInBillionsOfYears()
        {
            var roll = dice.Roll3d();
            if (_generatorOptions.ForceHabitablePlanet)
            {
                roll = dice.Roll(2, 6, 2);
            }
            var stellarAge = Tables.StellarAgeTable.First(x => x.Key.IsInRange(roll)).Value;
            var stepARoll = dice.Roll(1, 6, -1);
            var stepBRoll = dice.Roll(1, 6, -1);

            return stellarAge.BaseAge + (stellarAge.StepA * stepARoll) + (stellarAge.StepB * stepBRoll);
        }

        private StellarEvolution RollStellarEvolution(double mass)
        {
            return Tables.StellarEvolutionTable.First(x => x.Mass == mass);
        }

        private double CalculateLuminosity(StellarEvolution evolution, double age)
        {
            if (evolution.LMax == null || evolution.MSpan == null)
            {
                return evolution.LMin;
            }

            return evolution.LMin + ((age / evolution.MSpan.Value) * (evolution.LMax.Value - evolution.LMin));
        }

        private double CalculateRadiusInAU(double luminosity, double temperature)
        {
            return 155_000 * Math.Sqrt(luminosity) / Math.Pow(temperature, 2);
        }

        private double CalculateInnerLimitRadius(double mass, double luminosity)
        {
            var innerRadius1 = 0.1 * mass;
            var innerRadius2 = 0.01 * mass * Math.Sqrt(luminosity);

            return Math.Max(innerRadius1, innerRadius2);
        }

        private double CalculateOuterLimitRadius(double mass)
        {
            return 40 * mass;
        }

        private double CalculateSnowLineInAu(double luminosity)
        {
            return 4.85 * Math.Sqrt(luminosity);
        }

        private IEnumerable<World> GenerateWorlds(IEnumerable<Star> stars, GeneratorOptions generatorOptions)
        {
            List<World> planets = [];

            foreach (var star in stars)
            {
                planets.AddRange(GenerateWorlds(star, generatorOptions));
            }

            return planets;
        }

        private IEnumerable<World> GenerateWorlds(Star star, GeneratorOptions generatorOptions)
        {
            List<World> worlds = [];

            //first get the Gas Giant Arrangement - this determines how many gas giants are in the system and where the first gas giant will spawn
            var gasGiantArrangementTableRoll = dice.Roll3d();
            var gasGiantArrangement = GasGiantArrangementTable.First(x => x.Key.IsInRange(gasGiantArrangementTableRoll)).Value;

            //the orbital spacing is the distance ratio between each orbit
            var orbitalSpacing = dice.Roll3d() switch
            {
                3 or 4 => 1.4,
                5 or 6 => 1.5,
                7 or 8 => 1.6,
                >= 9 and <= 12 => 1.7,
                13 or 14 => 1.8,
                15 or 16 => 1.9,
                17 or 18 => 2.0,
                _ => 1.4
            };

            //only spawn planets for the primary star (for now, it is possible to spawn planets for companion stars but that's pretty complicated)
            if (star.IsPrimary)
            {
                //first get a list of orbital radii between the inner and outer limit
                //every next radius is multiplied by the ratio of the orbital spacing (in AU, so we need to reconvert the existing limits)
                List<double> orbits = [];
                var orbitalRadius = (star.InnerLimitRadius) * orbitalSpacing;
                while (orbitalRadius < star.OuterLimitRadius)
                {
                    var currentOrbit = orbitalRadius;
                    var orbit = orbitalRadius;
                    if (orbit > star.Radius * 2 && orbit > star.InnerLimitRadius && orbit < star.OuterLimitRadius)
                    {
                        orbits.Add(orbitalRadius);
                    }
                    orbitalRadius *= orbitalSpacing;
                }

                //if we need to generate a habitable planet, we predetermine the radius
                if (generatorOptions.ForceHabitablePlanet)
                {
                    //get the radius that is closest to the snowline
                    var closestToSnowLine = orbits
                        .Where(o => o <= star.SnowLine)
                        .Last();
                    //R = (77,300/B2) *  square root of L
                    //Here, R is the orbital radius in AU,
                    //B is the world’s blackbody temperature,
                    //and L is the star’s luminosity in solar units.
                    var habitableWorld = GenerateGardenWorld(star, closestToSnowLine);
                    //set the radius to whichever is bigger, but remove the closest radius to the snowline nevertheless
                    var orbitalRadiusHabitableWorld = Math.Max(closestToSnowLine, (77300 / Math.Pow(habitableWorld.BlackBodyTemperature, 2)) * Math.Sqrt(star.Luminosity));
                    habitableWorld.OrbitalRadius = orbitalRadiusHabitableWorld;
                    worlds.Add(habitableWorld);
                    orbits.Remove(closestToSnowLine);
                }

                //first we place some gas giants
                foreach (var orbit in orbits)
                {
                    //check if we need to place a gas giant at all
                    var gasGiantRoll = dice.Roll3d();
                    bool placeGasGiant = gasGiantArrangement switch
                    {
                        GasGiantArrangement.None => false,
                        GasGiantArrangement.Conventional => orbit >= star.SnowLine && gasGiantRoll <= 15,
                        GasGiantArrangement.Eccentric => orbit < star.SnowLine ? gasGiantRoll <= 8 : gasGiantRoll <= 14,
                        GasGiantArrangement.Epistellar => orbit < star.SnowLine ? gasGiantRoll <= 6 : gasGiantRoll <= 14,
                        _ => throw new NotImplementedException()
                    };

                    if (placeGasGiant)
                    {
                        //get the size of the gas giant and add it to the planets
                        var sizeRoll = dice.Roll3d();
                        var gasGiantSize = GasGiantSizeTable.First(x => x.Key.IsInRange(sizeRoll)).Value;
                        var massDensityRoll = dice.Roll3d();
                        var massAndDensity = GasGiantMassDensityTable.First(x => x.Key.Item1.IsInRange(massDensityRoll) && x.Key.Item2 == gasGiantSize).Value;
                        var diameter = Math.Cbrt(massAndDensity.Mass / massAndDensity.Density);
                        var eccentricityRoll = dice.Roll3d();
                        var eccentricity = EccentricityTable.First(x => x.Key.IsInRange(eccentricityRoll)).Value;
                        var periapsis = (1 - eccentricity) * orbit;
                        var apoapsis = (1 + eccentricity) * orbit;
                        var gasGiant = new GasGiant()
                        {
                            Size = ToWorldSize(gasGiantSize),
                            GasGiantSize = gasGiantSize,
                            OrbitalRadius = orbit,
                            Mass = massAndDensity.Mass,
                            Density = massAndDensity.Density,
                            Diameter = diameter,
                            OrbitalPeriod = Math.Sqrt(Math.Pow(orbit, 3) / star.Mass) * 365.26 * 24,
                            Apoapsis = apoapsis,
                            Eccentricity = eccentricity,
                            Periapsis = periapsis,
                        };

                        if (gasGiant.Diameter / 2 + gasGiant.OrbitalRadius < worlds.LastOrDefault()?.OrbitalRadius)
                        {
                            worlds.Add(gasGiant);
                        }

                    }
                }

                //now place the rest of the planets
                foreach (var orbit in orbits)
                {
                    //if any world is in this radius, skip
                    if (worlds.Any(x => x.OrbitalRadius == orbit))
                    {
                        continue;
                    }

                    //check if the world is in the forbidden zone
                    if (star.ForbiddenZoneInnerEdge != null && star.ForbiddenZoneOuterEdge != null)
                    {
                        if (orbit >= star.ForbiddenZoneInnerEdge && orbit <= star.ForbiddenZoneOuterEdge)
                        {
                            continue;
                        }
                    }

                    //we need to add modifiers if any world next to the current orbit is a gas giant
                    var inwardPlanet = worlds.LastOrDefault(x => x.OrbitalRadius < orbit);
                    var outwardPlanet = worlds.FirstOrDefault(x => x.OrbitalRadius > orbit);

                    var modifier = 0;
                    if (inwardPlanet != null && inwardPlanet is GasGiant)
                    {
                        modifier -= 3;
                    }

                    if (outwardPlanet != null && outwardPlanet is GasGiant)
                    {
                        modifier -= 3;
                    }

                    //determine what size (if any) of planet we need to place
                    var orbitContentRoll = dice.Roll3d(modifier);

                    var planet = orbitContentRoll switch
                    {
                        <= 3 => null,
                        >= 4 and <= 6 => GeneratePlanet(WorldSize.None, WorldClassifier.AsteroidBelt, star.Mass, orbit, star),
                        _ => GeneratePlanet(null, null, star.Mass, orbit, star)
                    };

                    if (planet != null)
                    {
                        var moons = GenerateMoons(planet, star);

                        planet.Moons = moons;
                        var rotationPeriod = GetRotationPeriod(planet, star);
                        planet.RotationPeriod = rotationPeriod.RotationPeriod;
                        planet.IsTideLocked = rotationPeriod.IsTideLocked;

                        worlds.Add(planet);
                    }

                }

            }

            return worlds;
        }

        private (GasGiant? GasGiant, double? Radius) GenerateFirstGasGiant(Star star, GasGiantArrangement gasGiantArrangement)
        {
            //the first gas giant is determined by the arrangement and has special calculations for the orbit radius
            if (gasGiantArrangement == GasGiantArrangement.None)
            {
                return (null, null);
            }

            double orbit = 0;

            if (gasGiantArrangement == GasGiantArrangement.Conventional)
            {
                orbit = (dice.Roll(2, 6, 2) * 0.05 + 1) * star.SnowLine;
            }

            if (gasGiantArrangement == GasGiantArrangement.Eccentric)
            {
                orbit = dice.Roll(1, 6) * 0.125 * star.SnowLine;
            }

            if (gasGiantArrangement == GasGiantArrangement.Epistellar)
            {
                orbit = dice.Roll3d() * 0.1 * star.InnerLimitRadius;
            }

            //check if the planet is in the forbidden zone
            if (star.ForbiddenZoneInnerEdge != null && star.ForbiddenZoneOuterEdge != null)
            {
                if (orbit >= star.ForbiddenZoneInnerEdge && orbit <= star.ForbiddenZoneOuterEdge)
                {
                    //we CAN allow the first gas giant to be placed in the forbidden zone for gameplay reasons
                    if (!_generatorOptions.AllowFirstGasGiantInForbiddenZone)
                    {
                        return (null, null);
                    }
                }
            }

            var densityRoll = dice.Roll3d();
            var massAndDensity = GasGiantMassDensityTable.First(x => x.Key.Item1.IsInRange(densityRoll) && x.Key.Item2 == GasGiantSize.Medium).Value;
            var diameter = Math.Cbrt(massAndDensity.Mass / massAndDensity.Density);
            var eccentricityRoll = dice.Roll3d();
            var eccentricity = EccentricityTable.First(x => x.Key.IsInRange(eccentricityRoll)).Value;
            var periapsis = (1 - eccentricity) * orbit;
            var apoapsis = (1 + eccentricity) * orbit;





            var gasGiant = new GasGiant()
            {
                Size = ToWorldSize(GasGiantSize.Medium),
                GasGiantSize = GasGiantSize.Medium,
                OrbitalRadius = orbit,
                Mass = massAndDensity.Mass,
                Density = massAndDensity.Density,
                Diameter = diameter,
                OrbitalPeriod = Math.Sqrt(Math.Pow(orbit, 3) / star.Mass) * 365.26 * 24,
                Apoapsis = apoapsis,
                Eccentricity = eccentricity,
                Periapsis = periapsis,
            };

            var moons = GenerateMoons(gasGiant, star);

            gasGiant.Moons = moons;
            var rotationPeriod = GetRotationPeriod(gasGiant, star);
            gasGiant.RotationPeriod = rotationPeriod.RotationPeriod;
            gasGiant.IsTideLocked = rotationPeriod.IsTideLocked;

            return (gasGiant, orbit);

        }

        private Planet GeneratePlanet(WorldSize? predefinedSize, WorldClassifier? predefinedClassifier, double mass, double orbit, Star? star)
        {
            //determine the overall type (hostile, barren or garden)
            var overallTypeRoll = dice.Roll3d();
            var overallType = GetOverallType(overallTypeRoll);
            var sizeRoll = dice.Roll3d();

            //if the size is not predefined, roll it
            var size = predefinedSize ?? overallType switch
            {
                WorldOverallType.Hostile => HostileWorldTable[sizeRoll].Item1,
                WorldOverallType.Barren => BarrenWorldTable[sizeRoll].Item1,
                WorldOverallType.Garden => GardenWorldTable[sizeRoll].Item1,
                _ => throw new NotImplementedException()
            };

            //if the classifier is not predefined, roll it
            var classifier = predefinedClassifier ?? overallType switch
            {
                WorldOverallType.Hostile => HostileWorldTable[sizeRoll].Item2,
                WorldOverallType.Barren => BarrenWorldTable[sizeRoll].Item2,
                WorldOverallType.Garden => GardenWorldTable[sizeRoll].Item2,
                _ => throw new NotImplementedException()
            };

            //determine all the properties!
            var atmosphericMass = dice.Roll3d() / 10d;

            var waterCoverage = dice.Roll(1, 6, 4) * 0.1;
            if (waterCoverage == 1) //100% water coverage not wanted
            {
                waterCoverage -= dice.Roll(1, 4) * 0.1;
            }
            var averageSurfaceTemperaturInK = AverageSurfaceTemperatureStepTable[(size, classifier)].TemperatureStep * dice.Roll3d(-3) + AverageSurfaceTemperatureStepTable[(size, classifier)].TemperatureRange.Min;
            var blackBodyTemperature = GetBlackbodyCorrection(size, classifier, atmosphericMass, waterCoverage) * averageSurfaceTemperaturInK;
            //if (star != null)
            //{
            //    blackBodyTemperature = Math.Floor(278 * Math.Pow(star.Luminosity / Constants.SOLAR_LUMINOSITY_IN_WATT, 1.0 / 4) / Math.Sqrt(orbit));
            //}
            var coreType = GetWorldCoreType(size, classifier);
            var densityRoll = dice.Roll3d();
            var density = coreType switch
            {
                WorldCoreType.Icy => IcyCoreDensityTable.First(x => x.Key.IsInRange(densityRoll)).Value,
                WorldCoreType.SmallIron => SmallIronCoreDensityTable.First(x => x.Key.IsInRange(densityRoll)).Value,
                WorldCoreType.LargeIron => LargeIronCoreDensityTable.First(x => x.Key.IsInRange(densityRoll)).Value,
                _ => throw new NotImplementedException()
            };
            var worldMinimumSize = 0d;
            var worldMaximumSize = 0d;
            if (size != WorldSize.None)
            {
                worldMinimumSize = Math.Sqrt(blackBodyTemperature / density) * SizeConstraintsTable[size].MinimumSize;
                worldMaximumSize = Math.Sqrt(blackBodyTemperature / density) * SizeConstraintsTable[size].MaximumSize;
            }

            var worldSizeRoll = dice.Roll(2, 6, -2);
            var worldDiameter = worldMinimumSize + worldSizeRoll * ((worldMaximumSize - worldMaximumSize) / 10);
            var surfaceGravity = density * worldDiameter;
            var planetMass = density * Math.Pow(worldDiameter, 3);
            var pressureFactor = GetPressureFactor(size, classifier);
            var pressure = atmosphericMass * pressureFactor * surfaceGravity;
            var resourcesRoll = dice.Roll3d();
            var (OverallValue, ValueModifier) = WorldResourceValueTable.First(x => x.Key.IsInRange(resourcesRoll)).Value;

            var eccentricityRoll = dice.Roll3d();
            var eccentricity = EccentricityTable.First(x => x.Key.IsInRange(eccentricityRoll)).Value;
            var periapsis = (1 - eccentricity) * orbit;
            var apoapsis = (1 + eccentricity) * orbit;

            var planet = new Planet()
            {
                Size = size,
                Classifier = classifier,
                AtmosphericMass = atmosphericMass,
                WaterCoverage = waterCoverage,
                AverageSurfaceTemperature = averageSurfaceTemperaturInK,
                BlackBodyTemperature = blackBodyTemperature,
                Density = density,
                Diameter = worldDiameter,
                Gravity = surfaceGravity,
                Mass = planetMass,
                Pressure = pressure,
                OverallResourceValue = OverallValue,
                Eccentricity = eccentricity,
                Apoapsis = apoapsis,
                Periapsis = periapsis,
                OrbitalRadius = orbit,
                OrbitalPeriod = Math.Sqrt(Math.Pow(orbit, 3) / mass) * 365.26 * 24,
            };

            if (star != null)
            {
                var moons = GenerateMoons(planet, star);
                planet.Moons = moons;
                var rotationPeriod = GetRotationPeriod(planet, star);
                planet.RotationPeriod = rotationPeriod.RotationPeriod;
                planet.IsTideLocked = rotationPeriod.IsTideLocked;
            }

            if (classifier == WorldClassifier.AsteroidBelt)
            {
                atmosphericMass = 0;
                density = 0;
            }

            return planet;
        }

        private IEnumerable<Moon> GenerateMoons(World world, Star star)
        {

            List<Moon> moons = [];
            if (world is GasGiant)
            {
                var firstFamilyModifier = (world.OrbitalRadius) switch
                {
                    <= 0.1 => -10,
                    > 0.1 and <= 0.5 => -8,
                    > 0.5 and <= 0.75 => -6,
                    > 0.75 and <= 1.5 => -3,
                    _ => 0
                };

                var numberOfFirstFamilyMoons = dice.Roll(2, 6, firstFamilyModifier);
                var numberOfSecondFamilyMoons = 0;
                var numberOfThirdFamilyMoons = 0;

                if ((world.OrbitalRadius) > 0.1)
                {
                    var secondFamilyModifier = (world.OrbitalPeriod) switch
                    {
                        <= 0.5 => -5,
                        > 0.5 and <= 0.75 => -4,
                        > 0.75 and <= 1.5 => -1,
                        _ => 0
                    };

                    numberOfSecondFamilyMoons = dice.Roll(1, 6, secondFamilyModifier);
                }

                if ((world.OrbitalRadius) > 0.5)
                {
                    var thirdFamilyModifier = (world.OrbitalPeriod) switch
                    {
                        > 0.5 and <= 0.75 => -5,
                        > 0.75 and <= 1.5 => -4,
                        > 1.5 and <= 3 => -1,
                        _ => 0
                    };

                    numberOfThirdFamilyMoons = dice.Roll(1, 6, thirdFamilyModifier);
                }

                for (int i = 0; i < numberOfFirstFamilyMoons; i++)
                {
                    moons.Add(GenerateMoon(MoonSize.Moonlet, world, star, 1));
                }

                for (int i = 0; i < numberOfSecondFamilyMoons; i++)
                {
                    moons.Add(GenerateMoon(MoonSize.Major, world, star, 2));
                }

                for (int i = 0; i < numberOfThirdFamilyMoons; i++)
                {
                    moons.Add(GenerateMoon(MoonSize.Moonlet, world, star, 3));
                }


            }
            else if (world is Planet p)
            {
                if (p.Classifier == WorldClassifier.AsteroidBelt)
                {
                    return [];
                }

                if ((world.OrbitalRadius) < 0.5)
                {
                    return moons;
                }

                var modifier = (world.OrbitalRadius) switch
                {
                    >= 0.5 and < 0.75 => -3,
                    >= 0.75 and < 1.5 => -1,
                    _ => 0
                };

                modifier += world.Size switch
                {
                    WorldSize.Tiny => -2,
                    WorldSize.Small => -1,
                    WorldSize.Large => 1,
                    _ => 0
                };

                var amountOfMajorMoons = Math.Max(0, dice.Roll(1, 6, modifier - 4));
                var amountOfMoonlets = 0;
                if (amountOfMajorMoons == 0)
                {
                    amountOfMoonlets = Math.Max(0, dice.Roll(1, 6, modifier - 2));
                }

                for(int i = 0; i < amountOfMajorMoons; i++) 
                {
                    moons.Add(GenerateMoon(MoonSize.Major, world, star));
                }

                for(int i = 0; i < amountOfMoonlets; i++)
                {
                    moons.Add(GenerateMoon(MoonSize.Moonlet, world, star));
                }
            }

            return moons;
        }

        private Moon GenerateMoon(MoonSize size, World world, Star star, int family = 1)
        {
            var worldSizeInt = (int)world.Size;
            var moonSizeRoll = dice.Roll3d();
            var moonWorldSize = size == MoonSize.Major ? (WorldSize?)Math.Max(0, worldSizeInt + MoonSizeTable.First(x => x.Key.IsInRange(moonSizeRoll)).Value) : null;
            var orbitalRadius = 0d;
            if (world is GasGiant)
            {
                if (family == 1)
                {
                    orbitalRadius = dice.Roll(1, 6, 4) / 4 * world.Diameter;
                }
                else if (family == 2)
                {
                    var roll = dice.Roll3d(3);
                    if (roll > 15)
                    {
                        roll += dice.Roll(2, 6);
                    }
                    orbitalRadius = roll / 2 * world.Diameter;
                }
                else if (family == 3)
                {
                    orbitalRadius = dice.Roll(1, 180, 20) * world.Diameter;
                }
            }
            else if (world is Planet)
            {
                if (size == MoonSize.Major)
                {
                    var modifier = 0;
                    var moonSizeModifierRoll = dice.Roll3d();
                    var moonSizeModifier = MoonSizeTable.First(x => x.Key.IsInRange(moonSizeModifierRoll)).Value;
                    if (moonSizeModifier == -2)
                    {
                        modifier = 2;
                    }
                    else if (moonSizeModifier == -1)
                    {
                        modifier = 4;
                    }
                    orbitalRadius = dice.Roll(2, 6, modifier) * 2.5 * world.Diameter;
                }
                else
                {
                    orbitalRadius = dice.Roll(1, 6, 4) / 4 * world.Diameter;
                }
            }



            Planet? moonAsPlanetForValues = null;
            if (size == MoonSize.Moonlet)
            {
                moonAsPlanetForValues = GeneratePlanet(WorldSize.Tiny, WorldClassifier.Rock, world.Mass, orbitalRadius, null);
                moonAsPlanetForValues.AtmosphericMass = 0;
                moonAsPlanetForValues.Pressure = 0;
                moonAsPlanetForValues.WaterCoverage = 0;
            }
            else
            {
                moonAsPlanetForValues = GeneratePlanet(moonWorldSize, WorldClassifier.Rock, world.Mass, orbitalRadius, null);
            }

            var orbitalPeriod = 0.166 * Math.Sqrt((Math.Pow(orbitalRadius, 3)) / (world.Mass + moonAsPlanetForValues.Mass));

            var tidalEffectOnPlanet = (2230000 * moonAsPlanetForValues.Mass * world.Diameter) * Math.Pow(orbitalPeriod, 3);
            var tidalEffectOnMoon = (2230000 * world.Mass * moonAsPlanetForValues.Diameter ) * Math.Pow(orbitalPeriod, 3);
            var totalTidalEffectOnMoon = (int)Math.Floor((tidalEffectOnMoon * star.Age / 1e9) / world.Mass);
            var isTideLocked = totalTidalEffectOnMoon >= 50;
            var rotationPeriod = orbitalPeriod;
            if (!isTideLocked)
            {
                rotationPeriod = dice.Roll3d(18);
                if (rotationPeriod > 36 || dice.LastNaturalValue >= 16)
                {
                    var unnaturalRotationRoll = dice.Roll(2, 6);
                    rotationPeriod = unnaturalRotationRoll switch
                    {
                        <= 6 => rotationPeriod,
                        7 => dice.Roll(6) * 24 * 2,
                        8 => dice.Roll(6) * 24 * 5,
                        9 => dice.Roll(6) * 24 * 10,
                        10 => dice.Roll(6) * 24 * 20,
                        11 => dice.Roll(6) * 24 * 50,
                        12 => dice.Roll(6) * 24 * 12,
                        _ => rotationPeriod
                    };
                }
            }

            if (rotationPeriod > orbitalPeriod)
            {
                rotationPeriod = orbitalPeriod;
            }

            return new Moon()
            {
                Size = size,
                WorldSize = moonWorldSize,
                Orbit = orbitalRadius,
                Density = moonAsPlanetForValues.Density,
                Diameter = moonAsPlanetForValues.Diameter,
                Mass = moonAsPlanetForValues.Mass,
                Eccentricity = moonAsPlanetForValues.Eccentricity,
                Apoapsis = moonAsPlanetForValues.Apoapsis,
                OrbitalPeriod = orbitalPeriod,
                Periapsis = moonAsPlanetForValues.Periapsis,
                TidalEffectOnWorld = tidalEffectOnPlanet,
                TidalEffectFromPlanet = tidalEffectOnMoon,
                RotationPeriod = rotationPeriod,
                IsTideLocked = isTideLocked
            };
        }

        private WorldSize ToWorldSize(GasGiantSize gasGiantSize)
        {
            switch (gasGiantSize)
            {
                case GasGiantSize.Small:
                    return WorldSize.Small;
                case GasGiantSize.Medium:
                    return WorldSize.Standard;
                case GasGiantSize.Large:
                    return WorldSize.Large;
                default:
                    return WorldSize.Standard;
            }
        }

        private Planet GenerateGardenWorld(Star star, double radius)
        {
            return GeneratePlanet(WorldSize.Standard, WorldClassifier.Garden, star.Mass, radius, star);
        }

        private (double RotationPeriod, bool IsTideLocked) GetRotationPeriod(World world, Star star)
        {
            var totalTideEffect = (int)Math.Floor((world.Moons.Sum(m => m.TidalEffectOnWorld) * star.Age / 1e9) / world.Mass);
            var isTideLocked = totalTideEffect >= 50;
            var rotationPeriod = world.OrbitalPeriod;
            if (!isTideLocked)
            {
                var rotationPeriodModifier = world.Size switch
                {
                    WorldSize.Large => 6,
                    WorldSize.Standard => 10,
                    WorldSize.Small => 14,
                    WorldSize.Tiny => 18,
                    _ => 0
                };


                rotationPeriod = dice.Roll3d(rotationPeriodModifier);
                if (rotationPeriod > 36 || dice.LastNaturalValue >= 16)
                {
                    var unnaturalRotationRoll = dice.Roll(2, 6);
                    rotationPeriod = unnaturalRotationRoll switch
                    {
                        <= 6 => rotationPeriod,
                        7 => dice.Roll(6) * 24 * 2,
                        8 => dice.Roll(6) * 24 * 5,
                        9 => dice.Roll(6) * 24 * 10,
                        10 => dice.Roll(6) * 24 * 20,
                        11 => dice.Roll(6) * 24 * 50,
                        12 => dice.Roll(6) * 24 * 12,
                        _ => rotationPeriod
                    };
                }
            }

            if (rotationPeriod > world.OrbitalPeriod)
            {
                rotationPeriod = world.OrbitalPeriod;
                isTideLocked = true;
            }

            return (rotationPeriod, isTideLocked);
        }
    }
}

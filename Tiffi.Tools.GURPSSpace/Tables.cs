using Tiffi.Tools.GURPSSpace.Models;

namespace Tiffi.Tools.GURPSSpace
{
    internal static class Tables
    {
        /// <summary>
        /// Stellar Mass Table
        /// Index of first dict key is first 3d6 roll
        /// Range of second dict key is second 3d6 roll
        /// Value of second dict is the stellar mass as a fraction of a solar mass
        /// </summary>
        public static IReadOnlyDictionary<int, IReadOnlyDictionary<Range, double>> StellarMassTable = new Dictionary<int, IReadOnlyDictionary<Range, double>>()
        {
            {
                3, new Dictionary<Range, double>()
                {
                    {
                        new(3,10), 2.0
                    },
                    {
                        new(11,18), 1.9
                    }
                }
            },
            {
                4, new Dictionary<Range, double>()
                {
                    {
                        new(3,8), 1.8
                    },
                    {
                        new(9,11), 1.7
                    },
                    {
                        new(12,18), 1.6
                    }
                }
            },
            {
                5, new Dictionary<Range, double>()
                {
                    {
                        new(3,7), 1.5
                    },
                    {
                        new(8,10), 1.45
                    },
                    {
                        new(11,12), 1.4
                    },
                    {
                        new(13,18), 1.35
                    }
                }
            },
            {
                6, new Dictionary<Range, double>()
                {
                    {
                        new(3,7), 1.3
                    },
                    {
                        new(8,9), 1.25
                    },
                    {
                        new(10,10), 1.2
                    },
                    {
                        new(11,12), 1.15
                    },
                    {
                        new(13,18), 1.1
                    }
                }
            },
            {
                7, new Dictionary<Range, double>()
                {
                    {
                        new(3,7), 1.05
                    },
                    {
                        new(8,9), 1.0
                    },
                    {
                        new(10,10), .95
                    },
                    {
                        new(11,12), .9
                    },
                    {
                        new(13,18), .85
                    }
                }
            },
            {
                8, new Dictionary<Range, double>()
                {
                    {
                        new(3,7), .8
                    },
                    {
                        new(8,9), .75
                    },
                    {
                        new(10,10), .7
                    },
                    {
                        new(11,12), .65
                    },
                    {
                        new(13,18), .6
                    }
                }
            },
            {
                9, new Dictionary<Range, double>()
                {
                    {
                        new(3,8), .55
                    },
                    {
                        new(9,11), .5
                    },
                    {
                        new(12,18), .45
                    }
                }
            },
            {
                10, new Dictionary<Range, double>()
                {
                    {
                        new(3,8), .4
                    },
                    {
                        new(9,11), .35
                    },
                    {
                        new(12,18), .3
                    }
                }
            },
            {
                11, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .25
                    }
                }
            },
            {
                12, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .2
                    }
                }
            },
            {
                13, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .15
                    }
                }
            },
            {
                14, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .1
                    }
                }
            },
            {
                15, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .1
                    }
                }
            },
            {
                16, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .1
                    }
                }
            },
            {
                17, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .1
                    }
                }
            },
            {
                18, new Dictionary<Range, double>()
                {
                    {
                        new(3,18), .1
                    }
                }
            },
        };

        public record StellarAge(string Population, double BaseAge, double StepA, double StepB);

        /// <summary>
        /// Stellar age
        /// </summary>
        public static IReadOnlyDictionary<Range, StellarAge> StellarAgeTable = new Dictionary<Range, StellarAge>()
        {
            {
                new(3,3), new StellarAge("Extreme Population I", 0, 0, 0)
            },
            {
                new(4,6), new StellarAge("Young Population I", 0.1, 0.3, 0.05)
            },
            {
                new(7,10), new StellarAge("Intermediate Population I", 2, 0.6, 0.1)
            },
            {
                new(11,14), new StellarAge("Old Population I", 5.6, 0.6, 0.1)
            },
            {
                new(15,17), new StellarAge("Intermediate Population II", 8, 0.6, 0.1)
            },
            {
                new(18,18), new StellarAge("Extreme Population II", 10, 0.6, 0.1)
            }
        };

        public record StellarEvolution(double Mass, string Type, double Temp, double LMin, double? LMax, double? MSpan, double? SSpan, double? GSpan);

        public static IEnumerable<StellarEvolution> StellarEvolutionTable =
        [
            new StellarEvolution(0.10, "M7", 3100, 0.0012, null, null, null, null),
            new StellarEvolution(0.15, "M6", 3200, 0.0036, null, null, null, null),
            new StellarEvolution(0.20, "M5", 3200, 0.0079, null, null, null, null),
            new StellarEvolution(0.25, "M4", 3300, 0.015, null, null, null, null),
            new StellarEvolution(0.30, "M4", 3300, 0.024, null, null, null, null),
            new StellarEvolution(0.35, "M3", 3400, 0.037, null, null, null, null),
            new StellarEvolution(0.40, "M2", 3500, 0.054, null, null, null, null),

            //With L-Max & M-Span
            new StellarEvolution(0.45, "M1", 3600, 0.07, 0.08, 70, null, null),
            new StellarEvolution(0.50, "M0", 3800, 0.09, 0.11, 59, null, null),
            new StellarEvolution(0.55, "K8", 4000, 0.11, 0.15, 50, null, null),
            new StellarEvolution(0.60, "K6", 4200, 0.13, 0.20, 42, null, null),
            new StellarEvolution(0.65, "K5", 4400, 0.15, 0.25, 37, null, null),
            new StellarEvolution(0.70, "K4", 4600, 0.19, 0.35, 30, null, null),
            new StellarEvolution(0.75, "K2", 4900, 0.23, 0.48, 24, null, null),
            new StellarEvolution(0.80, "K0", 5200, 0.19, 0.65, 20, null, null),
            new StellarEvolution(0.85, "G8", 5400, 0.23, 0.84, 17, null, null),
            new StellarEvolution(0.90, "G6", 5500, 0.23, 1.0, 14, null, null),

            //With all features
            new StellarEvolution(0.95, "G4", 5700, 0.56, 1.3, 12, 12, 1.1),
            new StellarEvolution(1.00, "G2", 5800, 0.68, 1.6, 10, 10, 1.2),
            new StellarEvolution(1.05, "G1", 5900, 0.87, 1.9, 8.8, 1.4, 0.8),
            new StellarEvolution(1.10, "G0", 6000, 1.1, 2.2, 7.7, 1.2, 0.7),
            new StellarEvolution(1.15, "F9", 6100, 1.3, 2.7, 6.7, 1.1, 0.6),
            new StellarEvolution(1.20, "F8", 6300, 1.7, 3.0, 5.9, 0.9, 0.6),
            new StellarEvolution(1.25, "F7", 6400, 2.1, 3.5, 5.2, 0.8, 0.5),
            new StellarEvolution(1.30, "F6", 6500, 2.5, 3.9, 4.6, 0.7, 0.4),
            new StellarEvolution(1.35, "F5", 6600, 3.1, 4.5, 4.1, 0.6, 0.4),
            new StellarEvolution(1.40, "F4", 6700, 3.7, 5.1, 3.7, 0.6, 0.4),
            new StellarEvolution(1.45, "F3", 6800, 4.3, 5.7, 3.3, 0.5, 0.3),
            new StellarEvolution(1.50, "F2", 7000, 5.1, 6.5, 2.8, 0.4, 0.3),
            new StellarEvolution(1.60, "F0", 7300, 6.7, 8.2, 2.5, 0.4, 0.2),
            new StellarEvolution(1.70, "A9", 7500, 8.6, 10, 2.1, 0.3, 0.2),
            new StellarEvolution(1.80, "A7", 7800, 11, 13, 1.8, 0.3, 0.2),
            new StellarEvolution(1.90, "A6", 8000, 13, 16, 1.5, 0.3, 0.2),
            new StellarEvolution(2.00, "A5", 8200, 16, 20, 1.3, 0.2, 0.1)
        ];

        public enum Separation
        {
            VeryClose,
            Close,
            Moderate,
            Wide,
            Distant
        }
        public record OrbitalSeparation(Separation Separation, double RadiusMultiplierInAu);

        public static IReadOnlyDictionary<Range, OrbitalSeparation> OrbitalSeparationTable = new Dictionary<Range, OrbitalSeparation>()
        {
            {
                new(-999, 6), new OrbitalSeparation(Separation.VeryClose, 0.05)
            },
            {
                new(7, 9), new OrbitalSeparation(Separation.Close, 0.5)
            },
            {
                new(10, 11), new OrbitalSeparation(Separation.Moderate, 2)
            },
            {
                new(12, 14), new OrbitalSeparation(Separation.Wide, 10)
            },
            {
                new(15, 999), new OrbitalSeparation(Separation.Distant, 50)
            },
        };

        public static IReadOnlyDictionary<Range, double> EccentricityTable = new Dictionary<Range, double>()
        {
            {
                new(-999,3), 0
            },
            {
                new(4,4), 0.1
            },
            {
                new(5,5), 0.2
            },
            {
                new(6,6), 0.3
            },
            {
                new(7,8), 0.4
            },
            {
                new(9,11), 0.5
            },
            {
                new(12,13), 0.6
            },
            {
                new(14,15), 0.7
            },
            {
                new(16,16), 0.8
            },
            {
                new(17,17), 0.9
            },
            {
                new(18,999), 0.95
            }
        };

        public enum GasGiantArrangement
        {
            None,
            Conventional,
            Eccentric,
            Epistellar
        }

        public static IReadOnlyDictionary<Range, GasGiantArrangement> GasGiantArrangementTable = new Dictionary<Range, GasGiantArrangement>()
        {
            {
                new (-999, 10), GasGiantArrangement.None
            },
            {
                new (11, 12), GasGiantArrangement.Conventional
            },
            {
                new (13,14), GasGiantArrangement.Eccentric
            },
            {
                new (15,18), GasGiantArrangement.Epistellar
            },
        };

        public static IReadOnlyDictionary<Range, double> OrbitalSpacingTable = new Dictionary<Range, double>()
        {
            {
                new(3,4), 1.4
            },
            {
                new(5,6), 1.5
            },
            {
                new(7,8), 1.6
            },
            {
                new(9,12), 1.7
            },
            {
                new(13,14), 1.8
            },
            {
                new(15,16), 1.9
            },
            {
                new(17,18), 2.0
            },
        };

        public static IReadOnlyDictionary<Range, GasGiantSize> GasGiantSizeTable = new Dictionary<Range, GasGiantSize>()
        {
            {
                new(3,10), GasGiantSize.Small
            },
            {
                new(11, 16), GasGiantSize.Medium
            },
            {
                new(17, 999), GasGiantSize.Large
            }
        };

        public enum WorldOverallType
        {
            Hostile,
            Barren,
            Garden
        }

        public static WorldOverallType GetOverallType(int roll)
        {
            return roll switch
            {
                <= 7 => WorldOverallType.Hostile,
                >= 8 and <= 13 => WorldOverallType.Barren,
                >= 14 and <= 18 => WorldOverallType.Garden,
                _ => WorldOverallType.Garden
            };
        }

        public static IReadOnlyDictionary<int, (WorldSize, WorldClassifier)> HostileWorldTable = new Dictionary<int, (WorldSize, WorldClassifier)>
        {
            {
                3, (WorldSize.Standard, WorldClassifier.Chthonian)
            },
            {
                4, (WorldSize.Standard, WorldClassifier.Chthonian)
            },
            {
                5, (WorldSize.Standard, WorldClassifier.Greenhouse)
            },
            {
                6, (WorldSize.Standard, WorldClassifier.Greenhouse)
            },
            {
                7, (WorldSize.Tiny, WorldClassifier.Sulfur)
            },
            {
                8, (WorldSize.Tiny, WorldClassifier.Sulfur)
            },
            {
                9, (WorldSize.Tiny, WorldClassifier.Sulfur)
            },
            {
                10, (WorldSize.Standard, WorldClassifier.Ammonia)
            },
            {
                11, (WorldSize.Standard, WorldClassifier.Ammonia)
            },
            {
                12, (WorldSize.Standard, WorldClassifier.Ammonia)
            },
            {
                13, (WorldSize.Large, WorldClassifier.Ammonia)
            },
            {
                14, (WorldSize.Large, WorldClassifier.Ammonia)
            },
            {
                15, (WorldSize.Large, WorldClassifier.Greenhouse)
            },
            {
                16, (WorldSize.Large, WorldClassifier.Greenhouse)
            },
            {
                17, (WorldSize.Large, WorldClassifier.Chthonian)
            },
            {
                18, (WorldSize.Large, WorldClassifier.Chthonian)
            }
        };

        public static IReadOnlyDictionary<int, (WorldSize, WorldClassifier)> BarrenWorldTable = new Dictionary<int, (WorldSize, WorldClassifier)>
        {
            {
                3, (WorldSize.Small, WorldClassifier.Hadean)
            },
            {
                4, (WorldSize.Small, WorldClassifier.Ice)
            },
            {
                5, (WorldSize.Small, WorldClassifier.Rock)
            },
            {
                6, (WorldSize.Small, WorldClassifier.Rock)
            },
            {
                7, (WorldSize.Tiny, WorldClassifier.Rock)
            },
            {
                8, (WorldSize.Tiny, WorldClassifier.Rock)
            },
            {
                9, (WorldSize.Tiny, WorldClassifier.Ice)
            },
            {
                10, (WorldSize.Tiny, WorldClassifier.Ice)
            },
            {
                11, (WorldSize.None, WorldClassifier.AsteroidBelt)
            },
            {
                12, (WorldSize.None, WorldClassifier.AsteroidBelt)
            },
            {
                13, (WorldSize.Standard, WorldClassifier.Ocean)
            },
            {
                14, (WorldSize.Standard, WorldClassifier.Ocean)
            },
            {
                15, (WorldSize.Standard, WorldClassifier.Ice)
            },
            {
                16, (WorldSize.Standard, WorldClassifier.Hadean)
            },
            {
                17, (WorldSize.Large, WorldClassifier.Ocean)
            },
            {
                18, (WorldSize.Large, WorldClassifier.Ice)
            }
        };

        public static IReadOnlyDictionary<int, (WorldSize, WorldClassifier)> GardenWorldTable = new Dictionary<int, (WorldSize, WorldClassifier)>
        {
            {
                3, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                4, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                5, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                6, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                7, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                8, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                9, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                10, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                11, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                12, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                13, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                14, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                15, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                16, (WorldSize.Standard, WorldClassifier.Garden)
            },
            {
                17, (WorldSize.Large, WorldClassifier.Garden)
            },
            {
                18, (WorldSize.Large, WorldClassifier.Garden)
            }
        };

        public enum AtmosphericPressureCategory
        {
            Trace,
            VeryThin,
            Thin,
            Standard,
            Dense,
            VeryDense,
            Superdense
        }

        public static AtmosphericPressureCategory GetPressureCategory(double atmosphericPressure)
        {
            return atmosphericPressure switch
            {
                < 0.01 => AtmosphericPressureCategory.Trace,
                >= 0.01 and <= 0.5 => AtmosphericPressureCategory.VeryThin,
                > 0.5 and <= 0.8 => AtmosphericPressureCategory.Thin,
                > 0.8 and <= 1.2 => AtmosphericPressureCategory.Standard,
                > 1.2 and <= 1.5 => AtmosphericPressureCategory.Dense,
                > 1.5 and <= 10 => AtmosphericPressureCategory.VeryDense,
                > 10 => AtmosphericPressureCategory.Superdense,
                double.NaN => AtmosphericPressureCategory.Standard
            };
        }

        public static IDictionary<(WorldSize, WorldClassifier), (Range TemperatureRange, int TemperatureStep)> AverageSurfaceTemperatureStepTable = new Dictionary<(WorldSize, WorldClassifier), (Range, int)>
        {
            {
                (WorldSize.None, WorldClassifier.AsteroidBelt), (new(140, 500), 24)
            },
            {
                (WorldSize.Tiny, WorldClassifier.Ice), (new(80, 140), 4)
            },
            {
                (WorldSize.Tiny, WorldClassifier.Sulfur), (new(80, 140), 4)
            },
            {
                (WorldSize.Tiny, WorldClassifier.Rock), (new(140, 500), 24)
            },
            {
                (WorldSize.Small, WorldClassifier.Hadean), (new(50, 80), 2)
            },
            {
                (WorldSize.Small, WorldClassifier.Ice), (new(80, 140), 4)
            },
            {
                (WorldSize.Small, WorldClassifier.Rock), (new(140, 500), 24)
            },
            {
                (WorldSize.Standard, WorldClassifier.Hadean), (new(50, 80), 2)
            },
            {
                (WorldSize.Standard, WorldClassifier.Ammonia), (new(140, 215), 5)
            },
            {
                (WorldSize.Standard, WorldClassifier.Ice), (new(80, 230), 10)
            },
            {
                (WorldSize.Standard, WorldClassifier.Ocean), (new(250, 340), 6)
            },
            {
                (WorldSize.Standard, WorldClassifier.Garden), (new(250, 340), 6)
            },
            {
                (WorldSize.Standard, WorldClassifier.Greenhouse), (new(500, 950), 30)
            },
            {
                (WorldSize.Standard, WorldClassifier.Chthonian), (new(500, 950), 30)
            },
            {
                (WorldSize.Large, WorldClassifier.Ammonia), (new(140, 215), 5)
            },
            {
                (WorldSize.Large, WorldClassifier.Ice), (new(80, 230), 10)
            },
            {
                (WorldSize.Large, WorldClassifier.Ocean), (new(230, 340), 6)
            },
            {
                (WorldSize.Large, WorldClassifier.Garden), (new(230, 340), 6)
            },
            {
                (WorldSize.Large, WorldClassifier.Greenhouse), (new(500, 950), 30)
            },
            {
                (WorldSize.Large, WorldClassifier.Chthonian), (new(500, 950), 30)
            },
        };

        public enum WorldClimateType
        {
            Frozen,
            VeryCold,
            Cold,
            Chilly,
            Cool,
            Normal,
            Warm,
            Tropical,
            Hot,
            VeryHot,
            Infernal
        }

        public static WorldClimateType GetClimateType(double temperatureInK)
        {
            return temperatureInK switch
            {
                < 244 => WorldClimateType.Frozen,
                >= 244 and <= 255 => WorldClimateType.VeryCold,
                > 255 and <= 266 => WorldClimateType.Cold,
                > 266 and <= 278 => WorldClimateType.Chilly,
                > 278 and <= 289 => WorldClimateType.Cool,
                > 289 and <= 300 => WorldClimateType.Normal,
                > 300 and <= 311 => WorldClimateType.Warm,
                > 311 and <= 322 => WorldClimateType.Tropical,
                > 322 and <= 333 => WorldClimateType.Hot,
                > 333 and <= 344 => WorldClimateType.VeryCold,
                > 344 => WorldClimateType.Infernal,
                double.NaN => WorldClimateType.Normal
            };
        }

        public static double GetBlackbodyCorrection(WorldSize worldSize, WorldClassifier worldClassifier, double atmosphericMass, double? hydrographics)
        {
            return (worldSize, worldClassifier) switch
            {
                (WorldSize.None, WorldClassifier.AsteroidBelt) => CalculateBlackbodyCorrection(0.97, 0, atmosphericMass),
                (WorldSize.Tiny, WorldClassifier.Ice) => CalculateBlackbodyCorrection(0.86, 0, atmosphericMass),
                (WorldSize.Tiny, WorldClassifier.Rock) => CalculateBlackbodyCorrection(0.97, 0, atmosphericMass),
                (WorldSize.Tiny, WorldClassifier.Sulfur) => CalculateBlackbodyCorrection(0.77, 0, atmosphericMass),
                (WorldSize.Small, WorldClassifier.Hadean) => CalculateBlackbodyCorrection(0.67, 0, atmosphericMass),
                (WorldSize.Small, WorldClassifier.Ice) => CalculateBlackbodyCorrection(0.93, 0.1, atmosphericMass),
                (WorldSize.Small, WorldClassifier.Rock) => CalculateBlackbodyCorrection(0.96, 0, atmosphericMass),
                (WorldSize.Standard, WorldClassifier.Hadean) => CalculateBlackbodyCorrection(0.67, 0, atmosphericMass),
                (WorldSize.Standard, WorldClassifier.Ammonia) or (WorldSize.Large, WorldClassifier.Ammonia) => CalculateBlackbodyCorrection(0.84, 0.2, atmosphericMass),
                (WorldSize.Standard, WorldClassifier.Ice) or (WorldSize.Large, WorldClassifier.Ice) => CalculateBlackbodyCorrection(0.86, 0.2, atmosphericMass),
                (WorldSize.Standard, WorldClassifier.Ocean) or (WorldSize.Large, WorldClassifier.Ocean) => hydrographics switch
                {
                    <= 0.2 => CalculateBlackbodyCorrection(0.95, 0.16, atmosphericMass),
                    > 0.2 and <= 0.5 => CalculateBlackbodyCorrection(0.92, 0.16, atmosphericMass),
                    > 0.5 and <= 0.9 => CalculateBlackbodyCorrection(0.88, 0.160, atmosphericMass),
                    > 0.9 => CalculateBlackbodyCorrection(0.84, 0.16, atmosphericMass),
                    _ => CalculateBlackbodyCorrection(0.88, 0.16, atmosphericMass),
                },
                (WorldSize.Standard, WorldClassifier.Greenhouse) or (WorldSize.Large, WorldClassifier.Greenhouse) => CalculateBlackbodyCorrection(0.77, 2, atmosphericMass),
                (WorldSize.Standard, WorldClassifier.Chthonian) or (WorldSize.Large, WorldClassifier.Chthonian) => CalculateBlackbodyCorrection(0.97, 0, atmosphericMass),
                _ => CalculateBlackbodyCorrection(0.88, 0.160, atmosphericMass)
            };
        }

        private static double CalculateBlackbodyCorrection(double absorptionFactor, double greenhouseFactor, double atmosphericMass)
        {
            return absorptionFactor * (1 + atmosphericMass * greenhouseFactor);
        }

        public enum WorldCoreType
        {
            Icy,
            SmallIron,
            LargeIron
        }

        public static WorldCoreType GetWorldCoreType(WorldSize worldSize, WorldClassifier worldClassifier)
        {
            return (worldSize, worldClassifier) switch
            {
                (WorldSize.Tiny, WorldClassifier.Sulfur) => WorldCoreType.Icy,
                (WorldSize.Tiny, WorldClassifier.Ice) => WorldCoreType.Icy,
                (WorldSize.Small, WorldClassifier.Hadean) => WorldCoreType.Icy,
                (WorldSize.Small, WorldClassifier.Ice) => WorldCoreType.Icy,
                (WorldSize.Standard, WorldClassifier.Hadean) => WorldCoreType.Icy,
                (WorldSize.Standard, WorldClassifier.Ammonia) => WorldCoreType.Icy,
                (WorldSize.Large, WorldClassifier.Ammonia) => WorldCoreType.Icy,

                (WorldSize.Tiny, WorldClassifier.Rock) => WorldCoreType.SmallIron,
                (WorldSize.Small, WorldClassifier.Rock) => WorldCoreType.SmallIron,

                _ => WorldCoreType.LargeIron
            };
        }

        public static IReadOnlyDictionary<Range, double> IcyCoreDensityTable = new Dictionary<Range, double>()
        {
            {
                new(3, 6), 0.3
            },
            {
                new(7, 10), 0.4
            },
            {
                new(11, 14), 0.5
            },
            {
                new(15, 17), 0.6
            },
            {
                new(18, 18), 0.7
            },
        };

        public static IReadOnlyDictionary<Range, double> SmallIronCoreDensityTable = new Dictionary<Range, double>()
        {
            {
                new(3, 6), 0.6
            },
            {
                new(7, 10), 0.7
            },
            {
                new(11, 14), 0.8
            },
            {
                new(15, 17), 0.9
            },
            {
                new(18, 18), 1
            },
        };

        public static IReadOnlyDictionary<Range, double> LargeIronCoreDensityTable = new Dictionary<Range, double>()
        {
            {
                new(3, 6), 0.8
            },
            {
                new(7, 10), 0.9
            },
            {
                new(11, 14), 1
            },
            {
                new(15, 17), 1.1
            },
            {
                new(18, 18), 1.2
            },
        };

        public static IReadOnlyDictionary<WorldSize, (double MinimumSize, double MaximumSize)> SizeConstraintsTable = new Dictionary<WorldSize, (double MinimumSize, double MaximumSize)>
        {
            {
                WorldSize.Large, (0.065, 0.091)
            },
            {
                WorldSize.Standard, (0.03, 0.065)
            },
            {
                WorldSize.Small, (0.024, 0.03)
            },
            {
                WorldSize.Tiny, (0.004, 0.024)
            },
        };

        public static double GetPressureFactor(WorldSize size, WorldClassifier classifier)
        {
            return (size, classifier) switch
            {
                (WorldSize.Small, WorldClassifier.Ice) => 10,

                (WorldSize.Standard, WorldClassifier.Ammonia) => 1,
                (WorldSize.Standard, WorldClassifier.Ice) => 1,
                (WorldSize.Standard, WorldClassifier.Ocean) => 1,
                (WorldSize.Standard, WorldClassifier.Garden) => 1,

                (WorldSize.Standard, WorldClassifier.Greenhouse) => 100,

                (WorldSize.Large, WorldClassifier.Ammonia) => 5,
                (WorldSize.Large, WorldClassifier.Ice) => 5,
                (WorldSize.Large, WorldClassifier.Ocean) => 5,
                (WorldSize.Large, WorldClassifier.Garden) => 5,

                (WorldSize.Large, WorldClassifier.Greenhouse) => 500,
                _ => 0
            };
        }

        public static IReadOnlyDictionary<Range, (OverallResourceValue OverallValue, int ValueModifier)> AsteroidBeltResourceValueTable = new Dictionary<Range, (OverallResourceValue OverallValue, int ValueModifier)>
        {
            {
                new(3,3), (OverallResourceValue.Worthless, -5)
            },
            {
                new(4,4), (OverallResourceValue.VeryScant, -4)
            },
            {
                new(5,5), (OverallResourceValue.Scant, -3)
            },
            {
                new(6,7), (OverallResourceValue.VeryPoor, -2)
            },
            {
                new(8,9), (OverallResourceValue.Poor, -1)
            },
            {
                new(10,11), (OverallResourceValue.Average, 0)
            },
            {
                new(12,13), (OverallResourceValue.Abundant, 1)
            },
            {
                new(14,15), (OverallResourceValue.VeryAbundant, 2)
            },
            {
                new(16,16), (OverallResourceValue.Rich, 3)
            },
            {
                new(17,17), (OverallResourceValue.VeryRich, 4)
            },
            {
                new(18,18), (OverallResourceValue.Motherlode, 5)
            },
        };

        public static IReadOnlyDictionary<Range, (OverallResourceValue OverallValue, int ValueModifier)> WorldResourceValueTable = new Dictionary<Range, (OverallResourceValue OverallValue, int ValueModifier)>
        {
            {
                new(-999,2), (OverallResourceValue.Scant, -3)
            },
            {
                new(3,4), (OverallResourceValue.VeryPoor, -2)
            },
            {
                new(5,7), (OverallResourceValue.Poor, -1)
            },
            {
                new(8,13), (OverallResourceValue.Average, 0)
            },
            {
                new(14,16), (OverallResourceValue.Abundant, 1)
            },
            {
                new(17,18), (OverallResourceValue.VeryAbundant, 2)
            },
            {
                new(19,999), (OverallResourceValue.Rich, 3)
            }
        };

        public static IReadOnlyDictionary<(Range, GasGiantSize), (double Mass, double Density)> GasGiantMassDensityTable = new Dictionary<(Range, GasGiantSize), (double, double)>()
        {
            #region Small
            {
                (new(-999, 8), GasGiantSize.Small), (10, 0.42)
            },
            {
                (new(9, 10), GasGiantSize.Small), (15, 0.26)
            },
            {
                (new(11,11), GasGiantSize.Small), (20, 0.22)
            },
            {
                (new(12,12), GasGiantSize.Small), (30, 0.19)
            },
            {
                (new(13,13), GasGiantSize.Small), (40, 0.17)
            },
            {
                (new(14,14), GasGiantSize.Small), (50, 0.17)
            },
            {
                (new(15,15), GasGiantSize.Small), (60, 0.17)
            },
            {
                (new(16,16), GasGiantSize.Small), (70, 0.17)
            },
            {
                (new(17, 999), GasGiantSize.Small), (80, 0.17)
            },
            #endregion
            #region Medium
            {
                (new(-999, 8), GasGiantSize.Medium), (100, 0.18)
            },
            {
                (new(9, 10), GasGiantSize.Medium), (150, 0.19)
            },
            {
                (new(11,11), GasGiantSize.Medium), (200, 0.20)
            },
            {
                (new(12,12), GasGiantSize.Medium), (250, 0.22)
            },
            {
                (new(13,13), GasGiantSize.Medium), (300, 0.24)
            },
            {
                (new(14,14), GasGiantSize.Medium), (350, 0.25)
            },
            {
                (new(15,15), GasGiantSize.Medium), (400, 0.26)
            },
            {
                (new(16,16), GasGiantSize.Medium), (450, 0.27)
            },
            {
                (new(17, 999), GasGiantSize.Medium), (500, 0.28)
            },
            #endregion
            #region Large
            {
                (new(-999, 8), GasGiantSize.Large), (600, 0.31)
            },
            {
                (new(9, 10), GasGiantSize.Large), (800, 0.35)
            },
            {
                (new(11,11), GasGiantSize.Large), (1000, 0.4)
            },
            {
                (new(12,12), GasGiantSize.Large), (1500, 0.6)
            },
            {
                (new(13,13), GasGiantSize.Large), (2000, 0.8)
            },
            {
                (new(14,14), GasGiantSize.Large), (2500, 1)
            },
            {
                (new(15,15), GasGiantSize.Large), (3000, 1.2)
            },
            {
                (new(16,16), GasGiantSize.Large), (3500, 1.4)
            },
            {
                (new(17, 999), GasGiantSize.Large), (4000, 1.6)
            },
            #endregion
        };

        public static IReadOnlyDictionary<Range, double> PlanetaryOrbitalEccentricityTable = new Dictionary<Range, double>()
        {
            {
                new(-999, 3), 0
            },
            {
                new(4,6), 0.05
            },
            {
                new(7,9), 0.1
            },
            {
                new(10,11), 0.15
            },
            {
                new(12,12), 0.2
            },
            {
                new(13,13), 0.3
            },
            {
                new(14,14), 0.4
            },
            {
                new(15,15), 0.5
            },
            {
                new(16,16), 0.6
            },
            {
                new(17,17), 0.7
            },
            {
                new(18, 999), 0.8
            },
        };

        public static IReadOnlyDictionary<Range, int> MoonSizeTable = new Dictionary<Range, int>()
        {
            {
                new(-99, 11), -3
            },
            {
                new(12,14), -2
            },
            {
                new(15, 999), -1
            },
        };
    }
}

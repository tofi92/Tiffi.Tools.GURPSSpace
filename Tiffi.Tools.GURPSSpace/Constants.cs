using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiffi.Tools.GURPSSpace
{
    internal static class Constants
    {
        public static double RADIANS_PER_ROTATION = 2.0 * Math.PI;

        public static double SUN_AGE_IN_YEARS = 4600000000;
        public static double ECCENTRICITY_COEFF = 0.077;                       // Dole's was 0.077			
        public static double PROTOPLANET_MASS = 1.0E-15;                     // Units of solar masses	
        public static double CHANGE_IN_EARTH_ANG_VEL = -1.3E-15;                    // Units of radians/sec/year
        public static double SOLAR_MASS_IN_GRAMS = 1.989E33;                    // Units of grams			
        public static double SOLAR_MASS_IN_KILOGRAMS = 1.989E30;                    // Units of kg				
        public static double EARTH_MASS_IN_GRAMS = 5.977E27;                    // Units of grams			
        public static double EARTH_RADIUS = 6.378E8;                     // Units of cm				
        public static double EARTH_DENSITY = 5.52;                        // Units of g/cc			
        public static double KM_EARTH_RADIUS = 6378.0;                      // Units of km
        public static double KM_EARTH_DIAMETER = 12742.0;		
        public static double EARTH_ACCELERATION = 980.7;                       // Units of cm/sec2 (was 981.0)
        public static double EARTH_AXIAL_TILT = 23.4;                        // Units of degrees	
        public static double EARTH_GRAVITY = 9.807;                         //Units of m/s²		
        public static double EARTH_EXOSPHERE_TEMP = 1273.0;                      // Units of degrees Kelvin	
        public static double SUN_MASS_IN_EARTH_MASSES = 332775.64;
        public static double ASTEROID_MASS_LIMIT = 0.001;                       // Units of Earth Masses	
        public static double EARTH_EFFECTIVE_TEMP = 250.0;                       // Units of degrees Kelvin (was 255);	
        public static double CLOUD_COVERAGE_FACTOR = 1.839E-8;                    // Km2/kg					
        public static double EARTH_WATER_MASS_PER_AREA = 3.83E15;                     // grams per square km		
        public static double EARTH_SURF_PRES_IN_MILLIBARS = 1013.25;
        public static double EARTH_SURF_PRES_IN_MMHG = 760.0;                       // Dole p. 15				
        public static double EARTH_SURF_PRES_IN_PSI = 14.696;                      // Pounds per square inch	
        public static double MMHG_TO_MILLIBARS = EARTH_SURF_PRES_IN_MILLIBARS / EARTH_SURF_PRES_IN_MMHG;
        public static double PSI_TO_MILLIBARS = EARTH_SURF_PRES_IN_MILLIBARS / EARTH_SURF_PRES_IN_PSI;
        public static double H20_ASSUMED_PRESSURE = 47.0 * MMHG_TO_MILLIBARS;    // Dole p. 15      
        public static double PPM_PRSSURE = EARTH_SURF_PRES_IN_MILLIBARS / 1000000.0;
        public static double SOLAR_LUMINOSITY_IN_WATT = 3.828e26;
        public static double EARTH_ATMOSPHERIC_MASS = 5.15e18;
        public static double SOLAR_RADIUS_IN_KM = 696342;

        public static double MOON_TO_EARTH_TIDAL_ACCELERATION = 1.1e-6; //in meters per second squared
        public static double EARTH_TO_MOON_TIDAL_ACCELERATION = 2.44e-5;

        public static double CM_PER_AU = 1.495978707E13;              // number of cm in an AU	
        public static double CM_PER_KM = 1.0E5;                       // number of cm in a km		
        public static double KM_PER_AU = CM_PER_AU / CM_PER_KM;
    }
}

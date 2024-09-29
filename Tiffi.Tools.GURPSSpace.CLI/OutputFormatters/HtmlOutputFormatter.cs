using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiffi.Tools.GURPSSpace.Models;

namespace Tiffi.Tools.GURPSSpace.CLI.OutputFormatters
{
    public class HtmlOutputFormatter : IOutputFormatter
    {
        public static double CM_PER_AU = 1.495978707E13;              // number of cm in an AU	
        public static double CM_PER_KM = 1.0E5;                       // number of cm in a km		
        public static double KM_PER_AU = CM_PER_AU / CM_PER_KM;

        public async Task<string> Format(StarSystem starSystem)
        {
            var size = 1500;
            var sunSize = 10;
            var maxOrbitInAU = starSystem.Worlds.Max(x => x.OrbitalRadius / KM_PER_AU);
            var minOrbitInAU = starSystem.Worlds.Min(x => x.OrbitalRadius / KM_PER_AU);


            var orbitScaling = size / 2 / maxOrbitInAU * 0.99;
            var sizeScaling = sunSize / starSystem.PrimaryStar.Radius * 2;

            var doc = new HtmlDocument();
            var style = doc.CreateElement("style");
            style.InnerHtml = ".t { fill: white; font: 10px sans-serif }";
            doc.DocumentNode.AppendChild(style);
            var svg = doc.CreateElement("svg");
            svg.SetAttributeValue("height", $"{size}");
            svg.SetAttributeValue("width", $"{size}");

            var bg = doc.CreateElement("rect");
            bg.SetAttributeValue("width", "100%");
            bg.SetAttributeValue("height", "100%");
            bg.SetAttributeValue("fill", "black");
            svg.AppendChild(bg);

            var seed = doc.CreateElement("text");
            seed.InnerHtml = $"Seed: {starSystem.Seed}";
            seed.SetAttributeValue("class", "t");
            seed.SetAttributeValue("x", "5");
            seed.SetAttributeValue("y", "10");
            svg.AppendChild(seed);

            var sun = doc.CreateElement("circle");
            sun.SetAttributeValue("cx", "50%");
            sun.SetAttributeValue("cy", "50%");
            sun.SetAttributeValue("r", $"{sunSize}");
            sun.SetAttributeValue("fill", "yellow");
            svg.AppendChild(sun);


            foreach (var world in starSystem.Worlds)
            {
                var orbit = world.OrbitalRadius / KM_PER_AU * orbitScaling;
                var planetSize = world.Diameter * sizeScaling;

                var orbitPath = doc.CreateElement("circle");
                orbitPath.SetAttributeValue("cx", "50%");
                orbitPath.SetAttributeValue("cy", "50%");
                orbitPath.SetAttributeValue("r", $"{orbit.ToString(CultureInfo.InvariantCulture)}");
                orbitPath.SetAttributeValue("fill", "transparent");
                orbitPath.SetAttributeValue("stroke", "white");
                orbitPath.SetAttributeValue("stroke-width", "0.2");

                var planetSvg = doc.CreateElement("circle");
                planetSvg.SetAttributeValue("cx", $"{((size / 2) + orbit).ToString(CultureInfo.InvariantCulture)}");
                planetSvg.SetAttributeValue("cy", "50%");
                planetSvg.SetAttributeValue("r", $"{planetSize.ToString(CultureInfo.InvariantCulture)}");
                planetSvg.SetAttributeValue("fill", GetColor(world));
                planetSvg.SetAttributeValue("data-type", world.GetType().Name);

                svg.AppendChild(orbitPath);
                svg.AppendChild(planetSvg);
            }



            doc.DocumentNode.AppendChild(svg);
            using var sw = new StringWriter();
            doc.DocumentNode.WriteContentTo(sw);
            return sw.ToString();
        }

        private string GetColor(World world)
        {
            if (world is Planet planet)
            {
                return planet.Classifier switch
                {
                    WorldClassifier.Sulfur => "orange",
                    WorldClassifier.Ammonia => "gray",
                    WorldClassifier.Hadean => "darkkhaki",
                    WorldClassifier.Ocean => "blue",
                    WorldClassifier.Ice => "lightsteelblue",
                    WorldClassifier.Garden => "green",
                    WorldClassifier.Greenhouse => "palegoldenrod",
                    WorldClassifier.Rock => "darkslategrey",
                    WorldClassifier.AsteroidBelt => "transparent",
                    WorldClassifier.Chthonian => "dimgrey",
                    _ => throw new NotImplementedException()
                };
            }
            else if (world is GasGiant)
            {
                return "cyan";
            }
            return "transparent";
        }
    }
}

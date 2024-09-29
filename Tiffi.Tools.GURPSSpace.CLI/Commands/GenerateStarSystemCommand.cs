using HtmlAgilityPack;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Tiffi.Tools.GURPSSpace.CLI.OutputFormatters;
using Tiffi.Tools.GURPSSpace.Models;
using Tiffi.Tools.GURPSSpace.Options;

namespace Tiffi.Tools.GURPSSpace.CLI.Commands
{
    public class GenerateStarSystemCommand : AsyncCommand<GenerateStarSystemCommand.Settings>
    {
        public static double CM_PER_AU = 1.495978707E13;              // number of cm in an AU	
        public static double CM_PER_KM = 1.0E5;                       // number of cm in a km		
        public static double KM_PER_AU = CM_PER_AU / CM_PER_KM;
        public sealed class Settings : CommandSettings
        {
            [Description("Force a garden world to be generated")]
            [CommandOption("-g|--garden")]
            [DefaultValue(false)]
            public bool ForceGardenWorld { get; init; }

            [Description("Set the random seed")]
            [CommandOption("-s|--seed")]
            [DefaultValue(null)]
            public int? Seed { get; init; }

            [Description("If multiple stars should be created (unstable feature)")]
            [CommandOption("--multiple-stars")]
            [DefaultValue(false)]
            public bool MultipleStars { get; init; }

            [Description("Output of the star system")]
            [CommandOption("-m|--mode")]
            [DefaultValue(OutputMode.Json)]
            public OutputMode OutputMode { get; init; }

            [Description("Path to save the system to. Defaults to current directory.")]
            [CommandArgument(0, "[outputPath]")]
            public string? OutputPath { get; init; }
        }

        public enum OutputMode
        {
            Json,
            Xml,
            Html
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            var options = new GeneratorOptions();
            if (settings.Seed.HasValue)
            {
                options.Seed = settings.Seed.Value;
            }
            options.ForceHabitablePlanet = settings.ForceGardenWorld;
            options.MultipleStars = settings.MultipleStars;

            var generator = new Generator(options);
            var starSystem = generator.Generate();


            var path = "NewStarSystem." + settings.OutputMode.ToString().ToLower();
            int tries = 1;
            while (File.Exists(path))
            {
                path = $"NewStarSystem({tries})." + settings.OutputMode.ToString().ToLower();
                tries++;
            }

            if (settings.OutputPath != null)
            {
                path = settings.OutputPath;
            }

            return settings.OutputMode switch
            {
                OutputMode.Json => await SaveAsJson(starSystem, path),
                OutputMode.Xml => SaveAsXml(starSystem, path),
                OutputMode.Html => await SaveAsHtml(starSystem, path),
                _ => throw new NotImplementedException()
            };

        }

        private static async Task<int> SaveAsHtml(StarSystem starSystem, string path)
        {
            var html = await new HtmlOutputFormatter().Format(starSystem);
            await File.WriteAllTextAsync(path, html);
            return 0;
        }

        private static int SaveAsXml(StarSystem starSystem, string path)
        {
            var serializer = new XmlSerializer(typeof(StarSystem));
            var file = File.OpenWrite(path);
            serializer.Serialize(file, starSystem);
            file.Close();
            return 0;
        }

        private static async Task<int> SaveAsJson(StarSystem starSystem, string path)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.NumberHandling = JsonNumberHandling.Strict;
            options.WriteIndented = true;
            var json = JsonSerializer.Serialize(starSystem, options);

            await File.WriteAllTextAsync(path, json);

            return 0;
        }
    }


}

using Tiffi.Tools.GURPSSpace.CLI.OutputFormatters;

namespace Tiffi.Tools.GURPSSpace.Tests.CLI
{
    public class TestHtmlGenerator
    {
        [Fact]
        public async Task TestHtmlOuput()
        {
            var generator = new Generator();
            var starSystem = generator.Generate(new Options.GeneratorOptions() { Seed = 131313 });

            var formatter = new HtmlOutputFormatter();
            var res = await formatter.Format(starSystem);
        }
    }
}

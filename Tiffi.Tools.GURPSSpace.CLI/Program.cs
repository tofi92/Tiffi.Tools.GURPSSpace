using Spectre.Console.Cli;
using Tiffi.Tools.GURPSSpace.CLI.Commands;

var app = new CommandApp();
app.Configure(c =>
{
    c.AddCommand<GenerateStarSystemCommand>("GenerateStarSystem");
});
return app.Run(args);
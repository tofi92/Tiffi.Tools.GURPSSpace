using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tiffi.Tools.GURPSSpace.Models;

namespace Tiffi.Tools.GURPSSpace.CLI.OutputFormatters
{
    internal class JsonOutputFormatter : IOutputFormatter
    {
        public Task<string> Format(StarSystem starSystem)
        {
            return Task.FromResult(JsonSerializer.Serialize(starSystem));
        }
    }
}

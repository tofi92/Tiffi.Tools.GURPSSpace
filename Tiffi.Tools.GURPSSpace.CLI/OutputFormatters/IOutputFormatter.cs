using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiffi.Tools.GURPSSpace.Models;

namespace Tiffi.Tools.GURPSSpace.CLI.OutputFormatters
{
    public interface IOutputFormatter
    {
        Task<string> Format(StarSystem starSystem);
    }
}

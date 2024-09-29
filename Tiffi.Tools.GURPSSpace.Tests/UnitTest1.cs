using System.Diagnostics;
using Tiffi.Tools.GURPSSpace.Models;

namespace Tiffi.Tools.GURPSSpace.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var generator = new Generator();
            
            List<StarSystem> systems = [];
            for(int x = 0; x < 100; x++)
            {
                for(int y = 0; y < 100; y++)
                {
                    for(int z = 0; z < 5; z++)
                    {
                        if (x + y + z % 15 == 0)
                        {
                            systems.Add(generator.Generate(new Options.GeneratorOptions() { ForceHabitablePlanet = true}));
                        }
                        else
                        {
                            systems.Add(generator.Generate());
                        }
                        
                    }
                }
            }
            

        }
    }
}
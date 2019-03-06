using System;
using System.Diagnostics;
using Factory.Alloying;
using Factory.Resources;

namespace Factory.Production
{
    class Program
    {
        static void Main(string[] args)
        {
            var bronzeAlloyer = new AlloyForge<BronzeIngot, CopperIngot, ZincIngot>(20);
            var lightBronzeAlloyer = new AlloyForge<LightBronzeIngot, BronzeIngot, AluminiumIngot>(15);
            
            var stopWatch = Stopwatch.StartNew();

            var copperIngots = FakeForge.ForgeIngots<CopperIngot>(30, 20);
            var zincIngots = FakeForge.ForgeIngots<ZincIngot>(10, 5);

            var bronzeIngots = bronzeAlloyer.Forge(copperIngots, zincIngots);

            var aluminiumIngots = FakeForge.ForgeIngots<AluminiumIngot>(15, 3);

            var lightBronzeIngots = lightBronzeAlloyer.Forge(bronzeIngots, aluminiumIngots);
            
            stopWatch.Stop();
            
            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeIngots.Length}");
        }
    }
}

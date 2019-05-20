using System;
using System.Diagnostics;
using Factory.Alloying;
using Factory.Resources;

namespace Factory.Production
{
    static class Program
    {
        static void Main(string[] args)
        {
            var bronzeAlloyer = new AlloyForge<BronzeIngot, CopperIngot, ZincIngot>(20);
            var lightBronzeAlloyer = new AlloyForge<LightBronzeIngot, BronzeIngot, AluminiumIngot>(15);
            
            var stopWatch = Stopwatch.StartNew();

            LogForgeStarting<CopperIngot>(stopWatch);
            var copperIngots = FakeForge.ForgeIngots<CopperIngot>(30, 20);
            LogForgeFinished<CopperIngot>(stopWatch);
            
            LogForgeStarting<ZincIngot>(stopWatch);
            var zincIngots = FakeForge.ForgeIngots<ZincIngot>(10, 5);
            LogForgeFinished<ZincIngot>(stopWatch);

            LogForgeStarting<BronzeIngot>(stopWatch);
            var bronzeIngots = bronzeAlloyer.Forge(copperIngots, zincIngots);
            LogForgeFinished<BronzeIngot>(stopWatch);

            LogForgeStarting<AluminiumIngot>(stopWatch);
            var aluminiumIngots = FakeForge.ForgeIngots<AluminiumIngot>(15, 3);
            LogForgeFinished<AluminiumIngot>(stopWatch);

            LogForgeStarting<LightBronzeIngot>(stopWatch);
            var lightBronzeIngots = lightBronzeAlloyer.Forge(bronzeIngots, aluminiumIngots);
            LogForgeFinished<LightBronzeIngot>(stopWatch);
            
            stopWatch.Stop();
            
            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeIngots.Length}");
        }

        private static void LogForgeStarting<TIngot>(Stopwatch stopwatch)
            where TIngot : Ingot, new()
        {
            Console.WriteLine($"Starting {typeof(TIngot).Name} forging at: {stopwatch.ElapsedMilliseconds}");
        }

        private static void LogForgeFinished<TIngot>(Stopwatch stopwatch)
            where TIngot : Ingot, new()
        {
            Console.WriteLine($"{typeof(TIngot).Name} forging finished at: {stopwatch.ElapsedMilliseconds}");
        }
    }
}

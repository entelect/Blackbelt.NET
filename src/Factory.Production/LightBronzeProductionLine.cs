using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Factory.Alloying;
using Factory.Resources;

namespace Factory.Production
{
    public class LightBronzeProductionLine
    {
        private const double CopperIngotWeight = 30;
        private const int CopperIngotCount = 20;

        private const double ZincIngotWeight = 10;
        private const int ZincIngotCount = 5;

        private const double AluminiumIngotWeight = 15;
        private const int AluminiumIngotCount = 3;
        
        private readonly AlloyForge<Bronze, Copper, Zinc> bronzeAlloyer;
        private readonly AlloyForge<LightBronze, Bronze, Aluminium> lightBronzeAlloyer;

        public LightBronzeProductionLine()
        {
            this.bronzeAlloyer = new AlloyForge<Bronze, Copper, Zinc>(20);
            this.lightBronzeAlloyer = new AlloyForge<LightBronze, Bronze, Aluminium>(15);
        }
        
        public void ForgeSerial()
        {
            var stopWatch = Stopwatch.StartNew();

            stopWatch.LogForgeStarting<Copper>();
            var copperIngots = FakeForge.ForgeIngots<Copper>(CopperIngotWeight, CopperIngotCount);
            stopWatch.LogForgeFinished<Copper>();

            stopWatch.LogForgeStarting<Zinc>();
            var zincIngots = FakeForge.ForgeIngots<Zinc>(ZincIngotWeight, ZincIngotCount);
            stopWatch.LogForgeFinished<Zinc>();

            stopWatch.LogForgeStarting<Bronze>();
            var bronzeIngots = bronzeAlloyer.Forge(copperIngots, zincIngots);
            stopWatch.LogForgeFinished<Bronze>();

            stopWatch.LogForgeStarting<Aluminium>();
            var aluminiumIngots = FakeForge.ForgeIngots<Aluminium>(AluminiumIngotWeight, AluminiumIngotCount);
            stopWatch.LogForgeFinished<Aluminium>();

            stopWatch.LogForgeStarting<LightBronze>();
            var lightBronzeIngots = lightBronzeAlloyer.Forge(bronzeIngots, aluminiumIngots);
            stopWatch.LogForgeFinished<LightBronze>();

            stopWatch.Stop();

            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeIngots.Length}");
        }

        public Task ForgeParallel()
        {
            var stopWatch = Stopwatch.StartNew();
            
            var copperTask = Task.Run(() => stopWatch.LogForgeStarting<Copper>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<Copper>(CopperIngotWeight, CopperIngotCount))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<Copper>();
                    return t;
                })
                .Unwrap();
            
            var zincTask = Task.Run(() => stopWatch.LogForgeStarting<Zinc>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<Zinc>(ZincIngotWeight, ZincIngotCount))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<Zinc>();
                    return t;
                })
                .Unwrap();

            var bronzeTask = Task.WhenAll(copperTask, zincTask)
                .ContinueWith(t => stopWatch.LogForgeStarting<Bronze>())
                .ContinueWith(t => bronzeAlloyer.ForgeAsync(copperTask.Result, zincTask.Result))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<Bronze>();
                    return t;
                })
                .Unwrap();

            var aluminiumTask = Task.Run(() => stopWatch.LogForgeStarting<Aluminium>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<Aluminium>(AluminiumIngotWeight, AluminiumIngotCount))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<Aluminium>();
                    return t;
                })
                .Unwrap();

            var lightBronzeTask = Task.Run(() =>
                {
                    bronzeTask.Wait();
                    aluminiumTask.Wait();
                    return (bronzeIngots: bronzeTask.Result, aluminiumIngots: aluminiumTask.Result);
                })
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeStarting<LightBronze>();
                    return t;
                })
                .Unwrap()
                .ContinueWith(
                    t =>
                    {
                        var (bronzeIngots, aluminiumIngots) = t.Result;

                        return lightBronzeAlloyer.ForgeAsync(bronzeIngots, aluminiumIngots);
                    })
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<LightBronze>();
                    return t;
                })
                .Unwrap();

            lightBronzeTask.Wait();

            stopWatch.Stop();

            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeTask.Result.Length}");

            return lightBronzeTask;
        }

        public async Task ForgeParallelAsync()
        {
            throw new NotImplementedException();
        }
    }
}
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
        
        private readonly AlloyForge<IBronze, ICopper, IZinc> bronzeAlloyer;
        private readonly AlloyForge<ILightBronze, IBronze, IAluminium> lightBronzeAlloyer;

        public LightBronzeProductionLine()
        {
            this.bronzeAlloyer = new AlloyForge<IBronze, ICopper, IZinc>(20);
            this.lightBronzeAlloyer = new AlloyForge<ILightBronze, IBronze, IAluminium>(15);
        }
        
        public void ForgeSerial()
        {
            var stopWatch = Stopwatch.StartNew();

            stopWatch.LogForgeStarting<ICopper>();
            var copperIngots = FakeForge.ForgeIngots<ICopper>(CopperIngotWeight, CopperIngotCount);
            stopWatch.LogForgeFinished<ICopper>();

            stopWatch.LogForgeStarting<IZinc>();
            var zincIngots = FakeForge.ForgeIngots<IZinc>(ZincIngotWeight, ZincIngotCount);
            stopWatch.LogForgeFinished<IZinc>();

            stopWatch.LogForgeStarting<IBronze>();
            var bronzeIngots = bronzeAlloyer.Forge(copperIngots, zincIngots);
            stopWatch.LogForgeFinished<IBronze>();

            stopWatch.LogForgeStarting<IAluminium>();
            var aluminiumIngots = FakeForge.ForgeIngots<IAluminium>(AluminiumIngotWeight, AluminiumIngotCount);
            stopWatch.LogForgeFinished<IAluminium>();

            stopWatch.LogForgeStarting<ILightBronze>();
            var lightBronzeIngots = lightBronzeAlloyer.Forge(bronzeIngots, aluminiumIngots);
            stopWatch.LogForgeFinished<ILightBronze>();

            stopWatch.Stop();

            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeIngots.Length}");
        }

        public Task ForgeParallel()
        {
            var stopWatch = Stopwatch.StartNew();
            
            var copperTask = Task.Run(() => stopWatch.LogForgeStarting<ICopper>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<ICopper>(CopperIngotWeight, CopperIngotCount))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<ICopper>();
                    return t;
                })
                .Unwrap();
            
            var zincTask = Task.Run(() => stopWatch.LogForgeStarting<IZinc>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<IZinc>(ZincIngotWeight, ZincIngotCount))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<IZinc>();
                    return t;
                })
                .Unwrap();

            var bronzeTask = Task.WhenAll(copperTask, zincTask)
                .ContinueWith(t => stopWatch.LogForgeStarting<IBronze>())
                .ContinueWith(t => bronzeAlloyer.ForgeAsync(copperTask.Result, zincTask.Result))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<IBronze>();
                    return t;
                })
                .Unwrap();

            var aluminiumTask = Task.Run(() => stopWatch.LogForgeStarting<IAluminium>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<IAluminium>(AluminiumIngotWeight, AluminiumIngotCount))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<IAluminium>();
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
                    stopWatch.LogForgeStarting<ILightBronze>();
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
                    stopWatch.LogForgeFinished<ILightBronze>();
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
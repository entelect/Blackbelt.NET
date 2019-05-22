using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Factory.Alloying;
using Factory.Resources;

namespace Factory.Production
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting serial forge");
            ForgeSerial();
            
            Console.WriteLine("\n\n\nStarting concurrent forge");
            await ForgeConcurrently();

            Console.WriteLine("\n\n\nStarting concurrent async await");
            await ForgeConcurrentWithAsyncAwait();
        }

        private static void ForgeSerial()
        {
            var bronzeAlloyer = new AlloyForge<IBronze, ICopper, IZinc>(20);
            var lightBronzeAlloyer = new AlloyForge<ILightBronze, IBronze, IAluminium>(15);

            var stopWatch = Stopwatch.StartNew();

            stopWatch.LogForgeStarting<ICopper>();
            var copperIngots = FakeForge.ForgeIngots<ICopper>(30, 20);
            stopWatch.LogForgeFinished<ICopper>();

            stopWatch.LogForgeStarting<IZinc>();
            var zincIngots = FakeForge.ForgeIngots<IZinc>(10, 5);
            stopWatch.LogForgeFinished<IZinc>();

            stopWatch.LogForgeStarting<IBronze>();
            var bronzeIngots = bronzeAlloyer.Forge(copperIngots, zincIngots);
            stopWatch.LogForgeFinished<IBronze>();

            stopWatch.LogForgeStarting<IAluminium>();
            var aluminiumIngots = FakeForge.ForgeIngots<IAluminium>(15, 3);
            stopWatch.LogForgeFinished<IAluminium>();

            stopWatch.LogForgeStarting<ILightBronze>();
            var lightBronzeIngots = lightBronzeAlloyer.Forge(bronzeIngots, aluminiumIngots);
            stopWatch.LogForgeFinished<ILightBronze>();

            stopWatch.Stop();

            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeIngots.Length}");
        }

        private static Task ForgeConcurrently()
        {
            var bronzeAlloyer = new AlloyForge<IBronze, ICopper, IZinc>(20);
            var lightBronzeAlloyer = new AlloyForge<ILightBronze, IBronze, IAluminium>(15);

            var stopWatch = Stopwatch.StartNew();
            
            var copperTask = Task.Run(() => stopWatch.LogForgeStarting<ICopper>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<ICopper>(30, 20))
                .Unwrap()
                .ContinueWith(t =>
                {
                    stopWatch.LogForgeFinished<ICopper>();
                    return t;
                })
                .Unwrap();
            
            var zincTask = Task.Run(() => stopWatch.LogForgeStarting<IZinc>())
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<IZinc>(10, 5))
                .Unwrap()
                .ContinueWith(t =>
                {
                    LogForgeFinished<IZinc>(stopWatch);
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
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<IAluminium>(15, 3))
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

        private static async Task ForgeConcurrentWithAsyncAwait()
        {
            throw new NotImplementedException();
        }

        private static void LogForgeStarting<TIngot>(this Stopwatch stopwatch)
            where TIngot : IMetal
        {
            Console.WriteLine($"Starting {typeof(TIngot).Name} forging at: {stopwatch.ElapsedMilliseconds}");
        }

        private static void LogForgeFinished<TIngot>(this Stopwatch stopwatch)
            where TIngot : IMetal
        {
            Console.WriteLine($"{typeof(TIngot).Name} forging finished at: {stopwatch.ElapsedMilliseconds}");
        }
    }
}

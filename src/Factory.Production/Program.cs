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

            LogForgeStarting<ICopper>(stopWatch);
            var copperIngots = FakeForge.ForgeIngots<ICopper>(30, 20);
            LogForgeFinished<ICopper>(stopWatch);

            LogForgeStarting<IZinc>(stopWatch);
            var zincIngots = FakeForge.ForgeIngots<IZinc>(10, 5);
            LogForgeFinished<IZinc>(stopWatch);

            LogForgeStarting<IBronze>(stopWatch);
            var bronzeIngots = bronzeAlloyer.Forge(copperIngots, zincIngots);
            LogForgeFinished<IBronze>(stopWatch);

            LogForgeStarting<IAluminium>(stopWatch);
            var aluminiumIngots = FakeForge.ForgeIngots<IAluminium>(15, 3);
            LogForgeFinished<IAluminium>(stopWatch);

            LogForgeStarting<ILightBronze>(stopWatch);
            var lightBronzeIngots = lightBronzeAlloyer.Forge(bronzeIngots, aluminiumIngots);
            LogForgeFinished<ILightBronze>(stopWatch);

            stopWatch.Stop();

            Console.WriteLine($"Time taken: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Light Bronze ingots forged: {lightBronzeIngots.Length}");
        }

        private static Task ForgeConcurrently()
        {
            var bronzeAlloyer = new AlloyForge<IBronze, ICopper, IZinc>(20);
            var lightBronzeAlloyer = new AlloyForge<ILightBronze, IBronze, IAluminium>(15);

            var stopWatch = Stopwatch.StartNew();

            var copperTask = Task.Run(() => LogForgeStarting<ICopper>(stopWatch))
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<ICopper>(30, 20))
                .Unwrap()
                .ContinueWith(t =>
                {
                    LogForgeFinished<ICopper>(stopWatch);
                    return t;
                })
                .Unwrap();
            
            var zincTask = Task.Run(() => LogForgeStarting<IZinc>(stopWatch))
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<IZinc>(10, 5))
                .Unwrap()
                .ContinueWith(t =>
                {
                    LogForgeFinished<IZinc>(stopWatch);
                    return t;
                })
                .Unwrap();

            var bronzeTask = Task.WhenAll(copperTask, zincTask)
                .ContinueWith(t => LogForgeStarting<IBronze>(stopWatch))
                .ContinueWith(t => bronzeAlloyer.ForgeAsync(copperTask.Result, zincTask.Result))
                .Unwrap()
                .ContinueWith(t =>
                {
                    LogForgeFinished<IBronze>(stopWatch);
                    return t;
                })
                .Unwrap();

            var aluminiumTask = Task.Run(() => LogForgeStarting<IAluminium>(stopWatch))
                .ContinueWith(t => FakeForge.ForgeIngotsAsync<IAluminium>(15, 3))
                .Unwrap()
                .ContinueWith(t =>
                {
                    LogForgeFinished<IAluminium>(stopWatch);
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
                    LogForgeStarting<ILightBronze>(stopWatch);
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
                    LogForgeFinished<ILightBronze>(stopWatch);
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

        private static void LogForgeStarting<TIngot>(Stopwatch stopwatch)
            where TIngot : IMetal
        {
            Console.WriteLine($"Starting {typeof(TIngot).Name} forging at: {stopwatch.ElapsedMilliseconds}");
        }

        private static void LogForgeFinished<TIngot>(Stopwatch stopwatch)
            where TIngot : IMetal
        {
            Console.WriteLine($"{typeof(TIngot).Name} forging finished at: {stopwatch.ElapsedMilliseconds}");
        }
    }
}

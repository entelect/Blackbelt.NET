using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Factory.Resources;

namespace Factory.Production
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var productionLine = new ProductionLine();
            
            Console.WriteLine("Starting serial forge");
            productionLine.ForgeSerial();
            
            Console.WriteLine("\n\n\nStarting parallel forge");
            await productionLine.ForgeParallel();

            Console.WriteLine("\n\n\nStarting parallel async await");
            await productionLine.ForgeParallelAsync();
        }

        public static void LogForgeStarting<TIngot>(this Stopwatch stopwatch)
            where TIngot : IMetal
        {
            Console.WriteLine($"Starting {typeof(TIngot).Name} forging at: {stopwatch.ElapsedMilliseconds}");
        }

        public static void LogForgeFinished<TIngot>(this Stopwatch stopwatch)
            where TIngot : IMetal
        {
            Console.WriteLine($"{typeof(TIngot).Name} forging finished at: {stopwatch.ElapsedMilliseconds}");
        }
    }
}

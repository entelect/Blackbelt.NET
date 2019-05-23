using System;
using System.Diagnostics;
using Factory.Resources;

namespace Factory.Production
{
    public static class Helpers
    {
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

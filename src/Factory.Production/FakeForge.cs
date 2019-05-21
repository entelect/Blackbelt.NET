using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Factory.Resources;

namespace Factory.Production
{
    internal static class FakeForge
    {
        public static Ingot<TMetal>[] ForgeIngots<TMetal>(double ingotWeight, int count)
            where TMetal : IMetal

        {
            Thread.Sleep(1000);

            var ingots = Ingots<TMetal>(ingotWeight, count);

            for(int i = 0; i < count; i++)
            {
                ingots[i].Cool();
            }

            return ingots;
        }

        public static async Task<Ingot<TMetal>[]> ForgeIngotsAsync<TMetal>(double ingotWeight, int count)
            where TMetal : IMetal
        {
            await Task.Delay(1000);

            var ingots = Ingots<TMetal>(ingotWeight, count);
                         
            var coolTasks = ingots.Select(async x => await x.CoolAsync());

            await Task.WhenAll(coolTasks);

            return ingots;
        }

        private static Ingot<TMetal>[] Ingots<TMetal>(double ingotWeight, int count) where TMetal : IMetal
        {
            var ingots = new Ingot<TMetal>[count];

            for (int i = 0; i < count; i++)
            {
                ingots[i] = new Ingot<TMetal>(ingotWeight);
            }

            return ingots;
        }
    }
}

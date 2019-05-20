using System.Threading;
using System.Threading.Tasks;
using Factory.Resources;

namespace Factory.Production
{
    internal static class FakeForge
    {
        public static TIngot[] ForgeIngots<TIngot>(double ingotWeight, int count)
            where TIngot : Ingot, new()
        {
            Thread.Sleep(1000);

            var ingots = new TIngot[count];

            for(int i = 0; i < count; i++)
            {
                ingots[i] = new TIngot();
                ingots[i].AddWeight(ingotWeight);
            }

            for(int i = 0; i < count; i++)
            {
                ingots[i].Cool();
            }

            return ingots;
        }

        public static async Task<TIngot[]> ForgeIngotsAsync<TIngot>(double ingotWeight, int count)
            where TIngot : Ingot, new()
        {
            await Task.Delay(1000);

            var ingots = new TIngot[count];

            for(int i = 0; i < count; i++)
            {
                ingots[i] = new TIngot();
                ingots[i].AddWeight(ingotWeight);
            }

            var ingotTasks = new Task[count];
            for(int i = 0; i < count; i++)
            {
                ingotTasks[i] = ingots[i].CoolAsync();
            }

            await Task.WhenAll(ingotTasks);

            return ingots;
        }
    }
}

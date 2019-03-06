using System.Threading;
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
    }
}

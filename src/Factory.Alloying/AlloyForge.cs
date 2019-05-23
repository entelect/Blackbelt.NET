using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Factory.Resources;

namespace Factory.Alloying
{
    public class AlloyForge<TAlloy, TBase, TSolute>
        where TBase : IMetal
        where TSolute : IMetal
        where TAlloy : IAlloy<TBase, TSolute>
    {
        private readonly double ingotWeight;
        private readonly int heatingTime;

        public AlloyForge(double ingotWeight)
        {
            this.ingotWeight = ingotWeight;
            this.heatingTime = ForgeHeatingTime.Get<TAlloy>();
        }

        public Ingot<TAlloy>[] Forge(Ingot<TBase>[] bases, Ingot<TSolute>[] solutes)
        {
            //Heat all oyen
            Thread.Sleep(this.heatingTime);

            var alloys = ForgeAlloys(bases, solutes);

            //Cool ingots
            foreach(var alloy in alloys)
            {
                alloy.Cool();
            }
            
            return alloys.ToArray();
        }

        public async Task<Ingot<TAlloy>[]> ForgeAsync(Ingot<TBase>[] bases, Ingot<TSolute>[] solutes)
        {
            await Task.Delay(this.heatingTime);

            var alloys = ForgeAlloys(bases, solutes);

            var coolTasks = alloys.Select(async x => await x.CoolAsync());

            await Task.WhenAll(coolTasks);

            return alloys;
        }

        private Ingot<TAlloy>[] ForgeAlloys(Ingot<TBase>[] bases, Ingot<TSolute>[] solutes)
        {
            var basesTotalWeight = bases.Sum(x => x.Weight);
            var solutesTotalWeight = solutes.Sum(x => x.Weight);
            var totalWeight = basesTotalWeight + solutesTotalWeight;
            var ingotCount = (int) Math.Ceiling(totalWeight / ingotWeight);
            var lastIndex = ingotCount - 1;

            var alloys = new Ingot<TAlloy>[ingotCount];

            for (int i = 0; i < lastIndex; i++)
            {
                alloys[i] = new Ingot<TAlloy>(this.ingotWeight);
            }

            alloys[lastIndex] = new Ingot<TAlloy>(totalWeight % lastIndex);
            
            return alloys;
        }
    }
}

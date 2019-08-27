using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Factory.Resources;

namespace Factory.Smelter
{
    public class Forge<TMetal>
        where TMetal : IPureMetal
    {
        private readonly double ingotWeight;
        private readonly int heatingTime;

        public Forge(double ingotWeight)
        {
            this.ingotWeight = ingotWeight;
            this.heatingTime = ForgeHeatingTime.Get<TMetal>();
        }

        public Ingot<TMetal>[] HeatForgeAndSmelt(IEnumerable<Ore<TMetal>> oreItem)
        {
            //Heat forge
            Thread.Sleep(this.heatingTime);

            var smelt = Smelt(oreItem);

            //Cool ingots
            foreach (var ingot in smelt)
            {
                ingot.Cool();
            }

            return smelt;
        }

        private Ingot<TMetal>[] Smelt(IEnumerable<Ore<TMetal>> oreItems)
        {
            var moltenCopper = oreItems.Sum(x => x.Weight * x.Concentration);

            var ingotCount = (int)Math.Ceiling(moltenCopper / ingotWeight);

            var ingots = new Ingot<TMetal>[ingotCount];
            var lastIndex = ingotCount - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                ingots[i] = new Ingot<TMetal>(this.ingotWeight);
            }

            ingots[lastIndex] = new Ingot<TMetal>(moltenCopper % ingotCount);

            return ingots;
        }
    }
}
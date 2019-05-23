using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Factory.Resources;

namespace Factory.Smelter
{
    public class ChalcopyriteForge
    {
        private readonly double ingotWeight;
        private readonly int heatingTime;

        public ChalcopyriteForge(double ingotWeight)
        {
            this.ingotWeight = ingotWeight;
            this.heatingTime = ForgeHeatingTime.Get<Copper>();
        }

        public Ingot<Copper>[] HeatForgeAndSmelt(IEnumerable<Chalcopyrite> oreItem)
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

        private Ingot<Copper>[] Smelt(IEnumerable<Chalcopyrite> oreItems)
        {
            var moltenCopper = oreItems.Sum(x => x.Weight * x.Concentration);

            var ingotCount = (int)Math.Ceiling(moltenCopper / ingotWeight);

            var ingots = new Ingot<Copper>[ingotCount];
            var lastIndex = ingotCount - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                ingots[i] = new Ingot<Copper>(this.ingotWeight);
            }

            ingots[lastIndex] = new Ingot<Copper>(moltenCopper % ingotCount);

            return ingots;
        }
    }
}
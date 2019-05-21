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

        public ChalcopyriteForge(double ingotWeight)
        {
            this.ingotWeight = ingotWeight;
        }

        public Ingot<ICopper>[] HeatForgeAndSmelt(IEnumerable<Chalcopyrite> oreItem)
        {
            //Heat forge
            Thread.Sleep(1000);

            var smelt = Smelt(oreItem);

            //Cool ingots
            foreach (var ingot in smelt) ingot.Cool();

            return smelt;
        }

        private Ingot<ICopper>[] Smelt(IEnumerable<Chalcopyrite> oreItems)
        {
            var moltenCopper = oreItems.Sum(x => x.Weight * x.Concentration);

            var ingotCount = (int)Math.Ceiling(moltenCopper / ingotWeight);

            var ingots = new Ingot<ICopper>[ingotCount];
            var lastIndex = ingotCount - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                ingots[i] = new Ingot<ICopper>(this.ingotWeight);
            }

            ingots[lastIndex] = new Ingot<ICopper>(moltenCopper % ingotCount);

            return ingots;
        }
    }
}
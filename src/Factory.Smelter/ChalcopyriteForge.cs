using System;
using System.Collections.Generic;
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
        
        public CopperIngot[] HeatForgeAndSmelt(IEnumerable<Chalcopyrite> oreItem)
        {
            //Heat forge
            Thread.Sleep(1000);

            var smelt = this.Smelt(oreItem);
            
            //Cool ingots
            foreach(var ingot in smelt)
            {
                ingot.Cool();
            }
            
            return smelt.ToArray();
        }

        private List<CopperIngot> Smelt(IEnumerable<Chalcopyrite> oreItems)
        {
            var ingots = new List<CopperIngot>();
            var ingot = new CopperIngot();

            foreach(var oreItem in oreItems)
            {
                var moltenIron = oreItem.Weight * oreItem.Concentration;
                var weightLeft = this.ingotWeight - ingot.Weight;
                var toAddToIngot = Math.Min(moltenIron, weightLeft);
                ingot.AddWeight(toAddToIngot);

                if(!(Math.Abs(this.ingotWeight - ingot.Weight) < 0.01))
                {
                    continue;
                }
                
                ingots.Add(ingot);
                ingot = new CopperIngot();
                ingot.AddWeight(moltenIron - toAddToIngot);
            }

            return ingots;
        }
    }
}

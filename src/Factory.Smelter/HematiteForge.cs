using System.Collections.Generic;
using System.Threading.Tasks;
using Factory.Resources;

namespace Factory.Smelter
{
    public class HematiteForge
    {
        private readonly double ingotWeight;

        public HematiteForge(double ingotWeight)
        {
            this.ingotWeight = ingotWeight;
        }
        
        public IronIngot[] HeadForgeAndSmelt(IEnumerable<Hematite> oreItem)
        {
            //TODO add heating process

            return this.Smelt(oreItem);
        }

        private IronIngot[] Smelt(IEnumerable<Hematite> oreItems)
        {
            var moltenIron = 0.0;

            foreach(var oreItem in oreItems)
            {
                var iron = oreItem.Weight * oreItem.Concentration;
                moltenIron += iron;
            }

            var ingots = (int)(moltenIron / this.ingotWeight);
            return new IronIngot[ingots];
        }
    }
}

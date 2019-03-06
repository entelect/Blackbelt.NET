using System;
using System.Collections.Generic;
using System.Threading;
using Factory.Resources;

namespace Factory.Alloying
{
    public class AlloyForge<TAlloy, TBase, TSolute>
        where TBase : Ingot, new()
        where TSolute : Ingot, new()
        where TAlloy : Ingot, IAlloy<TBase, TSolute>, new ()
    {
        private readonly double ratio;

        public AlloyForge(double ratio)
        {
            this.ratio = ratio;
        }

        public TAlloy[] Forge(TBase[] bases, TSolute[] solutes)
        {
            //Heat alloyer
            Thread.Sleep(1000);

            var alloys = new List<TAlloy>();

            foreach(var @base in bases)
            {
                var moltenBase = @base.Weight;
                foreach(var solute in solutes)
                {
                    var moltenSolute = solute.Weight;
                    //TODO figure out the math required for this
                }
            }
            
            //Cool ingots
            foreach(var alloy in alloys)
            {
                alloy.Cool();
            }
            
            return alloys.ToArray();
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Factory.Resources;
using Factory.Samplers;
using Factory.Smelter;

namespace Factory.Production
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            GenericPrac();
            
            LambdasPrac();

            await AsyncPrac();
        }

        private static void GenericPrac()
        {
            var delivery = new OreDelivery(1);
            
            var ironForge = new Forge<Iron>(10);
            var copperForge = new Forge<Copper>(10);
            var aluminiumForge = new Forge<Aluminium>(10);
            var zincForge = new Forge<Zinc>(20);

            var iron1 = ironForge.HeatForgeAndSmelt(delivery.GetHematiteBatch(10));
            var iron2 = ironForge.HeatForgeAndSmelt(delivery.GetMagnetiteBatch(10));

            var copper1 = copperForge.HeatForgeAndSmelt(delivery.GetChalcociteBatch(10));
            var copper2 = copperForge.HeatForgeAndSmelt(delivery.GetChalcopyriteBatch(10));

            var aluminium1 = aluminiumForge.HeatForgeAndSmelt(delivery.GetBauxiteBatch(10));
            var aluminium2 = aluminiumForge.HeatForgeAndSmelt(delivery.GetCorundumBatch(10));

            var zinc1 = zincForge.HeatForgeAndSmelt(delivery.GetCalamineBatch(10));
            var zinc2 = zincForge.HeatForgeAndSmelt(delivery.GetZinciteBatch(10));
            
            Console.WriteLine($"Hematite to iron: {iron1.Sum(x => x.Weight)}");
            Console.WriteLine($"Magnetite to iron: {iron2.Sum(x => x.Weight)}");
            
            Console.WriteLine($"Calcocite to iron: {copper1.Sum(x => x.Weight)}");
            Console.WriteLine($"Chalcopyrite to iron: {copper2.Sum(x => x.Weight)}");
            
            Console.WriteLine($"Bauxite to iron: {aluminium1.Sum(x => x.Weight)}");
            Console.WriteLine($"Corundum to iron: {aluminium2.Sum(x => x.Weight)}");
            
            Console.WriteLine($"Calamine to iron: {zinc1.Sum(x => x.Weight)}");
            Console.WriteLine($"Zincite to iron: {zinc2.Sum(x => x.Weight)}");

            //Use generic forge to forge each type of ore from above delivery
        }

        private static void LambdasPrac()
        {
            // Numbers here should yield 9 failed batches out of 2000
            var sampler = new Sampler(0.2, 118);

            var failCount = 0;

            foreach(var deliveryNr in Enumerable.Range(1, 2000))
            {
                var delivery = new OreDelivery(deliveryNr);

                var hematiteBatch = delivery.GetHematiteBatch(500);

                var isGoodBatch = sampler.RunCheckOnSample(hematiteBatch);

                if(!isGoodBatch)
                {
                    failCount++;
                }
            }
            
            Console.WriteLine("Number of bad batches: {0}", failCount);
        }

        private static async Task AsyncPrac()
        {
            var productionLine = new LightBronzeProductionLine();
            
            Console.WriteLine("Starting serial forge");
            productionLine.ForgeSerial();
            
            Console.WriteLine("\n\n\nStarting parallel forge");
            await productionLine.ForgeParallel();

            Console.WriteLine("\n\n\nStarting parallel async await");
            await productionLine.ForgeParallelAsync();
        }
    }
}

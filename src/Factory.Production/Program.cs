using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Factory.Resources;
using Factory.Samplers;

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

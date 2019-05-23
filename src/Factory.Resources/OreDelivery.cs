using System;
using System.Collections.Generic;

namespace Factory.Resources
{
    internal delegate TOre CreateOre<out TOre>(double weight, double concentration);
    
    public class OreDelivery
    {
        private readonly int hematiteSeed;
        private readonly int magnetiteSeed;
        private readonly int chalcopriteSeed;
        private readonly int chalcociteSeed;
        private readonly int bauxiteSeed;
        private readonly int corundumSeed;
        private readonly int zinciteSeed;
        private readonly int calamineSeed;


        public OreDelivery(int seed)
        {
            var seedGenerator = new Random(seed);
            this.hematiteSeed = seedGenerator.Next();
            this.magnetiteSeed = seedGenerator.Next();
            this.chalcopriteSeed = seedGenerator.Next();
            this.chalcociteSeed = seedGenerator.Next();
            this.bauxiteSeed = seedGenerator.Next();
            this.corundumSeed = seedGenerator.Next();
            this.zinciteSeed = seedGenerator.Next();
            this.calamineSeed = seedGenerator.Next();
        }

        public IReadOnlyList<Hematite> GetHematiteBatch(int count)
        {
            Hematite Constructor(double weight, double concentration) => new Hematite(weight, concentration);

            return GetRandomBatch(this.hematiteSeed, count, Constructor);
        }

        public IReadOnlyList<Magnetite> GetMagnetiteBatch(int count)
        {
            Magnetite Constructor(double weight, double concentration) => new Magnetite(weight, concentration);

            return GetRandomBatch(this.magnetiteSeed, count, Constructor);
        }

        public IReadOnlyList<Chalcopyrite> GetChalcopyriteBatch(int count)
        {
            Chalcopyrite Constructor(double weight, double concentration) => new Chalcopyrite(weight, concentration);

            return GetRandomBatch(this.chalcopriteSeed, count, Constructor);
        }

        public IReadOnlyList<Chalcocite> GetChalcociteBatch(int count)
        {
            Chalcocite Constructor(double weight, double concentration) => new Chalcocite(weight, concentration);

            return GetRandomBatch(this.chalcociteSeed, count, Constructor);
        }

        public IReadOnlyList<Bauxite> GetBauxiteBatch(int count)
        {
            Bauxite Constructor(double weight, double concentration) => new Bauxite(weight, concentration);

            return GetRandomBatch(this.bauxiteSeed, count, Constructor);
        }

        public IReadOnlyList<Corundum> GetCorundumBatch(int count)
        {
            Corundum Constructor(double weight, double concentration) => new Corundum(weight, concentration);

            return GetRandomBatch(this.corundumSeed, count, Constructor);
        }

        public IReadOnlyList<Zincite> GetZinciteBatch(int count)
        {
            Zincite Constructor(double weight, double concentration) => new Zincite(weight, concentration);

            return GetRandomBatch(this.zinciteSeed, count, Constructor);
        }

        public IReadOnlyList<Calamine> GetCalamineBatch(int count)
        {
            Calamine Constructor(double weight, double concentration) => new Calamine(weight, concentration);

            return GetRandomBatch(this.calamineSeed, count, Constructor);
        }

        private static IReadOnlyList<TOre> GetRandomBatch<TOre>(int seed, int batchSize, CreateOre<TOre> constructor)
        {
            var batch = new List<TOre>(batchSize);
            var random = new Random(seed);
            
            for(int i = 0; i < batchSize; i++)
            {
                var weight = random.NextDouble(10, 20);
                var concentration = random.NextDouble(0, 70);

                batch.Add(constructor(weight, concentration));
            }

            return batch.AsReadOnly();
        }
    }
    
    internal static class RandomExtension
    {
        internal static double NextDouble(this Random random, double min, double max)
        {
            var next = random.NextDouble();

            return next * (max - min) + min;
        }
    }
}

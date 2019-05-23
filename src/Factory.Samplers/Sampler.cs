using System;
using System.Collections.Generic;
using System.Diagnostics;
using Factory.Resources;

namespace Factory.Samplers
{
    public class Sampler
    {
        private readonly double samplePercentage;
        private readonly Random random;

        public Sampler(double samplePercentage, int seed)
        {
            Debug.Assert(samplePercentage > 0.0 && samplePercentage <= 1.0,
                $"{nameof(samplePercentage)} not between 0 and 1");
            this.samplePercentage = samplePercentage;
            this.random = new Random(seed);
        }

        private IEnumerable<Ore<TMetal>> GetSample<TMetal>(IReadOnlyCollection<Ore<TMetal>> batch)
            where TMetal : IPureMetal
        {
            var totalToTake = (int)(samplePercentage * batch.Count);
            var totalTaken = 0;
            
            foreach (var item in batch)
            {
                if (totalTaken == totalToTake)
                {
                    yield break;
                }
                
                var roll = this.random.NextDouble();

                if (!(roll <= this.samplePercentage))
                {
                    continue;
                }
                
                totalTaken++;
                yield return item;
            }
        }

        //TODO: Add expression to serve as check to this method
        public bool RunCheckOnSample<TMetal>(IReadOnlyList<Ore<TMetal>> batch)
            where TMetal : IPureMetal
        {
            var sample = GetSample(batch);

            throw new NotImplementedException("Checking mechanism has not been implemented yet.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Factory.Resources;

namespace Factory.Samplers
{
    public class Sampler
    {
        private readonly double samplePercentage;

        public Sampler(double samplePercentage)
        {
            Debug.Assert(samplePercentage > 0.0 && samplePercentage <= 1.0,
                $"{nameof(samplePercentage)} not between 0 and 1");
            this.samplePercentage = samplePercentage;
        }

        private IEnumerable<Ore<IPureMetal>> GetSample(IReadOnlyCollection<Ore<IPureMetal>> batch)
        {
            var totalToTake = (int)(samplePercentage * batch.Count);
            var totalTaken = 0;
            
            var random = new Random();

            foreach (var item in batch)
            {
                if (totalTaken == totalToTake)
                {
                    yield break;
                }
                
                var roll = random.NextDouble();

                if (!(roll <= this.samplePercentage))
                {
                    continue;
                }
                
                totalTaken++;
                yield return item;
            }
        }

        //TODO: Add expression to serve as check to this method
        public bool RunCheckOnSample(IReadOnlyCollection<Ore<IPureMetal>> batch)
        {
            var sample = GetSample(batch);
            
            throw new NotImplementedException("Checking mechanism has not been implemented yet.");
        }
    }
}
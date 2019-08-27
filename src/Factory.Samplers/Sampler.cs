using System;
using System.Collections.Generic;
using System.Diagnostics;
using Factory.Resources;

namespace Factory.Samplers
{
    public delegate bool CheckSample<TMetal>(IEnumerable<Ore<TMetal>> sample) where TMetal : IPureMetal;
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

        public bool RunCheckOnSample<TMetal>(
            IReadOnlyList<Ore<TMetal>> batch,
            Func<IEnumerable<Ore<TMetal>>, bool> checker)
            where TMetal : IPureMetal
        {
            var sample = GetSample(batch);

            return checker(sample);
        }
    }
}
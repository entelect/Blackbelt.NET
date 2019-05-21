using System;
using System.Threading;
using System.Threading.Tasks;

namespace Factory.Resources
{
    public class Ingot<TMetal>
        where TMetal: IMetal
    {
        private readonly int coolTimePerWeight;
        private readonly double weight;
        private bool isCooled;

        public Ingot(double weight)
        {
            this.weight = weight;
            this.isCooled = false;
            this.coolTimePerWeight = 2;
        }

        public double Weight => weight;

        private int CoolTime => (int)Math.Ceiling(coolTimePerWeight * weight);

        public bool IsCooled => this.isCooled;

        public void Cool()
        {
            Thread.Sleep(this.CoolTime);
            this.isCooled = true;
        }

        public async Task CoolAsync()
        {
            await Task.Delay(this.CoolTime);
            this.isCooled = true;
        }
    }
}

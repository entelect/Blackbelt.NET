using System.Collections.Generic;
using System.Collections.Immutable;
using Factory.Resources;

namespace Factory.Sorters
{
    public class IronOreSorter
    {
        private readonly Hematite[] items;
        private int count;

        public IronOreSorter(int capacity)
        {
            this.items = new Hematite[capacity];
            this.count = 0;
        }

        public bool AddItem(Hematite item)
        {
            if(this.count >= this.items.Length)
            {
                return false;
            }

            this.items[this.count++] = item;
            return true;
        }

        public float AverageConcentration
        {
            get
            {
                var total = 0f;
                
                for(int i = 0; i < this.count; i++)
                {
                    total += this.items[i].Concentration;
                }

                return total / this.count;
            }
        }

        public IEnumerable<Hematite> Sort()
        {
            return this.items.ToImmutableSortedSet(new HematiteClassifier());
        }
    }
}

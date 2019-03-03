using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Factory.Sorters
{
    public class Sorter<T>
    {
        private readonly T[] items;
        private int count;

        public Sorter(int capacity)
        {
            this.items = new T[capacity];
            this.count = 0;
        }
        
        public bool AddItem(T item)
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
                throw new NotImplementedException();
            }
        }

        public IEnumerable<T> Sort(IComparer<T> comparer)
        {
            return this.items.ToImmutableSortedSet(comparer);
        }
    }
}

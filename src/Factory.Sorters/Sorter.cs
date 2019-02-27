using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Factory.Sorters
{
    public class Sorter<T>
    {
        private readonly T[] items;
        private int index;

        public Sorter(int capacity)
        {
            this.items = new T[capacity];
            this.index = 0;
        }
        
        public bool AddItem(T item)
        {
            if(this.index >= this.items.Length)
            {
                return false;
            }
            
            this.items[this.index++] = item;
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

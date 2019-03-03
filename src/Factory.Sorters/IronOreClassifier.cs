using System.Collections.Generic;
using Factory.Resources;

namespace Factory.Sorters
{
    public class HematiteClassifier : IComparer<Hematite>
    {
        public int Compare(Hematite x, Hematite y)
        {
            return x.Concentration.CompareTo(y.Concentration);
        }
    }
}

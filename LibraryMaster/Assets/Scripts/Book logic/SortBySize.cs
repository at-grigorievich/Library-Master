using System.Collections.Generic;

namespace BookLogic
{
    public class SortBySize: IComparer<IWeightable>
    {
        public int Compare(IWeightable x, IWeightable y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            if (x.Weight > y.Weight)
                return -1;
            if (x.Weight < y.Weight)
                return 1;

            return 1;
        }
    }
}
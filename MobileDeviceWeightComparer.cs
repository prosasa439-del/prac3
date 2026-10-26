using System;
using System.Collections.Generic;

namespace Practice3_Collections
{
    public class MobileDeviceWeightComparer : IComparer<MobileDevice>
    {
        public int Compare(MobileDevice x, MobileDevice y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int weightComparison = x.Weight.CompareTo(y.Weight);
            if (weightComparison != 0)
                return weightComparison;

            return string.Compare(x.Brand, y.Brand, StringComparison.OrdinalIgnoreCase);
        }
    }
}
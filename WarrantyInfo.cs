using System;

namespace Practice3_Collections
{
    public class WarrantyInfo : ICloneable
    {
        public int Months { get; set; }
        public string ServiceCenter { get; set; }

        public WarrantyInfo(int months, string serviceCenter)
        {
            Months = months;
            ServiceCenter = serviceCenter;
        }

        public object Clone()
        {
            return new WarrantyInfo(Months, ServiceCenter);
        }

        public override string ToString() => $"{Months} мес. ({ServiceCenter})";
    }
}
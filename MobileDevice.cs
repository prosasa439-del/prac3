using System;

namespace Practice3_Collections
{
    public class MobileDevice : IComparable<MobileDevice>, IComparable, ICloneable
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public WarrantyInfo Warranty { get; set; }

        public MobileDevice()
        {
            Brand = "Unknown";
            Model = "Unknown";
            Price = 0m;
            Weight = 100;
            Warranty = new WarrantyInfo(12, "Официальный СЦ");
        }

        public MobileDevice(string brand, string model, decimal price, int weight, int warrantyMonths = 12, string serviceCenter = "Официальный СЦ")
        {
            Brand = brand;
            Model = model;
            Price = price >= 0m ? price : 0m;
            Weight = weight > 0 ? weight : 100;
            Warranty = new WarrantyInfo(warrantyMonths, serviceCenter);
        }

        public int CompareTo(MobileDevice other)
        {
            if (other == null) return 1;
            return Price.CompareTo(other.Price);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is MobileDevice otherDevice)
                return CompareTo(otherDevice);
            throw new ArgumentException("Объект не является типом MobileDevice");
        }

        public object Clone()
        {
            MobileDevice cloned = (MobileDevice)MemberwiseClone();
            cloned.Warranty = (WarrantyInfo)Warranty.Clone();
            return cloned;
        }

        public override bool Equals(object obj)
        {
            if (obj is MobileDevice other)
            {
                return string.Equals(Brand, other.Brand, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(Model, other.Model, StringComparison.OrdinalIgnoreCase) &&
                       Price == other.Price &&
                       Weight == other.Weight;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Brand?.ToLower(), Model?.ToLower(), Price, Weight);
        }

        public override string ToString()
        {
            return $"[{Brand} {Model} | Цена: {Price} руб. | Вес: {Weight} г | Гарантия: {Warranty}]";
        }
    }
}
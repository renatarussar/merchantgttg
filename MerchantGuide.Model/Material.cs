using System;

namespace MerchantGuide.Model
{
    public class Material
    {
        public string Name { get; set; }
        public double CreditValue { get; set; }

        public Material(string name, double value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid value for: Material.Name");
            }
            if (value < 0)
            {
                throw new ArgumentException("Invalid value for: Material.CreditValue");
            }

            Name = name;
            CreditValue = value;
        }

    }
}

using System;

namespace MerchantGuide.Model
{
    public class IntergalacticUnit
    {
        public string Name { get; set; }
        public NumeralBase UnitValue { get; set; }

        public IntergalacticUnit(string name, string value)
        {
            NumeralBase r;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid value for: IntergalacticUnit.Name");
            }

            try
            {
                r = new RomanNumeral(value);
            }
            catch
            {
                throw new ArgumentException("Invalid value for: IntergalacticUnit.Value");
            }

            Name = name;
            UnitValue = r;
        }
    }
}

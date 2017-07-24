using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuide.Model
{
    public abstract class NumeralBase
    {
        public string Text { get; protected set; }
        public int AbsoluteValue { get; protected set; }

        public NumeralBase(string text)
        {
            if (NumberIsValid(text))
            {
                Text = text;
                AbsoluteValue = CalculateAbsoluteValue();
            }
            else
            {
                throw new ArgumentException("Invalid value for: Text");
            }
        }

        public abstract int CalculateAbsoluteValue();

        public abstract bool NumberIsValid(string text);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuide.Model
{
    public abstract class NumeralModel : INumeralModel
    {
        public string Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AbsoluteValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public NumeralModel(string text)
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

        public int CalculateAbsoluteValue()
        {
            throw new NotImplementedException();
        }

        public bool NumberIsValid(string text)
        {
            throw new NotImplementedException();
        }
    }
}

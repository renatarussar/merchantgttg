using System;
using System.Text.RegularExpressions;

namespace MerchantGuide.Model
{
    public class RomanNumeral : NumeralBase
    {
        public enum RomanSymbol
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000,
            Null = 0
        }

        public RomanNumeral(string text) : base(text)
        {
        }

        public override int CalculateAbsoluteValue()
        {
            int resultValue = 0;
            RomanSymbol current = RomanSymbol.Null;
            for (int i = Text.Length - 1; i >= 0; i--)
            {
                RomanSymbol before = (RomanSymbol)Enum.Parse(typeof(RomanSymbol), Text[i].ToString());
                if (before >= current)
                {
                    resultValue += (int)before;
                }
                else
                {
                    resultValue -= (int)before;
                }
                current = before;
            }

            return resultValue;
        }

        public override bool NumberIsValid(string text)
        {
            bool result = false;

            string thousandsPattern = "(M{0,3})";
            string hundredsPattern = "(C{0,3}|CD|DC{0,3}|CM)";
            string tensPattern = "(X{0,3}|XL|LX{0,3}|XC)";
            string unitsPattern = "(I{0,3}|IV|VI{0,3}|IX)";
            string fullRomanNumeralPattern = string.Format("^{0}{1}{2}{3}$", thousandsPattern, hundredsPattern, tensPattern, unitsPattern);

            result = Regex.Match(text, fullRomanNumeralPattern).Success;

            return result;
        }

    }
}

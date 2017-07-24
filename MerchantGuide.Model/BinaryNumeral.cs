using System;
using System.Text.RegularExpressions;

namespace MerchantGuide.Model
{
    public class BinaryNumeral : NumeralBase
    {
        public BinaryNumeral(string text) : base(text)
        {
        }

        public override int CalculateAbsoluteValue()
        {
            int resultValue = 0;
            char[] textCharArray = Text.ToCharArray();
            Array.Reverse(textCharArray);
            string reversedText = new string(textCharArray);

            for(int i = 0; i<reversedText.Length; i++)
            {
                resultValue += (int) Math.Pow(2, i) * Int32.Parse(reversedText[i].ToString());
            }
            
            return resultValue;
        }

        public override bool NumberIsValid(string text)
        {
            bool result = false;

            string binaryPattern = "^[01]+$";
            
            result = Regex.Match(text, binaryPattern).Success;

            return result;
        }

    }
}

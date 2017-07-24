using MerchantGuide.Model;
using NUnit.Framework;
using System;

namespace MerchantGuide.UnitTest
{
    [TestFixture]
    public class RomanNumeralTest
    {
        [Test]
        public void ValidRomanNumeralSingleCharTest()
        {
            string text = "M";
            RomanNumeral r = new RomanNumeral(text);

            Assert.AreEqual(1000, r.AbsoluteValue);
        }

        [Test]
        public void ValidRomanNumeralAddingTest()
        {
            string text = "MDCCCLXXVI";
            RomanNumeral r = new RomanNumeral(text);

            Assert.AreEqual(1876, r.AbsoluteValue);
        }

        [Test]
        public void ValidRomanNumeralSubtractingTest()
        {
            string text = "MMMCMXCIV";
            RomanNumeral r = new RomanNumeral(text);

            Assert.AreEqual(3994, r.AbsoluteValue);
        }

        [Test]
        public void InvalidRomanNumeralTest()
        {
            string text = "MCMMXIIV";
            Assert.Throws<ArgumentException>(() => new RomanNumeral(text));
        }
    }
}

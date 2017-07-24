using MerchantGuide.Model;
using NUnit.Framework;
using System;

namespace MerchantGuide.UnitTest
{
    [TestFixture]
    public class BinaryNumeralTest
    {
        [Test]
        public void ValidBinaryNumeralSingleCharTest()
        {
            string text = "1";
            BinaryNumeral r = new BinaryNumeral(text);

            Assert.AreEqual(1, r.AbsoluteValue);
        }

        [Test]
        public void ValidBinaryNumeralTest()
        {
            string text = "100";
            BinaryNumeral r = new BinaryNumeral(text);

            Assert.AreEqual(4, r.AbsoluteValue);
        }

        [Test]
        public void ValidBinaryNumeralTest2()
        {
            string text = "010";
            BinaryNumeral r = new BinaryNumeral(text);

            Assert.AreEqual(2, r.AbsoluteValue);
        }

        [Test]
        public void InvalidBinaryNumeralTest()
        {
            string text = "A01";
            Assert.Throws<ArgumentException>(() => new BinaryNumeral(text));
        }
    }
}

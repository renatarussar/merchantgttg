using MerchantGuide.Model;
using NUnit.Framework;
using System;

namespace MerchantGuide.UnitTest
{
    [TestFixture]
    public class MaterialTest
    {
        [Test]
        public void MaterialCreationOkTest()
        {
            string name = "Silver";
            double value = 19.3;

            Material material = new Material(name, value);

            Assert.IsNotNull(material);
            Assert.IsTrue(material.Name == name);
            Assert.IsTrue(material.CreditValue == value);
        }

        [Test]
        public void MaterialCreationNameErrorTest()
        {
            string name = "";
            double value = 1;

            Assert.Throws<ArgumentException>(() => new Material(name, value));
        }

        [Test]
        public void MaterialCreationValueErrorTest()
        {
            string name = "Gold";
            double value = -459.2;

            Assert.Throws<ArgumentException>(() => new Material(name, value));
        }
    }
}

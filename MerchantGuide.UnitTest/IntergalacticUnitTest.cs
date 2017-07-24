using MerchantGuide.Model;
using NUnit.Framework;
using System;

namespace MerchantGuide.UnitTest
{
    [TestFixture]
    public class IntergalacticUnitTest
    {
        [Test]
        public void IUCreationOKTest()
        {
            string name = "blob";
            string value = "X";

            IntergalacticUnit iu = new IntergalacticUnit(name, value);

            Assert.IsNotNull(iu);
            Assert.IsTrue(iu.Name == name);
            Assert.IsTrue(iu.UnitValue.AbsoluteValue == 10);
        }

        [Test]
        public void IUCreationNameErrorTest()
        {
            string name = "";
            string value = "L";

            Assert.Throws<ArgumentException>(() => new IntergalacticUnit(name, value));
        }

        [Test]
        public void IUCreationValueErrorTest()
        {
            string name = "blob";
            string value = "3";

            Assert.Throws<ArgumentException>(() => new IntergalacticUnit(name, value));
        }
    }
}

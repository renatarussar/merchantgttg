using MerchantGuide.Controller;
using MerchantGuide.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace MerchantGuide.UnitTest
{
    [TestFixture]
    class GuideControllerTest
    {
        [Test]
        public void InvalidNoteTest()
        {
            List<string> notes = new List<string>() { "how much wood could a woodchuck chuck if a woodchuck could chuck wood ?" };
            GuideController guideController = new GuideController();

            guideController.ProcessNotes(notes);

            Assert.IsNotEmpty(guideController.Responses);
            Assert.IsTrue(guideController.Responses.Count == 1);
            Assert.AreEqual("I have no idea what you are talking about", guideController.Responses[0]);
        }

        [Test]
        public void NullNotesProvidedTest()
        {
            GuideController guideController = new GuideController();

            Assert.Throws<ArgumentNullException>(() => guideController.ProcessNotes(null));
        }

        [Test]
        public void InvalidIUnitNoteNoIsTest()
        {
            List<string> notes = new List<string>() { "pish iss X" };
            GuideController guideController = new GuideController();

            guideController.ProcessNotes(notes);

            Assert.IsNotEmpty(guideController.Responses);
            Assert.IsTrue(guideController.Responses.Count == 1);
            Assert.AreEqual("I have no idea what you are talking about", guideController.Responses[0]);
        }

        [Test]
        public void InvalidIUnitNoteNotRomanNumeralTest()
        {
            List<string> notes = new List<string>() { "pish is J" };
            GuideController guideController = new GuideController();

            Assert.Throws<ArgumentException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void InvalidIUnitNoteIUAlreadyDefinedTest()
        {
            List<string> notes = new List<string>() { "pish is I", "pish is V" };
            GuideController guideController = new GuideController();

            Assert.Throws<InvalidOperationException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void ValidIUnitNoteOKTest()
        {
            List<string> notes = new List<string>() { "pish is X" };
            GuideController guideController = new GuideController();

            guideController.ProcessNotes(notes);

            Assert.IsNotNull(guideController.Guide.IntergalacticUnitList);
            Assert.AreEqual(1, guideController.Guide.IntergalacticUnitList.Count);
            Assert.AreEqual("pish", guideController.Guide.IntergalacticUnitList[0].Name);
            Assert.AreEqual(new RomanNumeral("X").AbsoluteValue, guideController.Guide.IntergalacticUnitList[0].UnitValue.AbsoluteValue);
            Assert.AreEqual((int)RomanNumeral.RomanSymbol.X, guideController.Guide.IntergalacticUnitList[0].UnitValue.AbsoluteValue);
        }

        [Test]
        public void InvalidMaterialNoteNotIntegerCreditTest()
        {
            List<string> notes = new List<string>() { "pish pish Iron is X Credits" };
            GuideController guideController = new GuideController();

            Assert.Throws<ArgumentException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void FindMaterialSuccessTest()
        {
            List<string> notes = new List<string>() { "pish is X", "pish pish Iron is 3910 Credits" };
            GuideController guideController = new GuideController();

            guideController.ProcessNotes(notes);
            Material material = guideController.Guide.FindMaterialByName("Iron");

            Assert.IsNotNull(material);
            Assert.AreEqual("Iron", material.Name);
        }

        [Test]
        public void InvalidMaterialNoteIUNotDefinedTest()
        {
            List<string> notes = new List<string>() { "pish pish Iron is 3910 Credits" };
            GuideController guideController = new GuideController();

            Assert.Throws<InvalidOperationException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void InvalidMaterialNoteOneIUNotDefinedTest()
        {
            List<string> notes = new List<string>() { "pish is X", "pish glob Iron is 3910 Credits" };
            GuideController guideController = new GuideController();

            Assert.Throws<InvalidOperationException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void InvalidMaterialNoteMaterialAlreadyDefinedTest()
        {
            List<string> notes = new List<string>() { "pish is X", "pish pish Iron is 3910 Credits", "pish Iron is 10 Credits" };
            GuideController guideController = new GuideController();

            Assert.Throws<InvalidOperationException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void ValidMaterialNoteTest()
        {
            List<string> notes = new List<string>() { "pish is X", "pish pish Iron is 3910 Credits" };
            GuideController guideController = new GuideController();

            guideController.ProcessNotes(notes);

            Assert.IsNotNull(guideController.Guide.MaterialList);
            Assert.AreEqual(1, guideController.Guide.MaterialList.Count);
            Assert.AreEqual("Iron", guideController.Guide.MaterialList[0].Name);
            Assert.AreEqual(195.5, guideController.Guide.MaterialList[0].CreditValue);
        }

        [Test]
        public void InvalidIUQuestionNoteIUNotDefinedTest()
        {
            List<string> notes = new List<string>() { "pish is X", "how much is pish tegj glob glob ?" };
            GuideController guideController = new GuideController();

            Assert.Throws<InvalidOperationException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void InvalidMaterialQuestionNoteMaterialNotDefinedTest()
        {
            List<string> notes = new List<string>() { "glob is I", "prok is V", "pish is X", "tegj is L",
                                                      "how many Credits is glob prok Silver ?" };
            GuideController guideController = new GuideController();

            Assert.Throws<InvalidOperationException>(() => guideController.ProcessNotes(notes));
        }

        [Test]
        public void ReadFromConsoleOKTest()
        {
            string testInput = string.Format("Hello World{0}Foo bar{0}", Environment.NewLine);
            List<string> inputLines;

            using (StringReader sr = new StringReader(testInput))
            {
                Console.SetIn(sr);

                inputLines = GuideController.ReadFromInput();
            }

            Assert.IsNotEmpty(inputLines);
            Assert.IsTrue(inputLines.Count == 2);
            Assert.AreEqual("Hello World", inputLines[0]);
            Assert.AreEqual("Foo bar", inputLines[1]);
        }

        [Test]
        public void WriteToConsoleOKTest()
        {
            List<string> outputLines = new List<string> { "Goodbye World", "Bar foo" };
            string testOutput = string.Format("Goodbye World{0}Bar foo{0}", Environment.NewLine);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                GuideController.WriteToOutput(outputLines);

                Assert.AreEqual(testOutput, sw.ToString());
            }
        }

        [Test]
        public void RunMainEmptyInput()
        {
            using (StringReader sr = new StringReader(""))
            {
                Console.SetIn(sr);

                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    Program.Main(new string[] { });

                    Assert.IsTrue(sw.ToString().StartsWith("Something went wrong. Please review the input provided."));
                }
            }
        }

        [Test]
        public void RunMainTest()
        {
            string testInput1 = string.Format("glob is I{0}prok is V{0}pish is X{0}tegj is L{0}", Environment.NewLine);
            string testInput2 = string.Format("glob glob Silver is 34 Credits{0}glob prok Gold is 57800 Credits{0}pish pish Iron is 3910 Credits{0}", Environment.NewLine);
            string testInput3 = string.Format("how much is pish tegj glob glob ?{0}", Environment.NewLine);
            string testInput4 = string.Format("how many Credits is glob prok Silver ?{0}how many Credits is glob prok Gold ?{0}how many Credits is glob prok Iron ?{0}", Environment.NewLine);
            string testInput5 = string.Format("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?{0}", Environment.NewLine);
            string fullTestInput = string.Format("{0}{1}{2}{3}{4}", testInput1, testInput2, testInput3, testInput4, testInput5);

            string testOutput1 = string.Format("pish tegj glob glob is 42{0}", Environment.NewLine);
            string testOutput2 = string.Format("glob prok Silver is 68 Credits{0}glob prok Gold is 57800 Credits{0}glob prok Iron is 782 Credits{0}", Environment.NewLine);
            string testOutput3 = string.Format("I have no idea what you are talking about{0}", Environment.NewLine);
            string fullTestOutput = string.Format("{0}{1}{2}", testOutput1, testOutput2, testOutput3);

            using (StringReader sr = new StringReader(fullTestInput))
            {
                Console.SetIn(sr);

                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    Program.Main(new string[] { });

                    Assert.AreEqual(fullTestOutput, sw.ToString());
                }
            }
        }

    }
}

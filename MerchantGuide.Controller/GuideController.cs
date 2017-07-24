using MerchantGuide.Model;
using System;
using System.Collections.Generic;

namespace MerchantGuide.Controller
{
    public class GuideController
    {
        private Guide guide = new Guide();
        private List<string> responses = new List<string>();

        public Guide Guide
        {
            get
            {
                return guide;
            }
        }
        public List<string> Responses
        {
            get
            {
                return responses;
            }
        }

        public static List<string> ReadFromInput()
        {
            List<string> inputLines = new List<string>();
            string inputLine;

            while (!string.IsNullOrWhiteSpace(inputLine = Console.ReadLine()))
            {
                inputLines.Add(inputLine.Trim());
            }

            return inputLines;
        }

        public static void WriteToOutput(List<string> outputLines)
        {
            if (null == outputLines)
            {
                return;
            }
            foreach (string s in outputLines)
            {
                Console.WriteLine(s);
            }
        }

        public void Process()
        {
            ProcessNotes(ReadFromInput());
            WriteToOutput(Responses);
        }

        public void ProcessNotes(List<string> notes)
        {
            if (null == notes || notes.Count == 0)
            {
                throw new ArgumentNullException("notes");
            }
            foreach (string note in notes)
            {
                string intergalacticUnitName; string intergalacticUnitValue;
                List<string> intergalacticUnitNames; string materialName; int credits;

                if (InputHelper.TryParseIUnitLine(note, out intergalacticUnitName, out intergalacticUnitValue))
                {
                    ProcessIUnitNote(intergalacticUnitName, intergalacticUnitValue);
                }
                else if (InputHelper.TryParseMaterialLine(note, out intergalacticUnitNames, out materialName, out credits))
                {
                    ProcessMaterialNote(intergalacticUnitNames, materialName, credits);
                }
                else if (InputHelper.TryParseIUnitQuestionLine(note, out intergalacticUnitNames))
                {
                    ProcessIUnitQuestionNote(intergalacticUnitNames);
                }
                else if (InputHelper.TryParseMaterialQuestionLine(note, out intergalacticUnitNames, out materialName))
                {
                    ProcessMaterialQuestionNote(intergalacticUnitNames, materialName);

                }
                else
                {
                    ProcessInvalidNote();
                }

            }
        }

        private void ProcessIUnitNote(string intergalacticUnitName, string intergalacticUnitValue)
        {
            guide.AddIntergalacticUnit(intergalacticUnitName, intergalacticUnitValue);
        }

        private void ProcessMaterialNote(List<string> intergalacticUnitNames, string materialName, int credits)
        {
            guide.AddMaterial(intergalacticUnitNames, materialName, credits);
        }

        private void ProcessIUnitQuestionNote(List<string> intergalacticUnitNames)
        {
            NumeralBase iUnitValue = new NumeralBase(guide.GenerateRomanQueryString(intergalacticUnitNames));
            responses.Add(OutputHelper.GenerateIUnitResponse(intergalacticUnitNames.ToArray(), iUnitValue.AbsoluteValue));
        }

        private void ProcessMaterialQuestionNote(List<string> intergalacticUnitNames, string materialName)
        {
            NumeralBase iUnitValue = new NumeralBase(guide.GenerateRomanQueryString(intergalacticUnitNames));
            Material material = guide.FindMaterialByName(materialName);
            double totalCredits = (double)iUnitValue.AbsoluteValue * material.CreditValue;
            responses.Add(OutputHelper.GenerateMaterialResponse(intergalacticUnitNames.ToArray(), materialName, totalCredits));
        }

        private void ProcessInvalidNote()
        {
            responses.Add(OutputHelper.ErrorResponse);
        }
    }
}

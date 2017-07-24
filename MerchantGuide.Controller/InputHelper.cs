using System;
using System.Collections.Generic;
using System.Linq;

namespace MerchantGuide.Controller
{
    public static class InputHelper
    {
        public static bool TryParseIUnitLine(string line, out string intergalacticUnitName, out string intergalacticUnitValue)
        {
            bool parseSuccess = true;
            intergalacticUnitName = "";
            intergalacticUnitValue = "";

            if (null != line)
            {
                List<string> words = line.Split(' ').ToList();

                //IUnit definition sentence must contain 3 words: "XXXX is Y"
                if (words == null || words.Count != 3 || words.ElementAt(1) != "is")
                {
                    parseSuccess = false;
                }
                else
                {
                    intergalacticUnitName = words.ElementAt(0);
                    intergalacticUnitValue = words.ElementAt(2);
                }
            }
            else
            {
                parseSuccess = false;
            }

            return parseSuccess;
        }

        public static bool TryParseMaterialLine(string line, out List<string> intergalacticUnitNames, out string materialName, out int credits)
        {
            bool parseSuccess = true;
            materialName = "";
            intergalacticUnitNames = null;
            credits = 0;

            if (null != line)
            {
                List<string> words = line.Split(' ').ToList();

                //Material definition sentence must contain 5+ words: "XXXX+ YYYY is ZZZZ Credits"
                //where ZZZZ is an integer
                if (words == null || words.Count < 5 || words.ElementAt(words.Count - 3) != "is" || words.ElementAt(words.Count - 1) != "Credits")
                {
                    parseSuccess = false;
                }
                else if (!int.TryParse(words.ElementAt(words.Count - 2), out credits))
                {
                    throw new ArgumentException("Credit value must be integer", "credits");
                }
                else
                {
                    materialName = words.ElementAt(words.Count - 4);
                    intergalacticUnitNames = words.Take(words.IndexOf(materialName)).ToList();
                }
            }
            else
            {
                parseSuccess = false;
            }

            return parseSuccess;
        }

        public static bool TryParseIUnitQuestionLine(string line, out List<string> intergalacticUnitNames)
        {
            bool parseSuccess = true;
            intergalacticUnitNames = null;

            if (null != line)
            {
                List<string> words = line.Split(' ').ToList();

                //IUnit question must contain 4+ words: "how much is XXXX+ ?"
                if (words == null || words.Count < 5 || words.ElementAt(0) != "how" || words.ElementAt(1) != "much" || words.ElementAt(2) != "is"
                    || words.ElementAt(words.Count - 1) != "?")
                {
                    parseSuccess = false;
                }
                else
                {
                    int count = (words.Count - 1) - 3;
                    intergalacticUnitNames = words.GetRange(3, count);
                }
            }
            else
            {
                parseSuccess = false;
            }

            return parseSuccess;
        }

        public static bool TryParseMaterialQuestionLine(string line, out List<string> intergalacticUnitNames, out string materialName)
        {
            bool parseSuccess = true;
            intergalacticUnitNames = null;
            materialName = "";

            if (null != line)
            {
                List<string> words = line.Split(' ').ToList();

                //Material question must contain 4+ words: "how many Credits is XXXX+ YYYY ?"
                if (words == null || words.Count < 7 || words.ElementAt(0) != "how" || words.ElementAt(1) != "many" || words.ElementAt(2) != "Credits"
                    || words.ElementAt(3) != "is" || words.ElementAt(words.Count - 1) != "?")
                {
                    parseSuccess = false;
                }
                else
                {
                    int count = (words.Count - 2) - 4;
                    intergalacticUnitNames = words.GetRange(4, count);
                    materialName = words.ElementAt(words.Count - 2);
                }
            }
            else
            {
                parseSuccess = false;
            }

            return parseSuccess;
        }
    }
}

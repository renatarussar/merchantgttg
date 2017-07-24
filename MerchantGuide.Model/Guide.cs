using System;
using System.Collections.Generic;

namespace MerchantGuide.Model
{
    public class Guide
    {
        private List<IntergalacticUnit> intergalacticUnitList;
        private List<Material> materialList;

        public List<IntergalacticUnit> IntergalacticUnitList
        {
            get { return intergalacticUnitList; }
        }
        public List<Material> MaterialList
        {
            get { return materialList; }
        }

        public Guide()
        {
            intergalacticUnitList = new List<IntergalacticUnit>();
            materialList = new List<Material>();
        }

        private bool IntergalacticUnitAlreadyDefined(string name)
        {
            return intergalacticUnitList.Exists(iu => iu.Name == name);
        }

        private bool MaterialAlreadyDefined(string name)
        {
            return materialList.Exists(m => m.Name == name);
        }

        private double CalculateMaterialValue(List<string> intergalacticUnitNames, int credits)
        {
            double result = 0.0;

            RomanNumeral r = new RomanNumeral(GenerateRomanQueryString(intergalacticUnitNames));
            result = (double)credits / r.AbsoluteValue;

            return result;
        }

        public Material FindMaterialByName(string materialName)
        {
            Material material = materialList.Find(m => m.Name == materialName);
            if (null == material)
            {
                string exceptionMessage = string.Format("Material {0} undefined.", materialName);
                throw new InvalidOperationException(exceptionMessage);
            }

            return material;
        }

        public string GenerateRomanQueryString(List<string> intergalacticUnitNames)
        {
            string romanQuestion = "";
            if (null != intergalacticUnitNames)
            {
                foreach (string intergalacticUnitName in intergalacticUnitNames)
                {
                    if (IntergalacticUnitAlreadyDefined(intergalacticUnitName))
                    {
                        romanQuestion += intergalacticUnitList.Find(x => x.Name == intergalacticUnitName).RomanValue.Text;
                    }
                    else
                    {
                        string exceptionMessage = string.Format("Intergalactic unit {0} undefined.", intergalacticUnitName);
                        throw new InvalidOperationException(exceptionMessage);
                    }
                }
            }

            return romanQuestion;
        }

        public void AddIntergalacticUnit(string intergalacticUnitName, string intergalacticUnitValue)
        {
            if (!IntergalacticUnitAlreadyDefined(intergalacticUnitName))
            {
                intergalacticUnitList.Add(new IntergalacticUnit(intergalacticUnitName, intergalacticUnitValue));
            }
            else
            {
                string exceptionMessage = string.Format("Intergalactic unit {0} already defined before.", intergalacticUnitName);
                throw new InvalidOperationException(exceptionMessage);
            }
        }

        public void AddMaterial(List<string> intergalacticUnitNames, string materialName, int credits)
        {
            if (!MaterialAlreadyDefined(materialName))
            {
                double materialValue = CalculateMaterialValue(intergalacticUnitNames, credits);
                materialList.Add(new Material(materialName, materialValue));
            }
            else
            {
                string exceptionMessage = string.Format("Material {0} already defined before.", materialName);
                throw new InvalidOperationException(exceptionMessage);
            }
        }
    }
}

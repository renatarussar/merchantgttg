namespace MerchantGuide.Controller
{
    public static class OutputHelper
    {
        public static string GenerateIUnitResponse(string[] intergalacticUnitNames, int value)
        {
            string response = string.Format("{0} is {1}", string.Join(" ", intergalacticUnitNames), value);
            return response;
        }

        public static string GenerateMaterialResponse(string[] intergalacticUnitNames, string materialName, double value)
        {
            string response = string.Format("{0} {1} is {2} Credits", string.Join(" ", intergalacticUnitNames), materialName, value);
            return response;
        }

        public const string ErrorResponse = "I have no idea what you are talking about";
    }
}

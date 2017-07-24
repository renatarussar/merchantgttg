using MerchantGuide.Controller;
using System;

namespace MerchantGuide
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GuideController guideController = new GuideController();

            try
            {
                guideController.Process();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please review the input provided.");
                Console.WriteLine(e.Message);
            }
        }
    }
}

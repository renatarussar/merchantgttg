namespace MerchantGuide.Model
{
    interface INumeralModel
    {
        string Text { get; set; }
        int AbsoluteValue { get; set; }

        int CalculateAbsoluteValue();

        bool NumberIsValid(string text);
    }
}

namespace Source.Scripts.Currency
{
    public static class CurrencyConverter
    {
        public static string Convert(CurrencyType type, int number)
        {
            if (type == CurrencyType.Cash)
            {
                return (number / 100f).ToString(/*"##.##"*/"0.00") + '$';
            }

            return number.ToString();
        }
    }
}
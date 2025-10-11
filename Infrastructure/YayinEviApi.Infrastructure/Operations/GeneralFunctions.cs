namespace YayinEviApi.Infrastructure.Operations
{
    public static class GeneralFunctions
    {
        public static decimal TaxConverter(this string value)
        {
            if (value == "%1")
            {
                return Convert.ToDecimal(0.01);
            }
            else if (value == "%10")
                return Convert.ToDecimal(0.1);
            else if (value == "%20")
                return Convert.ToDecimal(0.2);
            else
                return Convert.ToDecimal(0);
        }
    }
}

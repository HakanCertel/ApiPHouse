using System.Security;

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

        public static IList<string> GetChangedFields<T>(this T oldEntity, T currentEntity)
        {
            IList<string> alanlar = new List<string>();
            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                if (oldEntity.GetType().GetProperty(prop.Name) == null) continue;
                var oldValue = prop.GetValue(oldEntity) ?? string.Empty;

                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                if (prop.PropertyType == typeof(byte[]))
                {
                    if (string.IsNullOrEmpty(oldValue.ToString()))
                        oldValue = new byte[] { 0 };
                    if (string.IsNullOrEmpty(currentValue.ToString()))
                        currentValue = new byte[] { 0 };
                    if (((byte[])oldValue).Length != ((byte[])currentValue).Length)
                        alanlar.Add(prop.Name);
                }
                else if (!currentValue.Equals(oldValue))
                    alanlar.Add(prop.Name);
            }
            return alanlar;
        }
    }
}

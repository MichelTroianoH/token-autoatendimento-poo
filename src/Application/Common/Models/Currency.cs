using System.Globalization;

namespace Application.Common.Models;

public struct Currency
{
    public static string StringFormat(decimal value)
    {
        return value.ToString("C", new CultureInfo("pt-BR"));
    }
    
    public static string? StringFormatWithoutCurrency(decimal value)
    {
        return value < 0 ? null : StringFormat(value)?.Replace("R$", "").Trim();
    }
}
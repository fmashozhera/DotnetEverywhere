namespace BlazorWebApp.Models;

public class ExchangeRateDetails
{
    public ExchangeRateDetails(string baseCurrency, string targetCurrency, decimal rate)
    {
        BaseCurrency = baseCurrency;
        TargetCurrency = targetCurrency;
        Rate = rate;
    }
    public string? BaseCurrency { get; set; }
    public string? TargetCurrency { get; set; }
    public Decimal Rate { get; set; }
}

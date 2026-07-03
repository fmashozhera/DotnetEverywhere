namespace BlazorWebApp.Client.Models;

public class ExchangeRatesListResponse
{
    public bool Success { get; set; }
    public string? Terms { get; set; }
    public string?Privacy { get; set; }
    public long TimeStamp { get; set; }
    public String? Source { get; set; }
    public Dictionary<string, decimal> Quotes { get; set; } = new();

}

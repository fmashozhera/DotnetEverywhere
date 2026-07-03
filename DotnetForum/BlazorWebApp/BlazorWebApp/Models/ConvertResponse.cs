namespace BlazorWebApp.Models;

public class ConvertResponse
{
    public bool Success { get; set; }
    public string? Terms { get; set; }
    public string? Privacy { get; set; }
    public ConvertQuery? Query { get; set; }
    public ConvertInfo? Info { get; set; }
    public decimal Result { get; set; }
}

public class ConvertQuery
{
    public string? From { get; set; }
    public string? To { get; set; }
    public decimal Amount { get; set; }
}

public class ConvertInfo
{
    public decimal Quote { get; set; }
}

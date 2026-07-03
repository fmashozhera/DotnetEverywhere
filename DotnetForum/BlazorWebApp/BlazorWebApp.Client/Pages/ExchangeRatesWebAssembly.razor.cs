using BlazorWebApp.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorWebApp.Client.Constants;
using System.Diagnostics;

namespace BlazorWebApp.Client.Pages;

public partial class ExchangeRatesWebAssembly
{
    #region Dependency Injection
    [Inject] IHttpClientFactory HttpClientFactory { get; set; } = default!;
    private IEnumerable<ExchangeRateDetails>? exchangeRateDetailsList;
    #endregion

    #region Private Variables
    private int currentPage = 1;
    private int pageSize = 10;
    private string? source;
    private string? terms;
    private string? privacy;
    private DateTimeOffset? timestamp;
    private bool isLoading = false;
    #endregion

    #region Pagination
    private int totalPages => exchangeRateDetailsList == null ? 1
        : (int)Math.Ceiling(exchangeRateDetailsList.Count() / (double)pageSize);

    private IEnumerable<ExchangeRateDetails> pagedList => exchangeRateDetailsList == null
        ? Enumerable.Empty<ExchangeRateDetails>()
        : exchangeRateDetailsList.Skip((currentPage - 1) * pageSize).Take(pageSize);
    #endregion

    #region Lifecycle Methods
    protected override async Task OnInitializedAsync()
    {
        await LoadExchangeRates();
    }
    #endregion

    #region Private Methods
    private async Task LoadExchangeRates()
    {
        isLoading = true;
        try
        {
            var http = HttpClientFactory.CreateClient(AppConstants.HTTP_CLIENT_NAME);
            var response = await http.GetFromJsonAsync<ExchangeRatesListResponse>($"live?access_key={AppConstants.ACCESS_KEY}");
            if (response != null && response.Success)
            {
                exchangeRateDetailsList = response.Quotes.Select(q => new ExchangeRateDetails(response.Source, q.Key.Substring(3), q.Value)).ToList();
                source = response.Source;
                terms = response.Terms;
                privacy = response.Privacy;
                timestamp = DateTimeOffset.FromUnixTimeSeconds(response.TimeStamp);
                currentPage = 1;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            isLoading = false;
        }
    }
    #endregion

    #region Event Handlers
    private void GoToPage(int page) => currentPage = Math.Clamp(page, 1, totalPages);
    private async Task RefreshRates() => await LoadExchangeRates();
    #endregion
}

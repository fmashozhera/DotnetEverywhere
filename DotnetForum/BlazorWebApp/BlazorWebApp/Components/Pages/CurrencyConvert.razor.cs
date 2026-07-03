using BlazorWebApp.Constants;
using BlazorWebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Diagnostics;

namespace BlazorWebApp.Components.Pages;

public partial class CurrencyConvert
{
    #region Dependency Injection
    [Inject] IHttpClientFactory HttpClientFactory { get; set; } = default!;
    [Inject] IJSRuntime JS { get; set; } = default!;
    #endregion

    #region Parameters
    [Parameter] public string From { get; set; } = "USD";
    [Parameter] public string To { get; set; } = "EUR";
    #endregion

    #region Private Variables
    private string fromInput = "USD";
    private string toInput = "EUR";
    private decimal amount = 1;
    private ConvertResponse? convertResult;
    private bool isLoading = false;
    #endregion

    #region Lifecycle Methods
    protected override Task OnParametersSetAsync()
    {
        fromInput = From;
        toInput = To;
        return Task.CompletedTask;
    }
    #endregion

    #region Private Methods
    private async Task Convert()
    {
        isLoading = true;
        convertResult = null;
        try
        {
            var http = HttpClientFactory.CreateClient(AppConstants.HTTP_CLIENT_NAME);
            var url = $"convert?access_key={AppConstants.ACCESS_KEY}&from={fromInput}&to={toInput}&amount={amount}";
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                await JS.InvokeVoidAsync("alert", $"Request failed with status {(int)response.StatusCode} ({response.ReasonPhrase}). Please try again.");
                return;
            }

            convertResult = await response.Content.ReadFromJsonAsync<ConvertResponse>();

            if (convertResult != null && !convertResult.Success)
            {
                await JS.InvokeVoidAsync("alert", $"Invalid currency code '{fromInput}' or '{toInput}'. Please enter valid 3-letter ISO codes.");
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
    private void SwapCurrencies()
    {
        (fromInput, toInput) = (toInput, fromInput);
    }
    #endregion
}

using Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "1c356d337f2b411093205865c58c287f";
        private List<Coin> coins = new List<Coin>
        {
            new Coin { IdCoin = "1", NameCoin = "USD" },
            new Coin { IdCoin = "2", NameCoin = "EUR" },
            new Coin { IdCoin = "3", NameCoin = "GBP" },
            new Coin { IdCoin = "4", NameCoin = "CNY" },
            new Coin { IdCoin = "5", NameCoin = "ILS" }
        };

        public ExchangeRatesRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Coin>> GetCoins()
        {
            return await Task.FromResult(coins);
        }

        public async Task<List<ExchangeRate>> GetExchangeRates(string baseCurrency)
        {
            if (baseCurrency == "USD")
            {
                var url = $"https://openexchangerates.org/api/latest.json?app_id={_apiKey}&base={baseCurrency}&symbols={string.Join(",", coins.Select(c => c.NameCoin).Where(c => c != baseCurrency))}";

                var response = await _httpClient.GetFromJsonAsync<ExchangeRatesApiResponse>(url);

                if (response == null || response.Rates == null)
                {
                    return new List<ExchangeRate>();
                }

                var exchangeRates = response.Rates.Select(rate => new ExchangeRate
                {
                    BaseCurrency = baseCurrency,
                    TargetCurrency = rate.Key,
                    Rate = rate.Value
                }).ToList();

                return exchangeRates;
            }
            else
            {
                var random = new Random();
                var exchangeRates = coins.Where(c => c.NameCoin != baseCurrency).Select(c => new ExchangeRate
                {
                    BaseCurrency = baseCurrency,
                    TargetCurrency = c.NameCoin,
                    Rate = (decimal)(random.NextDouble() * (5.0 - 0.5) + 0.5) // Random rate between 0.5 and 5.0
                }).ToList();

                return await Task.FromResult(exchangeRates);
            }
        }
    }
}

public class ExchangeRatesApiResponse
{
    public Dictionary<string, decimal>? Rates { get; set; }
}


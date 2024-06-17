using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IExchangeRatesService
    {
        Task<List<Coin>> GetCoins();
        Task<List<ExchangeRate>> GetExchangeRates(string baseCurrency);
    }
}

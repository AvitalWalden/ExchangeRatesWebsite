using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IExchangeRatesRepository
    {
        Task<List<Coin>> GetCoins();
        Task<List<ExchangeRate>> GetExchangeRates(string baseCurrency);
    }
}

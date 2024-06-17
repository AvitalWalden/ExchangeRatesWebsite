using Models;
using Repository;
using System.ComponentModel.Design;


namespace Service
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IExchangeRatesRepository _exchangeRatesRepository;

        public ExchangeRatesService(IExchangeRatesRepository exchangeRatesRepository)
        {
            _exchangeRatesRepository = exchangeRatesRepository;
        }

        public async Task<List<Coin>> GetCoins()
        {
            return await _exchangeRatesRepository.GetCoins();
        }

        public async Task<List<ExchangeRate>> GetExchangeRates(string baseCurrency)
        {
            return await _exchangeRatesRepository.GetExchangeRates(baseCurrency);
        }
    }
}

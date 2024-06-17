using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace ExchangeRatesWebsite.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService;

        public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;

        }
        [HttpGet("rates/{baseCurrency}")]
        public async Task<ActionResult<List<ExchangeRate>>> GetExchangeRates(string baseCurrency)
        {
            try
            {
                var rates = await _exchangeRatesService.GetExchangeRates(baseCurrency);
                if (rates != null)
                {
                    return Ok(rates);
                }
                else { return BadRequest(); }
            }
            catch
            (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex);
            }
        }

        [HttpGet("currencies")]
        public async Task<ActionResult<List<Coin>>> GetCurrencies()
        {
            try
            {
                var coins = await _exchangeRatesService.GetCoins();
                if (coins != null)
                {
                    return Ok(coins);
                }
                else { return BadRequest(); }
            }
            catch
            (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex);
            }
          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TradeCaretaker.Exchanges
{
    public class Poloniex : IExchange
    {
        private readonly HttpClient _client = new HttpClient();

        public Poloniex(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri(configuration["exchanges:poloniex:uri"]);
        }

        public async Task<IEnumerable<Trade>> GetTradeHistory(string asset, DateTime dateFrom, DateTime dateTo)
        {
            var unixFrom = (int) dateFrom.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var unixTo = (int) dateTo.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            //USDT_BTC
            var result =
                await _client.GetAsync(
                    $"public?command=returnTradeHistory&currencyPair={asset}&start={unixFrom}&end={unixTo}");
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Trade>>(content);
        }
    }
}
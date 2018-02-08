using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Trader.Exchanges
{
    public class Poloniex : IExchange
    {
        HttpClient _client = new HttpClient();

        public Poloniex()
        {
            _client.BaseAddress = new Uri("https://poloniex.com/");
        }

        public async Task<IEnumerable<Trade>> GetTradeHistory(string asset, DateTime dateFrom, DateTime dateTo)
        {
            var unixFrom = (Int32)(dateFrom.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var unixTo = (Int32)(dateTo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //USDT_BTC
            var result = await _client.GetAsync($"public?command=returnTradeHistory&currencyPair={asset}&start={unixFrom}&end={unixTo}");
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Trade>>(content);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Trader.Exchanges
{
    public class Poloniex: IExchange
    {
        HttpClient _client = new HttpClient();

        public Poloniex()
        {
            _client.BaseAddress = new Uri("https://poloniex.com/");
        }

        public async Task<Trade[]> GetTradeHistory(DateTime dateFrom, DateTime dateTo)
        {
            var unixFrom = (Int32)(dateFrom.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var unixTo = (Int32)(dateTo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var result = await _client.GetAsync($"public?command=returnTradeHistory&currencyPair=USDT_BTC&start={unixFrom}&end={unixTo}");
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Trade[]>(content);
        }
    }


    public struct Trade
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zoe.Exchanges
{
    public interface IExchange
    {
        Task<IEnumerable<Trade>> GetTradeHistory(string asset, DateTime dateFrom, DateTime dateTo);
    }
}
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker.Backfill
{
    public class BackFillStorage : IBackfillStorage
    {
        private readonly IMongoDatabase _database;

        public BackFillStorage(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task StoreTrades(string name, IEnumerable<Trade> trades)
        {
            var collection = _database.GetCollection<Trade>(name);

            if (collection == null)
            {
                await _database.CreateCollectionAsync(name);
                collection = _database.GetCollection<Trade>(name);
            }

            await collection.InsertManyAsync(trades);
        }

        public Task<List<Trade>> GetTrades(string name, DateTime dateFrom, DateTime dateTo)
        {
            var collection = _database.GetCollection<Trade>(name);
            return collection.AsQueryable().Where(trade => trade.Date > dateFrom && trade.Date < dateTo)
                .ToListAsync();
        }
    }
}
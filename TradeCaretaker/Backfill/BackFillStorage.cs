using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
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

        public async Task CreateIfNotExists(string name)
        {
            /// for debug purpose only;
            await _database.DropCollectionAsync(name);

            await _database.CreateCollectionAsync(name);
        }

        public Task StoreTrades(string name, IEnumerable<Trade> trades)
        {
            var collection = _database.GetCollection<BsonDocument>(name);
            return collection.InsertManyAsync(trades.Select(trade => trade.ToBsonDocument()));
        }
    }
}
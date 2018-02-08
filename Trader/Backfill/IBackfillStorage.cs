using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trader.Exchanges;

namespace Trader.Backfill
{
    public interface IBackfillStorage
    {
        Task CreateIfNotExists(string name);
        Task StoreTrades(string name, IEnumerable<Trade> trades);
    }

    public class BackFillStorage : IBackfillStorage
    {
        private readonly IMongoDatabase _database;

        public BackFillStorage(IMongoDatabase database)
        {
            _database = database;
        }

        public Task CreateIfNotExists(string name)
        {
            return _database.CreateCollectionAsync(name);
        }

        public Task StoreTrades(string name, IEnumerable<Trade> trades)
        {
            var collection = _database.GetCollection<BsonDocument>(name);
            return collection.InsertManyAsync(trades.Select(trade => trade.ToBsonDocument()));
        }
    }
}
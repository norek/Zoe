using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TradeCaretaker.Framework.Ioc
{
    public class MongoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
            {
                var settings = c.Resolve<IConfiguration>();
                return new MongoClient(settings["mongodb:connectionString"]);
            }).SingleInstance();

            builder.Register((c, p) =>
            {
                var mongoClient = c.Resolve<MongoClient>();
                var settings = c.Resolve<IConfiguration>();
                var database = mongoClient.GetDatabase(settings["mongodb:databaseName"]);

                return database;
            }).As<IMongoDatabase>();
        }
    }
}
using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using System.Text;

namespace Trader.Framework.Ioc
{
    public class MongoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
            {
                var settings = c.Resolve<IConfiguration>();
                return new MongoClient(settings["Mongodb:ConnectionString"]);
            }).SingleInstance();

            builder.Register((c, p) =>
            {
                var mongoClient = c.Resolve<MongoClient>();
                var settings = c.Resolve<IConfiguration>();
                var database = mongoClient.GetDatabase(settings["Mongodb:DatabaseName"]);

                return database;
            }).As<IMongoDatabase>();
        }
    }
}

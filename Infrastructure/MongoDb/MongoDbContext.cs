using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.DependencyInjection;
using Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace Infrastructure.MongoDb
{
    public interface IMongoDbContext
    {
        IMongoDatabase GetDatabase();
        IMongoClient GetMongoClient();
    }

    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoClient _mongoClient;

        public MongoDbContext(IDependencyResolver resolver)
        {
            var mongodbConfig = resolver.Resolve<MongoDbConfig>(); 
            var logger = resolver.Resolve<ILogger>(); 

            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(mongodbConfig.ConnectionString));
            mongoClientSettings.ClusterConfigurator = cb => {
                cb.Subscribe<CommandStartedEvent>(e => {
                    logger.LogInformation($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };
            _mongoClient = new MongoClient(mongodbConfig.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(mongodbConfig.DataBaseName);

            var camelCaseConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConventionPack, type => true);

        }

        public IMongoDatabase GetDatabase()
        {
            return _mongoDatabase;
        }

        public IMongoClient GetMongoClient()
        {
            return _mongoClient;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
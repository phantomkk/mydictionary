using System.Net;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.DataImport
{
    public class DataImportService:IDataImportService
    {
        private ImportConfig _importConfig;
        private readonly IMongoDatabase _mongoDatabase;

        public DataImportService(ImportConfig config, IMongoDbContext mongoDbContext)
        {
            _importConfig = config;
            _mongoDatabase = mongoDbContext.GetDatabase();
        }
        
        public async Task Import(string path)
        {
            using var jsonReader = new JsonReader(await System.IO.File.ReadAllTextAsync(path));
            var importTemplate = BsonSerializer.Deserialize<ImportTemplate>(jsonReader);
            foreach (var data in importTemplate.Data)
            {
                await Upsert(importTemplate, data);
            }
        }

        private async Task Upsert(ImportTemplate importTemplate, BsonDocument bsonDocument)
        {
            var filterDef = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument.GetElement("_id").Value);
            var collections = _mongoDatabase.GetCollection<BsonDocument>(importTemplate.Schema);
            await collections.UpdateOneAsync(filterDef, bsonDocument, new UpdateOptions() {IsUpsert = true});
        }
    }

    public interface IDataImportService
    {
        Task Import(string path);
    }
}
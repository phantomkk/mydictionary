using System.Collections.Generic;
using MongoDB.Bson;

namespace Infrastructure.DataImport
{
    public class ImportTemplate
    {
        public string Schema { get; set; }
        public string ImportMode { get; set; }
        
        public IList<BsonDocument> Data { get; set; }
    }
}
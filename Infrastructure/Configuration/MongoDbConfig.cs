namespace Infrastructure.Configuration
{
    [ConfigPath("MongoDb")]
    public class MongoDbConfig
    {
     public string ConnectionString { get; set; }   
     public string DataBaseName { get; set; }
    }
}
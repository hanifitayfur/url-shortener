namespace ITunes.UrlShortener.Entities.AppSettings
{
    public class MongoDbSettings
    {
        public string ConnectionString;
        public string Database;

        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseValue = nameof(Database);
    }
}
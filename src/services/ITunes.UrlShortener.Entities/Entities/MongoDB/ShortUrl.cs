using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ITunes.UrlShortener.Entities.Entities.MongoDB
{
    public class ShortUrl : MongoDbEntity
    {
        [BsonElement("ShortURL")] public string ShortUrlValue { get; set; }

        [BsonElement("LongURL")] public string LongUrlValue { get; set; }

        [BsonElement("ExpireDate")] public DateTime ExpireDate { get; set; }

        public User User { get; set; }
    }
}
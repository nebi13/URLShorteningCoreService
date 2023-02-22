using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShorteningCoreService.Models
{
    public class ShortUrlModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string OriginalUrl { get; set; }
    }

    public class URLShorteningDatabaseSettings : IURLShorteningDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string URLShorteningCollectionName { get; set; } = null!;
    }

    public interface IURLShorteningDatabaseSettings
    {
        string URLShorteningCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

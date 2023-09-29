using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtClub.BFF.Models
{
    public record struct LearningPath
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        
        [BsonElement("language")]
        public string Language { get; init; }
        
        [BsonElement("title")]
        public string Title { get; init; }

        [BsonElement("description")]
        public string Description { get; init; }
        
        [BsonElement("enabled")]
        public bool Enabled { get; init; }

        [BsonElement("steps")]
        public IList<Step> Steps { get; init; }
    }
}

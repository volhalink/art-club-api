using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ArtClub.BFF.Models
{
    public record LearningPathView
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; init; }

        [BsonElement("learningPathId")]
        [JsonPropertyName("id")]
        public string LearningPathId { get; init; }

        [BsonElement("language")]
        public string Language { get; init; }

        [BsonElement("title")]
        public string Title { get; init; }

        [BsonElement("description")]
        public string Description { get; init; }

        [BsonElement("enabled")]
        public bool Enabled { get; init; }
    }

    public record LearningPath : LearningPathView
    {

        [BsonElement("steps")]
        public IList<Step>? Steps { get; init; }
    }
}

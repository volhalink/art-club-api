﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ArtClub.BFF.Models
{
    //[JsonConverter(typeof(JsonStringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum StepType
    {
        Start =  0,
        Read = 1,
        Practice = 2,
    }

    public record struct Step
    {
        [BsonElement("id")]
        public string Id { get; init; }

        [BsonElement("order")]
        public int Order { get; init; }
        [BsonElement("title")]
        public string Title { get; init; }
        [BsonElement("description")]
        public string Description { get; init; }
        
        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public StepType Type { get; init; }
       
        [BsonElement("exercises")]
        public IList<Exercise> Exercises { get; init; }
    }
}

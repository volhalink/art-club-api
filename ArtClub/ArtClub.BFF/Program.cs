using ArtClub.BFF.Services;
using MongoDB.Driver;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

var mongoDbSettings = new MongoDbSettings();
builder.Configuration.GetSection(nameof(MongoDbSettings)).Bind(mongoDbSettings);
var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
builder.Services.AddSingleton(mongoClient.GetDatabase(mongoDbSettings.DatabaseName));
builder.Services.AddScoped<ILearningPathProvider, MongoDbLearningPathProvider>(s => new MongoDbLearningPathProvider(s.GetService<IMongoDatabase>(), mongoDbSettings.LearningPathesCollectionName));

var app = builder.Build();

app.MapGet("/api/{language}/learningpath", (string language, ILearningPathProvider learningPathProvider) => learningPathProvider.GetLearningPaths(language));
app.MapGet("/api/{language}/learningpath/{id}", (string language, string id, ILearningPathProvider learningPathProvider) => learningPathProvider.GetLearningPath(language, id));

app.Run();

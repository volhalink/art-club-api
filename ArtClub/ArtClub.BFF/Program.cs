using ArtClub.BFF.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = new MongoDbSettings();
builder.Configuration.GetSection(nameof(MongoDbSettings)).Bind(mongoDbSettings);
var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
builder.Services.AddSingleton(mongoClient.GetDatabase(mongoDbSettings.DatabaseName));
builder.Services.AddScoped<ILearningPathProvider, MongoDbLearningPathProvider>(s => new MongoDbLearningPathProvider(s.GetService<IMongoDatabase>(), mongoDbSettings.LearningPathesCollectionName));

var app = builder.Build();

app.MapGet("/api/{language}/learningpath", (string language, ILearningPathProvider learningPathProvider) => learningPathProvider.GetLearningPaths(language));

app.Run();

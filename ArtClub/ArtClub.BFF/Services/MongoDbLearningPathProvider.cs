using ArtClub.BFF.Models;
using MongoDB.Driver;

namespace ArtClub.BFF.Services
{
    public class MongoDbLearningPathProvider : ILearningPathProvider
    {
        private IMongoCollection<LearningPath> _learningPathCollection;

        public MongoDbLearningPathProvider(IMongoDatabase database, string collectionName)
        {
           _learningPathCollection = database.GetCollection<LearningPath>(collectionName);
        }

        public IList<LearningPath> GetLearningPaths(string language)
        {
            var filter = Builders<LearningPath>.Filter.Eq(p => p.Language, language)
                & Builders<LearningPath>.Filter.Eq(p => p.Enabled, true);
            var learningPathes = _learningPathCollection.Find(filter).ToList();

            return learningPathes;
        }

        public LearningPath GetTrainingPath(string id)
        {
            var filter = Builders<LearningPath>.Filter.Eq(p => p.Id, id)
                & Builders<LearningPath>.Filter.Eq(p => p.Enabled, true);
            var learningPath = _learningPathCollection.Find(filter).FirstOrDefault();

            return learningPath;
        }
    }
}

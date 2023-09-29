using ArtClub.BFF.Models;

namespace ArtClub.BFF.Services
{
    public interface ILearningPathProvider
    {
        IList<LearningPathView> GetLearningPaths(string language);
        LearningPath GetLearningPath(string language, string id);

    }
}

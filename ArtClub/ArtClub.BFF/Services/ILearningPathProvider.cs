using ArtClub.BFF.Models;

namespace ArtClub.BFF.Services
{
    public interface ILearningPathProvider
    {
        IList<LearningPath> GetLearningPaths(string language);
        LearningPath GetTrainingPath(string id);

    }
}

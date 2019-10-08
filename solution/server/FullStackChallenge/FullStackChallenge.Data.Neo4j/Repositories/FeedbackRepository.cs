using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories;

namespace FullStackChallenge.Data.Neo4j.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly INeo4jBaseRepository<Feedback> _baseRepository;
        
        public FeedbackRepository(INeo4jBaseRepository<Feedback> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        
        public void Dispose()
        {
            _baseRepository?.Dispose();
        }
    }
}
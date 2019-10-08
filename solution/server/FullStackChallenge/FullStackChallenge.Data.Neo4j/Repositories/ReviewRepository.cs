using System;
using System.Linq;
using System.Threading.Tasks;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories;

namespace FullStackChallenge.Data.Neo4j.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly INeo4jBaseRepository<Review> _baseRepository;
        
        public ReviewRepository(INeo4jBaseRepository<Review> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        void IDisposable.Dispose()
        {
            _baseRepository?.Dispose();
        }

        public Task<bool> InsertAsync(Review model)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> GetLastEmployeeReviewAsync(int employeeId)
        {
            var reviewEmployeeRelationship = new Tuple<string, string, int>(typeof(Employee).Name, "REVIEWS", employeeId);
            var reviews = await _baseRepository.GetAsync(reviewEmployeeRelationship);

            return reviews?.FirstOrDefault();
        }

        public Task<bool> UpdateAsync(Review review)
        {
            return _baseRepository.UpdateAsync(review);
        }
    }
}
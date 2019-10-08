using System;
using System.Collections.Generic;
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

        public async Task<Review> InsertAsync(int employeeId, Review review)
        {
            var reviewEmployeeRelationship = new SimpleNodeRelationship
            {
                TargetNodeName = typeof(Employee).Name,
                RelationshipName = "REVIEWS",
                TargetNodeId = employeeId
            };
            return await _baseRepository.InsertWithRelationshipAsync(review, reviewEmployeeRelationship);
        }

        public async Task<Review> GetLastEmployeeReviewAsync(int employeeId)
        {
            var reviewEmployeeRelationship = new SimpleNodeRelationship
            {
                TargetNodeName = typeof(Employee).Name,
                RelationshipName = "REVIEWS",
                TargetNodeId = employeeId
            };
            var reviews = await _baseRepository.GetByRelationshipAsync(reviewEmployeeRelationship, "createdDate", "desc");

            return reviews?.FirstOrDefault();
        }

        public Task<Review> UpdateAsync(Review review)
        {
            return _baseRepository.UpdateAsync(review);
        }
        
        public async Task SetPerformanceReviewFeedbackAsigneeAsync(int reviewId, int[] reviewFeedbackAsigneeIds)
        {

            var relationships = reviewFeedbackAsigneeIds.Select(employeeId =>
                new SimpleNodeRelationship
                {
                    TargetNodeName = typeof(Employee).Name,
                    RelationshipName = "IS_REVIEW_ASSIGNEE_OF",
                    TargetNodeId = employeeId
                });
            
            foreach (var relationship in relationships)
                await _baseRepository.SetRelationshipAsync(reviewId, relationship);
        }

        public Task<List<int>> GetFeedbackAssigneesByReviewId(int reviewId)
        {
            return Task.FromResult(new List<int>{0});
        }
    }
}
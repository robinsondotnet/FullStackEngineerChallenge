using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackChallenge.Data.Models;

namespace FullStackChallenge.Data.Repositories
{
    public interface IReviewRepository : IDisposable
    {
        Task<Review> GetLastEmployeeReviewAsync(int employeeId);

        Task<Review> InsertAsync(int employeeId, Review review);

        Task<Review> UpdateAsync(Review review);

        Task SetPerformanceReviewFeedbackAsigneeAsync(int reviewId, int[] reviewFeedbackAsigneeIds);
        
        Task<List<int>> GetFeedbackAssigneesByReviewId(int reviewId);
    }
}
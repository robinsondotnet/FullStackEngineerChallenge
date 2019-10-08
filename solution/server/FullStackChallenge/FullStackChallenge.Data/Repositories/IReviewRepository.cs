using System;
using System.Threading.Tasks;
using FullStackChallenge.Data.Models;

namespace FullStackChallenge.Data.Repositories
{
    public interface IReviewRepository : IDisposable
    {
        Task<Review> GetLastEmployeeReviewAsync(int employeeId);

        Task<bool> InsertAsync(Review review);

        Task<bool> UpdateAsync(Review review);
    }
}
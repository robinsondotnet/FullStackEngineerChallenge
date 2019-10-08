using System.Linq;
using System.Threading.Tasks;
using FullStackChallenge.Data.Commands;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories;
using FullStackChallenge.Infra;

namespace FullStackChallenge.Data.CommandHandlers
{
    public class UpdateEmployeeAndReviewCommandHandler : ICommandHandler<UpdateEmployeeAndReviewCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IReviewRepository _reviewRepository;

        public UpdateEmployeeAndReviewCommandHandler(IEmployeeRepository employeeRepository, IReviewRepository reviewRepository)
        {
            _employeeRepository = employeeRepository;
            _reviewRepository = reviewRepository;
        }
        
        public async Task HandleAsync(UpdateEmployeeAndReviewCommand command)
        {
            await _employeeRepository.UpdateAsync(command.Employee);

            if (!command.PerformanceReviewValue.HasValue)
                return;

            var lastEmployeeReview = await UpsertLastPerformanceReviewAsync(command.Employee.Id, command.PerformanceReviewValue.Value);

            if (command.ReviewFeedbackAssigneeIds == null || !command.ReviewFeedbackAssigneeIds.Any())
                return;

            await _reviewRepository.SetPerformanceReviewFeedbackAsigneeAsync(lastEmployeeReview.Id, command.ReviewFeedbackAssigneeIds);
        }

        private async Task<Review> UpsertLastPerformanceReviewAsync(int employeeId, int performanceReviewValue)
        {
            var lastEmployeeReview = await _reviewRepository.GetLastEmployeeReviewAsync(employeeId);

            if (lastEmployeeReview == null)
                return await _reviewRepository.InsertAsync(employeeId, new Review {Value = performanceReviewValue});

            lastEmployeeReview.Value = performanceReviewValue;
            return await _reviewRepository.UpdateAsync(lastEmployeeReview);
        }
    }
}
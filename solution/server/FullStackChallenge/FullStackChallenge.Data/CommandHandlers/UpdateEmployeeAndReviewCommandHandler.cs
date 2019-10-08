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

            await UpsertPerformanceReviewAsync(command.Employee.Id, command.PerformanceReviewValue.Value);
        }

        private async Task UpsertPerformanceReviewAsync(int employeeId, int performanceReviewValue)
        {
            var lastEmployeeReview = await _reviewRepository.GetLastEmployeeReviewAsync(employeeId);

            if (lastEmployeeReview == null)
            {
                await _reviewRepository.InsertAsync(new Review {Value = performanceReviewValue});
                return;
            }

            lastEmployeeReview.Value = performanceReviewValue;
            await _reviewRepository.UpdateAsync(lastEmployeeReview);
        }
    }
}
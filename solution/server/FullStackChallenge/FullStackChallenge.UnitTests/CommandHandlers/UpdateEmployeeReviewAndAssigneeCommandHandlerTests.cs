using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FullStackChallenge.Data.CommandHandlers;
using FullStackChallenge.Data.Commands;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories;
using NSubstitute;
using Xunit;

namespace FullStackChallenge.UnitTests.CommandHandlers
{
    public class UpdateEmployeeReviewAndAssigneeCommandHandlerTests
    {
        private readonly IFixture _autoFixture;
        
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IReviewRepository _reviewRepository;

        public UpdateEmployeeReviewAndAssigneeCommandHandlerTests()
        {
            _autoFixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _employeeRepository = _autoFixture.Freeze<IEmployeeRepository>();
            _reviewRepository = _autoFixture.Freeze<IReviewRepository>();
        }

        [Fact]
        public async Task HandleAsync_PerformanceReviewValueIsNull_ShouldNotUpdateReviewNorFeedBackAssignee()
        {
            //  Arrange
            var employee = _autoFixture.Create<Employee>();
            var command = new UpdateEmployeeReviewAndAssigneeCommand
            {
                Employee = employee,
                PerformanceReviewValue = null
            };
          
            var sut = CreateSut();
            
            // Act
            await sut.HandleAsync(command);

            // Verify
            await _employeeRepository.Received(1).UpdateAsync(Arg.Is(employee));
            await _reviewRepository.DidNotReceive().GetLastEmployeeReviewAsync(Arg.Any<int>());
            await _reviewRepository.DidNotReceive().InsertAsync(Arg.Any<int>(), Arg.Any<Review>());
            await _reviewRepository.DidNotReceive().UpdateAsync(Arg.Any<Review>());
        }

        [Fact]
        public async Task HandleAsync_NotFoundLastReviewAndNoFeedbackAssignees_ShouldCreateNewReview()
        {
            // Arrange
            var employee = _autoFixture.Create<Employee>();
            var command = new UpdateEmployeeReviewAndAssigneeCommand
            {
                Employee = employee,
                PerformanceReviewValue = 5,
                ReviewFeedbackAssigneeIds = null
            };

            _reviewRepository.GetLastEmployeeReviewAsync(Arg.Is(employee.Id)).Returns(Task.FromResult<Review>(null));
            
            var sut = CreateSut();

            // Act
            await sut.HandleAsync(command);

            // Verify
            await _employeeRepository.Received(1).UpdateAsync(Arg.Is(employee));
            await _reviewRepository
                .Received(1)
                .InsertAsync(Arg.Is(employee.Id), Arg.Is<Review>(r => r.Value == command.PerformanceReviewValue));
        }
        
        [Fact]
        public async Task HandleAsync_FoundLastReviewAndFeedbackAssignees_ShouldUpdateExistingReviewAndSetFeedbackAssignees()
        {
            // Arrange
            var employee = _autoFixture.Create<Employee>();
            var reviewFeedbackAssignees = _autoFixture.CreateMany<int>().ToArray();
            var command = new UpdateEmployeeReviewAndAssigneeCommand
            {
                Employee = employee,
                PerformanceReviewValue = 5,
                ReviewFeedbackAssigneeIds = reviewFeedbackAssignees
            };

            var review = _autoFixture.Create<Review>();

            _reviewRepository.GetLastEmployeeReviewAsync(Arg.Is(employee.Id)).Returns(Task.FromResult(review));
            _reviewRepository.UpdateAsync(Arg.Is<Review>(r => r.Value == command.PerformanceReviewValue))
                .Returns(Task.FromResult(review));
            
            var sut = CreateSut();

            // Act
            await sut.HandleAsync(command);

            // Verify
            await _employeeRepository.Received(1).UpdateAsync(Arg.Is(employee));
            await _reviewRepository
                .Received(0)
                .InsertAsync(Arg.Any<int>(), Arg.Any<Review>());
            await _reviewRepository.SetPerformanceReviewFeedbackAsigneeAsync(Arg.Is(review.Id),
                Arg.Is(reviewFeedbackAssignees));
        }

        private UpdateEmployeeReviewAndAssigneeCommandHandler CreateSut() 
            => new UpdateEmployeeReviewAndAssigneeCommandHandler(_employeeRepository, _reviewRepository);
    }
}
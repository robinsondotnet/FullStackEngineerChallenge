using System.Threading.Tasks;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Infra;

namespace FullStackChallenge.Data.Commands
{
    public class UpdateEmployeeReviewAndAssigneeCommand : ICommand
    {
        public Employee Employee;

        public int? PerformanceReviewValue;
        
        public int[] ReviewFeedbackAssigneeIds { get; set; }
    }
}
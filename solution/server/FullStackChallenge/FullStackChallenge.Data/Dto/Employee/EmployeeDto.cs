namespace FullStackChallenge.Data.Dto.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int? PerformanceReviewValue { get; set; }
        public int[] ReviewFeedbackAssigneeIds { get; set; }
    }
}

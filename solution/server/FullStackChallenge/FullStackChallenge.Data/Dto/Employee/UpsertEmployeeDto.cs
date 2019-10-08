namespace FullStackChallenge.Data.Dto.Employee
{
    public class UpsertEmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int? PerformanceReviewValue { get; set; }
    }
}

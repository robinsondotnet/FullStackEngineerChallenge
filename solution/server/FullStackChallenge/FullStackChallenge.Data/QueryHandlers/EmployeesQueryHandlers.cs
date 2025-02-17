﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackChallenge.Data.Dto.Employee;
using FullStackChallenge.Data.Queries;
using FullStackChallenge.Data.Repositories;
using FullStackChallenge.Infra;

namespace FullStackChallenge.Data.QueryHandlers
{
    public class EmployeesQueryHandler : IQueryHandler<GetEmployeesWithReviewAndAssigneeQuery, List<EmployeeDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesQueryHandler(IReviewRepository reviewRepository, IEmployeeRepository employeeRepository)
        {
            _reviewRepository = reviewRepository;
            _employeeRepository = employeeRepository;
        }
        
        public async Task<List<EmployeeDto>> RetrieveAsync(GetEmployeesWithReviewAndAssigneeQuery query)
        {
            var employeesDto = new List<EmployeeDto>();
            
            var employees = await _employeeRepository.GetAsync();

            foreach (var employee in employees)
            {
                var lastPerformanceReview = await _reviewRepository.GetLastEmployeeReviewAsync(employee.Id);
                
                Console.WriteLine(lastPerformanceReview);

                var feedbackAssignees = lastPerformanceReview != null
                    ? await _reviewRepository.GetFeedbackAssigneesByReviewId(lastPerformanceReview.Id)
                    : new List<int>();

                employeesDto.Add(new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Age = employee.Age,
                    PerformanceReviewValue = lastPerformanceReview?.Value,
                    ReviewFeedbackAssigneeIds = feedbackAssignees?.ToArray()
                });
            }

            return employeesDto;
        }
    }
}
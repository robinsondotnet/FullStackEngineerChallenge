using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories.Core;

namespace FullStackChallenge.Data.Repositories
{
    public interface IEmployeeRepository : IDisposable
    {
        Task<bool> InsertAsync(Employee employee);
        
        Task<List<Employee>> GetAsync();

        Task<Employee> UpdateAsync(Employee employee);
    }
    
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IBaseRepository<Employee> _baseRepository;

        public EmployeeRepository(IBaseRepository<Employee> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        void IDisposable.Dispose()
        {
            _baseRepository?.Dispose();
        }

        public Task<bool> InsertAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAsync()
        {
            return _baseRepository.GetAsync();
        }

        public Task<Employee> UpdateAsync(Employee employee)
        {
            return _baseRepository.UpdateAsync(employee);
        }
    }
}

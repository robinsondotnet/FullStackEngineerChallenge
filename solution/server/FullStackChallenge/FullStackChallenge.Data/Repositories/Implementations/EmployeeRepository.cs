using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories.Interfaces;

namespace FullStackChallenge.Data.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IRepository<Employee> _baseRepository;

        public EmployeeRepository(IRepository<Employee> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        void IDisposable.Dispose()
        {
            _baseRepository?.Dispose();
        }

        Task<List<Employee>> IRepository<Employee>.GetAsync()
        {
            return _baseRepository.GetAsync();
        }

        Task<bool> IRepository<Employee>.InsertAsync(Employee model)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<Employee>.UpdateAsync(Employee model)
        {
            throw new NotImplementedException();
        }
    }
}

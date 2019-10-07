using FullStackChallenge.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackChallenge.Data.Repositories.Interfaces
{
    public interface IRepository<TModel> : IDisposable where TModel : IModel
    {
        Task<List<TModel>> GetAsync();

        Task<bool> UpdateAsync(TModel model);

        Task<bool> InsertAsync(TModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackChallenge.Data.Models;

namespace FullStackChallenge.Data.Repositories.Core
{
    public interface IRepository<TModel> : IDisposable where TModel : IModel
    {
        Task<List<TModel>> GetAsync();
        
        Task<TModel> UpdateAsync(TModel model);

        Task<bool> InsertAsync(TModel model);
    }
}

using FullStackChallenge.Data.Models;

namespace FullStackChallenge.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TModel> : IRepository<TModel> where TModel : IModel
    {
    }
}

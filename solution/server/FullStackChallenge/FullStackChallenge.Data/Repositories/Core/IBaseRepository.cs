using FullStackChallenge.Data.Models;

namespace FullStackChallenge.Data.Repositories.Core
{
    public interface IBaseRepository<TModel> : IRepository<TModel> where TModel : IModel
    {
    }
}

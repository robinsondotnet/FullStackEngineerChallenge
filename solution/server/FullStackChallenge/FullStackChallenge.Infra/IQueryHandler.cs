using System.Threading.Tasks;

namespace FullStackChallenge.Infra
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> RetrieveAsync(TQuery query);
    }
}
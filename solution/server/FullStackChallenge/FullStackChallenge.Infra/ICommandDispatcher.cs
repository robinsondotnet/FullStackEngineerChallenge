using System.Threading.Tasks;

namespace FullStackChallenge.Infra
{
    public interface ICommandDispatcher
    {
        Task ExecuteActionAsync();
    }
}
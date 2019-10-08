using System.Threading.Tasks;

namespace FullStackChallenge.Infra
{
    // TODO: Complete this if want to centralize logging, transaction, etc logic
    public interface ICommandDispatcher
    {
        Task ExecuteActionAsync();
    }
}
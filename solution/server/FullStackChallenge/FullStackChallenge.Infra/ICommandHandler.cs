using System.Threading.Tasks;

namespace FullStackChallenge.Infra
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
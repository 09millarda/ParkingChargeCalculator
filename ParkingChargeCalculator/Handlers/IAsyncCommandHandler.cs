using System.Threading.Tasks;

namespace ParkingChargeCalculator.Handlers
{
    public interface IAsyncCommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        ICommandValidator<TCommand> CommandValidator { get; }
        Task<TResult> ExecuteAsync(TCommand command);
    }
}

namespace ParkingChargeCalculator.Handlers
{
    public interface ICommandValidator<TCommand> where TCommand : ICommand
    {
        void Validate(TCommand command);
    }
}

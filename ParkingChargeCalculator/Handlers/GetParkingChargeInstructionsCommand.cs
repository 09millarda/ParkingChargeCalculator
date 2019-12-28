using System.IO;

namespace ParkingChargeCalculator.Handlers
{
    public class GetParkingChargeInstructionsCommand : ICommand
    {
        public string FilePath { get; set; }

        public class Validator : ICommandValidator<GetParkingChargeInstructionsCommand>
        {
            public void Validate(GetParkingChargeInstructionsCommand command)
            {
                if (!File.Exists(command.FilePath)) throw new FileNotFoundException(null, command.FilePath);
            }
        }
    }
}

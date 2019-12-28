using ParkingChargeCalculator.FileReading;
using ParkingChargeCalculator.Files;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingChargeCalculator.Handlers
{
    public interface IGetParkingChargeInstructionsCommandHandler : IAsyncCommandHandler<GetParkingChargeInstructionsCommand, IEnumerable<ParkingChargeInstruction>> { }

    public class GetParkingChargeInstructionsCommandHandler : IGetParkingChargeInstructionsCommandHandler
    {
        public IFileParser _fileParser;
        public ICommandValidator<GetParkingChargeInstructionsCommand> CommandValidator { get; }

        public GetParkingChargeInstructionsCommandHandler(IFileParser fileParser, ICommandValidator<GetParkingChargeInstructionsCommand> commandValidator)
        {
            CommandValidator = commandValidator;
            _fileParser = fileParser;
        }

        public async Task<IEnumerable<ParkingChargeInstruction>> ExecuteAsync(GetParkingChargeInstructionsCommand command)
        {
            CommandValidator.Validate(command);
            var instructionRecords = await _fileParser.ParseAsync<ParkingChargeInstructionFile>(command.FilePath).ConfigureAwait(false);

            return instructionRecords.Select(r => new ParkingChargeInstruction
            {
                ChargeEnd = r.ChargeEnd,
                ChargeStart = r.ChargeStart,
                ChargeType = r.ChargeType
            });
        }
    }
}

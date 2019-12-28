using Microsoft.Extensions.Logging;
using ParkingChargeCalculator.Handlers;
using ParkingChargeCalculator.ParkingChargeCalculators;
using ParkingChargeCalculator.Util;
using System.Threading.Tasks;

namespace ParkingChargeCalculator.Executors
{
    public class CalculateParkingChargesExecutor : IExecutor
    {
        private const string FilePath = "./ParkingChargeInstructions.csv";

        private readonly IGetParkingChargeInstructionsCommandHandler _getParkingChargeInstructionsCommandHandler;
        private readonly IParkingChargeCalculatorFactory _parkingChargeCalculatorFactory;
        private readonly ILogger<CalculateParkingChargesExecutor> _logger;

        public CalculateParkingChargesExecutor(IGetParkingChargeInstructionsCommandHandler getParkingChargeInstructionsCommandHandler,
            IParkingChargeCalculatorFactory parkingChargeCalculatorFactory,
            ILogger<CalculateParkingChargesExecutor> logger)
        {
            _logger = logger;
            _parkingChargeCalculatorFactory = parkingChargeCalculatorFactory;
            _getParkingChargeInstructionsCommandHandler = getParkingChargeInstructionsCommandHandler;
        }

        public async Task Execute()
        {
            var parkingChargeInstructions = await _getParkingChargeInstructionsCommandHandler
                .ExecuteAsync(new GetParkingChargeInstructionsCommand { FilePath = FilePath })
                .ConfigureAwait(false);

            foreach (var parkingChargeInstruction in parkingChargeInstructions)
            {
                var parkingChargeCalculator = _parkingChargeCalculatorFactory.ResolveCalculator(parkingChargeInstruction.ChargeType);
                var parkingCost = parkingChargeCalculator.CalculateChargeGbp(parkingChargeInstruction);

                _logger.LogInformation("Charge Type: '{ChargeType}', From: '{ChargeStart}', To: '{ChargeTo}', Cost: '£{Cost}'",
                    parkingChargeInstruction.ChargeType.ToString(), parkingChargeInstruction.ChargeStart.ToString(), parkingChargeInstruction.ChargeEnd.ToString(), parkingCost.To2DpString());
            }
        }
    }
}

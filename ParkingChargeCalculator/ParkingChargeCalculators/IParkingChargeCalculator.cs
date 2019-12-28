using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.Handlers;

namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public interface IParkingChargeCalculator
    {
        public ChargeType ChargeType { get; }
        decimal CalculateChargeGbp(ParkingChargeInstruction instruction);
    }
}
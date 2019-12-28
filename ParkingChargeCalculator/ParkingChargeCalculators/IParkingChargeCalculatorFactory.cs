using ParkingChargeCalculator.Files;

namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public interface IParkingChargeCalculatorFactory
    {
        IParkingChargeCalculator ResolveCalculator(ChargeType chargeType);
    }
}
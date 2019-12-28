using ParkingChargeCalculator.Files;
using System;

namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public class ParkingChargeCalculatorFactory : IParkingChargeCalculatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ParkingChargeCalculatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IParkingChargeCalculator ResolveCalculator(ChargeType chargeType)
        {
            switch (chargeType)
            {
                case ChargeType.ShortStay: return (ShortStayParkingChargeCalculator)_serviceProvider.GetService(typeof(ShortStayParkingChargeCalculator));
                case ChargeType.LongStay: return (LongStayParkingChargeCalculator)_serviceProvider.GetService(typeof(LongStayParkingChargeCalculator));
                default: throw new NotImplementedException($"ParkingChargeCalculator for ChargeType '{chargeType.ToString()}' is not registered in the ServiceProvider");
            }
        }
    }
}

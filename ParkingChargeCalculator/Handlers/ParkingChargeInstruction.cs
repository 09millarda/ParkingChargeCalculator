using ParkingChargeCalculator.Files;
using System;

namespace ParkingChargeCalculator.Handlers
{
    public class ParkingChargeInstruction
    {
        public DateTime ChargeStart { get; set; }
        public DateTime ChargeEnd { get; set; }
        public ChargeType ChargeType { get; set; }
    }
}

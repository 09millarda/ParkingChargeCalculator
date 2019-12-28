using System;

namespace ParkingChargeCalculator.Files
{
    public class ParkingChargeInstructionFile : IFileRecord
    {
        [CsvMember(0)]
        public ChargeType ChargeType { get; set; }
        [CsvMember(1)]
        public DateTime ChargeStart { get; set; }
        [CsvMember(2)]
        public DateTime ChargeEnd { get; set; }

        public void Validate()
        {
            if (ChargeEnd < ChargeStart) throw new ArgumentOutOfRangeException(nameof(ChargeEnd), $"ChargeEnd is '{ChargeEnd.ToString()}' and ChargeStart is '{ChargeStart.ToString()}' but ChargeEnd must be after greater or equal to ChargeStart");
        }
    }
}

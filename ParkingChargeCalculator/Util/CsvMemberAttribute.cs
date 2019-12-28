using System;

namespace ParkingChargeCalculator
{
    public class CsvMemberAttribute : Attribute
    {
        public int MemberIndex { get; }

        public CsvMemberAttribute(int memberIndex)
        {
            MemberIndex = memberIndex;
        }
    }
}

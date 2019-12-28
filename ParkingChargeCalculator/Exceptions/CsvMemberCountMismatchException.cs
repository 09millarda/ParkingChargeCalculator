using System;

namespace ParkingChargeCalculator.Exceptions
{
    public class CsvMemberCountMismatchException : Exception
    {
        public CsvMemberCountMismatchException(string fileType, int expectedMemberCounts, int actualMemberCount)
            : base($"File type '{fileType}' expects {expectedMemberCounts} CsvMembers but found {actualMemberCount}") { }
    }
}

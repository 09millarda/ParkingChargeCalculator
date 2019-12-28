using System;

namespace ParkingChargeCalculator.Exceptions
{
    public class FileParseErrorForCsvMemberException : Exception
    {
        public FileParseErrorForCsvMemberException(int memberIndex, Type memberType, string memberValue, Exception innerException)
            : base($"Error parsing Member with Index '{memberIndex}'. Expected Type '{memberType}' but found value '{memberValue}'",
                  innerException)
        { }
    }
}

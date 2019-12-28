using System;

namespace ParkingChargeCalculator.Exceptions
{
    public class FileParseErrorOnLineException : Exception
    {
        public FileParseErrorOnLineException(int lineNumber, string filePath, Type fileType, Exception innerException)
            : base($"Error parsing file from path '{filePath}' to type '{fileType.Name}' on line {lineNumber}", innerException) { }
    }
}

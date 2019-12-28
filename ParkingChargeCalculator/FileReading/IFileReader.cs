using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingChargeCalculator.FileReading
{
    public interface IFileReader
    {
        Task<IEnumerable<string>> ReadAsync(string filePath);
    }
}

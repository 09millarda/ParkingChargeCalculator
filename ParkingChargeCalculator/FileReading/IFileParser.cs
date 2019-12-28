using ParkingChargeCalculator.Files;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingChargeCalculator.FileReading
{
    public interface IFileParser
    {
        Task<IEnumerable<TFileRecord>> ParseAsync<TFileRecord>(string filePath)
            where TFileRecord : class, IFileRecord, new();
    }
}
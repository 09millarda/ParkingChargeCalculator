using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ParkingChargeCalculator.FileReading
{
    public class FileReader : IFileReader
    {
        public async Task<IEnumerable<string>> ReadAsync(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException(null, filePath);
            using StreamReader reader = new StreamReader(filePath);

            var lines = new List<string>();

            var line = string.Empty;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null) lines.Add(line);

            return lines;
        }
    }
}

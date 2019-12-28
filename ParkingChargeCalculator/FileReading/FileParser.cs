using ParkingChargeCalculator.Exceptions;
using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkingChargeCalculator.FileReading
{
    public class FileParser : IFileParser
    {
        private readonly IFileReader _fileReader;

        public FileParser(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public async Task<IEnumerable<TFileRecord>> ParseAsync<TFileRecord>(string filePath)
            where TFileRecord : class, IFileRecord, new()
        {
            var csvRecords = (await _fileReader.ReadAsync(filePath).ConfigureAwait(false)).ToArray();

            var parsedRecords = new List<TFileRecord>();
            for (var lineNumber = 0; lineNumber < csvRecords.Count(); lineNumber++)
            {
                var csvRecord = csvRecords[lineNumber];
                try
                {
                    var parsedRecord = ParseLine<TFileRecord>(csvRecord);
                    parsedRecord.Validate();
                    parsedRecords.Add(parsedRecord);
                }
                catch (Exception exception)
                {
                    throw new FileParseErrorOnLineException(lineNumber, filePath, typeof(TFileRecord), exception);
                }
            }

            return parsedRecords;
        }

        private static TFileRecord ParseLine<TFileRecord>(string csvRecord) where TFileRecord : class, new()
        {
            var parsedRecord = new TFileRecord();

            var csvRecordSplit = csvRecord.Split(",");
            var csvProperties = typeof(TFileRecord)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Attribute.IsDefined(p, typeof(CsvMemberAttribute)));

            if (csvRecordSplit.Length != csvProperties.Count()) throw new CsvMemberCountMismatchException(typeof(TFileRecord).Name, csvProperties.Count(), csvRecordSplit.Length);

            foreach (var csvProperty in csvProperties)
            {
                var csvMemberIndex = GetCsvMemberIndex(csvProperty);

                try
                {
                    csvProperty.SetProperty(csvRecordSplit[csvMemberIndex], parsedRecord);
                }
                catch (Exception exception)
                {
                    throw new FileParseErrorForCsvMemberException(csvMemberIndex, csvProperty.PropertyType, csvRecordSplit[csvMemberIndex], exception);
                }
            }

            return parsedRecord;
        }

        private static int GetCsvMemberIndex(PropertyInfo property)
        {
            var csvMemberAttribute = (CsvMemberAttribute)property.GetCustomAttributes(typeof(CsvMemberAttribute), true)[0];
            return csvMemberAttribute.MemberIndex;
        }
    }
}

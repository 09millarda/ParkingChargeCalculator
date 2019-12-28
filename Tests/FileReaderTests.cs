using FluentAssertions;
using ParkingChargeCalculator.FileReading;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class FileReaderTests : TestBase
    {
        public FileReader FileReader { get; set; }

        [Fact]
        public async Task FileReader_successfully_reads_from_valid_file_path()
        {
            Given_a_FileReader_exists();
            await When_a_file_with_path_is_read("./TestFile.txt").ConfigureAwait(false);
            Then_the_read_result_equals(new List<string>
            {
                "The quick brown fox",
                "jumps over the lazy",
                "dog"
            });
        }

        [Fact]
        public async Task FileReader_throws_FileNotFoundException_when_given_invalid_file_path()
        {
            Given_a_FileReader_exists();
            await FileReader.Invoking(async r => await r.ReadAsync("INVALID_PATH"))
                .Should()
                .ThrowAsync<FileNotFoundException>();
        }

        private void Then_the_read_result_equals(IEnumerable<string> readResult)
        {
            Context.Get<IEnumerable<string>>("FileReadResult")
                .Should()
                .BeEquivalentTo(readResult);
        }

        private void Given_a_FileReader_exists()
        {
            FileReader = new FileReader();
        }

        private async Task When_a_file_with_path_is_read(string filePath)
        {
            var result = await FileReader.ReadAsync(filePath).ConfigureAwait(false);
            GetCreate(() => result, "FileReadResult");
        }
    }
}

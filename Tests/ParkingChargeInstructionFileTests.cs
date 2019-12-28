using FluentAssertions;
using Moq;
using ParkingChargeCalculator.Exceptions;
using ParkingChargeCalculator.FileReading;
using ParkingChargeCalculator.Files;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ParkingChargeInstructionFileTests : TestBase
    {
        public IFileParser FileParser { get; set; }

        [Fact]
        public async Task The_FileParser_parses_a_valid_ParkingChargeInstruction_file()
        {
            var fileContents = new List<string>
            {
                "0,2019-01-01T00:00:00,2019-01-01T12:00:00",
                "1,2019-01-01T01:00:00,2019-01-01T13:00:00",
                "ShortStay,2019-01-01T02:00:00,2019-01-01T14:00:00",
                "LongStay,2019-01-01T03:00:00,2019-01-01T15:00:00",
            };

            Given_the_FileParser_reads_file_with_contents(fileContents);
            await When_the_file_is_parsed().ConfigureAwait(false);

            var expectedResults = new List<ParkingChargeInstructionFile>()
            {
                new ParkingChargeInstructionFile
                {
                    ChargeType = ChargeType.ShortStay,
                    ChargeStart = new DateTime(2019, 01, 01, 00, 00, 00),
                    ChargeEnd = new DateTime(2019, 01, 01, 12, 00, 00)
                },
                new ParkingChargeInstructionFile
                {
                    ChargeType = ChargeType.LongStay,
                    ChargeStart = new DateTime(2019, 01, 01, 1, 0, 0),
                    ChargeEnd = new DateTime(2019, 01, 01, 13, 0, 0)
                },
                new ParkingChargeInstructionFile
                {
                    ChargeType = ChargeType.ShortStay,
                    ChargeStart = new DateTime(2019, 01, 01, 2, 0, 0),
                    ChargeEnd = new DateTime(2019, 01, 01, 14, 0, 0)
                },
                new ParkingChargeInstructionFile
                {
                    ChargeType = ChargeType.LongStay,
                    ChargeStart = new DateTime(2019, 01, 01, 3, 0, 0),
                    ChargeEnd = new DateTime(2019, 01, 01, 15, 0, 0)
                }
            };

            Then_the_parsed_instructions_equal(expectedResults);
        }

        [Theory]
        [InlineData("0,2019-01-01T00:00:00,2019-abc-01T12:00:00")] // Invalid ChargeEnd DateTime
        [InlineData("0,2019-abc-01T00:00:00,2019-01-01T12:00:00")] // Invalid ChargeStart DateTime
        [InlineData("INVALID,2019-01-01T00:00:00,2019-01-01T12:00:00")] // Invalid ChargeType DateTime
        [InlineData("0,2019-01-02T00:00:00,2019-01-01T12:00:00")] // ChargeEnd is before ChargeStart
        public async Task The_FileParser_throws_FileParseErrorOnLineException_when_parsing_invalid_ParkingChargeInstruction_file(string invalidFileRecord)
        {
            Given_the_FileParser_reads_file_with_contents(new List<string>
            {
                invalidFileRecord
            });
            await FileParser.Invoking(async p => await p.ParseAsync<ParkingChargeInstructionFile>(string.Empty))
                .Should()
                .ThrowAsync<FileParseErrorOnLineException>();
        }

        private void Then_the_parsed_instructions_equal(IEnumerable<ParkingChargeInstructionFile> expectedResults)
        {
            var actualResults = Context.Get<IEnumerable<ParkingChargeInstructionFile>>(ContextKeys.ParsedParkingChargeInstructionsKey);

            actualResults.Should().BeEquivalentTo(expectedResults);
        }

        private async Task When_the_file_is_parsed()
        {
            var parsedFile = await FileParser.ParseAsync<ParkingChargeInstructionFile>(string.Empty).ConfigureAwait(false);
            Context.Set(parsedFile, ContextKeys.ParsedParkingChargeInstructionsKey);
        }

        private void Given_the_FileParser_reads_file_with_contents(IEnumerable<string> fileContents)
        {
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(r => r.ReadAsync(It.IsAny<string>())).ReturnsAsync(fileContents);

            FileParser = new FileParser(fileReaderMock.Object);
        }

        class ContextKeys
        {
            public const string ParsedParkingChargeInstructionsKey = "ParsedParkingChargeInstructions";
        }
    }
}
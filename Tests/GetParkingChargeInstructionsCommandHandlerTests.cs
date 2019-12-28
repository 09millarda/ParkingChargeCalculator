using FluentAssertions;
using Moq;
using ParkingChargeCalculator.FileReading;
using ParkingChargeCalculator.Files;
using ParkingChargeCalculator.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class GetParkingChargeInstructionsCommandHandlerTests
    {
        [Fact]
        public async Task GetParkingChargeInstructionsCommandHandler_executed_with_valid_command_returns_expected_instruction_list()
        {
            var fileParserMock = new Mock<IFileParser>();
            var commandValidatorMock = new Mock<ICommandValidator<GetParkingChargeInstructionsCommand>>();

            fileParserMock.Setup(p => p.ParseAsync<ParkingChargeInstructionFile>(It.IsAny<string>())).ReturnsAsync(new List<ParkingChargeInstructionFile>
            {
                new ParkingChargeInstructionFile
                {
                    ChargeEnd = new DateTime(2019, 1, 2),
                    ChargeStart = new DateTime(2019, 1, 1),
                    ChargeType = ChargeType.LongStay
                },
                new ParkingChargeInstructionFile
                {
                    ChargeEnd = new DateTime(2019, 2, 2),
                    ChargeStart = new DateTime(2019, 2, 1),
                    ChargeType = ChargeType.ShortStay
                }
            });

            var commandHandler = new GetParkingChargeInstructionsCommandHandler(fileParserMock.Object, commandValidatorMock.Object);

            var actualInstructions = (await commandHandler.ExecuteAsync(new GetParkingChargeInstructionsCommand { FilePath = "" })).ToList();
            var expectedInstructions = new List<ParkingChargeInstruction>()
            {
                new ParkingChargeInstruction
                {
                    ChargeEnd = new DateTime(2019, 1, 2),
                    ChargeStart = new DateTime(2019, 1, 1),
                    ChargeType = ChargeType.LongStay
                },
                new ParkingChargeInstruction
                {
                    ChargeEnd = new DateTime(2019, 2, 2),
                    ChargeStart = new DateTime(2019, 2, 1),
                    ChargeType = ChargeType.ShortStay
                }
            };
            actualInstructions.Should().BeEquivalentTo(expectedInstructions);
            commandValidatorMock.Invocations.Count.Should().Be(1);
        }

        [Fact]
        public void GetParkingChargeCommandInstructionCommandValidator_throws_FileNotFoundException_with_invalid_FilePath()
        {
            var invalidCommand = new GetParkingChargeInstructionsCommand { FilePath = "INVALID_PATH" };
            var commandValidator = new GetParkingChargeInstructionsCommand.Validator();

            commandValidator.Invoking(v => v.Validate(invalidCommand)).Should().Throw<FileNotFoundException>();
        }

        [Fact]
        public void GetParkingChargeCommandInstructionCommandValidator_does_not_throw_when_command_is_valid()
        {
            var validCommand = new GetParkingChargeInstructionsCommand { FilePath = "./TestFile.txt" };
            var commandValidator = new GetParkingChargeInstructionsCommand.Validator();

            commandValidator.Invoking(v => v.Validate(validCommand)).Should().NotThrow();
        }
    }
}

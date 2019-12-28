using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingChargeCalculator.Executors;
using ParkingChargeCalculator.FileReading;
using ParkingChargeCalculator.Handlers;
using ParkingChargeCalculator.ParkingChargeCalculators;
using System;
using System.Threading.Tasks;

namespace ParkingChargeCalculator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var executor = serviceProvider.GetService<IExecutor>();
            await executor.Execute().ConfigureAwait(false);

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices()
        {
            var collection = new ServiceCollection();

            // Configure Logging
            collection.AddSingleton<ILoggerFactory, LoggerFactory>()
                      .AddLogging(loggingBuilder =>
                      {
                          loggingBuilder.AddConsole()
                                        .SetMinimumLevel(LogLevel.Information);
                      });

            collection
                .AddTransient<IFileParser, FileParser>()
                .AddTransient<IExecutor, CalculateParkingChargesExecutor>()
                .AddTransient<IFileReader, FileReader>()
                .AddTransient<IParkingChargeCalculatorFactory, ParkingChargeCalculatorFactory>()
                .AddTransient<IGetParkingChargeInstructionsCommandHandler, GetParkingChargeInstructionsCommandHandler>()
                .AddTransient<ICommandValidator<GetParkingChargeInstructionsCommand>, GetParkingChargeInstructionsCommand.Validator>()
                .AddTransient<ShortStayParkingChargeCalculator>()
                .AddTransient<LongStayParkingChargeCalculator>();

            return collection.BuildServiceProvider();
        }
    }
}

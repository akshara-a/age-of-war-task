using AgeOfWar.Contracts;
using AgeOfWar.Entities;
using AgeOfWar.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgeOfWar
{
    class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// Sets up service provider, configures logging, and runs the battle strategy calculator.
        /// </summary>
        static void Main()
        {
            ServiceProvider serviceProvider = ConfigureServices();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Application started.");

            try
            {
                IDataParserService parser = serviceProvider.GetRequiredService<IDataParserService>();
                IBattleStrategyService strategyService = serviceProvider.GetRequiredService<IBattleStrategyService>();
                IBattleReportService reportService = serviceProvider.GetRequiredService<IBattleReportService>();
                IBattleRuleService ruleService = serviceProvider.GetRequiredService<IBattleRuleService>();

                Console.WriteLine("Age of War - Battle Strategy Calculator");
                Console.WriteLine("=======================================");

                Console.WriteLine("Enter your platoons (format: UnitClass#Count;UnitClass#Count;...):");
                string? playerInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(playerInput))
                {
                    throw new ArgumentException("Enemy input was null or empty.");
                }

                Console.WriteLine("Enter enemy platoons (format: UnitClass#Count;UnitClass#Count;...):");
                string? enemyInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(enemyInput))
                {
                    throw new ArgumentException("Enemy input was null or empty.");
                }

                List<Platoon> players = parser.ParseData(playerInput);
                List<Platoon> enemies = parser.ParseData(enemyInput);
                logger.LogInformation("Parsed player and enemy platoons successfully.");

                List<Platoon>? winningArrangement = strategyService.FindWinningArrangement(players, enemies);

                if (winningArrangement == null)
                {
                    Console.WriteLine("There is no winning arrangement.");
                    logger.LogInformation("No winning arrangement found.");
                }
                else
                {
                    Console.WriteLine("\nWinning arrangement:");
                    Console.WriteLine(string.Join(";", winningArrangement.Select(p => p.ToString())));
                    reportService.DisplayBattleReport(winningArrangement, enemies, ruleService);
                    logger.LogInformation("Displayed battle report.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred during execution.");
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                logger.LogInformation("Application terminated.");
            }
        }

        /// <summary>
        /// Configures the service collection and returns a built service provider.
        /// </summary>
        /// <returns>A ServiceProvider configured with all required services and logging.</returns>
        static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(config =>
            {
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Information);
            });

            services.AddSingleton<IBattleRuleService, BattleRuleService>();
            services.AddSingleton<IBattleStrategyService, BattleStrategyService>();
            services.AddSingleton<IDataParserService, DataParserService>();
            services.AddSingleton<IBattleReportService, BattleReportService>();

            return services.BuildServiceProvider();
        }
    }
}
using AgeOfWar.Contracts;
using AgeOfWar.Entities;
using AgeOfWar.Enums;

namespace AgeOfWar.Services
{
    public class BattleReportService : IBattleReportService
    {
        /// <summary>
        /// Displays the battle report for a given arrangement of platoons against an enemy's platoons.
        /// </summary>
        /// <param name="players">The arrangement of platoons for the player.</param>
        /// <param name="enemies">The platoons of the enemy.</param>
        /// <param name="battleRuleService">The service used to calculate the outcome of battles between platoons.</param>
        public void DisplayBattleReport(List<Platoon> players, List<Platoon> enemies, IBattleRuleService battleRuleService)
        {
            Console.WriteLine("\nBattle Report:");
            Console.WriteLine("Own Platoon\t\tOpponent Platoon\tOutcome");
            Console.WriteLine("==================================================");

            int wins = 0;
            for (int i = 0; i < 5; i++)
            {
                ResultEnum result = battleRuleService.CalculateBattleResult(players[i], enemies[i]);
                if (result == ResultEnum.Win) wins++;
                Console.WriteLine($"Battle {i + 1}: {players[i]}\t{enemies[i]}\t{result}");
            }

            Console.WriteLine($"\nResult: {wins}/5 battles won");
        }
    }
}
using AgeOfWar.Contracts;
using AgeOfWar.Entities;
using AgeOfWar.Enums;

namespace AgeOfWar.Services
{
    public class BattleStrategyService : IBattleStrategyService
    {
        private readonly IBattleRuleService _battleRuleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleStrategyService"/> class.
        /// </summary>
        /// <param name="battleRuleService">The service used to determine the outcome of battles between platoons.</param>
        public BattleStrategyService(IBattleRuleService battleRuleService)
        {
            _battleRuleService = battleRuleService;
        }

        /// <summary>
        /// Finds the winning arrangement of the player's platoons that results in at least 3 wins against the enemy's platoons.
        /// </summary>
        /// <param name="players">The player's platoons.</param>
        /// <param name="enemies">The enemy's platoons.</param>
        /// /// <returns>The winning arrangement of platoons, or null if no winning arrangement is found.</returns>
        public List<Platoon>? FindWinningArrangement(List<Platoon> players, List<Platoon> enemies)
        {
            if (players.Count != 5 || enemies.Count != 5)
                throw new ArgumentException("Both armies must have exactly 5 platoons");

            List<List<Platoon>> allPermutations = GeneratePermutations(players);

            List<Platoon>? bestArrangement = null;
            int maxWins = 0;

            foreach (List<Platoon> arrangement in allPermutations)
            {
                int wins = 0;
                for (int i = 0; i < 5; i++)
                {
                    ResultEnum result = _battleRuleService.CalculateBattleResult(arrangement[i], enemies[i]);
                    if (result == ResultEnum.Win)
                        wins++;
                }

                if (wins > maxWins)
                {
                    maxWins = wins;
                    bestArrangement = arrangement;
                }

                if (wins == 5) 
                    break;
            }

            return bestArrangement;

        }

        /// <summary>
        /// Generates all possible permutations of the given list of platoons.
        /// </summary>
        /// <param name="list">The list of platoons to permute.</param>
        /// <returns>A list containing all permutations of the input platoon list.</returns>
        private List<List<Platoon>> GeneratePermutations(List<Platoon> list)
        {
            List<List<Platoon>> result = new List<List<Platoon>>();
            GenerateRecursive(list, 0, result);
            return result;
        }

        /// <summary>
        /// Generates all permutations of the list by recursively swapping elements and adding to the result list.
        /// </summary>
        /// <param name="list">The list to generate permutations of.</param>
        /// <param name="start">The starting index for the swap.</param>
        /// /// <param name="result">The list of generated permutations.</param>
        private void GenerateRecursive(List<Platoon> list, int start, List<List<Platoon>> result)
        {
            if (start == list.Count)
            {
                result.Add(list);
                return;
            }

            for (int i = start; i < list.Count; i++)
            {
                (list[start], list[i]) = (list[i], list[start]);
                GenerateRecursive(list, start + 1, result);
                (list[start], list[i]) = (list[i], list[start]);
            }
        }
    }
}
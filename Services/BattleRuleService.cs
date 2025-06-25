using AgeOfWar.Contracts;
using AgeOfWar.Entities;
using AgeOfWar.Enums;

namespace AgeOfWar.Services
{
    public class BattleRuleService : IBattleRuleService
    {
        private readonly Dictionary<UnitClassEnum, List<UnitClassEnum>> _advantages;

        /// <summary>
        /// Constructor for BattleRuleService.
        /// </summary>
        /// <remarks>
        /// This constructor sets up the advantages dictionary, which maps each unit class to a list of unit classes
        /// that it has an advantage over.
        /// </remarks>
        public BattleRuleService()
        {
            _advantages = new Dictionary<UnitClassEnum, List<UnitClassEnum>>
            {
                { UnitClassEnum.Militia, new List<UnitClassEnum> { UnitClassEnum.Spearmen, UnitClassEnum.LightCavalry } },
                { UnitClassEnum.Spearmen, new List<UnitClassEnum> { UnitClassEnum.LightCavalry, UnitClassEnum.HeavyCavalry } },
                { UnitClassEnum.LightCavalry, new List<UnitClassEnum> { UnitClassEnum.FootArcher, UnitClassEnum.CavalryArcher } },
                { UnitClassEnum.HeavyCavalry, new List<UnitClassEnum> { UnitClassEnum.Militia, UnitClassEnum.FootArcher, UnitClassEnum.LightCavalry } },
                { UnitClassEnum.CavalryArcher, new List<UnitClassEnum> { UnitClassEnum.Spearmen, UnitClassEnum.HeavyCavalry } },
                { UnitClassEnum.FootArcher, new List<UnitClassEnum> { UnitClassEnum.Militia, UnitClassEnum.CavalryArcher } }
            };
        }

        /// <summary>
        /// Checks if the attacker has an advantage over the defender.
        /// </summary>
        /// <param name="attacker">The unit class of the attacker.</param>
        /// <param name="defender">The unit class of the defender.</param>
        /// /// <returns>True if the attacker has an advantage over the defender, false otherwise.</returns>
        public bool HasAdvantage(UnitClassEnum attacker, UnitClassEnum defender)
        {
            return _advantages.TryGetValue(attacker, out List<UnitClassEnum>? list) && list.Contains(defender);
        }

        /// <summary>
        /// Calculates the battle result of an attacker against a defender.
        /// This is done by first setting the effective strength of the attacker and defender to their respective soldier counts.
        /// If the attacker has an advantage over the defender, then the defender's effective strength is halved.
        /// If the defender has an advantage over the attacker, then the attacker's effective strength is halved.
        /// The result of the battle is then determined by comparing the effective strengths of the attacker and defender.
        /// If the attacker's effective strength is higher, the result is a win. If they are equal, the result is a draw. Otherwise, the result is a loss.
        /// </summary>
        /// <param name="attacker">The attacker platoon.</param>
        /// <param name="defender">The defender platoon.</param>
        /// /// <returns>The result of the battle.</returns>
        public ResultEnum CalculateBattleResult(Platoon attacker, Platoon defender)
        {
            int effectiveAttackerStrength = attacker.SoldierCount;
            int effectiveDefenderStrength = defender.SoldierCount;

            if (HasAdvantage(attacker.UnitClass, defender.UnitClass))
                effectiveDefenderStrength /= 2;
            else if (HasAdvantage(defender.UnitClass, attacker.UnitClass))
                effectiveAttackerStrength /= 2;

            if (effectiveAttackerStrength > effectiveDefenderStrength) return ResultEnum.Win;
            if (effectiveAttackerStrength == effectiveDefenderStrength) return ResultEnum.Draw;
            return ResultEnum.Loss;
        }
    }
}
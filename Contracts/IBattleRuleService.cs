using AgeOfWar.Entities;
using AgeOfWar.Enums;

namespace AgeOfWar.Contracts;

/// <summary>
/// Defines the rules and logic for calculating the outcome of battles between platoons.
/// </summary>
public interface IBattleRuleService
{
    /// <summary>
    /// Calculates the result of a battle between an attacking and a defending platoon.
    /// </summary>
    /// <param name="attacker">The attacking platoon.</param>
    /// <param name="defender">The defending platoon.</param>
    /// <returns>
    /// A <see cref="ResultEnum"/> value indicating the outcome of the battle.
    /// </returns>
    ResultEnum CalculateBattleResult(Platoon attacker, Platoon defender);

    /// <summary>
    /// Determines whether the attacker has a class-based advantage over the defender.
    /// </summary>
    /// <param name="attacker">The unit class of the attacker.</param>
    /// <param name="defender">The unit class of the defender.</param>
    /// <returns>
    /// <c>true</c> if the attacker has an advantage; otherwise, <c>false</c>.
    /// </returns>
    bool HasAdvantage(UnitClassEnum attacker, UnitClassEnum defender);
}
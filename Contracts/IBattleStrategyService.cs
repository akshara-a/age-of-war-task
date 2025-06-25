using AgeOfWar.Entities;

namespace AgeOfWar.Contracts;

/// <summary>
/// Provides strategies for determining effective platoon arrangements in battle scenarios.
/// </summary>
public interface IBattleStrategyService
{
    /// <summary>
    /// Attempts to find an optimal arrangement of the player's platoons that increases the chance of winning against the enemy platoons.
    /// </summary>
    /// <param name="players">The list of the player's available platoons.</param>
    /// <param name="enemies">The list of the enemy's platoons to be countered.</param>
    /// <returns>
    /// A list representing the arranged player platoons that maximize the chance of victory,
    /// or <c>null</c> if no winning arrangement can be found.
    /// </returns>
    List<Platoon>? FindWinningArrangement(List<Platoon> players, List<Platoon> enemies);
}
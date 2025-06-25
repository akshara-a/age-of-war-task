using AgeOfWar.Entities;

namespace AgeOfWar.Contracts;

/// <summary>
/// Provides functionality to display the outcome of a battle between two platoon arrangements.
/// </summary>
public interface IBattleReportService
{
    /// <summary>
    /// Displays a battle report based on the provided platoon arrangements and battle rules.
    /// </summary>
    /// <param name="players">A list of the player's platoons participating in the battle.</param>
    /// <param name="enemies">A list of the enemy's platoons participating in the battle.</param>
    /// <param name="battleRuleService">The battle rule service used to resolve combat interactions.</param>
    void DisplayBattleReport(List<Platoon> players, List<Platoon> enemies, IBattleRuleService battleRuleService);
}
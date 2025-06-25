using AgeOfWar.Enums;

namespace AgeOfWar.Entities;

/// <summary>
/// Represents a platoon consisting of soldiers of a specific unit class and quantity.
/// </summary>
public class Platoon
{
    /// <summary>
    /// Gets the unit class of the soldiers in the platoon.
    /// </summary>
    public UnitClassEnum UnitClass { get; }

    /// <summary>
    /// Gets the number of soldiers in the platoon.
    /// </summary>
    public int SoldierCount { get; }

    /// <summary>
    /// Creates a new platoon with the given unit class and count of soldiers.
    /// </summary>
    /// <param name="unitClass">The unit class of the soldiers in the platoon.</param>
    /// <param name="soldierCount">The number of soldiers in the platoon.</param>
    public Platoon(UnitClassEnum unitClass, int soldierCount)
    {
        UnitClass = unitClass;
        SoldierCount = soldierCount;
    }

    /// <summary>
    /// Returns a string that represents the current platoon in the format "UnitClass#SoldierCount".
    /// </summary>
    /// <returns>A string representation of the platoon.</returns>
    public override string ToString() => $"{UnitClass}#{SoldierCount}";
}
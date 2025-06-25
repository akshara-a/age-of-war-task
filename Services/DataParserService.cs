using AgeOfWar.Common;
using AgeOfWar.Contracts;
using AgeOfWar.Entities;
using AgeOfWar.Enums;

namespace AgeOfWar.Services
{
    public class DataParserService : IDataParserService
    {
        /// <summary>
        /// Parses the given input string into a list of platoons.
        /// The input string should be a semicolon-separated list of platoons, where each platoon is represented by the unit class name followed by a hash mark and the number of soldiers in the platoon.
        /// For example, "Militia#10;Spearmen#5" would be parsed into two platoons, one with 10 Militia and one with 5 Spearmen.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>A list of platoons parsed from the input string.</returns>
        public List<Platoon> ParseData(string input)
        {
            return input.Split(Constants.SEMI_COLON).Select(str =>
            {
                string[]? parts = str.Split(Constants.HASH_TAG);
                if (parts.Length != 2 || !Enum.TryParse(parts[0], out UnitClassEnum unitClass) || !int.TryParse(parts[1], out int count))
                    throw new ArgumentException($"Invalid platoon format: {str}");
                return new Platoon(unitClass, count);
            }).ToList();
        }
    }
}
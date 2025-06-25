using AgeOfWar.Entities;

namespace AgeOfWar.Contracts
{
    /// <summary>
    /// Defines a service responsible for parsing input data into a list of platoons.
    /// </summary>
    public interface IDataParserService
    {
        /// <summary>
        /// Parses the provided input string and converts it into a list of <see cref="Platoon"/> objects.
        /// </summary>
        /// <param name="input">The raw input data to be parsed, typically in a structured format such as JSON, XML, or CSV.</param>
        /// <returns>
        /// A list of <see cref="Platoon"/> instances parsed from the input data.
        /// </returns>
        List<Platoon> ParseData(string input);
    }
}
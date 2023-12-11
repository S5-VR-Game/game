using System.Collections.Generic;
using System.Linq;

namespace Game.Tasks.BookRiddle
{
    /// <summary>
    /// This class represents the Solution for a Single Book riddle
    /// </summary>
    public record BookRiddleSolution
    {
        private readonly Dictionary<string, int> _solutionMap = new();

        /// <summary>
        /// Getter-Method for the Map with the solutions
        /// </summary>
        /// <returns>The Map itself</returns>
        public Dictionary<string, int> GetSolutionMap()
        {
            return _solutionMap;
        }

        public override string ToString()
        {
            var keys = GetSolutionMap().Keys;
            var erg = keys
                .Aggregate(
                    "Solution: ", 
                    (current, key) => current + ("Material: " + key + ", Digit: " + GetSolutionMap()[key] + ";"));
            return erg[..^1];
        }
    }
}
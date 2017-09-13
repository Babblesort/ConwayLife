using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Domain
{
    public class LifeRules
    {
        public static readonly int MinNeighborCount = 0;
        public static readonly int MaxNeighborCount = 8;

        public List<int> SurvivalNeighborCounts { get; private set; }
        public List<int> BirthNeighborCounts { get; private set; }

        public LifeRules()
        {
            SurvivalNeighborCounts = new List<int> { 2, 3 };
            BirthNeighborCounts = new List<int> { 3 };
        }

        public LifeRules(List<int> surviveCounts, List<int> birthCounts)
        {
            if (surviveCounts == null)
            {
                throw new ArgumentNullException(nameof(surviveCounts), "List cannot be null");
            }

            if (birthCounts == null)
            {
                throw new ArgumentNullException(nameof(birthCounts), "List cannot be null");
            }

            if (InvalidNeighborCount(surviveCounts))
            {
                throw new ArgumentOutOfRangeException(nameof(surviveCounts),
                    $"surviveCounts list must have at least one member and each number must be between {MinNeighborCount} and {MaxNeighborCount} inclusive.");
            }

            if (InvalidNeighborCount(birthCounts))
            {
                throw new ArgumentOutOfRangeException(nameof(birthCounts),
                    $"birthCounts list must have at least one member and each number must be between {MinNeighborCount} and {MaxNeighborCount} inclusive.");
            }

            SurvivalNeighborCounts = new List<int>(surviveCounts);
            BirthNeighborCounts = new List<int>(birthCounts);
        }

        private static bool InvalidNeighborCount(IList<int> list)
        {
            return !list.Any() || list.Any(i => i > MaxNeighborCount || i < MinNeighborCount);
        }

    }

}

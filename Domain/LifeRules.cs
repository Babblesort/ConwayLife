using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Domain
{
    public class LifeRules
    {
        public static readonly int MinNeighborCount = 0;
        public static readonly int MaxNeighborCount = 8;

        public List<int> SurvivalNeighborCounts { get; }
        public List<int> BirthNeighborCounts { get; }

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
                    $"Must have at least one number and each number must be between {MinNeighborCount} and {MaxNeighborCount} inclusive.");
            }

            if (InvalidNeighborCount(birthCounts))
            {
                throw new ArgumentOutOfRangeException(nameof(birthCounts),
                    $"Must have at least one number and each number must be between {MinNeighborCount} and {MaxNeighborCount} inclusive.");
            }

            SurvivalNeighborCounts = surviveCounts;
            BirthNeighborCounts = birthCounts;
        }

        private static bool InvalidNeighborCount(List<int> list)
        {
            return !list.Any() || list.Any(i => i > MaxNeighborCount || i < MinNeighborCount);
        }

    }

}

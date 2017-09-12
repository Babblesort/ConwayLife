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
            SurvivalNeighborCounts = new List<int>() { 2, 3 };
            BirthNeighborCounts = new List<int>() { 3 };
        }

        public LifeRules(List<int> surviveCounts, List<int> birthCounts)
        {
            if (surviveCounts != null && birthCounts != null)
            {
                if (ValidRuleCounts(surviveCounts, birthCounts))
                {
                    SurvivalNeighborCounts = new List<int>(surviveCounts);
                    BirthNeighborCounts = new List<int>(birthCounts);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format("Neighbor counts for surviveCounts and birthCounts lists must be between {0} and {1}.",
                                        MinNeighborCount,
                                        MaxNeighborCount));
                }
            }
            else
            {
                throw new ArgumentNullException("surviveCounts and birthCounts lists cannot be null.");
            }
        }

        private bool ValidRuleCounts(List<int> surviveCounts, List<int> birthCounts)
        {
            if (surviveCounts.Where(c => InvalidNeighborCount(c)).Count() > 0 ||
                birthCounts.Where(c => InvalidNeighborCount(c)).Count() > 0)
            {
                return false;
            }

            return true;
        }

        private bool InvalidNeighborCount(int count)
        {
            return (count < MinNeighborCount || count > MaxNeighborCount);
        }

    }

}

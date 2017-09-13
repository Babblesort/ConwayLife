﻿using System;
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
                throw new ArgumentNullException("surviveCounts", "List cannot be null");
            }

            if (birthCounts == null)
            {
                throw new ArgumentNullException("birthCounts", "List cannot be null");
            }

            if (InvalidNeighborCount(surviveCounts))
            {
                throw new ArgumentOutOfRangeException("surviveCounts",
                    string.Format("surviveCounts list must have at least one member and each number must be between {0} and {1} inclusive.",
                                    MinNeighborCount, MaxNeighborCount));
            }

            if (InvalidNeighborCount(birthCounts))
            {
                throw new ArgumentOutOfRangeException("birthCounts",
                    string.Format("birthCounts list must have at least one member and each number must be between {0} and {1} inclusive.",
                                    MinNeighborCount, MaxNeighborCount));
            }

            SurvivalNeighborCounts = new List<int>(surviveCounts);
            BirthNeighborCounts = new List<int>(birthCounts);
        }

        private bool InvalidNeighborCount(IList<int> list)
        {
            return list.Count() == 0 || list.Any(i => i > MaxNeighborCount || i < MinNeighborCount);
        }

    }

}

﻿using System;

namespace ConwayLife.Domain
{
    public class GameRunOptions
    {
        public static readonly int MinGenerations = 1;
        public static readonly int MaxGenerations = 10000;
        private int _allowedGenerations = 200;

        public static readonly int MinDelayMilliseconds = 50;
        public static readonly int MaxDelayMilliseconds = 5000;
        private int _delayStep = 250;

        public bool HaltOnExtinction { get; set; } = true;

        public int AllowedGenerations
        {
            get
            {
                return _allowedGenerations;
            }
            set
            {
                if (value < MinGenerations)
                {
                    throw new ArgumentOutOfRangeException("AllowedGenerations",
                        String.Format("Must be greater or equal to {0}.", MinGenerations));
                }
                if (value > MaxGenerations)
                {
                    throw new ArgumentOutOfRangeException("AllowedGenerations",
                        String.Format("Must be less than or equal to {0}.", MaxGenerations));
                }

                _allowedGenerations = value;
            }
        }

        public int DelayStepMilliseconds
        {
            get
            {
                return _delayStep;
            }
            set
            {
                if (value < MinDelayMilliseconds)
                {
                    throw new ArgumentOutOfRangeException("DelayStepMilliseconds",
                        String.Format("Must be greater or equal to {0}.", MinDelayMilliseconds));
                }
                if (value > MaxDelayMilliseconds)
                {
                    throw new ArgumentOutOfRangeException("DelayStepMilliseconds",
                        String.Format("Must be less than or equal to {0}.", MaxDelayMilliseconds));
                }

                _delayStep = value;
            }
        }
    }
}

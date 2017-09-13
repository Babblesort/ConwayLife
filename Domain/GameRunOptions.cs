using System;

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
                if (value < MinGenerations || value > MaxGenerations)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Must be between {MinGenerations} and {MaxGenerations} inclusive.");
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
                if (value < MinDelayMilliseconds || value > MaxDelayMilliseconds)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Must be between {MinDelayMilliseconds} and {MaxDelayMilliseconds} inclusive.");
                }

                _delayStep = value;
            }
        }
    }
}

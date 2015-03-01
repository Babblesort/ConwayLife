using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLife.Domain
{
    public class GameRunOptions
    {
        public static int MinGenerations = 1;
        public static int MaxGenerations = 10000;
        int _allowedGenerations = 200;

        public static int MinDelayMilliseconds = 50;
        public static int MaxDelayMilliseconds = 5000;
        int _delayStep = 250;

        bool _haltOnExtinction = true;

        public int AllowedGenerations
        {
            get
            {
                return _allowedGenerations;
            }
            set
            {
                if (value >= MinGenerations && value <= MaxGenerations)
                {
                    _allowedGenerations = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        String.Format("AllowedGenerations must be between {0} and {1}.",
                                        MinGenerations,
                                        MaxGenerations));
                }
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
                if (value >= MinDelayMilliseconds && value <= MaxDelayMilliseconds)
                {
                    _delayStep = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        String.Format("DelayStep must be between {0} and {1}.",
                                        MinDelayMilliseconds,
                                        MaxDelayMilliseconds));
                }
            }
        }

        public bool HaltOnExtinction
        {
            get
            {
                return _haltOnExtinction;
            }
            set
            {
                _haltOnExtinction = value;
            }
        }


    }
}

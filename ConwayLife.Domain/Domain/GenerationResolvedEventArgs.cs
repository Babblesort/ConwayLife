using System;
using System.Collections.Generic;

namespace ConwayLife.Domain
{
    public class GenerationResolvedEventArgs : EventArgs
    {
        public List<bool> cellStates { get; set; }
        public int generation { get; set; }
    }
}

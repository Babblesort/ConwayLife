using System;
using System.Collections.Generic;

namespace ConwayLife.Domain
{
    public class GenerationResolvedEventArgs : EventArgs
    {
        public List<bool> CellStates { get; set; }
        public int Generation { get; set; }
    }
}

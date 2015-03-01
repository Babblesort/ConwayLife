using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLife.Domain
{
    public class GenerationResolvedEventArgs : EventArgs
    {
        public List<bool> cellStates { get; set; }
        public int generation { get; set; }
    }
}

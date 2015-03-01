using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLife.Domain
{
    public class PlayFieldSizeChangedEventArgs : EventArgs
    {
        public int Cols { get; set; }
        public int Rows { get; set; }
    }
}

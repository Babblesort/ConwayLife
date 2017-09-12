using System;

namespace ConwayLife.Domain
{
    public class PlayFieldSizeChangedEventArgs : EventArgs
    {
        public int Cols { get; set; }
        public int Rows { get; set; }
    }
}

using System;

namespace ConwayLife.Domain
{
    public class PlayField
    {
        public event EventHandler<PlayFieldSizeChangedEventArgs> PlayFieldSizeChanged;

        public static readonly int MinSize = 1;
        public static readonly int MaxSize = 150;
        private int _rows = MinSize;
        private int _cols = MinSize;

        public PlayField() {}

        public PlayField(int rowCount, int colCount)
        {
            Rows = rowCount;
            Cols = colCount;
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                if (value < MinSize || value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException($"Rows value must be between {MinSize} and {MaxSize} inclusive.");
                }

                _rows = value;
                OnPlayFieldSizeChanged(new PlayFieldSizeChangedEventArgs { Rows = _rows, Cols = Cols });
            }
        }

        public int Cols
        {
            get
            {
                return _cols;
            }
            set
            {
                if (value < MinSize || value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException($"Cols value must be between {MinSize} and {MaxSize} inclusive.");
                }

                _cols = value;
                OnPlayFieldSizeChanged(new PlayFieldSizeChangedEventArgs { Rows = Rows, Cols = _cols });
            }
        }

        public int TotalCellCount => Rows * Cols;

        protected virtual void OnPlayFieldSizeChanged(PlayFieldSizeChangedEventArgs e)
        {
            PlayFieldSizeChanged?.Invoke(this, e);
        }
    }
}

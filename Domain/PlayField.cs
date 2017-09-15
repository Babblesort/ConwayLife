using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Domain
{
    public class PlayField
    {
        public event EventHandler<PlayFieldSizeChangedEventArgs> PlayFieldSizeChanged;

        public static readonly int MinSize = 1;
        public static readonly int MaxSize = 150;
        private int _rows;
        private int _cols;

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

        public List<bool> FreshCells => Enumerable.Repeat(false, TotalCellCount).ToList();

        public List<bool> RandomCells 
        {
            get
            {
                var rnd = new Random();
                return FreshCells.Select(x => rnd.Next(0, 100) > 66).ToList();
            }
        }

        public int CellIndex(int row, int col)
        {
            if (row < 0) throw new ArgumentOutOfRangeException("row must be zero or greater");
            if (col < 0) throw new ArgumentOutOfRangeException("col must be zero or greater");

            var index = row * Cols + col;
            if(index > TotalCellCount - 1) throw new ArgumentOutOfRangeException("index is outside play field");

            return index;
        }

        public int CellIndex(RowColTuple rowCol) => CellIndex(rowCol.Row, rowCol.Col);

        public RowColTuple CellRowCol(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index must be zero or greater");
            if (index >= TotalCellCount) throw new ArgumentOutOfRangeException("index is outside play field");

            var r = (int) (index / Cols);
            var c = index % Cols;

            return new RowColTuple(r, c);
        }

        public int TopLeftNeighborIndex(int row, int col)
        {
            if (row < 1 || col < 1) return -1;
            return CellIndex(row - 1, col - 1);
        }

        public int TopNeighborIndex(int row, int col)
        {
            if (row < 1) return -1;
            return CellIndex(row - 1, col);
        }

        public int TopRightNeighborIndex(int row, int col)
        {
            if (row < 1 || col >= Cols - 1) return -1;
            return CellIndex(row - 1, col + 1);
        }

        public int LeftNeighborIndex(int row, int col)
        {
            if (col < 1) return -1;
            return CellIndex(row, col - 1);
        }

        public int RightNeighborIndex(int row, int col)
        {
            if (col >= Cols - 1) return -1;
            return CellIndex(row, col + 1);
        }

        public int BottomLeftNeighborIndex(int row, int col)
        {
            if (row >= Rows - 1 || col < 1) return -1;
            return CellIndex(row + 1, col - 1);
        }

        public int BottomNeighborIndex(int row, int col)
        {
            if (row >= Rows - 1) return -1;
            return CellIndex(row + 1, col);
        }

        public int BottomRightNeighborIndex(int row, int col)
        {
            if (row >= Rows - 1 || col >= Cols - 1) return -1;
            return CellIndex(row + 1, col + 1);
        }

        protected virtual void OnPlayFieldSizeChanged(PlayFieldSizeChangedEventArgs e)
        {
            PlayFieldSizeChanged?.Invoke(this, e);
        }
    }
}

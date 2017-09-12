using System;

namespace ConwayLife.Domain
{
    public class PlayField
    {
        public event EventHandler<PlayFieldSizeChangedEventArgs> PlayFieldSizeChanged;
    
        public static readonly int MinSize = 1;
        public static readonly int MaxSize = 150;

        int _rows = MinSize;
        int _cols = MinSize;

        public PlayField() { }

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
                if (value >= MinSize && value <= MaxSize)
                {
                    _rows = value;
                    OnPlayFieldSizeChanged(new PlayFieldSizeChangedEventArgs { Rows = _rows, Cols = Cols });
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format("Rows value must be between {0} and {1}.", 
                                        MinSize, MaxSize));
                }
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
                if (value >= MinSize && value <= MaxSize)
                {
                    _cols = value;
                    var sizeChangedArgs = new PlayFieldSizeChangedEventArgs();
                    sizeChangedArgs.Rows = this.Rows;
                    sizeChangedArgs.Cols = _cols;
                    OnPlayFieldSizeChanged(sizeChangedArgs);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format("Cols value must be between {0} and {1}.",
                                        MinSize, MaxSize));
                }
            }
        }

        public int TotalCellCount
        {
            get
            {
                return Rows * Cols;
            }
        }

        protected virtual void OnPlayFieldSizeChanged(PlayFieldSizeChangedEventArgs e)
        {
            PlayFieldSizeChanged?.Invoke(this, e);
        }

    }
}

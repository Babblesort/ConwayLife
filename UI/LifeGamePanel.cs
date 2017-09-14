using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ConwayLife.UI
{
    public sealed partial class LifeGamePanel : Panel
    {
        private static readonly Pen GridLinePen = Pens.LightGray;
        private static readonly Brush CellBrush = new SolidBrush(Color.FromArgb(180, Color.ForestGreen));

        public List<bool> CellStates { get; set; }
        public int RowsCount { get; set; }
        public int ColsCount { get; set; }

        private float CellHeight => RowsCount > 0 ? (float)(Height - 5) / RowsCount : Height;
        private float CellWidth => ColsCount > 0 ? (float)(Width - 5) / ColsCount : Width;
        private int LiveCellsCount => CellStates.Select(c => c).Count();

        public LifeGamePanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        public void ClearBoard()
        {
            CellStates.Clear();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var cells = new RectangleF[LiveCellsCount];
            var cellIndex = 0;
            for (var r=0; r < RowsCount; r++)
            {
                e.Graphics.DrawLine(GridLinePen, 0, r * CellHeight, ColsCount * CellWidth, r * CellHeight);
                for (var c=0; c < ColsCount; c++)
                {
                    e.Graphics.DrawLine(GridLinePen, c * CellWidth, 0, c * CellWidth, RowsCount * CellHeight);
                    if (CellStates.Any() && CellStates[(r * ColsCount) + c])
                    {
                        cells[cellIndex++] = new RectangleF(c * CellWidth, r * CellHeight, CellWidth, CellHeight);
                    }
                }
            }
            if (cells.Length > 0)
            {
                e.Graphics.FillRectangles(CellBrush, cells);
            }
        }
    }
}

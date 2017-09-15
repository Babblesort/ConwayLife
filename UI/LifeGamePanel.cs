using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ConwayLife.Domain;

namespace ConwayLife.UI
{
    public partial class LifeGamePanel : Panel
    {
        public LifeGame Game { get; set; }
        private List<bool> CellStates => Game?.Cells ?? new List<bool>();
        private int RowsCount => Game?.Field.Rows ?? 0;
        private int ColsCount => Game?.Field.Cols ?? 0;
        private float CellHeight => RowsCount > 0 ? (float)(Height - 5) / RowsCount : Height;
        private float CellWidth => ColsCount > 0 ? (float)(Width - 5) / ColsCount : Width;
        private int LiveCellsCount => CellStates.Select(c => c).Count();
        private static readonly Pen GridLinePen = Pens.LightGray;
        private static readonly Brush CellBrush = new SolidBrush(Color.FromArgb(180, Color.ForestGreen));

        public LifeGamePanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
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

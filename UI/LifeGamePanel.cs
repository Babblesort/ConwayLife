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
            var cells = new RectangleF[CellStates.Select(c => c).Count()];
            var cellIndex = 0;
            for (var i=0; i < RowsCount; i++)
            {
                e.Graphics.DrawLine(GridLinePen, 0, i * CellHeight, ColsCount * CellWidth, i * CellHeight);
                for (var j=0; j < ColsCount; j++)
                {
                    e.Graphics.DrawLine(GridLinePen, j * CellWidth, 0, j * CellWidth, RowsCount * CellHeight);
                    if (CellStates.Any() && CellStates[(i * ColsCount) + j])
                    {
                        cells[cellIndex++] = new RectangleF(j * CellWidth, i * CellHeight, CellWidth, CellHeight);
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

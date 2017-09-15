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
        private int RowCount => Game.Field.Rows;
        private int ColCount => Game.Field.Cols;
        private float CellHeight => (float)(Height - 5) / RowCount;
        private float CellWidth => (float)(Width - 5) / ColCount;
        private int LiveCellsCount => Game.Cells.Select(c => c).Count();
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
            if (Game == null) return;

            for (var r=0; r < RowCount; r++)
            {
                for (var c=0; c < ColCount; c++)
                {
                    e.Graphics.DrawLine(GridLinePen, 0, r * CellHeight, ColCount * CellWidth, r * CellHeight);
                    e.Graphics.DrawLine(GridLinePen, c * CellWidth, 0, c * CellWidth, RowCount * CellHeight);
                }
            }

            var cells = Game.Cells
                .Select((c, index) => new { alive = c, index = index })
                .Where(c => c.alive)
                .Select(c => Game.Field.CellRowCol(c.index))
                .Select(rowCol => new RectangleF(rowCol.Col * CellWidth, rowCol.Row * CellHeight, CellWidth, CellHeight))
                .ToArray();

            e.Graphics.FillRectangles(CellBrush, cells);
        }
    }
}

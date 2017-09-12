using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ConwayLifeWinForms
{
    public partial class LifeGamePanel : Panel
    {
        static readonly Pen gridLinePen = Pens.LightGray;
        static readonly Pen cellBorderPen = Pens.DarkGreen;
        static readonly Brush cellBrush = new SolidBrush(Color.FromArgb(180, Color.ForestGreen));

        public List<bool> CellStates { get; set; }
        public int RowsCount { get; set; }
        public int ColsCount { get; set; }

        public LifeGamePanel()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        public void ClearBoard()
        {
            CellStates = new List<bool>();
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float cellWidth = CellWidthCurr();
            float cellHeight = CellHeightCurr();

            DrawGridLines(e.Graphics, cellWidth, cellHeight);
            DrawCells(e.Graphics, cellWidth, cellHeight);
        }

        private void DrawGridLines(Graphics graphicsContext, float cellWidth, float cellHeight)
        {
            for (int i = 0; i <= ColsCount; i++)
            {
                graphicsContext.DrawLine(gridLinePen, i * cellWidth, 0, i * cellWidth, RowsCount * cellHeight);
            }
            for (int i = 0; i <= RowsCount; i++)
            {
                graphicsContext.DrawLine(gridLinePen, 0, i * cellHeight, ColsCount * cellWidth, i * cellHeight);
            }
        }

        private void DrawCells(Graphics graphicsContext, float cellWidth, float cellHeight)
        {
            if ((CellStates != null) && (CellStates.Count > 0) && (CellStates.Count == RowsCount * ColsCount))
            {
                RectangleF[] cells = new RectangleF[CellStates.Where(c => c).Count()];
                int cellIndex = 0;
                for (var i = 0; i < RowsCount; i++)
                {
                    for (var j = 0; j < ColsCount; j++)
                    {
                        if (CellStates[(i * ColsCount) + j])
                        {
                            RectangleF cellBox = new RectangleF(j * cellWidth, i * cellHeight, cellWidth, cellHeight);
                            cells[cellIndex++] = cellBox;
                        }
                    }
                }
                if (cells.Length > 0)
                {
                    graphicsContext.FillRectangles(cellBrush, cells);
                }
            }
        }

        private float CellWidthCurr()
        {
            if (ColsCount > 0)
            {
                return (float)(this.Width - 5) / (float)ColsCount;
            }
            else
            {
                return this.Width;
            }
        }

        private float CellHeightCurr()
        {
            if (RowsCount > 0)
            {
                return (float)(this.Height - 5) / (float)RowsCount;
            }
            else
            {
                return this.Height;
            }
        }

    }
}

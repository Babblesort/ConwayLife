using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Domain
{
    public class LifeGame
    {
        public event EventHandler<GenerationResolvedEventArgs> GenerationResolvedHandler;

        public LifeRules Rules { get; }
        public PlayField Field { get; }
        public List<bool> Cells { get; private set; }
        public int Generation { get; private set; }
        public bool Extinction { get; private set; }

        public LifeGame(LifeRules lifeRules, PlayField playField)
        {
            if (lifeRules == null)
            {
                throw new ArgumentNullException(nameof(lifeRules), "Attempt to create LifeGame with null LifeRules");
            }

            if (playField == null)
            {
                throw new ArgumentNullException(nameof(playField), "Attempt to create LifeGame with null PlayField");
            }

            Rules = lifeRules;
            Field = playField;
            Extinction = false;
            Generation = 0;
        }

        public void ResolveNextGeneration()
        {
            Cells = IsGenerationZero ? Field.RandomCells : CalculateNextGen();
            Generation += 1;
            Extinction = !LiveCellsRemain;

            OnGenerationResolved(new GenerationResolvedEventArgs { CellStates = Cells, Generation = Generation });
        }

        private List<bool> CalculateNextGen()
        {
            var nextGenCells = Field.FreshCells;
            if (LiveCellsRemain)
            {
                for (var row = 0; row < Field.Rows; row++)
                {
                    for (var col = 0; col < Field.Cols; col++)
                    {
                        var i = Field.CellIndex(row, col);
                        var neighborCount = GetLivingNeighborsCount(row, col);
                        nextGenCells[i] = CellSurvives(Cells[i], neighborCount);
                    }
                }
            }
            return nextGenCells;
        }

        protected virtual void OnGenerationResolved(GenerationResolvedEventArgs e)
        {
            GenerationResolvedHandler?.Invoke(this, e);
        }

        private bool LiveCellsRemain => Cells.Any(c => c);
        private bool IsGenerationZero => Generation == 0;

        private int GetLivingNeighborsCount(int row, int col)
        {
            var count = 0;
            count += CellCountAtIndex(Field.TopLeftNeighborIndex(row, col));
            count += CellCountAtIndex(Field.TopNeighborIndex(row, col));
            count += CellCountAtIndex(Field.TopRightNeighborIndex(row, col));
            count += CellCountAtIndex(Field.LeftNeighborIndex(row, col));
            count += CellCountAtIndex(Field.RightNeighborIndex(row, col));
            count += CellCountAtIndex(Field.BottomLeftNeighborIndex(row, col));
            count += CellCountAtIndex(Field.BottomNeighborIndex(row, col));
            count += CellCountAtIndex(Field.BottomRightNeighborIndex(row, col));
            return count;
        }

        private int CellCountAtIndex(int index)
        {
            if (index < 0) return 0;
            return Cells[index] ? 1 : 0;
        }

        private bool CellSurvives(bool alive, int livingNeighborCount)
        {
            return (alive && (Rules.SurvivalNeighborCounts.Contains(livingNeighborCount))) ||
                   (!alive && (Rules.BirthNeighborCounts.Contains(livingNeighborCount)));
        }
    }
}

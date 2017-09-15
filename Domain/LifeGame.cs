using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Domain
{
    public class LifeGame
    {
        public event EventHandler<GenerationResolvedEventArgs> GenerationResolvedHandler;

        private readonly LifeRules _rules;
        private readonly PlayField _field;
        private List<bool> _cells;

        public int Generation { get; private set; }
        public bool Extinction { get; private set; }

        public LifeGame(LifeRules lifeRules, PlayField playField)
        {
            if (lifeRules == null)
            {
                throw new ArgumentNullException(nameof(lifeRules),  "Attempt to create LifeGame with null LifeRules");
            }

            if (playField == null)
            {
                throw new ArgumentNullException(nameof(playField), "Attempt to create LifeGame with null PlayField");
            }

            _rules = lifeRules;
            _field = playField;
            Extinction = false;
            Generation = 0;
        }

        public void ResolveNextGeneration()
        {
            _cells = IsGenerationZero ? _field.RandomCells : CalculateNextGen();
            Generation += 1;
            Extinction = !LiveCellsRemain;

            OnGenerationResolved(new GenerationResolvedEventArgs { CellStates = _cells, Generation = Generation });
        }

        private List<bool> CalculateNextGen()
        {
            var nextGenCells = _field.FreshCells;
            if (LiveCellsRemain)
            {
                for (var row = 0; row < _field.Rows; row++)
                {
                    for (var col = 0; col < _field.Cols; col++)
                    {
                        var i = _field.CellIndex(row, col);
                        var neighborCount = GetLivingNeighborsCount(row, col);
                        nextGenCells[i] = CellSurvives(_cells[i], neighborCount);
                    }
                }
            }
            return nextGenCells;
        }

        protected virtual void OnGenerationResolved(GenerationResolvedEventArgs e)
        {
            GenerationResolvedHandler?.Invoke(this, e);
        }
        
        private bool LiveCellsRemain => _cells.Any(c => c);
        private bool IsGenerationZero => Generation == 0;

        private int GetLivingNeighborsCount(int row, int col)
        {
            var count = 0;

            // Check the eight positions around current cell for neighbor alive status

            // 123
            // 4 5
            // 678
            
            // Top Left Neighbor
            if (row > 0 && col > 0)
            {
                count += IncrementIfAlive(_cells[_field.TopLeftNeighborIndex(row, col)]);
            }

            // Top Neighbor
            if (row > 0)
            {
                count += IncrementIfAlive(_cells[_field.TopNeighborIndex(row, col)]);
            }

            // Top Right Neighbor
            if (row > 0 && col < _field.Cols - 1)
            {
                count += IncrementIfAlive(_cells[_field.TopRightNeighborIndex(row, col)]);
            }

            // Left Neighbor
            if (col > 0)
            {
                count += IncrementIfAlive(_cells[_field.LeftNeighborIndex(row, col)]);
            }

            // Right Neighbor
            if (col < _field.Cols - 1)
            {
                count += IncrementIfAlive(_cells[_field.RightNeighborIndex(row, col)]);
            }

            // Bottom Left Neighbor
            if (row < _field.Rows - 1 && col > 0)
            {
                count += IncrementIfAlive(_cells[_field.BottomLeftNeighborIndex(row, col)]);
            }

            // Bottom Neighbor
            if (row < _field.Rows - 1)
            {
                count += IncrementIfAlive(_cells[_field.BottomNeighborIndex(row, col)]);
            }

            // Bottom Right Neighbor
            if (row < _field.Rows - 1 && col < _field.Cols - 1)
            {
                count += IncrementIfAlive(_cells[_field.BottomRightNeighborIndex(row, col)]);
            }

            return count;
        }

        private static int IncrementIfAlive(bool alive)
        {
            return alive ? 1 : 0;
        }

        private bool CellSurvives(bool alive, int livingNeighborCount)
        {
            return (alive && (_rules.SurvivalNeighborCounts.Contains(livingNeighborCount))) || 
                   (!alive && (_rules.BirthNeighborCounts.Contains(livingNeighborCount)));
        }
    }
}

using ConwayLife.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConwayLife.Domain
{
    public class LifeGame
    {
        public event EventHandler<GenerationResolvedEventArgs> GenerationResolved;

        LifeRules rules;
        PlayField field;

        public int Generation { get; private set; }
        public bool Extinction { get; private set; }

        List<bool> currentCellStates;
        List<bool> nextGenCellStates;


        public LifeGame(LifeRules lifeRules, PlayField playField)
        {
            if (lifeRules == null || playField == null)
            {
                throw new ArgumentNullException("Null argument in constructor.");
            }
            else
            {
                rules = lifeRules;
                field = playField;
                Extinction = false;
                Generation = 0;
            }
        }

        public void ResolveNextGeneration()
        {

            if (Generation == 0)
            {
                currentCellStates = GetRandomizedCells();
            }
            else
            {
                nextGenCellStates = new List<bool>(Enumerable.Repeat(false, currentCellStates.Count).ToList());
                if (LiveCellsRemain())
                {
                    int cellIndexCurr;
                    int livingNeighborCount;
                    for (int row = 0; row < field.Rows; row++)
                    {
                        for (int col = 0; col < field.Cols; col++)
                        {
                            cellIndexCurr = IndexOfRowCol(row, col, field.Cols);
                            livingNeighborCount = GetLivingNeighborsCount(row, col);
                            nextGenCellStates[cellIndexCurr] = CellSurvives(currentCellStates[cellIndexCurr], livingNeighborCount);
                        }
                    }
                }
                currentCellStates = nextGenCellStates;
            }
            Generation += 1;
            if (!LiveCellsRemain())
            {
                Extinction = true;
            }

            GenerationResolvedEventArgs resolvedArgs = new GenerationResolvedEventArgs();
            resolvedArgs.cellStates = currentCellStates;
            resolvedArgs.generation = Generation;
            OnGenerationResolved(resolvedArgs);
        }

        protected virtual void OnGenerationResolved(GenerationResolvedEventArgs e)
        {
            EventHandler<GenerationResolvedEventArgs> handler = GenerationResolved;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        private List<bool> GetRandomizedCells()
        {
            var randomLifeCells = new List<bool>();
            Random rnd = new Random();
            for (var i = 0; i < field.TotalCellCount; i++)
            {
                // About 1 in 3 init alive
                randomLifeCells.Add(rnd.Next(0, 100) > 66);
            }
            return randomLifeCells;
        }

        private bool LiveCellsRemain()
        {
            // Return true if at least one cell is living
            return currentCellStates.Where(c => c).Count() > 0;
        }

        private int IndexOfRowCol(int row, int col, int colsCount)
        {
            // Convert a given row/column into a linear index
            return (row * colsCount) + col;
        }

        private int GetLivingNeighborsCount(int row, int col)
        {
            int count = 0;

            // Check the eight positions around current cell for neighbor alive status

            // 123
            // 4 5
            // 678
            
            // Top Left Neighbor
            if (row > 0 && col > 0)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row - 1, col - 1, field.Cols)]);
            }

            // Top Neighbor
            if (row > 0)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row - 1, col, field.Cols)]);
            }

            // Top Right Neighbor
            if (row > 0 && col < field.Cols - 1)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row - 1, col + 1, field.Cols)]);
            }

            // Left Neighbor
            if (col > 0)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row, col - 1, field.Cols)]);
            }

            // Right Neighbor
            if (col < field.Cols - 1)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row, col + 1, field.Cols)]);
            }

            // Bottom Left Neighbor
            if (row < field.Rows - 1 && col > 0)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row + 1, col - 1, field.Cols)]);
            }

            // Bottom Neighbor
            if (row < field.Rows - 1)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row + 1, col, field.Cols)]);
            }

            // Bottom Right Neighbor
            if (row < field.Rows - 1 && col < field.Cols - 1)
            {
                count += IncrementIfAlive(currentCellStates[IndexOfRowCol(row + 1, col + 1, field.Cols)]);
            }

            return count;
        }

        private int IncrementIfAlive(bool alive)
        {
            return alive ? 1 : 0;
        }

        private bool CellSurvives(bool alive, int livingNeighborCount)
        {
            return (alive && (rules.SurvivalNeighborCounts.Contains(livingNeighborCount))) || 
                   (!alive && (rules.BirthNeighborCounts.Contains(livingNeighborCount)));
        }


    }
}

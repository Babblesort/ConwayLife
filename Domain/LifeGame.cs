﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Domain
{
    public class LifeGame
    {
        public event EventHandler<GenerationResolvedEventArgs> GenerationResolvedHandler;

        private readonly LifeRules _rules;
        private readonly PlayField _field;

        public int Generation { get; private set; }
        public bool Extinction { get; private set; }

        private List<bool> _currentCells;
        private List<bool> _nextCells;

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

            if (Generation == 0)
            {
                _currentCells = GetRandomizedCells();
            }
            else
            {
                _nextCells = new List<bool>(Enumerable.Repeat(false, _currentCells.Count).ToList());
                if (LiveCellsRemain())
                {
                    for (var row=0; row < _field.Rows; row++)
                    {
                        for (var col=0; col < _field.Cols; col++)
                        {
                            var i = _field.CellIndex(row, col);
                            var neighborCount = GetLivingNeighborsCount(row, col);
                            _nextCells[i] = CellSurvives(_currentCells[i], neighborCount);
                        }
                    }
                }
                _currentCells = _nextCells;
            }
            Generation += 1;
            if (!LiveCellsRemain())
            {
                Extinction = true;
            }

            OnGenerationResolved(new GenerationResolvedEventArgs { CellStates = _currentCells, Generation = Generation });
        }

        protected virtual void OnGenerationResolved(GenerationResolvedEventArgs e)
        {
            GenerationResolvedHandler?.Invoke(this, e);
        }
        
        private List<bool> GetRandomizedCells()
        {
            var rnd = new Random();
            return Enumerable.Range(1, _field.TotalCellCount)
                .Select(x => rnd.Next(0, 100) > 66)
                .ToList();
        }

        private bool LiveCellsRemain()
        {
            return _currentCells.Any(c => c);
        }

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
                count += IncrementIfAlive(_currentCells[_field.TopLeftNeighborIndex(row, col)]);
            }

            // Top Neighbor
            if (row > 0)
            {
                count += IncrementIfAlive(_currentCells[_field.TopNeighborIndex(row, col)]);
            }

            // Top Right Neighbor
            if (row > 0 && col < _field.Cols - 1)
            {
                count += IncrementIfAlive(_currentCells[_field.TopRightNeighborIndex(row, col)]);
            }

            // Left Neighbor
            if (col > 0)
            {
                count += IncrementIfAlive(_currentCells[_field.LeftNeighborIndex(row, col)]);
            }

            // Right Neighbor
            if (col < _field.Cols - 1)
            {
                count += IncrementIfAlive(_currentCells[_field.RightNeighborIndex(row, col)]);
            }

            // Bottom Left Neighbor
            if (row < _field.Rows - 1 && col > 0)
            {
                count += IncrementIfAlive(_currentCells[_field.BottomLeftNeighborIndex(row, col)]);
            }

            // Bottom Neighbor
            if (row < _field.Rows - 1)
            {
                count += IncrementIfAlive(_currentCells[_field.BottomNeighborIndex(row, col)]);
            }

            // Bottom Right Neighbor
            if (row < _field.Rows - 1 && col < _field.Cols - 1)
            {
                count += IncrementIfAlive(_currentCells[_field.BottomRightNeighborIndex(row, col)]);
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
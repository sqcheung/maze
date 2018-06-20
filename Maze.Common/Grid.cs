using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Common
{
    public class Grid
    {
        readonly GridCell[][] _grid;
        
        public int RowCount { get; }
        public int ColumnCount { get; }
        public int Size => RowCount * ColumnCount;
        
        public Grid(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            _grid = PrepareGrid(rowCount, columnCount);
            ConfigCells();
        }

        void ConfigCells()
        {
            foreach (GridCell[] rows in _grid)
            {
                foreach (GridCell cell in rows)
                {
                    var cellRow = cell.Row;
                    var cellColumn = cell.Column;
                    cell.SetNeighbors(
                        this[cellRow - 1, cellColumn],
                        this[cellRow + 1, cellColumn],
                        this[cellRow, cellColumn + 1],
                        this[cellRow, cellColumn - 1]);
                }
            }
        }

        static GridCell[][] PrepareGrid(int rows, int columns)
        {
            var theGrid = new GridCell[rows][];
            for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
            {
                var currentRow = new GridCell[columns];
                for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
                {
                    currentRow[columnIndex] = new GridCell(rowIndex, columnIndex);
                }

                theGrid[rowIndex] = currentRow;
            }

            return theGrid;
        }

        public GridCell this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex < 0 || rowIndex >= RowCount) return null;
                if (columnIndex < 0 || columnIndex >= ColumnCount) return null;
                return _grid[rowIndex][columnIndex];
            }
        }
        
        public GridCell GetRandomCell()
        {
            var random = new Random();
            int rowIndex = random.Next(RowCount);
            int columnIndex = random.Next(_grid[rowIndex].Length);
            return this[rowIndex, columnIndex];
        }

        public IEnumerable<GridCell[]> GetRows() => _grid.AsEnumerable();
        public IEnumerable<GridCell> GetCells() => _grid.SelectMany(row => row);
    }
}
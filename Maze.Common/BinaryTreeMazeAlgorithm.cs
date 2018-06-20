using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Common
{
    public class BinaryTreeMazeAlgorithm
    {
        public void Update(Grid grid)
        {
            var random = new Random();

            foreach (GridCell cell in grid.GetCells())
            {
                var neighbors = new List<GridCell>();
                if (cell.North != null) { neighbors.Add(cell.North);}
                if (cell.East != null) { neighbors.Add(cell.East); }

                if (!neighbors.Any()) continue;

                int selectedIndexToLink = random.Next(neighbors.Count);
                cell.Link(neighbors[selectedIndexToLink]);
            }
        }
    }
}
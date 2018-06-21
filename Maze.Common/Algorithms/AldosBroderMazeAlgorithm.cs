using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Common.Algorithms
{
    public class AldosBroderMazeAlgorithm : IMazeUpdater
    {
        public void Update(Grid grid)
        {
            var rand = new Random();
            GridCell cell = grid.GetRandomCell();
            int unvisited = grid.Size - 1;

            while (unvisited > 0)
            {
                IList<GridCell> neighbors = cell.Neighbors;
                GridCell neighbor = neighbors[rand.Next(neighbors.Count)];
                if (!neighbor.Links.Any())
                {
                    cell.Link(neighbor);
                    --unvisited;
                }

                cell = neighbor;
            }
        }
    }
}
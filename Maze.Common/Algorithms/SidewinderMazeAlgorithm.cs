using System;
using System.Collections.Generic;

namespace Maze.Common.Algorithms
{
    public class SidewinderMazeAlgorithm : IMazeUpdater
    {
        public void Update(Grid grid)
        {
            var rand = new Random();
            
            foreach (GridCell[] row in grid.GetRows())
            {
                var run = new List<GridCell>();
                foreach (GridCell cell in row)
                {
                    run.Add(cell);

                    bool atEasternBoundary = cell.East == null;
                    bool atNorthenBoundary = cell.North == null;

                    bool shouldCloseOut = atEasternBoundary || (!atNorthenBoundary && rand.Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        GridCell member = run[rand.Next(run.Count)];
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }
        }
    }
}
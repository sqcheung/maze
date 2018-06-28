using Maze.Common;
using Maze.Common.Algorithms;

namespace Maze.GameLevelGenerator
{
    public class MazeGridFactory
    {
        public MazeGridSettings Settings { get; }

        public MazeGridFactory(MazeGridSettings settings)
        {
            Settings = settings;
        }
        
        Grid Create()
        {
            var grid = new Grid(Settings.RowCount, Settings.ColumnCount);
            Settings.Algorithm.Update(grid);
            return grid;
        } 
        
        public RenderGrid CreateRenderGrid(bool solve = false)
        {
            Grid grid = Create();
            if (solve)
            {
                new DijkstraSolvingAlgorithm(0, 0).Update(grid);
            }
            
            return new RenderGrid(grid);
        }
    }
}
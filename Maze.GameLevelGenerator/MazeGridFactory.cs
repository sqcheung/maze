using Maze.Common;

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
        
        public RenderGrid CreateRenderGrid()
        {
            return new RenderGrid(Create());
        }
    }
}
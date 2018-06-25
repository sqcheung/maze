using Maze.Common;
using Maze.Common.Algorithms;

namespace Maze.GameLevelGenerator
{
    public class MazeGridSettings
    {
        public MazeGridSettings(int rowCount, int columnCount, IMazeUpdater algorithm = null)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            Algorithm = algorithm ?? new AldosBroderMazeAlgorithm();
        }

        public int RowCount { get; }
        public int ColumnCount { get; }
        public IMazeUpdater Algorithm { get; }
    }
}
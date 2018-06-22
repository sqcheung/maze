namespace Maze.GameLevelGenerator
{
    public class GameLevelRendererSettings
    {
        public GameLevelRendererSettings(int cellSize, int margin)
        {
            CellSize = cellSize;
            Margin = margin;
        }

        public int CellSize { get; }
        public int Margin { get; }
    }
}
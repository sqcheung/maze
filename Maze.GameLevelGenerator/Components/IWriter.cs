using System.IO;

namespace Maze.GameLevelGenerator.Components
{
    public interface IWriter
    {
        void Write(Stream stream, MazeGridSettings mazeSettings);
    }
}
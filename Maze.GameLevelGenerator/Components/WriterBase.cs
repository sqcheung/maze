using System.IO;
using Maze.Common;

namespace Maze.GameLevelGenerator.Components
{
    public abstract class WriterBase: IWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = CreateRenderer();
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }

        protected abstract GameLevelRenderer CreateRenderer();
    }
}
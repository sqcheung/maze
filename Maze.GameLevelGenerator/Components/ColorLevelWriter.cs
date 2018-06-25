using System.IO;
using Maze.Common;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings, GameLevelRenderSettings renderSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var factory = new ColorComponentFactory(renderSettings);
            var renderer = new NormalGameLevelRenderer(
                factory.CreateBackgroundRenderers(),
                factory.CreateGroundRenderers(),
                factory.CreateWallRenderers(),
                factory.CreateSettings());
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }
    }
}
using System.IO;
using Maze.Common;

namespace Maze.GameLevelGenerator.Components.NewDesign
{
    public class GrassLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var factory = new GrassComponentFactory();
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
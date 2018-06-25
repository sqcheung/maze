using System.IO;
using Maze.Common;

namespace Maze.GameLevelGenerator.Components.NewDesign
{
    public class CityLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var factory = new CityComponentFactory();
            var renderer = new Fake3DGameLevelRenderer(
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
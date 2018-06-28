using System.IO;
using Maze.Common;
using Maze.Common.Algorithms;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorSolvingLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid(true);
            
            var factory = new ColorSolvingLevelFactory();
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
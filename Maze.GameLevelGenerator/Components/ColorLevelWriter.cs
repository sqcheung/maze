using System.IO;
using System.Linq;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings, GameLevelRenderSettings renderSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = new NormalGameLevelRenderer(
                new [] {(AreaRenderer) new AreaColorRender(Rgba32.Black)},
                Enumerable.Empty<CellRenderer>(),
                new [] {(CellRenderer) new CellColorRender(Rgba32.LightSkyBlue)},
                renderSettings);
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }
    }
}
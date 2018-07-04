using System.IO;
using System.Linq;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorLevelWriter : WriterBase
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

        protected override GameLevelRenderer CreateRenderer()
        {
            return new NormalGameLevelRenderer(
                new [] {(AreaRenderer) new AreaColorRender(Rgba32.Black)},
                Enumerable.Empty<CellRenderer>(),
                new [] {(CellRenderer) new CellColorRender(Rgba32.LightSkyBlue)},
                new GameLevelRenderSettings(4, 10));
        }
    }
}
using System.Collections.Generic;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorSolvingLevelFactory : IGameLevelComponentFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new AreaColorRender(Rgba32.DarkGray);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new CellColorRender(Rgba32.Black);
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return new ColorGradientRenderer(
                "resolve_distance",
                0,
                500,
                Rgba32.White,
                Rgba32.DarkBlue);
        }

        public GameLevelRenderSettings CreateSettings()
        {
            return new GameLevelRenderSettings(4, 10);
        }
    }
}
using System.Collections.Generic;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorComponentFactory : IGameLevelComponentFactory
    {
        public Rgba32 BackgroundColor { get; set; } = Rgba32.Black;
        public Rgba32 WallColor { get; set; } = Rgba32.LightSkyBlue;

        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new AreaColorRender(BackgroundColor);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new CellColorRender(WallColor);
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield break;
        }

        public GameLevelRenderSettings CreateSettings()
        {
            return new GameLevelRenderSettings(4, 10);
        }
    }
}
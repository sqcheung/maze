using System.Collections.Generic;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components.NewDesign
{
    public class ColorComponentFactory : IGameLevelComponentFactory
    {
        readonly GameLevelRenderSettings _renderSettings;

        public ColorComponentFactory(GameLevelRenderSettings renderSettings = null)
        {
            _renderSettings = renderSettings ?? new GameLevelRenderSettings(4, 10);
        }

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
            return _renderSettings;
        }
    }
}
using System.Collections.Generic;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorMazeFactory : IGameLevelComponentFactory, IGameLevelRendererFactory
    {
        public Rgba32 BackgroundColor { get; set; } = Rgba32.Black;
        public Rgba32 WallColor { get; set; } = Rgba32.LightSkyBlue;
        public int CellSize { get; set; } = 5;


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

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(CellSize, 20);
        }

        public GameLevelRenderer CreateRenderer()
        {
            return new NormalGameLevelRenderer(this);
        }
    }
}
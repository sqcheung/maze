using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator
{
    public class GrassGameLevelComponentFactory : IGameLevelComponentFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            Image<Rgba32> resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png");
            yield return new AreaTileImageRenderer(resource, true);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new GrassWallRenderer();
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return new GrassGroundRenderer();
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(32, 20);
        }
    }
    
    public class FlowerBoxGameLevelComponentFactory : IGameLevelComponentFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new LovelyTreeBackgroundRenderer();
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new GrassGroundRenderer();
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return new LovelyTreeAccessoryRenderer();
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(31, 20);
        }
    }
    
    public class LovelyTreeGameLevelComponentFactory : IGameLevelComponentFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new LovelyTreeBackgroundRenderer();
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new LovelyRandomTreeCellRenderer();
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return new LovelyTreeAccessoryRenderer();
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(78, 20);
        }
    }
}
using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator
{
    public class LovelyTreeGameLevelComponentFactory : IGameLevelComponentFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            Image<Rgba32> resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.lovely_tree_ground.png");
            yield return new AreaTileImageRenderer(
                resource,
                true);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return CreateTreeCellRenderer();
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return CreateAccessoryRenderer();
        }

        static CellRenderer CreateAccessoryRenderer()
        {
            string[] resourceKeys =
            {
                "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_1.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_2.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_3.png"
            };
            
            return new RandomizedImageVisibilityCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(resourceKeys),
                true);
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(78, 20);
        }

        static CellRenderer CreateTreeCellRenderer()
        {
            string[] resourceKeys =
            {
                "Maze.GameLevelGenerator.Textures.lovely_tree_wall_1.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_wall_2.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_wall_3.png"
            };
            
            return new RandomizedImageCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(resourceKeys),
                true);
        }
    }
}
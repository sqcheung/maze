using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components.NewDesign
{
    public class TreeComponentFactory : IGameLevelComponentFactory
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

        public GameLevelRenderSettings CreateSettings()
        {
            return new GameLevelRenderSettings(78, 20);
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
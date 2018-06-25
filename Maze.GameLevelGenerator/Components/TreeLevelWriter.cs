using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class TreeLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = new NormalGameLevelRenderer(
                CreateBackgroundRenderers(),
                CreateGroundRenderers(),
                CreateWallRenderers(),
                new GameLevelRenderSettings(78, 20));
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }
        
        IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            Image<Rgba32> resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.lovely_tree_ground.png");
            yield return new AreaTileImageRenderer(
                resource,
                true);
        }

        IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return CreateTreeCellRenderer();
        }

        IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return CreateAccessoryRenderer();
        }

        CellRenderer CreateAccessoryRenderer()
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

        CellRenderer CreateTreeCellRenderer()
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
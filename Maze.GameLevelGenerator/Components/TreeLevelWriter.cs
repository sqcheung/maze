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
                CreateGrass(),
                CreateAccessories(),
                CreateTrees(),
                new GameLevelRenderSettings(78, 20));
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }
        
        IEnumerable<AreaRenderer> CreateGrass()
        {
            Image<Rgba32> resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.lovely_tree_ground.png");
            yield return new AreaTileImageRenderer(
                resource,
                true);
        }

        static IEnumerable<CellRenderer> CreateTrees()
        {
            string[] resourceKeys =
            {
                "Maze.GameLevelGenerator.Textures.lovely_tree_wall_1.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_wall_2.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_wall_3.png"
            };
            yield return new RandomizedImageCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(resourceKeys),
                true);
        }

        IEnumerable<CellRenderer> CreateAccessories()
        {
            string[] resourceKeys =
            {
                "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_1.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_2.png",
                "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_3.png"
            };
            yield return new RandomizedImageVisibilityCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(resourceKeys),
                true);
        }
    }
}
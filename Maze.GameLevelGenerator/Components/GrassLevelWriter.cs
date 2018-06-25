using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class GrassLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = new NormalGameLevelRenderer(
                CreateBackgroundRenderers(),
                CreateGroundRenderers(),
                CreateWallRenderers(),
                new GameLevelRenderSettings(32, 20));
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }

        static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

        IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            Image<Rgba32> resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png");
            yield return new AreaTileImageRenderer(resource, true);
        }

        IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return CreateGrassGroundRenderer();
            yield return CreateGrassDartRenderer();
        }
        
        static CellRenderer CreateGrassGroundRenderer()
        {
            string[] resourceKeys =
            {
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_2.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_3.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_4.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_5.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_6.png"
            };

            return new RandomizedImageCellRenderer(
                Assembly.LoadEmbeddedResources(resourceKeys),
                true);
        }
        
        static CellRenderer CreateGrassDartRenderer()
        {
            return new DirectedCellRenderer(
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east_west.png"));
        }

        static CellRenderer CreateGrassWallRenderer()
        {
            Image<Rgba32> east = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east.png");
            Image<Rgba32> eastWest = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east_west.png");
            Image<Rgba32> north = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north.png");
            Image<Rgba32> northEast = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_east.png");
            Image<Rgba32> northSouth = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_south.png");
            Image<Rgba32> northWest = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_west.png");
            Image<Rgba32> south = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south.png");
            Image<Rgba32> southEast = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east.png");
            Image<Rgba32> southEastWest = 
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east_west.png");
            Image<Rgba32> southWest =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_west.png");
            Image<Rgba32> west =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_west.png");
            return new DirectedCellRenderer(
                north,
                south,
                east,
                west,
                northSouth,
                northEast,
                northWest,
                southEast,
                southWest,
                eastWest,
                southEast,
                southWest,
                eastWest,
                southEastWest,
                southEastWest); 
        }
        
        IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return CreateGrassWallRenderer();
        }
    }
}
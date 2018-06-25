using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class GrassGameLevelFactory : IGameLevelComponentFactory, IGameLevelRendererFactory
    {
        static CellRenderer CreateGrassDartRenderer()
        {
            Assembly assembly = typeof(GrassGameLevelFactory).Assembly;
            
            return new DirectedCellRenderer(
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_east.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_east.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_east.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_east_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_east_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_east_west.png"),
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east_west.png"));
        }

        static CellRenderer CreateGrassWallRenderer()
        {
            Assembly assembly = typeof(GrassGameLevelFactory).Assembly;
            Image<Rgba32> east = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east.png");
            Image<Rgba32> eastWest = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east_west.png");
            Image<Rgba32> north = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north.png");
            Image<Rgba32> northEast = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_east.png");
            Image<Rgba32> northSouth = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_south.png");
            Image<Rgba32> northWest = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_west.png");
            Image<Rgba32> south = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south.png");
            Image<Rgba32> southEast = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east.png");
            Image<Rgba32> southEastWest = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east_west.png");
            Image<Rgba32> southWest =
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_west.png");
            Image<Rgba32> west =
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_west.png");
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

        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            Image<Rgba32> resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png");
            yield return new AreaTileImageRenderer(resource, true);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return CreateGrassWallRenderer();
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
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
                typeof(GrassGameLevelFactory).Assembly.LoadEmbeddedResources(resourceKeys),
                true);
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(32, 20);
        }

        public GameLevelRenderer CreateRenderer()
        {
            return new NormalGameLevelRenderer(this);
        }
    }
}
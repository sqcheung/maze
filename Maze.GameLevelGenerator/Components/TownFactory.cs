using System;
using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class TownFactory : IGameLevelComponentFactory, IGameLevelRendererFactory
    {
        static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new AreaColorRender(Rgba32.White);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new RandomizedImageCellRenderer(
                Assembly.LoadEmbeddedResources(
                    new []
                    {
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_1.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_2.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_3.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_4.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_5.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_6.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_7.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_8.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_9.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_10.png"
                    }),
                true);
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            Image<Rgba32> northSouth =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south.png");
            Image<Rgba32> eastWest =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_east_west.png");
            yield return new DirectedCellRenderer(
                northSouth,
                northSouth,
                eastWest,
                eastWest,
                northSouth,
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_south_west.png"),
                eastWest,
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_south_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_all.png"));
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            return Array.Empty<AreaRenderer>();
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(100, 50);
        }

        public GameLevelRenderer CreateRenderer()
        {
            return new Fake3DGameLevelRenderer(this);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class TownLevelWriter : IWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = new Fake3DGameLevelRenderer(
                new [] {new AreaColorRender(Rgba32.White)},
                CreateRoads(),
                CreateFake3DTownWall(),
                new GameLevelRenderSettings(100, 50));
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }
        
        static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        static IEnumerable<CellRenderer> CreateFake3DTownWall()
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

        static IEnumerable<CellRenderer> CreateRoads()
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
    }
}
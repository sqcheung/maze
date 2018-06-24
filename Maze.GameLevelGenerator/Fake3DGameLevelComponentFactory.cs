using System;
using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator
{
    public class Fake3DGameLevelComponentFactory : IGameLevelComponentFactory, IGameLevelRendererFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new AreaColorRender(Rgba32.White);
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new RandomizedImageCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(
                    new []
                    {
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_1.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_2.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_3.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_4.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_5.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_6.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_7.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_8.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_9.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_10.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_11.png"
                    }),
                true);
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            return Array.Empty<CellRenderer>();
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            return Array.Empty<AreaRenderer>();
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(200, 100);
        }

        public GameLevelRenderer CreateRenderer()
        {
            return new Fake3DGameLevelRenderer(this);
        }
    }
}
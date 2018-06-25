using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class CityLevelWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            RenderGrid renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = new Fake3DGameLevelRenderer(
                CreateBackgroundRenderers(),
                Array.Empty<CellRenderer>(),
                CreateWallRenderers(),
                new GameLevelRenderSettings(200, 100));
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }
        
        IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new AreaColorRender(Rgba32.White);
        }
        
        IEnumerable<CellRenderer> CreateWallRenderers()
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
    }
}
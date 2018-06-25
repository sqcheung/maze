using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common.Renderers;
using Maze.GameLevelGenerator;
using Maze.GameLevelGenerator.Components;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace Maze.Test
{
    public class RenderFacts
    {
        class GameLevelFactory : IGameLevelComponentFactory
        {   
            public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
            {
                yield return new AreaColorRender(Rgba32.Blue);
            }

            public IEnumerable<CellRenderer> CreateWallRenderers()
            {
                Image<Rgba32> north = CreateImage(Rgba32.LightBlue);
                Image<Rgba32> south = CreateImage(Rgba32.LightCoral);
                Image<Rgba32> east = CreateImage(Rgba32.LightGoldenrodYellow);
                Image<Rgba32> west = CreateImage(Rgba32.LightGray);
                Image<Rgba32> northSouth = CreateImage(Rgba32.LightGreen);
                Image<Rgba32> northEast = CreateImage(Rgba32.LightPink);
                Image<Rgba32> northWest = CreateImage(Rgba32.LightSalmon);
                Image<Rgba32> southEast = CreateImage(Rgba32.LightSeaGreen);
                Image<Rgba32> southWest = CreateImage(Rgba32.LightSkyBlue);
                Image<Rgba32> eastWest = CreateImage(Rgba32.LightSlateGray);
                Image<Rgba32> northSouthEast = CreateImage(Rgba32.LightSteelBlue);
                Image<Rgba32> northSouthWest = CreateImage(Rgba32.LightYellow);
                Image<Rgba32> northEastWest = CreateImage(Rgba32.Aqua);
                Image<Rgba32> southEastWest = CreateImage(Rgba32.Aquamarine);
                Image<Rgba32> northSouthEastWest = CreateImage(Rgba32.Azure);

                yield return new DirectedCellRenderer(
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
                    northSouthEast,
                    northSouthWest,
                    northEastWest,
                    southEastWest,
                    northSouthEastWest);
            }

            public IEnumerable<CellRenderer> CreateGroundRenderers()
            {
                Image<Rgba32> north = CreateImage(Rgba32.DarkBlue);
                Image<Rgba32> south = CreateImage(Rgba32.DarkCyan);
                Image<Rgba32> east = CreateImage(Rgba32.DarkGoldenrod);
                Image<Rgba32> west = CreateImage(Rgba32.DarkGray);
                Image<Rgba32> northSouth = CreateImage(Rgba32.DarkGreen);
                Image<Rgba32> northEast = CreateImage(Rgba32.DarkKhaki);
                Image<Rgba32> northWest = CreateImage(Rgba32.DarkMagenta);
                Image<Rgba32> southEast = CreateImage(Rgba32.DarkOliveGreen);
                Image<Rgba32> southWest = CreateImage(Rgba32.DarkOrange);
                Image<Rgba32> eastWest = CreateImage(Rgba32.DarkOrchid);
                Image<Rgba32> northSouthEast = CreateImage(Rgba32.DarkRed);
                Image<Rgba32> northSouthWest = CreateImage(Rgba32.DarkSalmon);
                Image<Rgba32> northEastWest = CreateImage(Rgba32.DarkSeaGreen);
                Image<Rgba32> southEastWest = CreateImage(Rgba32.DarkSlateBlue);
                Image<Rgba32> northSouthEastWest = CreateImage(Rgba32.DarkSlateGray);
                Image<Rgba32> unknown = CreateImage(Rgba32.DarkTurquoise);
                
                yield return new DirectedCellRenderer(
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
                    northSouthEast,
                    northSouthWest,
                    northEastWest,
                    southEastWest,
                    northSouthEastWest,
                    unknown);
            }

            public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
            {
                yield return new AreaColorRender(new Rgba32(20, 100, 80, 20));
            }

            public GameLevelRendererSettings CreateLevelSettings()
            {
                return new GameLevelRendererSettings(20, 20);
            }

            public GameLevelRenderer CreateRenderer()
            {
                throw new NotImplementedException();
            }

            static Image<Rgba32> CreateImage(Rgba32 backgroundColor)
            {
                return new Image<Rgba32>(Configuration.Default, 20, 20, backgroundColor);
            }
        }

        [Fact]
        public void should_render_normal_game_level()
        {
            var stream = new MemoryStream();
            using (var renderer = new NormalGameLevelRenderer(new GameLevelFactory()))
            {
                renderer.Render(ComplexMazeFixture.Create(), stream);
            }
            
            stream.Seek(0, SeekOrigin.Begin);
            
            using (Image<Rgba32> actual = Image.Load<Rgba32>(stream))
            using (Stream expectedStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Maze.Test.Resources.test.png"))
            using (Image<Rgba32> expected = Image.Load<Rgba32>(expectedStream))
            {
                AssertImageEqual(actual, expected);
            }
        }

        [Fact]
        public void should_render_fake_3d_game_level()
        {
            var stream = new MemoryStream();
            using (var renderer = new Fake3DGameLevelRenderer(new GameLevelFactory()))
            {
                renderer.Render(ComplexMazeFixture.Create(), stream);
            }
            
            stream.Seek(0, SeekOrigin.Begin);
            
            using (Image<Rgba32> actual = Image.Load<Rgba32>(stream))
            using (Stream expectedStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Maze.Test.Resources.test_3d.png"))
            using (Image<Rgba32> expected = Image.Load<Rgba32>(expectedStream))
            {
                AssertImageEqual(actual, expected);
            }
        }

        static void AssertImageEqual(Image<Rgba32> actual, Image<Rgba32> expected)
        {
            Assert.Equal(expected.Width, actual.Width);
            Assert.Equal(expected.Height, actual.Height);
            for (int x = 0; x < expected.Width; ++x)
            {
                for (int y = 0; y < expected.Height; ++y)
                {
                    Assert.Equal(expected[x, y], actual[x, y]);
                }
            }
        }
    }
}
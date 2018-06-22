using System;
using System.IO;
using System.Reflection;
using Maze.Common;
using Maze.Common.Algorithms;
using Maze.Common.Renderers;
using Maze.GameLevelGenerator;
using SixLabors.ImageSharp.Processing.Overlays;
using C = System.Console;

namespace Maze.Console
{
    static class Program
    {
        static void Main()
        {
            var grid = new Grid(5, 5);
            new AldosBroderMazeAlgorithm().Update(grid);
            var renderGrid = new RenderGrid(grid);

            using (FileStream stream = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png")))
            using (var gameLevelRenderer = new GameLevelRenderer(
                new [] {new LovelyTreeBackgroundRenderer()},
                new [] {new LovelyTreeAccessoryRenderer()},
                new [] {new LovelyRandomTreeCellRenderer()},
                Array.Empty<AreaRenderer>(),
                new GameLevelRendererSettings(78, 20), 
                true))
            {
                gameLevelRenderer.Render(renderGrid, stream);
            }
        }
    }
}
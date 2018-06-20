using System;
using System.IO;
using Maze.Common;
using C = System.Console;

namespace Maze.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(200, 100);
            new BinaryTreeMazeAlgorithm().Update(grid);
            var renderGrid = new RenderGrid(grid);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            using (var stream = File.Create(Path.Combine(baseDir, "maze.png")))
            {
                new GridRenderer().Render(renderGrid, stream);
            }
        }
    }
}
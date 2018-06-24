using System;
using System.Collections.Generic;
using System.IO;
using Axe.Cli.Parser;
using Axe.Cli.Parser.Transformers;
using Maze.Common;
using Maze.Common.Algorithms;
using Maze.GameLevelGenerator;
using C = System.Console;

namespace Maze.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            ArgsParser parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddOptionWithValue("kind", 'k', "Specify the kind of maze to render.", true)
                .AddOptionWithValue("row", 'r', "Specify the number of rows in the maze.", true,
                    new IntegerTransformer())
                .AddOptionWithValue("column", 'c', "Specify the number of columns in the maze.", true,
                    new IntegerTransformer())
                .EndCommand()
                .Build();

            ArgsParsingResult argsParsingResult = parser.Parse(args);
            if (!argsParsingResult.IsSuccess)
            {
                PrintUsage();
                return;
            }

            string mazeKind = argsParsingResult.GetFirstOptionValue<string>("--kind");
            int numberOfRows = argsParsingResult.GetFirstOptionValue<int>("--row");
            int numberOfColumns = argsParsingResult.GetFirstOptionValue<int>("--column");
            
            if (numberOfRows <= 0 || numberOfColumns <= 0)
            {
                PrintUsage();
                return;
            }
            
            IGameLevelComponentFactory factory = CreateMazeFactory(mazeKind);
            if (factory == null)
            {
                PrintUsage();
                return;
            }

            var grid = new Grid(numberOfRows, numberOfColumns);
            new AldosBroderMazeAlgorithm().Update(grid);
            var renderGrid = new RenderGrid(grid);

            using (FileStream stream = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png")))
            using (var gameLevelRenderer = new GameLevelRenderer(factory))
            {
                gameLevelRenderer.Render(renderGrid, stream);
            }
        }

        static IGameLevelComponentFactory CreateMazeFactory(string mazeKind)
        {
            switch (mazeKind)
            {
                case "lovely-tree": return new LovelyTreeGameLevelComponentFactory();
                case "farm": return new GrassGameLevelComponentFactory();
                default: return null;
            }
        }

        static void PrintUsage()
        {
            C.WriteLine("Usage:");
            C.WriteLine("--kind | -k   The kind of maze to render. Supported values are");
            C.WriteLine("              lovely-tree, farm");
            C.WriteLine("--row  | -r   Specify the number of rows in the maze.");
            C.WriteLine("--column | -c Specify the number of columns in the maze.");
        }
    }
}
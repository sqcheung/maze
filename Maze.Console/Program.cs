using System;
using System.IO;
using Axe.Cli.Parser;
using Axe.Cli.Parser.Transformers;
using Maze.Common;
using Maze.Common.Algorithms;
using Maze.GameLevelGenerator;
using Maze.GameLevelGenerator.Components;
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
            
            IGameLevelRendererFactory factory = CreateRendererFactory(mazeKind);
            if (factory == null)
            {
                PrintUsage();
                return;
            }

            var grid = new Grid(numberOfRows, numberOfColumns);
            new AldosBroderMazeAlgorithm().Update(grid);
            var renderGrid = new RenderGrid(grid);

            using (FileStream stream = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png")))
            using (GameLevelRenderer gameLevelRenderer = factory.CreateRenderer())
            {
                gameLevelRenderer.Render(renderGrid, stream);
            }
        }

        static IGameLevelRendererFactory CreateRendererFactory(string mazeKind)
        {
            switch (mazeKind)
            {
                case "tree": return new LovelyTreeGameLevelFactory();
                case "grass": return new GrassGameLevelFactory();
                case "city": return new CityFactory();
                case "town": return new TownFactory();
                default: return null;
            }
        }

        static void PrintUsage()
        {
            C.WriteLine("Usage:");
            C.WriteLine("--kind | -k   The kind of maze to render. Supported values are");
            C.WriteLine("              tree, grass, city, town");
            C.WriteLine("--row  | -r   Specify the number of rows in the maze.");
            C.WriteLine("--column | -c Specify the number of columns in the maze.");
        }
    }
}
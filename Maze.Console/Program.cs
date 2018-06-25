using System;
using System.IO;
using Axe.Cli.Parser;
using Axe.Cli.Parser.Transformers;
using Maze.Common;
using Maze.Common.Algorithms;
using Maze.GameLevelGenerator;
using Maze.GameLevelGenerator.Components;
using SixLabors.ImageSharp.PixelFormats;
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
                .BeginCommand("exp", "Doing some experiment on highly customizable maze.")
                .AddOptionWithValue("row", 'r', "Specify the number of rows in the maze.", true, new IntegerTransformer())
                .AddOptionWithValue("column", 'c', "Specify the number of columns in the maze.", true, new IntegerTransformer())
                .AddOptionWithValue("cellsize", 's', "Specify the width of each cell.", true, new IntegerTransformer())
                .AddOptionWithValue("wallcolor", null, "Specify the color of the wall in hex format", true, new Rgba32Transformer())
                .EndCommand()
                .Build();
            
            ArgsParsingResult argsParsingResult = parser.Parse(args);
            if (!argsParsingResult.IsSuccess)
            {
                PrintUsage(argsParsingResult.Error);
                return;
            }

            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png");
            
            if (argsParsingResult.Command.Symbol == null)
            {
                RenderPredefinedMaze(argsParsingResult, imagePath);
            }
            else if (argsParsingResult.Command.Symbol.Equals("exp", StringComparison.OrdinalIgnoreCase))
            {
                RenderCusomizedMaze(argsParsingResult, imagePath);
            }
        }

        static void RenderCusomizedMaze(ArgsParsingResult argsParsingResult, string imagePath)
        {
            int numberOfRows = argsParsingResult.GetFirstOptionValue<int>("--row");
            int numberOfColumns = argsParsingResult.GetFirstOptionValue<int>("--column");
            int cellSize = argsParsingResult.GetFirstOptionValue<int>("--cellsize");
            Rgba32 wallColor = argsParsingResult.GetFirstOptionValue<Rgba32>("--wallcolor");

            if (numberOfRows <= 0 || numberOfColumns <= 0)
            {
                PrintUsage(argsParsingResult.Error);
                return;
            }

            using (var renderer = new NormalGameLevelRenderer(new ColorMazeFactory
            {
                BackgroundColor = Rgba32.Black,
                WallColor = wallColor,
                CellSize = cellSize
            }))
            using (FileStream stream = File.Create(imagePath))
            {
                renderer.Render(CreateMazeGrid(numberOfRows, numberOfColumns), stream);
            }
        }

        static void RenderPredefinedMaze(ArgsParsingResult argsParsingResult, string imagePath)
        {
            string mazeKind = argsParsingResult.GetFirstOptionValue<string>("--kind");
            int numberOfRows = argsParsingResult.GetFirstOptionValue<int>("--row");
            int numberOfColumns = argsParsingResult.GetFirstOptionValue<int>("--column");
            
            if (numberOfRows <= 0 || numberOfColumns <= 0)
            {
                PrintUsage(argsParsingResult.Error);
                return;
            }
            
            IGameLevelRendererFactory factory = CreateRendererFactory(mazeKind);
            if (factory == null)
            {
                PrintUsage(argsParsingResult.Error);
                return;
            }

            RenderGrid renderGrid = CreateMazeGrid(numberOfRows, numberOfColumns);

            using (FileStream stream = File.Create(imagePath))
            using (GameLevelRenderer gameLevelRenderer = factory.CreateRenderer())
            {
                gameLevelRenderer.Render(renderGrid, stream);
            }
        }

        static RenderGrid CreateMazeGrid(int numberOfRows, int numberOfColumns)
        {
            var grid = new Grid(numberOfRows, numberOfColumns);
            new AldosBroderMazeAlgorithm().Update(grid);
            var renderGrid = new RenderGrid(grid);
            return renderGrid;
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

        static void PrintUsage(ArgsParsingError error)
        {
            C.Error.WriteLine("Error: ");
            C.Error.WriteLine($"Code: {error.Code.ToString()}. Tigger: {error.Trigger}");
            
            C.WriteLine();
            C.WriteLine("Usage:");
            
            C.WriteLine("--kind | -k   The kind of maze to render. Supported values are");
            C.WriteLine("              tree, grass, city, town");
            C.WriteLine("--row  | -r   Specify the number of rows in the maze.");
            C.WriteLine("--column | -c Specify the number of columns in the maze.");
        }
    }
}
﻿using System;
using System.IO;
using Axe.Cli.Parser;
using Axe.Cli.Parser.Transformers;
using Maze.Common.Algorithms;
using Maze.GameLevelGenerator;
using Maze.GameLevelGenerator.Components;
using SixLabors.ImageSharp.PixelFormats;
using C = System.Console;

namespace Maze.Console
{
    static class Program
    {
        const int InvalidArgumentCode = -2;
        
        static int Main(string[] args)
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
                .EndCommand()
                .Build();
            
            ArgsParsingResult argsParsingResult = parser.Parse(args);
            if (!argsParsingResult.IsSuccess)
            {
                PrintUsage(argsParsingResult.Error.Code.ToString(), argsParsingResult.Error.Trigger);
                return (int)argsParsingResult.Error.Code;
            }

            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png");
            
            if (argsParsingResult.Command.Symbol == null)
            {
                return RenderPredefinedMaze(argsParsingResult, imagePath);
            }

            if (argsParsingResult.Command.Symbol.Equals("exp", StringComparison.OrdinalIgnoreCase))
            {
                return RenderCusomizedMaze(argsParsingResult, imagePath);
            }

            return -1; // should not be here.
        }

        static int RenderCusomizedMaze(ArgsParsingResult argsParsingResult, string imagePath)
        {
            int numberOfRows = argsParsingResult.GetFirstOptionValue<int>("--row");
            int numberOfColumns = argsParsingResult.GetFirstOptionValue<int>("--column");
            int cellSize = argsParsingResult.GetFirstOptionValue<int>("--cellsize");

            if (numberOfRows <= 0 || numberOfColumns <= 0)
            {
                PrintUsage("InvalidArgument", "--row or --column");
                return (int)argsParsingResult.Error.Code;
            }

            using (FileStream stream = File.Create(imagePath))
            {
                new ColorLevelWriter().Write(
                    stream, 
                    new MazeGridSettings(numberOfRows, numberOfColumns), 
                    new GameLevelRenderSettings(cellSize, 10));
            }

            return 0;
        }

        static int RenderPredefinedMaze(ArgsParsingResult argsParsingResult, string imagePath)
        {
            string mazeKind = argsParsingResult.GetFirstOptionValue<string>("--kind");
            int numberOfRows = argsParsingResult.GetFirstOptionValue<int>("--row");
            int numberOfColumns = argsParsingResult.GetFirstOptionValue<int>("--column");
            
            if (numberOfRows <= 0 || numberOfColumns <= 0)
            {
                PrintUsage("InvalidArgument", $"--row {numberOfRows} --column {numberOfColumns}");
                return InvalidArgumentCode;
            }

            using (FileStream stream = File.Create(imagePath))
            {
                return RenderPredefinedMaze(stream, mazeKind, new MazeGridSettings(numberOfRows, numberOfColumns));
            }
        }

        static int RenderPredefinedMaze(FileStream stream, string mazeKind, MazeGridSettings mazeGridSettings)
        {
            switch (mazeKind)
            {
                case "tree": new TreeLevelWriter().Write(stream, mazeGridSettings);
                    break;
                case "grass": new GrassLevelWriter().Write(stream, mazeGridSettings);
                    break;
                case "city": new CityLevelWriter().Write(stream, mazeGridSettings);
                    break;
                case "town": new TownLevelWriter().Write(stream, mazeGridSettings);
                    break;
                default: 
                    PrintUsage("InvalidArgument", $"--kind {mazeKind}");
                    return InvalidArgumentCode;
            }

            return 0;
        }

        static void PrintUsage(string code, string trigger)
        {
            C.Error.WriteLine("Error: ");
            C.Error.WriteLine($"Code: {code}. Tigger: {trigger}");
        }
    }
}
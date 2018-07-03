using System;
using System.IO;
using Axe.Cli.Parser;
using Axe.Cli.Parser.Transformers;
using Maze.GameLevelGenerator;
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
                .Build();
            
            ArgsParsingResult argsParsingResult = parser.Parse(args);
            if (!argsParsingResult.IsSuccess)
            {
                PrintUsage(argsParsingResult.Error.Code.ToString(), argsParsingResult.Error.Trigger);
                return (int)argsParsingResult.Error.Code;
            }

            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png");
            return RenderPredefinedMaze(argsParsingResult, imagePath);
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
            var writer = new WriterFactory().CreateWriter(mazeKind);
            if (writer == null)
            {
                PrintUsage("InvalidArgument", $"--kind {mazeKind}");
                return InvalidArgumentCode;
            }
            writer.Write(stream,mazeGridSettings);
            return 0;
        }

        static void PrintUsage(string code, string trigger)
        {
            C.Error.WriteLine("Error: ");
            C.Error.WriteLine($"Code: {code}. Tigger: {trigger}");
        }
    }
}
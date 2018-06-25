using System.Collections.Generic;
using Axe.Cli.Parser;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.Console
{
    class Rgba32Transformer : ValueTransformer
    {
        protected override IList<string> SplitArgument(string argument)
        {
            return new[] {argument};
        }

        protected override object TransformSingleArgument(string argument)
        {
            return Rgba32.FromHex(argument);
        }
    }
}
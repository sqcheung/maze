using System.Linq;
using Maze.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator
{
    public class LovelyTreeAccessoryRenderer : RandomizedVisibleCellRenderer
    {
        static readonly string[] ResourceKeys =
        {
            "Maze.GameLevelGenerator.Textures.accessory_ground_15.png",
            "Maze.GameLevelGenerator.Textures.accessory_ground_26_28.png",
            "Maze.GameLevelGenerator.Textures.accessory_ground_31_23.png"
        };
        
        static Image<Rgba32>[] LoadTextures()
        {
            return ResourceKeys
                .Select(key => typeof(LovelyTreeAccessoryRenderer).Assembly.GetManifestResourceStream(key))
                .Select(Image.Load<Rgba32>)
                .ToArray();
        }
        
        public LovelyTreeAccessoryRenderer() : base(LoadTextures(), true)
        {
        }

        protected override bool IsSupported(RenderCell cell)
        {
            return cell.RenderType == RenderType.Ground;
        }
    }
}
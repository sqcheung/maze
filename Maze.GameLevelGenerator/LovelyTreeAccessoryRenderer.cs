using Maze.Common;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public class LovelyTreeAccessoryRenderer : RandomizedImageVisibilityCellRenderer
    {
        static readonly string[] ResourceKeys =
        {
            "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_1.png",
            "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_2.png",
            "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_3.png"
        };
        
        public LovelyTreeAccessoryRenderer() 
            : base(typeof(LovelyTreeAccessoryRenderer).Assembly.LoadEmbeddedResources(ResourceKeys), true)
        {
        }

        protected override bool IsSupported(RenderCell cell)
        {
            return cell.RenderType == RenderType.Ground;
        }
    }
}
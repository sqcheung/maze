using Maze.Common;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public class LovelyRandomTreeCellRenderer : RandomizedImageCellRenderer
    {
        static readonly string[] ResourceKeys =
        {
            "Maze.GameLevelGenerator.Textures.lovely_tree_wall_1.png",
            "Maze.GameLevelGenerator.Textures.lovely_tree_wall_2.png",
            "Maze.GameLevelGenerator.Textures.lovely_tree_wall_3.png"
        };
        
        public LovelyRandomTreeCellRenderer()
            : base(typeof(LovelyRandomTreeCellRenderer).Assembly.LoadEmbeddedResources(ResourceKeys), true)
        {
        }

        protected override bool IsSupported(RenderCell cell)
        {
            return cell.RenderType == RenderType.Wall;
        }
    } 
}
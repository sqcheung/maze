using Maze.Common;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public class GrassGroundRenderer : RandomizedImageCellRenderer
    {
        static readonly string[] ResourceKeys =
        {
            "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png",
            "Maze.GameLevelGenerator.Textures.sm_grass_ground_2.png",
            "Maze.GameLevelGenerator.Textures.sm_grass_ground_3.png",
            "Maze.GameLevelGenerator.Textures.sm_grass_ground_4.png",
            "Maze.GameLevelGenerator.Textures.sm_grass_ground_5.png",
            "Maze.GameLevelGenerator.Textures.sm_grass_ground_6.png"
        };
        
        public GrassGroundRenderer() 
            : base(
                typeof(GrassGroundRenderer).Assembly.LoadEmbeddedResources(ResourceKeys),
                true)
        {
        }

        protected override bool IsSupported(RenderCell cell)
        {
            return cell.RenderType == RenderType.Ground;
        }
    }
}
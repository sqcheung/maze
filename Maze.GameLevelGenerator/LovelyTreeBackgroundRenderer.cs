using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public class LovelyTreeBackgroundRenderer : AreaTileImageRenderer
    {
        public LovelyTreeBackgroundRenderer()
            : base(
                typeof(LovelyTreeBackgroundRenderer).Assembly.LoadEmbeddedResource(
                    "Maze.GameLevelGenerator.Textures.lovely_tree_ground.png"), true)
        {
        }
    }
}
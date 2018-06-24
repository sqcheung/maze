using System.Collections.Generic;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public class LovelyTreeGameLevelComponentFactory : IGameLevelComponentFactory
    {
        public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
        {
            yield return new LovelyTreeBackgroundRenderer();
        }

        public IEnumerable<CellRenderer> CreateWallRenderers()
        {
            yield return new LovelyRandomTreeCellRenderer();
        }

        public IEnumerable<CellRenderer> CreateGroundRenderers()
        {
            yield return new LovelyTreeAccessoryRenderer();
        }

        public IEnumerable<AreaRenderer> CreateAtomsphereRenderers()
        {
            yield break;
        }

        public GameLevelRendererSettings CreateLevelSettings()
        {
            return new GameLevelRendererSettings(78, 20);
        }
    }
}
using System.Collections.Generic;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public interface IGameLevelComponentFactory
    {
        IEnumerable<AreaRenderer> CreateBackgroundRenderers();
        IEnumerable<CellRenderer> CreateWallRenderers();
        IEnumerable<CellRenderer> CreateGroundRenderers();
        GameLevelRenderSettings CreateSettings();
    }
}
using System;

namespace Maze.Common
{
    [Flags]
    public enum Direction
    {
        Unknown = 0,
        East = 1,
        West = 2,
        North = 4,
        South = 8
    }

    public enum RenderType
    {
        Unknown = 0,
        Ground = 1,
        Wall = 2
    }
}
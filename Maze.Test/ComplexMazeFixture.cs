using Maze.Common;

namespace Maze.Test
{
    static class ComplexMazeFixture
    {
        public static RenderGrid Create()
        {
            var grid = new Grid(6, 6);
            grid[0, 0].Link(grid[0, 0].East);
            grid[0, 0].Link(grid[0, 0].South);
            grid[0, 1].Link(grid[0, 1].East);
            grid[0, 2].Link(grid[0, 2].East);
            grid[0, 2].Link(grid[0, 2].South);
            grid[0, 3].Link(grid[0, 3].East);
            grid[0, 4].Link(grid[0, 4].South);
            grid[1, 0].Link(grid[1, 0].East);
            grid[1, 0].Link(grid[1, 0].South);
            grid[1, 2].Link(grid[1, 2].South);
            grid[1, 3].Link(grid[1, 3].South);
            grid[1, 4].Link(grid[1, 4].South);
            grid[2, 0].Link(grid[2, 0].South);
            grid[2, 0].Link(grid[2, 0].East);
            grid[2, 1].Link(grid[2, 1].South);
            grid[2, 1].Link(grid[2, 1].East);
            grid[2, 2].Link(grid[2, 2].South);
            grid[2, 2].Link(grid[2, 2].East);
            grid[2, 3].Link(grid[2, 3].East);
            grid[2, 4].Link(grid[2, 4].South);
            grid[3, 0].Link(grid[3, 0].South);
            grid[3, 2].Link(grid[3, 2].South);
            grid[3, 3].Link(grid[3, 3].East);
            grid[4, 0].Link(grid[4, 0].East);
            grid[4, 1].Link(grid[4, 1].East);
            grid[4, 2].Link(grid[4, 2].East);
            grid[4, 3].Link(grid[4, 3].East);
            grid[4, 4].Link(grid[4, 4].North);
            
            return new RenderGrid(grid);
        }
    }
}
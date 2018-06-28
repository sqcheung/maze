using Maze.Common;
using Maze.Common.Algorithms;
using Xunit;

namespace Maze.Test
{
    public class DijkstraSolvingAlgorithmFacts
    {
        [Fact]
        public void should_correctly_calculate_distance()
        {
            // (0,0)-(0,1)
            //         |
            // (1,0)-(1,1)
            
            var grid = new Grid(2, 2);
            grid[0, 0].Link(grid[0, 1]);
            grid[0, 1].Link(grid[1, 1]);
            grid[1, 1].Link(grid[1, 0]);

            var algorithm = new DijkstraSolvingAlgorithm(0, 0);
            algorithm.Update(grid);

            Assert.Equal(0, GetDistance(grid[0, 0]));
            Assert.Equal(1, GetDistance(grid[0, 1]));
            Assert.Equal(2, GetDistance(grid[1, 1]));
            Assert.Equal(3, GetDistance(grid[1, 0]));
        }
        
        [Fact]
        public void should_correctly_calculate_distance_when_contains_closed_point()
        {
            // (0,0)-(0,1) (0,2)
            //         |
            // (1,0)-(1,1)-(1,2)
            
            var grid = new Grid(2, 3);
            grid[0, 0].Link(grid[0, 1]);
            grid[0, 1].Link(grid[1, 1]);
            grid[1, 1].Link(grid[1, 0]);
            grid[1, 1].Link(grid[1, 2]);

            var algorithm = new DijkstraSolvingAlgorithm(0, 0);
            algorithm.Update(grid);

            Assert.Equal(0, GetDistance(grid[0, 0]));
            Assert.Equal(1, GetDistance(grid[0, 1]));
            Assert.Equal(int.MaxValue, GetDistance(grid[0, 2]));
            Assert.Equal(2, GetDistance(grid[1, 1]));
            Assert.Equal(3, GetDistance(grid[1, 0]));
            Assert.Equal(3, GetDistance(grid[1, 2]));
        }

        static int GetDistance(GridCell gridCell)
        {
            return (int)gridCell.Tags[DijkstraSolvingAlgorithm.ResolvingDistance];
        }
    }
}
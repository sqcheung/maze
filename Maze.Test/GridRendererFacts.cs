using System.Linq;
using System.Text;
using Maze.Common;
using Xunit;

namespace Maze.Test
{
    public class GridRendererFacts
    {
        [Fact]
        public void should_render_single_cell()
        {
            var grid = new Grid(1, 1);
            var renderGrid = new RenderGrid(grid);
            
            // | n-w |  n  | n-e |
            // |  w  |  c  |  e  |
            // | s-w |  s  | s-e |
            
            Assert.Equal(3, renderGrid.RowCount);
            Assert.Equal(3, renderGrid.ColumnCount);
            
            var expectedRenderGrid = new[,]
            {
                {"W,SE", "W,EW", "W,SW"},
                {"W,NS", "G,U",  "W,NS"},
                {"W,NE", "W,EW", "W,NW"}
            };
            
            Assert.Equal(expectedRenderGrid, RenderToArray(renderGrid));
        }

        [Fact]
        public void should_render_multiple_columns()
        {
            var grid = new Grid(1, 2);
            grid[0, 0].Link(grid[0, 1]);
            var renderGrid = new RenderGrid(grid);

            var expectedRenderGrid = new[,]
            {
                {"W,SE", "W,EW", "W,EW", "W,EW", "W,SW"},
                {"W,NS", "G,E", "G,EW", "G,W", "W,NS"},
                {"W,NE", "W,EW", "W,EW", "W,EW", "W,NW"}
            };
            
            Assert.Equal(expectedRenderGrid, RenderToArray(renderGrid));
        }

        [Fact]
        public void should_render_links_which_is_an_L_shape()
        {
            var grid = new Grid(2, 2);
            grid[0, 0].Link(grid[0, 1]);
            grid[0, 0].Link(grid[1, 0]);

            var expectedRenderGrid = new[,]
            {
                {"W,SE", "W,EW", "W,EW", "W,EW", "W,SW"},
                {"W,NS", "G,SE", "G,EW", "G,W", "W,NS"},
                {"W,NS", "G,NS", "W,SE", "W,EW", "W,NSW"},
                {"W,NS", "G,N", "W,NS", "G,U", "W,NS"},
                {"W,NE", "W,EW", "W,NEW", "W,EW", "W,NW"}
            };

            string[,] actualResult = RenderToArray(new RenderGrid(grid));
            Assert.Equal(expectedRenderGrid, actualResult);
        }

        [Fact]
        public void should_render_links_which_is_an_I_shape()
        {
            var grid = new Grid(2, 2);
            grid[0, 0].Link(grid[0, 1]);
            grid[0, 0].Link(grid[1, 0]);
            grid[0, 1].Link(grid[1, 1]);
            
            var expectedRenderGrid = new[,]
            {
                {"W,SE", "W,EW",  "W,EW",   "W,EW",  "W,SW"},
                {"W,NS", "G,SE",  "G,EW",   "G,SW",  "W,NS"},
                {"W,NS", "G,NS",  "W,S",    "G,NS",  "W,NS"},
                {"W,NS", "G,N",   "W,NS",   "G,N",   "W,NS"},
                {"W,NE", "W,EW",  "W,NEW",  "W,EW",  "W,NW"}
            };
            
            string[,] actualResult = RenderToArray(new RenderGrid(grid));
            Assert.Equal(expectedRenderGrid, actualResult);
        }
        
        [Fact]
        public void should_remove_isolated_connectors()
        {
            var grid = new Grid(2, 2);
            grid[0, 0].Link(grid[0, 1]);
            grid[1, 0].Link(grid[1, 1]);
            grid[0, 0].Link(grid[1, 0]);
            grid[0, 1].Link(grid[1, 1]);

            var expectedRenderGrid = new[,]
            {
                {"W,SE", "W,EW",  "W,EW",   "W,EW",  "W,SW"},
                {"W,NS", "G,SE",  "G,SEW",  "G,SW",  "W,NS"},
                {"W,NS", "G,NSE", "G,NSEW", "G,NSW", "W,NS"},
                {"W,NS", "G,NE",  "G,NEW",  "G,NW",  "W,NS"},
                {"W,NE", "W,EW",  "W,EW",   "W,EW",  "W,NW"}
            };

            string[,] actualResult = RenderToArray(new RenderGrid(grid));
            Assert.Equal(expectedRenderGrid, actualResult);
        }

        static string[,] RenderToArray(RenderGrid renderGrid)
        {
            var result = new string[renderGrid.RowCount, renderGrid.ColumnCount];
            for (int rowIndex = 0; rowIndex < renderGrid.RowCount; ++rowIndex)
            {
                for (int columnIndex = 0; columnIndex < renderGrid.ColumnCount; ++columnIndex)
                {
                    RenderCell cell = renderGrid[rowIndex, columnIndex];
                    char renderType = cell.RenderType.ToString().First();
                    string direction = CreateDirectionString(cell.Direction);
                    result[rowIndex, columnIndex] = $"{renderType},{direction}";
                }
            }

            return result;
        }

        static string CreateDirectionString(Direction cellDirection)
        {
            var builder = new StringBuilder();
            
            // north, south, east, west

            if ((cellDirection & Direction.North) == Direction.North) builder.Append('N');
            if ((cellDirection & Direction.South) == Direction.South) builder.Append('S');
            if ((cellDirection & Direction.East) == Direction.East) builder.Append('E');
            if ((cellDirection & Direction.West) == Direction.West) builder.Append('W');
            if (builder.Length == 0) builder.Append("U");

            return builder.ToString();
        }

        static void AssertCellProperties(RenderCell cell, RenderType renderType, Direction direction,
            object northNeighbor,
            RenderCell southNeighbor, RenderCell eastNeighbor, object westNeighbor)
        {
            Assert.Equal(direction, cell.Direction);
            Assert.Equal(renderType, cell.RenderType);
            Assert.Equal(northNeighbor, cell.North);
            Assert.Equal(southNeighbor, cell.South);
            Assert.Equal(westNeighbor, cell.West);
            Assert.Equal(eastNeighbor, cell.East);
        }
    }
}
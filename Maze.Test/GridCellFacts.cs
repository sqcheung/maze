using System;
using System.Linq;
using Maze.Common;
using Xunit;

namespace Maze.Test
{
    public class GridCellFacts
    {
        [Fact]
        public void should_create_grid_cell_with_valid_row_and_column_index()
        {
            const int rowIndex = 2;
            const int columnIndex = 0;
            var gridCell = new GridCell(rowIndex, columnIndex);
            
            Assert.Equal(rowIndex, gridCell.Row);
            Assert.Equal(columnIndex, gridCell.Column);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-2, 0)]
        public void should_throw_if_row_or_column_index_is_invalid(int rowIndex, int columnIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GridCell(rowIndex, columnIndex));
        }

        [Fact]
        public void should_get_empty_neighbors()
        {
            var gridCell = new GridCell(2, 3);
            
            Assert.Empty(gridCell.Neighbors);
        }

        [Fact]
        public void should_get_neighbors()
        {
            var gridCell = new GridCell(1, 1);
            var northCell = new GridCell(1, 0);

            gridCell.SetNeighbors(northCell, null, null, null);
            
            Assert.Equal(northCell, gridCell.Neighbors.Single());
        }

        [Fact]
        public void should_linked_to_neighbors()
        {
            var gridCell = new GridCell(1, 1);
            var northCell = new GridCell(1, 0);
            var southCell = new GridCell(1, 2);

            gridCell.SetNeighbors(northCell, southCell, null, null);
            northCell.SetNeighbors(null, gridCell, null, null);
            southCell.SetNeighbors(gridCell, null, null, null);
            gridCell.Link(gridCell.North);
            
            Assert.True(gridCell.IsLinked(gridCell.North));
            Assert.True(gridCell.North.IsLinked(gridCell));
            Assert.False(gridCell.IsLinked(gridCell.South));
            Assert.False(gridCell.South.IsLinked(gridCell));
        }

        [Fact]
        public void should_unlink_neighbors()
        {
            var gridCell = new GridCell(1, 1);
            var northCell = new GridCell(1, 0);

            gridCell.SetNeighbors(northCell, null, null, null);
            northCell.SetNeighbors(null, gridCell, null, null);

            gridCell.Link(northCell);

            Assert.True(gridCell.IsLinked(northCell));
            Assert.True(northCell.IsLinked(gridCell));

            gridCell.Unlink(northCell);
            
            Assert.False(gridCell.IsLinked(northCell));
            Assert.False(northCell.IsLinked(gridCell));
        }
        
        [Fact]
        public void should_not_linked_to_null_neighbors()
        {
            var gridCell = new GridCell(1, 1);

            Assert.Throws<ArgumentNullException>(() => gridCell.Link(null));
        }

        [Fact]
        public void should_not_unlink_to_null_neighbors()
        {
            var gridCell = new GridCell(1, 1);

            Assert.Throws<ArgumentNullException>(() => gridCell.Unlink(null));            
        }

        [Fact]
        public void should_not_unlink_to_non_neighbors()
        {
            var gridCell = new GridCell(1, 1);
            var nonNeighbor = new GridCell(1, 2);

            Assert.Throws<ArgumentException>(() => gridCell.Unlink(nonNeighbor));
        }

        [Fact]
        public void should_not_linked_to_non_neighbors()
        {
            var gridCell = new GridCell(1, 1);
            var notNeighbor = new GridCell(0, 1);

            Assert.Throws<ArgumentException>(() => gridCell.Link(notNeighbor));
        }

        [Fact]
        public void should_be_unlinked_for_null()
        {
            Assert.False(new GridCell(1, 1).IsLinked(null));
        }
    }
}
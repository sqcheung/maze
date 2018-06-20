using System;
using System.Collections.Generic;

namespace Maze.Common
{
    public class GridCell : IEquatable<GridCell>
    {
        Guid Id { get; } = Guid.NewGuid();
        public int Row { get; }
        public int Column { get; }
        HashSet<GridCell> Links { get; } = new HashSet<GridCell>();
        
        public GridCell North { get; private set; }
        public GridCell South { get; private set; }
        public GridCell East { get; private set; }
        public GridCell West { get; private set; }

        public GridCell(int row, int column)
        {
            Row = row >= 0 ? row : throw new ArgumentOutOfRangeException(nameof(row));
            Column = column >= 0 ? column : throw new ArgumentOutOfRangeException(nameof(column));
        }

        public void SetNeighbors(GridCell north, GridCell south, GridCell east, GridCell west)
        {
            North = north;
            South = south;
            East = east;
            West = west;
        }

        public GridCell Link(GridCell gridCell)
        {
            return Link(gridCell, true);
        }

        GridCell Link(GridCell gridCell, bool bidirectional)
        {
            if (gridCell == null) throw new ArgumentNullException(nameof(gridCell));
            if (!IsNeighbor(gridCell)) throw new ArgumentException("Can only be linked to neighbors");
            
            Links.Add(gridCell);
            if (bidirectional) gridCell.Link(this, false);
            return this;
        }

        bool IsNeighbor(GridCell gridCell)
        {
            if (gridCell == null) return false;
            return gridCell.Equals(North) ||
                   gridCell.Equals(South) ||
                   gridCell.Equals(East) ||
                   gridCell.Equals(West);
        }

        public GridCell Unlink(GridCell gridCell, bool bidirectional = true)
        {
            if (gridCell == null) throw new ArgumentNullException(nameof(gridCell));
            if (!IsNeighbor(gridCell)) throw new ArgumentException("The cell is not a neighbor.");
            
            Links.Remove(gridCell);
            if (bidirectional) gridCell.Unlink(this, false);
            return this;
        }

        public bool IsLinked(GridCell gridCell)
        {
            return gridCell != null && Links.Contains(gridCell);
        }

        public IList<GridCell> Neighbors
        {
            get
            {
                var cells = new List<GridCell>();
                if (North != null) cells.Add(North);
                if (South != null) cells.Add(South);
                if (East != null) cells.Add(East);
                if (West != null) cells.Add(West);
                return cells;
            }
        }

        public bool Equals(GridCell other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GridCell) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
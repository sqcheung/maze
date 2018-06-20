using System.Collections.Generic;

namespace Maze.Common
{
    public class RenderCell
    {
        public RenderCell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void SetNeighbors(RenderCell north, RenderCell south, RenderCell east, RenderCell west)
        {
            North = north;
            South = south;
            East = east;
            West = west;
        }

        public IEnumerable<RenderCell> Neighbors
        {
            get
            {
                if (North != null) yield return North;
                if (South != null) yield return South;
                if (East != null) yield return East;
                if (West != null) yield return West;
            }
        }
        
        public int Row { get; }
        public int Column { get; }
        
        public RenderCell North { get; private set; }
        public RenderCell South { get; private set; }
        public RenderCell East { get; private set; }
        public RenderCell West { get; private set; }
        
        public RenderType RenderType { get; set; }
        public Direction Direction { get; set; }
    }
}
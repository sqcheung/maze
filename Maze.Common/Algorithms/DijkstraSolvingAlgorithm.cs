using System;
using System.Linq;
using Maze.Common.Collections;

namespace Maze.Common.Algorithms
{
    public class DijkstraSolvingAlgorithm : IMazeUpdater
    {
        const string PriorityNodeKey = "priority_node";
        public const string ResolvingDistance = "resolve_distance";
        readonly int _startRow;
        readonly int _startColumn;

        public DijkstraSolvingAlgorithm(int startRow, int startColumn)
        {
            _startRow = startRow < 0 ? throw new ArgumentOutOfRangeException(nameof(startRow)) : startRow;
            _startColumn = startColumn < 0 ? throw new ArgumentOutOfRangeException(nameof(startColumn)) : startColumn;
        }
        
        public void Update(Grid grid)
        {
            if (_startRow >= grid.RowCount || _startColumn >= grid.ColumnCount)
            {
                throw new ArgumentException("The grid is too small to hold source cell index", nameof(grid));
            }
                        
            GridCell sourceCell = grid[_startRow, _startColumn];
            sourceCell.Tags.Add(PriorityNodeKey, CreatePriorityNode(sourceCell, 0));
            
            foreach (GridCell cell in grid.GetCells().Where(cell => !sourceCell.Equals(cell)))
            {
                PriorityQueueNode<int, CellNodeValue> priorityQueueNode = CreatePriorityNode(cell, int.MaxValue);
                cell.Tags.Add(PriorityNodeKey, priorityQueueNode);
            }

            var priorityQueue = new PriorityQueue<CellNodeValue, int>(grid.Size);
            foreach (GridCell cell in grid.GetCells())
            {
                PriorityQueueNode<int, CellNodeValue> cellTag = GetPriorityNodeOnCell(cell);
                priorityQueue.Enqueue(cellTag, cellTag.Priority);
            }

            while (priorityQueue.Count > 0)
            {
                PriorityQueueNode<int, CellNodeValue> minDistanceNode = priorityQueue.Dequeue();
                GridCell minDistanceCell = minDistanceNode.Value.Current;
                foreach (GridCell neighbor in minDistanceCell.Links)
                {
                    int altDistance = minDistanceNode.Priority + 1;
                    PriorityQueueNode<int, CellNodeValue> neighborNode = GetPriorityNodeOnCell(neighbor);
                    if (altDistance < neighborNode.Priority)
                    {
                        neighborNode.Value.Previous = minDistanceCell;
                        priorityQueue.UpdatePriority(neighborNode, altDistance);
                    }
                }
            }

            foreach (GridCell cell in grid.GetCells())
            {
                PriorityQueueNode<int, CellNodeValue> node = GetPriorityNodeOnCell(cell);
                cell.Tags.Remove(PriorityNodeKey);
                cell.Tags[ResolvingDistance] = node.Priority;
            }
        }

        static PriorityQueueNode<int, CellNodeValue> GetPriorityNodeOnCell(GridCell cell)
        {
            return (PriorityQueueNode<int, CellNodeValue>)cell.Tags[PriorityNodeKey];
        }

        static PriorityQueueNode<int, CellNodeValue> CreatePriorityNode(GridCell cell, int distance)
        {
            return new PriorityQueueNode<int, CellNodeValue>
            {
                Priority = distance,
                Value = new CellNodeValue
                {
                    Current = cell,
                    Previous = null
                }
            };
        }

        class CellNodeValue
        {
            public GridCell Current { get; set; }
            public GridCell Previous { get; set; }
        }
    }
}
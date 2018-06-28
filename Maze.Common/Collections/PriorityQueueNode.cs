namespace Maze.Common.Collections
{
    class PriorityQueueNode<TPriority, TValue>
    {
        public TValue Value { get; set; }

        /// <summary>
        /// The Priority to insert this node at. Must be set BEFORE adding a node to the queue (ideally just once,
        /// in the node's constructor). Should not be manually edited once the node has been enqueued - use
        /// queue.UpdatePriority() instead
        /// </summary>
        public TPriority Priority { get; set; }

        /// <summary>
        /// Represents the current position in the queue
        /// </summary>
        internal int QueueIndex { get; set; }

        /// <summary>
        /// Represents the order the node was inserted in
        /// </summary>
        internal long InsertionIndex { get; set; }
    }
}
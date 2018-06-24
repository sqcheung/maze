using System;
using System.IO;
using Maze.Common;

namespace Maze.GameLevelGenerator
{
    public abstract class GameLevelRenderer : IDisposable
    {
        protected IGameLevelComponentFactory ComponentFactory { get; }

        protected GameLevelRenderer(IGameLevelComponentFactory componentFactory)
        {
            ComponentFactory = componentFactory;
        }
        
        public abstract void Render(RenderGrid grid, Stream stream);

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Maze.Common;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator
{
    public abstract class GameLevelRenderer : IDisposable
    {
        protected DisposableCollection<AreaRenderer> BackgroundRenderers { get; }
        protected DisposableCollection<CellRenderer> GroundRenderers { get; }
        protected DisposableCollection<CellRenderer> WallRenderers { get; }
        protected GameLevelRenderSettings Settings { get; }
        bool _isDisposed;
        
        static DisposableCollection<T> ToDisposableCollection<T>(IEnumerable<T> collection)
            where T : IDisposable
        {
            return new DisposableCollection<T>(collection);
        }

        protected GameLevelRenderer(
            IEnumerable<AreaRenderer> backgroundRenderers,
            IEnumerable<CellRenderer> groundRenderers,
            IEnumerable<CellRenderer> wallRenderers,
            GameLevelRenderSettings settings)
        {
            BackgroundRenderers = ToDisposableCollection(backgroundRenderers);
            GroundRenderers = ToDisposableCollection(groundRenderers);
            WallRenderers = ToDisposableCollection(wallRenderers);
            Settings = settings;
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                BackgroundRenderers.Dispose();
                GroundRenderers.Dispose();
                WallRenderers.Dispose();
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Render(RenderGrid grid, Stream stream);
    } 
}
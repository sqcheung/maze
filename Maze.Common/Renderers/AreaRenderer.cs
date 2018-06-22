using System;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public abstract class AreaRenderer : IDisposable
    {
        public abstract void Render(
            IImageProcessingContext<Rgba32> context, 
            Rectangle renderArea);

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.Processing.Drawing.Brushes;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public class AreaTileImageRenderer : AreaRenderer
    {
        bool _isDisposed;
        readonly Image<Rgba32> _tileImage;
        readonly bool _disposeTexturesWhenClose;

        public AreaTileImageRenderer(Image<Rgba32> tileImage, bool disposeTexturesWhenClose)
        {
            _tileImage = tileImage;
            _disposeTexturesWhenClose = disposeTexturesWhenClose;
        }
        
        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle renderArea)
        {
            var brush = new ImageBrush<Rgba32>(_tileImage);
            context.Fill(brush, new RectangleF(renderArea.X, renderArea.Y, renderArea.Width, renderArea.Height));
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing && _disposeTexturesWhenClose) { _tileImage.Dispose(); }

            _isDisposed = true;
        }
    }
}
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public abstract class KeyedImageCellRenderer : CellRenderer
    {
        readonly bool _disposeTextureWhenClose;
        readonly DisposableCollection<Image<Rgba32>> _textures;
        readonly IDictionary<string, Image<Rgba32>> _map;
        bool _isDisposed;
        
        protected abstract bool IsSupported(RenderCell cell);

        protected KeyedImageCellRenderer(
            IDictionary<string, Image<Rgba32>> map,
            bool disposeTexturesWhenClose)
        {
            _disposeTextureWhenClose = disposeTexturesWhenClose;
            _textures = new DisposableCollection<Image<Rgba32>>(map.Values);
            _map = map;
        }

        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            if (!IsSupported(cell)) return;
            Image<Rgba32> texture = _map[GetTextureKey(cell)];
            if (texture == null) return;
            var location = new Point(
                (cellArea.Left + cellArea.Right) / 2,
                cellArea.Bottom);
            location.X -= texture.Width / 2;
            location.Y -= texture.Height;
            context.DrawImage(texture, PixelBlenderMode.Normal, 1.0f, location);
        }

        protected abstract string GetTextureKey(RenderCell cell);

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing && _disposeTextureWhenClose)
            {
                _textures.Dispose();
                _map.Clear();
            }

            _isDisposed = true;
        }

    }
}
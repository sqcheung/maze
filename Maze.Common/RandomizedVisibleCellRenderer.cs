using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;
using Rectangle = System.Drawing.Rectangle;

namespace Maze.Common
{
    public abstract class RandomizedVisibleCellRenderer : CellRenderer
    {
        readonly bool _disposeTexturesWhenClose;
        readonly Image<Rgba32>[] _textures;
        readonly Random _random = new Random();
        bool _isDisposed;

        protected abstract bool IsSupported(RenderCell cell);
        public int Possibility { get; set; } = 30;
        
        protected RandomizedVisibleCellRenderer(
            IEnumerable<Image<Rgba32>> textures, 
            bool disposeTexturesWhenClose)
        {
            _disposeTexturesWhenClose = disposeTexturesWhenClose;
            _textures = textures.ToArray();
        }
        
        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            if (!IsSupported(cell)) return;
            bool shouldRenderer = _random.Next(100) <= Possibility;
            if (shouldRenderer)
            {
                Image<Rgba32> texture = _textures[_random.Next(_textures.Length)];
                int maxX = cellArea.Width - texture.Width;
                int maxY = cellArea.Height - texture.Height;
                
                // Will not renderer if texture is too large
                if (maxX < 0 || maxY < 0) return;

                context.DrawImage(
                    texture, 
                    1.0f, 
                    new Point(cellArea.Left + _random.Next(maxX), cellArea.Top + _random.Next(maxY)));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing && _disposeTexturesWhenClose)
            {
                foreach (Image<Rgba32> texture in _textures)
                {
                    texture.Dispose();
                }
            }

            _isDisposed = true;
        }
    }
}
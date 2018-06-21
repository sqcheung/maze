using System;
using System.IO;
using System.Linq;
using Maze.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;
using Rectangle = System.Drawing.Rectangle;

namespace Maze.GameLevelGenerator
{
    public class LovelyRandomTreeCellRenderer : CellRenderer
    {
        static readonly string[] ResourceKeys =
        {
            "Maze.GameLevelGenerator.Textures.tree1_86_100.png",
            "Maze.GameLevelGenerator.Textures.tree2_86_100.png"
        };

        const int ResourceHalfWidth = 43;
        const int ResourceHeight = 100;
        
        bool _isDisposed;
        readonly Image<Rgba32>[] _textures;
        
        public LovelyRandomTreeCellRenderer()
        {
            _textures = ResourceKeys.Select(key =>
            {
                using (var stream = typeof(LovelyRandomTreeCellRenderer).Assembly.GetManifestResourceStream(key))
                {
                    return Image.Load<Rgba32>(stream);
                }
            }).ToArray();
        }

        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            var rand = new Random();
            var texture = _textures[rand.Next(_textures.Length)];
            
            var location = new Point(
                (cellArea.Left + cellArea.Right) / 2,
                cellArea.Bottom);
            location.X -= ResourceHalfWidth;
            location.Y -= ResourceHeight;
            context.DrawImage(texture, PixelBlenderMode.Normal, 1.0f, location);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            
            if (disposing)
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
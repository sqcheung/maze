using System.Drawing;
using System.IO;
using System.Reflection;
using Maze.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.Processing.Drawing.Brushes;

namespace Maze.GameLevelGenerator
{
    public class LovelyTreeBackgroundRenderer : ContextBasedRenderer
    {
        bool _isDisposed;
        readonly Image<Rgba32> _texture;

        public LovelyTreeBackgroundRenderer()
        {
            using (Stream stream = typeof(LovelyTreeBackgroundRenderer).Assembly
                .GetManifestResourceStream("Maze.GameLevelGenerator.Textures.tree_ground_256.jpg"))
            {
                _texture = Image.Load<Rgba32>(stream);
            }
        }

        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea)
        {
            var brush = new ImageBrush<Rgba32>(_texture);
            context.Fill(brush);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            
            if (disposing)
            {
                _texture.Dispose();
            }
            
            _isDisposed = true;
        }
    }
}
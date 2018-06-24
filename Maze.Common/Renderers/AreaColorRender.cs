using System;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public class AreaColorRender : AreaRenderer
    {
        readonly Rgba32 _color;

        public AreaColorRender(Rgba32 color)
        {
            _color = color;
        }
        
        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle renderArea)
        {
            context.Fill(_color, renderArea);
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
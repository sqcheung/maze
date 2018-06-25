using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public class CellColorRender : CellRenderer
    {
        readonly Rgba32 _color;

        public CellColorRender(Rgba32 color)
        {
            _color = color;
        }
        
        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            context.Fill(_color, cellArea);
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
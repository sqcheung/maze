using System;
using System.Linq;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;

namespace Maze.GameLevelGenerator
{
    public class ColorGradientRenderer : CellRenderer
    {
        readonly string _tagKey;
        readonly int _min;
        readonly int _max;
        readonly Rgba32 _gradientStart;
        readonly Rgba32 _gradientEnd;
        readonly float _stepR;
        readonly float _stepG;
        readonly float _stepB;

        public ColorGradientRenderer(string tagKey, int min, int max, Rgba32 gradientStart, Rgba32 gradientEnd)
        {
            _tagKey = tagKey;
            _min = min;
            _max = max;
            _gradientStart = gradientStart;
            _gradientEnd = gradientEnd;
            _stepR = ((float)_gradientEnd.R - _gradientStart.R) / (max - min);
            _stepG = ((float)_gradientEnd.G - _gradientStart.G) / (max - min);
            _stepB = ((float)_gradientEnd.B - _gradientStart.B) / (max - min);
        }
        
        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            Rgba32? renderingColor = GetRenderingColor(cell);
            if (renderingColor == null) return;
            
            context.Fill(renderingColor.Value, cellArea);
        }

        Rgba32? GetRenderingColor(RenderCell cell)
        {
            object tag = cell.GetTag(_tagKey);
            if (tag == null)
            {
                RenderCell neighborGround = cell.Neighbors
                    .FirstOrDefault(n => n.RenderType == RenderType.Ground);
                return neighborGround != null ? GetRenderingColor(neighborGround) : null;
            }
            
            if (!(tag is int)) return null;
            var value = (int)tag;

            Rgba32 theColor;

            if (value <= _min) theColor = _gradientStart;
            else if (value >= _max) theColor = _gradientEnd;
            else
            {
                int diff = (value - _min);
                theColor = new Rgba32(
                    (byte)(_gradientStart.R + diff * _stepR),
                    (byte)(_gradientStart.G + diff * _stepG),
                    (byte)(_gradientStart.B + diff * _stepB));
            }

            return theColor;
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
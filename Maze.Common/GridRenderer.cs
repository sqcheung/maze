using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.Processing.Overlays;
using SixLabors.Primitives;

namespace Maze.Common
{
    public class GridRenderer
    {
        const int CellSize = 3;
        
        public void Render(RenderGrid grid, Stream stream)
        {
            using (var image = new Image<Rgba32>(grid.ColumnCount * CellSize, grid.RowCount * CellSize))
            {
                image.Mutate(context =>
                {
                    context.BackgroundColor(Rgba32.Black);
                    foreach (RenderCell cell in grid.GetCells())
                    {
                        if (cell.RenderType == RenderType.Wall)
                        {
                            PointF upperLeft = new PointF(cell.Column * CellSize, cell.Row * CellSize);
                            context.Fill(Rgba32.DarkRed, new RectangleF(upperLeft, new SizeF(CellSize, CellSize)));
                        }
                    }
                });
                image.Save(stream, ImageFormats.Png);
            }
        }
    }
}
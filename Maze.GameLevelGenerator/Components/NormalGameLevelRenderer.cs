using System.Collections.Generic;
using System.IO;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Maze.GameLevelGenerator.Components
{
    public class NormalGameLevelRenderer : GameLevelRenderer
    {
        public NormalGameLevelRenderer(IEnumerable<AreaRenderer> backgroundRenderers,
            IEnumerable<CellRenderer> groundRenderers, IEnumerable<CellRenderer> wallRenderers,
            GameLevelRenderSettings settings) : base(backgroundRenderers, groundRenderers, wallRenderers, settings)
        {
        }

        int CalculateDimension(int length)
        {
            return length * Settings.CellSize + Settings.Margin * 2;
        }

        int TranslateCoordWithMargin(int original)
        {
            return original + Settings.Margin;
        }

        public override void Render(RenderGrid grid, Stream stream)
        {
            int width = CalculateDimension(grid.ColumnCount);
            int height = CalculateDimension(grid.RowCount);
            using (var image = new Image<Rgba32>(width, height))
            {
                image.Mutate();
                image.Mutate(context =>
                {
                    var fullArea = new Rectangle(0, 0, width, height);
                    
                    foreach (AreaRenderer backgroundRenderer in BackgroundRenderers)
                    {
                        backgroundRenderer.Render(context, fullArea);
                    }
                    
                    foreach (RenderCell cell in grid.GetCells())
                    {
                        var cellArea = new Rectangle(
                            TranslateCoordWithMargin(cell.Column * Settings.CellSize), 
                            TranslateCoordWithMargin(cell.Row * Settings.CellSize), 
                            Settings.CellSize, 
                            Settings.CellSize);

                        if (cell.RenderType == RenderType.Ground)
                        {
                            foreach (CellRenderer groundRenderer in GroundRenderers)
                            {
                                groundRenderer.Render(context, cellArea, cell);
                            }
                        }

                        if (cell.RenderType == RenderType.Wall)
                        {
                            foreach (CellRenderer wallRenderer in WallRenderers)
                            {
                                wallRenderer.Render(context, cellArea, cell);
                            }
                        }
                    }
                });
                
                image.Save(stream, ImageFormats.Png);
            }
        }
    }
}
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
    public class Fake3DGameLevelRenderer : GameLevelRenderer
    {
        public Fake3DGameLevelRenderer(IEnumerable<AreaRenderer> backgroundRenderers,
            IEnumerable<CellRenderer> groundRenderers, IEnumerable<CellRenderer> wallRenderers,
            GameLevelRenderSettings settings) : base(backgroundRenderers, groundRenderers, wallRenderers, settings)
        {
        }

        int TranslateCoordWithMargin(int original)
        {
            return original + Settings.Margin;
        }

        public override void Render(RenderGrid grid, Stream stream)
        {
            int width = Settings.CellSize / 2 * (grid.RowCount + grid.ColumnCount) + Settings.Margin * 2;
            int height = Settings.CellSize / 4 * (grid.RowCount + grid.ColumnCount) + Settings.Margin * 2;
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

                    for (int columnIndex = grid.ColumnCount - 1; columnIndex >= 0; --columnIndex)
                    {
                        for (int rowIndex = 0; rowIndex < grid.RowCount; ++rowIndex)
                        {
                            RenderCell cell = grid[rowIndex, columnIndex];
                            
                            var cellArea = new Rectangle(
                                TranslateCoordWithMargin((cell.Column + cell.Row) * Settings.CellSize / 2), 
                                TranslateCoordWithMargin((grid.ColumnCount - cell.Column - 1 + cell.Row) * Settings.CellSize / 4), 
                                Settings.CellSize, 
                                Settings.CellSize / 2);

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
                    }
                });
                
                image.Save(stream, ImageFormats.Png);
            }
        }
    }
}
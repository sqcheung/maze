using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Maze.Common;
using Maze.Common.Algorithms;
using Maze.GameLevelGenerator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Overlays;
using C = System.Console;

namespace Maze.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(5, 10);
            new AldosBroderMazeAlgorithm().Update(grid);
            var renderGrid = new RenderGrid(grid);

            using (FileStream stream = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png")))
            {
                new GridRenderer().Render(renderGrid, stream);
            }
        }
    }
    
    public class GridRenderer : IDisposable
    {
        readonly LovelyRandomTreeCellRenderer _cellRenderer;
        readonly LovelyTreeBackgroundRenderer _backgroundRenderer;
        const int CellSize = 86;

        public GridRenderer()
        {
            try
            {
                _cellRenderer = new LovelyRandomTreeCellRenderer();
                _backgroundRenderer = new LovelyTreeBackgroundRenderer();
            }
            catch
            {
                DisposeResources();
                throw;
            }
        }
        
        public void Render(RenderGrid grid, Stream stream)
        {
            int width = grid.ColumnCount * CellSize;
            int height = grid.RowCount * CellSize;
            using (var image = new Image<Rgba32>(width, height))
            {
                image.Mutate();
                image.Mutate(context =>
                {
                    _backgroundRenderer.Render(context, new Rectangle(0, 0, width, height));
                    foreach (RenderCell cell in grid.GetCells())
                    {
                        if (cell.RenderType == RenderType.Wall)
                        {
                            _cellRenderer.Render(
                                context, new Rectangle(cell.Column * CellSize, cell.Row * CellSize, CellSize, CellSize), cell);
                        }
                    }
                });
                image.Save(stream, ImageFormats.Png);
            }
        }

        public void Dispose()
        {
            DisposeResources();
        }

        void DisposeResources()
        {
            _cellRenderer?.Dispose();
        }
    }
}
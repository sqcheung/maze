using System;
using System.Collections.Generic;
using System.IO;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Maze.GameLevelGenerator
{
    public class GameLevelRenderer : IDisposable
    {
        readonly DisposableCollection<AreaRenderer> _backgroundRenderers;
        readonly DisposableCollection<CellRenderer> _groundRenderers;
        readonly DisposableCollection<CellRenderer> _wallRenderers;
        readonly DisposableCollection<AreaRenderer> _atomsphereRenderers;
        readonly GameLevelRendererSettings _settings;
        readonly bool _disposeRenderersWhenClose;
        bool _isDisposed;

        public GameLevelRenderer(
            IEnumerable<AreaRenderer> backgroundRenderer,
            IEnumerable<CellRenderer> groundRenderers,
            IEnumerable<CellRenderer> wallRenderers,
            IEnumerable<AreaRenderer> atomsphereRenderers,
            GameLevelRendererSettings settings,
            bool disposeRenderersWhenClose)
        {
            _backgroundRenderers = new DisposableCollection<AreaRenderer>(backgroundRenderer);
            _groundRenderers = new DisposableCollection<CellRenderer>(groundRenderers);
            _wallRenderers = new DisposableCollection<CellRenderer>(wallRenderers);
            _atomsphereRenderers = new DisposableCollection<AreaRenderer>(atomsphereRenderers);
            _settings = settings;
            _disposeRenderersWhenClose = disposeRenderersWhenClose;
        }
        
        public void Dispose()
        {
            if (_isDisposed) return;
            if (_disposeRenderersWhenClose)
            {
                _backgroundRenderers.Dispose();
                _groundRenderers.Dispose();
                _wallRenderers.Dispose();
                _atomsphereRenderers.Dispose();
            }

            _isDisposed = true;
        }

        int CalculateDimension(int length)
        {
            return length * _settings.CellSize + _settings.Margin * 2;
        }

        int TranslateCoordWithMargin(int original)
        {
            return original + _settings.Margin;
        } 
        
        public void Render(RenderGrid grid, Stream stream)
        {
            int width = CalculateDimension(grid.ColumnCount);
            int height = CalculateDimension(grid.RowCount);
            using (var image = new Image<Rgba32>(width, height))
            {
                image.Mutate();
                image.Mutate(context =>
                {
                    var fullArea = new Rectangle(0, 0, width, height);
                    
                    foreach (AreaRenderer backgroundRenderer in _backgroundRenderers)
                    {
                        backgroundRenderer.Render(context, fullArea);
                    }
                    
                    foreach (RenderCell cell in grid.GetCells())
                    {
                        var cellArea = new Rectangle(
                            TranslateCoordWithMargin(cell.Column * _settings.CellSize), 
                            TranslateCoordWithMargin(cell.Row * _settings.CellSize), 
                            _settings.CellSize, 
                            _settings.CellSize);

                        if (cell.RenderType == RenderType.Ground)
                        {
                            foreach (CellRenderer groundRenderer in _groundRenderers)
                            {
                                groundRenderer.Render(context, cellArea, cell);
                            }
                        }

                        if (cell.RenderType == RenderType.Wall)
                        {
                            foreach (CellRenderer wallRenderer in _wallRenderers)
                            {
                                wallRenderer.Render(context, cellArea, cell);
                            }
                        }
                    }
                    
                    foreach (AreaRenderer atomsphereRenderer in _atomsphereRenderers)
                    {
                        atomsphereRenderer.Render(context, fullArea);
                    }
                });
                
                image.Save(stream, ImageFormats.Png);
            }
        }
    }
}
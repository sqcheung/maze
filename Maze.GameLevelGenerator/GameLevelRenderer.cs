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
        bool _isDisposed;

        static DisposableCollection<T> ToDisposableCollection<T>(IEnumerable<T> collection)
            where T : IDisposable
        {
            return new DisposableCollection<T>(collection);
        }

        public GameLevelRenderer(IGameLevelComponentFactory factory)
        {
            _backgroundRenderers = ToDisposableCollection(factory.CreateBackgroundRenderers());
            _groundRenderers = ToDisposableCollection(factory.CreateGroundRenderers());
            _wallRenderers = ToDisposableCollection(factory.CreateWallRenderers());
            _atomsphereRenderers = ToDisposableCollection(factory.CreateAtomsphereRenderers());
            _settings = factory.CreateLevelSettings();
        }
        
        public void Dispose()
        {
            if (_isDisposed) return;
            
            _backgroundRenderers.Dispose();
            _groundRenderers.Dispose();
            _wallRenderers.Dispose();
            _atomsphereRenderers.Dispose();
            
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
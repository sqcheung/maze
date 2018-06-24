using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.Common.Renderers
{
    public class DirectedCellRenderer : KeyedImageCellRenderer
    {
        public DirectedCellRenderer(
            Image<Rgba32> north,
            Image<Rgba32> south,
            Image<Rgba32> east,
            Image<Rgba32> west,
            Image<Rgba32> northSouth,
            Image<Rgba32> northEast,
            Image<Rgba32> northWest,
            Image<Rgba32> southEast,
            Image<Rgba32> southWest,
            Image<Rgba32> eastWest,
            Image<Rgba32> northSouthEast,
            Image<Rgba32> northSouthWest,
            Image<Rgba32> northEastWest,
            Image<Rgba32> southEastWest,
            Image<Rgba32> northSouthEastWest) : base(CreateDirectedResources(
                north,
                south,
                east,
                west,
                northSouth,
                northEast,
                northWest,
                southEast,
                southWest,
                eastWest,
                northSouthEast,
                northSouthWest,
                northEastWest,
                southEastWest,
                northSouthEastWest), true)
        {
        }

        static IDictionary<string, Image<Rgba32>> CreateDirectedResources(
            Image<Rgba32> north, Image<Rgba32> south, Image<Rgba32> east, Image<Rgba32> west, Image<Rgba32> northSouth, 
            Image<Rgba32> northEast, Image<Rgba32> northWest, Image<Rgba32> southEast, Image<Rgba32> southWest, 
            Image<Rgba32> eastWest, Image<Rgba32> northSouthEast, Image<Rgba32> northSouthWest, 
            Image<Rgba32> northEastWest, Image<Rgba32> southEastWest, Image<Rgba32> northSouthEastWest)
        {
            return new Dictionary<string, Image<Rgba32>>
            {
                {"North", north},
                {"South", south},
                {"East", east},
                {"West", west},
                {"NorthSouth", northSouth},
                {"NorthEast", northEast},
                {"NorthWest", northWest},
                {"SouthEast", southEast},
                {"SouthWest", southWest},
                {"EastWest", eastWest},
                {"NorthSouthEast", northSouthEast},
                {"NorthSouthWest", northSouthWest},
                {"NorthEastWest", northEastWest},
                {"SouthEastWest", southEastWest},
                {"NorthSouthEastWest", northSouthEastWest},
            };
        }

        protected override bool IsSupported(RenderCell cell)
        {
            return true;
        }

        protected override string GetTextureKey(RenderCell cell)
        {
            Direction cellDirection = cell.Direction;
            var keyBuilder = new StringBuilder();
            if ((cellDirection & Direction.North) == Direction.North) keyBuilder.Append("North");
            if ((cellDirection & Direction.South) == Direction.South) keyBuilder.Append("South");
            if ((cellDirection & Direction.East) == Direction.East) keyBuilder.Append("East");
            if ((cellDirection & Direction.West) == Direction.West) keyBuilder.Append("West");
            return keyBuilder.ToString();
        }
    }
}
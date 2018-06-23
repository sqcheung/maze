using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Maze.Common;
using Maze.Common.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator
{
    public class GrassWallRenderer : KeyedImageCellRenderer
    {
        static IDictionary<string, Image<Rgba32>> LoadResources()
        {
            Assembly assembly = typeof(GrassWallRenderer).Assembly;
            Image<Rgba32> east = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east.png");
            Image<Rgba32> eastWest = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east_west.png");
            Image<Rgba32> north = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north.png");
            Image<Rgba32> northEast = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_east.png");
            Image<Rgba32> northSouth = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_south.png");
            Image<Rgba32> northWest = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_west.png");
            Image<Rgba32> south = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south.png");
            Image<Rgba32> southEast = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east.png");
            Image<Rgba32> southEastWest = 
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east_west.png");
            Image<Rgba32> southWest =
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_west.png");
            Image<Rgba32> west =
                assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_west.png");
            
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
                {"NorthSouthEast", southEast},
                {"NorthSouthWest", southWest},
                {"NorthEastWest", eastWest},
                {"SouthEastWest", southEastWest},
                {"NorthSouthEastWest", southEastWest},
            };
        }
        
        public GrassWallRenderer() : base(LoadResources(), true)
        {
        }

        protected override bool IsSupported(RenderCell cell)
        {
            return cell.RenderType == RenderType.Wall;
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
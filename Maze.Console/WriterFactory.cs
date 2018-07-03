using Maze.GameLevelGenerator.Components;

namespace Maze.Console
{
    class WriterFactory
    {
        public IWriter CreateWriter(string mazeKind)
        {
            switch (mazeKind)
            {
                case "tree": return new TreeLevelWriter();
                case "grass": return new GrassLevelWriter();
                case "city": return new CityLevelWriter();
                case "town": return new TownLevelWriter();
                case "color": return new ColorLevelWriter();
                default:
                    return null;
            }
        }
    }
}
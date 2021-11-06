using RogueSharp;
using SibRus.Core;

namespace SibRus.Systems
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;

        private readonly DungeonMap _map;

        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            _map.Initialize( _width, _height);
            foreach (Cell cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            foreach (Cell cell in _map.GetCellsInRows( 0, _height - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            foreach (Cell cell in _map.GetCellsInColumns(0, _width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return _map;
        }
    }
}

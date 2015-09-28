using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    public class Tile
    {
        public TileType type;
        public List<IObject> objectsOnTile;

        public enum TileType
        {
            Empty,
            Wall,
            Floor,
            Slope,
            Teleport,
            Tower,
            Bad,
            Door,
            Slow,
            OneByOne,
            BreakPass
        }

        public Tile(TileType _type)
        {
            type = _type;
            objectsOnTile = new List<IObject>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    public class Tile
    {
        public int x;
        public int y;
        public List<IObject> objectsOnTile;
        private TileType type;

        public TileType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnTileChanged(EventArgs.Empty);
            }
        }

        public event EventHandler TileChanged;

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

        public void Initialize(TileType _type, int _x, int _y)
        {
            x = _x;
            y = _y;
            Type = _type;
            objectsOnTile = new List<IObject>();
        }

        // Notify the terrain.
        private void OnTileChanged(EventArgs e)
        {
            if (TileChanged != null)
            {
                TileChanged(this, e);
            }
        }
    }
}

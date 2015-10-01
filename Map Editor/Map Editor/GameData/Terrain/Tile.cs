using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    [DefaultPropertyAttribute("Name")]
    public class Tile
    {
        public string path;
        public int x;
        public int y;
        [CategoryAttribute("Speed"), DescriptionAttribute("Changes the speed of entities walking on the tile.")]
        public int speedModifier { get; set; }
        public Orientation orientation;

        //public List<IObject> objectsOnTile;
        private TileType type;
        

        public enum Orientation { Top, Right, Bottom, Left }
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

        [field: NonSerialized]
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
            //objectsOnTile = new List<IObject>();
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

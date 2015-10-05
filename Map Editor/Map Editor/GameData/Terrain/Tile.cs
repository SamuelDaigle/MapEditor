using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    public class Position
    {
        public Position(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public Position()
        {
        }
        public int x;
        public int y;
    }

    [DefaultPropertyAttribute("Name")]
    public class Tile
    {
        public string path;
        public Position position;
        public int speedModifier { get; set; }
        public Orientation orientation { get; set; }
        public Point[] wayPoints { get; set; }
      

        public GameObject objectOnTile;
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
            wayPoints = new Point[25];
            position = new Position(_x, _y);
            Type = _type;
            if (objectOnTile == null)
            {
                objectOnTile = new GameObject();
            }
        }

        public void UnsetEvents()
        {
            objectOnTile.ObjectChanged -= OnObjectChanged;
        }

        public void SetEvents()
        {
            objectOnTile.ObjectChanged += OnObjectChanged;
        }

        // Notify the tile.
        private void OnObjectChanged(object sender, EventArgs e)
        {
            OnTileChanged(e);
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

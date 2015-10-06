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

    [DefaultPropertyAttribute("Name")]
    public class Tile
    {
        public string path;
        public Point position { get; set; }
        public int speedModifier { get; set; }
        public Orientation orientation { get; set; }
        public Point[] wayPoints { get; set; }
        public Point teleportPoint { get; set; }
      

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
            [Description("../../Resources/Tile/Empty.png")]
            Empty,
            [Description("../../Resources/Tile/Wall.png")]
            Wall,
            [Description("../../Resources/Tile/Floor.png")]
            Floor,
            [Description("../../Resources/Tile/Slope.png")]
            Slope,
            [Description("../../Resources/Tile/Teleport.png")]
            Teleport,
            [Description("../../Resources/Tile/Tower.png")]
            Tower,
            [Description("../../Resources/Tile/Bad.png")]
            Bad,
            [Description("../../Resources/Tile/Door.png")]
            Door,
            [Description("../../Resources/Tile/Slow.png")]
            Slow,
            [Description("../../Resources/Tile/OneByOne.png")]
            OneByOne,
            [Description("../../Resources/Tile/Breaking.png")]
            BreakPass
        }

        public void Initialize(TileType _type, int _x, int _y)
        {
            wayPoints = new Point[25];
            position = new Point(_x, _y);
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

        public static string ToDescriptionString(TileType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public void SetTile(string _path)
        {
            foreach (TileType type in Enum.GetValues(typeof(TileType)))
            {
                if (ToDescriptionString(type) == _path)
                {
                    this.type = type;
                    break;
                }
            }

            OnTileChanged(EventArgs.Empty);
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

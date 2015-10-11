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

    /// <summary>
    /// Represents a tile, can contain an object on it.
    /// </summary>
    [DefaultPropertyAttribute("Name")]
    public class Tile
    {
        public string path;
        public Point position { get; set; }
        public int speedModifier { get; set; }
        public Orientation orientation { get; set; }

        [XmlIgnore]
        public Point[] wayPoints { get; set; }
        public Point teleportPoint { get; set; }
      
        public TileObject objectOnTile;
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

        /// <summary>
        /// Initializes the specified _type.
        /// </summary>
        /// <param name="_type">The _type.</param>
        /// <param name="_x">The _x.</param>
        /// <param name="_y">The _y.</param>
        public void Initialize(TileType _type, int _x, int _y)
        {
            wayPoints = new Point[25];
            position = new Point(_x, _y);
            Type = _type;
            if (objectOnTile == null)
            {
                objectOnTile = new TileObject();
            }
        }

        /// <summary>
        /// Unsets the events.
        /// </summary>
        public void UnsetEvents()
        {
            objectOnTile.ObjectChanged -= OnObjectChanged;
        }

        /// <summary>
        /// Sets the events.
        /// </summary>
        public void SetEvents()
        {
            objectOnTile.ObjectChanged += OnObjectChanged;
        }

        /// <summary>
        /// Gets the path of the type, with its description.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static string ToDescriptionString(TileType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Sets the tile with the path.
        /// </summary>
        /// <param name="_path">The _path.</param>
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
        /// <summary>
        /// Called when [object changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnObjectChanged(object sender, EventArgs e)
        {
            OnTileChanged(e);
        }

        // Notify the terrain.
        /// <summary>
        /// Raises the <see cref="E:TileChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnTileChanged(EventArgs e)
        {
            if (TileChanged != null)
            {
                TileChanged(this, e);
            }
        }
    }
}

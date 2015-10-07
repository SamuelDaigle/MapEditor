using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    /// <summary>
    /// Represents a floor and contains a 2D array of tiles.
    /// </summary>
    public class Floor
    {
        public int width;
        public int height;
        private Tile[][] tiles;

        public Tile[] Tiles
        {
            get
            {
                Tile[] result = new Tile[height * width];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        result[y * width + x] = tiles[y][x];
                    }
                }
                return result;
            }
            set
            {
                Initialize(width, height, Tile.TileType.Empty);
                Tile[] readData = value;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        tiles[y][x].Type = readData[y * width + x].Type;
                        tiles[y][x].path = readData[y * width + x].path;
                        tiles[y][x].objectOnTile = readData[y * width + x].objectOnTile;
                        tiles[y][x].teleportPoint = readData[y *width + x].teleportPoint;
                        tiles[y][x].speedModifier = readData[y * width + x].speedModifier;
                        tiles[y][x].orientation = readData[y * width + x].orientation;
                        tiles[y][x].wayPoints = readData[y * width + x].wayPoints;
                    }
                }
            }
        }

        public event EventHandler TerrainChanged;

        /// <summary>
        /// Initializes the specified _width.
        /// </summary>
        /// <param name="_width">The _width.</param>
        /// <param name="_height">The _height.</param>
        /// <param name="_type">The _type.</param>
        public void Initialize(int _width, int _height, Tile.TileType _type)
        {
            width = _width;
            height = _height;
            tiles = new Tile[_height][];
            for (int y = 0; y < _height; y++)
            {
                tiles[y] = new Tile[_width];
                for (int x = 0; x < _width; x++)
                {
                    tiles[y][x] = new Tile();
                    tiles[y][x].TileChanged += OnTerrainChanged;
                    tiles[y][x].Initialize(_type, x, y);
                }
            }
        }

        /// <summary>
        /// Unsets the events.
        /// </summary>
        public void UnsetEvents()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[y][x].TileChanged -= OnTerrainChanged;
                    tiles[y][x].UnsetEvents();
                }
            }
        }

        /// <summary>
        /// Sets the events.
        /// </summary>
        public void SetEvents()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[y][x].TileChanged += OnTerrainChanged;
                    tiles[y][x].SetEvents();
                }
            }
        }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="_X">The _ x.</param>
        /// <param name="_Y">The _ y.</param>
        /// <returns></returns>
        public Tile GetTile(int _X, int _Y)
        {
            if (_X >= 0 && _X < width && _Y >= 0 && _Y < height)
            {
                return tiles[_Y][_X];
            }
            return null;
        }

        /// <summary>
        /// Sets the tile.
        /// </summary>
        /// <param name="_X">The _ x.</param>
        /// <param name="_Y">The _ y.</param>
        /// <param name="_tile">The _tile.</param>
        public void SetTile(int _X, int _Y, Tile _tile)
        {
            tiles[_Y][_X] = _tile;
        }

        // Received by the tile.
        /// <summary>
        /// Called when [tile changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnTileChanged(object sender, EventArgs e)
        {
            OnTerrainChanged(sender, e);
        }


        // Notify the scene.
        /// <summary>
        /// Called when [terrain changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnTerrainChanged(object sender, EventArgs e)
        {
            if (TerrainChanged != null)
            {
                TerrainChanged(sender, e);
            }
        }
    }
}

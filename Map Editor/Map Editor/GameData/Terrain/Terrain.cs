using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    public class Terrain
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
                        result[x * width + y] = tiles[x][y];
                    }
                }
                return result;
            }
            set
            {
                Initialize(width, height);
                Tile[] readData = value;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        tiles[x][y] = readData[x * width + y];
                    }
                }
            }
        }

        [field: NonSerialized]
        public event EventHandler TerrainChanged;

        public void Initialize(int _width, int _height)
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
                    tiles[y][x].Initialize(Tile.TileType.Empty, x, y);
                }
            }
        }

        public Tile GetTile(int _X, int _Y)
        {
            return tiles[_Y][_X];
        }

        // Received by the tile.
        private void OnTileChanged(object sender, EventArgs e)
        {
            OnTerrainChanged(sender, e);
        }


        // Notify the scene.
        private void OnTerrainChanged(object sender, EventArgs e)
        {
            if (TerrainChanged != null)
            {
                TerrainChanged(sender, e);
            }
        }
    }
}

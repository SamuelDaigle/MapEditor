using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
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
            UpdateTiles();
        }

        public Tile GetTile(int _X, int _Y)
        {
            return tiles[_Y][_X];
        }

        public void SetTile(int _X, int _Y, Tile _tile)
        {
            tiles[_Y][_X] = _tile;
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

        private void UpdateTiles()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // triggers on tile changed event.
                    tiles[y][x].objectOnTile.OnObjectChanged(EventArgs.Empty);
                }
            }
        }
    }
}

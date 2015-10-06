using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    public class Scene
    {
        public string name;
        public Floor selectedFloor;

        public List<Floor> floors;
        public int floorWidth;
        public int floorHeight;

        public event EventHandler SceneChanged;
        public event EventHandler FloorAdded;

        public Scene()
        {
            floors = new List<Floor>();
            selectedFloor = new Floor();
        }

        private void CreateNewScene()
        {
            floors = new List<Floor>();
            AddFloor();
        }

        private void LoadScene(Scene _other)
        {
            floors = new List<Floor>();
            foreach (Floor floor in _other.floors)
            {
                AddFloor();
                floors[floors.Count - 1] = floor;
            }
        }

        public void UnsetEvents()
        {
            selectedFloor.TerrainChanged -= OnTerrainChanged;
            selectedFloor.UnsetEvents();
        }

        public void SetEvents()
        {
            selectedFloor.TerrainChanged += OnTerrainChanged;
            selectedFloor.SetEvents();
        }

        public void LoadFromFile(string _filepath)
        {
            XmlCustomSerializer<Scene> sceneXML = new XmlCustomSerializer<Scene>(_filepath);
            Scene loadedScene = sceneXML.Load();
            if (loadedScene != null)
            {
                LoadScene(loadedScene);
            }
            else
            {
                CreateNewScene();
            }
            OnSceneChanged(this, EventArgs.Empty);
        }

        public void AddFloor()
        {
            Floor floor = new Floor();
            floor.Initialize(floorWidth, floorHeight, Tile.TileType.Empty);
            selectedFloor = floor;
            SetEvents();
            floors.Add(floor);
            OnFloorAdded(floor, EventArgs.Empty);
        }

        private void SetAllTiles(Tile.TileType _type)
        {
            for (int y = 0; y < floorWidth; y++)
            {
                for (int x = 0; x < floorHeight; x++)
                {
                    selectedFloor.GetTile(x, y).Type = _type;
                }
            }
        }

        public Tile GetTopTile(int _X, int _Y)
        {
            Tile tile = null;
            for (int i = floors.Count - 1; i >= 0; i--)
            {
                if (floors[i].GetTile(_X, _Y).Type != Tile.TileType.Empty)
                {
                    tile = floors[i].GetTile(_X, _Y);
                    break;
                }
            }
            return tile;
        }

        public void SelectTopFloor()
        {
            Floor topFloor = new Floor();
            topFloor.Initialize(floorWidth, floorHeight, Tile.TileType.Empty);
            for (int y = 0; y < floorHeight; y++)
            {
                for (int x = 0; x < floorWidth; x++)
                {
                    topFloor.SetTile(x, y, GetTopTile(x, y));
                }
            }
        }

        public void SelectFloor(int _id)
        {
            selectedFloor = floors[_id];
            OnSceneChanged(this, EventArgs.Empty);
        }

        // Received the tile by the terrain.
        private void OnTerrainChanged(object sender, EventArgs e)
        {
            OnSceneChanged(sender, e);
        }

        // Notify the view.
        private void OnSceneChanged(object sender, EventArgs e)
        {
            if (SceneChanged != null)
            {
                SceneChanged(sender, e);
            }
        }

        // Notify the view.
        private void OnFloorAdded(object sender, EventArgs e)
        {
            if (FloorAdded != null)
            {
                FloorAdded(sender, e);
            }
        }

        public bool ValidateMap()
        {
           Validator validator = new Validator(this);
           return validator.ValidateMap();
        }


    }
}

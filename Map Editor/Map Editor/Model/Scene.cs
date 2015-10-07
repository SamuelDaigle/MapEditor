using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    /// <summary>
    /// Model, represents the scene, has all the floors and have more logic to it.
    /// </summary>
    public class Scene
    {
        public static int MAX_FLOOR = 5;

        public string name;
        public Floor selectedFloor;

        public List<Floor> floors;
        public int floorWidth;
        public int floorHeight;

        public event EventHandler SceneChanged;
        public event EventHandler FloorAdded;

        private int floorID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            floors = new List<Floor>();
            selectedFloor = new Floor();
        }

        /// <summary>
        /// Creates the new scene.
        /// </summary>
        private void CreateNewScene()
        {
            floors = new List<Floor>();
            AddFloor();
        }

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="_other">The _other.</param>
        private void LoadScene(Scene _other)
        {
            floors = new List<Floor>();
            this.name = _other.name;
            this.floorWidth = _other.floorWidth;
            this.floorHeight = _other.floorHeight;

            foreach (Floor floor in _other.floors)
            {
                AddFloor();
                floors[floors.Count - 1] = floor;
            }
        }

        /// <summary>
        /// Unsets the events.
        /// </summary>
        public void UnsetEvents()
        {
            selectedFloor.TerrainChanged -= OnTerrainChanged;
            selectedFloor.UnsetEvents();
        }

        /// <summary>
        /// Sets the events.
        /// </summary>
        public void SetEvents()
        {
            selectedFloor.TerrainChanged += OnTerrainChanged;
            selectedFloor.SetEvents();
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="_filepath">The _filepath.</param>
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
                OnSceneChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adds the floor.
        /// </summary>
        public void AddFloor()
        {
            if (floors.Count < 5)
            {
                Floor floor = new Floor();
                floor.Initialize(floorWidth, floorHeight, Tile.TileType.Empty);
                floors.Add(floor);
                SelectFloor(floors.Count - 1);
                SetEvents();
                OnFloorAdded(floor, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Sets all tiles.
        /// </summary>
        /// <param name="_type">The _type.</param>
        public void SetAllTiles(Tile.TileType _type)
        {
            for (int y = 0; y < floorHeight; y++)
            {
                for (int x = 0; x < floorWidth; x++)
                {
                    selectedFloor.GetTile(x, y).Type = _type;
                }
            }
        }

        /// <summary>
        /// Gets the top tile.
        /// </summary>
        /// <param name="_X">The _ x.</param>
        /// <param name="_Y">The _ y.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Selects the top floor.
        /// </summary>
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
            selectedFloor = topFloor;
            OnSceneChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Selects the floor.
        /// </summary>
        /// <param name="_id">The _id.</param>
        public void SelectFloor(int _id)
        {
            floorID = _id;
            selectedFloor = floors[_id];
            OnSceneChanged(this, EventArgs.Empty);
        }

        // Received the tile by the terrain.
        /// <summary>
        /// Called when [terrain changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnTerrainChanged(object sender, EventArgs e)
        {
            floors[floorID] = selectedFloor;
            OnSceneChanged(sender, e);
        }

        // Notify the view.
        /// <summary>
        /// Called when [scene changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSceneChanged(object sender, EventArgs e)
        {
            if (SceneChanged != null)
            {
                SceneChanged(sender, e);
            }
        }

        // Notify the view.
        /// <summary>
        /// Called when [floor added].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnFloorAdded(object sender, EventArgs e)
        {
            if (FloorAdded != null)
            {
                FloorAdded(sender, e);
            }
        }

        /// <summary>
        /// Validates the map.
        /// </summary>
        /// <returns></returns>
        public bool ValidateMap()
        {
            Validator validator = new Validator(this);
            return validator.ValidateMap();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    public class Scene
    {
        public string name;
        public Floor selectedTerrain;

        public List<Floor> floors;

        public event EventHandler SceneChanged;

        public Scene()
        {
            floors = new List<Floor>();
            selectedTerrain = new Floor();
            SetEvents();
        }

        public void UnsetEvents()
        {
            selectedTerrain.TerrainChanged -= OnTerrainChanged;
            selectedTerrain.UnsetEvents();
        }

        public void SetEvents()
        {
            selectedTerrain.TerrainChanged += OnTerrainChanged;
            selectedTerrain.SetEvents();
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

        public bool ValidateMap()
        {
            int numberOfSpawns = 0;
            int numberOfBalls = 0;
            int numberOfGoal = 0;

            foreach (Floor F in floors)
            {
                foreach (Tile T in F.Tiles)
                {
                    if (T.objectOnTile.utilType == GameObject.UtilType.Goal)
                    {
                        numberOfGoal++;
                    }
                    if (T.objectOnTile.utilType == GameObject.UtilType.Spawn)
                    {
                        numberOfSpawns++;
                    }
                    if (T.objectOnTile.utilType == GameObject.UtilType.Ball)
                    {
                        numberOfBalls++;
                    }
                }
                
            }
            if (numberOfGoal == 1 && numberOfBalls >= 1 && numberOfSpawns == 4)
            {
                return true;
            }
            return false;
        }
    }
}

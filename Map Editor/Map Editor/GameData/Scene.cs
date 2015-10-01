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
        }

        public void SetEvents()
        {
            selectedTerrain.TerrainChanged += OnTerrainChanged;
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
    }
}
